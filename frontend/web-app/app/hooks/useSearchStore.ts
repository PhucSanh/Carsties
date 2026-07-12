import { create } from "zustand";

type State = {
    pageNumber: number;
    pageSize: number;
    pageCount: number;
    searchQuery: string;
    sortBy: string;
    sortDirection: 'asc' | 'desc';
}
type Actions = {
    setParams: (params: Partial<State>) => void;
    reset: () => void;
}
const initialState: State = {
    pageNumber: 1,
    pageSize: 8,
    pageCount: 1,
    searchQuery: '',
    sortBy: 'Make',
    sortDirection: 'asc'
}

export const useSearchStore = create<State & Actions>((set) => ({
    ...initialState,
    setParams: (newParams: Partial<State>) => {
        set((state) => {
            return {
                ...state,
                ...newParams,
                pageNumber: newParams.pageNumber ? newParams.pageNumber : 1
            }
        })
    },
    reset: () => { set(initialState) }
}))