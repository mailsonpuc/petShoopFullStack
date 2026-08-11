export function Dashboard() {
  return (
    <div className="space-y-6">
      <div>
        <h2 className="text-2xl font-bold text-white">Dashboard</h2>
        <p className="mt-1 text-sm text-slate-400">Visão geral do sistema PetShoop</p>
      </div>

      <div className="grid grid-cols-1 gap-4 sm:grid-cols-2 lg:grid-cols-4">
        {[
          { title: "Clientes", value: "12", color: "blue" },
          { title: "Pets", value: "48", color: "cyan" },
          { title: "Agendamentos", value: "7", color: "green" },
          { title: "Vendas Hoje", value: "R$ 1.250", color: "purple" },
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
    </div>
  );
}
