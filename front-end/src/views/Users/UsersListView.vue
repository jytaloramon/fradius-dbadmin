<template>
  <div>
    <div class="w-full flex justify-between">
      <div>
        <span class="text-base">Lista de Usuários</span>
        <span class="ml-3">&lt;</span>
        <span class="ml-2">&gt;</span>
      </div>

      <div>
        <button class="px-3 py-1 border rounded-md text-sm">
          <font-awesome-icon icon="fa-solid fa-rotate-left" size="sm" />
          atualizar
        </button>
      </div>
    </div>

    <app-table class="mt-1" :pagination="{ actual: 1, max: 2 }">
      <template v-slot:table-headcol>
        <th class="p-2">
          <div class="mt-1">
            <span>
              <checkbox-input-less-icon v-if="indexesSelected.size < users.length"
                :is-checked-input="indexesSelected.size > 0" @input-changed="v => eventTableCheckbox(v)" />

              <checkbox-input-square-icon v-else :is-checked-input="true" @input-changed="v => eventTableCheckbox(v)" />
            </span>
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
      </template>

      <template v-slot:table-bodyrow>
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
      </template>
    </app-table>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import IndexOutOfRangeException from '@/exceptions/IndexOutOfRangeException';

import CheckboxInputSquareIcon from '@/components/CheckboxInputSquareIcon.vue';
import CheckboxInputLessIcon from '@/components/CheckboxInputLessIcon.vue';

import AppTable from '@/components/table/AppTable.vue';


export default defineComponent({
  name: 'UsersListView',

  components: { CheckboxInputSquareIcon, CheckboxInputLessIcon, AppTable },

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