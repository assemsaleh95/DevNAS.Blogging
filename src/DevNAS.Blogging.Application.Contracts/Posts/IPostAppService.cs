using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace DevNAS.Blogging.Posts
{
    public interface IPostAppService :
        ICrudAppService<
            PostDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreatePostDto,
            UpdatePostDto>
    {
        Task<ListResultDto<AuthorLookupDto>> GetAuthorLookupAsync();
    }
}
