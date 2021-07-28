import Vue from 'vue'

class EventBus {
    constructor() {
        this.bus = new Vue();
    }

    /**
     * Hàm lắng nghe sự kiện
     * NVTOAN 05/07/2021
     * @param {string} event 
     * @param {function} handler 
     */
    on(event, handler) {
        this.bus.$on(event, handler);
    }

    /**
     * Hàm lắng nghe sự kiện 1 lần
     * NVTOAN 05/07/2021
     * @param {string} event 
     * @param {function} handler 
     */
    once(event, handler) {
        this.bus.$once(event, handler);
    }

    /**
     * Hàm hủy lắng nghe sự kiện
     * NVTOAN 05/07/2021
     * @param {string} event 
     * @param {function} handler 
     */
    off(event, handler) {
        this.bus.$off(event, handler);
    }

    /**
     * Hàm emit sự kiện
     * NVTOAN 05/07/2021
     * @param {string} event 
     * @param {*} handler 
     */
    emit(event, ...args) {
        this.bus.$emit(event, ...args);
    }
}

export default {
    install(Vue) {
        const bus = new EventBus();

        Vue.prototype.$bus = bus;
    }
}