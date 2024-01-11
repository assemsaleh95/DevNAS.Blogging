using System;
using System.ComponentModel.DataAnnotations;

namespace DevNAS.Blogging.Posts
{
    public class UpdatePostDto
    {
        [Required]
        [StringLength(128)]
        public string? Title { get; set; } = string.Empty;

        [Required]
        public string ShortDescription { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public PostType Type { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; }

    }
}
