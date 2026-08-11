import { useState } from "react";
import { useCrud } from "../../Hooks/useCrud";
import { consultasApi } from "../../Services/api";
import type { Consulta, CreateConsultaDto } from "../../Types";
import { Modal } from "../../Components/Modal";
import { ConfirmDialog } from "../../Components/ConfirmDialog";

export function ConsultasPage() {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [editingItem, setEditingItem] = useState<Consulta | null>(null);
  const [deleteId, setDeleteId] = useState<string | null>(null);
  const [formData, setFormData] = useState<CreateConsultaDto>({
    petId: "",
    funcionarioId: "",
    dataConsulta: "",
    peso: 0,
    temperatura: 0,
    diagnostico: "",
    prescricao: "",
  });

  const { items, isLoading, error, createItem, updateItem, deleteItem } = useCrud<Consulta>({
    fetchFn: consultasApi.list,
    createFn: consultasApi.create,
    updateFn: consultasApi.update,
    deleteFn: consultasApi.delete,
  });

  const openCreate = () => {
    setEditingItem(null);
    setFormData({ petId: "", funcionarioId: "", dataConsulta: "", peso: 0, temperatura: 0, diagnostico: "", prescricao: "" });
    setIsModalOpen(true);
  };

  const openEdit = (consulta: Consulta) => {
    setEditingItem(consulta);
    setFormData({
      petId: consulta.petId,
      funcionarioId: consulta.funcionarioId,
      dataConsulta: consulta.dataConsulta.split(".")[0].replace("T", " "),
      peso: consulta.peso,
      temperatura: consulta.temperatura,
      diagnostico: consulta.diagnostico,
      prescricao: consulta.prescricao,
    });
    setIsModalOpen(true);
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (editingItem) {
      await updateItem(editingItem.id, formData);
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
          <h2 className="text-2xl font-bold text-white">Consultas</h2>
          <p className="mt-1 text-sm text-slate-400">Gerenciar consultas veterinárias</p>
        </div>
        <button onClick={openCreate} className="rounded-lg bg-blue-600 px-4 py-2 text-sm font-semibold text-white shadow-md shadow-blue-600/25 hover:bg-blue-500">Nova Consulta</button>
      </div>

      {error && <div className="rounded-lg border border-red-800 bg-red-950/50 p-3 text-sm text-red-400">{error}</div>}

      <div className="overflow-hidden rounded-xl border border-slate-800 bg-slate-900">
        <table className="w-full text-left text-sm">
          <thead className="border-b border-slate-800 bg-slate-800/50">
            <tr>
              <th className="px-6 py-3 font-medium text-slate-300">Pet ID</th>
              <th className="px-6 py-3 font-medium text-slate-300">Funcionário ID</th>
              <th className="px-6 py-3 font-medium text-slate-300">Data Consulta</th>
              <th className="px-6 py-3 font-medium text-slate-300">Peso</th>
              <th className="px-6 py-3 font-medium text-slate-300">Diagnóstico</th>
              <th className="px-6 py-3 font-medium text-slate-300">Ações</th>
            </tr>
          </thead>
          <tbody className="divide-y divide-slate-800">
            {isLoading ? (
              <tr><td colSpan={6} className="px-6 py-8 text-center text-slate-400">Carregando...</td></tr>
            ) : items.length === 0 ? (
              <tr><td colSpan={6} className="px-6 py-8 text-center text-slate-400">Nenhuma consulta encontrada</td></tr>
            ) : (
              items.map((item) => (
                <tr key={item.id} className="hover:bg-slate-800/30">
                  <td className="px-6 py-4 text-white">{item.petId}</td>
                  <td className="px-6 py-4 text-slate-300">{item.funcionarioId}</td>
                  <td className="px-6 py-4 text-slate-300">{item.dataConsulta}</td>
                  <td className="px-6 py-4 text-slate-300">{item.peso} kg</td>
                  <td className="px-6 py-4 text-slate-300">{item.diagnostico.substring(0, 30)}...</td>
                  <td className="px-6 py-4">
                    <div className="flex gap-2">
                      <button onClick={() => openEdit(item)} className="text-blue-400 hover:text-blue-300">Editar</button>
                      <button onClick={() => setDeleteId(item.id)} className="text-red-400 hover:text-red-300">Excluir</button>
                    </div>
                  </td>
                </tr>
              ))
            )}
          </tbody>
        </table>
      </div>

      <Modal isOpen={isModalOpen} onClose={() => setIsModalOpen(false)} title={editingItem ? "Editar Consulta" : "Nova Consulta"}>
        <form onSubmit={handleSubmit} className="space-y-4">
          <div className="grid grid-cols-2 gap-4">
            <div>
              <label className="mb-1.5 block text-sm font-medium text-slate-300">Pet ID</label>
              <input required value={formData.petId} onChange={(e) => setFormData({ ...formData, petId: e.target.value })} className="w-full rounded-lg border border-slate-800 bg-slate-950 px-4 py-2.5 text-sm text-white" />
            </div>
            <div>
              <label className="mb-1.5 block text-sm font-medium text-slate-300">Funcionário ID</label>
              <input required value={formData.funcionarioId} onChange={(e) => setFormData({ ...formData, funcionarioId: e.target.value })} className="w-full rounded-lg border border-slate-800 bg-slate-950 px-4 py-2.5 text-sm text-white" />
            </div>
          </div>
          <div className="grid grid-cols-2 gap-4">
            <div>
              <label className="mb-1.5 block text-sm font-medium text-slate-300">Data Consulta</label>
              <input required type="datetime-local" value={formData.dataConsulta} onChange={(e) => setFormData({ ...formData, dataConsulta: e.target.value })} className="w-full rounded-lg border border-slate-800 bg-slate-950 px-4 py-2.5 text-sm text-white" />
            </div>
            <div>
              <label className="mb-1.5 block text-sm font-medium text-slate-300">Peso (kg)</label>
              <input required type="number" step="0.1" value={formData.peso} onChange={(e) => setFormData({ ...formData, peso: parseFloat(e.target.value) })} className="w-full rounded-lg border border-slate-800 bg-slate-950 px-4 py-2.5 text-sm text-white" />
            </div>
          </div>
          <div>
            <label className="mb-1.5 block text-sm font-medium text-slate-300">Temperatura (°C)</label>
            <input required type="number" step="0.1" value={formData.temperatura} onChange={(e) => setFormData({ ...formData, temperatura: parseFloat(e.target.value) })} className="w-full rounded-lg border border-slate-800 bg-slate-950 px-4 py-2.5 text-sm text-white" />
          </div>
          <div>
            <label className="mb-1.5 block text-sm font-medium text-slate-300">Diagnóstico</label>
            <textarea required value={formData.diagnostico} onChange={(e) => setFormData({ ...formData, diagnostico: e.target.value })} className="w-full rounded-lg border border-slate-800 bg-slate-950 px-4 py-2.5 text-sm text-white" rows={2} />
          </div>
          <div>
            <label className="mb-1.5 block text-sm font-medium text-slate-300">Prescrição</label>
            <textarea required value={formData.prescricao} onChange={(e) => setFormData({ ...formData, prescricao: e.target.value })} className="w-full rounded-lg border border-slate-800 bg-slate-950 px-4 py-2.5 text-sm text-white" rows={2} />
          </div>
          <div className="flex justify-end gap-3 pt-2">
            <button type="button" onClick={() => setIsModalOpen(false)} className="rounded-lg border border-slate-700 px-4 py-2 text-sm font-medium text-slate-300 hover:bg-slate-800">Cancelar</button>
            <button type="submit" className="rounded-lg bg-blue-600 px-4 py-2 text-sm font-semibold text-white hover:bg-blue-500">Salvar</button>
          </div>
        </form>
      </Modal>

      <ConfirmDialog isOpen={!!deleteId} onClose={() => setDeleteId(null)} onConfirm={handleDelete} title="Excluir Consulta" message="Tem certeza que deseja excluir esta consulta?" />
    </div>
  );
}
