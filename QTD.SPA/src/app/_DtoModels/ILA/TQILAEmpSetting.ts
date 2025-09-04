import { EmpSettingsReleaseType } from "@models/EmpSettingsReleaseType/EmpSettingsReleaseType";
import { ILA } from "./ILA";
import { Entity } from "@models/Entity";

export class TQILAEmpSetting extends Entity{
    ilaId: string;
    tqRequired: boolean;
    releaseAtOnce: boolean;
    releaseOneAtTime: boolean;
    releaseOnClassStart: boolean;
    releaseOnClassEnd: boolean;
    specificTime?: number | null;
    priorToSpecificTime: boolean;
    oneSignOffRequired: boolean;
    multipleSignOffRequired?: number | null;
    tqDueDate: number;
    empSettingsReleaseTypeId?: number | null;
    ila?: ILA | null;
    empSettingsReleaseType?: EmpSettingsReleaseType | null;
    showTaskSuggestions:boolean;
    showTaskQuestions:boolean;
}
