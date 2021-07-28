<template>
    <div class="main">
        <Header ref="Header" @makeNavSmall="makeNavSmall"/>
        <Content/>
    </div>
</template>

<script>
import Header from './main/TheHeader.vue'
import Content from './main/TheContent.vue'

export default {
  components: {
      Header,
      Content
  },
  methods: {
      /**
       * Hàm gọi cha để thu nhỏ navbar
       * NVTOAN 05/07/2021
       */
      makeNavSmall() {
          this.$emit('makeNavSmall');
      },
      
      /**
       * Hàm thông báo header mở rộng navbar
       * NVTOAN 05/07/2021
       */
      makeNavBig() {
          this.$refs.Header.makeNavBig('makeNavBig');
      },
  }
}
</script>

<style>
.main {
    width: calc(100% - var(--nav-width));
    height: 100vh;
}
</style>