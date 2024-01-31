using DocumentStorageEasyAppDemo.Routes;

var builder = WebApplication.CreateBuilder(args);

SetupHelpers.RegisterServices(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.MapGroup("/documents")
    .DocumentStorageApi()
    //.WithTags("Public")
    .WithOpenApi();

app.Run();

public partial class Program { }