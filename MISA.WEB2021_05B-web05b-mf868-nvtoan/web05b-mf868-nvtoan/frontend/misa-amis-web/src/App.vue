<template>
  <div class="wrapper" id="app" :class="{'nav-small': smallNav}">
    <Navbar ref="Navbar" :customData="navbarData" @makeNavBig="makeNavBig" />
    <Main ref="Main" @makeNavSmall="makeNavSmall"/>
    <div class="loader" v-show="showLoader">
      <Loader/>
    </div>
    <ToastMessage ref="ToastMessage" :customData="customToast"/>
  </div>
</template>

<script>
import Navbar from './layout/TheNavbar.vue'
import Main from './layout/TheMain.vue'
import Loader from './components/Loader.vue'
import ToastMessage from './components/ToastMessage.vue'

export default {
  name: "App",
  components: {
    Navbar,
    Main,
    Loader,
    ToastMessage
  },
  data() {
    return {
      smallNav: false,
      overlayShow: false,
      navbarData: [
        {
          iconClass: "nav__icon-dashboard",
          itemName: "Tổng quan",
          routerLink: "/employees",
        },
        {
          iconClass: "nav__icon-cash",
          itemName: "Tiền mặt",
          routerLink: "",
        },
        {
          iconClass: "nav__icon-purchase",
          itemName: "Tiền gửi",
          routerLink: "",
        },
        {
          iconClass: "nav__icon-sale",
          itemName: "bán hàng",
          routerLink: "",
        },
        {
          iconClass: "nav__icon-invoice",
          itemName: "Quản lý hóa đơn",
          routerLink: "",
        },
        {
          iconClass: "nav__icon-stock",
          itemName: "Kho",
          routerLink: "",
        },
        {
          iconClass: "nav__icon-tools",
          itemName: "Công cụ dụng cụ",
          routerLink: "",
        },
        {
          iconClass: "nav__icon-assets",
          itemName: "Tài sản cố định",
          routerLink: "",
        },
        {
          iconClass: "nav__icon-tax",
          itemName: "Thuế",
          routerLink: "",
        },
        {
          iconClass: "nav__icon-price",
          itemName: "Giá thành",
          routerLink: "",
        },
        {
          iconClass: "nav__icon-general",
          itemName: "Tổng hợp",
          routerLink: "",
        },
        {
          iconClass: "nav__icon-budget",
          itemName: "Ngân sách",
          routerLink: "",
        },
        {
          iconClass: "nav__icon-report",
          itemName: "Báo cáo",
          routerLink: "",
        },
        {
          iconClass: "nav__icon-finance",
          itemName: "Phân tích tài chính",
          routerLink: "",
        },
      ],
    }
  },
  computed: {
    customToast() {
      return this.$store.getters.toastData;
    },
    showLoader() {
      return this.$store.getters.showLoader;
    }
  },
  watch: {
    customToast: {
      deep: true,
      handler() {
        this.$refs.ToastMessage.showToast();
      }
    }
  },
  methods: {
    /**
     * Hàm thu nhỏ navbar
     * NVTOAN 05/07/2021
     */
    makeNavSmall() {
      this.$refs.Navbar.makeNavSmall();
      this.smallNav = true;
    },

    /**
     * Hàm thông báo cho component con mở rộng navbar
     * NVTOAN 05/07/2021
     */
    makeNavBig() {
      this.$refs.Main.makeNavBig();
      this.smallNav = false;
    }
  }
}
</script>

<style>
@import url("./assets/css/common/reset.css");
@import url("./assets/css/common/global.css");

.nav-small {
    --nav-width: 52px;
}

.wrapper {
    display: flex;
    width: 100%;
}

.loader {
  position: fixed;
  top: 0;
  bottom: 0;
  right: 0;
  left: 0;
  display: flex;
  align-items: center;
  justify-content: center;
  z-index: 999;
}
</style>
