import TimeInterval from "./TimeInterval";

class EmployeeDTO {
    id: string = '';
    FullName: string = '';
    CPF: string = '';
    Email: string = '';
    PhoneNumber: string = '';
    shift : TimeInterval = new TimeInterval();
    salary : number = 2000;
    appointmentsDone : number = 0;
}
  
export default EmployeeDTO;