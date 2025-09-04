export class ILANercStandard_LinkOptions {
  iLAId:any;
  stdId:any;
  nERCStdMemberId:any;
  creditHoursByStd:number;
  cehHours?:number;
  nercStdValues : NercStdValues[];


}

export class  NercStdValues {
  nERCStdMemberId:any;
  creditHoursByStd:number;
}
