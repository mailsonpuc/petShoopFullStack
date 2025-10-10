import { useContext } from "react"
import { CartContext } from "../../components/Context/CartContext"
import { Link } from "react-router-dom"
import { getFullImageUrl } from "../../pages/GetImageRelativePath"


export function Cart() {
    const { cart, total, AddItemCart, RemoveItemCart } = useContext(CartContext)


    return (
        <div className="w-full max-w-7xl mx-auto">
            <h1 className="font-medium text-2xl text-center my-4"> Meu Carrinho</h1>

            {cart.length === 0 && (
                <div className="flex flex-col items-center justify-center">
                    <p className="font-medium">Seu Carrinho está vazio...</p>
                    <Link
                        to="/"
                        className="bg-slate-600 my-3 p-1 px-3 text-white font-medium rounded"
                    >
                        Acessar produtos
                    </Link>
                </div>
            )}

            {cart.map((item) => (
                <section className="flex items-center justify-between border-b-2 border-gray-300" key={item.productId}>
                    <img
                        className="w-28"
                        src={getFullImageUrl(item.imagemUrl)}
                        alt={item.title}
                    />

                    <strong>
                        Preço: {item.price.toLocaleString("pt-BR", {
                            style: "currency",
                            currency: "BRL"
                        })}
                    </strong>

                    <div className="flex items-center justify-center gap-3">
                        <button className="bg-slate-600 px-2 rounded text-white- font-medium flex items-center justify-center" onClick={() => RemoveItemCart(item)}>
                            -
                        </button>
                        {item.amount}
                        <button className="bg-slate-600 px-2 rounded text-white- font-medium flex items-center justify-center" onClick={() => AddItemCart(item)}>
                            +
                        </button>
                    </div>

                    <strong className="float-right">
                        SubTotal: {item.total.toLocaleString("pt-BR", {
                            style: "currency",
                            currency: "BRL"
                        })}
                    </strong>
                </section>
            ))}

            {
                cart.length !== 0 &&
                <p className="font-bold mt-4">Total: {total}</p>
            }
        </div>
    )
}