<template>
  <div class="timeline-vertical-container">
    <div class="timeline-vertical">
      <div
        v-for="(item, idx) in items"
        :key="idx"
        v-click
        class="timeline-vertical-item"
      >
        <div class="timeline-marker">
          <span class="timeline-dot"></span>
          <span v-if="idx < items.length - 1" class="timeline-line"></span>
        </div>
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
      { title: '2017', description: 'Steve Sanderson Demos Blazor' },
      { title: '2018', description: 'Blazor announced' },
      { title: '2019', description: 'Blazor Server released' },
      { title: '2020', description: 'Blazor WebAssembly released' },
      { title: '2022', description: 'Blazor Hybrid released' },
      { title: '2023', description: 'Blazor Web App released with .NET 8' },
    ]
  }
})
</script>

<style scoped>
.timeline-vertical-container {
  display: flex;
  justify-content: center;
  margin: 2rem 0;
}
.timeline-vertical {
  display: flex;
  flex-direction: column;
  position: relative;
  gap: 0;
}
.timeline-vertical-item {
  display: flex;
  align-items: flex-start;
  position: relative;
  min-height: 60px;
  padding: 0.5rem 0 0.5rem 0.5rem;
  opacity: 0.5;
  transition: background 0.3s, opacity 0.3s;
}
.timeline-vertical-item[data-v-click] {
  opacity: 1;
}
.timeline-vertical-item[data-v-click]:last-of-type {
  background: #f0f8ff;
  border-radius: 8px;
  box-shadow: 0 4px 16px #0078d733;
}
.timeline-marker {
  display: flex;
  flex-direction: column;
  align-items: center;
  margin-right: 1.2rem;
  position: relative;
  min-width: 24px;
}
.timeline-dot {
  width: 16px;
  height: 16px;
  background: #bbb;
  border-radius: 50%;
  border: 3px solid #e0e0e0;
  z-index: 2;
  transition: all 0.3s cubic-bezier(.4,2,.6,1);
}
.timeline-vertical-item[data-v-click]:last-of-type .timeline-dot {
  background: #0078d7;
  border-color: #0078d7;
  width: 24px;
  height: 24px;
  box-shadow: 0 0 0 4px #e3f1ff;
}
.timeline-line {
  flex: 1 1 auto;
  width: 4px;
  background: #e0e0e0;
  margin-top: 2px;
  margin-bottom: -2px;
  min-height: 32px;
  z-index: 1;
}
.timeline-content {
  flex: 1;
  padding-bottom: 0.5rem;
}
.timeline-content h3 {
  margin: 0 0 0.25rem 0;
  font-size: 1.1rem;
  color: #222;
}
.timeline-content p {
  margin: 0;
  font-size: 1rem;
  color: #444;
}
</style> 