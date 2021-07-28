import BaseAPI from "../../base/BaseAPI";

class EmployeesAPI extends BaseAPI {
    constructor() {
        super();

        this.controller = "api/v1/Departments";
    }

}

export default new EmployeesAPI();