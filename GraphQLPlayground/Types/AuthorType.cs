using GraphQLPlayground.Data;
using HotChocolate.Types;

namespace GraphQLPlayground.Types
{
    public class AuthorType : ObjectType<Author>
    {
        protected override void Configure(IObjectTypeDescriptor<Author> descriptor)
        {
            //descriptor
            //    .ImplementsNode()
            //    .IdField(x => x.Id)
            //    .ResolveNode((ctx, id) => ctx.DataLoader<AuthorByIdDataLoader>().LoadAsync(id, ctx.RequestAborted));

            descriptor
                .Field(x => x.Books)
                .UseDbContext<BookContext>()
                //.UsePaging()
                .UseFiltering();
                //.UseSorting();

            descriptor.Description("Author of the book");
        }
    }
}
