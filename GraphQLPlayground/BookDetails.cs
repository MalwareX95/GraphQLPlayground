using HotChocolate.Data;
using HotChocolate.Types;
using System.Threading.Tasks;
using HotChocolate;
using System.Threading;
using GraphQLPlayground.Dataloader;

namespace GraphQLPlayground
{
    [ExtendObjectType(typeof(Book))]
    public class BookDetails
    {
        //[IsProjected]
        //public int GetId([Parent] Book book) => book.Id;

        public Task<string?> GetDescriptionAsync(
            [Parent] Book book,
            BookDescriptionByIdDataLoader detailsById,
            CancellationToken cancellationToken)
            => detailsById.LoadAsync(book.Id, cancellationToken);
    }
}
