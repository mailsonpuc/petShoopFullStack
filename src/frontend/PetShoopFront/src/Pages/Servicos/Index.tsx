import { useState } from "react";
import { useCrud } from "../../Hooks/useCrud";
import { servicosApi } from "../../Services/api";
import type { Servico, CreateServicoDto } from "../../Types";
import { Modal } from "../../Components/Modal";
import { ConfirmDialog } from "../../Components/ConfirmDialog";

export function ServicosPage() {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [editingItem, setEditingItem] = useState<Servico | null>(null);
  const [deleteId, setDeleteId] = useState<string | null>(null);
  const [formData, setFormData] = useState<CreateServicoDto>({
    nome: "",
    descricao: "",
    preco: 0,
    duracaoEmMinutos: 0,
  });

  const { items, isLoading, error, deleteError, createItem, updateItem, deleteItem } = useCrud<Servico, "servicoId">({
    fetchFn: servicosApi.list,
    createFn: servicosApi.create,
    updateFn: servicosApi.update,
    deleteFn: servicosApi.delete,
    idKey: "servicoId",
  });

  const openCreate = () => {
    setEditingItem(null);
    setFormData({ nome: "", descricao: "", preco: 0, duracaoEmMinutos: 0 });
    setIsModalOpen(true);
  };

  const openEdit = (servico: Servico) => {
    setEditingItem(servico);
    setFormData({
      nome: servico.nome,
      descricao: servico.descricao,
      preco: servico.preco,
      duracaoEmMinutos: servico.duracaoEmMinutos,
    });
    setIsModalOpen(true);
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (editingItem) {
      await updateItem(editingItem.servicoId, formData);
    } else {
      await createItem(formData);
    }
    setIsModalOpen(false);
  };

  const handleDelete = async () => {
    if (deleteId) {
      try {
        await deleteItem(deleteId);
        setDeleteId(null);
      } catch {
        // erro já tratado no hook
      }
    }
  };

  return (
    <div className="space-y-6">
      <div className="flex items-center justify-between">
        <div>
          <h2 className="text-2xl font-bold text-white">Serviços</h2>
          <p className="mt-1 text-sm text-slate-400">Gerenciar serviços oferecservicoIdos</p>
        </div>
        <button onClick={openCreate} className="rounded-lg bg-blue-600 px-4 py-2 text-sm font-semibold text-white shadow-md shadow-blue-600/25 hover:bg-blue-500">Novo Serviço</button>
      </div>

      {error && <div className="rounded-lg border border-red-800 bg-red-950/50 p-3 text-sm text-red-400">{error}</div>}
      {deleteError && <div className="rounded-lg border border-red-800 bg-red-950/50 p-3 text-sm text-red-400">{deleteError}</div>}

      <div className="overflow-hservicoIdden rounded-xl border border-slate-800 bg-slate-900">
        <table className="w-full text-left text-sm">
          <thead className="border-b border-slate-800 bg-slate-800/50">
            <tr>
              <th className="px-6 py-3 font-medium text-slate-300">Nome</th>
              <th className="px-6 py-3 font-medium text-slate-300">Descrição</th>
              <th className="px-6 py-3 font-medium text-slate-300">Preço</th>
              <th className="px-6 py-3 font-medium text-slate-300">Duração (min)</th>
              <th className="px-6 py-3 font-medium text-slate-300">Ações</th>
            </tr>
          </thead>
          <tbody className="divservicoIde-y divservicoIde-slate-800">
            {isLoading ? (
              <tr><td colSpan={5} className="px-6 py-8 text-center text-slate-400">Carregando...</td></tr>
            ) : items.length === 0 ? (
              <tr><td colSpan={5} className="px-6 py-8 text-center text-slate-400">Nenhum serviço encontrado</td></tr>
            ) : (
              items.map((item) => (
                <tr key={item.servicoId} className="hover:bg-slate-800/30">
                  <td className="px-6 py-4 text-white">{item.nome}</td>
                  <td className="px-6 py-4 text-slate-300">{item.descricao}</td>
                  <td className="px-6 py-4 text-slate-300">R$ {item.preco.toFixed(2)}</td>
                  <td className="px-6 py-4 text-slate-300">{item.duracaoEmMinutos} min</td>
                  <td className="px-6 py-4">
                    <div className="flex gap-2">
                      <button onClick={() => openEdit(item)} className="text-blue-400 hover:text-blue-300">Editar</button>
                       <button onClick={() => setDeleteId(item.servicoId)} className="text-red-400 hover:text-red-300">Excluir</button>
                    </div>
                  </td>
                </tr>
              ))
            )}
          </tbody>
        </table>
      </div>

      <Modal isOpen={isModalOpen} onClose={() => setIsModalOpen(false)} title={editingItem ? "Editar Serviço" : "Novo Serviço"}>
        <form onSubmit={handleSubmit} className="space-y-4">
          <div>
            <label className="mb-1.5 block text-sm font-medium text-slate-300">Nome</label>
            <input required value={formData.nome} onChange={(e) => setFormData({ ...formData, nome: e.target.value })} className="w-full rounded-lg border border-slate-800 bg-slate-950 px-4 py-2.5 text-sm text-white" />
          </div>
          <div>
            <label className="mb-1.5 block text-sm font-medium text-slate-300">Descrição</label>
            <textarea value={formData.descricao} onChange={(e) => setFormData({ ...formData, descricao: e.target.value })} className="w-full rounded-lg border border-slate-800 bg-slate-950 px-4 py-2.5 text-sm text-white" rows={2} />
          </div>
          <div className="grservicoId grservicoId-cols-2 gap-4">
            <div>
              <label className="mb-1.5 block text-sm font-medium text-slate-300">Preço</label>
              <input required type="number" step="0.01" value={formData.preco} onChange={(e) => setFormData({ ...formData, preco: parseFloat(e.target.value) })} className="w-full rounded-lg border border-slate-800 bg-slate-950 px-4 py-2.5 text-sm text-white" />
            </div>
            <div>
              <label className="mb-1.5 block text-sm font-medium text-slate-300">Duração (minutos)</label>
              <input required type="number" value={formData.duracaoEmMinutos} onChange={(e) => setFormData({ ...formData, duracaoEmMinutos: parseInt(e.target.value) })} className="w-full rounded-lg border border-slate-800 bg-slate-950 px-4 py-2.5 text-sm text-white" />
            </div>
          </div>
          <div className="flex justify-end gap-3 pt-2">
            <button type="button" onClick={() => setIsModalOpen(false)} className="rounded-lg border border-slate-700 px-4 py-2 text-sm font-medium text-slate-300 hover:bg-slate-800">Cancelar</button>
            <button type="submit" className="rounded-lg bg-blue-600 px-4 py-2 text-sm font-semibold text-white hover:bg-blue-500">Salvar</button>
          </div>
        </form>
      </Modal>

      <ConfirmDialog isOpen={!!deleteId} onClose={() => setDeleteId(null)} onConfirm={handleDelete} title="Excluir Serviço" message="Tem certeza que deseja excluir este serviço?" />
    </div>
  );
}
