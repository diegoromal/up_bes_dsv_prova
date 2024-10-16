using backend.Models;
using Microsoft.EntityFrameworkCore;

public static class FuncionarioRotas
{
    public static void AddRotasFuncionarios(this WebApplication app)
    {
        var rotasFuncionario = app.MapGroup("api/funcionario");

        rotasFuncionario.MapPost("cadastrar", async (AddFuncionarioRequest request, AppDbContext db, CancellationToken ct) =>
        {
             var funcionarioExiste = await db.Funcionarios.AnyAsync(funcionario => funcionario.Cpf == request.Cpf, ct);

            if (funcionarioExiste)
                return Results.Conflict("Funcionário já cadastrado");

            var novoFuncionario = new Funcionario(request.Nome, request.Cpf);

            await db.Funcionarios.AddAsync(novoFuncionario, ct);

            await db.SaveChangesAsync(ct);

            var retornoFuncionario = new SaidaFuncionarioDTO(novoFuncionario.FuncionarioId, novoFuncionario.Nome, novoFuncionario.Cpf);

            return Results.Created("", retornoFuncionario);
        });

        rotasFuncionario.MapGet("listar", async(AppDbContext db, CancellationToken ct) =>
        {
            var funcionarios = await db.Funcionarios.Select(funcionario => new SaidaFuncionarioDTO(funcionario.FuncionarioId, funcionario.Nome, funcionario.Cpf)).ToListAsync(ct);

            return Results.Ok(funcionarios);
        });
    }
}