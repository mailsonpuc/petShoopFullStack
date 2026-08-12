import { useState, useEffect } from "react";
import { clientesApi } from "../../Services/api";
import type { Cliente, CreateClienteDto } from "../../Types";
import { Modal } from "../../Components/Modal";
import { ConfirmDialog } from "../../Components/ConfirmDialog";
import { FaChevronLeft, FaChevronRight } from "react-icons/fa";

export function ClientesPage() {
  const [items, setItems] = useState<Cliente[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize] = useState(10);
  const [totalPages, setTotalPages] = useState(0);
  const [totalCount, setTotalCount] = useState(0);

  const [isModalOpen, setIsModalOpen] = useState(false);
  const [editingItem, setEditingItem] = useState<Cliente | null>(null);
  const [deleteId, setDeleteId] = useState<string | null>(null);
  const [formData, setFormData] = useState<CreateClienteDto>({
    nome: "",
    cpf: "",
    email: "",
    telefone: "",
    dataDeNascimento: "",
    endereco: "",
  });

  const loadClientes = async () => {
    setIsLoading(true);
    setError(null);
    try {
      const response = await clientesApi.getPaged(pageNumber, pageSize);
      setItems(response.data);
      setTotalPages(response.pagination.totalPages);
      setTotalCount(response.pagination.totalCount);
    } catch (err: unknown) {
      const message = err instanceof Error ? err.message : "Erro ao carregar clientes";
      setError(message);
    } finally {
      setIsLoading(false);
    }
  };

  useEffect(() => {
    loadClientes();
  }, [pageNumber]);

  const openCreate = () => {
    setEditingItem(null);
    setFormData({
      nome: "",
      cpf: "",
      email: "",
      telefone: "",
      dataDeNascimento: "",
      endereco: "",
    });
    setIsModalOpen(true);
  };

  const openEdit = (cliente: Cliente) => {
    setEditingItem(cliente);
    setFormData({
      nome: cliente.nome,
      cpf: cliente.cpf,
      email: cliente.email,
      telefone: cliente.telefone,
      dataDeNascimento: cliente.dataDeNascimento.split("T")[0],
      endereco: cliente.endereco,
    });
    setIsModalOpen(true);
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      if (editingItem) {
        await clientesApi.update(editingItem.clienteId, formData);
      } else {
        await clientesApi.create(formData);
      }
      setIsModalOpen(false);
      loadClientes();
    } catch (err: unknown) {
      const message = err instanceof Error ? err.message : "Erro ao salvar cliente";
      setError(message);
    }
  };

  const handleDelete = async () => {
    if (deleteId) {
      try {
        await clientesApi.delete(deleteId);
        setDeleteId(null);
        loadClientes();
      } catch (err: unknown) {
        const message = err instanceof Error ? err.message : "Erro ao excluir cliente";
        setError(message);
      }
    }
  };

  const startItem = items.length > 0 ? (pageNumber - 1) * pageSize + 1 : 0;
  const endItem = Math.min(pageNumber * pageSize, totalCount);

  return (
    <div className="space-y-6">
      <div className="flex items-center justify-between">
        <div>
          <h2 className="text-2xl font-bold text-white">Clientes</h2>
          <p className="mt-1 text-sm text-slate-400">Gerenciar clientes do petshop</p>
        </div>
        <button
          onClick={openCreate}
          className="rounded-lg bg-blue-600 px-4 py-2 text-sm font-semibold text-white shadow-md shadow-blue-600/25 transition-all hover:bg-blue-500"
        >
          Novo Cliente
        </button>
      </div>

      {error && <div className="rounded-lg border border-red-800 bg-red-950/50 p-3 text-sm text-red-400">{error}</div>}

      <div className="overflow-hidden rounded-xl border border-slate-800 bg-slate-900">
        <table className="w-full text-left text-sm">
          <thead className="border-b border-slate-800 bg-slate-800/50">
            <tr>
              <th className="px-6 py-3 font-medium text-slate-300">Nome</th>
              <th className="px-6 py-3 font-medium text-slate-300">CPF</th>
              <th className="px-6 py-3 font-medium text-slate-300">Email</th>
              <th className="px-6 py-3 font-medium text-slate-300">Telefone</th>
              <th className="px-6 py-3 font-medium text-slate-300">Ações</th>
            </tr>
          </thead>
          <tbody className="divide-y divide-slate-800">
            {isLoading ? (
              <tr>
                <td colSpan={5} className="px-6 py-8 text-center text-slate-400">Carregando...</td>
              </tr>
            ) : items.length === 0 ? (
              <tr>
                <td colSpan={5} className="px-6 py-8 text-center text-slate-400">Nenhum cliente encontrado</td>
              </tr>
            ) : (
              items.map((item) => (
                <tr key={item.clienteId} className="hover:bg-slate-800/30">
                  <td className="px-6 py-4 text-white">{item.nome}</td>
                  <td className="px-6 py-4 text-slate-300">{item.cpf}</td>
                  <td className="px-6 py-4 text-slate-300">{item.email}</td>
                  <td className="px-6 py-4 text-slate-300">{item.telefone}</td>
                  <td className="px-6 py-4">
                    <div className="flex gap-2">
                      <button onClick={() => openEdit(item)} className="text-blue-400 hover:text-blue-300">Editar</button>
                      <button onClick={() => setDeleteId(item.clienteId)} className="text-red-400 hover:text-red-300">Excluir</button>
                    </div>
                  </td>
                </tr>
              ))
            )}
          </tbody>
        </table>
      </div>

      <div className="flex items-center justify-between">
        <div className="text-sm text-slate-400">
          Mostrando {startItem} a {endItem} de {totalCount} clientes
        </div>
        <div className="flex items-center gap-2">
          <button
            onClick={() => setPageNumber((p) => Math.max(1, p - 1))}
            disabled={pageNumber === 1}
            className="flex items-center gap-1 rounded-lg border border-slate-700 px-3 py-2 text-sm font-medium text-slate-300 hover:bg-slate-800 disabled:opacity-50 disabled:cursor-not-allowed"
          >
            <FaChevronLeft className="h-4 w-4" />
            Voltar
          </button>
          <span className="flex items-center px-3 text-sm text-slate-400">
            Página {pageNumber} de {totalPages}
          </span>
          <button
            onClick={() => setPageNumber((p) => Math.min(totalPages, p + 1))}
            disabled={pageNumber === totalPages || totalPages === 0}
            className="flex items-center gap-1 rounded-lg border border-slate-700 px-3 py-2 text-sm font-medium text-slate-300 hover:bg-slate-800 disabled:opacity-50 disabled:cursor-not-allowed"
          >
            Avançar
            <FaChevronRight className="h-4 w-4" />
          </button>
        </div>
      </div>

      <Modal isOpen={isModalOpen} onClose={() => setIsModalOpen(false)} title={editingItem ? "Editar Cliente" : "Novo Cliente"}>
        <form onSubmit={handleSubmit} className="space-y-4">
          <div>
            <label className="mb-1.5 block text-sm font-medium text-slate-300">Nome</label>
            <input required value={formData.nome} onChange={(e) => setFormData({ ...formData, nome: e.target.value })} placeholder="Nome completo do cliente" maxLength={100} className="w-full rounded-lg border border-slate-800 bg-slate-950 px-4 py-2.5 text-sm text-white" />
          </div>
          <div>
            <label className="mb-1.5 block text-sm font-medium text-slate-300">CPF</label>
            <input required
              placeholder="00000000000 (somente números)"
              maxLength={11}
              value={formData.cpf}

              onChange={(e) => {
                const onlyNumbers = e.target.value.replace(/\D/g, "");
                setFormData({ ...formData, cpf: onlyNumbers });
              }}
              className="w-full rounded-lg border border-slate-800 bg-slate-950 px-4 py-2.5 text-sm text-white" />
          </div>

          <div>
            <label className="mb-1.5 block text-sm font-medium text-slate-300">Email</label>
            <input required type="email" value={formData.email} onChange={(e) => setFormData({ ...formData, email: e.target.value })} placeholder="exemplo@email.com" maxLength={100} className="w-full rounded-lg border border-slate-800 bg-slate-950 px-4 py-2.5 text-sm text-white" />
          </div>
          <div>
            <label className="mb-1.5 block text-sm font-medium text-slate-300">Telefone</label>
            <input required value={formData.telefone} onChange={(e) => setFormData({ ...formData, telefone: e.target.value })} placeholder="11999999999" maxLength={11} className="w-full rounded-lg border border-slate-800 bg-slate-950 px-4 py-2.5 text-sm text-white" />
          </div>
          <div>
            <label className="mb-1.5 block text-sm font-medium text-slate-300">Data de Nascimento</label>
            <input required type="date" value={formData.dataDeNascimento} onChange={(e) => setFormData({ ...formData, dataDeNascimento: e.target.value })} className="w-full rounded-lg border border-slate-800 bg-slate-950 px-4 py-2.5 text-sm text-white" />
          </div>
          <div>
            <label className="mb-1.5 block text-sm font-medium text-slate-300">Endereço</label>
            <input required value={formData.endereco} onChange={(e) => setFormData({ ...formData, endereco: e.target.value })} placeholder="Rua, número, bairro, cidade" maxLength={200} className="w-full rounded-lg border border-slate-800 bg-slate-950 px-4 py-2.5 text-sm text-white" />
          </div>
          <div className="flex justify-end gap-3 pt-2">
            <button type="button" onClick={() => setIsModalOpen(false)} className="rounded-lg border border-slate-700 px-4 py-2 text-sm font-medium text-slate-300 hover:bg-slate-800">Cancelar</button>
            <button type="submit" className="rounded-lg bg-blue-600 px-4 py-2 text-sm font-semibold text-white hover:bg-blue-500">Salvar</button>
          </div>
        </form>
      </Modal>

      <ConfirmDialog isOpen={!!deleteId} onClose={() => setDeleteId(null)} onConfirm={handleDelete} title="Excluir Cliente" message="Tem certeza que deseja excluir este cliente?" />
    </div>
  );
}
