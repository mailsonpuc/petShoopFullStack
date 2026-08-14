import { useState, useEffect } from "react";
import { useCrud } from "../../Hooks/useCrud";
import { itemVendasApi, produtosApi, vendasApi } from "../../Services/api";
import type { ItemVenda, CreateItemVendaDto, Produto, Venda } from "../../Types";
import { Modal } from "../../Components/Modal";
import { ConfirmDialog } from "../../Components/ConfirmDialog";
import { PaginationControls } from "../../Components/PaginationControls";

export function ItemVendasPage() {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [editingItem, setEditingItem] = useState<ItemVenda | null>(null);
  const [deleteId, setDeleteId] = useState<string | null>(null);
  const [produtos, setProdutos] = useState<Produto[]>([]);
  const [vendas, setVendas] = useState<Venda[]>([]);
  const [formData, setFormData] = useState<CreateItemVendaDto>({
    vendaId: "",
    produtoId: "",
    quantidade: 0,
    valorUnitario: 0,
  });

  const { items, isLoading, error, deleteError, pageNumber, pageSize, pagination, setPageNumber, createItem, updateItem, deleteItem } = useCrud<ItemVenda, "itemVendaId">({
    fetchFn: itemVendasApi.list,
    fetchPagedFn: itemVendasApi.getPaged,
    createFn: itemVendasApi.create,
    updateFn: itemVendasApi.update,
    deleteFn: itemVendasApi.delete,
    idKey: "itemVendaId",
  });

  useEffect(() => {
    Promise.all([produtosApi.list(), vendasApi.list()]).then(([p, v]) => {
      setProdutos(p);
      setVendas(v);
    }).catch(() => {});
  }, []);

  const openCreate = () => {
    setEditingItem(null);
    setFormData({ vendaId: "", produtoId: "", quantidade: 0, valorUnitario: 0 });
    setIsModalOpen(true);
  };

  const openEdit = (item: ItemVenda) => {
    setEditingItem(item);
    setFormData({
      vendaId: item.vendaId,
      produtoId: item.produtoId,
      quantidade: item.quantidade,
      valorUnitario: item.valorUnitario,
    });
    setIsModalOpen(true);
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (editingItem) {
      await updateItem(editingItem.itemVendaId, formData);
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
          <h2 className="text-2xl font-bold text-white">Itens de Venda</h2>
          <p className="mt-1 text-sm text-slate-400">Gerenciar itens de venda</p>
        </div>
        <button onClick={openCreate} className="rounded-lg bg-blue-600 px-4 py-2 text-sm font-semibold text-white shadow-md shadow-blue-600/25 hover:bg-blue-500">Novo Item</button>
      </div>

      {error && <div className="rounded-lg border border-red-800 bg-red-950/50 p-3 text-sm text-red-400">{error}</div>}
      {deleteError && <div className="rounded-lg border border-red-800 bg-red-950/50 p-3 text-sm text-red-400">{deleteError}</div>}

      <div className="overflow-hidden rounded-xl border border-slate-800 bg-slate-900">
        <table className="w-full text-left text-sm">
          <thead className="border-b border-slate-800 bg-slate-800/50">
            <tr>
              <th className="px-6 py-3 font-medium text-slate-300">Venda</th>
              <th className="px-6 py-3 font-medium text-slate-300">Cliente</th>
              <th className="px-6 py-3 font-medium text-slate-300">Produto</th>
              <th className="px-6 py-3 font-medium text-slate-300">Quantidade</th>
              <th className="px-6 py-3 font-medium text-slate-300">Valor Unitário</th>
              <th className="px-6 py-3 font-medium text-slate-300">Ações</th>
            </tr>
          </thead>
          <tbody className="divide-y divide-slate-800">
            {isLoading ? (
              <tr><td colSpan={6} className="px-6 py-8 text-center text-slate-400">Carregando...</td></tr>
            ) : items.length === 0 ? (
              <tr><td colSpan={6} className="px-6 py-8 text-center text-slate-400">Nenhum item encontrado</td></tr>
            ) : (
              items.map((item) => (
                <tr key={item.itemVendaId} className="hover:bg-slate-800/30">
                  <td className="px-6 py-4 text-white">{item.vendaInfo || "-"}</td>
                  <td className="px-6 py-4 text-slate-300">{item.clienteNome || "-"}</td>
                  <td className="px-6 py-4 text-slate-300">{item.produtoNome || "-"}</td>
                  <td className="px-6 py-4 text-slate-300">{item.quantidade}</td>
                  <td className="px-6 py-4 text-slate-300">R$ {item.valorUnitario.toFixed(2)}</td>
                  <td className="px-6 py-4">
                    <div className="flex gap-2">
                      <button onClick={() => openEdit(item)} className="text-blue-400 hover:text-blue-300">Editar</button>
                       <button onClick={() => setDeleteId(item.itemVendaId)} className="text-red-400 hover:text-red-300">Excluir</button>
                    </div>
                  </td>
                </tr>
              ))
            )}
          </tbody>
        </table>
      </div>

      <PaginationControls label="itens" pageNumber={pageNumber} pageSize={pageSize} totalCount={pagination.totalCount} totalPages={pagination.totalPages} setPageNumber={setPageNumber} />

      <Modal isOpen={isModalOpen} onClose={() => setIsModalOpen(false)} title={editingItem ? "Editar Item de Venda" : "Novo Item de Venda"}>
        <form onSubmit={handleSubmit} className="space-y-4">
          <div className="grid grid-cols-2 gap-4">
            <div>
              <label className="mb-1.5 block text-sm font-medium text-slate-300">Venda</label>
              <select required value={formData.vendaId} onChange={(e) => setFormData({ ...formData, vendaId: e.target.value })} className="w-full rounded-lg border border-slate-800 bg-slate-950 px-4 py-2.5 text-sm text-white">
                <option value="">Selecione</option>
                {vendas.map((venda) => (
                  <option key={venda.vendaId} value={venda.vendaId}>{venda.clienteNome || venda.vendaId} - {new Date(venda.dataVenda).toLocaleString()}</option>
                ))}
              </select>
            </div>
            <div>
              <label className="mb-1.5 block text-sm font-medium text-slate-300">Produto</label>
              <select required value={formData.produtoId} onChange={(e) => setFormData({ ...formData, produtoId: e.target.value })} className="w-full rounded-lg border border-slate-800 bg-slate-950 px-4 py-2.5 text-sm text-white">
                <option value="">Selecione</option>
                {produtos.map((produto) => (
                  <option key={produto.produtoId} value={produto.produtoId}>{produto.nome}</option>
                ))}
              </select>
            </div>
          </div>
          <div className="grid grid-cols-2 gap-4">
            <div>
              <label className="mb-1.5 block text-sm font-medium text-slate-300">Quantidade</label>
              <input required type="number" value={formData.quantidade} onChange={(e) => setFormData({ ...formData, quantidade: parseInt(e.target.value) })} className="w-full rounded-lg border border-slate-800 bg-slate-950 px-4 py-2.5 text-sm text-white" />
            </div>
            <div>
              <label className="mb-1.5 block text-sm font-medium text-slate-300">Valor Unitário</label>
              <input required type="number" step="0.01" value={formData.valorUnitario} onChange={(e) => setFormData({ ...formData, valorUnitario: parseFloat(e.target.value) })} className="w-full rounded-lg border border-slate-800 bg-slate-950 px-4 py-2.5 text-sm text-white" />
            </div>
          </div>
          <div className="flex justify-end gap-3 pt-2">
            <button type="button" onClick={() => setIsModalOpen(false)} className="rounded-lg border border-slate-700 px-4 py-2 text-sm font-medium text-slate-300 hover:bg-slate-800">Cancelar</button>
            <button type="submit" className="rounded-lg bg-blue-600 px-4 py-2 text-sm font-semibold text-white hover:bg-blue-500">Salvar</button>
          </div>
        </form>
      </Modal>

      <ConfirmDialog isOpen={!!deleteId} onClose={() => setDeleteId(null)} onConfirm={handleDelete} title="Excluir Item de Venda" message="Tem certeza que deseja excluir este item?" />
    </div>
  );
}
