import { CBT_ScormRegistration } from "@models/CBT/CBT_ScormRegistration";
export class ScormUpload{
    id:number;
    cbtId:string;
    name:string;
    scormStatus:string;
    scormPackageId:number;
    connectedDate:Date;
    disconnectedDate:Date;
    active: boolean;
    cbT_ScormRegistration:CBT_ScormRegistration[];
}