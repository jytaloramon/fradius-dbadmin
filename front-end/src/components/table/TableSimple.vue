<template>
  <table class="w-full table-auto text-sm text-left">
    <thead class="text-gray-600">
      <tr class="border-y">
        <slot name="head"></slot>
      </tr>
    </thead>

    <tbody>
      <slot name="body"></slot>
    </tbody>

    <slot name="footer"></slot>
  </table>
</template>

<script lang="ts">
import { defineComponent } from 'vue';

export default defineComponent({
  name: 'TableSimple',
});
</script>