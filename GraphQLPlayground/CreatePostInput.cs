using GraphQLPlayground.Data;

namespace GraphQLPlayground
{
    public record CreatePostInput(int AuthorId, string Description);

    public record CreatePostPayload(Post Post);

    public class AuthorPostCreated
    {
        public AuthorPostCreated(Post post)
        {
            Post = post;
        }

        public Post Post { get; }
    }
}
