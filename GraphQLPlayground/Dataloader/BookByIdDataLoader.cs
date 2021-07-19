using GreenDonut;
using HotChocolate.DataLoader;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GraphQLPlayground.Dataloader
{
    public class BookByIdDataLoader : BatchDataLoader<int, Book>
    {
        private readonly IDbContextFactory<BookContext> dbContextFactory;

        public BookByIdDataLoader(
            IBatchScheduler batchScheduler,
            IDbContextFactory<BookContext> dbContextFactory,
            DataLoaderOptions<int>? options = null) : base(batchScheduler, options)
        {
            this.dbContextFactory = dbContextFactory;
        }

        protected override async Task<IReadOnlyDictionary<int, Book>> LoadBatchAsync(IReadOnlyList<int> keys, CancellationToken cancellationToken)
        {
            await using var dbContext = dbContextFactory.CreateDbContext();

            return await dbContext.Books
                .Where(x => keys.Contains(x.Id))
                .ToDictionaryAsync(x => x.Id);
        }
    }
}
