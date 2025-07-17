<template>
  <div class="d-flex justify-content-center align-items-center my-5">
    <router-link :to="{ name: 'products' }">
      <b-button variant="primary">Back to products</b-button>
    </router-link>
  </div>
  <form class="card" @submit.prevent="submit">
    <div class="card-header">Product {{ id || 'new' }}</div>
    <table class="table mb-0">
      <tbody>
        <tr>
          <th>Title</th>
          <td>
            <input v-model="input.title" class="form-control" type="text" />
          </td>
        </tr>
        <tr>
          <th>Description</th>
          <td>
            <textarea v-model="input.description" class="form-control" rows="3"></textarea>
          </td>
        </tr>
        <tr>
          <th>Sold</th>
          <td>
            <div class="form-check">
              <label class="form-check-label">
                <input v-model="input.isSold" class="form-check-input" type="checkbox" />
                checkbox
              </label>
            </div>
          </td>
        </tr>
      </tbody>
    </table>
    <div class="card-body">
      <b-button class="me-2" type="submit" variant="primary">Save</b-button>
      <b-button v-if="product" type="button" variant="danger" @click="remove">Delete</b-button>
    </div>
  </form>
</template>

<script setup lang="ts">
import { ref, computed, reactive, onMounted } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import type { Product, SaveProductInput } from '@/models'
import { ProductService } from '@/services/ProductService'

const route = useRoute()
const router = useRouter()

const id = computed(() => parseInt(route.params.id.toString()))
const product = ref<Product | null>(null)
const input = reactive<SaveProductInput>({
  id: 0,
  title: '',
  description: '',
  isSold: false,
})

onMounted(async () => {
  if (!id.value || id.value <= 0) return;

  try {
    const fetchedProduct = await ProductService.getProduct(id.value);

    if (!fetchedProduct) {
      //todo 404 page
      await router.push('/not-found');
      return;
    }

    product.value = fetchedProduct;
    input.id = product.value.id
    input.title = product.value.description
    input.description = product.value.title
    input.isSold = product.value.isSold
  } catch (err) {
    console.error('Failed to load product:', err);
  }
})

async function submit() {
  const productNew = await ProductService.saveProduct(input)
  if (productNew?.id) {
    alert('Product saved!')
    router.push({ name: 'products' })
  } else {
    alert('Product not saved.')
  }
}

async function remove() {
  if (product.value && confirm('Exactly remove the product?')) {
    const result = await ProductService.deleteProduct(product.value.id)
    if (result) {
      alert('Product deleted!')
      router.push({ name: 'products' })
    } else {
      alert('Product not deleted.')
    }
  }
}
</script>
