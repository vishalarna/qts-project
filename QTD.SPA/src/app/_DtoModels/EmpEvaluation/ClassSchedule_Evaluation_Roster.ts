import { Employee } from "../Employee/Employee";
import { Entity } from "../Entity";
import { ClassSchedules } from "../SchedulesClassses/ClassSchedules";
import { StudentEvaluation } from "../StudentEvaluation/StudentEvaluation";

export class ClassSchedule_Evaluation_Roster extends Entity{
  classScheduleInfo!:ClassSchedules;
  employee!:Employee;
  studentEvaluationInfo!:StudentEvaluation;
  releaseDate!:any;
  completedDate!:any;
  classScheduleId!:any;
  employeeId!:any;
  isCompleted!:any;
  isStarted!:any;
  isAllowed!:any;
}
