<template>
  <a class="cursor-pointer text-sm">
    <div class="flex flex-col w-10 h-10 justify-center border rounded-md border-red-500">
      <font-awesome-icon :icon="description.design.icon" :size="description.design.size" />
    </div>
  </a>

  <!--
  <div class="inline relative group text-sm">
    <a class="cursor-pointer relative">
      <font-awesome-icon :icon="description.design.icon" :size="description.design.size" :class="styleButton" />
    </a>
    
    <span :class="styleLabel">
      {{ description.label.value }}
    </span>
  </div>
  -->
</template>

<script lang="ts">
import { defineComponent, type PropType } from 'vue';


interface IAppButtonSquare {
  label: {
    value: string;
    style: string;
  }
  design: {
    icon: string;
    size: string;
    style: string;
  };
}


export default defineComponent({
  name: 'AppButtonSquare',

  props: {
    description: {
      type: Object as PropType<IAppButtonSquare>,
      required: true
    }
  },

  data() {
    return {
      styleLabel: `absolute hidden px-2 top-5 left-0 border rounded-md group-hover:inline ${this.description.label.style}`,
      styleButton: `border rounded-md ${this.description.design.style}`
    };
  },
});
</script>
