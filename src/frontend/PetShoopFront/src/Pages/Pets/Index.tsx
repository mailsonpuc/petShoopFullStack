import { useState, useEffect } from "react";
import { useCrud } from "../../Hooks/useCrud";
import { petsApi, clientesApi } from "../../Services/api";
import type { Pet, CreatePetDto, Cliente } from "../../Types";
import { Modal } from "../../Components/Modal";
import { ConfirmDialog } from "../../Components/ConfirmDialog";

export function PetsPage() {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [editingItem, setEditingItem] = useState<Pet | null>(null);
  const [deleteId, setDeleteId] = useState<string | null>(null);
  const [clientes, setClientes] = useState<Cliente[]>([]);
  const [formData, setFormData] = useState<CreatePetDto>({
    nome: "",
    especie: "",
    raca: "",
    sexo: "",
    dataDeNascimento: "",
    peso: 0,
    cor: "",
    porte: "",
    observacoes: "",
    clienteId: "",
  });

  const { items, isLoading, error, createItem, updateItem, deleteItem } = useCrud<Pet>({
    fetchFn: petsApi.list,
    createFn: petsApi.create,
    updateFn: petsApi.update,
    deleteFn: petsApi.delete,
  });

  useEffect(() => {
    clientesApi.list().then(setClientes).catch(() => setClientes([]));
  }, []);

  const openCreate = () => {
    setEditingItem(null);
    setFormData({
      nome: "",
      especie: "",
      raca: "",
      sexo: "",
      dataDeNascimento: "",
      peso: 0,
      cor: "",
      porte: "",
      observacoes: "",
      clienteId: "",
    });
    setIsModalOpen(true);
  };

  const openEdit = (pet: Pet) => {
    setEditingItem(pet);
    setFormData({
      nome: pet.nome,
      especie: pet.especie,
      raca: pet.raca,
      sexo: pet.sexo,
      dataDeNascimento: pet.dataDeNascimento.split("T")[0],
      peso: pet.peso,
      cor: pet.cor,
      porte: pet.porte,
      observacoes: pet.observacoes,
      clienteId: pet.clienteId,
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
          <h2 className="text-2xl font-bold text-white">Pets</h2>
          <p className="mt-1 text-sm text-slate-400">Gerenciar pets dos clientes</p>
        </div>
        <button onClick={openCreate} className="rounded-lg bg-blue-600 px-4 py-2 text-sm font-semibold text-white shadow-md shadow-blue-600/25 hover:bg-blue-500">Novo Pet</button>
      </div>

      {error && <div className="rounded-lg border border-red-800 bg-red-950/50 p-3 text-sm text-red-400">{error}</div>}

      <div className="overflow-hidden rounded-xl border border-slate-800 bg-slate-900">
        <table className="w-full text-left text-sm">
          <thead className="border-b border-slate-800 bg-slate-800/50">
            <tr>
              <th className="px-6 py-3 font-medium text-slate-300">Nome</th>
              <th className="px-6 py-3 font-medium text-slate-300">Espécie</th>
              <th className="px-6 py-3 font-medium text-slate-300">Raça</th>
              <th className="px-6 py-3 font-medium text-slate-300">Sexo</th>
              <th className="px-6 py-3 font-medium text-slate-300">Peso</th>
              <th className="px-6 py-3 font-medium text-slate-300">Cliente</th>
              <th className="px-6 py-3 font-medium text-slate-300">Ações</th>
            </tr>
          </thead>
          <tbody className="divide-y divide-slate-800">
            {isLoading ? (
              <tr><td colSpan={7} className="px-6 py-8 text-center text-slate-400">Carregando...</td></tr>
            ) : items.length === 0 ? (
              <tr><td colSpan={7} className="px-6 py-8 text-center text-slate-400">Nenhum pet encontrado</td></tr>
            ) : (
              items.map((item) => (
                <tr key={item.id} className="hover:bg-slate-800/30">
                  <td className="px-6 py-4 text-white">{item.nome}</td>
                  <td className="px-6 py-4 text-slate-300">{item.especie}</td>
                  <td className="px-6 py-4 text-slate-300">{item.raca}</td>
                  <td className="px-6 py-4 text-slate-300">{item.sexo}</td>
                  <td className="px-6 py-4 text-slate-300">{item.peso} kg</td>
                  <td className="px-6 py-4 text-slate-300">{item.clienteNome || "-"}</td>
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

      <Modal isOpen={isModalOpen} onClose={() => setIsModalOpen(false)} title={editingItem ? "Editar Pet" : "Novo Pet"}>
        <form onSubmit={handleSubmit} className="space-y-4">
          <div>
            <label className="mb-1.5 block text-sm font-medium text-slate-300">Nome</label>
            <input required value={formData.nome} onChange={(e) => setFormData({ ...formData, nome: e.target.value })} className="w-full rounded-lg border border-slate-800 bg-slate-950 px-4 py-2.5 text-sm text-white" />
          </div>
          <div className="grid grid-cols-2 gap-4">
            <div>
              <label className="mb-1.5 block text-sm font-medium text-slate-300">Espécie</label>
              <select required value={formData.especie} onChange={(e) => setFormData({ ...formData, especie: e.target.value })} className="w-full rounded-lg border border-slate-800 bg-slate-950 px-4 py-2.5 text-sm text-white">
                <option value="">Selecione</option>
                <option value="Cachorro">Cachorro</option>
                <option value="Gato">Gato</option>
                <option value="Ave">Ave</option>
                <option value="Roedor">Roedor</option>
                <option value="Reptil">Reptil</option>
                <option value="Outro">Outro</option>
              </select>
            </div>
            <div>
              <label className="mb-1.5 block text-sm font-medium text-slate-300">Raça</label>
              <input required value={formData.raca} onChange={(e) => setFormData({ ...formData, raca: e.target.value })} className="w-full rounded-lg border border-slate-800 bg-slate-950 px-4 py-2.5 text-sm text-white" />
            </div>
          </div>
          <div className="grid grid-cols-2 gap-4">
            <div>
              <label className="mb-1.5 block text-sm font-medium text-slate-300">Sexo</label>
              <select value={formData.sexo} onChange={(e) => setFormData({ ...formData, sexo: e.target.value })} className="w-full rounded-lg border border-slate-800 bg-slate-950 px-4 py-2.5 text-sm text-white">
                <option value="">Selecione</option>
                <option value="Macho">Macho</option>
                <option value="Femea">Fêmea</option>
              </select>
            </div>
            <div>
              <label className="mb-1.5 block text-sm font-medium text-slate-300">Porte</label>
              <select value={formData.porte} onChange={(e) => setFormData({ ...formData, porte: e.target.value })} className="w-full rounded-lg border border-slate-800 bg-slate-950 px-4 py-2.5 text-sm text-white">
                <option value="">Selecione</option>
                <option value="Pequeno">Pequeno</option>
                <option value="Medio">Médio</option>
                <option value="Grande">Grande</option>
              </select>
            </div>
          </div>
          <div className="grid grid-cols-2 gap-4">
            <div>
              <label className="mb-1.5 block text-sm font-medium text-slate-300">Data de Nascimento</label>
              <input required type="date" value={formData.dataDeNascimento} onChange={(e) => setFormData({ ...formData, dataDeNascimento: e.target.value })} className="w-full rounded-lg border border-slate-800 bg-slate-950 px-4 py-2.5 text-sm text-white" />
            </div>
            <div>
              <label className="mb-1.5 block text-sm font-medium text-slate-300">Peso (kg)</label>
              <input required type="number" step="0.1" value={formData.peso} onChange={(e) => setFormData({ ...formData, peso: parseFloat(e.target.value) })} className="w-full rounded-lg border border-slate-800 bg-slate-950 px-4 py-2.5 text-sm text-white" />
            </div>
          </div>
          <div>
            <label className="mb-1.5 block text-sm font-medium text-slate-300">Cor</label>
            <input required value={formData.cor} onChange={(e) => setFormData({ ...formData, cor: e.target.value })} className="w-full rounded-lg border border-slate-800 bg-slate-950 px-4 py-2.5 text-sm text-white" />
          </div>
          <div>
            <label className="mb-1.5 block text-sm font-medium text-slate-300">Cliente</label>
            <select required value={formData.clienteId} onChange={(e) => setFormData({ ...formData, clienteId: e.target.value })} className="w-full rounded-lg border border-slate-800 bg-slate-950 px-4 py-2.5 text-sm text-white">
              <option value="">Selecione</option>
              {clientes.map((cliente) => (
                <option key={cliente.id} value={cliente.id}>{cliente.nome}</option>
              ))}
            </select>
          </div>
          <div>
            <label className="mb-1.5 block text-sm font-medium text-slate-300">Observações</label>
            <textarea value={formData.observacoes} onChange={(e) => setFormData({ ...formData, observacoes: e.target.value })} className="w-full rounded-lg border border-slate-800 bg-slate-950 px-4 py-2.5 text-sm text-white" rows={3} />
          </div>
          <div className="flex justify-end gap-3 pt-2">
            <button type="button" onClick={() => setIsModalOpen(false)} className="rounded-lg border border-slate-700 px-4 py-2 text-sm font-medium text-slate-300 hover:bg-slate-800">Cancelar</button>
            <button type="submit" className="rounded-lg bg-blue-600 px-4 py-2 text-sm font-semibold text-white hover:bg-blue-500">Salvar</button>
          </div>
        </form>
      </Modal>

      <ConfirmDialog isOpen={!!deleteId} onClose={() => setDeleteId(null)} onConfirm={handleDelete} title="Excluir Pet" message="Tem certeza que deseja excluir este pet?" />
    </div>
  );
}
