<template>
  <button class="flex w-full p-3 justify-center text-base text-white bg-green-600 rounded-lg">
    <span>
      <font-awesome-icon :icon="buttonDesc.icon.icon" :size="buttonDesc.icon.size" :rotate="buttonDesc.icon.rotate"
        :color="buttonDesc.icon.color" />
    </span>

    <span class="ml-3">{{ buttonDesc.label }}</span>

    <slot></slot>
  </button>
</template>

<script lang="ts">
import { defineComponent, type PropType } from 'vue';


export interface IAppButton {
  label: string;
  icon: {
    icon: string;
    size?: string;
    rotate?: string;
    color?: string;
  };
}


export default defineComponent({
  name: 'AppButton',

  props: {
    buttonDesc: {
      type: Object as PropType<IAppButton>,
      required: true
    }
  }
});
</script>
