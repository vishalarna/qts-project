import { TrainingIssue_ActionItem_VM } from "./TrainingIssue_ActionItem_VM";
import { TrainingIssue_DataElement_VM } from "./TrainingIssue_DataElement_VM";

export class TrainingIssue_VM{
    id: string;
    issueCode: string;
    issueTitle: string;
    description: string;
    createdDate: Date;
    dueDate: Date;
    statusId: string;
    severityId: string;
    driverTypeId: string;
    driverSubTypeId: string;
    otherComments: string;
    dataElement: TrainingIssue_DataElement_VM = new TrainingIssue_DataElement_VM();
    actionItems: TrainingIssue_ActionItem_VM[] = [];
    plannedResponse: string;
    status:string;
    severityLevel:string;
    driverType:string;
    driverSubType:string;
    taskReviewId:string;
}