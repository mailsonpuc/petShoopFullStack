import { useState } from "react";

export function Header() {
  const [isMenuOpen, setIsMenuOpen] = useState(false);

  return (
    <header className="sticky top-0 z-50 w-full border-b border-slate-800 bg-slate-900/80 backdrop-blur-md">
      <div className="mx-auto flex max-w-7xl items-center justify-between px-6 py-4">
        
        {/* Logo */}
        <div className="flex items-center gap-3">
          <div className="flex h-10 w-10 items-center justify-center rounded-xl bg-gradient-to-tr from-blue-600 to-cyan-400 font-bold text-white shadow-lg shadow-blue-500/30">
            H
          </div>
          <span className="text-xl font-bold bg-gradient-to-r from-white via-slate-200 to-blue-400 bg-clip-text text-transparent">
            MinhaMarca
          </span>
        </div>

        {/* Links de Navegação (Desktop) */}
        <nav className="hidden items-center gap-8 md:flex">
          <a
            href="#home"
            className="text-sm font-medium text-slate-300 transition-colors hover:text-blue-400"
          >
            Início
          </a>
          <a
            href="#recursos"
            className="text-sm font-medium text-slate-300 transition-colors hover:text-blue-400"
          >
            Recursos
          </a>
          <a
            href="#precos"
            className="text-sm font-medium text-slate-300 transition-colors hover:text-blue-400"
          >
            Preços
          </a>
          <a
            href="#sobre"
            className="text-sm font-medium text-slate-300 transition-colors hover:text-blue-400"
          >
            Sobre
          </a>
        </nav>

        {/* Botão de Ação (CTA) */}
        <div className="hidden items-center gap-4 md:flex">
          <a
            href="#login"
            className="text-sm font-medium text-slate-300 transition-colors hover:text-white"
          >
            Entrar
          </a>
          <a
            href="#comecar"
            className="rounded-lg bg-blue-600 px-4 py-2.5 text-sm font-semibold text-white shadow-md shadow-blue-600/25 transition-all hover:bg-blue-500 hover:shadow-lg hover:shadow-blue-500/35 focus:outline-none focus:ring-2 focus:ring-blue-400 focus:ring-offset-2 focus:ring-offset-slate-900"
          >
            Começar Grátis
          </a>
        </div>

        {/* Botão do Menu Mobile */}
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

      {/* Dropdown do Menu Mobile */}
      {isMenuOpen && (
        <div className="border-b border-slate-800 bg-slate-900 px-6 py-4 md:hidden">
          <nav className="flex flex-col gap-4">
            <a
              href="#home"
              className="text-sm font-medium text-slate-300 hover:text-blue-400"
            >
              Início
            </a>
            <a
              href="#recursos"
              className="text-sm font-medium text-slate-300 hover:text-blue-400"
            >
              Recursos
            </a>
            <a
              href="#precos"
              className="text-sm font-medium text-slate-300 hover:text-blue-400"
            >
              Preços
            </a>
            <a
              href="#sobre"
              className="text-sm font-medium text-slate-300 hover:text-blue-400"
            >
              Sobre
            </a>
            <hr className="border-slate-800" />
            <a
              href="#login"
              className="text-sm font-medium text-slate-300 hover:text-white"
            >
              Entrar
            </a>
            <a
              href="#comecar"
              className="rounded-lg bg-blue-600 py-2.5 text-center text-sm font-semibold text-white shadow-md shadow-blue-600/25 hover:bg-blue-500"
            >
              Começar Grátis
            </a>
          </nav>
        </div>
      )}
    </header>
  );
}

export function Home() {
  return (
    <div className="min-h-screen w-full bg-slate-950 text-white">
      <Header />
      <main className="mx-auto max-w-7xl px-6 py-12">
        <h1 className="text-3xl font-bold">Página Home</h1>
      </main>
    </div>
  );
}