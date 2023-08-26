<template>
  <app-table-head-item>
    <div class="flex">
      <slot></slot>
    </div>
  </app-table-head-item>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import AppTableHeadItem from './AppTableHeadItem.vue';


export default defineComponent({
  name: 'TableHeadItemWithSort',

  components: { AppTableHeadItem }
});
</script>