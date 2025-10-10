import { RouterProvider } from 'react-router-dom'
import { router } from './Routes'
import './App.css'
import CartProvider from './components/Context/CartContext'
import { Toaster } from 'react-hot-toast'
import { Footer } from './pages/Footer'

function App() {

  return (
    <div>
      {/* agora tudo passar pelo CartProvider */}
      <CartProvider>
        <Toaster
          position='top-center'
          reverseOrder={false}
        />
        <RouterProvider router={router} />
        <Footer/>
      </CartProvider>
    </div>
  )
}

export default App
