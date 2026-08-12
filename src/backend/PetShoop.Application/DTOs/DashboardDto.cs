namespace PetShoop.Application.DTOs;

public class DashboardDto
{
    public int TotalClientes { get; set; }
    public int TotalPets { get; set; }
    public int TotalFuncionarios { get; set; }
    public int TotalProdutos { get; set; }
    public int TotalAgendamentos { get; set; }
    public int TotalVendas { get; set; }
    public decimal ReceitaTotal { get; set; }
    public int AgendamentosHoje { get; set; }
    public int AgendamentosPendentes { get; set; }
}
