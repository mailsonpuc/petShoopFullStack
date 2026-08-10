import { useState } from "react";
import type { FormEvent } from "react";

export function Login() {
  const [formData, setFormData] = useState({
    userName: "",
    password: "",
  });
  const [showPassword, setShowPassword] = useState(false);
  const [isLoading, setIsLoading] = useState(false);

  const handleSubmit = async (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    setIsLoading(true);

    // O objeto enviado no payload possui exatamente os campos userName e password
    console.log("Payload enviado:", formData);

    // Simulação de requisição
    setTimeout(() => {
      setIsLoading(false);
    }, 1000);
  };

  return (
    <div className="flex min-h-screen w-full items-center justify-center bg-slate-950 px-4 py-12">
      <div className="w-full max-w-md rounded-2xl border border-slate-800 bg-slate-900/90 p-8 shadow-2xl shadow-blue-950/20 backdrop-blur-sm">
        
        {/* Cabeçalho */}
        <div className="mb-8 text-center">
          <div className="mx-auto mb-3 flex h-12 w-12 items-center justify-center rounded-xl bg-gradient-to-tr from-blue-600 to-cyan-400 font-bold text-white shadow-lg shadow-blue-500/30">
            H
          </div>
          <h1 className="text-2xl font-bold tracking-tight text-white">
            Acesse sua conta
          </h1>
          <p className="mt-2 text-sm text-slate-400">
            Insira suas credenciais para continuar
          </p>
        </div>

        {/* Formulário */}
        <form onSubmit={handleSubmit} className="space-y-5">
          {/* Campo Username */}
          <div>
            <label
              htmlFor="userName"
              className="mb-1.5 block text-sm font-medium text-slate-300"
            >
              Usuário
            </label>
            <input
              id="userName"
              type="text"
              required
              value={formData.userName}
              onChange={(e) =>
                setFormData({ ...formData, userName: e.target.value })
              }
              placeholder="Digite seu usuário"
              className="w-full rounded-lg border border-slate-800 bg-slate-950 px-4 py-2.5 text-sm text-white placeholder-slate-500 transition-colors focus:border-blue-500 focus:outline-none focus:ring-1 focus:ring-blue-500"
            />
          </div>

          {/* Campo Senha */}
          <div>
            <div className="mb-1.5 flex items-center justify-between">
              <label
                htmlFor="password"
                className="text-sm font-medium text-slate-300"
              >
                Senha
              </label>
              <a
                href="#esqueci-senha"
                className="text-xs text-blue-400 hover:text-blue-300"
              >
                Esqueceu a senha?
              </a>
            </div>
            <div className="relative">
              <input
                id="password"
                type={showPassword ? "text" : "password"}
                required
                value={formData.password}
                onChange={(e) =>
                  setFormData({ ...formData, password: e.target.value })
                }
                placeholder="••••••••"
                className="w-full rounded-lg border border-slate-800 bg-slate-950 px-4 py-2.5 pr-10 text-sm text-white placeholder-slate-500 transition-colors focus:border-blue-500 focus:outline-none focus:ring-1 focus:ring-blue-500"
              />
              <button
                type="button"
                onClick={() => setShowPassword(!showPassword)}
                className="absolute right-3 top-1/2 -translate-y-1/2 text-slate-400 hover:text-slate-200"
                aria-label={showPassword ? "Ocultar senha" : "Mostrar senha"}
              >
                <svg
                  className="h-5 w-5"
                  fill="none"
                  stroke="currentColor"
                  viewBox="0 0 24 24"
                >
                  {showPassword ? (
                    <path
                      strokeLinecap="round"
                      strokeLinejoin="round"
                      strokeWidth={2}
                      d="M13.875 18.825A10.05 10.05 0 0112 19c-4.478 0-8.268-2.943-9.543-7a9.97 9.97 0 011.563-3.029m5.858-5.908a8.97 8.97 0 013.682-.763c4.478 0 8.268 2.943 9.543 7a10.025 10.025 0 01-4.132 5.411m-2.115-2.115a3.5 3.5 0 01-4.95-4.95"
                    />
                  ) : (
                    <path
                      strokeLinecap="round"
                      strokeLinejoin="round"
                      strokeWidth={2}
                      d="M15 12a3 3 0 11-6 0 3 3 0 016 0z"
                    />
                  )}
                  {!showPassword && (
                    <path
                      strokeLinecap="round"
                      strokeLinejoin="round"
                      strokeWidth={2}
                      d="M2.458 12C3.732 7.943 7.523 5 12 5c4.478 0 8.268 2.943 9.542 7-1.274 4.057-5.064 7-9.542 7-4.477 0-8.268-2.943-9.542-7z"
                    />
                  )}
                </svg>
              </button>
            </div>
          </div>

          {/* Botão de Envio */}
          <button
            type="submit"
            disabled={isLoading}
            className="w-full rounded-lg bg-blue-600 py-2.5 text-sm font-semibold text-white shadow-md shadow-blue-600/25 transition-all hover:bg-blue-500 hover:shadow-lg hover:shadow-blue-500/35 focus:outline-none focus:ring-2 focus:ring-blue-400 focus:ring-offset-2 focus:ring-offset-slate-900 disabled:opacity-50"
          >
            {isLoading ? "Entrando..." : "Entrar"}
          </button>
        </form>

        {/* Rodapé */}
        <p className="mt-8 text-center text-xs text-slate-400">
          Não tem uma conta?{" "}
          <a href="#cadastro" className="font-medium text-blue-400 hover:text-blue-300">
            Cadastre-se
          </a>
        </p>
      </div>
    </div>
  );
}