<template>
  <header class="header-page" id="header-page">
    <div class="header__left">
      <div class="page-icon" @click="makeNavSmall" v-show="bigNav">
        <div class="header__icon-small-nav"></div>
      </div>
      <div class="header__branch">
        <span class="header__branch-name text-uppercase"
          >Công ty TNHH Sản xuất - Thương mại - Dịch vụ Qui Phúc</span
        >
        <div class="page-icon">
          <div class="header__icon-branch-work"></div>
        </div>
      </div>
    </div>
    <div class="header__right">
      <div class="page-icon">
        <div class="header__icon-notify"></div>
      </div>
      <div class="header__user">
        <div class="header__icon-user"></div>
        <div class="user__name text-semibold">Nguyễn Văn Toàn</div>
        <div class="page-icon">
          <div class="header__icon-user-menu"></div>
        </div>
      </div>
    </div>
  </header>
</template>

<script>
export default {
  data() {
    return {
      bigNav: true,
    };
  },
  methods: {
    /**
     * Hàm gọi cha để thu nhỏ navbar
     * NVTOAN 05/07/2021
     */
    makeNavSmall() {
      this.$emit("makeNavSmall");
      this.bigNav = false;
    },

    /**
     * Hàm hiện icon khi navbar to
     * NVTOAN 05/07/2021
     */
    makeNavBig() {
      this.bigNav = true;
    },
  },
};
</script>

<style scoped>
@import url("../../assets/css/layout/header.css");
</style>