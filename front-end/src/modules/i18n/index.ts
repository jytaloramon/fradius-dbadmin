import { createI18n } from 'vue-i18n';
import { i18nPtBr } from './ptBr';

const i18nModule = createI18n({
  locale: 'pt-BR',
  messages: {
    'pt-BR': i18nPtBr
  }
});

export { i18nModule };
