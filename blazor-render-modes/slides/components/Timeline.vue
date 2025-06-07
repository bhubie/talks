<template>
  <div class="flex justify-center my-8">
    <div class="grid w-full max-w-4xl gap-8 grid-cols-1 sm:grid-cols-2 md:grid-cols-3">
      <div
        v-for="(item, idx) in items"
        :key="idx"
        v-click
        class="block p-6 bg-white border border-gray-200 rounded-lg shadow-sm hover:bg-gray-100 transition duration-200 dark:bg-gray-800 dark:border-gray-700 dark:hover:bg-gray-700 opacity-50 data-[v-click]:opacity-100"
      >
        <h3 class="mb-2 text-2xl font-bold tracking-tight text-gray-900 dark:text-white">{{ item.title }}</h3>
        <p class="font-normal text-gray-700 dark:text-gray-400">{{ item.description }}</p>
      </div>
    </div>
  </div>
</template>

<script setup>
const props = defineProps({
  items: {
    type: Array,
    default: () => [
      { title: '2017', description: 'Steve Sanderson Demos Blazor' },
      { title: '2018', description: 'Microsoft announces experimental support for Blazor' },
      { title: '2019', description: 'Blazor Server released' },
      { title: '2020', description: 'Blazor WebAssembly released' },
      { title: '2022', description: 'Blazor Hybrid released' },
      { title: '2023', description: 'Blazor Web App released with .NET 8' },
    ]
  }
})
</script>

<!-- All card-related custom CSS removed; Tailwind classes are now used for all styling. --> 