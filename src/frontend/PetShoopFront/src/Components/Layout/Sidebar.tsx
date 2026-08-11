import { NavLink } from "react-router-dom";
import {
  HiHome,
  HiUserGroup,
  HiOutlineUser,
  HiCube,
  HiCog,
  HiCalendar,
  HiClipboardList,
  HiShieldCheck,
  HiDocumentText,
  HiCurrencyDollar,
  HiShoppingCart,
} from "react-icons/hi";

const menuItems = [
  { path: "/dashboard", label: "Dashboard", icon: HiHome },
  { path: "/clientes", label: "Clientes", icon: HiUserGroup },
  { path: "/pets", label: "Pets", icon: HiOutlineUser },
  { path: "/funcionarios", label: "Funcionários", icon: HiOutlineUser },
  { path: "/produtos", label: "Produtos", icon: HiCube },
  { path: "/servicos", label: "Serviços", icon: HiCog },
  { path: "/agendamentos", label: "Agendamentos", icon: HiCalendar },
  { path: "/consultas", label: "Consultas", icon: HiClipboardList },
  { path: "/vacinas", label: "Vacinas", icon: HiShieldCheck },
  { path: "/prontuarios", label: "Prontuários", icon: HiDocumentText },
  { path: "/vendas", label: "Vendas", icon: HiCurrencyDollar },
  { path: "/itens-venda", label: "Itens de Venda", icon: HiShoppingCart },
];

export function Sidebar() {
  return (
    <aside className="flex h-full w-64 flex-col border-r border-slate-800 bg-slate-900">
      <div className="flex items-center gap-3 px-6 py-5">
        <div className="flex h-10 w-10 items-center justify-center rounded-xl bg-gradient-to-tr from-blue-600 to-cyan-400 font-bold text-white shadow-lg shadow-blue-500/30">P</div>
        <span className="text-xl font-bold text-white">PetShoop</span>
      </div>
      <nav className="flex-1 space-y-1 px-3 py-4">
        {menuItems.map((item) => (
          <NavLink key={item.path} to={item.path} className={({ isActive }) => `flex items-center gap-3 rounded-lg px-3 py-2.5 text-sm font-medium transition-colors ${isActive ? "bg-blue-600/10 text-blue-400" : "text-slate-400 hover:bg-slate-800 hover:text-white"}`}>
            <item.icon className="h-5 w-5" />
            {item.label}
          </NavLink>
        ))}
      </nav>
      <div className="border-t border-slate-800 px-6 py-4">
        <p className="text-xs text-slate-500">PetShoop FullStack v1.0</p>
      </div>
    </aside>
  );
}
