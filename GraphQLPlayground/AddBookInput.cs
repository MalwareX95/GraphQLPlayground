using GraphQLPlayground.Data;
using HotChocolate.Types.Relay;

namespace GraphQLPlayground
{
    public record AddBookInput(string Title, [ID(nameof(Author))]int AuthorId);
}
