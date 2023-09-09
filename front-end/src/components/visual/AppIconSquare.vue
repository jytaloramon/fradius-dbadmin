<template>
  <div class="flex flex-col w-10 h-10 justify-center border rounded-md">
    <font-awesome-icon :icon="description.icon" :rotation="description.rotation" />
  </div>
</template>
  
<script lang="ts">
import { defineComponent, type PropType } from 'vue';


export interface IAppIconSquare {
  icon: string;
  rotation?: string;
}


export default defineComponent({
  name: 'AppIconSquare',

  props: {
    description: {
      type: Object as PropType<IAppIconSquare>,
      required: true
    }
  }
});
</script>