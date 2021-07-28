<template>
  <div class="grid" ref="Grid">
    <div class="table">
      <table>
        <thead>
          <tr>
            <td
              v-for="(item, index) in customData.column"
              :class="{
                'text-right': item.dataType == Resource.DataTypeColumn.Number,
                'text-center':
                  item.dataType == Resource.DataTypeColumn.Date ||
                  item.dataType == Resource.DataTypeColumn.OrderNumber,
                'col-function text-center': item.functionColumn,
                'col-selectBox': item.selectBoxColumn,
              }"
              :key="index"
              :title="item.columnName"
            >
              <div
                class="select-box"
                v-show="item.selectBoxColumn"
                @click="toggleSelectAllRow"
                :class="{
                  'rotate-90 box-selected':
                    currentSelectedRows.length == customData.pageSize,
                }"
              >
                <div
                  class="table__icon-row-select"
                  v-show="currentSelectedRows.length == customData.pageSize"
                ></div>
              </div>
              {{ item.columnName }}
            </td>
          </tr>
        </thead>
        <tbody ref="tbody">
          <tr
            tabindex="0"
            :ref="index"
            v-for="(item, index) in customData.gridData"
            :ItemId="item[customData.idFieldName]"
            :key="index"
            @mouseenter.exact="rowHover"
            @mouseleave="rowUnhover"
            @click.exact="clickRow(index)"
            @click.ctrl="ctrlClickRow(index)"
            @dblclick="dbClickRow(item)"
            @keydown.up="pressUpArrowKey(index)"
            @keydown.down="pressDownArrowKey(index)"
            @mouseenter.shift="mouseEnterWhenShiftPressed(index)"
            @contextmenu.prevent="rightClickRow($event, index)"
          >
            <td
              v-for="(col, index) in customData.column"
              :class="{
                'text-right': col.dataType == Resource.DataTypeColumn.Number,
                'text-center':
                  col.dataType == Resource.DataTypeColumn.Date ||
                  col.dataType == Resource.DataTypeColumn.OrderNumber,
                'col-function': col.functionColumn,
                'col-selectBox': col.selectBoxColumn,
                'tr-selected': checkRowSelected(
                  customData.gridData.indexOf(item)
                ),
              }"
              :style="[
                col.functionColumn
                  ? {
                      'z-index': dropup
                        ? customData.gridData.indexOf(item)
                        : customData.gridData.length -
                          customData.gridData.indexOf(item),
                    }
                  : {},
              ]"
              :key="index"
              :title="
                getDisplayValue(item[col.fieldName], col.dataType, col.enumName)
              "
            >
              <div
                class="select-box"
                :class="{
                  'rotate-90 box-selected': checkRowSelected(
                    customData.gridData.indexOf(item)
                  ),
                }"
                v-show="col.selectBoxColumn"
                @click.stop="selectBoxClick(customData.gridData.indexOf(item))"
                @dblclick.prevent.stop
              >
                <div
                  class="table__icon-row-select"
                  v-show="
                    currentSelectedRows.includes(
                      customData.gridData.indexOf(item)
                    )
                  "
                ></div>
              </div>
              <div class="function-cell" v-show="col.functionColumn">
                <span
                  class="text-semibold"
                  @click="clickDefaultFunction(item)"
                  >{{ customData.defaultFunction }}</span
                >
                <div
                  class="function-cel__dropdown"
                  tabindex="0"
                  @blur="closeDropdown"
                >
                  <div
                    class="page-icon"
                    :class="{
                      'dropdown-icon-select':
                        currentDropdown == customData.gridData.indexOf(item),
                    }"
                    @click.stop="toggleDropdownFunction($event, item)"
                    @dblclick.prevent.stop
                  >
                    <div class="table__icon-dropdown"></div>
                  </div>
                  <transition name="slide-fade">
                    <div
                      ref="Dropdown"
                      class="dropdown-function"
                      :style="[
                        dropup
                          ? { top: '-' + (dropdownHeight + 6) + 'px' }
                          : {},
                        dropdownRightPosition
                          ? { right: dropdownRightPosition + 'px' }
                          : {},
                      ]"
                      v-if="
                        currentDropdown == customData.gridData.indexOf(item)
                      "
                    >
                      <div
                        class="dropdown-function__item"
                        v-for="(fn, index) in customData.functions"
                        :key="index"
                        @click="clickFunctionItem(fn, item)"
                      >
                        {{ fn }}
                      </div>
                    </div>
                  </transition>
                </div>
              </div>
              {{
                col.dataType == Resource.DataTypeColumn.OrderNumber
                  ? customData.gridData.indexOf(item) + 1
                  : getDisplayValue(
                      item[col.fieldName],
                      col.dataType,
                      col.enumName
                    )
              }}
            </td>
          </tr>
        </tbody>
      </table>
      <div
        class="data-empty"
        v-if="!customData.gridData || customData.gridData.length == 0"
      >
        <div class="data-empty__content">
          <img src="../../assets/images/eport_nodata.76e50bd8.svg" alt="" />
          <span>Không có dữ liệu</span>
        </div>
      </div>
    </div>
    <Paging
      :customData="paging"
      @clickPageNum="clickPageNum"
      @changePageSize="changePageSize"
      ref="Paging"
    />
  </div>
</template>

<script>
import CommonFn from "../../js/common/CommonFn";
import Paging from "./Paging.vue";
import Resource from "../../js/common/Resource";

export default {
  components: {
    Paging,
  },
  props: {
    customData: {
      type: Object,
      required: true,
    },
  },
  mounted() {
    this.displayDropdown = true;
  },
  data() {
    return {
      Resource: Resource,
      currentSelectedRows: [],
      selectedRow: null,
      countClick: 0,
      timer: null,
      currentDropdown: null,
      dropup: false,
      dropdownHeight: null,
      dropdownRightPosition: 0,
      showEmptyData: false,
      paging: {
        pageSize: 1,
        totalRecord: 0,
        totalPage: 1,
        maxPageNumDispaly: 0,
        currentPageNum: 1,
      },
    };
  },
  created() {
    this.paging.pageSize = this.customData.pageSize;
    this.paging.totalRecord = this.customData.totalRecord;
    this.paging.totalPage = this.customData.totalPage;
    this.paging.maxPageNumDisplay = this.customData.maxPageNumDisplay;
    this.paging.currentPageNum = this.customData.currentPageNum;
  },
  watch: {
    customData: {
      deep: true,
      handler(val) {
        //Dữ liệu paging
        this.paging.pageSize = val.pageSize;
        this.paging.totalRecord = val.totalRecord;
        this.paging.totalPage = val.totalPage;
        this.paging.maxPageNumDisplay = val.maxPageNumDisplay;
        this.paging.currentPageNum = val.currentPageNum;
      },
    },
    currentSelectedRows: {
      deep: true,
      immediate: true,
      handler(val) {
        this.$emit("changeListSelectedRow", val);
      },
    },
  },
  methods: {
    /**
     * Hàm chọn/bỏ chọn tất cả các row
     * NVTOAN 06/07/2021
     */
    toggleSelectAllRow() {
      //Auto chọn bản ghi đầu
      if (this.currentSelectedRows.length == this.customData.pageSize) {
        this.clickRow(0);
      } else {
        this.currentSelectedRows = [...Array(this.customData.gridData.length).keys()];
      }
    },
    /**
     * Hàm xử lý hover chuột vào row
     * NVTOAN 06/07/2021
     */
    rowHover(e) {
      e.target.classList.add("tr-hover");
    },

    /**
     * Hàm xử lý bỏ hover chuột vào row
     * NVTOAN 06/07/2021
     */
    rowUnhover(e) {
      e.target.classList.remove("tr-hover");
    },

    /**
     * Hàm xử lý click chọn một row
     * NVTOAN 15/07/2021
     */
    clickRow(index) {
      //Reset list
      this.currentSelectedRows = [];

      //Thêm row được chọn vào list
      this.currentSelectedRows.push(index);

      //focus vào ô được chọn
      this.$refs[index][0].focus();
    },

    /**
     * Hàm xử lý khi ctrl click vào row
     * NVTOAN 19/07/2021
     */
    ctrlClickRow(index) {
      //Thêm row được chọn vào list
      this.currentSelectedRows.push(index);

      //focus vào ô được chọn
      this.$refs[index][0].focus();
    },

    /**
     * Hàm xử lý click chuột vào row
     * NVTOAN 07/07/2021
     */
    dbClickRow(item) {
      this.$emit("dbClickRow", item);
    },

    /**
     * Hàm xử lý chọn row khi ấn mũi tên lên
     * NVTOAN 19/07/2021
     */
    pressUpArrowKey(index) {
      if (index > 0) {
        this.clickRow(index - 1);
      }
    },

    /**
     * Hàm xử lý chọn row khi ấn mũi tên xuống
     * NVTOAN 19/07/2021
     */
    pressDownArrowKey(index) {
      if (index < this.customData.gridData.length - 1) {
        this.clickRow(index + 1);
      }
    },

    /**
     * Hảm chọn nhiều khi chạy chuột và nhấn shift
     * NVTOAN 19/07/2021
     */
    mouseEnterWhenShiftPressed(index) {
      if (!this.currentSelectedRows.includes(index)) {
        this.ctrlClickRow(index);
      }
    },

    /**
     * Hàm hiện context menu
     * NVTOAN 19/07/2021
     */
    rightClickRow(event, index) {
      this.clickRow(index);

      //Hiển thị context menu
      this.currentDropdown = index;

      //vị trí ấn hiện tại
      var currentClickPosition = event.target.getBoundingClientRect();

      //Lấy ra function cell
      var listCell = event.target.closest("tr").querySelectorAll("td");
      var functonCell = listCell[listCell.length - 1];

      functonCell.querySelector(".function-cel__dropdown").focus();

      //Xét vị trí context menu
      setTimeout(() => {
        //Thông tin của dropdown
        let dropdownBound = functonCell
          .querySelector(".dropdown-function")
          .getBoundingClientRect();

        //Chiều cao của dropdown
        this.dropdownHeight = dropdownBound.height;
        this.dropdownRightPosition =
          window.innerWidth - event.x - dropdownBound.width;

        //Đểm cuối của table
        var tableHeight =
          this.$refs.Grid.querySelector(".table").getBoundingClientRect()
            .bottom;

        //Nếu vị trí ấn hiện tại + height dropdown vượt ra khỏi table => dropdown ngược lên
        if (currentClickPosition.y + this.dropdownHeight > tableHeight) {
          this.dropup = true;
        }
      }, 0.2);
    },

    /**
     * Hàm xử lý click chuột vào ô select
     * NVTOAN 06/07/2021
     */
    selectBoxClick(index) {
      if (this.currentSelectedRows.includes(index)) {
        this.currentSelectedRows.splice(
          this.currentSelectedRows.indexOf(index),
          1
        );
      } else {
        //Không thì thêm vào list đang được chọn
        this.currentSelectedRows.push(index);
      }
    },

    /**
     * Hàm kiểm tra xem row đã được chọn hay chưa
     * NVTOAN 06/07/2021
     */
    checkRowSelected(index) {
      return this.currentSelectedRows.includes(index);
    },

    /**
     * Hàm chuyển đổi dữ liệu để hiển thị lên bảng
     * NVTOAN 06/07/2021
     */
    getDisplayValue(data, dataType, enumName) {
      return CommonFn.convertOriginData(data, dataType, enumName);
    },

    /**
     * Hàm lấy ra số lượng item đang được chọn để hiện lên thông báo
     * NVTOAN 06/07/2021
     */
    getNumberSelectedItem() {
      return this.currentSelectedRows.length;
    },

    /**
     * Mở dropdown function
     * NVTOAN 06/07/2021
     */
    toggleDropdownFunction(event, item) {
      if (this.currentDropdown == null) {
        this.currentDropdown = this.customData.gridData.indexOf(item);
      } else {
        this.currentDropdown = null;
      }

      //vị trí ấn hiện tại
      var currentClickPosition = event.target.getBoundingClientRect();

      var cellFunction = event.target.closest(".function-cel__dropdown");

      //Hiển thị dropdown function
      setTimeout(() => {
        cellFunction.focus();

        //Chiều cao của dropdown
        this.dropdownHeight = cellFunction
          .querySelector(".dropdown-function")
          .getBoundingClientRect().height;

        //Đểm cuối của table
        var tableHeight =
          this.$refs.Grid.querySelector(".table").getBoundingClientRect()
            .bottom;

        //Nếu vị trí ấn hiện tại + height dropdown vượt ra khỏi table => dropdown ngược lên
        if (currentClickPosition.y + this.dropdownHeight > tableHeight) {
          this.dropup = true;
        }
      }, 0.2);
    },

    /**
     * Hàm đóng dropdown function
     * NVTOAN 06/07/2021
     */
    closeDropdown() {
      this.currentDropdown = null;
      this.dropup = false;
      this.dropdownRightPosition = 0;
    },

    /**
     * Gọi cha để thông báo người dùng click vào default function
     * NVTOAN 07/07/2021
     */
    clickDefaultFunction(item) {
      this.$emit("clickDefaultFunction", item);
    },

    /**
     * Gọi cha để thông báo người dùng click vào một item function
     * NVTOAN 07/07/2021
     */
    clickFunctionItem(fn, item) {
      this.$emit("clickFunctionItem", fn, item);
    },

    /**
     * Hàm gọi cha để chuyển trang được click
     * NVTOAN 06/07/2021
     */
    clickPageNum(pageNum) {
      this.$emit("clickPageNum", pageNum);
    },

    /**
     * Hàm lấy id của những row đang được chọn
     * NVTOAN 06/07/2021
     */
    getListIdSelectedItem() {
      let listId = [],
        rows = this.$refs.tbody.querySelectorAll(".tr-selected");

      //Lấy tất cả id của row đang được chọn
      for (let i = 0; i < rows.length; i++) {
        listId.push(rows[i].getAttribute("ItemId"));
      }

      return listId;
    },

    /**
     * Hàm thực hiện reset list item đang được chọn về focus vào dòng đầu tiên
     * NVTOAN 17/07/2021
     */
    resetCurrentSelectedRows() {
      this.clickRow(0);
    },

    /**
     * Hàm gọi cha thay đổi page size
     * NVTOAN 06/07/2021
     */
    changePageSize(pageSize) {
      this.$emit("changePageSize", pageSize);
    },
  },
};
</script>

<style scoped>
@import url("../../assets/css/components/table/table.css");
.slide-fade-enter-active {
  transition: transform 0.2s ease;
}
.slide-fade-leave-active {
  transition: transform 0s cubic-bezier(1, 0.5, 0.8, 1);
}
.slide-fade-enter,
.slide-fade-leave-to {
  transform: translateY(10px);
  opacity: 0;
}
</style>