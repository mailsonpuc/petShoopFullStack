import { useState, useEffect } from "react";
import { useCrud } from "../../Hooks/useCrud";
import { prontuariosApi, petsApi, funcionariosApi } from "../../Services/api";
import type { Prontuario, CreateProntuarioDto, Pet, Funcionario } from "../../Types";
import { Modal } from "../../Components/Modal";
import { ConfirmDialog } from "../../Components/ConfirmDialog";

export function ProntuariosPage() {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [editingItem, setEditingItem] = useState<Prontuario | null>(null);
  const [deleteId, setDeleteId] = useState<string | null>(null);
  const [pets, setPets] = useState<Pet[]>([]);
  const [funcionarios, setFuncionarios] = useState<Funcionario[]>([]);
  const [formData, setFormData] = useState<CreateProntuarioDto>({
    petId: "",
    funcionarioId: "",
    dataRegistro: "",
    descricao: "",
  });

  const { items, isLoading, error, deleteError, createItem, updateItem, deleteItem } = useCrud<Prontuario, "prontuarioId">({
    fetchFn: prontuariosApi.list,
    createFn: prontuariosApi.create,
    updateFn: prontuariosApi.update,
    deleteFn: prontuariosApi.delete,
    idKey: "prontuarioId",
  });

  useEffect(() => {
    Promise.all([petsApi.list(), funcionariosApi.list()]).then(([p, f]) => {
      setPets(p);
      setFuncionarios(f);
    }).catch(() => {});
  }, []);

  const openCreate = () => {
    setEditingItem(null);
    setFormData({ petId: "", funcionarioId: "", dataRegistro: "", descricao: "" });
    setIsModalOpen(true);
  };

  const openEdit = (prontuario: Prontuario) => {
    setEditingItem(prontuario);
    setFormData({
      petId: prontuario.petId,
      funcionarioId: prontuario.funcionarioId,
      dataRegistro: prontuario.dataRegistro.split(".")[0].replace("T", " "),
      descricao: prontuario.descricao,
    });
    setIsModalOpen(true);
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (editingItem) {
      await updateItem(editingItem.prontuarioId, formData);
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
          <h2 className="text-2xl font-bold text-white">Prontuários</h2>
          <p className="mt-1 text-sm text-slate-400">Gerenciar prontuários médicos</p>
        </div>
        <button onClick={openCreate} className="rounded-lg bg-blue-600 px-4 py-2 text-sm font-semibold text-white shadow-md shadow-blue-600/25 hover:bg-blue-500">Novo Prontuário</button>
      </div>

      {error && <div className="rounded-lg border border-red-800 bg-red-950/50 p-3 text-sm text-red-400">{error}</div>}
      {deleteError && <div className="rounded-lg border border-red-800 bg-red-950/50 p-3 text-sm text-red-400">{deleteError}</div>}

      <div className="overflow-hidden rounded-xl border border-slate-800 bg-slate-900">
        <table className="w-full text-left text-sm">
          <thead className="border-b border-slate-800 bg-slate-800/50">
            <tr>
              <th className="px-6 py-3 font-medium text-slate-300">Pet</th>
              <th className="px-6 py-3 font-medium text-slate-300">Funcionário</th>
              <th className="px-6 py-3 font-medium text-slate-300">Data Registro</th>
              <th className="px-6 py-3 font-medium text-slate-300">Descrição</th>
              <th className="px-6 py-3 font-medium text-slate-300">Ações</th>
            </tr>
          </thead>
          <tbody className="divide-y divide-slate-800">
            {isLoading ? (
              <tr><td colSpan={5} className="px-6 py-8 text-center text-slate-400">Carregando...</td></tr>
            ) : items.length === 0 ? (
              <tr><td colSpan={5} className="px-6 py-8 text-center text-slate-400">Nenhum prontuário encontrado</td></tr>
            ) : (
              items.map((item) => (
                <tr key={item.prontuarioId} className="hover:bg-slate-800/30">
                  <td className="px-6 py-4 text-slate-300">{item.petNome || "-"}</td>
                  <td className="px-6 py-4 text-slate-300">{item.funcionarioNome || "-"}</td>
                  <td className="px-6 py-4 text-slate-300">{item.dataRegistro}</td>
                  <td className="px-6 py-4 text-slate-300">{item.descricao.substring(0, 40)}...</td>
                  <td className="px-6 py-4">
                    <div className="flex gap-2">
                      <button onClick={() => openEdit(item)} className="text-blue-400 hover:text-blue-300">Editar</button>
                       <button onClick={() => setDeleteId(item.prontuarioId)} className="text-red-400 hover:text-red-300">Excluir</button>
                    </div>
                  </td>
                </tr>
              ))
            )}
          </tbody>
        </table>
      </div>

      <Modal isOpen={isModalOpen} onClose={() => setIsModalOpen(false)} title={editingItem ? "Editar Prontuário" : "Novo Prontuário"}>
        <form onSubmit={handleSubmit} className="space-y-4">
          <div className="grid grid-cols-2 gap-4">
            <div>
              <label className="mb-1.5 block text-sm font-medium text-slate-300">Pet</label>
              <select required value={formData.petId} onChange={(e) => setFormData({ ...formData, petId: e.target.value })} className="w-full rounded-lg border border-slate-800 bg-slate-950 px-4 py-2.5 text-sm text-white">
                <option value="">Selecione</option>
                {pets.map((pet) => (
                  <option key={pet.petId} value={pet.petId}>{pet.nome}</option>
                ))}
              </select>
            </div>
            <div>
              <label className="mb-1.5 block text-sm font-medium text-slate-300">Funcionário</label>
              <select required value={formData.funcionarioId} onChange={(e) => setFormData({ ...formData, funcionarioId: e.target.value })} className="w-full rounded-lg border border-slate-800 bg-slate-950 px-4 py-2.5 text-sm text-white">
                <option value="">Selecione</option>
                {funcionarios.map((funcionario) => (
                  <option key={funcionario.funcionarioId} value={funcionario.funcionarioId}>{funcionario.nome}</option>
                ))}
              </select>
            </div>
          </div>
          <div>
            <label className="mb-1.5 block text-sm font-medium text-slate-300">Data Registro</label>
            <input required type="datetime-local" value={formData.dataRegistro} onChange={(e) => setFormData({ ...formData, dataRegistro: e.target.value })} className="w-full rounded-lg border border-slate-800 bg-slate-950 px-4 py-2.5 text-sm text-white" />
          </div>
          <div>
            <label className="mb-1.5 block text-sm font-medium text-slate-300">Descrição</label>
            <textarea required value={formData.descricao} onChange={(e) => setFormData({ ...formData, descricao: e.target.value })} className="w-full rounded-lg border border-slate-800 bg-slate-950 px-4 py-2.5 text-sm text-white" rows={3} />
          </div>
          <div className="flex justify-end gap-3 pt-2">
            <button type="button" onClick={() => setIsModalOpen(false)} className="rounded-lg border border-slate-700 px-4 py-2 text-sm font-medium text-slate-300 hover:bg-slate-800">Cancelar</button>
            <button type="submit" className="rounded-lg bg-blue-600 px-4 py-2 text-sm font-semibold text-white hover:bg-blue-500">Salvar</button>
          </div>
        </form>
      </Modal>

      <ConfirmDialog isOpen={!!deleteId} onClose={() => setDeleteId(null)} onConfirm={handleDelete} title="Excluir Prontuário" message="Tem certeza que deseja excluir este prontuário?" />
    </div>
  );
}
