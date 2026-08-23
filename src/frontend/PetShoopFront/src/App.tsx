import { createBrowserRouter } from "react-router-dom";
import { Home } from "./Pages/Home/Index";
import { Login } from "./Pages/Login/Index";
import { Register } from "./Pages/Register/Index";
import { Sobre } from "./Pages/Sobre/Index";
import { Layout } from "./Components/Layout/Index";
import { ProtectedRoute, AdminProtectedRoute } from "./Components/ProtectedRoute";
import { Dashboard } from "./Pages/Dashboard/Index";
import { ClientesPage } from "./Pages/Clientes/Index";
import { PetsPage } from "./Pages/Pets/Index";
import { FuncionariosPage } from "./Pages/Funcionarios/Index";
import { ProdutosPage } from "./Pages/Produtos/Index";
import { ServicosPage } from "./Pages/Servicos/Index";
import { AgendamentosPage } from "./Pages/Agendamentos/Index";
import { ConsultasPage } from "./Pages/Consultas/Index";
import { VacinasPage } from "./Pages/Vacinas/Index";
import { ProntuariosPage } from "./Pages/Prontuarios/Index";
import { VendasPage } from "./Pages/Vendas/Index";
import { ItemVendasPage } from "./Pages/ItemVendas/Index";
import { SemPermissao } from "./Pages/SemPermissao/Index";

const router = createBrowserRouter([
  {
    path: "/",
    element: <Home />,
  },
  {
    path: "/sobre",
    element: <Sobre />,
  },
  {
    path: "/login",
    element: <Login />,
  },
  {
    path: "/register",
    element: <Register />,
  },
  {
    path: "/sem-permissao",
    element: <SemPermissao />,
  },
  {
    path: "/",
    element: (
      <ProtectedRoute>
        <Layout />
      </ProtectedRoute>
    ),
    children: [
      { path: "dashboard", element: (
        <AdminProtectedRoute>
          <Dashboard />
        </AdminProtectedRoute>
      )},
      { path: "clientes", element: (
        <AdminProtectedRoute>
          <ClientesPage />
        </AdminProtectedRoute>
      )},
      { path: "pets", element: (
        <AdminProtectedRoute>
          <PetsPage />
        </AdminProtectedRoute>
      )},
      { path: "funcionarios", element: (
        <AdminProtectedRoute>
          <FuncionariosPage />
        </AdminProtectedRoute>
      )},
      { path: "produtos", element: (
        <AdminProtectedRoute>
          <ProdutosPage />
        </AdminProtectedRoute>
      )},
      { path: "servicos", element: (
        <AdminProtectedRoute>
          <ServicosPage />
        </AdminProtectedRoute>
      )},
      { path: "agendamentos", element: (
        <AdminProtectedRoute>
          <AgendamentosPage />
        </AdminProtectedRoute>
      )},
      { path: "consultas", element: (
        <AdminProtectedRoute>
          <ConsultasPage />
        </AdminProtectedRoute>
      )},
      { path: "vacinas", element: (
        <AdminProtectedRoute>
          <VacinasPage />
        </AdminProtectedRoute>
      )},
      { path: "prontuarios", element: (
        <AdminProtectedRoute>
          <ProntuariosPage />
        </AdminProtectedRoute>
      )},
      { path: "vendas", element: (
        <AdminProtectedRoute>
          <VendasPage />
        </AdminProtectedRoute>
      )},
      { path: "itens-venda", element: (
        <AdminProtectedRoute>
          <ItemVendasPage />
        </AdminProtectedRoute>
      )},
    ],
  },
]);

export { router };
