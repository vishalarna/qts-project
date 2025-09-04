export class ProcedureCreateOptions {
  issuingAuthorityId!: any;
  title!: string;
  number!: string;
  description!: string;
  revisionNumber!: string;
  effectiveDate!: Date;
  proceduresFileUpload!: string;
  isActive!: boolean;
  isDeleted!: boolean;
  isPublished!: boolean;
  changeNotes?:string;
  hyperlink!: string;
  uploads!:any[];
  file!:string;
  fileName!:string;
}
