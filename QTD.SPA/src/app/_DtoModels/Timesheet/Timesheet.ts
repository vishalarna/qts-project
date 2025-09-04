import { Employee_Task } from '../EmployeeTask/Employee_Task';

export class Timesheet {
  employeeTaskId!: any;
  date!: Date | string;
  methodId!: any;
  note!: string;
  employee_Task!: Employee_Task;
}
