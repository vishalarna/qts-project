import { BreakpointObserver } from "@angular/cdk/layout";
import { StepperOrientation } from "@angular/cdk/stepper";
import { DatePipe, formatDate } from "@angular/common";
import { AfterViewInit, Component, EventEmitter, Input, OnDestroy, OnInit, Output, ViewChild } from "@angular/core";
import { UntypedFormGroup, UntypedFormControl, UntypedFormBuilder, Validators } from "@angular/forms";
import { MatStepper } from "@angular/material/stepper";
import { ActivatedRoute } from "@angular/router";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { Task } from 'src/app/_DtoModels/Task/Task';
import { DutyArea } from "src/app/_DtoModels/DutyArea/DutyArea";
import { SubdutyArea } from "src/app/_DtoModels/SubdutyArea/SubdutyArea";
import { TaskCreateOptions } from "src/app/_DtoModels/Task/TaskCreateOptions";
import { TaskNumberVM } from "src/app/_DtoModels/Task/TaskNumberVM";
import { TaskOptions } from "src/app/_DtoModels/Task/TaskOptions";
import { TaskUpdateOptions } from "src/app/_DtoModels/Task/TaskUpdateOptions";
import { DutyAreaService } from "src/app/_Services/QTD/duty-area.service";
import { PositionsService } from "src/app/_Services/QTD/positions.service";
import { ProceduresService } from "src/app/_Services/QTD/procedures.service";
import { TasksService } from "src/app/_Services/QTD/tasks.service";
import { DataBroadcastService } from "src/app/_Shared/services/DataBroadcast.service";
import { SweetAlertService } from "src/app/_Shared/services/sweetalert.service";
import { SubSink } from "subsink";
import { Position } from 'src/app/_DtoModels/Position/Position';
import { EmpCertificateUpdateOptions } from "src/app/_DtoModels/Employee/EmpCertificateUpdateOptions";
import { EmployeesService } from "src/app/_Services/QTD/employees.service";
import { CertificationService } from "src/app/_Services/QTD/certification.service";

@Component({
  selector: 'app-fly-panel-renew-certification',
  templateUrl: './fly-panel-renew-certification.component.html',
  styleUrls: ['./fly-panel-renew-certification.component.scss']
})
export class FlyPanelRenewCertificationComponent implements OnInit, OnDestroy, AfterViewInit
{
  @Output() closed = new EventEmitter<any>();
  @Output() refresh = new EventEmitter<any>();
  @Input() oldTask: Task;
  @Input() isCopy: boolean = false;
  @ViewChild('stepper') stepper:MatStepper;
  @Input() empId: any;
  @Input() certId: any;
  @Input() empCertId:any;
  empCertObj:any;
certObj:any;
  taskNumber = new TaskNumberVM();
  showSpinner: boolean = false;
  stepperOrientation: Observable<StepperOrientation>;
  addTask: boolean = true;
  addDutyArea: boolean = false;
  addSubdutyArea: boolean = false;
  procId = '';
  newDutyAreaNumber: any;
  dutyAreas: DutyArea[] = [];
  subDutyAreas: SubdutyArea[] = [];
  positions: Position[] = [];
  positionList: any[] = [];
  step1Form: UntypedFormGroup;
  step2Form: UntypedFormGroup;
  step3Form: UntypedFormGroup;
  datePipe = new DatePipe('en-us');
  dateError = false;
  sdaPlaceholder = 'Select Sub Dutyarea';
  symbol = '';
  disableDate:boolean=true;
  subscription = new SubSink();
  positionsControl = new UntypedFormControl([]);
  Renewdate:Date;
  initialExpirationDate:any;
  renewalTimeFrame:any;
  constructor(
    private daSrvc: DutyAreaService,
    private fb: UntypedFormBuilder,
    private positionService: PositionsService,
    public breakpointObserver: BreakpointObserver,
    public taskService: TasksService,
    public alert: SweetAlertService,
    public route: ActivatedRoute,
    public procService: ProceduresService,
    public dataBroadcastService: DataBroadcastService,
    private employeeService: EmployeesService,
    private certificationservice:CertificationService
  ) {
    this.stepperOrientation = breakpointObserver
      .observe('(min-width: 800px)')
      .pipe(map(({ matches }) => (matches ? 'horizontal' : 'vertical')));
  }

  ngOnInit(): void {
    this.readyStep1Form();
    this.readyStep2Form();
    this.readyStep3Form();
    if(this.empCertId != undefined)
    {
      this.employeeService.getEmployeeCertification(this.empCertId) .then((res: any) => {
        this.empCertObj =res;

        this.readyRenewCertificateWithData();
      });
    }
    if(this.certId != undefined)
    {
      this.certificationservice.get(this.certId).then((res: any) => {
        this.certObj =res;
        this.renewalTimeFrame = res.renewalInterval;

      });
    }
  }

  ngAfterViewInit(): void {
    this.subscription.sink = this.route.params.subscribe((res: any) => {
      this.procId = res.id;
    });
  }

  readyRenewCertificateWithData(){

    this.step1Form.patchValue({
      certDate: this.datePipe.transform(this.empCertObj.issueDate, "yyyy-MM-dd"),
      renewDate: this.datePipe.transform(this.empCertObj.renewalDate, "yyyy-MM-dd"),
      exptDate: this.datePipe.transform(this.empCertObj.expirationDate, "yyyy-MM-dd")
    });

    this.step2Form.patchValue({
      rolloverhours : this.empCertObj.rollOverHours
    })
    /* .get('certDate')?.setValue(this.datePipe.transform(this.empCertObj.issueDate, "yyyy-MM-dd")) */
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
 
  selectedChanged(event: any) {

  }

  readyStep1Form() {
    this.step1Form = this.fb.group({
      certDate: new UntypedFormControl(this.datePipe.transform(Date.now(), "yyyy-MM-dd")),
      renewDate: new UntypedFormControl(this.datePipe.transform(Date.now(), "yyyy-MM-dd")),
      exptDate: new UntypedFormControl(this.datePipe.transform(Date.now(), "yyyy-MM-dd")),
    });
  }

  readyStep2Form() {
    this.step2Form = this.fb.group({
      rolloverhours: new UntypedFormControl('', [Validators.required, this.decimalValidator()])
    });
  }

  decimalValidator() {
    return (control: UntypedFormControl) => {
      const value = control.value;
      if (!value || isNaN(value)) {
        return { invalidDecimal: true };
      }
      const regex = /^[+-]?\d+(\.\d+)?$/;
      if (!regex.test(value)) {
        return { invalidDecimal: true };
      }
      return null;
    };
  }

  readyStep3Form() {
    this.step3Form = this.fb.group({
      effectiveDate: new UntypedFormControl(
        this.datePipe.transform(Date.now(), 'yyyy-MM-dd')
      ),
      reason: new UntypedFormControl('')
    });
  }

/*   dateChanged(event: any) {
    if (event >= formatDate(Date.now(), 'yyyy-MM-dd', 'en')) {
      this.dateError = false;
    } else {
      this.dateError = true;
    }
  }
  renewdateChanged(event: any)
  {
    if (event >= formatDate(Date.now(), 'yyyy-MM-dd', 'en')) {
      this.dateError = false;
    } else {
      this.dateError = true;
    }
  } */


  renewCertificate() {

    var options = new EmpCertificateUpdateOptions();
    options.issueDate = this.step1Form.get('certDate')?.value;
    options.renewalDate = this.step1Form.get('renewDate')?.value;
    options.expirationDate = this.step1Form.get('exptDate')?.value;
    options.reason = this.step3Form.get('reason')?.value;
    options.effectedDate = this.step3Form.get('effectiveDate')?.value;
    options.rolloverHours = this.step2Form.get('rolloverhours')?.value;

    this.employeeService.renewcertificate(this.empCertId,options)
      .then((res: any) => {
        this.alert.successToast('Certificate Renewed Successfully');
        this.refresh.emit();
        this.closed.emit();
      });
  }

  selectRenewDateIncrease(y:any,m:any,d:any,date:any){
    let getExpDate = date
    let [year,month, day,] = getExpDate.split('-');
    let date1 = new Date(Number(y)+Number(year), Number(m) + Number(month-1), Number(d) + Number(day));
    this.step1Form.get("exptDate").setValue(this.datePipe.transform(date1,"yyyy-MM-dd"));
}

/* selectRenewDateDecrease(y:any,m:any,d:any){
  let getExpDate = this.step1Form.get("exptDate")?.value;
  let [year,month, day,] = getExpDate.split('-');
  let date1 = new Date(Number(year) + Number(y), Number(month-1) + Number(m), Number(day) + Number(d));

  console.log ('final date : ',date1);

  this.step1Form.get("exptDate").setValue(this.datePipe.transform(date1,"yyyy-MM-dd"));
} */
getDiffDays() {
/*   if(this.certificationnempLinkForm.get("renewDate")?.value > this.datePipe.transform(Date.now(),"yyyy-MM-dd")){ */
    // var startDate = new Date(this.datePipe.transform(Date.now(),"yyyy-MM-dd"));
    // var endDate = new Date(this.step1Form.get("renewDate")?.value);

    // let Time:any;
//     if(this.step1Form.get("renewDate")?.value < this.datePipe.transform(Date.now(),"yyyy-MM-dd")){
//       /* Time = startDate.getTime() - endDate.getTime();
//       var newTime = Time / (1000 * 3600 * 24);


//       var years = newTime / 365
//       var months = (newTime % 365) / 30
//       var days = (newTime % 365) % 30
// */

//        this.selectRenewDateDecrease(this.renewalTimeFrame,this.step1Form.get("renewDate")?.value)
//     }else{
      // Time = endDate.getTime();
      // var newTime = Time / (1000 * 3600 * 24);


      // var years = newTime / 365
      // var months = (newTime % 365) / 30
      // var days = (newTime % 365) % 30



      //  this.selectRenewDateIncrease(Math.floor(years),Math.floor(months),Math.floor(days),this.initialExpirationDate)

      var startDate = new Date(this.datePipe.transform(Date.now(),"yyyy-MM-dd"));

      var endDate = new Date(this.step1Form.get("renewDate")?.value);



    let Time:any;



    let getExpDate = this.step1Form.get("renewDate")?.value;

    let [year,month, day,] = getExpDate.split('-');

    let date1 = new Date(Number(this.renewalTimeFrame)+Number(year), Number(month-1), Number(day));

    this.step1Form.get("exptDate").setValue(this.datePipe.transform(date1,"yyyy-MM-dd"));

    // }




 /*  } */

/*   else if(this.certificationnempLinkForm.get("renewDate")?.value < this.datePipe.transform(Date.now(),"yyyy-MM-dd")){
    var startDate = new Date(this.datePipe.transform(Date.now(),"yyyy-MM-dd"));
    var endDate = new Date(this.certificationnempLinkForm.get("renewDate")?.value);

    var Time = endDate.getTime() - startDate.getTime();
    var newTime = Time / (1000 * 3600 * 24);


    var years = newTime / 365
    var months = (newTime % 365) / 30
    var days = (newTime % 365) % 30


    this.selectRenewDateDecrease(Math.floor(years),Math.floor(months),Math.floor(days))
  }

   */
}

selectRenewDateDecrease(renewDate:any,date:any){

  // let getExpDate = this.initialExpirationDate
  let getExpDate = date
     let [year,month, day,] = getExpDate.split('-');
     let date1 = new Date(Number(renewDate)+Number(year), Number(month-1),Number(day));
     this.step1Form.get("exptDate").setValue(this.datePipe.transform(date1,"yyyy-MM-dd"));
 }


  resetAll(){
    // this.step1Form.reset();
    // this.step2Form.reset();
    // this.step3Form.reset();
    this.readyStep1Form();
    this.readyStep2Form();

    this.step3Form.patchValue({
      effectiveDate: new UntypedFormControl(
        this.datePipe.transform(Date.now(), 'yyyy-MM-dd')
      ),
      reason: new UntypedFormControl(''),
      addNew:new UntypedFormControl(false),
    })
    this.stepper.reset();
    this.clearPositions();
  }

  clearPositions() {
    this.positionsControl.setValue([]);
  }

}
