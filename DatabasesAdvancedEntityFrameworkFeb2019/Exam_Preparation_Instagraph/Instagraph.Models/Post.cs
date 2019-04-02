using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Instagraph.Models
{
    public class Post
    {
        //        •	Id – an integer, Primary Key
        //•	Caption – a string
        //•	UserId – an integer
        //•	User – a User
        //•	PictureId – an integer
        //•	Picture – a Picture
        //•	Comments – a Collection of type Comment
        public Post()
        {
            this.Comments = new HashSet<Comment>();
        }

        public int Id { get; set; }

        [Required]
        public string Caption { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required]
        public int PictureId { get; set; }
        public Picture Picture { get; set; }

        public ICollection<Comment> Comments { get; set; }

    }
}
