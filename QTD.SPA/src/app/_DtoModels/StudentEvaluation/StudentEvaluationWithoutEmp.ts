import { Employee } from "../Employee/Employee";
import { Entity } from "../Entity";
import { ClassSchedules } from "../SchedulesClassses/ClassSchedules";
import { StudentEvaluationQuestion } from "../StudentEvaluationQuestion/StudentEvaluationQuestion";
import { RatingScaleExpanded } from "./RatingScaleN";
import { StudentEvaluation } from "./StudentEvaluation";

export class StudentEvaluationWithoutEmp extends Entity{
  studentEvaluationId!:any;
  employeeId?:any;
  classScheduleId!:any;
  questionId!:any;
  dataMode!:string;
  ratingScale?:number;
  high!:number;
  average!:number;
  low!:number;
  notes!:string;
  additionalComments!:string;
  isCompleted!:string;
  studentEvaluation!:StudentEvaluation;
  classSchedule!:ClassSchedules;
  studentEvaluationQuestions!:StudentEvaluationQuestion;
  employee!:Employee;
  ratingScaleExpanded:RatingScaleExpanded;
}
