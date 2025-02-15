import { Http } from './Http'
import type { Product, SaveProductInput } from '@/models'

const apiOrigin = 'https://localhost:7178'

export class ProductService {
  public static async getProducts() {
    const response = await Http.get<Product[]>(`${apiOrigin}/products`)
    return response.data
  }

  public static async getProduct(id: number) {
    const response = await Http.get<Product>(`${apiOrigin}/products/${id}`)
    return response.data
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
