<template>
  <app-access-account-box :header="headerTexts">
    <div class="mt-3">
      <form>
        <input-text-with-left-icon :input-desc="form.username"
          @input-changed="value => { form.username.data.value = value }" />

        <input-text-with-left-icon :input-desc="form.password" class="mt-5"
          @input-changed="value => { form.password.data.value = value }" />

        <div class="mt-3">
          <div class="ml-1 text-sm text-gray-600 hover:text-gray-800">
            <router-link to="recovery">
              {{ $t('label.forgotYourPassword') }}?
            </router-link>
          </div>

          <button-with-loading :button-with-loading-desc="buttonSubmit" class="mt-5" />
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
  name: 'AccountSigninPage',

  components: { AppAccessAccountBox, ButtonWithLoading, InputTextWithLeftIcon },

  data() {
    return {
      headerTexts: {
        title: this.$t('message.loginTitle'),
        message: this.$t('message.welcome'),
        subtitle: this.$t('label.logIn')
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
        },
        password: {
          type: 'password',
          label: 'Password',
          placeholder: '**** ****',
          icon: 'fa-solid fa-lock',
          data: {
            value: '',
            error: ''
          }
        }
      },
      buttonSubmit: {
        label: 'Entrar',
        icon: {
          icon: 'fa-solid fa-right-to-bracket',
        },
        isLoading: false
      }
    };
  }
});
</script>
