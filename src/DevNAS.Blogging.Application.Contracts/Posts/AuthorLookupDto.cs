using System;
using Volo.Abp.Application.Dtos;

namespace DevNAS.Blogging.Posts
{
    public class AuthorLookupDto : EntityDto<Guid>
    {
        public string Name { get; set; }
    }
}
