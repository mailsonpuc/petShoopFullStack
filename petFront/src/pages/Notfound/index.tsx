import { Link } from "react-router-dom";

export function NotFound() {
    return (
        // Container principal: tela cheia, centralizado e com fundo suave
        <div className="min-h-screen bg-gray-50 flex flex-col justify-center items-center p-4">

            <div className="text-center">


                <p className="text-9xl font-extrabold text-indigo-600 opacity-90 tracking-widest sm:text-[150px]">
                    404
                </p>


                <div className="bg-gray-800 text-white px-4 py-2 text-sm rounded-md rotate-[-2deg] shadow-lg mb-8">
                    <h1 className="text-2xl font-bold tracking-wider uppercase sm:text-3xl">
                        Opps! Essa página não existe.
                    </h1>
                </div>


                <p className="text-lg text-gray-600 mb-8 max-w-md mx-auto">
                    Parece que você seguiu um link quebrado ou digitou um endereço que não está ativo. Não se preocupe, acontece!
                </p>


                <Link
                    to="/"
                    className="inline-flex items-center px-6 py-3 border border-transparent text-base font-medium rounded-full shadow-sm text-white bg-indigo-600 hover:bg-indigo-700 transition duration-300 ease-in-out transform hover:scale-105"
                >


                    <svg className="w-5 h-5 mr-2" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
                        <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M3 12l2-2m0 0l7-7 7 7M5 10v10a1 1 0 001 1h3m10-11l2 2m-2-2v10a1 1 0 01-1 1h-3m-6 0a1 1 0 001-1v-4a1 1 0 011-1h2a1 1 0 011 1v4a1 1 0 001 1m-6 0h6"></path>
                    </svg>
                    Voltar para a Home
                </Link>

            </div>
        </div>
    );
}