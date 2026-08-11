import { useState, useEffect, useCallback } from "react";

interface UseCrudOptions<T extends Record<string, any>, TIdKey extends keyof T = "id", TCreate = Omit<T, TIdKey>> {
  fetchFn: () => Promise<T[]>;
  createFn: (data: TCreate) => Promise<T>;
  updateFn: (id: string, data: TCreate) => Promise<T>;
  deleteFn: (id: string) => Promise<void>;
  idKey?: TIdKey;
}

export function useCrud<T extends Record<string, any>, TIdKey extends keyof T = "id", TCreate = Omit<T, TIdKey>>(options: UseCrudOptions<T, TIdKey, TCreate>) {
  const [items, setItems] = useState<T[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  const idField = options.idKey ?? "id";

  const loadItems = useCallback(async () => {
    try {
      setIsLoading(true);
      setError(null);
      const data = await options.fetchFn();
      setItems(data);
    } catch (err) {
      setError(err instanceof Error ? err.message : "Erro ao carregar dados");
    } finally {
      setIsLoading(false);
    }
  }, [options.fetchFn]);

  const createItem = async (data: TCreate) => {
    const newItem = await options.createFn(data);
    setItems((prev) => [...prev, newItem]);
    return newItem;
  };

  const updateItem = async (id: string, data: TCreate) => {
    const updated = await options.updateFn(id, data);
    setItems((prev) => prev.map((item) => (item[idField] === id ? updated : item)));
    return updated;
  };

  const deleteItem = async (id: string) => {
    await options.deleteFn(id);
    setItems((prev) => prev.filter((item) => item[idField] !== id));
  };

  useEffect(() => {
    loadItems();
  }, [loadItems]);

  return {
    items,
    isLoading,
    error,
    reload: loadItems,
    createItem,
    updateItem,
    deleteItem,
  };
}
