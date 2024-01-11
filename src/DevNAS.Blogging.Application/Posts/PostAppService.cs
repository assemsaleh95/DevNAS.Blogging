
using DevNAS.Blogging.Authors;
using DevNAS.Blogging.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace DevNAS.Blogging.Posts
{
    [Authorize(BloggingPermissions.Posts.Default)]
    public class PostAppService :
        CrudAppService<
            Post,
            PostDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreatePostDto,
            UpdatePostDto>, IPostAppService
    {
        private readonly IAuthorRepository _authorRepository;

        public PostAppService(IRepository<Post, Guid> repository, IAuthorRepository authorRepository) : base(repository)
        {
            _authorRepository = authorRepository;
            GetPolicyName = BloggingPermissions.Posts.Default;
            GetListPolicyName = BloggingPermissions.Posts.Default;
            CreatePolicyName = BloggingPermissions.Posts.Create;
            UpdatePolicyName = BloggingPermissions.Posts.Edit;
            DeletePolicyName = BloggingPermissions.Posts.Delete;
        }

        public override async Task<PostDto> GetAsync(Guid id)
        {
            //Get the IQueryable<Post> from the repository
            var queryable = await Repository.GetQueryableAsync();

            //Prepare a query to join posts and authors
            var query = from post in queryable
                        join author in await _authorRepository.GetQueryableAsync() on post.AuthorId equals author.Id
                        where post.Id == id
                        select new { post, author };

            //Execute the query and get the post with author
            var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
            if (queryResult == null)
            {
                throw new EntityNotFoundException(typeof(Post), id);
            }

            var postDto = ObjectMapper.Map<Post, PostDto>(queryResult.post);
            postDto.AuthorName = queryResult.author.Name;
            return postDto;
        }

        public override async Task<PagedResultDto<PostDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            //Get the IQueryable<Post> from the repository
            var queryable = await Repository.GetQueryableAsync();

            //Prepare a query to join posts and authors
            var query = from post in queryable
                        join author in await _authorRepository.GetQueryableAsync() on post.AuthorId equals author.Id
                        select new { post, author };

            //Paging
            query = query
                .OrderBy(NormalizeSorting(input.Sorting))
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

            //Execute the query and get a list
            var queryResult = await AsyncExecuter.ToListAsync(query);

            //Convert the query result to a list of PostDto objects
            var postDtos = queryResult.Select(x =>
            {
                var postDto = ObjectMapper.Map<Post, PostDto>(x.post);
                postDto.AuthorName = x.author.Name;
                return postDto;
            }).ToList();

            //Get the total count with another query
            var totalCount = await Repository.GetCountAsync();

            return new PagedResultDto<PostDto>(
                totalCount,
                postDtos
            );
        }


        public async Task<ListResultDto<AuthorLookupDto>> GetAuthorLookupAsync()
        {
            var authors = await _authorRepository.GetListAsync();

            return new ListResultDto<AuthorLookupDto>(
                ObjectMapper.Map<List<Author>, List<AuthorLookupDto>>(authors)
            );
        }

        private static string NormalizeSorting(string sorting)
        {
            if (sorting.IsNullOrEmpty())
            {
                return $"post.{nameof(Post.Title)}";
            }

            if (sorting.Contains("authorName", StringComparison.OrdinalIgnoreCase))
            {
                return sorting.Replace(
                    "authorName",
                    "author.Name",
                    StringComparison.OrdinalIgnoreCase
                );
            }

            return $"post.{sorting}";
        }

    }
}
