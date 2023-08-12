<template>
  <div>
    <div class="w-full flex flex-col items-center text-sm">
      <div class="w-full flex justify-between">
        <div>
          <span class="text-base">Lista de Usuários</span>
          <span class="ml-3">&lt;</span>
          <span class="ml-2">&gt;</span>
        </div>

        <div>
          <button type="button" class="px-3 py-1 border rounded-md">atualizar</button>
        </div>
      </div>

      <table class="w-full table-fixed">
        <thead class="text-left text-gray-600">
          <tr class="border-b-2">
            <th class="p-2">
              <div class="mt-1">
                <span>
                  <checkbox-input-less-icon v-if="indexesSelected.size < users.length"
                    :is-checked-input="indexesSelected.size > 0" @input-changed="v => eventTableCheckbox(v)" />

                  <checkbox-input-square-icon v-else :is-checked-input="true"
                    @input-changed="v => eventTableCheckbox(v)" />
                </span>
                
                <span class="text-xs">(62)</span>
              </div>
            </th>
            <th class="p-2">
              <div>USUÁRIO</div>
            </th>
            <th class="p-2">
              <div>EMAIL</div>
            </th>
            <th class="p-2">
              <div>ÚLTIMO REGISTRO</div>
            </th>
            <th class="p-2 text-center">
              <div>ATIVO</div>
            </th>
          </tr>
        </thead>

        <tbody>
          <tr v-for="(user, idx) in users" :key="idx" class="border-b-2">
            <td class="w-1/2 p-2">
              <div class="mt-1">
                <checkbox-input-square-icon :is-checked-input="indexesSelected.has(idx)"
                  @input-changed="v => eventItemCheckbox(v, idx)" />
              </div>
            </td>
            <td class="p-2">{{ user.username }}</td>
            <td class="p-2">{{ user.email }}</td>
            <td class="p-2">{{ user.lastRecord }}</td>
            <td class="p-2 text-center">
              <p>{{ user.isActive ? 'sim' : 'não' }}</p>
            </td>
          </tr>
        </tbody>
      </table>

      <ul class="w-full flex justify-center border-b-2 border-gray-200">
        <li>1</li>
        <li>2</li>
      </ul>

      <ul class="mt-4 py-2 flex border border-gray-500 rounded-lg text-xs">
        <li v-for="(p, k) in t" :key="k" class="px-2 py-1 border-r last:border-r-0 border-gray-400">
          <a href="" class="p-2">{{ p }}</a>
        </li>
      </ul>
    </div>

  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import IndexOutOfRangeException from '@/exceptions/IndexOutOfRangeException';

import CheckboxInputSquareIcon from '@/components/CheckboxInputSquareIcon.vue';
import CheckboxInputLessIcon from '@/components/CheckboxInputLessIcon.vue';


export default defineComponent({
  name: 'UsersListView',

  components: { CheckboxInputSquareIcon, CheckboxInputLessIcon },

  data() {
    return {
      indexesSelected: new Set() as Set<number>,
      t: ['<< Anterior', '1', '2', '3', '4', 'Próxima >>'],
      users: [
        { username: '@test1', email: 'test1@mail.com', lastRecord: '09:43 12/08/2019', isActive: true },
        { username: '@test2', email: 'test2@mail.com', lastRecord: '09:43 12/08/2019', isActive: false },
        { username: '@test3', email: 'test3@mail.com', lastRecord: '09:43 12/08/2019', isActive: true },
        { username: '@test4', email: 'test4@mail.com', lastRecord: '09:43 12/08/2019', isActive: true }
      ]
    };
  },

  methods: {
    eventTableCheckbox(value: boolean) {
      value ? this.selecteAllItems() : this.removeAllItemsSelected();
    },

    eventItemCheckbox(value: boolean, idx: number) {
      value ? this.addItemToSelected(idx) : this.removeItemFromSelected(idx);
    },

    removeAllItemsSelected() {
      if (this.indexesSelected.size <= 0) return;

      this.indexesSelected.clear();
    },

    selecteAllItems() {
      if (this.indexesSelected.size >= this.users.length) return;

      this.indexesSelected = new Set(Array(this.users.length).keys())
    },

    addItemToSelected(idx: number) {
      if (idx < 0 || idx >= this.users.length)
        throw new IndexOutOfRangeException(this.$t('message.indexOutOfRange', { interval: idx }));

      if (this.indexesSelected.has(idx)) return;

      this.indexesSelected.add(idx);
    },

    removeItemFromSelected(idx: number) {
      if (idx < 0 || idx >= this.users.length)
        throw new IndexOutOfRangeException(this.$t('message.indexOutOfRange', { interval: idx }));

      if (!this.indexesSelected.has(idx)) return;

      this.indexesSelected.delete(idx);
    }
  }
});
</script>