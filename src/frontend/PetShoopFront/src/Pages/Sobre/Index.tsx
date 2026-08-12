import { Header } from "../Header/Index";

export function Sobre() {
  return (
    <div className="min-h-screen w-full bg-slate-950 text-white">
      <Header />
      <main className="mx-auto max-w-7xl px-6 py-12">
        <h1 className="text-3xl font-bold text-white">Sobre</h1>
        <p className="mt-4 text-slate-300">
          O PetShoop é uma plataforma completa para gerenciamento de petshops,
          oferecendo controle de clientes, pets, agendamentos, vendas e muito mais.
          Nossa missão é simplificar o dia a dia de petshops com tecnologia,
          agilidade e uma experiência intuitiva para equipes e tutores.
        </p>
        <p className="mt-4 text-slate-300">
          Desenvolvido com foco em praticidade, o sistema reúne em um só lugar
          informações essenciais para o funcionamento do negócio, permitindo
          acompanhar atendimentos, estoque, finanças e histórico de pets com
          rapidez e organização.
        </p>
      </main>
    </div>
  );
}
