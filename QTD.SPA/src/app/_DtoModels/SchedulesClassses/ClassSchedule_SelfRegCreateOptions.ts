export interface ClassSchedule_SelfRegCreateOptions {
    classScheduleId: string;
    makeAvailableForSelfReg: boolean;
    requireAdminApproval: boolean;
    acknowledgeRegDisclaimer: boolean;
    regDisclaimer: string;
    limitForLinkedPositions: boolean;
    closeRegOnStartDate: boolean;
    classSize: number;
    enableWaitlist: boolean;
    sendApprovedEmail:boolean;
    updateRecurrences:boolean;
}