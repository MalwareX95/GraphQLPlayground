using HotChocolate.Types;

namespace GraphQLPlayground.Types
{
    public class BookType : ObjectType<Book>
    {
        protected override void Configure(IObjectTypeDescriptor<Book> descriptor)
        {
            //descriptor
            //    .ImplementsNode()
            //    .IdField(x => x.Id)
            //    .ResolveNode((ctx, id) => ctx.DataLoader<BookByIdDataLoader>().LoadAsync(id, ctx.RequestAborted));

            //descriptor
            //    .Field(x => x.AuthorId)
            //    .ID(nameof(Author));
        }
    }
}
