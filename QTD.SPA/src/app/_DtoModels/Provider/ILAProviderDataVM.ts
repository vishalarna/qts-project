import { ILADetailsVM } from "@models/ILA/ILADetailsVM";

export class ILAProviderDataVM{
    providerId:string;
    providerName:string;
    providerActive:boolean;
    ilaDetails:ILADetailsVM[];
}