using DevNAS.Blogging.Authors;
using DevNAS.Blogging.Posts;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace DevNAS.Blogging
{
    public class BloggingDataSeederContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<Post, Guid> _postRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly AuthorManager _authorManager;


        public BloggingDataSeederContributor(IRepository<Post, Guid> postRepository, IAuthorRepository authorRepository,
        AuthorManager authorManager)
        {
            _postRepository = postRepository;
            _authorRepository = authorRepository;
            _authorManager = authorManager;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _postRepository.GetCountAsync() > 0)
            {
                return;
            }

            var orwell = await _authorRepository.InsertAsync(
                await _authorManager.CreateAsync(
                    "George Orwell",
                    new DateTime(1903, 06, 25),
                    "Orwell produced literary criticism and poetry, fiction and polemical journalism; and is best known for the allegorical novella Animal Farm (1945) and the dystopian novel Nineteen Eighty-Four (1949)."
                )
            );

            var douglas = await _authorRepository.InsertAsync(
                await _authorManager.CreateAsync(
                    "Douglas Adams",
                    new DateTime(1952, 03, 11),
                    "Douglas Adams was an English author, screenwriter, essayist, humorist, satirist and dramatist. Adams was an advocate for environmentalism and conservation, a lover of fast cars, technological innovation and the Apple Macintosh, and a self-proclaimed 'radical atheist'."
                )
            );



            await _postRepository.InsertAsync(
                new Post
                {
                    AuthorId = orwell.Id, // SET THE AUTHOR
                    Title = "Introduction to C# Programming",
                    ShortDescription = "A beginner's guide to C# programming language.",
                    Description = "A beginner's guide to C# programming language.A beginner's guide to C# programming language.A beginner's guide to C# programming language.A beginner's guide to C# programming language.A beginner's guide to C# programming language.A beginner's guide to C# programming language.A beginner's guide to C# programming language.",
                    Type = PostType.IT,
                    PublishDate = new DateTime(2022, 6, 8),
                },
                autoSave: true
            );

            await _postRepository.InsertAsync(
                new Post
                {
                    AuthorId = douglas.Id, // SET THE AUTHOR
                    Title = "Advancements in Machine Learning",
                    Description = "Exploring the latest developments in machine learning.Exploring the latest developments in machine learningExploring the latest developments in machine learningExploring the latest developments in machine learningExploring the latest developments in machine learningExploring the latest developments in machine learning",
                    ShortDescription = "Exploring the latest developments in machine learning",
                    Type = PostType.Engineering,
                    PublishDate = new DateTime(2023, 9, 27),
                },
                autoSave: true
            );
            await _postRepository.InsertAsync(
                new Post
                {
                    AuthorId = douglas.Id, // SET THE AUTHOR
                    Title = "Understanding Mechanical Engineering",
                    Description = "Exploring the fundamentals of mechanical engineering.Exploring the fundamentals of mechanical engineering.Exploring the fundamentals of mechanical engineering.Exploring the fundamentals of mechanical engineering.Exploring the fundamentals of mechanical engineering.Exploring the fundamentals of mechanical engineering.",
                    ShortDescription = "Exploring the fundamentals of mechanical engineering.",
                    Type = PostType.Engineering,
                    PublishDate = new DateTime(2023, 12, 27),
                },
                autoSave: true
            );


        }
    }
}
