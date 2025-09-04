import { Result } from './result';

export interface PagedResult<TData> extends Result<TData[]> {
  totalItems: number;
  totalPages: number;
}
