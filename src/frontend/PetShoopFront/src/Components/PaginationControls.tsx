import { FaChevronLeft, FaChevronRight } from "react-icons/fa";
import type { Dispatch, SetStateAction } from "react";

interface PaginationControlsProps {
  label: string;
  pageNumber: number;
  pageSize: number;
  totalCount: number;
  totalPages: number;
  setPageNumber: Dispatch<SetStateAction<number>>;
}

export function PaginationControls({
  label,
  pageNumber,
  pageSize,
  totalCount,
  totalPages,
  setPageNumber,
}: PaginationControlsProps) {
  const startItem = totalCount > 0 ? (pageNumber - 1) * pageSize + 1 : 0;
  const endItem = Math.min(pageNumber * pageSize, totalCount);

  return (
    <div className="flex items-center justify-between">
      <div className="text-sm text-slate-400">
        Mostrando {startItem} a {endItem} de {totalCount} {label}
      </div>
      <div className="flex items-center gap-2">
        <button
          onClick={() => setPageNumber((p) => Math.max(1, p - 1))}
          disabled={pageNumber === 1}
          className="flex items-center gap-1 rounded-lg border border-slate-700 px-3 py-2 text-sm font-medium text-slate-300 hover:bg-slate-800 disabled:cursor-not-allowed disabled:opacity-50"
        >
          <FaChevronLeft className="h-4 w-4" />
          Voltar
        </button>
        <span className="flex items-center px-3 text-sm text-slate-400">
          Página {pageNumber} de {totalPages}
        </span>
        <button
          onClick={() => setPageNumber((p) => Math.min(totalPages, p + 1))}
          disabled={pageNumber === totalPages || totalPages === 0}
          className="flex items-center gap-1 rounded-lg border border-slate-700 px-3 py-2 text-sm font-medium text-slate-300 hover:bg-slate-800 disabled:cursor-not-allowed disabled:opacity-50"
        >
          Avançar
          <FaChevronRight className="h-4 w-4" />
        </button>
      </div>
    </div>
  );
}
