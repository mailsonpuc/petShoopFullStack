import { useEffect, useState, useContext } from "react"
import { useParams, useNavigate } from "react-router-dom"
import type { ProductProps } from "../Home"
import { api } from "../../services/Api"
import { getFullImageUrl } from "../../pages/GetImageRelativePath"
import { BsCartPlus } from "react-icons/bs"
import { CartContext } from "../../components/Context/CartContext"
import toast from "react-hot-toast";
import { Loading } from "../../components/Loading";


export function ProdutoDetail() {
    const { id } = useParams()
    const [product, setProducts] = useState<ProductProps>()
    const { AddItemCart } = useContext(CartContext)
    const navigate = useNavigate()
    const [loading, setLoading] = useState(true);


    useEffect(() => {
        async function GetProduct() {
            try {
                setLoading(true);
                const response = await api.get(`/api/Products/${id}`)
                //console.log(response.data)
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



        GetProduct()
    }, [id])


    function HandleAddItem(product: ProductProps) {
        toast.success("Produto adicionado no carrinho.")
        AddItemCart(product)

        navigate("/cart")
    }


    // Mostra o componente de carregamento enquanto busca os produtos
    if (loading) {
        return <Loading />;
    }


    
    return (
        <div>
            <main className="w-full max-w-7xl px-4 mx-auto my-6">
                {product && (
                    <section className="w-full">
                        <div className="flex flex-col lg:flex-row">
                            <img
                                className="flex-1 w-full max-h-72 object-contain"
                                //pegar o http://localhost:5297/+ img/51HvjwRCBJL._AC_SL1000_.jpg
                                src={getFullImageUrl(product.imagemUrl)}
                                alt={product.title}
                            />

                            <div className="flex-1">
                                <p className="font-bold text-2xl mt-4 mb-2">{product.title}</p>
                                <p className="my-4">{product.description}</p>
                                <strong className="text-zinc-700/90 text-xl">
                                    {product.price.toLocaleString("pt-BR", {
                                        style: "currency",
                                        currency: "BRL"
                                    })}
                                </strong>
                                <button className="bg-zinc-900 p-1 rounded ml-3" onClick={() => HandleAddItem(product)}>
                                    <BsCartPlus size={20} color="#FFF" />
                                </button>
                            </div>


                        </div>
                    </section>
                )}

            </main>
        </div>
    )

}