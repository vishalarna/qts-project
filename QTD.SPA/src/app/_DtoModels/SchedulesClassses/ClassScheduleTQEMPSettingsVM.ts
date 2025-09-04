
export class ClassScheduleTQEMPSettingsVM  {
    id : string ;
    classScheduleId: string;
    tqRequired: boolean;
    releaseOnClassStart: boolean;
    releaseOnClassEnd: boolean;
    specificTime?: number;
    priorToSpecificTime: boolean;
    showTaskSuggestions:boolean;
    showTaskQuestions:boolean;
}
