<template>
  <app-input-field :field-desc="{ label: selectDesc.label, error: selectDesc.error }">
    <div class="flex">
      <div class="w-12 ml-2 after:absolute after:-mt-1 after:mx-3 after:text-lg after:content-['|']">
        <font-awesome-icon :icon="selectDesc.icon" size="sm" />
      </div>

      <select class="w-full bg-white focus:outline-none cursor-pointer" @change="e => chosenOption(e.target?.value)">
        <option value="" :disabled="isChanged">Selecione uma opção</option>
        <option v-for="(data, idx) in selectDesc.data" :key="idx" :value="data">{{ data }}</option>
      </select>
    </div>
  </app-input-field>
</template>

<script lang="ts">
import { defineComponent, type PropType } from 'vue';
import AppInputField from './AppInputField.vue';


interface ISelectWithLeftIcon {
  label: string;
  icon: string;
  data: string[]
  error?: string;
}


export default defineComponent({
  name: 'SelectWithLeftIcon',

  components: { AppInputField },

  props: {
    selectDesc: {
      type: Object as PropType<ISelectWithLeftIcon>,
      required: true
    }
  },

  data() {
    return {
      isChanged: false
    };
  },

  methods: {
    chosenOption(value: string): void {
      this.isChanged = true;
      console.log(value);
    }
  }
}); 
</script>