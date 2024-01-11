using DevNAS.Blogging.Posts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace DevNAS.Blogging.Web.Pages.Posts
{
    public class CreateModalModel : BloggingPageModel
    {
        [BindProperty]
        public CreatePostViewModel Post { get; set; }

        public List<SelectListItem> Authors { get; set; }

        private readonly IPostAppService _postAppService;

        public CreateModalModel(IPostAppService postAppService)
        {
            _postAppService = postAppService;
        }

        public async void OnGet()
        {
            Post = new CreatePostViewModel();

            var authorLookup = await _postAppService.GetAuthorLookupAsync();
            Authors = authorLookup.Items.Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _postAppService.CreateAsync(
                ObjectMapper.Map<CreatePostViewModel, CreatePostDto>(Post)
                );
            return NoContent();
        }

        public class CreatePostViewModel
        {
            [SelectItems(nameof(Authors))]
            [DisplayName("Author")]
            public Guid AuthorId { get; set; }

            [Required]
            [StringLength(128)]
            public string? Title { get; set; } = string.Empty;

            [Required]
            [TextArea]
            public string ShortDescription { get; set; }

            [Required]
            [TextArea]
            public string Description { get; set; }

            [Required]
            public PostType Type { get; set; }

            [Required]
            [DataType(DataType.Date)]
            public DateTime PublishDate { get; set; }

        }

    }

}
