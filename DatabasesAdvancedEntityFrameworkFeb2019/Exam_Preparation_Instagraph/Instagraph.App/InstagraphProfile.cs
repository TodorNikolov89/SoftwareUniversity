using AutoMapper;
using Instagraph.DataProcessor.Dto.Import;
using Instagraph.Models;

namespace Instagraph.App
{
    public class InstagraphProfile : Profile
    {
        public InstagraphProfile()
        {
            CreateMap<PictureDto, Picture>();

            CreateMap<UserDto, User>();

            CreateMap<PostDto, Post>();
        }
    }
}
