<template>
  <app-box class="w-[420px] m-auto">
    <h1 class="text-center text-base font-bold">{{ headerTexts.title }}</h1>
    <h2 class="mt-2 text-base text-justify">{{ headerTexts.message }}</h2>
    <h3
      class="py-2 mt-2 text-sm text-center before:float-left before:w-[130px] before:mt-3 before:border-dashed before:border-[1px]  after:float-right after:w-[130px] after:mt-3 after:border-dashed after:border-[1px]">
      {{ headerTexts.subtitle }}
    </h3>

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
  </app-box>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import AppBox from '@/components/boxes/AppBox.vue';
import ButtonWithLoading from '@/components/buttons/ButtonWithLoading.vue'
import InputTextWithLeftIcon from '@/components/inputs/InputTextWithLeftIcon.vue'


export default defineComponent({
  name: 'AccountRecoveryPage',

  components: { AppBox, ButtonWithLoading, InputTextWithLeftIcon },

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
