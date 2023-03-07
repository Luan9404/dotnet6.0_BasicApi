using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSqlServer<ApplicationDbContext>(builder.Configuration["Database:SqlServer"]);

var app = builder.Build();
var configuration = app.Configuration;

ProductRepository.Init(configuration);

app.MapPost("/product", ( Product product) => {
  ProductRepository.Add(product);

  return Results.Created($"/product/{product.Code}", product.Code);
});

app.MapGet("/product/{code}", ([FromRoute] string code) => {
  var product = ProductRepository.Find(code);

  if(product != null )
    return Results.Ok(product);

  return Results.NotFound();
});

app.MapPut("/product", (Product product) => {
  Product savedProduct = ProductRepository.Find(product.Code);
  savedProduct.Name = product.Name;

  return Results.Ok();
});

app.MapDelete("/product/{code}", ([FromRoute] string code) => {
  Product savedProduct = ProductRepository.Find(code);
  ProductRepository.Delete(savedProduct); 

  return Results.Ok();
});

app.Run();
