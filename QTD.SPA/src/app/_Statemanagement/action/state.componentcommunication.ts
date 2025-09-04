import { createAction, props } from "@ngrx/store";
import { EmpEvaluationVM } from "src/app/_DtoModels/EmpEvaluation/EmpEvaluationVM";

export const saveILA = createAction("[SAVE ILA] SaveIla",props<{ saveData? : any,tabIndex?:any,update?:boolean }>());
export const deleteILA = createAction("[DELETE ILA] DeleteIla",props<{tabIndex?:any}>());

export const saveTraining = createAction("[SAVE Training] SaveTraining",props<{ saveData? : any,tabIndex?:any,update?:boolean }>());
export const deleteTraining = createAction("[DELETE Training] DeleteTraining",props<{tabIndex?:any}>());

export const evalationInformation = createAction("[SAVE EMPEVAL] evalationInformation",props<{ evalData?: EmpEvaluationVM}>());
export const getTestInfo = createAction("[GET TestInfo] GetTestInfo",props<{ saveData? : any,tabIndex?:any,update?:boolean }>());

export const navigateTQ = createAction("[NAVIGATE TQ] NavigateTQ",props<{shouldNavigate?:boolean,filterOptions?:any}>());
