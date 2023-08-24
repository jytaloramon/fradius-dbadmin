<template>
  <th class="py-2 px-3">
    <slot></slot>
  </th>
</template>

<script lang="ts">
import { defineComponent } from 'vue';


export default defineComponent({
  name: 'AppTableHeadItem'
});
</script>