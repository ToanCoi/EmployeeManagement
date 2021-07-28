<template>
  <form v-if="showForm">
    <div class="form">
      <div class="form__title">
        <div class="form__title-left">
          <div class="form__title-text">
            <span class="text-semibold">Thông tin nhân viên</span>
          </div>
          <div class="form__title-selectBox d-flex">
            <div class="selectBox d-flex" @click="selectCustomerBox">
              <div
                class="select-box"
                :class="{
                  'rotate-90 box-selected': customerBoxSelected,
                }"
              >
                <div
                  class="table__icon-row-select"
                  v-if="customerBoxSelected"
                ></div>
              </div>
              <span>Là khách hàng</span>
            </div>
            <div class="selectBox d-flex" @click="selectProviderBox">
              <div
                class="select-box"
                :class="{
                  'rotate-90 box-selected': providerBoxSelected,
                }"
              >
                <div
                  class="table__icon-row-select"
                  v-if="providerBoxSelected"
                ></div>
              </div>
              <span>Là nhà cung cấp</span>
            </div>
          </div>
        </div>
        <div class="form__title-right d-flex">
          <Tooltip :customData="'Trợ giúp'">
            <div class="page-icon">
              <div class="form__icon-help"></div>
            </div>
          </Tooltip>
          <Tooltip :customData="'Đóng'">
            <div class="page-icon" @click="closeForm">
              <div class="form__icon-cancel"></div>
            </div>
          </Tooltip>
        </div>
      </div>
      <div class="form__content" ref="FormData">
        <div class="form-group">
          <div class="form-row">
            <div class="form-col d-flex">
              <div class="form-item">
                <FieldInputLabel
                  MustValidate="true"
                  :saveValidate="saveValidate"
                  :customData="employeeCodeInput"
                  :model="employee.employeeCode"
                  @invalidData="invalidData"
                  @updateValueInput="updateValueInput"
                  @getOriginData="getOriginData"
                  @checkUnique="checkUnique"
                  :ref="employeeCodeInput.inputFieldName"
                />
              </div>
              <FieldInputLabel
                MustValidate="true"
                :saveValidate="saveValidate"
                :customData="employeeNameInput"
                :model="employee.employeeName"
                @invalidData="invalidData"
                @updateValueInput="updateValueInput"
                @getOriginData="getOriginData"
              />
            </div>
            <div class="form-col d-flex">
              <div class="form-item">
                <FieldInputLabel
                  :customData="dateOfBirthInput"
                  :model="employee.dateOfBirth"
                  @updateValueInput="updateValueInput"
                  @getOriginData="getOriginData"
                />
              </div>
              <RadioButton
                style="margin-left: 10px"
                :customData="genderRadioButton"
                :model="employee.gender"
                @updateValueInput="updateValueInput"
              />
            </div>
          </div>
          <div class="form-row">
            <div class="form-col">
              <label class="text-semibold"
                >Đơn vị <span style="color: red">*</span></label
              >
              <div MustValidate="true">
                <ComboBox
                  tabindex="0"
                  :saveValidate="saveValidate"
                  style="margin-top: 4px"
                  :customData="departmentComboBox"
                  :model="employee.departmentId"
                  @valueChanged="updateValueInput"
                  @invalidData="invalidData"
                />
              </div>
            </div>
            <div class="form-col d-flex">
              <div class="form-item">
                <FieldInputLabel
                  :customData="identityNumberInput"
                  :model="employee.identityNumber"
                  @updateValueInput="updateValueInput"
                  @getOriginData="getOriginData"
                />
              </div>
              <FieldInputLabel
                :customData="identityDateInput"
                :model="employee.identityDate"
                @updateValueInput="updateValueInput"
                @getOriginData="getOriginData"
              />
            </div>
          </div>
          <div class="form-row">
            <div class="form-col">
              <FieldInputLabel
                :customData="positionInput"
                :model="employee.employeePosition"
                @updateValueInput="updateValueInput"
                @getOriginData="getOriginData"
              />
            </div>
            <FieldInputLabel
              ieldInputLabel
              :customData="identityPlaceInput"
              :model="employee.identityPlace"
              @updateValueInput="updateValueInput"
              @getOriginData="getOriginData"
            />
          </div>
        </div>
        <div class="form-group">
          <div class="form-row">
            <FieldInputLabel
              :customData="addressInput"
              :model="employee.address"
              @updateValueInput="updateValueInput"
              @getOriginData="getOriginData"
            />
          </div>
          <div class="form-row">
            <div class="form-col d-flex">
              <div class="form-item">
                <FieldInputLabel
                  :customData="telephoneNumberInput"
                  :model="employee.telephoneNumber"
                  @updateValueInput="updateValueInput"
                  @getOriginData="getOriginData"
                />
              </div>
              <FieldInputLabel
                :customData="phoneNumberInput"
                :model="employee.phoneNumber"
                @updateValueInput="updateValueInput"
                @getOriginData="getOriginData"
              />
            </div>
            <div class="form-col">
              <FieldInputLabel
                MustValidate="true"
                :saveValidate="saveValidate"
                :customData="emailInput"
                :model="employee.email"
                @invalidData="invalidData"
                @updateValueInput="updateValueInput"
                @getOriginData="getOriginData"
              />
            </div>
          </div>
          <div class="form-row">
            <div class="form-col d-flex">
              <div class="form-item">
                <FieldInputLabel
                  :customData="bankAccountNumberInput"
                  :model="employee.bankAccountNumber"
                  @updateValueInput="updateValueInput"
                  @getOriginData="getOriginData"
                />
              </div>
              <FieldInputLabel
                :customData="bankNameInput"
                :model="employee.bankName"
                @updateValueInput="updateValueInput"
                @getOriginData="getOriginData"
              />
            </div>
            <div class="form-col">
              <FieldInputLabel
                :customData="bankBranchNameInput"
                :model="employee.bankBranchName"
                @updateValueInput="updateValueInput"
                @getOriginData="getOriginData"
              />
            </div>
          </div>
        </div>
      </div>
      <div class="form__footer">
        <div
          class="btn btn-default text-semibold"
          tabindex="0"
          @click="showForm = false"
          @keyup.enter="showForm = false"
        >
          Hủy
        </div>
        <div class="btn-group d-flex">
          <div
            class="btn btn-default text-semibold"
            tabindex="0"
            @click="saveAndOut"
            @keyup.enter="saveAndOut"
          >
            Cất
          </div>
          <div
            class="btn btn-primary text-semibold"
            tabindex="0"
            @click="saveAndAdd"
            @keyup.enter="saveAndAdd"
          >
            Cất và thêm
          </div>
        </div>
      </div>
    </div>
    <!-- Popup hỏi lại khi đã sửa dữ liệu mà thoát -->
    <BasePopup
      ref="ConfirmPopup"
      class="popup-confirm"
      v-show="showConfirmPopup"
    >
      <template #content>
        <div class="page-icon popup-icon">
          <div class="popup__icon-confirm"></div>
        </div>
        <div class="popup__text">
          Dữ liệu đã bị thay đổi. Bạn có muốn cất không?
        </div>
      </template>
      <template #footer>
        <div
          class="btn btn-default btn-cancel"
          @click="showConfirmPopup = false"
        >
          Hủy
        </div>
        <div class="btn-group d-flex">
          <div class="btn btn-default" @click="showForm = false">Không</div>
          <div class="btn btn-primary">Có</div>
        </div>
      </template>
    </BasePopup>

    <!-- Popup thông báo lỗi -->
    <BasePopup class="popup-error" v-show="showErrorPopup">
      <template #content>
        <div class="page-icon popup-icon">
          <div
            :class="uniqueError ? 'popup__icon-warning' : 'popup__icon-danger'"
          ></div>
        </div>
        <div class="popup__text">
          {{ errorMessage }}
        </div>
      </template>
      <template #footer>
        <div
          class="footer__btn d-flex"
          :class="uniqueError ? 'flex-end' : 'flex-center'"
        >
          <div class="btn btn-primary" @click="closeErrorPopup">
            {{ uniqueError ? "Đồng ý" : "Đóng" }}
          </div>
        </div>
      </template>
    </BasePopup>
  </form>
</template>

<script>
import FieldInputLabel from "../../components/FieldInputLabel.vue";
import ComboBox from "../../components/Combobox.vue";
import RadioButton from "../../components/RadioButton.vue";
import BasePopup from "../../components/BasePopup.vue";
import Tooltip from "../../components/Tooltip.vue";
import EmployeesAPI from "../../api/components/employees/EmployeesAPI";
import DepartmentAPI from "../../api/components/departments/DepartmentsAPI";
import Resource from "../../js/common/Resource";
import Enumeration from "../../js/common/Enumeration";
import CommonFn from "../../js/common/CommonFn";

function initState(show) {
  return {
    //input
    employeeCodeInput: {
      inputFieldName: "employeeCode",
      labelText: "Mã",
      width: "calc(var(--column-width) * 1.5 - 10px)",
      height: "35px",
      isRequired: true,
      inputType: "text",
      isUnique: true,
      maxLength: 50,
    },
    employeeNameInput: {
      inputFieldName: "employeeName",
      labelText: "Tên",
      width: "calc(var(--column-width) * 2.5 - 10px)",
      height: "35px",
      isRequired: true,
      inputType: "text",
      dataType: Resource.DataTypeColumn.Name,
      maxLength: 100,
    },

    dateOfBirthInput: {
      inputFieldName: "dateOfBirth",
      labelText: "Ngày sinh",
      width: "calc(var(--column-width) * 1.5 + 3px)",
      height: "37px",
      inputType: "date",
      dataType: Resource.DataTypeColumn.Date,
    },

    positionInput: {
      inputFieldName: "employeePosition",
      labelText: "Chức danh",
      width: "calc(var(--column-width) * 4 - 2px)",
      height: "35px",
      inputType: "text",
    },

    identityNumberInput: {
      inputFieldName: "identityNumber",
      labelText: "Số CMTND/ Căn cước",
      width: "calc(var(--column-width) * 2.5 - 10px)",
      height: "35px",
      inputType: "text",
      maxLength: 20,
    },
    identityDateInput: {
      inputFieldName: "identityDate",
      labelText: "Ngày cấp",
      width: "calc(var(--column-width) * 1.5 + 3px)",
      height: "37px",
      inputType: "date",
      dataType: Resource.DataTypeColumn.Date,
    },
    identityPlaceInput: {
      inputFieldName: "identityPlace",
      labelText: "Nơi cấp",
      width: "calc(var(--column-width) * 4 - 1px)",
      height: "35px",
      inputType: "text",
    },
    addressInput: {
      inputFieldName: "address",
      labelText: "Địa chỉ",
      width: "calc(var(--column-width) * 8 + 8px + 25px)",
      height: "35px",
      inputType: "text",
    },
    emailInput: {
      inputFieldName: "email",
      labelText: "Email",
      width: "calc(var(--column-width) * 4 - 1px)",
      height: "35px",
      inputType: "text",
      dataType: "Email",
      maxLength: 50,
    },
    phoneNumberInput: {
      inputFieldName: "phoneNumber",
      labelText: "ĐT di động",
      width: "calc(var(--column-width) * 2 - 12px)",
      height: "35px",
      inputType: "text",
      maxLength: 20,
    },
    telephoneNumberInput: {
      inputFieldName: "telephoneNumber",
      labelText: "ĐT cố định",
      width: "calc(var(--column-width) * 2 - 12px)",
      height: "35px",
      inputType: "text",
      maxLength: 20,
    },
    bankAccountNumberInput: {
      inputFieldName: "bankAccountNumber",
      labelText: "Tài khoản ngân hàng",
      width: "calc(var(--column-width) * 2 - 12px)",
      height: "35px",
      inputType: "text",
      maxLength: 20,
    },
    bankNameInput: {
      inputFieldName: "bankName",
      labelText: "Tên ngân hàng",
      width: "calc(var(--column-width) * 2 - 12px)",
      height: "35px",
      inputType: "text",
    },
    bankBranchNameInput: {
      inputFieldName: "bankBranchName",
      labelText: "Chi nhánh",
      width: "calc(var(--column-width) * 4 - 1px)",
      height: "35px",
      inputType: "text",
    },

    //ComboBox
    departmentComboBox: {
      inputFieldName: "departmentId",
      labelText: "Đơn vị",
      defaultValue: "",
      displayValues: [],
      keys: [],
      width: "calc(var(--column-width) * 4 + 10px)",
      height: "37px",
    },

    //Radio button
    genderRadioButton: {
      inputFieldName: "gender",
      labelText: "Giới tính",
      height: "37px",
      items: [
        {
          text: "Nam",
          value: 0,
        },
        {
          text: "Nữ",
          value: 1,
        },
        {
          text: "Khác",
          value: 2,
        },
      ],
      defaultValue: 0,
    },

    showForm: show,
    showConfirmPopup: false,
    showErrorPopup: false,
    customerBoxSelected: false,
    providerBoxSelected: false,
    employee: {},
    originData: {},
    employeeId: null,
    saveValidate: false,
    allInputValid: true,
    allUniue: true,
    formType: null,
    errorMessage: "",
    uniqueError: false,
  };
}

export default {
  components: {
    FieldInputLabel,
    ComboBox,
    RadioButton,
    BasePopup,
    Tooltip,
  },
  data() {
    return initState(false);
  },
  methods: {
    /**
     * Hàm mở form
     * NVTOAN 06/07/2021
     */
    async openForm(employeeId, employee) {
      //Gán lại giá trị của form
      await Object.assign(this.$data, initState(false));
      this.showForm = true;

      //Lấy department để bind lên combobox
      await DepartmentAPI.getAll()
        .then((response) => {
          response.data.map((department) => {
            this.departmentComboBox.displayValues.push(
              department.departmentName
            );

            this.departmentComboBox.keys.push(department.departmentId);
          });
        })
        .catch(() => {
          //Nếu không lấy được phòng ban
          this.allInputValid = false;
          this.errorMessage = Resource.Message.GetDepartmentError;
          this.showErrorPopup = true;
        });

      //Nếu là form sửa
      if (employeeId.length > 0) {
        //Xác định formType
        this.employeeId = employeeId;
        this.formType = Enumeration.FormMode.Edit;

        await EmployeesAPI.getById(employeeId)
          .then((response) => {
            this.employee = response.data;
          })
          .catch(() => {
            //Nếu không lấy được dữ liệu từ db
            this.allInputValid = false;
            this.errorMessage = Resource.Message.ServerError;
            this.showErrorPopup = true;
          });
      } else {
        //Xác định formType
        this.formType = Enumeration.FormMode.Add;

        //Nếu có employee thì là nhân bản
        if (employee) {
          //Xác định formType
          this.formType = Enumeration.FormMode.Clone;

          this.employee = JSON.parse(JSON.stringify(employee));
          this.employee.employeeName = "";
        }

        await EmployeesAPI.getNewEmployeeCode()
          .then((response) => {
            this.employee.employeeCode = response.data;
          })
          .catch(() => {
            //Nếu không lấy được mã nhân viên mới
            this.allInputValid = false;
            this.errorMessage = Resource.Message.GetNewEmployeeCodeError;
            this.showErrorPopup = true;
          });
      }

      // //Focus vào ô đầu tiên
      this.$refs[this.employeeCodeInput.inputFieldName].$vnode.elm
        .querySelector(".field-input")
        .focus();

      //Lấy dữ liệu gốc để đối chiếu xem người dùng đã thay đổi dữ liệu chưa
      this.originData = JSON.parse(JSON.stringify(this.employee));
    },

    /**
     * Hàm update dữ liệu khi người dùng thay đổi
     * NVTOAN 06/07/2021
     */
    updateValueInput(key, value) {
      this.$set(this.employee, key, value);
    },

    /**
     * Hàm lấy ra dữ liệu gốc
     * NVTOAN 06/07/2021
     */
    getOriginData(key, value) {
      this.$set(this.originData, key, value);
    },

    /**
     * Hàm đóng form
     * NVTOAN 07/06/2021
     */
    closeForm() {
      if (this.dataChanged()) {
        this.showConfirmPopup = true;
      } else {
        this.showForm = false;
      }
    },

    /**
     * Hàm kiểm tra dùng đã thay đổi dữ liệu trên form
     * NVTOAN 06/07/2021
     */
    dataChanged() {
      // this.userEdit = false;
      for (let prop in this.employee) {
        if (this.employee[prop] != null) {
          if (this.employee[prop].toString().length == 0) {
            if (this.originData[prop]) {
              return true;
            }
          } else if (this.employee[prop] !== this.originData[prop]) return true;
        }
      }

      return false;
    },

    /**
     * Hàm thêm và thoát form
     * NVTOAN 08/07/2021
     */
    async saveAndOut() {
      await this.saveData();

      //Nếu thêm thành công
      if (this.allInputValid) {
        this.showForm = false;
      }
    },

    /**
     * Hàm cất và thêm dữ liệu
     * NVTOAN 08/07/2021
     */
    async saveAndAdd() {
      this.saveValidate = true;
      await this.saveData();

      //Nếu thêm thành công
      if (this.allInputValid) {
        this.saveValidate = false;
        this.employee = {};

        //Lấy lại mã nhan viên
        await EmployeesAPI.getNewEmployeeCode()
          .then((response) => {
            this.employee.employeeCode = response.data;

            //Lấy dữ liệu gốc để đối chiếu xem người dùng đã thay đổi dữ liệu chưa
            this.originData = JSON.parse(JSON.stringify(this.employee));
          })
          .catch(() => {
            //Nếu không lưu được thì thông báo lỗi
            this.allInputValid = false;
            this.errorMessage = Resource.Message.ServerError;
            this.showErrorPopup = true;
          });
      }
    },

    /**
     * Hàm gọi api check unique
     * NVTOAN 09/07/2021
     */
    async checkUnique(propName, propValue) {
      await EmployeesAPI.getEmployeeByProperty(propName, propValue).then(
        async (response) => {
          switch (this.formType) {
            case Enumeration.FormMode.Add:
              if (response.status != 204) {
                this.allUniue = false;
              }
              break;

            case Enumeration.FormMode.Clone:
              if (response.status != 204) {
                this.allUniue = false;
              }
              break;

            case Enumeration.FormMode.Edit:
              if (response.data.employeeId != this.employee.employeeId) {
                this.allUniue = false;
              }
              break;
          }
        }
      );
    },

    /**
     * Hàm Lưu dữ liệu
     * NVTOAN 07/07/2021
     */
    async saveData() {
      //Biến đánh dấu để hiện popup
      this.saveValidate = true;

      //Trước khi lưu cần validate hết
      await this.validateAll();

      if (this.allInputValid) {
        switch (this.formType) {
          //Nếu là form thêm
          case Enumeration.FormMode.Add:
          case Enumeration.FormMode.Clone:
            await EmployeesAPI.insert(this.employee)
              .then((response) => {
                if (response.data.code == 200) {
                  //Hiển thị toast message
                  this.$store.commit('SHOW_TOAST', {
                    toastType: Resource.Toast.Success,
                    toastMessage: Resource.Message.AddSuccess,
                  });
                }

                this.$emit("refreshData");
              })
              .catch((e) => {
                //Nếu không lưu được thì thông báo lỗi
                this.allInputValid = false;
                this.errorMessage = e.response.data.data.status[0]
                  ? e.response.data.data.status[0]
                  : Resource.Message.ServerError;
                this.showErrorPopup = true;
              });
            break;
          //Nếu là form sửa
          case Enumeration.FormMode.Edit:
            await EmployeesAPI.update(this.employeeId, this.employee)
              .then((response) => {
                if (response.data.code == 200) {
                  //Hiển thị toast message
                  this.$store.commit('SHOW_TOAST', {
                    toastType: Resource.Toast.Success,
                    toastMessage: Resource.Message.EditSuccess,
                  });
                }

                this.$emit("refreshData");
              })
              .catch(() => {
                //Nếu không lưu được thì thông báo lỗi
                this.allInputValid = false;
                this.errorMessage = Resource.Message.ServerError;
                this.showErrorPopup = true;
              });
            break;
        }
      }
    },

    /**
     * Hàm validate tất cả dữ liệu trước khi thêm
     * NVTOAN 09/07/2021
     */
    async validateAll() {
      //Reset
      this.allInputValid = true;
      this.allUniue = true;

      //Lấy tất cả trường cần validate
      let elValidate = this.$refs.FormData.querySelectorAll("[MustValidate]");

      //Validate tất cả trường
      for (let i = 0; i < elValidate.length; i++) {
        await elValidate[i].querySelector(".field-input").focus();
        await elValidate[i].querySelector(".field-input").blur();
      }

      await this.checkUnique("EmployeeCode", this.employee.employeeCode);

      if (!this.allUniue) {
        let errorMessage = CommonFn.getUniqueErrorMsg(
          "Mã nhân viên <" + this.employee.employeeCode + ">"
        );

        await this.invalidData(errorMessage, Enumeration.ErrorType.Unique);
      }
    },

    /**
     * Đánh dấu dữ liệu chưa hợp lệ
     * NVTOAN 08/07/2021
     */
    invalidData(errorMessage, typeError) {
      this.allInputValid = false;

      //Nếu là ấn nút save và chưa có lỗi nào trước đó thì lấy lỗi đầu tiên để hiển thị
      if (!this.errorMessage && this.saveValidate) {
        this.errorMessage = errorMessage;

        //Tùy vào các loại lỗi thì hiển thị popup tương ứng
        switch (typeError) {
          case Enumeration.ErrorType.Require:
          case Enumeration.ErrorType.DataType:
          case Enumeration.ErrorType.MaxLength:
            this.uniqueError = false;
            break;
          case Enumeration.ErrorType.Unique:
            this.uniqueError = true;
            break;
        }

        this.showErrorPopup = true;
      }
    },

    /**
     * Hàm thực hiện đóng popup lỗi
     * NVTOAN 09/07/2021
     */
    closeErrorPopup() {
      this.showErrorPopup = false;

      //Reset lại giá trị all valid
      this.allInputValid = true;

      //Reset lại thông điệp lỗi
      this.errorMessage = "";

      //reset biến đánh dấu ấn save
      this.saveValidate = false;
    },

    /**
     * Hàm thực hiện khi nhấn chọn là khách hàng
     * NVTOAN 06/07/2021
     */
    selectCustomerBox() {
      this.customerBoxSelected = !this.customerBoxSelected;
    },

    /**
     * Hàm thực hiện khi nhấn chọn là nhà cung cấp
     * NVTOAN 06/07/2021
     */
    selectProviderBox() {
      this.providerBoxSelected = !this.providerBoxSelected;
    },
  },
};
</script>

<style scoped>
@import url("../../assets/css/views/employees/form.css");
@import url("../../assets/css/components/popup.css");

.slide-leave-active,
.slide-enter-active {
  transition: all 0.5s ease;
}
.slide-enter,
.slide-leave-to {
  transform: translateY(-50%);
  opacity: 0;
}
</style>