import { useState, useEffect } from "react";
import { useCrud } from "../../Hooks/useCrud";
import { vendasApi, clientesApi } from "../../Services/api";
import type { Venda, CreateVendaDto, Cliente } from "../../Types";
import { Modal } from "../../Components/Modal";
import { ConfirmDialog } from "../../Components/ConfirmDialog";

export function VendasPage() {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [editingItem, setEditingItem] = useState<Venda | null>(null);
  const [deleteId, setDeleteId] = useState<string | null>(null);
  const [clientes, setClientes] = useState<Cliente[]>([]);
  const [formData, setFormData] = useState<CreateVendaDto>({
    clienteId: "",
    dataVenda: "",
    valorTotal: 0,
    formaPagamento: "",
  });

  const { items, isLoading, error, deleteError, createItem, updateItem, deleteItem } = useCrud<Venda, "vendaId">({
    fetchFn: vendasApi.list,
    createFn: vendasApi.create,
    updateFn: vendasApi.update,
    deleteFn: vendasApi.delete,
    idKey: "vendaId",
  });

  useEffect(() => {
    clientesApi.list().then(setClientes).catch(() => setClientes([]));
  }, []);

  const openCreate = () => {
    setEditingItem(null);
    setFormData({ clienteId: "", dataVenda: "", valorTotal: 0, formaPagamento: "" });
    setIsModalOpen(true);
  };

  const openEdit = (venda: Venda) => {
    setEditingItem(venda);
    setFormData({
      clienteId: venda.clienteId,
      dataVenda: venda.dataVenda.split(".")[0].replace("T", " "),
      valorTotal: venda.valorTotal,
      formaPagamento: venda.formaPagamento,
    });
    setIsModalOpen(true);
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (editingItem) {
      await updateItem(editingItem.vendaId, formData);
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
          <h2 className="text-2xl font-bold text-white">Vendas</h2>
          <p className="mt-1 text-sm text-slate-400">Gerenciar vendas</p>
        </div>
        <button onClick={openCreate} className="rounded-lg bg-blue-600 px-4 py-2 text-sm font-semibold text-white shadow-md shadow-blue-600/25 hover:bg-blue-500">Nova Venda</button>
      </div>

      {error && <div className="rounded-lg border border-red-800 bg-red-950/50 p-3 text-sm text-red-400">{error}</div>}
      {deleteError && <div className="rounded-lg border border-red-800 bg-red-950/50 p-3 text-sm text-red-400">{deleteError}</div>}

      <div className="overflow-hidden rounded-xl border border-slate-800 bg-slate-900">
        <table className="w-full text-left text-sm">
          <thead className="border-b border-slate-800 bg-slate-800/50">
            <tr>
              <th className="px-6 py-3 font-medium text-slate-300">Cliente</th>
              <th className="px-6 py-3 font-medium text-slate-300">Data Venda</th>
              <th className="px-6 py-3 font-medium text-slate-300">Valor Total</th>
              <th className="px-6 py-3 font-medium text-slate-300">Forma Pagamento</th>
              <th className="px-6 py-3 font-medium text-slate-300">Ações</th>
            </tr>
          </thead>
          <tbody className="divide-y divide-slate-800">
            {isLoading ? (
              <tr><td colSpan={5} className="px-6 py-8 text-center text-slate-400">Carregando...</td></tr>
            ) : items.length === 0 ? (
              <tr><td colSpan={5} className="px-6 py-8 text-center text-slate-400">Nenhuma venda encontrada</td></tr>
            ) : (
              items.map((item) => (
                <tr key={item.vendaId} className="hover:bg-slate-800/30">
                  <td className="px-6 py-4 text-white">{item.clienteNome || "-"}</td>
                  <td className="px-6 py-4 text-slate-300">{item.dataVenda}</td>
                  <td className="px-6 py-4 text-slate-300">R$ {item.valorTotal.toFixed(2)}</td>
                  <td className="px-6 py-4 text-slate-300">{item.formaPagamento}</td>
                  <td className="px-6 py-4">
                    <div className="flex gap-2">
                      <button onClick={() => openEdit(item)} className="text-blue-400 hover:text-blue-300">Editar</button>
                       <button onClick={() => setDeleteId(item.vendaId)} className="text-red-400 hover:text-red-300">Excluir</button>
                    </div>
                  </td>
                </tr>
              ))
            )}
          </tbody>
        </table>
      </div>

      <Modal isOpen={isModalOpen} onClose={() => setIsModalOpen(false)} title={editingItem ? "Editar Venda" : "Nova Venda"}>
        <form onSubmit={handleSubmit} className="space-y-4">
          <div>
            <label className="mb-1.5 block text-sm font-medium text-slate-300">Cliente</label>
            <select required value={formData.clienteId} onChange={(e) => setFormData({ ...formData, clienteId: e.target.value })} className="w-full rounded-lg border border-slate-800 bg-slate-950 px-4 py-2.5 text-sm text-white">
              <option value="">Selecione</option>
              {clientes.map((cliente) => (
                <option key={cliente.clienteId} value={cliente.clienteId}>{cliente.nome}</option>
              ))}
            </select>
          </div>
          <div className="grid grid-cols-2 gap-4">
            <div>
              <label className="mb-1.5 block text-sm font-medium text-slate-300">Data Venda</label>
              <input required type="datetime-local" value={formData.dataVenda} onChange={(e) => setFormData({ ...formData, dataVenda: e.target.value })} className="w-full rounded-lg border border-slate-800 bg-slate-950 px-4 py-2.5 text-sm text-white" />
            </div>
            <div>
              <label className="mb-1.5 block text-sm font-medium text-slate-300">Valor Total</label>
              <input required type="number" step="0.01" value={formData.valorTotal} onChange={(e) => setFormData({ ...formData, valorTotal: parseFloat(e.target.value) })} className="w-full rounded-lg border border-slate-800 bg-slate-950 px-4 py-2.5 text-sm text-white" />
            </div>
          </div>
          <div>
            <label className="mb-1.5 block text-sm font-medium text-slate-300">Forma de Pagamento</label>
            <select required value={formData.formaPagamento} onChange={(e) => setFormData({ ...formData, formaPagamento: e.target.value })} className="w-full rounded-lg border border-slate-800 bg-slate-950 px-4 py-2.5 text-sm text-white">
              <option value="">Selecione</option>
              <option value="Dinheiro">Dinheiro</option>
              <option value="CartaoCredito">Cartão de Crédito</option>
              <option value="CartaoDebito">Cartão de Débito</option>
              <option value="Pix">Pix</option>
            </select>
          </div>
          <div className="flex justify-end gap-3 pt-2">
            <button type="button" onClick={() => setIsModalOpen(false)} className="rounded-lg border border-slate-700 px-4 py-2 text-sm font-medium text-slate-300 hover:bg-slate-800">Cancelar</button>
            <button type="submit" className="rounded-lg bg-blue-600 px-4 py-2 text-sm font-semibold text-white hover:bg-blue-500">Salvar</button>
          </div>
        </form>
      </Modal>

      <ConfirmDialog isOpen={!!deleteId} onClose={() => setDeleteId(null)} onConfirm={handleDelete} title="Excluir Venda" message="Tem certeza que deseja excluir esta venda?" />
    </div>
  );
}
