import { Entity } from './../Entity';



export class Training extends Entity {
    providerID: number
    ilaid: number
    startDateTime: string
    endDateTime: string
    instructorId: number
    locationId: number
    specialInstructions: string
    webinarLink: string
}
