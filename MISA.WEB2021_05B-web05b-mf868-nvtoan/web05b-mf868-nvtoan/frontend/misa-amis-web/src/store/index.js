import Vue from "vue";
import Vuex from "vuex";

Vue.use(Vuex);

export const store = new Vuex.Store({
  state: {
    //Dữ liệu custom toast message
    toastData: {},

    //Hiển thị loader
    showLoader: false,
  },
  mutations: {
    /**
     * Hiển thị toast message
     * NVTOAN 17/07/2021
     * @param {*} state 
     * @param {*} toastData 
     */
    SHOW_TOAST(state, toastData) {

      state.toastData = JSON.parse(JSON.stringify(toastData));
    },
    
    /**
     * Hiển thị loader
     * NVTOAN 17/07/2021
     * @param {*} state 
     * @param {boolean} showLoader
     */
    SHOW_LOADER(state, showLoader) {
      state.showLoader = showLoader;
    },
    
  },
  getters: {
    //Dữ liệu custom toast message
    toastData: state => {
      return state.toastData;
    },

    //loader
    showLoader: state => {
      return state.showLoader;
    }
  }
});