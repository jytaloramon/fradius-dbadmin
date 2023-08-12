import { library } from '@fortawesome/fontawesome-svg-core';
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';

import { faUser } from '@fortawesome/free-regular-svg-icons';
import {
  faChartLine,
  faArrowUpFromBracket,
  faRotateLeft,
  faTrash,
  faPenToSquare
} from '@fortawesome/free-solid-svg-icons';

library.add(faUser, faChartLine, faArrowUpFromBracket, faRotateLeft, faTrash, faPenToSquare);

export default FontAwesomeIcon;
