export type PageResult<T> = {
    data: T[];
    totalCount: number;
    pageCount: number;
};