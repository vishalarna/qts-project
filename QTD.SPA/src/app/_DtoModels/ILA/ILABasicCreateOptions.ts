import { CEHUpdateOptions } from "./CEHUpdationOptions";

export class ILABasicCreateOptions{
    providerId!: string;
    name!: string;
    number!: string;
    totalHours?: number;
    deliveryMethodId?: any;
    isSelfPacedILA?: boolean;
    isPublished?: boolean;
    cehUpdates?:CEHUpdateOptions[];
}