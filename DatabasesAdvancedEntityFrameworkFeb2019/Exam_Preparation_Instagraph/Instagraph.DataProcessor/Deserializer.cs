using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;

using Newtonsoft.Json;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

using Instagraph.Data;
using Instagraph.Models;
using System.ComponentModel.DataAnnotations;
using Instagraph.DataProcessor.Dto.Import;
using System.Xml.Serialization;
using System.IO;

namespace Instagraph.DataProcessor
{
    public class Deserializer
    {
        public static string SuccessMessage = "Successfully imported {0} {1}.";
        public static string ErrorMessage = $"Error: Invalid data.";
        public static string SuccessMessageUserFollower = "Successfully imported Follower {0} to User {1}.";

        public static string ImportPictures(InstagraphContext context, string jsonString)
        {

            var deserializedPictures = JsonConvert.DeserializeObject<PictureDto[]>(jsonString);

            List<Picture> pictures = new List<Picture>();
            StringBuilder sb = new StringBuilder();

            foreach (var pictureDto in deserializedPictures)
            {
                if (!IsValid(pictureDto) || pictures.Any(p => p.Path.Equals(pictureDto.Path, StringComparison.OrdinalIgnoreCase)))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Picture picture = Mapper.Map<Picture>(pictureDto);
                pictures.Add(picture);
                sb.AppendLine(string.Format(SuccessMessage, nameof(Picture), picture.Path));

            }

            context.Pictures.AddRange(pictures);
            context.SaveChanges();

            string result = sb.ToString();

            return result;
        }

        public static string ImportUsers(InstagraphContext context, string jsonString)
        {
            var deserializedUsers = JsonConvert.DeserializeObject<UserDto[]>(jsonString);
            List<User> users = new List<User>();

            StringBuilder sb = new StringBuilder();

            foreach (var userDto in deserializedUsers)
            {
                if (!IsValid(userDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var profilePicture = context
                    .Pictures.FirstOrDefault(p => p.Path.Equals(userDto.ProfilePicture, StringComparison.OrdinalIgnoreCase));

                if (profilePicture == null)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                User user = new User
                {
                    Username = userDto.Username,
                    Password = userDto.Password,
                    ProfilePicture = profilePicture
                };

                users.Add(user);
                sb.AppendLine(string.Format(SuccessMessage, nameof(User), user.Username));
            }
            ;
            context.Users.AddRange(users);
            context.SaveChanges();
            string result = sb.ToString();

            return result;
        }

        public static string ImportFollowers(InstagraphContext context, string jsonString)
        {
            var deserializedUsers = JsonConvert.DeserializeObject<UserFollowerDto[]>(jsonString);

            List<UserFollower> usersFollowers = new List<UserFollower>();

            var users = context.Users;

            StringBuilder sb = new StringBuilder();

            foreach (var ufDto in deserializedUsers)
            {
                if (!IsValid(ufDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var user = context.Users
                    .SingleOrDefault(u => u.Username.Equals(ufDto.User, StringComparison.OrdinalIgnoreCase));
                if (user == null)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var follower = context.Users
                    .SingleOrDefault(u => u.Username.Equals(ufDto.Follower, StringComparison.OrdinalIgnoreCase));
                if (follower == null)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var isDuplicateEntry = usersFollowers
                    .Any(u => u.UserId == user.Id && u.FollowerId == follower.Id);
                if (isDuplicateEntry)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }


                UserFollower uf = new UserFollower
                {
                    UserId = user.Id,
                    FollowerId = follower.Id
                };

                sb.AppendLine(string.Format(SuccessMessageUserFollower, ufDto.Follower, ufDto.User));

                usersFollowers.Add(uf);

            }

            context.UsersFollowers.AddRange(usersFollowers);
            context.SaveChanges();

            string result = sb.ToString();
            return result;
        }

        public static string ImportPosts(InstagraphContext context, string xmlString)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(PostDto[]), new XmlRootAttribute("posts"));
            var deserializedPosts = (PostDto[])serializer.Deserialize(new MemoryStream(Encoding.UTF8.GetBytes(xmlString)));

            List<Post> posts = new List<Post>();
            StringBuilder sb = new StringBuilder();

            foreach (var dto in deserializedPosts)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                User user = context.Users.SingleOrDefault(u => u.Username.Equals(dto.User, StringComparison.OrdinalIgnoreCase));

                if (user == null)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Picture picture = context.Pictures.SingleOrDefault(u => u.Path.Equals(dto.Picture, StringComparison.OrdinalIgnoreCase));

                if (picture == null)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Post post = new Post
                {
                    Caption = dto.Caption,
                    UserId = user.Id,
                    PictureId = picture.Id
                };

                posts.Add(post);

                sb.AppendLine(string.Format(SuccessMessage, nameof(Post), post.Caption));
            }

            context.Posts.AddRange(posts);
            context.SaveChanges();

            string result = sb.ToString();

            return result;

        }

        public static string ImportComments(InstagraphContext context, string xmlString)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(CommentDto[]), new XmlRootAttribute("comments"));
            var deserializedComments = (CommentDto[])serializer.Deserialize(new MemoryStream(Encoding.UTF8.GetBytes(xmlString)));

            List<Comment> comments = new List<Comment>();

            StringBuilder sb = new StringBuilder();

            foreach (var dto in deserializedComments)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                User user = context.Users.SingleOrDefault(u => u.Username.Equals(dto.User, StringComparison.OrdinalIgnoreCase));

                if (user == null)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Post post = context.Posts.FirstOrDefault(p => p.Id == dto.Post.Id);

                if (post == null)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Comment comment = new Comment
                {
                    Content = dto.Content,
                    UserId = user.Id,
                    PostId = post.Id
                };

                comments.Add(comment);
                sb.AppendLine(string.Format(SuccessMessage, nameof(Comment), comment.Content));
            }
            context.Comments.AddRange(comments);
            context.SaveChanges();

            string result = sb.ToString();

            return result;

        }
        private static bool IsValid(object obj)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
            var validationresult = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(obj, validationContext, validationresult, true);
            return isValid;
        }
    }
}
