export interface Product {
  id: number
  createdAt: string
  updatedAt: string
  deletedAt: string | null
  title: string
  description: string
  isSold: boolean
}

export interface SaveProductInput {
  id: number
  title: string
  description: string
  isSold: boolean
}
