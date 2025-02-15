<template>
  <div class="d-flex justify-content-center align-items-center">
    <router-link :to="{ name: 'products' }">
      <b-button variant="primary">Open products</b-button>
    </router-link>
  </div>
</template>

<script setup lang="ts"></script>

<style scoped lang="scss">
.align-items-center {
  height: 100vh;
}
</style>
