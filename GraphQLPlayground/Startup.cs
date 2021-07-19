using GraphQLPlayground.Dataloader;
using GraphQLPlayground.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace GraphQLPlayground
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services
               .AddPooledDbContextFactory<BookContext>((s, o) => o
                    .UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Books;Integrated Security=True")
                    .LogTo(Console.WriteLine))
                .AddGraphQLServer()
                .AddQueryType<Query>()
                //.AddMutationType<Mutation>()
                //.AddSubscriptionType<Subscription>()
                //.AddTypeExtension<BookDetails>()
                .AddType<AuthorType>()
                .AddType<BookType>()
                .AddDataLoader<AuthorByIdDataLoader>()
                .AddDataLoader<BookByIdDataLoader>()
                .AddProjections()
                .AddFiltering()
                .AddSorting()
                .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = true)
                .AddInMemorySubscriptions();
                //.EnableRelaySupport();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            using (var scope = app.ApplicationServices.CreateScope())
            using (var context = scope.ServiceProvider.GetRequiredService<IDbContextFactory<BookContext>>().CreateDbContext())
            {
                context.Database.Migrate();
            }

                app.UseRouting();
            app.UseWebSockets();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
            });
        }
    }
}
