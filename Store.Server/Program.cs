//Minimal API
using Store.Server.Models;
using Store.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);

// CORS configuration for this API
builder.Services.AddCors(options => options.AddDefaultPolicy(builder =>
{
    builder.WithOrigins("https://localhost:7043")
           .AllowAnyMethod()
           .AllowAnyHeader();
}));

// Setting the MySql Version im using
var serverVersion = new MySqlServerVersion(new Version(10, 4, 11));

builder.Services.AddDbContext<StoreDbContext>(options =>
        options.UseMySql(builder.Configuration.GetConnectionString("MySqlConnection"), 
        serverVersion
));

var app = builder.Build();

// Using the CORS configuration
app.UseCors();


// GET All
app.MapGet("/api/things", async (StoreDbContext context) => 
    await context.Things.AsNoTracking().ToListAsync()
);

// GET one
app.MapGet("/api/things/{Id}",async (int Id, StoreDbContext context) => 
{
    //read
    Thing? Thing = await context.Things.FindAsync(Id);
    if(Thing is null)
    {
        return Results.NotFound();
    }
    return Results.Ok(Thing);
})
.WithName("GetThing");

//POST 
app.MapPost("/api/thing", async (Thing thing, StoreDbContext context)=>
{
    //create
    context.Things.Add(thing);
    await context.SaveChangesAsync();
    return Results.CreatedAtRoute("GetThing", new {id = thing.Id}, thing);
});
// .WithParameterValidation();
// for some reason MinimalApis.Extensions says it is incompatible with all my frameworks...

// PUT
app.MapPut("/api/thing", async (Thing UpdatedThing, StoreDbContext context) => 
{
    //update
    var RowsAffected = await context.Things.Where(th => th.Id == UpdatedThing.Id)
        .ExecuteUpdateAsync(updates  =>  updates.SetProperty(th => th.Name, UpdatedThing.Name)
                                                .SetProperty(th => th.Description, UpdatedThing.Description)
        );
    return RowsAffected == 0?Results.NotFound(): Results.NoContent();
});

// DELETE
app.MapDelete("/api/thing/{Id}", async (int Id, StoreDbContext context) =>
{
    //delete
    var RowsAffected = await context.Things.Where(th => th.Id == Id)
        .ExecuteDeleteAsync();
    return RowsAffected == 0?Results.NotFound(): Results.NoContent();
});

app.Run();
