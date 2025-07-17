import { Http } from './Http'
import type { Product, SaveProductInput } from '@/models'
import axios from 'axios'

const apiOrigin = 'https://localhost:7178'

export class ProductService {
  public static async getProducts(isSold?: boolean | null) {
    const params = isSold !== null ? { isSold } : {}
    const response = await Http.get<Product[]>(`${apiOrigin}/products`, {params: params})
    return response.data
  }

  public static async getProduct(id: number) {
    try {
      const response = await Http.get<Product>(`${apiOrigin}/products/${id}`);

      return response.data;
    } catch (error) {
      if (axios.isAxiosError(error)) {
        if (error.response?.status === 404) {
          console.warn(`Product with ID ${id} not found`);
          return null;
        }

        //if else block, todo other errors
        console.error(`API Error: ${error.message}`, error.response?.data);
        throw new Error('Failed to fetch product');
      }
      console.error('Unexpected error:', error);
      throw new Error('An unexpected error occurred');
    }
  }

  public static async saveProduct(input: SaveProductInput) {
    const response = await Http.post<Product | null>(`${apiOrigin}/products`, { data: input })
    return response.data
  }

  public static async deleteProduct(id: number) {
    const response = await Http.delete<Boolean>(`${apiOrigin}/products/${id}`)
    return response.data
  }
}
