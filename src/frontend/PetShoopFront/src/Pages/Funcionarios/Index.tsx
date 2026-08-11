import { useState } from "react";
import { useCrud } from "../../Hooks/useCrud";
import { funcionariosApi } from "../../Services/api";
import type { Funcionario, CreateFuncionarioDto } from "../../Types";
import { Modal } from "../../Components/Modal";
import { ConfirmDialog } from "../../Components/ConfirmDialog";

export function FuncionariosPage() {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [editingItem, setEditingItem] = useState<Funcionario | null>(null);
  const [deleteId, setDeleteId] = useState<string | null>(null);
  const [formData, setFormData] = useState<CreateFuncionarioDto>({
    nome: "",
    cpf: "",
    email: "",
    telefone: "",
    cargo: "",
    salario: 0,
    dataAdmissao: "",
  });

  const { items, isLoading, error, createItem, updateItem, deleteItem } = useCrud<Funcionario>({
    fetchFn: funcionariosApi.list,
    createFn: funcionariosApi.create,
    updateFn: funcionariosApi.update,
    deleteFn: funcionariosApi.delete,
  });

  const openCreate = () => {
    setEditingItem(null);
    setFormData({ nome: "", cpf: "", email: "", telefone: "", cargo: "", salario: 0, dataAdmissao: "" });
    setIsModalOpen(true);
  };

  const openEdit = (funcionario: Funcionario) => {
    setEditingItem(funcionario);
    setFormData({
      nome: funcionario.nome,
      cpf: funcionario.cpf,
      email: funcionario.email,
      telefone: funcionario.telefone,
      cargo: funcionario.cargo,
      salario: funcionario.salario,
      dataAdmissao: funcionario.dataAdmissao.split("T")[0],
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
          <h2 className="text-2xl font-bold text-white">Funcionários</h2>
          <p className="mt-1 text-sm text-slate-400">Gerenciar funcionários</p>
        </div>
        <button onClick={openCreate} className="rounded-lg bg-blue-600 px-4 py-2 text-sm font-semibold text-white shadow-md shadow-blue-600/25 hover:bg-blue-500">Novo Funcionário</button>
      </div>

      {error && <div className="rounded-lg border border-red-800 bg-red-950/50 p-3 text-sm text-red-400">{error}</div>}

      <div className="overflow-hidden rounded-xl border border-slate-800 bg-slate-900">
        <table className="w-full text-left text-sm">
          <thead className="border-b border-slate-800 bg-slate-800/50">
            <tr>
              <th className="px-6 py-3 font-medium text-slate-300">Nome</th>
              <th className="px-6 py-3 font-medium text-slate-300">CPF</th>
              <th className="px-6 py-3 font-medium text-slate-300">Email</th>
              <th className="px-6 py-3 font-medium text-slate-300">Cargo</th>
              <th className="px-6 py-3 font-medium text-slate-300">Ações</th>
            </tr>
          </thead>
          <tbody className="divide-y divide-slate-800">
            {isLoading ? (
              <tr><td colSpan={5} className="px-6 py-8 text-center text-slate-400">Carregando...</td></tr>
            ) : items.length === 0 ? (
              <tr><td colSpan={5} className="px-6 py-8 text-center text-slate-400">Nenhum funcionário encontrado</td></tr>
            ) : (
              items.map((item) => (
                <tr key={item.id} className="hover:bg-slate-800/30">
                  <td className="px-6 py-4 text-white">{item.nome}</td>
                  <td className="px-6 py-4 text-slate-300">{item.cpf}</td>
                  <td className="px-6 py-4 text-slate-300">{item.email}</td>
                  <td className="px-6 py-4 text-slate-300">{item.cargo}</td>
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

      <Modal isOpen={isModalOpen} onClose={() => setIsModalOpen(false)} title={editingItem ? "Editar Funcionário" : "Novo Funcionário"}>
        <form onSubmit={handleSubmit} className="space-y-4">
          <div>
            <label className="mb-1.5 block text-sm font-medium text-slate-300">Nome</label>
            <input required value={formData.nome} onChange={(e) => setFormData({ ...formData, nome: e.target.value })} className="w-full rounded-lg border border-slate-800 bg-slate-950 px-4 py-2.5 text-sm text-white" />
          </div>
          <div className="grid grid-cols-2 gap-4">
            <div>
              <label className="mb-1.5 block text-sm font-medium text-slate-300">CPF</label>
              <input required value={formData.cpf} onChange={(e) => setFormData({ ...formData, cpf: e.target.value })} className="w-full rounded-lg border border-slate-800 bg-slate-950 px-4 py-2.5 text-sm text-white" />
            </div>
            <div>
              <label className="mb-1.5 block text-sm font-medium text-slate-300">Email</label>
              <input required type="email" value={formData.email} onChange={(e) => setFormData({ ...formData, email: e.target.value })} className="w-full rounded-lg border border-slate-800 bg-slate-950 px-4 py-2.5 text-sm text-white" />
            </div>
          </div>
          <div className="grid grid-cols-2 gap-4">
            <div>
              <label className="mb-1.5 block text-sm font-medium text-slate-300">Telefone</label>
              <input required value={formData.telefone} onChange={(e) => setFormData({ ...formData, telefone: e.target.value })} className="w-full rounded-lg border border-slate-800 bg-slate-950 px-4 py-2.5 text-sm text-white" />
            </div>
            <div>
              <label className="mb-1.5 block text-sm font-medium text-slate-300">Cargo</label>
              <select value={formData.cargo} onChange={(e) => setFormData({ ...formData, cargo: e.target.value })} className="w-full rounded-lg border border-slate-800 bg-slate-950 px-4 py-2.5 text-sm text-white">
                <option value="">Selecione</option>
                <option value="Veterinario">Veterinário</option>
                <option value="Atendente">Atendente</option>
                <option value="Banhista">Banhista</option>
                <option value="Administrador">Administrador</option>
              </select>
            </div>
          </div>
          <div className="grid grid-cols-2 gap-4">
            <div>
              <label className="mb-1.5 block text-sm font-medium text-slate-300">Salário</label>
              <input required type="number" step="0.01" value={formData.salario} onChange={(e) => setFormData({ ...formData, salario: parseFloat(e.target.value) })} className="w-full rounded-lg border border-slate-800 bg-slate-950 px-4 py-2.5 text-sm text-white" />
            </div>
            <div>
              <label className="mb-1.5 block text-sm font-medium text-slate-300">Data Admissão</label>
              <input required type="date" value={formData.dataAdmissao} onChange={(e) => setFormData({ ...formData, dataAdmissao: e.target.value })} className="w-full rounded-lg border border-slate-800 bg-slate-950 px-4 py-2.5 text-sm text-white" />
            </div>
          </div>
          <div className="flex justify-end gap-3 pt-2">
            <button type="button" onClick={() => setIsModalOpen(false)} className="rounded-lg border border-slate-700 px-4 py-2 text-sm font-medium text-slate-300 hover:bg-slate-800">Cancelar</button>
            <button type="submit" className="rounded-lg bg-blue-600 px-4 py-2 text-sm font-semibold text-white hover:bg-blue-500">Salvar</button>
          </div>
        </form>
      </Modal>

      <ConfirmDialog isOpen={!!deleteId} onClose={() => setDeleteId(null)} onConfirm={handleDelete} title="Excluir Funcionário" message="Tem certeza que deseja excluir este funcionário?" />
    </div>
  );
}
