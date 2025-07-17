<template>
  <div class="d-flex gap-3 justify-content-center align-items-center my-5">
    <router-link :to="{ name: 'product', params: { id: 0 } }">
      <b-button variant="primary">Add product</b-button>
    </router-link>
    <router-link :to="{ name: 'home' }">
      <b-button variant="primary">Back to home</b-button>
    </router-link>

    <b-form-select
      v-model="soldFilter"
      :options="soldOptions"
      class="w-auto"
    />
  </div>
  <table class="table table-hover">
    <thead>
      <tr>
        <th>Id</th>
        <th>Title</th>
        <th>Description</th>
        <th>Sold</th>
      </tr>
    </thead>
    <tbody>
      <tr
        v-for="product in products"
        @click="router.push({ name: 'product', params: { id: product.id } })"
      >
        <td>{{ product.id }}</td>
        <td>{{ product.title }}</td>
        <td>{{ product.description }}</td>
        <td>{{ product.isSold }}</td>
      </tr>
    </tbody>
  </table>
</template>

<script setup lang="ts">
import { ref, onMounted, watch } from 'vue'
import { useRouter } from 'vue-router'
import type { Product } from '@/models'
import { ProductService } from '@/services/ProductService'

const router = useRouter()
const products = ref<Product[]>([])

const soldFilter = ref<boolean | null>(null)
const soldOptions = [
  { value: null, text: 'No matter' },
  { value: true, text: 'Sold' },
  { value: false, text: 'Not sold' }
]

const loadProducts = async () => {
  try {
    products.value = await ProductService.getProducts(soldFilter.value)
  } catch (error: any) {
    console.error(error)
    alert(error?.message ?? error)
  }
}

watch(soldFilter, loadProducts)
onMounted(loadProducts)

</script>

<style scoped lang="scss">
tbody tr {
  cursor: pointer;
}
</style>
