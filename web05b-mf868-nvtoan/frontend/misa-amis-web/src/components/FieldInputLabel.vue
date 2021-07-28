<template>
  <div
    class="input-label tooltip-error"
    :style="{ '--scale': scaleTooltip, '--tooltip-message': tooltipMsg }"
  >
    <label :for="customData.inputFieldName" class="text-semibold"
      >{{ customData.labelText }}
      <span v-if="customData.isRequired">
        <span style="color: red">*</span>
      </span>
    </label>
    <!-- <Tooltip :customData="errorMessage"> -->
    <template>
      <DxDateBox
        v-if="customData.dataType == 'Date'"
        v-model="cloneModel"
        displayFormat="dd/MM/yyyy"
        :height="customData.height"
        placeholder="dd/mm/yyyy"
        :useMaskBehavior="true"
        :width="customData.width"
        :showClearButton="true"
        class="dxDateBox"
      />
      <input
        v-else-if="customData.dataType == 'Number'"
        autocomplete="off"
        :type="customData.inputType"
        class="field-input text-right"
        :class="{ invalid: invalidInput }"
        :placeholder="customData.placeholder"
        :id="customData.inputFieldName"
        @focus="focus"
        @blur="blur"
        ref="input"
        v-model="cloneModel"
        v-money="money"
      />
      <input
        v-else
        autocomplete="off"
        :type="customData.inputType"
        :style="[{ width: customData.width }, { height: customData.height }]"
        class="field-input"
        :class="{ invalid: invalidInput }"
        :placeholder="customData.placeholder"
        :id="customData.inputFieldName"
        @focus="focus"
        @blur="blur"
        @keypress="validateMaxLength($event, cloneModel)"
        ref="input"
        v-model="cloneModel"
      />
    </template>
    <!-- </Tooltip> -->
  </div>
</template>

<script>
import DxDateBox from "devextreme-vue/date-box";
import Resource from "../js/common/Resource";
import moment from "moment";
import CommonFn from "../js/common/CommonFn";
import Enumeration from "../js/common/Enumeration";
// import Tooltip from "../components/Tooltip.vue";

export default {
  components: {
    DxDateBox,
    // Tooltip,
  },
  props: {
    customData: {
      type: Object,
      require: true,
    },
    model: {
      default: null,
    },
    saveValidate: {
      default: false,
    },
  },
  data() {
    return {
      errorMessage: "",
      tooltipMsg: "",
      invalidInput: false,
      valueUnique: true,
      cloneModel: "",
      focused: false,
      typed: false,
      originData: null,
      showTooltip: false,
      scaleTooltip: 0,
      money: {
        decimal: "",
        thousands: ".",
        prefix: "",
        suffix: " (VND) ",
        precision: 0,
        masked: false,
      },
    };
  },
  created() {
    this.cloneModel = this.model;
  },
  watch: {
    cloneModel: function (val) {
      //Nếu người dùng chưa nhấn gì thì truyền ra dữ liệu gốc
      if (!this.focused) {
        this.originData = val;
        this.$emit("getOriginData", this.customData.inputFieldName, val);
      }

      //Nếu người dùng đã từng gõ thì đánh dấu là đã gõ để validate
      if (val != this.originData) {
        this.typed = true;
      }

      //Nếu là required thì validate khi đang nhập
      if (this.customData.isRequired && this.typed && val != null) {
        this.invalidInput = !this.validateRequired(val);
      }

      //Nếu người dùng đang nhập thì bỏ cảnh báo đỏ và format dữ liệu
      if (val) {
        this.invalidInput = false;
        this.scaleTooltip = 0;
        this.errorMessage = "";

        //Format dữ liệu dựa theo kiểu dữ liệu tương ứng
        val = this.formatData(val);
      }
      
      //Nếu xóa hết trong khi trường đó không bắt buộc nhập thì xóa hết lỗi
      if(!val && !this.customData.isRequired) {
        this.invalidInput = false;
        this.scaleTooltip = 0;
        this.errorMessage = "";
      }

      //Thay đổi model gốc ra ngoài
      this.$emit("updateValueInput", this.customData.inputFieldName, val);
    },
    model: {
      deep: true,
      immediate: true,
      handler(val) {
        this.cloneModel = JSON.parse(JSON.stringify(val));
      },
    },
    saveValidate(val) {
      if (val) {
        this.validate();
      }
    },
  },
  methods: {
    /**
     * Hàm xử lý sự kiện focus
     * NVTOAN 08/07/2021
     */
    focus() {
      this.focused = true;
      this.$refs.input.select();
    },
    /**
     * Hàm xử lý sự kiện khi không focus vào input nữa (check validate)
     * NVTOAN 08/07/2021
     */
    blur() {
      if (this.typed) {
        this.validate();
      }
    },

    /**
     * Hàm format dữ liệu để hiển thị theo kiểu dữ liệu tương ứng
     * NVTOAN 15/07/2021
     */
    formatData(val) {
      switch (this.customData.dataType) {
        case Resource.DataTypeColumn.Number:
          return val.toString().replace(/[^\d]/gi, "");
        case Resource.DataTypeColumn.Date:
          return (this.cloneModel = moment(this.cloneModel).format(
            "yyyy-MM-DD"
          ));
        case Resource.DataTypeColumn.Name:
          return CommonFn.formatName(val);
        default:
          return val;
      }
    },

    /**
     * Hàm validate dữ liệu
     * NVTOAN 08/07/2021
     */
    validate() {
      let value = this.cloneModel,
        isValid = true;

      if (this.customData.isRequired) {
        isValid = this.validateRequired(value);
      }

      if (isValid && value) {
        isValid = this.validateDataType(value);
      }

      if (!isValid) {
        this.invalidInput = true;
      }
    },

    /**
     * Hàm validate required
     * NVTOAN 08/07/2021
     */
    validateRequired(value) {
      if (!value || value.length <= 0) {
        //Thông điệp lỗi
        this.errorMessage = CommonFn.getRequiredErrorMsg(
          this.customData.labelText
        );
        this.displayTooltip();

        //Dữ liệu chưa hợp lệ
        this.$emit(
          "invalidData",
          this.errorMessage,
          Enumeration.ErrorType.Require
        );

        return false;
      }

      return true;
    },

    /**
     * Hàm gọi cha để validate unique
     * NVTOAN 08/07/2021
     */
    async validateUnique(value) {
      //gọi cha truy vấn api xem có dữ liệu hay chưa
      await this.$emit("checkUnique", this.customData.inputFieldName, value);

      return true;
    },

    /**
     * Hàm đánh dấu giá trị nhập không unique
     * NVTOAN 08/07/2021
     */
    async valueNotUnique() {
      //Đánh dấu không unque để hiện lỗi
      this.invalidInput = true;

      //Thông điệp lỗi
      let valueMsg = this.customData.labelText + "<" + this.cloneModel + ">";
      this.errorMessage = CommonFn.getUniqueErrorMsg(valueMsg);
      this.displayTooltip();

      //Dữ liệu chưa hợp lệ
      await this.$emit(
        "invalidData",
        this.errorMessage,
        Enumeration.ErrorType.Unique
      );
    },

    /**
     * Hàm validate độ dài tối đa
     * NVTOAN 15/07/2021
     */
    validateMaxLength(event, val) {
      //Nếu đã nhập đến độ dài tối đa
      if (
        this.customData.maxLength &&
        val &&
        val.length == this.customData.maxLength
      ) {
        //Ngăn người dùng tiếp tục nhập
        event.preventDefault();

        //Hiện lỗi
        this.invalidInput = true;

        //Tạo thông điệp lỗi
        this.errorMessage = CommonFn.getMaxLengthErrorMsg(
          this.customData.labelText,
          this.customData.maxLength
        );
        this.displayTooltip();

        //Thông báo lỗi ra ngoài
        this.$emit(
          "invalidData",
          this.errorMessage,
          Enumeration.ErrorType.MaxLength
        );
      }
    },

    /**
     * Hàm validate theo kiểu dữ liệu
     * NVTOAN 08/07/2021
     */
    validateDataType(value) {
      let res = true;

      switch (this.customData.dataType) {
        case Resource.DataTypeColumn.Email:
          res = this.validateEmail(value);
          break;
      }

      if (!res) {
        this.errorMessage = CommonFn.getDataTypeErrorMsg(
          this.customData.labelText
        );
        this.displayTooltip();

        //Dữ liệu chưa hợp lệ
        this.$emit(
          "invalidData",
          this.errorMessage,
          Enumeration.ErrorType.DataType
        );
      }

      return res;
    },

    /**
     * Validate Email
     * NVTOAN 08/07/2021
     */
    validateEmail(value) {
      let regex =
        /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

      return regex.test(value);
    },

    /**
     * Hàm hiển thị tooltip
     * NVTOAN 15/07/2021
     */
    displayTooltip() {
      this.tooltipMsg = "'" + this.errorMessage + "'";
      this.scaleTooltip = 1;
    }
  },
};
</script>

<style scoped>
@import url("../assets/css/components/filed-input-label.css");
@import url('../assets/css/common/tooltip-error.css');

.tooltip-error::before,
.tooltip-error::after {
  top: 30px;
}
</style>