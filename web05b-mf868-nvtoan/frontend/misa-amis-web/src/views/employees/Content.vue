<template>
  <div class="content">
    <HeaderContent @openForm="openForm" />
    <div class="content__main">
      <ContentUtility
        @refreshData="refreshData"
        @filterDataTable="getDataFilter"
        @exportData="exportData"
        @deleteSelectedRows="
          employeeTable.currentSelectedRows.length > 0
            ? openMultipleDeletePopup()
            : (showNoRecordPopup = true)
        "
      />
      <Table
        ref="Table"
        :customData="employeeTable"
        @dbClickRow="openForm"
        @clickDefaultFunction="openForm"
        @clickFunctionItem="clickFunctionItem"
        @clickPageNum="changePageNum"
        @changePageSize="changePageSize"
        @changeListSelectedRow="changeListSelectedRow"
      />
    </div>
    <Form ref="Form" @refreshData="refreshData" />

    <!-- Popup confirm xóa một bản ghi -->
    <BasePopup v-show="showDeletePopup">
      <template #content>
        <div class="page-icon popup-icon">
          <div class="popup__icon-warning"></div>
        </div>
        <div class="popup__text">
          Bạn có thực sự muốn xóa Nhân viên {{ deleteString }} không?
        </div>
      </template>
      <template #footer>
        <div
          class="btn btn-default btn-cancel"
          @click="showDeletePopup = false"
        >
          Không
        </div>
        <div class="btn btn-primary" @click="deleteSelectedEmployee">Có</div>
      </template>
    </BasePopup>

    <!-- Popup confirm xóa nhiều -->
    <BasePopup v-show="showMultipleDeletePopup">
      <template #content>
        <div class="page-icon popup-icon">
          <div class="popup__icon-warning"></div>
        </div>
        <div class="popup__text">
          Bạn có thực sự muốn xóa
          <span class="text-semibold">{{
            employeeTable.currentSelectedRows.length
          }}</span>
          Nhân viên không?
        </div>
      </template>
      <template #footer>
        <div
          class="btn btn-default btn-cancel"
          @click="showMultipleDeletePopup = false"
        >
          Không
        </div>
        <div class="btn btn-primary" @click="deleteSelectedRows">Có</div>
      </template>
    </BasePopup>

    <!-- Popup khi ấn xóa mà chưa chọn gì -->
    <BasePopup class="popup-error" v-show="showNoRecordPopup">
      <template #content>
        <div class="page-icon popup-icon">
          <div class="popup__icon-warning"></div>
        </div>
        <div class="popup__text">
          Vui lòng chọn ít nhất một bản ghi trước khi xóa
        </div>
      </template>
      <template #footer>
        <div class="footer__btn d-flex flex-center">
          <div class="btn btn-primary" @click="showNoRecordPopup = false">
            Đóng
          </div>
        </div>
      </template>
    </BasePopup>

    <!-- popup thông báo lỗi -->
    <BasePopup class="popup-error" v-show="showErrorPopup">
      <template #content>
        <div class="page-icon popup-icon">
          <div class="popup__icon-danger"></div>
        </div>
        <div class="popup__text">Có lỗi xảy ra, vui lòng liên hệ MISA</div>
      </template>
      <template #footer>
        <div class="footer__btn d-flex flex-center">
          <div class="btn btn-primary" @click="showErrorPopup = false">
            Đóng
          </div>
        </div>
      </template>
    </BasePopup>
  </div>
</template>

<script>
import HeaderContent from "./HeaderContent.vue";
import ContentUtility from "./ContentUtility.vue";
import Form from "./Form.vue";
import Table from "../../components/table/Table.vue";
import BasePopup from "../../components/BasePopup.vue";
import EmployeesAPI from "../../api/components/employees/EmployeesAPI";
import Resource from "../../js/common/Resource";

export default {
  components: {
    HeaderContent,
    ContentUtility,
    Table,
    Form,
    BasePopup,
  },
  data() {
    return {
      employeeTable: {
        column: [
          {
            selectBoxColumn: true,
          },
          {
            columnName: "STT",
            dataType: Resource.DataTypeColumn.OrderNumber,
          },
          {
            columnName: "Mã nhân viên",
            fieldName: "employeeCode",
          },
          {
            columnName: "Tên nhân viên",
            fieldName: "employeeName",
          },
          {
            columnName: "Giới tính",
            fieldName: "gender",
            dataType: Resource.DataTypeColumn.Enum,
            enumName: "Gender",
          },
          {
            columnName: "Ngày sinh",
            fieldName: "dateOfBirth",
            dataType: Resource.DataTypeColumn.Date,
          },
          {
            columnName: "Số CMND",
            fieldName: "identityNumber",
          },
          {
            columnName: "Chức danh",
            fieldName: "employeePosition",
          },
          {
            columnName: "Tên đơn vị",
            fieldName: "departmentName",
          },
          {
            columnName: "Số tài khoản",
            fieldName: "bankAccountNumber",
          },
          {
            columnName: "Tên ngân hàng",
            fieldName: "bankName",
          },
          {
            columnName: "Chi nhánh ngân hàng",
            fieldName: "bankBranchName",
          },
          {
            columnName: "Chức năng",
            functionColumn: true,
          },
        ],
        functions: ["Nhân bản", "Xóa", "Ngưng sử dụng"],
        defaultFunction: "Sửa",
        gridData: null,
        currentSelectedRows: [],
        idFieldName: "EmployeeId",
        pageSize: 10,
        currentPageNum: 1,
        totalPage: 1,
        maxPageNumDisplay: 5,
        totalRecord: 0,
        filterValue: null,
      },
      showDeletePopup: false,
      showMultipleDeletePopup: false,
      showErrorPopup: false,
      showNoRecordPopup: false,
      employeeDelete: null,
      deleteString: "",
    };
  },
  created() {
    this.getDataServer();
  },
  methods: {
    /**
     * Hàm lấy dữ liệu trên server
     * NVTOAN 16/06/2021
     */
    async getDataServer() {
      this.$store.commit("SHOW_LOADER", true);

      await EmployeesAPI.filter(
        this.employeeTable.pageSize,
        this.employeeTable.currentPageNum,
        this.employeeTable.filterValue
      )
        .then((response) => {
          this.employeeTable.gridData = response.data.data;
          this.employeeTable.totalRecord = response.data.totalRecord;
          this.employeeTable.totalPage = response.data.totalPage;
        })
        .catch(() => {
          this.showErrorPopup = true;
          this.employeeTable.gridData = [];
          this.$store.commit("SHOW_LOADER", false);
        });

      await this.$store.commit("SHOW_LOADER", false);

      this.$refs.Table.resetCurrentSelectedRows();
    },

    /**
     * Hàm refresh dữ liệu
     * NVTOAN
     */
    refreshData() {
      //Reset lại các dữ liệu
      this.employeeTable.currentPageNum = 1;

      this.getDataServer();
    },

    /**
     * Hàm mở form thêm sửa
     * NVTOAN 14/06/2021
     */
    openForm(employee) {
      if (employee) {
        this.$refs.Form.openForm(employee.employeeId);
      } else {
        this.$refs.Form.openForm("");
      }
    },

    /**
     * Hàm xử lý sự kiện của item function
     * NVTOAN 07/07/2021
     */
    clickFunctionItem(fn, item) {
      switch (fn) {
        case "Xóa":
          this.deleteString = "<" + item.employeeCode + ">";
          this.employeeDelete = item;
          this.showDeletePopup = true;
          break;
        case "Nhân bản":
          this.$refs.Form.openForm("", item);
          break;
      }
    },

    /**
     * Hàm xóa một nhân viên đã chọn
     * NVTOAN 07/07/2021
     */
    async deleteSelectedEmployee() {
      await EmployeesAPI.delete(this.employeeDelete.employeeId)
        .then(async (response) => {
          if (response.status != 204) {
            //Tắt popup
            this.showDeletePopup = false;

            //Reload dữ liệu
            await this.getDataServer();

            //Hiển thị toast message
            this.$store.commit("SHOW_TOAST", {
              toastType: "success",
              toastMessage: Resource.Message.DeleteSuccess,
            });
          }
        })
        .catch(() => {
          this.showErrorPopup = true;
        });
    },

    /**
     * Hàm filter dữ liệu
     * NVTOAN 09/07/2021
     */
    async getDataFilter(filterValue) {
      this.employeeTable.filterValue = filterValue;
      this.employeeTable.currentPageNum = 1;

      this.getDataServer();
    },

    /**
     * Hàm thay đổi page size
     * NVTOAN 23/06/2021
     */
    changePageSize(pageSize) {
      this.employeeTable.pageSize = pageSize;

      //Reset page nubmer
      this.employeeTable.currentPageNum = 1;

      //Lấy lại dữ liệu
      this.getDataServer();
    },

    /**
     * Hàm thay đổi page nubmer
     * NVTOAN 09/07/2021
     */
    changePageNum(pageNum) {
      this.employeeTable.currentPageNum = pageNum;
      this.getDataServer();
    },

    /**
     * Hàm export dữ liệu sang file Excel
     * NVTOAN 12/07/2021
     */
    exportData() {
      EmployeesAPI.exportData(this.employeeTable.filterValue)
        .then((response) => {
          if (response) {
            const blob = new Blob([response.data], {
              type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            });
            const link = document.createElement("a");
            link.href = URL.createObjectURL(blob);
            link.download = "Danh sách nhân viên";
            link.click();
            URL.revokeObjectURL(link.href);
          }
        })
        .catch(() => {
          this.showErrorPopup = true;
        });
    },

    /**
     * Hàm thay đổi list bản ghi được chọn
     * NVTOAN 15/07/2021
     */
    changeListSelectedRow(list) {
      this.employeeTable.currentSelectedRows = JSON.parse(JSON.stringify(list));
    },

    /**
     * Hàm mở popup xóa nhiều
     * NVTOAN 19/07/2021
     */
    openMultipleDeletePopup() {
      //Nếu chỉ có một nhân viên thì hiện popup xóa 1
      if(this.employeeTable.currentSelectedRows.length == 1) {
        this.employeeDelete = this.employeeTable.gridData[this.employeeTable.currentSelectedRows[0]];

        this.deleteString = "<" + this.employeeDelete.employeeCode + ">";

        this.showDeletePopup = true;
      }
      else {
        this.showMultipleDeletePopup = true;
      }
    },

    /**
     * Hàm xóa tất cả bản ghi đã được chọn
     * NVTOAN 15/07/2021
     */
    async deleteSelectedRows() {
      let listId = [];

      //Lấy ra list id bản ghi đã chọn
      this.employeeTable.currentSelectedRows.map((item) => {
        listId.push(this.employeeTable.gridData[item].employeeId);
      });

      //Xóa
      await EmployeesAPI.multipleDelete(listId)
        .then(async (response) => {
          if (response.status != 204) {
            //Tắt popup
            this.showMultipleDeletePopup = false;

            //Reload dữ liệu
            await this.getDataServer();

            //Hiển thị toast message
            this.$store.commit("SHOW_TOAST", {
              toastType: "success",
              toastMessage: Resource.Message.DeleteSuccess,
            });
          }
        })
        .catch(() => {
          this.showErrorPopup = true;
        });
    },
  },
};
</script>

<style scoped>
.content {
  padding: 0 20px;
  background-color: #f4f5f8;
  height: calc(100vh - var(--header-height));
}

.content__main {
  background-color: #fff;
  max-height: calc(100vh - var(--header-height) - 86px);
  padding: 0 16px;
}
</style>