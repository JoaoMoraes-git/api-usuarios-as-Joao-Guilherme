using api_usuarios_as_João_Guilherme.Application.DTOs;
using api_usuarios_as_João_Guilherme.Application.Interfaces;
using api_usuarios_as_João_Guilherme.Application.Services;
using api_usuarios_as_João_Guilherme.Infrastructure.Persistence;
using api_usuarios_as_João_Guilherme.Infrastructure.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using api_usuarios_as_Joao_Guilherme.Application.Validators;
using api_usuarios_as_João_Guilherme.Application.Validators;
using api_usuarios_as_João_Guilherme.domain;
using System.ComponentModel.DataAnnotations;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddOpenApi();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<UsuarioCreateValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UsuarioUpdateValidator>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


var app = builder.Build();

//Cria usuario.db automaticamente
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
} 

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
app.MapPost("/usuarios", async ([FromBody] UsuarioCreateDto usuarioDto, IValidator<UsuarioCreateDto> validator, IUsuarioService service, IUsuarioRepository repo, CancellationToken ct) =>
{
    if (usuarioDto?.Email != null)
        usuarioDto = usuarioDto with { Email = usuarioDto.Email.Trim().ToLower() };

    var validationResult = await validator.ValidateAsync(usuarioDto, ct);

    if (!validationResult.IsValid)
    {
        return Results.ValidationProblem(validationResult.ToDictionary());
    }

    if (await repo.EmailExistsAsync(usuarioDto.Email.Trim().ToLower(), null , ct))
        return Results.Conflict(new { mensagem = "Email já cadastrado"});

    var novoUsuario = await service.CriarAsync(usuarioDto, ct);
    return Results.Created($"/usuarios/{novoUsuario.Id}", novoUsuario);

    // var usuario = await service.CriarAsync(usuarioDto, ct);
    // return Results.Created($"/produtos/{usuario.Id}", usuario);
});

//Atualizar usuário completo
app.MapPut("/usuarios/{id}", async (int id, [FromBody] UsuarioUpdateDto usuarioDto, IValidator<UsuarioUpdateDto> validator, [FromServices] IUsuarioService service, IUsuarioRepository repo, CancellationToken ct) =>
{
    if (usuarioDto?.Email != null)
        usuarioDto = usuarioDto with { Email = usuarioDto.Email.Trim().ToLower() };

    var validationResult = await validator.ValidateAsync(usuarioDto, ct);

    if (!validationResult.IsValid)
    {
        return Results.ValidationProblem(validationResult.ToDictionary());
    }

    if (await repo.EmailExistsAsync(usuarioDto.Email, id, ct))
    {
        return Results.Conflict(new { mensagem = "O email já está sendo usado"});
    }

    var usuarioAtualizado = await service.AtualizarAsync(id, usuarioDto, ct);

    if (usuarioAtualizado == null) return Results.NotFound();

    return Results.Ok(usuarioAtualizado);
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

