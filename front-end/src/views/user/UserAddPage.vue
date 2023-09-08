<template>
  <app-box class="w-full h-5/6 mt-4">
    <div class="w-full">
      <div class="relative">
        <div class="absolute w-full h-1 bg-blue-200"></div>
        <div class="absolute w-2/3 h-1 bg-blue-400"></div>
        <span class="absolute w-3 h-3 -top-1 bg-blue-500 border-2 border-blue-400 rounded-full"></span>
        <span class="absolute w-3 h-3 -top-1 left-1/3 bg-blue-500 border-2 border-blue-400 rounded-full"></span>
        <span class="absolute w-3 h-3 -top-1 left-2/3 bg-blue-500 border-2 border-blue-400 rounded-full"></span>
        <span class="absolute w-3 h-3 -top-1 left-full bg-blue-400 border-2 border-blue-200 rounded-full"></span>
      </div>

      <div class="relative top-3 text-sm">
        <span class="float-left">Início</span>
        <span class="absolute left-[33%]">Lote</span>
        <span class="absolute left-[64%]">Individual</span>
        <span class="float-right">Fim</span>
      </div>
    </div>

    <div class="flex flex-row h-[90%] mt-12">
      <div class="w-8/12 ">
        <input-text-with-left-icon
          :input-desc="{ label: 'Usuário', data: { value: '' }, icon: 'fa-regular fa-user', placeholder: 'mary' }" />

        <div class="h-[75%] mt-8 overflow-y-auto">
          <app-table>
            <template v-slot:head>
              <table-head-item-with-sort class="py-2 px-3">Atributo</table-head-item-with-sort>
              <table-head-item-with-sort class="py-2 px-3">Operação</table-head-item-with-sort>
              <table-head-item-with-sort class="py-2 px-3">Valor</table-head-item-with-sort>
              <table-head-item-with-sort class="py-2 px-3"></table-head-item-with-sort>
            </template>

            <template v-slot:body>
              <tr v-for="i in 15" :key="i" class="border-b even:bg-white odd:bg-slate-100">
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
        </div>
      </div>

      <div class="w-4/12 pl-6 mt-6 ml-5 border-l border-dashed overflow-y-auto">
        <app-table>
          <template v-slot:head>
            <table-head-item-with-sort class="py-1 px-3">#</table-head-item-with-sort>
            <table-head-item-with-sort class="py-1 px-3">Username</table-head-item-with-sort>
            <table-head-item-with-sort class="py-1 px-3">Ações</table-head-item-with-sort>
          </template>

          <template v-slot:body>
            <tr v-for="i in 15" :key="i" class="border-b even:bg-white odd:bg-slate-100">
              <td class="py-1 px-3">{{ i }}</td>
              <td class="py-1 px-3">Attr-Teste-{{ i }}</td>
              <td class="py-1 px-3">
                <div class="pt-1">
                  <button-square
                    :description="{ label: 'Excluir', icon: 'fa-solid fa-trash', style: { borderColor: 'border-red-500' } }"
                    class="text-red-500" />
                </div>
              </td>
            </tr>
          </template>
        </app-table>
      </div>
    </div>
  </app-box>
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