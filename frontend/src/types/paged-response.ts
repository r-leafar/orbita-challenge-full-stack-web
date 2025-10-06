export type PagedResponse<T> = {
    Data: T[];
    PageNumber: number;
    PageSize: number;
    TotalPages: number;
    TotalRecords: number;
}