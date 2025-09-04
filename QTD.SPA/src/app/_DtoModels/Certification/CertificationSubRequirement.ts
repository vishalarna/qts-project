import { Certification } from "./Certification";

export class CertificationSubRequirement {
    id :string;
    certificationId: string;
    reqName: string;
    reqHour: number;
    certification: Certification; // Assuming Certification is another TypeScript class
}

