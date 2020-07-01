import { Employee } from "./";

export class EmployeeInfo extends  Employee {
  userName: string;
  type: string;
  basicRate: number;
  employmentDate: Date;
  dependants: Employee[];
}
