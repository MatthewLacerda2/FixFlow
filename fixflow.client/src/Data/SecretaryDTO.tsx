import TimeInterval from "./TimeInterval";

interface EmployeeDTO {
    id: string;
    FullName: string;
    CPF: string;    
    Email: string;
    PhoneNumber: string;
    shift : TimeInterval;
    salary : number;
}
  
export default EmployeeDTO;