<template>
  <div class="content__util">
    <div class="search-box" :class="{ 'search-box-active': searchFocused }">
      <input
        type="text"
        class="field-input__input"
        placeholder="Tìm theo mã, tên hoặc số điện thoại"
        v-debounce:400ms="filterDataTable"
        @focus="searchFocused = true"
        @blur="searchFocused = false"
      />
      <div class="page-icon">
        <div class="util__icon-search"></div>
      </div>
    </div>
    <div class="btn-refresh page-icon" @click="refreshData">
      <Tooltip :customData="'Làm mới'">
        <div class="util__icon-refresh"></div>
      </Tooltip>
    </div>
    <div class="btn-export page-icon" @click="exportData">
      <Tooltip :customData="'Xuất khẩu'">
        <div class="util__icon-export"></div>
      </Tooltip>
    </div>
    <div class="page-icon" @click="deleteSelectedRows">
      <Tooltip style="height: 20px" :customData="'Xóa bản ghi'">
        <svg
          width="20px"
          height="20px"
          opacity="0.4"
          aria-hidden="true"
          focusable="false"
          data-prefix="fas"
          data-icon="trash-alt"
          class="svg-inline--fa fa-trash-alt fa-w-14"
          role="img"
          xmlns="http://www.w3.org/2000/svg"
          viewBox="0 0 448 512"
        >
          <path
            fill="currentColor"
            d="M32 464a48 48 0 0 0 48 48h288a48 48 0 0 0 48-48V128H32zm272-256a16 16 0 0 1 32 0v224a16 16 0 0 1-32 0zm-96 0a16 16 0 0 1 32 0v224a16 16 0 0 1-32 0zm-96 0a16 16 0 0 1 32 0v224a16 16 0 0 1-32 0zM432 32H312l-9.4-18.7A24 24 0 0 0 281.1 0H166.8a23.72 23.72 0 0 0-21.4 13.3L136 32H16A16 16 0 0 0 0 48v32a16 16 0 0 0 16 16h416a16 16 0 0 0 16-16V48a16 16 0 0 0-16-16z"
          ></path>
        </svg>
      </Tooltip>
    </div>
  </div>
</template>

<script>
import Tooltip from "../../components/Tooltip.vue";
export default {
  components: {
    Tooltip,
  },
  data() {
    return {
      searchFocused: false,
      refreshFocused: false,
      exportFocused: false,
    };
  },
  methods: {
    /**
     * Hàm refresh dữ liệu
     * NVTOAN 05/07/2021
     */
    refreshData() {
      this.$emit("refreshData");
    },

    /**
     * Hàm filter dữ liệu trên bảng
     * NVTOAN 09/07/2021
     */
    filterDataTable(val) {
      this.$emit("filterDataTable", val);
    },

    /**
     * Hàm export dữ liệu sang excel
     * NVTOAN 12/07/2021
     */
    exportData() {
      this.$emit("exportData");
    },

    /**
     * Hàm xóa bản ghi đã chọn
     * NVTOAN 15/07/2021
     */
    deleteSelectedRows() {
      this.$emit('deleteSelectedRows');
    }
  },
};
</script>

<style scoped>
@import url("../../assets/css/views/employees/contentUtility.css");
</style>