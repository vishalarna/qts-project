import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { LinkR5UpdateTasksModel } from 'src/app/_DtoModels/Position_Task/LinkR5UpdateTasksModel';
import { map } from 'rxjs/operators';
import { UpdateMarkAsR6Model } from 'src/app/_DtoModels/Position_Task/UpdateMarkAsR6Model';
import { UpdateUnmarkAsR6Model } from 'src/app/_DtoModels/Position_Task/UpdateUnmarkAsR6Model';
import { DeleteR5TaskModel } from 'src/app/_DtoModels/Position_Task/DeleteR5TaskModel';
import { firstValueFrom } from 'rxjs';
@Injectable({
  providedIn: 'root',
})
export class PositionTaskService {
  baseUrl = environment.QTD + 'positionTasks';
  constructor(private http: HttpClient) {}

  updateR5TasksAsync(positionTaskId: string, options: LinkR5UpdateTasksModel) {
    return firstValueFrom(this.http
      .put(this.baseUrl + `/${positionTaskId}/updateR5Tasks`, options)
      .pipe(
        map((res: any) => {
          return res;
        })
      )
      );
  }

  updateUnmarkAsR6Async(positionTaskId: string, options: UpdateUnmarkAsR6Model) {
    return firstValueFrom(this.http
      .put(this.baseUrl + `/${positionTaskId}/unmarkAsR6`, options)
      .pipe(
        map((res: any) => {
          return res;
        })
      )
      );
  }

  updateMarkAsR6Async(options: UpdateMarkAsR6Model) {
    return firstValueFrom(this.http
      .put(this.baseUrl + `/markAsR6`, options)
      .pipe(
        map((res: any) => {
          return res;
        })
      )
      );
  }

  deleteR5TaskAsync(positionTaskId: string, r5ImpactedTaskId: string, options: DeleteR5TaskModel) {
    return firstValueFrom(this.http
      .delete(
        this.baseUrl + `/${positionTaskId}/deleteR5Task/${r5ImpactedTaskId}`, {body:options})
      .pipe(
        map((res: any) => {
          return res;
        })
      )
      );
  }

  getPositionTaskByPositionIdAsync(positionId:string) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${positionId}`)
      .pipe(
        map((res: any) => {
           return res as any;
        })
      )
      );
  }
}
