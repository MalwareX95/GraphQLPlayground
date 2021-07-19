using GreenDonut;
using HotChocolate.DataLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GraphQLPlayground.Dataloader
{
    public class BookDescriptionByIdDataLoader : BatchDataLoader<int, string?>
    {
        public BookDescriptionByIdDataLoader(IBatchScheduler batchScheduler, DataLoaderOptions<int>? options = null) 
            : base(batchScheduler, options)
        {
        }

        protected override async Task<IReadOnlyDictionary<int, string?>> LoadBatchAsync(IReadOnlyList<int> keys, CancellationToken cancellationToken)
        {
            return keys.ToDictionary(x => x, x => x.ToString());
        }
    }
}
