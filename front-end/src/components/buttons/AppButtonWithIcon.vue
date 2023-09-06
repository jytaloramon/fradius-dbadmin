<template>
  <app-button :description="description">
    <font-awesome-icon :icon="description.icon.icon" :rotation="description.icon.rotate" class="mr-3" />
  </app-button>
</template>

<script lang="ts">
import { defineComponent, type PropType } from 'vue';
import AppButton, { type IAppButton } from './AppButton.vue';


export interface IAppButtonWithIcon extends IAppButton {
  icon: {
    icon: string;
    rotate?: string;
  };
}


export default defineComponent({
  name: 'AppButtonWithIcon',

  props: {
    description: {
      type: Object as PropType<IAppButtonWithIcon>,
      required: true
    }
  },

  components: { AppButton },
});
</script>