'use client'
import { AiOutlineCar } from 'react-icons/ai'
import { useSearchStore } from '../hooks/useSearchStore'
import { usePathname, useRouter } from 'next/navigation';

export default function Logo() {
    const router = useRouter();
    const pathname = usePathname();
    const handleReset = () => {
        if (pathname !== '/') {
            router.push('/');
        }
        resetParams();

    }
    const resetParams = useSearchStore((state) => state.reset);
    return (
        <div onClick={handleReset} className="flex cursor-pointer items-center gap-2 text-3xl font-semibold text-red-500">
            <AiOutlineCar size={34} />
            <div>Carsties Auction</div>
        </div>
    )
}
