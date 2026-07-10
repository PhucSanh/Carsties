'use server'
import { Auction } from "../types/Auction";
import { PageResult } from "../types/PageResult";

export async function getData(queryString: string): Promise<PageResult<Auction>> {
    const res = await fetch(`http://localhost:6001/search${queryString}`);

    if (!res.ok) {
        throw new Error("Failed to fetch data");
    }
    return res.json();
}