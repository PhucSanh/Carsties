'use client';
import { FaSearch } from 'react-icons/fa'
import { useSearchStore } from '../hooks/useSearchStore';
import { ChangeEvent, useEffect, useState } from 'react';

export default function Search() {
    const setParams = useSearchStore((state) => state.setParams);
    const searchQuery = useSearchStore((state) => state.searchQuery);
    const [value, setValue] = useState('');
    const handleChange = (e: ChangeEvent<HTMLInputElement>) => {
        setValue(e.target.value);
    }
    useEffect(() => {
        if (searchQuery === '') {
            setValue('');
        }
    }, [searchQuery]);
    const handleSearch = () => {
        setParams({ searchQuery: value });
    }
    return (
        <div className='flex w-[50%] items-center border-2 border-gray-300 rounded-full py-1 shadow-2xl'>
            <input type="text" placeholder='Search by cars make model'
                onChange={handleChange}
                onKeyDown={(e) => {
                    if (e.key === "Enter") {
                        handleSearch();
                    }
                }}
                value={value}
                className="flex-grow pl-5 text-gray-700 border-transparent focus:outline-none focus:ring-0" />
            <button onClick={handleSearch} className='flex items-center justify-center'>
                <FaSearch size={34} className='bg-red-400 text-white rounded-full p-2 mx-2' />
            </button>
        </div >
    )
}
