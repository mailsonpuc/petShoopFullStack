import { createBrowserRouter } from "react-router-dom";
import { Home } from "./Pages/Home/Index";
import { Login } from "./Pages/Login/Index";
import { Layout } from "./Components/Layout/Index";
import { ProtectedRoute } from "./Components/ProtectedRoute";
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

const router = createBrowserRouter([
  {
    path: "/",
    element: <Home />,
  },
  {
    path: "/login",
    element: <Login />,
  },
  {
    path: "/",
    element: (
      <ProtectedRoute>
        <Layout />
      </ProtectedRoute>
    ),
    children: [
      { path: "dashboard", element: <Dashboard /> },
      { path: "clientes", element: <ClientesPage /> },
      { path: "pets", element: <PetsPage /> },
      { path: "funcionarios", element: <FuncionariosPage /> },
      { path: "produtos", element: <ProdutosPage /> },
      { path: "servicos", element: <ServicosPage /> },
      { path: "agendamentos", element: <AgendamentosPage /> },
      { path: "consultas", element: <ConsultasPage /> },
      { path: "vacinas", element: <VacinasPage /> },
      { path: "prontuarios", element: <ProntuariosPage /> },
      { path: "vendas", element: <VendasPage /> },
      { path: "itens-venda", element: <ItemVendasPage /> },
    ],
  },
]);

export { router };
