import { Auction } from "@/app/types/Auction";
import Image from "next/image";
import CountdownTimer from "./CountdownTimer";
import CarImage from "./CarImage";
type Props = {
    auction: Auction;

};

export default function AuctionCard({ auction }: Props) {
    return (
        <a href="#" className="cursor-pointer">
            <div className=" relative w-full bg-gray-200 aspect-16/10 rounded-lg overflow-hidden">
                <CarImage image={auction.imageUrl} />
                <div className="absolute top-2 right-2">
                    <CountdownTimer auctionEndDate={auction.auctionEnd} />
                </div>
            </div>
            <div className="flex justify-between items-center mt-4">
                <h3 className="text-gray-700">{auction.make} {auction.model}</h3>
                <p className="text-sm font-semibold">{auction.year}</p>
            </div>
        </a >
    )
}
