import { AiOutlineCar } from "react-icons/ai";

export default function NavBar() {
    return (
        <header className="sticky top-0 z-100 flex justify-between
         bg-white p-5 items-center h-20 text-gray-800 shadow-md">
            <div className="flex items-center gap-2 text-3xl font-semibold text-red-500">
                <AiOutlineCar size={34} />
                <div>Carsties Auction</div>
            </div>
            <div>Navigation</div>
            <div>Profile</div>
        </header>
    )
}
