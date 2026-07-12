import React from 'react'

export default function AuctionNotFound() {
    return (
        <div className='flex flex-col items-center justify-center gap-4 h-[calc(100vh-120px)]'>
            <div className="flex justify-center items-center text-gray-500 text-xl">Auctions not found Please try again later.</div>
            <button className="bg-red-500 w-50 text-white px-4 py-2 rounded-lg hover:bg-red-600" onClick={() => window.location.href = '/'}>Go to Home</button>
        </div>
    )
}
