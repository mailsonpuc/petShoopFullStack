import { Header } from "../Header/Index";

export function Home() {
  return (
    <div className="min-h-screen w-full bg-slate-950 text-white">
      <Header />
      <main className="mx-auto max-w-7xl px-6 py-12">
        <h1 className="text-3xl font-bold">Página Home</h1>
      </main>
    </div>
  );
}