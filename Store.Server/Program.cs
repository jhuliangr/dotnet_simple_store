using Store.Server.Models;


List<Thing> Things = new () 
{   
    new Thing()
    {
        Id = 1,
        Name = "Object 1",
        Description = "Test Object number one"
    },
    new Thing()
    {
        Id = 2,
        Name = "Object 2",
        Description = "This might by the test object number two"
    }

}; 

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => options.AddDefaultPolicy(builder =>
{
    builder.WithOrigins("https://localhost:7043")
           .AllowAnyMethod()
           .AllowAnyHeader();
}));

var app = builder.Build();

app.UseCors();

app.MapGet("/api/things", () => Things);

app.MapGet("/api/things/{Id}", (int Id) => 
{
    //read
    Thing? Thing = Things.Find(th => th.Id == Id);
    if(Thing is null)
    {
        return Results.NotFound();
    }
    return Results.Ok(Thing);
})
.WithName("GetThing");

app.MapPost("/api/thing", (Thing thing)=>
{
    
    //create
    if(Things.Count() == 0)
    {
        thing.Id = 1;
    }
    else
    {
        thing.Id = Things.Max(Th => Th.Id) + 1;
    }
    Things.Add(thing);
    return Results.CreatedAtRoute("GetThing", new {id = thing.Id}, thing);
});
// .WithParameterValidation();

app.MapPut("/api/thing", (Thing UpdatedThing) => 
{
    //update
    Thing? prevThing = Things.Find(Th => Th.Id == UpdatedThing.Id );
    if (prevThing is null)
    {
        Things.Add(UpdatedThing);
        return Results.CreatedAtRoute("GetThing", new {id = UpdatedThing.Id}, UpdatedThing);
    }
    prevThing.Name = UpdatedThing.Name;
    prevThing.Description = UpdatedThing.Description;
    return Results.NoContent();
});

app.MapDelete("/api/thing/{Id}", (int Id) =>
{
    //delete
    Thing? ToDelete = Things.Find(Th => Th.Id == Id);
    if( ToDelete is null)
    {
        return Results.NotFound();
    }
    Things.Remove(ToDelete);
    return Results.NoContent();
});

app.Run();
