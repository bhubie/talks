<template>
  <div class="slidev-layout my-header-center-layout header-center h-full flex flex-col">
    <header class="mb-8">
      <slot name="title">
        <h1 v-if="title" v-html="title" />
      </slot>
    </header>
    <main class="flex-1 flex items-center justify-center">
      <slot />
    </main>
  </div>
</template>

<script setup lang="ts">
const { title } = defineProps<{ title?: string }>()
</script>

<style>
.my-header-center-layout {
  width: 100%;
  height: 100%;
  display: flex;
  flex-direction: column;
}

.my-header-center-layout main {
  flex: 1;
  display: flex;
  align-items: center;
  justify-content: center;
}
</style>