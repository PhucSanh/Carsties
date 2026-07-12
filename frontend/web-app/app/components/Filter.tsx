'use client'
import { ButtonGroup } from "flowbite-react";
import { useSearchStore } from "../hooks/useSearchStore";

const orderButtons = [
    { label: 'Alphabetical', value: 'Make', icon: 'AiOutlineSortAlphabetically' },
    { label: 'End Date', value: 'AuctionEnd', icon: 'AiOutlineClockCircle' },
    { label: 'Recently Added', value: 'createAt', icon: 'BsFillStopwatchFill' }
];

export default function Filter() {
    const setParams = useSearchStore((state) => state.setParams);
    const sortBy = useSearchStore((state) => state.sortBy);
    const sortDirection = useSearchStore((state) => state.sortDirection);
    return (
        <div className="flex">
            <ButtonGroup outline className="flex gap-2">
                {orderButtons.map((button) => (
                    <button
                        key={button.value}
                        className={`flex items-center gap-2 bg-gray-200 p-2 rounded-lg text-gray-700 hover:bg-gray-300 ${sortBy === button.value ? 'bg-red-500 text-white' : ''}`}
                        onClick={() => {
                            setParams({ sortBy: button.value, sortDirection: sortDirection === 'asc' ? 'desc' : 'asc' });
                        }}
                    >
                        {sortDirection === 'asc' && sortBy === button.value ? <span>&#9650;</span> : sortDirection === 'desc' && sortBy === button.value ? <span>&#9660;</span> : null}
                        {button.label}
                    </button>
                ))}
            </ButtonGroup>
        </div>
    )
}