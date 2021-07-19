using GraphQLPlayground.Data;
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
    public class AuthorByIdDataLoader : BatchDataLoader<int, Author>
    {
        private readonly IDbContextFactory<BookContext> dbContextFactory;

        public AuthorByIdDataLoader(
            IBatchScheduler batchScheduler,
            IDbContextFactory<BookContext> dbContextFactory) : base(batchScheduler)
        {
            this.dbContextFactory = dbContextFactory;
        }

        protected override async Task<IReadOnlyDictionary<int, Author>> LoadBatchAsync(IReadOnlyList<int> keys, CancellationToken cancellationToken)
        {
            await using var dbContext = dbContextFactory.CreateDbContext();

            return await dbContext.Authors
                .Where(x => keys.Contains(x.Id))
                .ToDictionaryAsync(x => x.Id);
        }
    }
}
