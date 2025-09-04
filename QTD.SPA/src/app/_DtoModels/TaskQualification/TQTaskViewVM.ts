import { MatLegacyTableDataSource as MatTableDataSource } from "@angular/material/legacy-table";
import { TaskQualsByTaskVM } from "./TaskQualsByTaskVM";

export class TQTaskViewVM {
  taskId!: string;
  letter!: string;
  number!:number;
  taskNumber!:string;
  taskDescription!: string;
  taskQualsByTaskVMs:TaskQualsByTaskVM[] | MatTableDataSource<TaskQualsByTaskVM>;
}