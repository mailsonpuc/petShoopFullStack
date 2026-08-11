import { useState } from "react";
import { Link, useLocation } from "react-router-dom";

const menuItems = [
  { path: "/dashboard", label: "Dashboard" },
  { path: "/clientes", label: "Clientes" },
  { path: "/pets", label: "Pets" },
  { path: "/agendamentos", label: "Agendamentos" },
  { path: "/servicos", label: "Serviços" },
  { path: "/vendas", label: "Vendas" },
];

export function Header() {
  const [isMenuOpen, setIsMenuOpen] = useState(false);
  const location = useLocation();

  const isActive = (path: string) => location.pathname === path;

  return (
    <header className="sticky top-0 z-50 w-full border-b border-slate-800 bg-slate-900/80 backdrop-blur-md">
      <div className="mx-auto flex max-w-7xl items-center justify-between px-6 py-4">
        <Link to="/" className="flex items-center gap-3">
          <div className="flex h-10 w-10 items-center justify-center rounded-xl bg-gradient-to-tr from-blue-600 to-cyan-400 font-bold text-white shadow-lg shadow-blue-500/30">
            P
          </div>
          <span className="text-xl font-bold bg-gradient-to-r from-white via-slate-200 to-blue-400 bg-clip-text text-transparent">
            PetShoop
          </span>
        </Link>

        <nav className="hidden items-center gap-6 md:flex">
          {menuItems.map((item) => (
            <Link
              key={item.path}
              to={item.path}
              className={`text-sm font-medium transition-colors ${
                isActive(item.path)
                  ? "text-blue-400"
                  : "text-slate-300 hover:text-white"
              }`}
            >
              {item.label}
            </Link>
          ))}
        </nav>

        <div className="hidden items-center gap-4 md:flex">
          <Link
            to="/login"
            className="rounded-lg bg-blue-600 px-4 py-2 text-sm font-semibold text-white shadow-md shadow-blue-600/25 transition-all hover:bg-blue-500 hover:shadow-lg hover:shadow-blue-500/35 focus:outline-none focus:ring-2 focus:ring-blue-400 focus:ring-offset-2 focus:ring-offset-slate-900"
          >
            Entrar
          </Link>
        </div>

        <button
          onClick={() => setIsMenuOpen(!isMenuOpen)}
          className="rounded-lg p-2 text-slate-400 hover:bg-slate-800 hover:text-white md:hidden"
          aria-label="Abrir Menu"
        >
          <svg
            className="h-6 w-6"
            fill="none"
            stroke="currentColor"
            viewBox="0 0 24 24"
          >
            {isMenuOpen ? (
              <path
                strokeLinecap="round"
                strokeLinejoin="round"
                strokeWidth={2}
                d="M6 18L18 6M6 6l12 12"
              />
            ) : (
              <path
                strokeLinecap="round"
                strokeLinejoin="round"
                strokeWidth={2}
                d="M4 6h16M4 12h16M4 18h16"
              />
            )}
          </svg>
        </button>
      </div>

      {isMenuOpen && (
        <div className="border-b border-slate-800 bg-slate-900 px-6 py-4 md:hidden">
          <nav className="flex flex-col gap-4">
            {menuItems.map((item) => (
              <Link
                key={item.path}
                to={item.path}
                onClick={() => setIsMenuOpen(false)}
                className={`text-sm font-medium transition-colors ${
                  isActive(item.path)
                    ? "text-blue-400"
                    : "text-slate-300 hover:text-white"
                }`}
              >
                {item.label}
              </Link>
            ))}
            <hr className="border-slate-800" />
            <Link
              to="/login"
              onClick={() => setIsMenuOpen(false)}
              className="rounded-lg bg-blue-600 py-2.5 text-center text-sm font-semibold text-white shadow-md shadow-blue-600/25 hover:bg-blue-500"
            >
              Entrar
            </Link>
          </nav>
        </div>
      )}
    </header>
  );
}
