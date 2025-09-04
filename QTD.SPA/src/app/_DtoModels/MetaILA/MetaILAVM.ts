import { MetaILAEmployeeVM } from "@models/MetaILAEmployeesLink/MetaILAEmployeeVM";
import { MetaILA_ILAMemberVM } from "@models/MetaILAMembersLink/MetaILA_ILAMemberVM";

export class MetaILAVM{
    id: string;
    metaILA_SummaryTest_FinalTestId: string | null;
    metaILA_SummaryTest_RetakeTestId: string | null;
    studentEvaluationId: string | null;
    metaILAStatusId: string;
    name: string;
    description: string;
    reason: string;
    active: boolean;
    isDeleteAllowed: boolean;
    isReleasedToEmployees: boolean;
    metaIlaCount: number | null;
    effectiveDate: Date | null;
    metaILA_EmployeeVM:MetaILAEmployeeVM[];
    metaILA_ILAMemberVM:MetaILA_ILAMemberVM[];
    providerId:string;
}