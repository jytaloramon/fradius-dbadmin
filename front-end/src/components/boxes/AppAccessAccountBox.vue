<template>
  <div class="w-[420px] py-8 px-6 m-auto text-gray-700 bg-white rounded-md shadow-md">
    <div class="text-center">
      <h1 class="text-base font-bold">{{ header.title }}</h1>
      <h2 class="mt-2 text-base">{{ header.message }}</h2>
    </div>

    <h3
      class="py-2 mt-2 text-sm text-center before:float-left before:w-[130px] before:mt-3 before:border-dashed before:border-[1px]  after:float-right after:w-[130px] after:mt-3 after:border-dashed after:border-[1px]">
      {{ header.subtitle }}
    </h3>

    <slot></slot>
  </div>
</template>

<script lang="ts">
import { defineComponent, type PropType } from 'vue';


interface IAccountBox {
  title: string;
  message: string;
  subtitle: string;
}


export default defineComponent({
  name: 'AppAccessAccountBox',

  props: {
    header: {
      type: Object as PropType<IAccountBox>,
      required: true
    }
  }
});
</script>
