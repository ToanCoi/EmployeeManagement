import Resource from "./Resource"
import Enumeration from "./Enumeration"
import moment from "moment"

var CommonFn = CommonFn || {};

/**
 * hàm format số nguyên
 * NVTOAN 06/07/2021
 */
 CommonFn.formatNumber = number => {
    if(number && !isNaN(number)) {
        return number.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
    } else {
        return number;
    }
 }

 /**
  * Format ngày tháng từ dữ liệu thô
  * NVTOAN 06/07/2021
  * @param {*} dateSrc 
  * @returns 
  */
 CommonFn.formatDate = dateSrc => {
     if(dateSrc)
        dateSrc =  moment(dateSrc).format("DD/MM/YYYY");

    return dateSrc;
 }

 /**
  * Format tên 
  * NVTOAN 06/07/2021
  * @param {string} name 
  * @returns 
  */
 CommonFn.formatName = name => {
     let fullName = name.split(' '),
         res = '';

     fullName.map(item => {
        item = item.charAt(0).toUpperCase() + item.slice(1);
        res += item + ' ';
     });

     return res.trim();
 }

 /**
  * Hàm lấy dữ liệu từ dang Enum
  * NVTOAN 06/07/2021
  */
  CommonFn.getEnumValue = (data, enumName) => {
    let enumeration = Enumeration[enumName],
        resource  = Resource[enumName];

    for(let propName in enumeration) {
        if(enumeration[propName] == data) {
            data = resource[propName];
        }
    }

    return data;
}

 /**
 * Hàm chuyển đổi dữ liệu để hiển thị lên bảng
 * NVTOAN 06/07/2021
 */
  CommonFn.convertOriginData = (data, dataType, enumName) => {
    let temp = ''; 
    
    if(data != null) {
        temp = data;
    
        switch(dataType) {
            case Resource.DataTypeColumn.Number:
                temp = CommonFn.formatNumber(temp);
                break;
            case Resource.DataTypeColumn.Name:
                temp = CommonFn.formatName(temp);
                break;
            case Resource.DataTypeColumn.Date:
                temp = CommonFn.formatDate(temp);
                break;
            case Resource.DataTypeColumn.Enum:
                temp = CommonFn.getEnumValue(temp, enumName);
                break;
        }
    }
    
    return temp;
}

/**
 * Hàm tạo lỗi bắt buộc nhập
 * @param {string} fieldName Tên trường bị lỗi
 * @returns Thông điệp lỗi bắt buộc nhập
 * NVTOAN 09/07/2021
 */
CommonFn.getRequiredErrorMsg = (fieldName) => {
    return fieldName + ' không được để trống';
}

/**
 * Hàm tạo thông điệp lỗi bị trùng
 * @param {string} value Tên trường cùng giá trị bị trùng
 * @returns Thông điệp lỗi tồn tại trong hệ thống
 * NVTOAN 09/07/2021
 */
CommonFn.getUniqueErrorMsg = (value) => {
    return value + ' đã tồn tại trong hệ thống, vui lòng kiểm tra lại';
}

CommonFn.getMaxLengthErrorMsg = (fieldName, maxLength) => {
    return fieldName + ' không được vượt quá ' + maxLength + ' ký tự';
}

/**
 * Hàm tạo thông điệp lỗi kiểu dữ liệu
 * @param {string} fieldName Tên trường dữ liệu bị lỗi
 * @returns Thông điệp lỗi
 * NVTOAN 09/07/2021
 */
CommonFn.getDataTypeErrorMsg = (fieldName) => {
    return fieldName + ' không hợp lệ';
}

export default CommonFn;