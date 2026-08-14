import { useState, useEffect, useCallback } from "react";
import type { PaginationMetadata, PagedResponse } from "../Types";

interface UseCrudOptions<T extends Record<string, any>, TIdKey extends keyof T = "id", TCreate = Omit<T, TIdKey>> {
  fetchFn: () => Promise<T[]>;
  fetchPagedFn?: (pageNumber: number, pageSize: number) => Promise<PagedResponse<T>>;
  createFn: (data: TCreate) => Promise<T>;
  updateFn: (id: string, data: TCreate) => Promise<T>;
  deleteFn: (id: string) => Promise<void>;
  idKey?: TIdKey;
  pageSize?: number;
}

export function useCrud<T extends Record<string, any>, TIdKey extends keyof T = "id", TCreate = Omit<T, TIdKey>>(options: UseCrudOptions<T, TIdKey, TCreate>) {
  const [items, setItems] = useState<T[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [deleteError, setDeleteError] = useState<string | null>(null);
  const [pageNumber, setPageNumber] = useState(1);
  const [pagination, setPagination] = useState<PaginationMetadata>({
    totalCount: 0,
    pageSize: options.pageSize ?? 10,
    currentPage: 1,
    totalPages: 0,
    hasNextPage: false,
    hasPreviousPage: false,
  });

  const idField = options.idKey ?? "id";
  const pageSize = options.pageSize ?? 10;

  const clearError = useCallback(() => setError(null), []);

  const loadItems = useCallback(async () => {
    try {
      setIsLoading(true);
      setError(null);
      setDeleteError(null);
      if (options.fetchPagedFn) {
        const response = await options.fetchPagedFn(pageNumber, pageSize);
        setItems(response.data);
        setPagination(response.pagination);
      } else {
        const data = await options.fetchFn();
        setItems(data);
        setPagination({
          totalCount: data.length,
          pageSize: data.length,
          currentPage: 1,
          totalPages: data.length > 0 ? 1 : 0,
          hasNextPage: false,
          hasPreviousPage: false,
        });
      }
    } catch (err) {
      setError(err instanceof Error ? err.message : "Erro ao carregar dados");
    } finally {
      setIsLoading(false);
    }
  }, [options.fetchFn, options.fetchPagedFn, pageNumber, pageSize]);

  const createItem = async (data: TCreate) => {
    try {
      const newItem = await options.createFn(data);
      if (options.fetchPagedFn) {
        await loadItems();
      } else {
        setItems((prev) => [...prev, newItem]);
      }
      return newItem;
    } catch (err) {
      setError(err instanceof Error ? err.message : "Erro ao criar item");
      throw err;
    }
  };

  const updateItem = async (id: string, data: TCreate) => {
    try {
      const updated = await options.updateFn(id, data);
      if (options.fetchPagedFn) {
        await loadItems();
      } else {
        setItems((prev) => prev.map((item) => (item[idField] === id ? updated : item)));
      }
      return updated;
    } catch (err) {
      setError(err instanceof Error ? err.message : "Erro ao atualizar item");
      throw err;
    }
  };

  const deleteItem = async (id: string) => {
    try {
      setDeleteError(null);
      await options.deleteFn(id);
      if (options.fetchPagedFn) {
        await loadItems();
      } else {
        setItems((prev) => prev.filter((item) => item[idField] !== id));
      }
    } catch (err) {
      setDeleteError(err instanceof Error ? err.message : "Erro ao excluir item");
      throw err;
    }
  };

  useEffect(() => {
    loadItems();
  }, [loadItems]);

  return {
    items,
    isLoading,
    error,
    deleteError,
    pageNumber,
    pageSize,
    pagination,
    setPageNumber,
    reload: loadItems,
    createItem,
    updateItem,
    deleteItem,
    clearError,
  };
}
