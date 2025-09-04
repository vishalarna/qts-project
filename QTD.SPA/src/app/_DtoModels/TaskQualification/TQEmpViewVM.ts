import { MatLegacyTableDataSource as MatTableDataSource } from "@angular/material/legacy-table";
import { TaskQualsByEmpVM } from "./TaskQualsByEmpVM";

export class TQEmpViewVM {
  empId!: string;
  empFName!: string;
  empLName!: string;
  empImage!: string;
  empEmail!: string;
  taskQualsByEmpVMs!: MatTableDataSource<TaskQualsByEmpVM>;
  positions!: string;
  employeeNumber!: string;
  isLocked!: boolean;
}