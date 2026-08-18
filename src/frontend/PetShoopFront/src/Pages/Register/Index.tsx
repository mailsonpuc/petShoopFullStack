import { useState, type FormEvent } from "react";
import { Link, useNavigate } from "react-router-dom";

export function Register() {
  const [formData, setFormData] = useState({
    userName: "",
    email: "",
    password: "",
  });
  const [showPassword, setShowPassword] = useState(false);
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [passwordRequirements, setPasswordRequirements] = useState({
    length: false,
    lowercase: false,
    uppercase: false,
    digit: false,
    specialChar: false,
  });
  const navigate = useNavigate();

  const validatePassword = (password: string) => {
    setPasswordRequirements({
      length: password.length >= 6,
      lowercase: /[a-z]/.test(password),
      uppercase: /[A-Z]/.test(password),
      digit: /\d/.test(password),
      specialChar: /[^a-zA-Z0-9]/.test(password),
    });
  };

  const handlePasswordChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const newPassword = e.target.value;
    setFormData({ ...formData, password: newPassword });
    validatePassword(newPassword);
  };

  const handleSubmit = async (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    setIsLoading(true);
    setError(null);

    const allRequirementsMet = Object.values(passwordRequirements).every(Boolean);
    if (!allRequirementsMet) {
      setError("A senha não atende aos requisitos mínimos");
      setIsLoading(false);
      return;
    }

    try {
      const response = await fetch("http://localhost:5100/api/v1/Auth/register", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(formData),
      });

      if (!response.ok) {
        const errorData = await response.json();
        throw new Error(errorData.message || "Erro ao criar conta");
      }

      navigate("/login");
    } catch (err) {
      setError(err instanceof Error ? err.message : "Erro ao criar conta");
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <div className="flex min-h-screen w-full items-center justify-center bg-slate-950 px-4 py-12">
      <div className="w-full max-w-md rounded-2xl border border-slate-800 bg-slate-900/90 p-8 shadow-2xl shadow-blue-950/20 backdrop-blur-sm">
        <div className="mb-8 text-center">
          <div className="mx-auto mb-3 flex h-12 w-12 items-center justify-center rounded-xl bg-gradient-to-tr from-blue-600 to-cyan-400 font-bold text-white shadow-lg shadow-blue-500/30">
            <Link to="/" className="flex items-center gap-2 text-white no-underline hover:text-slate-100">
              <span>P</span>
            </Link>
          </div>
          <h1 className="text-2xl font-bold tracking-tight text-white">
            Criar conta
          </h1>
          <p className="mt-2 text-sm text-slate-400">
            Preencha os dados para se cadastrar
          </p>
        </div>

        {error && (
          <div className="mb-4 rounded-lg border border-red-800 bg-red-950/50 p-3 text-sm text-red-400">
            {error}
          </div>
        )}

        <form onSubmit={handleSubmit} className="space-y-5">
          <div>
            <label htmlFor="userName" className="mb-1.5 block text-sm font-medium text-slate-300">
              Usuário
            </label>
            <input
              id="userName"
              type="text"
              required
              value={formData.userName}
              onChange={(e) => setFormData({ ...formData, userName: e.target.value })}
              placeholder="Digite seu usuário"
              className="w-full rounded-lg border border-slate-800 bg-slate-950 px-4 py-2.5 text-sm text-white placeholder-slate-500 transition-colors focus:border-blue-500 focus:outline-none focus:ring-1 focus:ring-blue-500"
            />
          </div>

          <div>
            <label htmlFor="email" className="mb-1.5 block text-sm font-medium text-slate-300">
              Email
            </label>
            <input
              id="email"
              type="email"
              required
              value={formData.email}
              onChange={(e) => setFormData({ ...formData, email: e.target.value })}
              placeholder="seu@email.com"
              className="w-full rounded-lg border border-slate-800 bg-slate-950 px-4 py-2.5 text-sm text-white placeholder-slate-500 transition-colors focus:border-blue-500 focus:outline-none focus:ring-1 focus:ring-blue-500"
            />
          </div>

          <div>
            <div className="mb-1.5 flex items-center justify-between">
              <label htmlFor="password" className="text-sm font-medium text-slate-300">
                Senha
              </label>
            </div>
            <div className="relative">
              <input
                id="password"
                type={showPassword ? "text" : "password"}
                required
                value={formData.password}
                onChange={handlePasswordChange}
                placeholder="••••••••"
                className="w-full rounded-lg border border-slate-800 bg-slate-950 px-4 py-2.5 pr-10 text-sm text-white placeholder-slate-500 transition-colors focus:border-blue-500 focus:outline-none focus:ring-1 focus:ring-blue-500"
              />
              <button
                type="button"
                onClick={() => setShowPassword(!showPassword)}
                className="absolute right-3 top-1/2 -translate-y-1/2 text-slate-400 hover:text-slate-200"
                aria-label={showPassword ? "Ocultar senha" : "Mostrar senha"}
              >
                {showPassword ? "🙈" : "👁️"}
              </button>
            </div>
            <div className="mt-2 space-y-1">
              <p className="text-xs font-medium text-slate-400">A senha deve conter:</p>
              <ul className="space-y-0.5">
                <li className={`flex items-center gap-1.5 text-xs ${passwordRequirements.length ? "text-green-400" : "text-slate-500"}`}>
                  <span>{passwordRequirements.length ? "✓" : "•"}</span>
                  Mínimo de 6 caracteres
                </li>
                <li className={`flex items-center gap-1.5 text-xs ${passwordRequirements.lowercase ? "text-green-400" : "text-slate-500"}`}>
                  <span>{passwordRequirements.lowercase ? "✓" : "•"}</span>
                  Pelo menos 1 letra minúscula (a-z)
                </li>
                <li className={`flex items-center gap-1.5 text-xs ${passwordRequirements.uppercase ? "text-green-400" : "text-slate-500"}`}>
                  <span>{passwordRequirements.uppercase ? "✓" : "•"}</span>
                  Pelo menos 1 letra maiúscula (A-Z)
                </li>
                <li className={`flex items-center gap-1.5 text-xs ${passwordRequirements.digit ? "text-green-400" : "text-slate-500"}`}>
                  <span>{passwordRequirements.digit ? "✓" : "•"}</span>
                  Pelo menos 1 número (0-9)
                </li>
                <li className={`flex items-center gap-1.5 text-xs ${passwordRequirements.specialChar ? "text-green-400" : "text-slate-500"}`}>
                  <span>{passwordRequirements.specialChar ? "✓" : "•"}</span>
                  Pelo menos 1 caractere especial (!@#$% etc.)
                </li>
              </ul>
            </div>
          </div>

          <button
            type="submit"
            disabled={isLoading}
            className="w-full rounded-lg bg-blue-600 py-2.5 text-sm font-semibold text-white shadow-md shadow-blue-600/25 transition-all hover:bg-blue-500 hover:shadow-lg hover:shadow-blue-500/35 focus:outline-none focus:ring-2 focus:ring-blue-400 focus:ring-offset-2 focus:ring-offset-slate-900 disabled:opacity-50"
          >
            {isLoading ? "Criando conta..." : "Criar conta"}
          </button>
        </form>

        <p className="mt-8 text-center text-xs text-slate-400">
          Já tem uma conta?{" "}
          <Link to="/login" className="font-medium text-blue-400 no-underline hover:text-blue-300">
            Faça login
          </Link>
        </p>
      </div>
    </div>
  );
}
