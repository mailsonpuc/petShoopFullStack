import { createContext, useState, type ReactNode } from "react"
import type { ProductProps } from "../../pages/Home";

interface CartContextData {
    cart: CartProps[]
    cartAmount: number;
    AddItemCart: (newItem: ProductProps) => void;
    RemoveItemCart: (product: CartProps) => void;
    total: string;

}

interface CartProps {
    productId: string;
    title: string;
    description: string;
    price: number;
    imagemUrl: string

    amount: number;
    total: number;
}

interface CartProviderProps {
    children: ReactNode;
}


export const CartContext = createContext({} as CartContextData)



function CartProvider({ children }: CartProviderProps) {
    const [cart, setCart] = useState<CartProps[]>([])
    const [total, setTotal] = useState("")
    function AddItemCart(newItem: ProductProps) {
        //adicioona no carrinho 
        // se ja nao existe no carrinho
        const indexItem = cart.findIndex(item => item.productId === newItem.productId)

        if (indexItem !== -1) {
            // se entrou aqui apenas soma +1 na quantidade e calcular o total desse carrinho
            let cartList = cart;
            cartList[indexItem].amount = cartList[indexItem].amount + 1
            cartList[indexItem].total = cartList[indexItem].amount * cartList[indexItem].price
            setCart(cartList)
            TotalResultCart(cartList)
            return

        }

        //adicionar esse item na lista
        let data = {
            ...newItem,
            amount: 1,
            total: newItem.price
        }

        setCart(products => [...products, data])
        TotalResultCart([...cart, data])
    }


    function RemoveItemCart(product: CartProps) {
        const indexItem = cart.findIndex(item => item.productId === product.productId)
        if (cart[indexItem]?.amount > 1) {
            //Diminuir apenas 1 amount do que tem

            let cartList = cart

            cartList[indexItem].amount = cartList[indexItem].amount - 1
            cartList[indexItem].total = cartList[indexItem].total - cartList[indexItem].price
            setCart(cartList)
            TotalResultCart(cartList)
            return
        }

        //remover
        const removeItem = cart.filter(item => item.productId !== product.productId)
        setCart(removeItem)
        TotalResultCart(removeItem)
    }

    function TotalResultCart(items: CartProps[]) {
        let myCart = items
        let result = myCart.reduce((acc, obj) => { return acc + obj.total }, 0)
        const resultFormated = result.toLocaleString("pt-BR", { style: "currency", currency: "BRL" })
        setTotal(resultFormated)
    }

    return (
        <CartContext.Provider value={{
            cart,
            cartAmount: cart.length,
            AddItemCart,
            RemoveItemCart,
            total,
        }}>
            {children}
        </CartContext.Provider>
    )
}


export default CartProvider