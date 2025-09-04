export interface Result<TData = unknown> {
  isSuccess: boolean;
  data?: TData | null;
  error?: string;
}
