<template>
  <div class="timeline-container">
    <div class="timeline">
      <div
        v-for="(item, idx) in items"
        :key="idx"
        v-click
        class="timeline-item"
      >
        <div class="timeline-content">
          <h3>{{ item.title }}</h3>
          <p>{{ item.description }}</p>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
const props = defineProps({
  items: {
    type: Array,
    default: () => [
      { title: '2018', description: 'Blazor announced' },
      { title: '2019', description: 'Blazor Server released' },
      { title: '2020', description: 'Blazor WebAssembly released' },
      { title: '2023', description: 'Blazor United preview' },
    ]
  }
})
</script>

<style scoped>
.timeline-container {
  display: flex;
  align-items: center;
  justify-content: center;
  gap: 1.5rem;
  margin: 2rem 0;
}
.timeline {
  display: flex;
  gap: 2rem;
  align-items: center;
  min-width: 400px;
}
.timeline-item {
  background: #f3f3f3;
  border-radius: 8px;
  padding: 1rem 1.5rem;
  min-width: 120px;
  text-align: center;
  box-shadow: 0 2px 8px #0001;
  border: 2px solid transparent;
  user-select: none;
  opacity: 0.5;
  transform: scale(1);
  transition: all 0.3s cubic-bezier(.4,2,.6,1);
}
.timeline-item[data-v-click] {
  opacity: 1;
}
.timeline-item[data-v-click]:last-of-type {
  background: #fff;
  border: 2px solid #0078d7;
  box-shadow: 0 4px 16px #0078d733;
  transform: scale(1.2);
  opacity: 1;
  z-index: 2;
}
.timeline-content h3 {
  margin: 0 0 0.5rem 0;
  font-size: 1.2rem;
}
.timeline-content p {
  margin: 0;
  font-size: 1rem;
  color: #444;
}
</style> 