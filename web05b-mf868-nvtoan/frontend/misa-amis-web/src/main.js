import Vue from 'vue'
import App from './App.vue'
import router from './router'
import Bus from './EventBus'
import vueDebounce from 'vue-debounce'
import 'devextreme/dist/css/dx.light.css';
import FieldInputLabel from './components/FieldInputLabel.vue'
import ComboBox from './components/Combobox.vue'
import Dropdown from './components/Dropdown.vue'

import Resource from './js/common/Resource'
import Enumeration from './js/common/Enumeration'
import CommonFn from './js/common/CommonFn'
import { store } from './store/index'

Vue.config.productionTip = false

Vue.use(Resource);
Vue.use(Enumeration)
Vue.use(CommonFn);

//Event bus
Vue.use(Bus);

// debounce
Vue.use(vueDebounce)
Vue.use(vueDebounce, {
  lock: true
})
Vue.use(vueDebounce, {
  listenTo: ['input', 'keyup']
})
Vue.use(vueDebounce, {
  defaultTime: '700ms'
})

//Component
Vue.component('FieldInputLabel', FieldInputLabel);
Vue.component('ComboBox', ComboBox);
Vue.component('Dropdown', Dropdown);

new Vue({
  store: store,
  router,
  render: h => h(App)
}).$mount('#app')
