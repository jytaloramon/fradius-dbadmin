<template>
  <app-input-field :field-desc="{ label: inputDesc.label, error: inputDesc.data.error }">
    <div class="flex">
      <div class="w-12 ml-2 after:absolute after:-mt-1 after:mx-3 after:text-lg after:content-['|']">
        <font-awesome-icon :icon="inputDesc.icon" size="sm" />
      </div>

      <input :type="inputDesc.type" class="w-full placeholder:italic focus:outline-none"
        :placeholder="inputDesc.placeholder" :value="inputDesc.data.value"
        @input="e => $emit('inputChanged', e.target?.value)" />
    </div>
  </app-input-field>
</template>

<script lang="ts">
import { defineComponent, type PropType } from 'vue';
import AppInputField from './AppInputField.vue';


interface IInputTextWithLeftIcon {
  type?: string,
  label: string;
  placeholder: string;
  icon: string;
  data: {
    value: string;
    error?: string;
  }
}


export default defineComponent({
  name: 'InputTextWithLeftIcon',

  components: {
    AppInputField,
  },

  props: {
    inputDesc: {
      type: Object as PropType<IInputTextWithLeftIcon>,
      required: true
    }
  }
});
</script>
