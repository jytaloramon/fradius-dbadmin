<template>
  <div class="w-full flex flex-col items-center text-sm">
    <table class="w-full table-fixed">
      <thead class="text-left text-gray-600">
        <tr class="border-b-2">
          <slot name="table-headcol"></slot>
        </tr>
      </thead>

      <tbody>
        <slot name="table-bodyrow"></slot>
      </tbody>

      <slot name="table-foot"></slot>
    </table>

    <div v-if="pagination!.max > 1" class="mt-6 py-2 flex border-2 border-gray-500 rounded-lg text-xs">
      <button-pagination-pass-table :button-desc="{ label: '<< Anterior', disable: pagination!.actual <= 1 }"
        class="border-r" />

      <ul class="flex">
        <li v-for="(p, k) in pagination?.max" :key="k" class="px-2 py-1 border-r last:border-r-0 border-gray-400">
          <a v-if="p === pagination?.actual" class="p-2 cursor-pointer border-b border-gray-400">{{ p }}</a>
          <a v-else class="p-2 cursor-pointer">{{ p }}</a>
        </li>
      </ul>

      <button-pagination-pass-table :button-desc="{ label: 'Próxima >>', disable: pagination!.actual >= pagination!.max }"
        class="border-l" />
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, type PropType } from 'vue';

import ButtonPaginationPassTable from './ButtonPaginationPassTable.vue';

interface IPagination {
  actual: number;
  max: number;
}

export default defineComponent({
  name: 'AppTable',

  props: {
    pagination: {
      type: Object as PropType<IPagination>,
      required: false
    }
  },

  components: { ButtonPaginationPassTable },
});
</script>