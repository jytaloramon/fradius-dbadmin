<template>
  <th>
    <div class="flex">
      <slot></slot>

      <div v-if="description.isVisible" class="ml-2">
        <a class="cursor-pointer">
          <font-awesome-icon :class="getIconClass" icon="fa-solid fa-caret-up" :rotation="getIconRotation" />
        </a>
      </div>
    </div>
  </th>
</template>
  
<script lang="ts">
import { defineComponent, type PropType } from 'vue';

interface ITableHItemWithSort {
  isVisible: boolean;
  isDesc?: boolean;
}

export default defineComponent({
  name: 'TableHItemWithSort',

  props: {
    description: {
      type: Object as PropType<ITableHItemWithSort>,
      required: true,
    }
  },

  computed: {
    getIconClass(): string {
      const textColor = this.description.isDesc || this.description.isDesc ? 'text-red-500' : 'text-green-600';

      return `text-sm ${textColor}`;
    },

    getIconRotation(): string {
      return this.description.isDesc ? '0' : '180';
    }
  }
});
</script>