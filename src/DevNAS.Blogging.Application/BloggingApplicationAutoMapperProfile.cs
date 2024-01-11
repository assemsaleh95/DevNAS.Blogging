using AutoMapper;
using DevNAS.Blogging.Authors;
using DevNAS.Blogging.Posts;

namespace DevNAS.Blogging;

public class BloggingApplicationAutoMapperProfile : Profile
{
    public BloggingApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<Post, PostDto>();
        CreateMap<CreatePostDto, Post>();
        CreateMap<UpdatePostDto, Post>();
        CreateMap<Author, AuthorDto>();
        CreateMap<Author, AuthorLookupDto>();
    }
}
