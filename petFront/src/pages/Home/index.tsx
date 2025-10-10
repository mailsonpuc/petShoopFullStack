import { BsCartPlus } from "react-icons/bs";
import { useEffect, useState, useContext } from "react";
import { api } from "../../services/Api";
import { CartContext } from "../../components/Context/CartContext";
import toast from "react-hot-toast";
import { Link } from "react-router-dom";
import { Loading } from "../../components/Loading";
import { getFullImageUrl } from "../../pages/GetImageRelativePath"




export interface ProductProps {
    productId: string;
    title: string;
    description: string;
    price: number;
    imagemUrl: string
}

export function Home() {
    const { AddItemCart } = useContext(CartContext)
    const [products, setProducts] = useState<ProductProps[]>([])
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        async function GetProducts() {
            try {
                setLoading(true);
                const response = await api.get("/api/Products")
                // se o retorno é um array direto:
                setProducts(response.data)
            }

            catch (error) {
                console.error("Erro ao buscar produtos:", error);
                toast.error("Erro ao carregar produtos.");
            }

            finally {
                setLoading(false);
            }


        }

        GetProducts()
    }, [])




    function HandleAddCartItem(product: ProductProps) {
        // console.log(product)
        toast.success("Produto adicionado no carrinho.")
        AddItemCart(product)

    }


    // Mostra o componente de carregamento enquanto busca os produtos
    if (loading) {
        return <Loading />;
    }

    return (
        <main className="w-full max-w-7xl px-4 mx-auto">
            <h1 className="font-bold text-2xl mb-4 mt-10 text-center">Produtos em alta</h1>

            {/* md (medium) tela pequena:
            lg (large): tela grandes */}

            <div className="grid grid-cols-1 gap-6 md:grid-cols-2  lg:grid-cols-5">

                {products.map((product) => (
                    <section key={product.productId} className="w-full p-2">
                        <Link to={`/product/${product.productId}`}>
                            <img
                                className="w-full rounded-lg max-h-70 mb-2 object-contain"
                                //pegar o http://localhost:5297/+ img/51HvjwRCBJL._AC_SL1000_.jpg
                                src={getFullImageUrl(product.imagemUrl)}
                                alt={product.title}
                            />
                        </Link>

                        <p className="font-medium mt-1 mb-2 line-clamp-2 h-12">{product.title}</p>
                        <div className="flex gap-3 items-center">
                            <strong className="text-zinc-700/90">
                                {product.price.toLocaleString("pt-br", {
                                    style: "currency",
                                    currency: "BRL"
                                })}
                            </strong>
                            <button
                                className="bg-zinc-900 p-1 rounded"
                                onClick={() => HandleAddCartItem(product)}
                            >
                                <BsCartPlus size={20} color="#FFF" />
                            </button>
                        </div>
                    </section>
                ))}


            </div>
        </main>
    )
}