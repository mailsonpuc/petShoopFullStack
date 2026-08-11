import { useState, useEffect } from "react";
import { useCrud } from "../../Hooks/useCrud";
import { agendamentosApi, petsApi, servicosApi, funcionariosApi } from "../../Services/api";
import type { Agendamento, CreateAgendamentoDto, Pet, Servico, Funcionario } from "../../Types";
import { Modal } from "../../Components/Modal";
import { ConfirmDialog } from "../../Components/ConfirmDialog";

export function AgendamentosPage() {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [editingItem, setEditingItem] = useState<Agendamento | null>(null);
  const [deleteId, setDeleteId] = useState<string | null>(null);
  const [pets, setPets] = useState<Pet[]>([]);
  const [servicos, setServicos] = useState<Servico[]>([]);
  const [funcionarios, setFuncionarios] = useState<Funcionario[]>([]);
  const [formData, setFormData] = useState<CreateAgendamentoDto>({
    petId: "",
    servicoId: "",
    funcionarioId: "",
    dataHora: "",
    status: "Agendado",
    observacoes: "",
  });

  const { items, isLoading, error, createItem, updateItem, deleteItem } = useCrud<Agendamento>({
    fetchFn: agendamentosApi.list,
    createFn: agendamentosApi.create,
    updateFn: agendamentosApi.update,
    deleteFn: agendamentosApi.delete,
  });

  useEffect(() => {
    Promise.all([petsApi.list(), servicosApi.list(), funcionariosApi.list()]).then(([p, s, f]) => {
      setPets(p);
      setServicos(s);
      setFuncionarios(f);
    }).catch(() => {});
  }, []);

  const openCreate = () => {
    setEditingItem(null);
    setFormData({ petId: "", servicoId: "", funcionarioId: "", dataHora: "", status: "Agendado", observacoes: "" });
    setIsModalOpen(true);
  };

  const openEdit = (agendamento: Agendamento) => {
    setEditingItem(agendamento);
    setFormData({
      petId: agendamento.petId,
      servicoId: agendamento.servicoId,
      funcionarioId: agendamento.funcionarioId,
      dataHora: agendamento.dataHora.split(".")[0].replace("T", " "),
      status: agendamento.status,
      observacoes: agendamento.observacoes,
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
          <h2 className="text-2xl font-bold text-white">Agendamentos</h2>
          <p className="mt-1 text-sm text-slate-400">Gerenciar agendamentos</p>
        </div>
        <button onClick={openCreate} className="rounded-lg bg-blue-600 px-4 py-2 text-sm font-semibold text-white shadow-md shadow-blue-600/25 hover:bg-blue-500">Novo Agendamento</button>
      </div>

      {error && <div className="rounded-lg border border-red-800 bg-red-950/50 p-3 text-sm text-red-400">{error}</div>}

      <div className="overflow-hidden rounded-xl border border-slate-800 bg-slate-900">
        <table className="w-full text-left text-sm">
          <thead className="border-b border-slate-800 bg-slate-800/50">
            <tr>
              <th className="px-6 py-3 font-medium text-slate-300">Pet</th>
              <th className="px-6 py-3 font-medium text-slate-300">Serviço</th>
              <th className="px-6 py-3 font-medium text-slate-300">Funcionário</th>
              <th className="px-6 py-3 font-medium text-slate-300">Data/Hora</th>
              <th className="px-6 py-3 font-medium text-slate-300">Status</th>
              <th className="px-6 py-3 font-medium text-slate-300">Ações</th>
            </tr>
          </thead>
          <tbody className="divide-y divide-slate-800">
            {isLoading ? (
              <tr><td colSpan={6} className="px-6 py-8 text-center text-slate-400">Carregando...</td></tr>
            ) : items.length === 0 ? (
              <tr><td colSpan={6} className="px-6 py-8 text-center text-slate-400">Nenhum agendamento encontrado</td></tr>
            ) : (
              items.map((item) => (
                <tr key={item.id} className="hover:bg-slate-800/30">
                  <td className="px-6 py-4 text-slate-300">{item.petNome || "-"}</td>
                  <td className="px-6 py-4 text-slate-300">{item.servicoNome || "-"}</td>
                  <td className="px-6 py-4 text-slate-300">{item.funcionarioNome || "-"}</td>
                  <td className="px-6 py-4 text-slate-300">{item.dataHora}</td>
                  <td className="px-6 py-4 text-slate-300">{item.status}</td>
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

      <Modal isOpen={isModalOpen} onClose={() => setIsModalOpen(false)} title={editingItem ? "Editar Agendamento" : "Novo Agendamento"}>
        <form onSubmit={handleSubmit} className="space-y-4">
          <div className="grid grid-cols-2 gap-4">
            <div>
              <label className="mb-1.5 block text-sm font-medium text-slate-300">Pet</label>
              <select required value={formData.petId} onChange={(e) => setFormData({ ...formData, petId: e.target.value })} className="w-full rounded-lg border border-slate-800 bg-slate-950 px-4 py-2.5 text-sm text-white">
                <option value="">Selecione</option>
                {pets.map((pet) => (
                  <option key={pet.id} value={pet.id}>{pet.nome}</option>
                ))}
              </select>
            </div>
            <div>
              <label className="mb-1.5 block text-sm font-medium text-slate-300">Serviço</label>
              <select required value={formData.servicoId} onChange={(e) => setFormData({ ...formData, servicoId: e.target.value })} className="w-full rounded-lg border border-slate-800 bg-slate-950 px-4 py-2.5 text-sm text-white">
                <option value="">Selecione</option>
                {servicos.map((servico) => (
                  <option key={servico.id} value={servico.id}>{servico.nome}</option>
                ))}
              </select>
            </div>
          </div>
          <div>
            <label className="mb-1.5 block text-sm font-medium text-slate-300">Funcionário</label>
            <select required value={formData.funcionarioId} onChange={(e) => setFormData({ ...formData, funcionarioId: e.target.value })} className="w-full rounded-lg border border-slate-800 bg-slate-950 px-4 py-2.5 text-sm text-white">
              <option value="">Selecione</option>
              {funcionarios.map((funcionario) => (
                <option key={funcionario.id} value={funcionario.id}>{funcionario.nome}</option>
              ))}
            </select>
          </div>
          <div className="grid grid-cols-2 gap-4">
            <div>
              <label className="mb-1.5 block text-sm font-medium text-slate-300">Data/Hora</label>
              <input required type="datetime-local" value={formData.dataHora} onChange={(e) => setFormData({ ...formData, dataHora: e.target.value })} className="w-full rounded-lg border border-slate-800 bg-slate-950 px-4 py-2.5 text-sm text-white" />
            </div>
            <div>
              <label className="mb-1.5 block text-sm font-medium text-slate-300">Status</label>
              <select value={formData.status} onChange={(e) => setFormData({ ...formData, status: e.target.value })} className="w-full rounded-lg border border-slate-800 bg-slate-950 px-4 py-2.5 text-sm text-white">
                <option value="Agendado">Agendado</option>
                <option value="Confirmado">Confirmado</option>
                <option value="Cancelado">Cancelado</option>
                <option value="Concluido">Concluído</option>
              </select>
            </div>
          </div>
          <div>
            <label className="mb-1.5 block text-sm font-medium text-slate-300">Observações</label>
            <textarea value={formData.observacoes} onChange={(e) => setFormData({ ...formData, observacoes: e.target.value })} className="w-full rounded-lg border border-slate-800 bg-slate-950 px-4 py-2.5 text-sm text-white" rows={2} />
          </div>
          <div className="flex justify-end gap-3 pt-2">
            <button type="button" onClick={() => setIsModalOpen(false)} className="rounded-lg border border-slate-700 px-4 py-2 text-sm font-medium text-slate-300 hover:bg-slate-800">Cancelar</button>
            <button type="submit" className="rounded-lg bg-blue-600 px-4 py-2 text-sm font-semibold text-white hover:bg-blue-500">Salvar</button>
          </div>
        </form>
      </Modal>

      <ConfirmDialog isOpen={!!deleteId} onClose={() => setDeleteId(null)} onConfirm={handleDelete} title="Excluir Agendamento" message="Tem certeza que deseja excluir este agendamento?" />
    </div>
  );
}
