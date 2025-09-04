import { Employee } from '../Employee/Employee';
import { Timesheet } from '../Timesheet/Timesheet';

export class Employee_Task {
  employeeId!: any;
  taskId!: any;
  majorVersion!: number;
  minorVersion!: number;
  archived!: boolean;
  employee!: Employee;
  task!: Task;
  timesheets!: Timesheet[];
}
