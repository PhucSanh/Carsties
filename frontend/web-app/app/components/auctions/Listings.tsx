'use client'
import { useEffect, useState } from "react";
import AppPagination from "../AppPagination";
import AuctionCard from "./AuctionCard";
import { Auction } from "@/app/types/Auction";
import { getData } from "@/app/actions/AuctionActions";
import PageSize from "../PageSize";
import { PageResult } from "@/app/types/PageResult";
import { useSearchStore } from "@/app/hooks/useSearchStore";
import { useShallow } from "zustand/shallow";
import queryString from "query-string";
import Filter from "../Filter";
import AuctionNotFound from "./AuctionNotFound";



export default function Listings() {
    const [data, setData] = useState<PageResult<Auction>>();
    const params = useSearchStore(useShallow((state) => ({
        pageNumber: state.pageNumber,
        pageSize: state.pageSize,
        searchQuery: state.searchQuery,
        sortBy: state.sortBy,
        sortDirection: state.sortDirection
    })));
    const setParams = useSearchStore((state) => state.setParams);
    const setPageNumber = (pageNumber: number) => {
        setParams({ pageNumber });
    }
    const url = queryString.stringifyUrl({
        url: '',
        query: params
    }, { skipEmptyString: true });
    useEffect(() => {
        getData(url).then((data) => {
            setData(data);

        });
    }, [url]);
    if (!data) {
        return <div>Loading...</div>
    }

    if (!data.data || data.data.length === 0) {
        return <AuctionNotFound />;
    }
    return (
        <div className="flex flex-col gap-4 min-h-[calc(100vh-120px)]">
            <Filter />
            <div className="grid grid-cols-4 gap-6">{data?.data?.map((auction: Auction) => (
                <AuctionCard key={auction.id} auction={auction} />
            ))}</div>
            <div className=" flex justify-end mt-auto items-center gap-2 mb-2 ">
                <PageSize />

                <AppPagination setPageNumber={setPageNumber} currentPage={params.pageNumber} pageCount={data?.pageCount} />

            </div >


        </div>
    )
}


