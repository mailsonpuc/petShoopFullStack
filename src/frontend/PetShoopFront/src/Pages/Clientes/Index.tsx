import { useState } from "react";
import { useCrud } from "../../Hooks/useCrud";
import { clientesApi } from "../../Services/api";
import type { Cliente, CreateClienteDto } from "../../Types";
import { Modal } from "../../Components/Modal";
import { ConfirmDialog } from "../../Components/ConfirmDialog";

export function ClientesPage() {
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

  const { items, isLoading, error, createItem, updateItem, deleteItem } = useCrud<Cliente, "clienteId">({
    fetchFn: clientesApi.list,
    createFn: clientesApi.create,
    updateFn: clientesApi.update,
    deleteFn: clientesApi.delete,
    idKey: "clienteId",
  });

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
    if (editingItem) {
      await updateItem(editingItem.clienteId, formData);
    } else {
      await createItem(formData);
    }
    setIsModalOpen(false);
  };

  const handleDelete = async () => {
    if (deleteId) {
      await deleteItem(deleteId);
      setDeleteId(null);
    }
  };

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

      <div className="overflow-hclienteIdden rounded-xl border border-slate-800 bg-slate-900">
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
          <tbody className="divclienteIde-y divclienteIde-slate-800">
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

      <Modal isOpen={isModalOpen} onClose={() => setIsModalOpen(false)} title={editingItem ? "Editar Cliente" : "Novo Cliente"}>
        <form onSubmit={handleSubmit} className="space-y-4">
          <div>
            <label className="mb-1.5 block text-sm font-medium text-slate-300">Nome</label>
            <input required value={formData.nome} onChange={(e) => setFormData({ ...formData, nome: e.target.value })} className="w-full rounded-lg border border-slate-800 bg-slate-950 px-4 py-2.5 text-sm text-white" />
          </div>
          <div>
            <label className="mb-1.5 block text-sm font-medium text-slate-300">CPF</label>
            <input required value={formData.cpf} onChange={(e) => setFormData({ ...formData, cpf: e.target.value })} className="w-full rounded-lg border border-slate-800 bg-slate-950 px-4 py-2.5 text-sm text-white" />
          </div>
          <div>
            <label className="mb-1.5 block text-sm font-medium text-slate-300">Email</label>
            <input required type="email" value={formData.email} onChange={(e) => setFormData({ ...formData, email: e.target.value })} className="w-full rounded-lg border border-slate-800 bg-slate-950 px-4 py-2.5 text-sm text-white" />
          </div>
          <div>
            <label className="mb-1.5 block text-sm font-medium text-slate-300">Telefone</label>
            <input required value={formData.telefone} onChange={(e) => setFormData({ ...formData, telefone: e.target.value })} className="w-full rounded-lg border border-slate-800 bg-slate-950 px-4 py-2.5 text-sm text-white" />
          </div>
          <div>
            <label className="mb-1.5 block text-sm font-medium text-slate-300">Data de Nascimento</label>
            <input required type="date" value={formData.dataDeNascimento} onChange={(e) => setFormData({ ...formData, dataDeNascimento: e.target.value })} className="w-full rounded-lg border border-slate-800 bg-slate-950 px-4 py-2.5 text-sm text-white" />
          </div>
          <div>
            <label className="mb-1.5 block text-sm font-medium text-slate-300">Endereço</label>
            <input required value={formData.endereco} onChange={(e) => setFormData({ ...formData, endereco: e.target.value })} className="w-full rounded-lg border border-slate-800 bg-slate-950 px-4 py-2.5 text-sm text-white" />
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
