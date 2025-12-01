using api_usuarios_as_João_Guilherme.Application.DTOs;
using api_usuarios_as_João_Guilherme.Application.Interfaces;
using api_usuarios_as_João_Guilherme.Application.Services;
using api_usuarios_as_João_Guilherme.Infrastructure.Persistence;
using api_usuarios_as_João_Guilherme.Infrastructure.Repositories;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddOpenApi();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

app.UseHttpsRedirection();


//Listar todos os usuários
app.MapGet("/usuarios", async (IUsuarioService service, CancellationToken ct) =>
{
    return Results.Ok(await service.ListarAsync(ct));
});

//Buscar usuário por ID
app.MapGet("/usuarios/{id}", async (int id, IUsuarioService service, CancellationToken ct) =>
{
    var usuario = await service.ObterAsync(id, ct);
    return usuario != null ? Results.Ok(usuario) : Results.NotFound();
});

//Criar novo usuário
app.MapPost("/usuarios", async (UsuarioCreateDto usuarioDto, IUsuarioService service, CancellationToken ct) =>
{
    var usuario = await service.CriarAsync(usuarioDto, ct);
    return Results.Created($"/produtos/{usuario.Id}", usuario);
});

//Atualizar usuário completo
app.MapPut("/usuarios/{id}", async (int id, [FromBody] UsuarioUpdateDto usuarioDto, [FromServices] IUsuarioService service, CancellationToken ct) =>
{
    var usuario = await service.AtualizarAsync(id, usuarioDto, ct);
    if (usuario == null) return Results.NotFound();
    return Results.Ok(usuario);
});

//Remover usuário (soft delete)
app.MapDelete("/usuarios/{id}", async (int id, IUsuarioService service, CancellationToken ct) =>
{
    var usuario = await service.ObterAsync(id);
    if (usuario == null) return Results.NotFound();
    await service.RemoverAsync(id);
    return Results.NoContent();
});



app.Run();

