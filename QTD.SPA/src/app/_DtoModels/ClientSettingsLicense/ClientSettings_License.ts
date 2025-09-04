import {Entity} from "../Entity";
import {ClientSettings_License_ProductInfo} from "./ClientSettings_License_ProductInfo";

export class ClientSettings_License extends Entity {
  activationCode!: string;
  clientId!: string;
  expiration!: Date;
  licenseType!: string;
  totalEmployeeRecordsAvailable!: number;
  employeeRecordsUsed!: number
  products: ClientSettings_License_ProductInfo[];

  constructor() {
    super();
  }
}
