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
    public class EditModalModel : BloggingPageModel
    {
        [BindProperty]
        public EditPostViewModel Post { get; set; }

        public List<SelectListItem> Authors { get; set; }


        private readonly IPostAppService _postAppService;

        public EditModalModel(IPostAppService postAppService)
        {
            _postAppService = postAppService;
        }

        public async Task OnGetAsync(Guid Id)
        {
            var postDto = await _postAppService.GetAsync(Id);
            Post = ObjectMapper.Map<PostDto, EditPostViewModel>(postDto);

            var authorLookup = await _postAppService.GetAuthorLookupAsync();
            Authors = authorLookup.Items
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                .ToList();

        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _postAppService.UpdateAsync(
                Post.Id,
                ObjectMapper.Map<EditPostViewModel, UpdatePostDto>(Post)
                );
            return NoContent();
        }

        public class EditPostViewModel
        {
            [HiddenInput]
            [BindProperty(SupportsGet = true)]
            public Guid Id { get; set; }

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
