using GraphQLPlayground.Data;
using HotChocolate;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;
using HotChocolate.Types;
using System.Threading;
using System.Threading.Tasks;

namespace GraphQLPlayground
{
    public class Subscription
    {
        [Subscribe]
        [Topic]
        public Book OnBookReleased([EventMessage] Book book) => book;

        [Subscribe(With = nameof(SubscribeToOnPostCreated))]
        public AuthorPostCreated OnPostCreated(
            int authorId,
            [EventMessage] Post post,
            CancellationToken cancellationToken)
        {
            return new AuthorPostCreated(post);
        }

        public ValueTask<ISourceStream<Post>> SubscribeToOnPostCreated(
            int authorId,
            [Service] ITopicEventReceiver eventReceiver,
            CancellationToken cancellationToken) => eventReceiver.SubscribeAsync<string, Post>(
                "AuthorChannel" + authorId, cancellationToken);

        [Subscribe]
        [Topic]
        public Author OnAuthorReleased([EventMessage] Author author) => author;
    }
}
