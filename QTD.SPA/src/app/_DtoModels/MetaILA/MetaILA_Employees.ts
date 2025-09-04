import { Employee } from "../Employee/Employee";
import { Entity } from "../Entity";
import { MetaILA } from "./MetaILA";

export class MetaILA_Employees extends Entity {
   id:string;
   metaILAId:string;
   employeeId:string;
   metaILA:MetaILA;
   employee:Employee;
  }
  