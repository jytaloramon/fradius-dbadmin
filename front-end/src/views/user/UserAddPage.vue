<template>
  <div class="py-4 text-sm">
    <app-box class="w-1/2">
      <input-text-with-left-icon
        :input-desc="{ label: 'Usuário', data: { value: '' }, icon: 'fa-regular fa-user', placeholder: 'mary' }" />

      <app-table class="mt-8">
        <template v-slot:head>
          <table-head-item-with-sort class="py-2 px-3">Atributo</table-head-item-with-sort>
          <table-head-item-with-sort class="py-2 px-3">Operação</table-head-item-with-sort>
          <table-head-item-with-sort class="py-2 px-3">Valor</table-head-item-with-sort>
          <table-head-item-with-sort class="py-2 px-3"></table-head-item-with-sort>
        </template>

        <template v-slot:body>
          <tr v-for="i in 4" :key="i" class="border-b even:bg-white odd:bg-slate-100">
            <td class="py-3 px-3 fosem">Attr-Teste-{{ i }}</td>
            <td class="py-3 px-3">
              <select class="w-16 py-1.5 px-2 bg-gray-300 rounded-md text-center appearance-none">
                <option value="">=</option>
                <option value="">&gt;</option>
              </select>
            </td>
            <td class="py-3 px-3">
              <input type="text" class="py-1.5 px-3 bg-gray-300 rounded-md focus:outline-none" placeholder="Valor">
            </td>
            <td class="py-3 px-3">
              <button-square
                :description="{ label: 'Excluir', icon: 'fa-solid fa-trash', style: { borderColor: 'border-red-500' } }"
                class="text-red-500" />
            </td>
          </tr>
        </template>

        <template v-slot:foot>
          <tr class="text-white border-b bg-slate-900">
            <td class="py-3 px-3">+ Attributos</td>
            <td class="py-3 px-3" colspan="3">
              <select class="py-1.5 px-2 w-full bg-slate-700 rounded-md appearance-none">
                <option selected value="">Selecione</option>
                <option value="">Passowrd-MD5</option>
                <option value="">Passowrd-SHA256</option>
                <option value="">Passowrd-MD5</option>
                <option value="">Passowrd-SHA256</option>
                <option value="">Passowrd-MD5</option>
                <option value="">Passowrd-SHA256</option>
                <option value="">Passowrd-MD5</option>
                <option value="">Passowrd-SHA256</option>
              </select>
            </td>
          </tr>
        </template>
      </app-table>


    </app-box>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import AppBox from '@/components/boxes/AppBox.vue';
import InputTextWithLeftIcon from '@/components/inputs/InputTextWithLeftIcon.vue';
import AppTable from '@/components/table/AppTable.vue';
import TableHeadItemWithSort from '@/components/table/TableHeadItemWithSort.vue';
import ButtonSquare from '@/components/buttons/ButtonSquare.vue';


export default defineComponent({
  name: 'UserAddPage',

  components: { AppBox, AppTable, ButtonSquare, InputTextWithLeftIcon, TableHeadItemWithSort }
});
</script>