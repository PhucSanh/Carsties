import React from 'react'
import { useSearchStore } from '../hooks/useSearchStore';

const pageSizes = [8, 16, 32, 64];
export default function PageSize() {
    const pageSize = useSearchStore((state) => state.pageSize);
    const setParams = useSearchStore((state) => state.setParams);
    return (
        <select className="bg-gray-200 p-2 text-gray-700 rounded-lg" value={pageSize} onChange={(e) => {
            setParams({ pageSize: parseInt(e.target.value) });
        }}>
            {pageSizes.map((size) => (
                <option key={size} value={size}>
                    {size}
                </option>
            ))}
        </select>
    )
}
