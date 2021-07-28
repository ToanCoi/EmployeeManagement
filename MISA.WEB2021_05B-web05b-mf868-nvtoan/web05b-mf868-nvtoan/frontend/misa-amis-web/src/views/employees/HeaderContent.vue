<template>
    <div class="content__header">
        <div class="content__name text-bold">
            <span>Nhân viên</span>
        </div>
        <div class="btn btn-primary btn__add" @click="openForm">
            <span class="btn__name">Thêm mới nhân viên</span>
        </div>
    </div>
</template>

<script>
export default {
    methods: {
        /**
         * Hàm gọi cha mở form thêm sửa
         * NVTOAN 13/06/2021
         */
        openForm() {
            this.$emit('openForm', null);
        },
    },
}
</script>

<style scoped>
@import url('../../assets/css/common/button.css');
@import url('../../assets/css/views/employees/headerContent.css');
</style>