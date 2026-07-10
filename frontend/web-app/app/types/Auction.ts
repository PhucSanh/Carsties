export type Auction = {
    id: string;
    reservePrice?: number;
    seller: string;
    winner?: string | null;
    soldAmount?: number;
    make: string;
    currentHighBid?: number;
    createdAt: string;
    updatedAt: string;
    auctionEnd: string;
    status: 'Live' | 'Completed' | 'ReserveNotMet' | string;
    model: string;
    year: number;
    color: string;
    mileage: number;
    imageUrl: string;
};