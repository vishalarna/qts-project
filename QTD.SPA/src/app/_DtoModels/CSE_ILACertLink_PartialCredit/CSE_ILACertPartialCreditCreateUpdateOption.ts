export class CSE_ILACertPartialCreditCreateUpdateOption{
    cSE_ILACertPartialCredits:CSE_ILACertPartialCredit[] = [];
}

export class CSE_ILACertPartialCredit{
    classScheduleEmployeeId:string;
    partialCreditHours?:number;
    subRequirements:CSE_ILACertSubRequirementPartialCredit[] = [];
}

export class CSE_ILACertSubRequirementPartialCredit{
    reqName:string;
    partialCreditHours?:number;
}