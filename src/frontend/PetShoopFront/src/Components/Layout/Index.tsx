import { Outlet } from "react-router-dom";
import { Sidebar } from "./Sidebar";
import { useAuth } from "../../Contexts/AuthContext";

export function Layout() {
  const { logout, user } = useAuth();

  return (
    <div className="flex h-screen w-full bg-slate-950 text-white">
      <Sidebar />
      <div className="flex flex-1 flex-col overflow-hidden">
        <header className="flex h-16 items-center justify-between border-b border-slate-800 bg-slate-900/80 px-6 backdrop-blur-md">
          <h1 className="text-lg font-semibold text-white">Painel Administrativo</h1>
          <div className="flex items-center gap-4">
            <span className="text-sm text-slate-400">{user?.userName || user?.email}</span>
            <button
              onClick={logout}
              className="rounded-lg bg-slate-800 px-4 py-2 text-sm font-medium text-slate-300 transition-colors hover:bg-slate-700 hover:text-white"
            >
              Sair
            </button>
          </div>
        </header>
        <main className="flex-1 overflow-y-auto p-6">
          <Outlet />
        </main>
      </div>
    </div>
  );
}
