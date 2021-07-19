using GraphQLPlayground.Data;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Subscriptions;
using System.Threading;
using System.Threading.Tasks;

namespace GraphQLPlayground
{
    public class Mutation
    {
        [UseDbContext(typeof(BookContext))]
        public async Task<AddAuthorPayload> AddAuthorAsync(
            AddAuthorInput input,
            [Service] ITopicEventSender eventSender,
            [ScopedService] BookContext dbContext,
            CancellationToken cancellationToken)
        {
            var author = new Author
            {
                Name = input.Name
            };

            dbContext.Authors.Add(author);
            await dbContext.SaveChangesAsync();
            await eventSender.SendAsync(nameof(Subscription.OnAuthorReleased), author, cancellationToken);

            return new AddAuthorPayload(author);
        }

        public async Task<CreatePostPayload> CreatePostAsync(
            CreatePostInput input,
            [Service] ITopicEventSender eventSender,
            CancellationToken cancellationToken)
        {
            var post = new Post
            {
                AuthorId = input.AuthorId,
                Description = input.Description,
            };

            await eventSender.SendAsync(
                "AuthorChannel" + input.AuthorId,
                post,
                cancellationToken);

            return new CreatePostPayload(post);
        }
        
        [UseDbContext(typeof(BookContext))]
        public async Task<AddBookPayload> AddBookAsync(
            AddBookInput input,
            [ScopedService] BookContext dbContext,
            [Service] ITopicEventSender eventSender,
            CancellationToken cancellationToken)
        {
            var book = new Book
            {
                Title = input.Title,
                AuthorId = input.AuthorId
            };

            dbContext.Books.Add(book);
            await dbContext.SaveChangesAsync(cancellationToken);

            await eventSender.SendAsync(nameof(Subscription.OnBookReleased), book, cancellationToken);

            return new AddBookPayload(book);
        }
    }
}
