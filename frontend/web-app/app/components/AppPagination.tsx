'use client'
import { Pagination } from 'flowbite-react';
import { useState } from 'react'

type Props = {
    currentPage: number;
    pageCount: number;
    setPageNumber: (page: number) => void;

}
export default function AppPagination({ currentPage, pageCount, setPageNumber }: Props) {
    if (!currentPage || !pageCount || !setPageNumber) {
        return null;
    }
    return (

        <Pagination
            currentPage={currentPage}
            totalPages={pageCount}
            onPageChange={(page) => {
                setPageNumber(page);
            }}
            layout='pagination'
            showIcons={true}
            className='text-blue-500 mb-2'
        />

    )
}
