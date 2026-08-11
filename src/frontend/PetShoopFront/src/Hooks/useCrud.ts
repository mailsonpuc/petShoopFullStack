import { useState, useEffect, useCallback } from "react";

interface UseCrudOptions<T> {
  fetchFn: () => Promise<T[]>;
  createFn: (data: Omit<T, "id">) => Promise<T>;
  updateFn: (id: string, data: Omit<T, "id">) => Promise<T>;
  deleteFn: (id: string) => Promise<void>;
}

export function useCrud<T extends { id: string }>(options: UseCrudOptions<T>) {
  const [items, setItems] = useState<T[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

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

  const createItem = async (data: Omit<T, "id">) => {
    const newItem = await options.createFn(data);
    setItems((prev) => [...prev, newItem]);
    return newItem;
  };

  const updateItem = async (id: string, data: Omit<T, "id">) => {
    const updated = await options.updateFn(id, data);
    setItems((prev) => prev.map((item) => (item.id === id ? updated : item)));
    return updated;
  };

  const deleteItem = async (id: string) => {
    await options.deleteFn(id);
    setItems((prev) => prev.filter((item) => item.id !== id));
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
