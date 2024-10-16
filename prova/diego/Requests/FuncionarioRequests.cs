namespace backend.Models;
public record AddFuncionarioRequest(string Nome, string Cpf);

public record SaidaFuncionarioDTO(Guid FuncionarioId, string Nome, string Cpf);