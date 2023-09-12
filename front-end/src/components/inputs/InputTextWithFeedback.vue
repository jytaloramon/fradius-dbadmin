<template>
  <app-field :description="description">
    <div class="flex justify-between">
      <div class="w-[95%] flex pr-2">
        <div class="h-6 pt-0.5 ml-1.5 after:absolute after:-mt-1.5 after:mx-3 after:text-lg after:content-['|']">
          <font-awesome-icon :icon="description.icon.icon" :rotation="description.icon.rotation" class="mt-1" />
        </div>

        <input type="text" class="w-full ml-7 text-sm placeholder:italic focus:outline-none" :value="description.value"
          :placeholder="description.placeholder">
      </div>

      <span v-if="description.isOk"
        class="w-5 h-5 mt-1 text-sm font-bold text-center text-white bg-green-600 rounded-full">
        <font-awesome-icon icon="fa-solid fa-check" size="sm" class="mt-0.5" />
      </span>

      <span v-else-if="description.isOk === false"
        class="w-5 h-5 mt-1 text-sm font-bold text-center text-white bg-red-500 rounded-full">!</span>
    </div>
  </app-field>
</template>

<script lang="ts">
import { defineComponent, type PropType } from 'vue';
import AppField, { type IAppField } from './AppField.vue';

interface IInputTextWithFeedback extends IAppField {
  value: string;
  placeholder: string;
  icon: {
    icon: string;
    rotation?: string;
  }
  isOk?: boolean;
}

export default defineComponent({
  name: 'InputTextWithFeedback',

  components: { AppField },

  props: {
    description: {
      type: Object as PropType<IInputTextWithFeedback>,
      required: true,
    }
  }
});
</script>