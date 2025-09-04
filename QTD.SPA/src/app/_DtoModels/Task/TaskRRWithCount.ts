import { RRIssuingAuthority } from "@models/RR_IssuingAuthority/RR_IssuingAuthority";

export class TaskRRWithCount {
  number: string;

  description: string;

  id: number;

  linkCount: number;

  active: boolean;
  rR_IssuingAuthority_Title?:string;
}
