using backend.Models;
using Microsoft.EntityFrameworkCore;

public static class FolhaRotas
{
    public static void AddRotasFolhas(this WebApplication app)
    {
        var rotasFolha = app.MapGroup("api/folha");

        rotasFolha.MapPost("cadastrar", async (AddFolhaRequest request, AppDbContext db, CancellationToken ct) =>
        {
             var funcionarioExiste = await db.Funcionarios.AnyAsync(funcionario => funcionario.FuncionarioId == request.FuncionarioId, ct);

            if (!funcionarioExiste)
                return Results.NotFound();

            var folhaExiste = await db.Folhas
                    .Where(folha => folha.Mes == request.Mes && folha.Ano == request.Ano && folha.FuncionarioId == request.FuncionarioId)
                    .Select(folha => new {folha.FolhaId, folha.Valor, folha.Quantidade, folha.Mes, folha.Ano, folha.SalarioBruto, folha.ImpostoIrrf, folha.ImpostoInss, folha.ImpostoFgts, folha.SalarioLiquido, folha.FuncionarioId, folha.Funcionario})
                    .ToListAsync(ct);

            if (folhaExiste.Any())
                return Results.Conflict("Folha já cadastrada");

            var novaFolha = new Folha(request.Valor, request.Quantidade, request.Mes, request.Ano, request.FuncionarioId);

            await db.Folhas.AddAsync(novaFolha, ct);

            await db.SaveChangesAsync(ct);

            return Results.Created("", novaFolha);
        });

        rotasFolha.MapGet("listar", async(AppDbContext db, CancellationToken ct) =>
        {
            var folhas = await db.Folhas
                    .Select(folha => new {folha.FolhaId, folha.Valor, folha.Quantidade, folha.Mes, folha.Ano, folha.SalarioBruto, folha.ImpostoIrrf, folha.ImpostoInss, folha.ImpostoFgts, folha.SalarioLiquido, folha.FuncionarioId, folha.Funcionario})
                    .ToListAsync(ct);

            return Results.Ok(folhas);
        });

        rotasFolha.MapGet("buscar/{cpf}/{mes}/{ano}", async (string cpf, int mes, int ano, AppDbContext db, CancellationToken ct) =>
        {
            var funcionario = await db.Funcionarios.SingleOrDefaultAsync(funcionario => funcionario.Cpf == cpf, ct);

            if (funcionario == null)
                return Results.NotFound();
            
            var folha = await db.Folhas
                    .Where(folha => folha.Mes == mes && folha.Ano == ano && folha.FuncionarioId == funcionario.FuncionarioId)
                    .Select(folha => new {folha.FolhaId, folha.Valor, folha.Quantidade, folha.Mes, folha.Ano, folha.SalarioBruto, folha.ImpostoIrrf, folha.ImpostoInss, folha.ImpostoFgts, folha.SalarioLiquido, folha.FuncionarioId, folha.Funcionario})
                    .ToListAsync(ct);

            if (folha == null)
                return Results.NotFound();

            return Results.Ok(folha);


        });
    }
}