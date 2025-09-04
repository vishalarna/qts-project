export interface CreateProcedureReview {
  procedureId: number
  procedureReviewTitle: string
  startDateTime: any
  endDateTime: any
  procedureReviewInstructions: string
  isEmployeeShowResponses: boolean
  procedureReviewAcknowledgement: string
}

export class ProcedureReviewDeleteOptions {
  actionType: string
  procedurereviewIds: number[]
}

export class AddEmployeeToProcedureReviewCreationOptions {
  procedureReviewId: string;
  employeeIds: any[] = [];
}

export class procedureReviewEmpUpdateOptions {
  response: string;
  comments: string;
  procedureReviewId: string;
}

export class procedureReviewEmpExitoptions {
  response: string|null;
  comments!: string;
  procedureReviewId: string;
}
