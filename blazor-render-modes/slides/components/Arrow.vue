<script setup lang="ts">
import { computed } from 'vue'
const props = defineProps({
  direction: {
    type: String,
    default: 'right',
    validator: (v: string) => ['left', 'right'].includes(v),
  },
  width: {
    type: [Number, String],
    default: 60,
  },
  height: {
    type: [Number, String],
    default: 24,
  },
  color: {
    type: String,
    default: '#888',
  },
  stroke: {
    type: [Number, String],
    default: 3,
  },
  label: {
    type: String,
    default: '',
  },
})

const isRight = computed(() => props.direction === 'right')
const numericWidth = computed(() => Number(props.width))
const numericHeight = computed(() => Number(props.height))
</script>

<template>
  <div class="flex flex-col items-center justify-center">
    <span v-if="label" class="text-sm text-gray-600 mb-1">{{ label }}</span>
    <svg :width="numericWidth" :height="numericHeight" :viewBox="`0 0 ${numericWidth} ${numericHeight}`" fill="none" xmlns="http://www.w3.org/2000/svg">
      <line
        :x1="isRight ? 2 : numericWidth - 2"
        :y1="numericHeight / 2"
        :x2="isRight ? numericWidth - 2 : 2"
        :y2="numericHeight / 2"
        :stroke="color"
        :stroke-width="stroke"
        :marker-end="isRight ? 'url(#arrowhead)' : undefined"
        :marker-start="!isRight ? 'url(#arrowhead-left)' : undefined"
      />
      <defs>
        <marker id="arrowhead" markerWidth="8" markerHeight="8" refX="8" refY="4" orient="auto">
          <polygon points="0 0, 8 4, 0 8" :fill="color"/>
        </marker>
        <marker id="arrowhead-left" markerWidth="8" markerHeight="8" refX="0" refY="4" orient="auto">
          <polygon points="8 0, 0 4, 8 8" :fill="color"/>
        </marker>
      </defs>
    </svg>
  </div>
</template> 