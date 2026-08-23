import { Navigate } from "react-router-dom";
import { useAdminRole } from "../../Hooks/useAdminRole";
import { Link } from "react-router-dom";

export function SemPermissao() {
  const { isAdmin, loading } = useAdminRole();

  if (loading) {
    return (
      <div className="flex h-screen items-center justify-center bg-slate-950">
        <div className="h-8 w-8 animate-spin rounded-full border-4 border-blue-600 border-t-transparent"></div>
      </div>
    );
  }

  if (isAdmin) {
    return <Navigate to="/dashboard" replace />;
  }

  return (
    <div className="flex h-screen items-center justify-center bg-slate-950">
      <div className="max-w-md rounded-xl border border-yellow-800 bg-yellow-950/50 p-6 text-center">
        <h2 className="text-xl font-bold text-yellow-400">Acesso restrito</h2>
        <p className="mt-2 text-sm text-yellow-300">
          Você precisa ser admin para acessar este conteúdo. <b className="text-zinc-300 text-2xl">Contate o administrador do sistema para obter permissão.</b>
        </p>

        <Link to="/" className="mt-4 inline-block rounded-md bg-blue-600 px-4 py-2 text-white hover:bg-blue-700">
          Voltar para a página inicial
        </Link>
      </div>
    </div>
  );
}
