export type deleteResponse = {
  message: string;
};

export interface PagedResponse<T> {
  page: number;
  limit: number;
  total: number;
  items: T[];
}

export interface PagedRequest {
  page: number;
  limit: number;
}
