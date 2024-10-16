public class Folha
{
    public Guid FolhaId { get; init;}
    public double Valor { get; private set; }
    public double Quantidade { get; private set;}
    public int Mes { get; private set; }
    public int Ano { get; private set; }
    public double SalarioBruto { get; private set; }
    public double ImpostoIrrf { get; private set; }
    public double ImpostoInss { get; private set; }
    public double ImpostoFgts { get; private set; }
    public double SalarioLiquido { get; private set; }
    public Guid FuncionarioId {get; init;}
    public Funcionario? Funcionario { get; private set; }

    public Folha(double valor, double quantidade, int mes, int ano, Guid funcionarioId)
    {
        FolhaId = Guid.NewGuid();
        Valor = valor;  
        Quantidade = quantidade;
        Mes = mes;
        Ano = ano;
        FuncionarioId = funcionarioId;
        SalarioBruto = valor * quantidade;
        ImpostoIrrf = CalculaIrrf(SalarioBruto);
        ImpostoInss = CalculaInss(SalarioBruto);
        ImpostoFgts = SalarioBruto * (8 / 100);
        SalarioLiquido = SalarioBruto - ImpostoIrrf - ImpostoInss;
    }

    private double CalculaIrrf(double salarioBruto)
    {
        if (salarioBruto <= 1903.98)
            return 0;
        if (salarioBruto <= 2826.65)
            return salarioBruto * (7.5 / 100);
        if (salarioBruto <= 3751.05)
            return salarioBruto * (15 / 100);
        if (salarioBruto <= 4664.68)
           return salarioBruto * (22.5 / 100);
        return salarioBruto * (27.5 / 100);
    }

    private double CalculaInss(double salarioBruto)
    {
        if (salarioBruto <= 1693.72)
            return salarioBruto * (8 / 100);
        if (salarioBruto <= 2822.90)
            return salarioBruto * (9 / 100);
        if (salarioBruto <= 5645.80)
            return salarioBruto * (11 / 100);
        return 621.03;
    }
}