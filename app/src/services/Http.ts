import axios, { type AxiosRequestConfig, type AxiosResponse } from 'axios'

export class Http {
  public static async request<TResult = any, TData = any>(
    url: string,
    options: AxiosRequestConfig<TData>,
  ) {
    try {
      return await axios.request<TResult, AxiosResponse<TResult, TData>, TData>({ url, ...options })
    } catch (ex) {
      console.error(ex) // eslint-disable-line no-console
      throw ex
    }
  }

  public static async get<TResult = any, TData = any>(
    url: string,
    options: AxiosRequestConfig<TData> = {},
  ) {
    return await this.request<TResult, TData>(url, { method: 'get', ...options })
  }

  public static async post<TResult = any, TData = any>(
    url: string,
    options: AxiosRequestConfig<TData> = {},
  ) {
    return await this.request<TResult, TData>(url, { method: 'post', ...options })
  }

  public static async delete<TResult = any, TData = any>(
    url: string,
    options: AxiosRequestConfig<TData> = {},
  ) {
    return await this.request<TResult, TData>(url, { method: 'delete', ...options })
  }
}
