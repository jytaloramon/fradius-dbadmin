<template>
  <app-access-account-box :header="headerTexts">
    <div class="mt-3">
      <form>
        <input-text-with-left-icon :input-desc="form.username"
          @input-changed="value => { form.username.data.value = value }" />

        <div class="mt-6 flex justify-between">
          <span class="my-auto">
            <router-link class="text-xs font-bold text-green-600 hover:text-green-700" to="signin">
              {{ $t('message.backToLogin') }}
            </router-link>
          </span>

          <span class="w-1/2">
            <button-with-loading :button-with-loading-desc="buttonSubmit" />
          </span>
        </div>
      </form>
    </div>
  </app-access-account-box>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import AppAccessAccountBox from '@/components/boxes/AppAccessAccountBox.vue';
import ButtonWithLoading from '@/components/buttons/ButtonWithLoading.vue'
import InputTextWithLeftIcon from '@/components/inputs/InputTextWithLeftIcon.vue'


export default defineComponent({
  name: 'AccountRecoveryPage',

  components: { AppAccessAccountBox, ButtonWithLoading, InputTextWithLeftIcon },

  data() {
    return {
      headerTexts: {
        title: this.$t('label.forgotYourPassword'),
        message: this.$t('message.passwordRecoverInstrution'),
        subtitle: this.$t('label.fillIn')
      },
      form: {
        username: {
          label: 'Username',
          placeholder: 'ramon04, mary_64',
          icon: 'fa-regular fa-user',
          data: {
            value: '',
            error: ''
          }
        }
      },
      buttonSubmit: {
        label: 'Entrar',
        icon: {
          icon: 'fa-solid fa-paper-plane',
        },
        isLoading: false
      }
    };
  }

});
</script>
