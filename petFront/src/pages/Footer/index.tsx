
export function Footer() {
  // Define os links de navegação
  const navLinks = [
    { title: 'Início', href: '/' },
    { title: 'Produtos', href: '/products' },
    { title: 'Sobre Nós', href: '/about' },
    { title: 'Contato', href: '/contact' },
  ];

  return (
    <footer className="bg-gray-900 text-white mt-12">
      <div className="max-w-7xl mx-auto py-12 px-4 sm:px-6 lg:px-8">
        <div className="grid grid-cols-2 md:grid-cols-4 gap-8">
          
          {/* Coluna 1: Nome da Loja e Slogan */}
          <div className="col-span-2 md:col-span-1">
            <h3 className="text-2xl font-extrabold text-indigo-400 tracking-wider uppercase">
              O Rei das Rações
            </h3>
            <p className="mt-2 text-sm text-gray-400">
              Sua melhor escolha em ração para seu pet.
            </p>
          </div>

          {/* Coluna 2: Navegação Rápida */}
          <div>
            <h4 className="text-lg font-semibold text-white tracking-wider uppercase">
              Navegação
            </h4>
            <ul className="mt-4 space-y-2">
              {navLinks.map((link) => (
                <li key={link.title}>
                  <a
                    href={link.href}
                    className="text-base text-gray-400 hover:text-indigo-400 transition duration-300"
                  >
                    {link.title}
                  </a>
                </li>
              ))}
            </ul>
          </div>

          {/* Coluna 3: Ajuda e Suporte */}
          <div>
            <h4 className="text-lg font-semibold text-white tracking-wider uppercase">
              Ajuda
            </h4>
            <ul className="mt-4 space-y-2">
              <li>
                <a
                  href="/faq"
                  className="text-base text-gray-400 hover:text-indigo-400 transition duration-300"
                >
                  FAQ
                </a>
              </li>
              <li>
                <a
                  href="/shipping"
                  className="text-base text-gray-400 hover:text-indigo-400 transition duration-300"
                >
                  Entrega
                </a>
              </li>
              <li>
                <a
                  href="/returns"
                  className="text-base text-gray-400 hover:text-indigo-400 transition duration-300"
                >
                  Trocas e Devoluções
                </a>
              </li>
            </ul>
          </div>

          {/* Coluna 4: Contato */}
          <div className="col-span-2 md:col-span-1">
            <h4 className="text-lg font-semibold text-white tracking-wider uppercase">
              Fale Conosco
            </h4>
            <address className="mt-4 space-y-2 not-italic text-gray-400">
              <p>
                <strong className="text-white">Email:</strong> contato@racao.com
              </p>
              <p>
                <strong className="text-white">Telefone:</strong> (99) 99999-9999
              </p>
              <p>
                <strong className="text-white">Endereço:</strong> Rua localHost, 127
              </p>
            </address>
          </div>
        </div>

        {/* Linha de Direitos Reservados */}
        <div className="mt-12 pt-8 border-t border-gray-700">
          <p className="text-center text-sm text-gray-500">
            &copy; {new Date().getFullYear()} **O Rei das rações**. Todos os direitos reservados.
          </p>
          <p className="text-center text-xs text-gray-600 mt-1">
            Desenvolvido com React e Tailwind CSS. + Dotnet
          </p>
        </div>

      </div>
    </footer>
  );
}