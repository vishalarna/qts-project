export class ClassScheduleTQEMPSettingsCreateOptions {
    classScheduleId: string;
    tqRequired: boolean;
    releaseOnClassStart: boolean;
    releaseOnClassEnd: boolean;
    specificTime?: number;
    priorToSpecificTime: boolean;
    showTaskSuggestions:boolean;
    showTaskQuestions:boolean;
}
export class ClassScheduleEvaluatorLinksVM {
    classScheduleId: string;
    evaluatorIds: string[];
  }
export class TrainingCreationOptions  {
    providerID: any;
    ilaid: any;
    startDateTime: any;
    endDateTime: any;
    instructorId: any;
    locationId: any;
    specialInstructions: string;
    webinarLink: string;
    recurringOptions:any[];
    notes?:string;
    classSize?:number;
    isStartAndEndTimeEmpty:boolean;
    isPubliclyAvailable:boolean;
    
}

export interface TrainingEnrollStudentCreationOptions {
    ilaId: number;
    makeAvailableForSelfReg: boolean;
    requireAdminApproval: boolean;
    acknowledgeRegDisclaimer: boolean;
    regDisclaimer: string;
    limitForLinkedPositions: boolean;
    closeRegOnStartDate: boolean;
    classSize: number;
    enableWaitlist: boolean;
    sendApprovedEmail:boolean;
}

export class TrainingStudentCreationOptions {
    classScheduleId: string;
    employeeIds: any[] = [];
}


export interface EMPSettingTestReleaseCreationOptions {
    ilaId: any;
    finalTestId: number;
    preTestId: number;
    usePreTestAndTest: boolean;
    preTestRequired: boolean;
    preTestAvailableOnEnrollment: boolean;
    preTestAvailableOneStartDate: boolean;
    showStudentSubmittedPreTestAnswers: boolean;
    showCorrectIncorrectPreTestAnswers: boolean;
    makeAvailableBeforeDays: number;
    finalTestPassingScore: string;
    makeFinalTestAvailableImmediatelyAfterStartDate: boolean;
    makeFinalTestAvailableOnClassEndDate: boolean;
    makeFinalTestAvailableAfterCBTCompleted: boolean;
    makeFinalTestAvailableOnSpecificTime: number;
    finalTestSpecificTimePrior: boolean;
    finalTestDueDate: number;
    empSettingsReleaseTypeId : string;
    showStudentSubmittedFinalTestAnswers: boolean;
    showStudentSubmittedRetakeTestAnswers: boolean;
    showCorrectIncorrectFinalTestAnswers: boolean;
    showCorrectIncorrectRetakeTestAnswers: boolean;
    autoReleaseRetake: boolean;
    preTestScore:number;
    retakeEnabled: boolean;
    makeAvailableBeforeWeeks?: number;
    daysOrWeeks?: number;
    numberOfRetakes: number;
    retakesTestIds: number[];
}


export interface EMPSettingCBTCreationOptions {
    ilaId: any;
    cbtRequiredForCource: boolean;
    releaseCBTLearningContract: boolean;
    cbtLearningContractInstructions: string;
    makeAvailableOnClassStartDate: boolean;
    makeAvailableOnClassEndDate: boolean;
    makeAvailableAfterPretestCompleted: boolean;
    cbtDueDate: number;
}


export interface EMPSettingEvaluationCreationOptions {
    ilaId: any;
    evaluationRequired: boolean;
    evaluationUsedToDeployStudentEvaluation:boolean;
    evaluationAvailableOnStartDate: boolean;
    evaluationAvailableOnEndDate: boolean;
    finalGradeRequired: boolean;
    releaseOnSpecificTimeAfterClassEndDate: boolean;
    releaseAfterEndTime: number;
    releasePrior: boolean;
    releaseAfterGradeAssigned: boolean;
    evaluationDueDate: number;
    empSettingsReleaseTypeId: string;
}

export interface EMPSettingTQCreationOptions {
    ilaId: number;
    tqRequired: boolean;
    releaseAtOnce: boolean;
    releaseOneAtTime: boolean;
    releaseOnClassStart: boolean;
    releaseOnClassEnd: boolean;
    specificTime?: number;
    priorToSpecificTime: boolean;
    oneSignOffRequired: boolean;
    multipleSignOffRequired?: number;
    tqDueDate?: number;
    empSettingsReleaseTypeId?: string;
    showTaskSuggestions:boolean;
    showTaskQuestions:boolean;
}

export interface EMPSettingStudentEval {
    ilaId: number;
    studentEvalFormIDs: number[];
    studentEvalAvailabilityID: number | null;
    studentEvalAudienceID: number |null;
    isAllQuestionMandatory: boolean |null;
}

export interface EMPSettingsTQTaskEvaluation {
    ilaId: string;
    evaluatorIds: string[];
}



