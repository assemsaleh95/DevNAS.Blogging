using AutoMapper;
using DevNAS.Blogging.Authors;
using DevNAS.Blogging.Posts;

namespace DevNAS.Blogging.Web;

public class BloggingWebAutoMapperProfile : Profile
{
    public BloggingWebAutoMapperProfile()
    {
        //Define your AutoMapper configuration here for the Web project.
        CreateMap<PostDto, CreatePostDto>();
        CreateMap<PostDto, UpdatePostDto>();

        CreateMap<Pages.Authors.CreateModalModel.CreateAuthorViewModel,
            CreateAuthorDto>();
        CreateMap<AuthorDto, Pages.Authors.EditModalModel.EditAuthorViewModel>();
        CreateMap<Pages.Authors.EditModalModel.EditAuthorViewModel,
                    UpdateAuthorDto>();

        CreateMap<Pages.Posts.CreateModalModel.CreatePostViewModel, CreatePostDto>();
        CreateMap<PostDto, Pages.Posts.EditModalModel.EditPostViewModel>();
        CreateMap<Pages.Posts.EditModalModel.EditPostViewModel, UpdatePostDto>();


    }
}
