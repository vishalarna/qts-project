import { Injectable } from '@angular/core';
import {
  Router,
  Resolve,
  RouterStateSnapshot,
  ActivatedRouteSnapshot,
} from '@angular/router';
import { Observable, of } from 'rxjs';
import { DutyArea } from '../_DtoModels/DutyArea/DutyArea';
import { DutyAreaService } from '../_Services/QTD/duty-area.service';

@Injectable({
  providedIn: 'root',
})
export class JTAMenuResolver implements Resolve<DutyArea[]> {
  /**
   *
   */
  constructor(private _DutyAreaService: DutyAreaService) {}
  async resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Promise<DutyArea[]> {
    
    let listDA: DutyArea[] = [];
    await this._DutyAreaService
      .getAll()
      .then((res) => {
        listDA = res;
      });
    return listDA;
  }
}
