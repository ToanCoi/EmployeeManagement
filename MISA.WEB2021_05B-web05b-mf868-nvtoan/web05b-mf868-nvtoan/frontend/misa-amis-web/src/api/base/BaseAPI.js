import BaseAPIConfig from "./BaseAPIConfig";

export default class BaseAPI {
    constructor() {
        this.controller = null;
    }

    /**
     * Hàm lấy tất cả dữ liệu
     * CreatedBy: NVTOAN 05/07/2021
     */
    getAll() {
        return BaseAPIConfig.get(`${this.controller}`);
    }

    /**
     * Hàm lấy bản ghi theo Id
     * CreatedBy: NVTOAN 05/07/2021
     * @param {*} id 
     */
    getById(id) {
        return BaseAPIConfig.get(`${this.controller}/${id}`)
    }

    /**
     * Hàm thêm mới dữ liệu
     * CreatedBy: NVTOAN 05/07/2021
     * @param {*} data 
     * @returns 
     */
    insert(data) {
        return BaseAPIConfig.post(`${this.controller}`, data);
    }

    /**
     * Hàm sửa dữ liệu
     * CreatedBy: NVTOAN 05/07/2021
     * @param {*} id 
     * @param {*} data 
     * @returns 
     */
    update(id, data) {
        return BaseAPIConfig.put(`${this.controller}/${id}`, data);
    }

    /**
     * Hàm xóa dữ liệu
     * CreatedBy: NVTOAN 05/07/2021
     * @param {*} id 
     * @returns 
     */
    delete(id) {
        return BaseAPIConfig.delete(`${this.controller}/${id}`);
    }
    
    /**
     * Hàm xóa nhiều bản ghi
     * @param {array} listId Danh sách Id bản ghi cần xóa
     * @returns Trạng thái xóa các bản ghi
     */
    multipleDelete(listId) {
        return BaseAPIConfig.delete(`${this.controller}`, {data: listId});
    }
}