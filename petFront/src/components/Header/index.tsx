import { Link } from "react-router-dom"
import { FiShoppingCart } from "react-icons/fi"
import { CartContext } from "../Context/CartContext"
import { useContext } from "react"


export function Header() {
    const { cartAmount } = useContext(CartContext)


    return (
        <header className="w-full px-1 text-2xl font-extrabold text-indigo-400 tracking-wider uppercase bg-gray-900">
            <nav className="w-full max-w-7xl h-14 flex items-center justify-between px-5 mx-auto">

                {/* Logo */}
                <Link to="/" className="font-bold text-2xl">
                    PetShoop
                </Link>

                {/* Ícone do Carrinho */}
                <Link to="/cart" className="relative text-indigo-400">
                    <FiShoppingCart size={24} />
                    {
                        cartAmount > 0 && (
                            <span className="absolute -top-3 -right-3 px-2.5 bg-sky-500 rounded-full w-6 h-6 flex items-center justify-center text-white text-xs">
                                {cartAmount}
                            </span>
                        )
                    }

                </Link>


            </nav>
        </header>
    )
}