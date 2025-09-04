import { ScormUpload } from "../Scorm/ScormUpload";
import { CBTAvailablity } from "./CBTAvailablity";
import { CBTDueDateInterval } from "./CBTDueDateInterval";

export class CBT {
    id:number;
    active:boolean;
    deleted:boolean;
    createdBy:string;
    createdDate:Date;
    ilaId: number;
    scormUploads: ScormUpload[];
    availablity: CBTAvailablity;
    cbtLearningContractInstructions: string;
    dueDateAmount: number;
    dueDateInterval: CBTDueDateInterval;
    empSettingsReleaseTypeId! :string;
    modifiedBy:string;
    modifiedDate:Date;
}
