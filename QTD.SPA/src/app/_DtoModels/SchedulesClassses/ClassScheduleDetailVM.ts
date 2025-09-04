export class ClassScheduleDetailVM{
  id: string;
  ilaId: string;
  instructorId: string;
  locationId: string;
  providerId: string;
  startDateTime: Date;
  endDateTime: Date;
  locationName: string;
  instructorName: string;
  ilaNumber: string;
  ilaName: string;
  isILASelfPaced: boolean | null;
  locationAddress: string;
  useForEmp: boolean | null;
  isStartAndEndTimeEmpty: boolean;
  classSize: number | null;
  isRecurring: boolean;
  specialInstruction: string;
  webinarLink: string;
  providerName: string;
  isPubliclyAvailable: boolean;
}