import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ILA } from 'src/app/_DtoModels/ILA/ILA';
import { ClassScheduleService } from 'src/app/_Services/QTD/class-schedule.service';
import { EmployeesService } from 'src/app/_Services/QTD/employees.service';
import { IlaService } from 'src/app/_Services/QTD/ila.service';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { catchError } from 'rxjs/operators';
import { scheduled, throwError } from 'rxjs';



@Component({
  selector: 'app-cbt-demo-page',
  templateUrl: './cbt-demo-page.component.html',
  styleUrls: ['./cbt-demo-page.component.scss']
})
export class CbtDemoPageComponent implements OnInit {

  public ila: any;
  public spinner: boolean;
  public scormAttachBeginMsg: string;
  public scormAttachSuccessMsg: string;
  public scormAttachErrorMsg: string;
  public scormDisconnectBeginMsg: string;
  public scormDisconnectSuccessMsg: string;
  public scormDisconnectErrorMsg: string;
  public employees: any;
  public cbtScormRegistrationLink;
  constructor(private _ilaService: IlaService, private _employeesService: EmployeesService,
    private classScheduleService: ClassScheduleService,
    private router: Router) { }

  ngOnInit(): void {
    this.spinner = true;
    this.getILADetailAsync();
    this.getEmployees();
  }

  public async getEmployees() {
    var employee = await this._employeesService.get(1)
    this.employees = [employee];
  }
  public async getILADetailAsync(): Promise<void> {
    this.ila = await this._ilaService.get(1);           //used hardcoded ILA id as 1 for testing purpose
    
    this.spinner = false;
  }

  public getAttachBeginEvent(event: any) {
    this.scormAttachBeginMsg = event;
  }
  public getAttachCourseSuccessEvent(event: any) {
    this.scormAttachSuccessMsg = JSON.stringify(event);
  }

  public getAttachCourseError(event: any) {
    this.scormAttachErrorMsg = event.message;
  }

  public getDisconnectBeginEvent(event: any) {
    this.scormDisconnectBeginMsg = event;
  }
  public getDisconnectSuccessEvent(event: any) {
    this.scormDisconnectSuccessMsg = JSON.stringify(event);
  }

  public getDisconnectError(event: any) {
    this.scormDisconnectErrorMsg = event.message;
  }

  public addToCbtClick(employeeId: number) {
    if (this.ila != null) {
      const cbtId = this.ila.cbTs.filter(item => item.active === true)[0];
      this._ilaService.RegisterEmployeeToCbtAsync(759, 89);
    }
  }

  public async startCourse(classScheduleId : any){
   await this.classScheduleService.getCBTScormRegistrationLink(classScheduleId).then((res) => {
     this.cbtScormRegistrationLink = res;
     window.open(this.cbtScormRegistrationLink.launchLink, '_blank');  
   });
  }

}
