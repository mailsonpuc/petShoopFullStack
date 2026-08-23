import { useEffect, useState } from "react";
import { dashboardApi } from "../../Services/api";

export function Dashboard() {
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [data, setData] = useState<{
    totalClientes: number;
    totalPets: number;
    totalFuncionarios: number;
    totalProdutos: number;
    totalAgendamentos: number;
    totalVendas: number;
    receitaTotal: number;
    agendamentosHoje: number;
    agendamentosPendentes: number;
  } | null>(null);

  useEffect(() => {
    async function load() {
      try {
        setLoading(true);
        setError(null);
        const response = await dashboardApi.get();
        setData(response);
      } catch (err) {
        setError(err instanceof Error ? err.message : "Erro ao carregar dashboard");
      } finally {
        setLoading(false);
      }
    }
    load();
  }, []);

  const formatCurrency = (value: number) =>
    new Intl.NumberFormat("pt-BR", {
      style: "currency",
      currency: "BRL",
    }).format(value);

  if (loading) {
    return (
      <div className="space-y-6">
        <div>
          <h2 className="text-2xl font-bold text-white">Dashboard</h2>
          <p className="mt-1 text-sm text-slate-400">Visão geral do sistema PetShoop</p>
        </div>
        <p className="text-sm text-slate-400">Carregando...</p>
      </div>
    );
  }

  if (error) {
    return (
      <div className="space-y-6">
        <div>
          <h2 className="text-2xl font-bold text-white">Dashboard</h2>
          <p className="mt-1 text-sm text-slate-400">Visão geral do sistema PetShoop</p>
        </div>
        <p className="text-sm text-red-400">{error}</p>
      </div>
    );
  }

  return (
    <div className="space-y-6">
      <div>
        <h2 className="text-2xl font-bold text-white">Dashboard</h2>
        <p className="mt-1 text-sm text-slate-400">Visão geral do sistema PetShoop</p>
      </div>

      <div className="grid grid-cols-1 gap-4 sm:grid-cols-2 lg:grid-cols-4">
        {[
          { title: "Clientes", value: data?.totalClientes ?? 0, color: "blue" },
          { title: "Pets", value: data?.totalPets ?? 0, color: "cyan" },
          { title: "Agendamentos", value: data?.totalAgendamentos ?? 0, color: "green" },
          { title: "Receita Total", value: data ? formatCurrency(data.receitaTotal) : "R$ 0,00", color: "purple" },
        ].map((stat) => (
          <div
            key={stat.title}
            className="rounded-xl border border-slate-800 bg-slate-900 p-6"
          >
            <p className="text-sm font-medium text-slate-400">{stat.title}</p>
            <p className="mt-2 text-3xl font-bold text-white">{stat.value}</p>
          </div>
        ))}
      </div>

      <div className="grid grid-cols-1 gap-4 sm:grid-cols-2 lg:grid-cols-4">
        {[
          { title: "Funcionários", value: data?.totalFuncionarios ?? 0, color: "yellow" },
          { title: "Produtos", value: data?.totalProdutos ?? 0, color: "orange" },
          { title: "Vendas", value: data?.totalVendas ?? 0, color: "pink" },
          { title: "Agendamentos Hoje", value: data?.agendamentosHoje ?? 0, color: "emerald" },
        ].map((stat) => (
          <div
            key={stat.title}
            className="rounded-xl border border-slate-800 bg-slate-900 p-6"
          >
            <p className="text-sm font-medium text-slate-400">{stat.title}</p>
            <p className="mt-2 text-3xl font-bold text-white">{stat.value}</p>
          </div>
        ))}
      </div>

      {data && data.agendamentosPendentes > 0 && (
        <div className="rounded-xl border border-yellow-800 bg-yellow-950/50 p-4">
          <p className="text-sm font-medium text-yellow-400">
            {data.agendamentosPendentes} agendamento(s) pendente(s)
          </p>
        </div>
      )}
    </div>
  );
}
