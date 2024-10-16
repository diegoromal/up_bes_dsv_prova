namespace backend.Models;
public record AddFolhaRequest(double Valor, double Quantidade, int Mes, int Ano, Guid FuncionarioId);