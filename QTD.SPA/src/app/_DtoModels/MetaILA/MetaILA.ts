import { MetaILA_Employees } from "./MetaILA_Employees";
import { Meta_ILAMembers_Link } from "./Meta_ILAMembers_Link";

export class MetaILA {
  id: string;
  name: string;
  description: string;
  metaILAStatusId: any;
  reason: string;
  effectiveDate: Date;
  isPriority!: boolean;
  active: boolean;
  metaILA_SummaryTest_FinalTestId?: string;
  metaILA_SummaryTest_RetakeTestId?: string;
  studentEvaluationId?: string;
  meta_ILAMembers_Links: Meta_ILAMembers_Link[];
  metaILA_Employees : MetaILA_Employees[];
}
