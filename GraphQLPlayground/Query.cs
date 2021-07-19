using System.Linq;
using GraphQLPlayground.Data;
using GraphQLPlayground.Types;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;

namespace GraphQLPlayground
{
    public class Query
    {
        [UseDbContext(typeof(BookContext))]
        [UsePaging]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Book> GetBooks([ScopedService] BookContext context) => context.Books;

        [UseDbContext(typeof(BookContext))]
        [UsePaging]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Author> GetAuthors([ScopedService] BookContext context) => context.Authors;
    }
}
