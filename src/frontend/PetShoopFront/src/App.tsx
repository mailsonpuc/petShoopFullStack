import { createBrowserRouter } from "react-router-dom";
import { Home } from "./Pages/Home/Index";
import { Login } from "./Pages/Login/Index";


const router = createBrowserRouter([
  {
    path: "/",
    element: <Home />,
  },
  {
    path: "/login",
    element: <Login />,
  },
]);

export { router };