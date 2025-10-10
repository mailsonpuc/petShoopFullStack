import { createBrowserRouter } from "react-router-dom"
import { Layout } from "./components/Layout"
import { Home } from "./pages/Home"
import { Cart } from "./pages/Cat"
import { ProdutoDetail } from "./pages/Details"
import { NotFound } from "./pages/Notfound"


const router = createBrowserRouter([
    {
        // Rota Raiz que contém o Layout
        element: <Layout />,
        children: [
            {
                path: "/",
                element: <Home />,
            },
            {
                path: "/cart",
                element: <Cart />
            }
            ,
            {
                path: "/product/:id",
                element: <ProdutoDetail />
            },
            {
                path: "*",
                element: <NotFound />,
            },
        ]
    }
])

export { router };