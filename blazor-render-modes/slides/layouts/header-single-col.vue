<template>
  <div class="slidev-layout header-single-col h-full">
    <!-- <header class="mb-4">
      <slot name="title">
        <h1 v-if="title" v-html="title" />
      </slot>
    </header> -->
    <slot name="default" />
    <div class="flex justify-center">
      <slot name="content" />
    </div>

  </div>
</template>

<script setup lang="ts">
const { title } = defineProps<{ title?: string }>()
</script>

<style>
.header-single-col {
  display: grid;
  grid-template-rows: 40% 1fr;
}


.header-single-col h1 {
  margin-bottom: 0;
}
</style> 