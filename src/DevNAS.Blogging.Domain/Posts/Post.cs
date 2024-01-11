using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace DevNAS.Blogging.Posts
{
    public class Post : AuditedAggregateRoot<Guid>
    {
        public string Title { get; set; }

        public string ShortDescription { get; set; }
        public string Description { get; set; }

        public PostType Type { get; set; }

        public DateTime PublishDate { get; set; }

        public Guid AuthorId { get; set; }

    }
}
