using Volo.Abp.Application.Dtos;

namespace DevNAS.Blogging.Authors
{
    public class GetAuthorListDto : PagedAndSortedResultRequestDto
    {
        public string? Filter { get; set; }
    }
}