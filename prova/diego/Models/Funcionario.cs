public class Funcionario
{
    public Guid FuncionarioId { get; init;}
    public string Nome { get; private set; }
    public string Cpf { get; private set;}
    public ICollection<Folha> Folhas { get; private set; } = new List<Folha>();

    public Funcionario(string nome, string cpf)
    {
        FuncionarioId = Guid.NewGuid();
        Nome = nome;
        Cpf = cpf;
    }
}