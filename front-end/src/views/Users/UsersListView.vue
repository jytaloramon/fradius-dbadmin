<template>
  <div>
    <table class="table-fixed text-sm">
      <thead class="text-left text-gray-800">
        <tr>
          <th>
            <checkbox-input-less-icon v-if="indexesSelected.size < users.length"
              :is-checked-input="indexesSelected.size > 0" @input-changed="v => eventTableCheckbox(v)" />

            <checkbox-input-square-icon v-else :is-checked-input="true" @input-changed="v => eventTableCheckbox(v)" />
          </th>

          <th>USUÁRIO</th>
          <th>E-MAIL</th>
          <th>ATIVO</th>
        </tr>
      </thead>

      <tbody>
        <tr v-for="(user, idx) in users" :key="idx">
          <td>
            <checkbox-input-square-icon :is-checked-input="indexesSelected.has(idx)"
              @input-changed="v => eventItemCheckbox(v, idx)" />
          </td>
          <td>{{ user.username }}</td>
          <td>{{ user.email }}</td>
          <td>{{ user.isActive ? 'sim' : 'não' }}</td>
        </tr>
      </tbody>
    </table>
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
      users: [
        { username: '@test1', email: 'test1@mail.com', isActive: true },
        { username: '@test2', email: 'test2@mail.com', isActive: true },
        { username: '@test3', email: 'test3@mail.com', isActive: true },
        { username: '@test4', email: 'test4@mail.com', isActive: true }
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