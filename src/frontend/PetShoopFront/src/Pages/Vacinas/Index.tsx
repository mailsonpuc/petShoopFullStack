import { useState, useEffect } from "react";
import { useCrud } from "../../Hooks/useCrud";
import { vacinasApi, petsApi } from "../../Services/api";
import type { Vacina, CreateVacinaDto, Pet } from "../../Types";
import { Modal } from "../../Components/Modal";
import { ConfirmDialog } from "../../Components/ConfirmDialog";

export function VacinasPage() {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [editingItem, setEditingItem] = useState<Vacina | null>(null);
  const [deleteId, setDeleteId] = useState<string | null>(null);
  const [pets, setPets] = useState<Pet[]>([]);
  const [formData, setFormData] = useState<CreateVacinaDto>({
    petId: "",
    nome: "",
    fabricante: "",
    dataAplicacao: "",
    proximaDose: "",
  });

  const { items, isLoading, error, deleteError, createItem, updateItem, deleteItem } = useCrud<Vacina, "vacinaId">({
    fetchFn: vacinasApi.list,
    createFn: vacinasApi.create,
    updateFn: vacinasApi.update,
    deleteFn: vacinasApi.delete,
    idKey: "vacinaId",
  });

  useEffect(() => {
    petsApi.list().then(setPets).catch(() => setPets([]));
  }, []);

  const openCreate = () => {
    setEditingItem(null);
    setFormData({ petId: "", nome: "", fabricante: "", dataAplicacao: "", proximaDose: "" });
    setIsModalOpen(true);
  };

  const openEdit = (vacina: Vacina) => {
    setEditingItem(vacina);
    setFormData({
      petId: vacina.petId,
      nome: vacina.nome,
      fabricante: vacina.fabricante,
      dataAplicacao: vacina.dataAplicacao.split(".")[0].replace("T", " "),
      proximaDose: vacina.proximaDose.split(".")[0].replace("T", " "),
    });
    setIsModalOpen(true);
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (editingItem) {
      await updateItem(editingItem.vacinaId, formData);
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
          <h2 className="text-2xl font-bold text-white">Vacinas</h2>
          <p className="mt-1 text-sm text-slate-400">Gerenciar vacinas dos pets</p>
        </div>
        <button onClick={openCreate} className="rounded-lg bg-blue-600 px-4 py-2 text-sm font-semibold text-white shadow-md shadow-blue-600/25 hover:bg-blue-500">Nova Vacina</button>
      </div>

      {error && <div className="rounded-lg border border-red-800 bg-red-950/50 p-3 text-sm text-red-400">{error}</div>}
      {deleteError && <div className="rounded-lg border border-red-800 bg-red-950/50 p-3 text-sm text-red-400">{deleteError}</div>}

      <div className="overflow-hvacinaIdden rounded-xl border border-slate-800 bg-slate-900">
        <table className="w-full text-left text-sm">
          <thead className="border-b border-slate-800 bg-slate-800/50">
            <tr>
              <th className="px-6 py-3 font-medium text-slate-300">Pet</th>
              <th className="px-6 py-3 font-medium text-slate-300">Nome</th>
              <th className="px-6 py-3 font-medium text-slate-300">Fabricante</th>
              <th className="px-6 py-3 font-medium text-slate-300">Data Aplicação</th>
              <th className="px-6 py-3 font-medium text-slate-300">Próxima Dose</th>
              <th className="px-6 py-3 font-medium text-slate-300">Ações</th>
            </tr>
          </thead>
          <tbody className="divvacinaIde-y divvacinaIde-slate-800">
            {isLoading ? (
              <tr><td colSpan={6} className="px-6 py-8 text-center text-slate-400">Carregando...</td></tr>
            ) : items.length === 0 ? (
              <tr><td colSpan={6} className="px-6 py-8 text-center text-slate-400">Nenhuma vacina encontrada</td></tr>
            ) : (
              items.map((item) => (
                <tr key={item.vacinaId} className="hover:bg-slate-800/30">
                  <td className="px-6 py-4 text-slate-300">{item.petNome || "-"}</td>
                  <td className="px-6 py-4 text-white">{item.nome}</td>
                  <td className="px-6 py-4 text-slate-300">{item.fabricante}</td>
                  <td className="px-6 py-4 text-slate-300">{item.dataAplicacao}</td>
                  <td className="px-6 py-4 text-slate-300">{item.proximaDose}</td>
                  <td className="px-6 py-4">
                    <div className="flex gap-2">
                      <button onClick={() => openEdit(item)} className="text-blue-400 hover:text-blue-300">Editar</button>
                       <button onClick={() => setDeleteId(item.vacinaId)} className="text-red-400 hover:text-red-300">Excluir</button>
                    </div>
                  </td>
                </tr>
              ))
            )}
          </tbody>
        </table>
      </div>

      <Modal isOpen={isModalOpen} onClose={() => setIsModalOpen(false)} title={editingItem ? "Editar Vacina" : "Nova Vacina"}>
        <form onSubmit={handleSubmit} className="space-y-4">
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
            <label className="mb-1.5 block text-sm font-medium text-slate-300">Nome da Vacina</label>
            <input required value={formData.nome} onChange={(e) => setFormData({ ...formData, nome: e.target.value })} className="w-full rounded-lg border border-slate-800 bg-slate-950 px-4 py-2.5 text-sm text-white" />
          </div>
          <div>
            <label className="mb-1.5 block text-sm font-medium text-slate-300">Fabricante</label>
            <input required value={formData.fabricante} onChange={(e) => setFormData({ ...formData, fabricante: e.target.value })} className="w-full rounded-lg border border-slate-800 bg-slate-950 px-4 py-2.5 text-sm text-white" />
          </div>
          <div className="grvacinaId grvacinaId-cols-2 gap-4">
            <div>
              <label className="mb-1.5 block text-sm font-medium text-slate-300">Data Aplicação</label>
              <input required type="datetime-local" value={formData.dataAplicacao} onChange={(e) => setFormData({ ...formData, dataAplicacao: e.target.value })} className="w-full rounded-lg border border-slate-800 bg-slate-950 px-4 py-2.5 text-sm text-white" />
            </div>
            <div>
              <label className="mb-1.5 block text-sm font-medium text-slate-300">Próxima Dose</label>
              <input required type="datetime-local" value={formData.proximaDose} onChange={(e) => setFormData({ ...formData, proximaDose: e.target.value })} className="w-full rounded-lg border border-slate-800 bg-slate-950 px-4 py-2.5 text-sm text-white" />
            </div>
          </div>
          <div className="flex justify-end gap-3 pt-2">
            <button type="button" onClick={() => setIsModalOpen(false)} className="rounded-lg border border-slate-700 px-4 py-2 text-sm font-medium text-slate-300 hover:bg-slate-800">Cancelar</button>
            <button type="submit" className="rounded-lg bg-blue-600 px-4 py-2 text-sm font-semibold text-white hover:bg-blue-500">Salvar</button>
          </div>
        </form>
      </Modal>

      <ConfirmDialog isOpen={!!deleteId} onClose={() => setDeleteId(null)} onConfirm={handleDelete} title="Excluir Vacina" message="Tem certeza que deseja excluir esta vacina?" />
    </div>
  );
}
