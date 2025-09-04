import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from "@angular/router";
import { DeliveryMethod } from "../_DtoModels/DeliveryMethod/DeliveryMethod";
import { DeliveryMethodeService } from "../_Services/QTD/delivery-methode.service";

@Injectable({
  providedIn:'root'
})
export class DeliveryMethodsResolverService implements Resolve<DeliveryMethod[]>{

  constructor(private deliveryMethodeService:DeliveryMethodeService){

  }

  async resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot):Promise<DeliveryMethod[]> {
    var deliveryMethods:DeliveryMethod[] = [];
    await this.deliveryMethodeService.getAll().then((res)=>{
      deliveryMethods = res;
    }).catch((err)=>{
      console.error(err);
    })
    return deliveryMethods;
  }
}
