<template>
  <th>
    <div class="flex">
      <slot></slot>

      <div v-if="isV" class="ml-2">
        <font-awesome-icon :class="getIconClass" icon="fa-solid fa-caret-up" :rotation="getIconRotation" />
      </div>
    </div>
  </th>
</template>

<script lang="ts">
import { defineComponent } from 'vue';

interface ITableHItemWithSort {
  isVisible: boolean;
  isDesc?: boolean;
}

export default defineComponent({
  name: 'TableHItemWithSort',

  data() {
    return {
      isV: true,
      isDesc: false
    };
  },

  computed: {
    getIconClass(): string {
      const textColor = this.isDesc || this.isDesc ? 'text-red-500' : 'text-green-600';

      return `text-sm ${textColor}`;
    },

    getIconRotation(): string {
      return this.isDesc ? '0' : '180';
    }
  }
});
</script>