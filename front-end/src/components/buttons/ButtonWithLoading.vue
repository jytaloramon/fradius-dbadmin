<template>
  <app-button :button-desc="buttonWithLoadingDesc">
    <circle-spin-animation v-if="buttonWithLoadingDesc.isLoading" />
  </app-button>
</template>

<script lang="ts">
import { defineComponent, type PropType } from 'vue';
import AppButton, { type IAppButton } from './AppButton.vue';
import CircleSpinAnimation from '../animations/CircleSpinAnimation.vue'


interface IButtonWithLoading extends IAppButton {
  isLoading?: boolean
}


export default defineComponent({
  name: 'ButtonWithLoading',

  components: { AppButton, CircleSpinAnimation },

  props: {
    buttonWithLoadingDesc: {
      type: Object as PropType<IButtonWithLoading>,
      required: true,
    }
  }
});
</script>


