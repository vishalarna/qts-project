import { Entity } from "../Entity";

export class ClientSettings_License_ProductInfo extends Entity {
  clientSettings_LicenseId: number;
  productName!: string;
  productAcronym!: string;
  version!: string;
  releaseDate!: string;
  enabled!: boolean;

  constructor(){
    super();
  }
}
