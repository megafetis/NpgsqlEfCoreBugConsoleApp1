using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NpgsqlEfCoreBugConsoleApp1;
using NpgsqlEfCoreBugConsoleApp1.CompiledModels;

var webAppPath = Path.Combine(AppContext.BaseDirectory);


var services = new ServiceCollection();


// Error in query, when i apply o.UseModel(MainDbContextModel.Instance);

// You can remove CompiledModels folder and generate it again:

// To install dotnet tools: dotnet tool install --global dotnet-ef (dotnet tool update --global dotnet-ef)
// To create optimized model: dotnet ef dbcontext optimize --context MainDbContext



services.AddDbContext<MainDbContext>(o =>
{
    o.LogTo(Console.WriteLine, LogLevel.Information);
    o.UseNpgsql(Constants.ConnectionString);
    o.ConfigureWarnings(x => x.Ignore(RelationalEventId.MultipleCollectionIncludeWarning));
    //o.UseModel(MainDbContextModel.Instance);   // Uncomment this to get error
});

var container = services.BuildServiceProvider();


var db = container.GetRequiredService<MainDbContext>();

db.Database.EnsureCreated();
//db.Database.Migrate();

#region Populate test data

if(!db.Articles.Any(p=>p.Id == "1"))
{
    db.Articles.Add(new SomeInterestingArticle()
    {
        Id = "1",
        Title = "Article 1",
        Tags = ["news","weather"]
    });
}
if (!db.Articles.Any(p => p.Id == "2"))
{
    db.Articles.Add(new SomeInterestingArticle()
    {
        Id = "2",
        Title = "Article 2",
        Tags = ["posts", "blog"]
    });
}
if (!db.Articles.Any(p => p.Id == "3"))
{
    db.Articles.Add(new SomeInterestingArticle()
    {
        Id = "3",
        Title = "Article 3",
        Tags = ["news", "tech"]
    });
}
db.SaveChanges();

#endregion


#region query

var q = db.Articles.Where(p => p.Tags.Any());

var results = q.ToList();

foreach(var r in results)
{
    Console.WriteLine(r.Title);
}

#endregion