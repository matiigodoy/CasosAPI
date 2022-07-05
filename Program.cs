using Microsoft.EntityFrameworkCore;
using MinimalAPICasosIVR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<CasoDB>(opt => opt.UseInMemoryDatabase("casos"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/crear", async (Caso caso, CasoDB db) =>
{
    db.Casos.Add(caso);
    await db.SaveChangesAsync();

    return Results.Created($"/crear/{caso.Id}", caso);
});

app.MapPut("/actualizar/{Id}", async (int Id, Caso inputCaso, CasoDB db) =>
{
    var caso = await db.Casos.FindAsync(Id);
    if (caso is null) return Results.NotFound();
    caso.Titulo = inputCaso.Titulo;
    caso.Descripcion = inputCaso.Descripcion;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/eliminar/{Id}", async (int Id, CasoDB db) =>
{
    if (await db.Casos.FindAsync(Id) is Caso caso)
    {
        db.Casos.Remove(caso);
        await db.SaveChangesAsync();
        return Results.Ok(caso);
    }

    return Results.NotFound();
});

app.MapGet("/consultar", async (CasoDB db) =>
    await db.Casos.ToListAsync());

app.MapGet("/consultar/{Id}", async (int Id, CasoDB db) =>
    await db.Casos.FindAsync(Id)
        is Caso caso
            ? Results.Ok(caso)
            : Results.NotFound());


app.Run();