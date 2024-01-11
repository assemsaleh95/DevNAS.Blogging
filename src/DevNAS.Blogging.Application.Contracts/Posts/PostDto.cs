using System;
using Volo.Abp.Application.Dtos;

namespace DevNAS.Blogging.Posts
{
    public class PostDto : AuditedEntityDto<Guid>
    {
        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public string Description { get; set; }

        public PostType Type { get; set; }

        public DateTime PublishDate { get; set; }

        public Guid AuthorId { get; set; }

        public string AuthorName { get; set; }

    }
}
