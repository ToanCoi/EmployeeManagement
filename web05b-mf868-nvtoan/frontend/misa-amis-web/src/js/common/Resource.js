var Resource = Resource || {};

//Kiểu dữ liệu của cột trong grid
Resource.DataTypeColumn = {
    Number: "Number",
    Date: "Date",
    Enum: "Enum",
    Email: "Email",
    Name: "Name",
    DateForm: "DateForm",
    OrderNumber: "OrderNumber"
}

//Giới tính
Resource.Gender = {
    Male: "Nam",
    Female: "Nữ",
    Other: "Khác"
}

//Trạng thái làm việc
Resource.WorkStatus = {
    Active: "Đang làm việc",
    Intern: "Đang thử việc",
    Retired: "Đã nghỉ việc"
}

//Các chế độ của form
Resource.FormType = {
    Add: "Add",
    Edit: "Edit",
    Delete: "Delete",
    Refresh: "Refresh"
}

//Tin nhắn trả về người dùng
Resource.Message = {
    AddSuccess: "Thêm dữ liệu thành công",
    EditSuccess: "Cập nhật dữ liệu thành công",
    DeleteSuccess: "Xoá dữ liệu thành công",
    ServerError: "Có lỗi xảy ra, vui lòng liên hệ MISA",
    GetDepartmentError: "Có lỗi khi lấy phòng ban, vui lòng liên hệ MISA",
    GetNewEmployeeCodeError: "Không thể lấy mã nhân viên mới, vui lòng liên hệ MISA"
}

//Các chế độ toast message
Resource.Toast = {
    Success: "success",
    Warning: "warning",
    Error: "error"
}

export default Resource;