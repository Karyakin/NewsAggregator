using AutoMapper;
using Entities.Entity.NewsEnt;
using Entities.Entity.Users;
using Entities.Models;
using Entities.Models.AssembledModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObject
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<News, NewsGetDTO>().ReverseMap();
            //CreateMap<News, NewsCategoryRssSourceDTO>().ReverseMap(); 

            CreateMap<News, NewsModel>().ReverseMap();
            CreateMap<Category, CategoryModel>().ReverseMap();
            CreateMap<RssSource, RssSourceDto>().ReverseMap();
            CreateMap<RssSource, RssSourceModel>().ReverseMap();

            CreateMap<RssSourceModel, RssSource>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(s => Guid.NewGuid()));

            CreateMap<RssSource, SourseWithNewsCategory>().ReverseMap();
            // CreateMap<IEnumerable<News>, IEnumerable<NewsInfoFromRssSourseDto>>();
            CreateMap<News, NewsInfoFromRssSourseDto>().ReverseMap();


            CreateMap<RegisterDto, User>().ReverseMap();
            CreateMap<RoleDto, Role>().ReverseMap();
            CreateMap<Comment, CommentDto>();
            CreateMap<CommentDto, Comment>();
          /*  CreateMap<IEnumerable<CommentDto>, IEnumerable<Comment>>().ReverseMap();*/
         /*   CreateMap<IEnumerable<Comment>, IEnumerable<CommentDto>>();*/



            CreateMap<NewsWithCommentsDTO, NewsGetDTO>();
            CreateMap<IEnumerable<NewsGetDTO>, IEnumerable<NewsWithCommentsDTO>>();




            //var res = news.Select(x=> _mapper.Map<NewsDto>(x).ToList())








        }
    }
}
