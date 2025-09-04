import { Entity } from "../Entity";

export class Location extends Entity {
    locCategoryId!: any;
    locNumber!: number;
    locName!: string;
    locDescription:string;
    locAddress:string;
    locCity: string;
    locState:string;
    locZipCode: string;
    locPhone: string;
    Notes: string;
    effectiveDate:any;
    classSchedules:any;
    }