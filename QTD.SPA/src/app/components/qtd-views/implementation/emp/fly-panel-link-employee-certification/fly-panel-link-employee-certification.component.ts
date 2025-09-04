import { TemplatePortal } from '@angular/cdk/portal';
import { DatePipe, formatDate } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output, ViewContainerRef } from '@angular/core';
import { UntypedFormGroup, UntypedFormBuilder, UntypedFormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { EmployeeCertification } from '@models/EmployeeCertification/EmployeeCertification';
import { EmployeeLinkCertification } from 'src/app/_DtoModels/Employee/EmployeeLinkCertification';
import { EmployeeCertificateCreateOptions } from 'src/app/_DtoModels/EmployeeCertification/EmployeeCertificateCreateOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { CertificationService } from 'src/app/_Services/QTD/certification.service';
import { EmployeesService } from 'src/app/_Services/QTD/employees.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-fly-panel-link-employee-certification',
  templateUrl: './fly-panel-link-employee-certification.component.html',
  styleUrls: ['./fly-panel-link-employee-certification.component.scss']
})
export class FlyPanelLinkEmployeeCertificationComponent implements OnInit {
  @Input() isCopy: any;
  @Input() mode: "Add" | "Edit" | "Copy" = "Add"
  isEdit = false;
  showSpinner = false;
  AddAnotherposition: boolean = false;
  dateError = false;
  defCategory: any;
  certificationnempLinkForm: UntypedFormGroup = new UntypedFormGroup({});
  datePipe = new DatePipe('en-us');
  certList: any;
  empCertObj: any;
  effectiveDate: any;
  @Input() oldempCert: any;
  @Input() empId: any
  @Input() certId: any
  @Input() empcertId: any
  @Output() closed = new EventEmitter<any>();
  @Output() refresh = new EventEmitter<any>();
  @Input() isFromHistory: boolean = false;
  renewalTimeFrame: any;
  initialExpirationDate: any;
  employeeCertifications:EmployeeCertification[];
  isAlreadyPresentCertNumber:boolean = false;
  constructor(
    public flyPanelSrvc: FlyInPanelService,
    private vcf: ViewContainerRef,
    private fb: UntypedFormBuilder,
    private alert: SweetAlertService,
    private certService: CertificationService,
    private route: Router,
    private empSrvc: EmployeesService,
    private labelPipe:LabelReplacementPipe

    //private proc_issueAuthService: IssuingAuthoritiesService,
    //private alert: SweetAlertService,
    //private dataBroadcastService: DataBroadcastService,
  ) { }

  ngOnInit(): void {
    // if (this.oldIssuingAuthority !== undefined && !this.isCopy) {
    //   this.isEdit = true;
    //   this.readyIssuingAuthorityFormWithData();
    // }
    // else if (this.oldIssuingAuthority && this.isCopy) {
    //   this.isEdit = false;
    //   this.readyIssuingAuthorityFormWithData();
    // }
    // else {
    this.isEdit = false;
    //this.psoitionList = ["Category 1","Category 2","Category 3","Category 4","Category 5","Category 6"]
    if (this.mode === "Add") {
      this.getcertificationList();
    }
    this.readylinkcertificationForm();

    if (this.oldempCert !== undefined) {

      this.fillCertificationData();
    }
    this.getEmployeeCertifications();
    // }
  }

  ngAfterViewInit(): void {

  }

  closedefinition() {
    // this.flyPanelSrvc.close();
    this.closed.emit('IA_Proc closed');
  }


  dateChanged(event: any) {
    if (event >= formatDate(Date.now(), 'yyyy-MM-dd', 'en')) {
      this.dateError = false;
    }
    else {
      this.dateError = true;
    }
  }

  readylinkcertificationForm() {
    this.certificationnempLinkForm = this.fb.group({
      certificate: new UntypedFormControl('', Validators.required),
      certDate: new UntypedFormControl(this.datePipe.transform(Date.now(), "yyyy-MM-dd")),
      renewDate: new UntypedFormControl(this.datePipe.transform(Date.now(), "yyyy-MM-dd")),
      exptDate: new UntypedFormControl(this.datePipe.transform(Date.now(), "yyyy-MM-dd")),
      AddAnother: new UntypedFormControl(false),
      certnumber: new UntypedFormControl('')
    });
  }
  async getcertificationList() {

    await this.certService
      .getAll()
      .then((res: any) => {


        if (res != null) {
          this.certList = res;
        }
      })
      .catch(async (err: any) => {
        this.alert.errorToast(await this.labelPipe.transform('Certification') + ' List error');
      });
  }
  async LinkCertificationtoEmployee() {

    this.showSpinner = true;
    var options = new EmployeeLinkCertification();
    options.certificationId = this.certificationnempLinkForm.get("certificate")?.value;
    options.issueDate = this.certificationnempLinkForm.get("certDate")?.value;
    options.renewalDate = this.certificationnempLinkForm.get("renewDate")?.value;
    options.expirationDate = this.certificationnempLinkForm.get("exptDate")?.value;
    options.certificationNumber = this.certificationnempLinkForm.get("certnumber")?.value;

    // let segments = this.route.url.split('/')
    //
    // if (segments.includes('edit'))
    //  {
    //   this.empId = segments[segments.length - 1];
    // }


    await this.empSrvc.createCertification(this.empId, options).then(async (res: any) => {
      // this.saveSHCatHistory(res.id);
      if (this.certificationnempLinkForm.get('AddAnother')?.value) {
        this.certificationnempLinkForm.reset();
        this.certificationnempLinkForm.get('certDate')?.setValue(this.datePipe.transform(Date.now(), "yyyy-MM-dd"));
        this.certificationnempLinkForm.get('renewDate')?.setValue(this.datePipe.transform(Date.now(), "yyyy-MM-dd"));
        this.certificationnempLinkForm.get('exptDate')?.setValue(this.datePipe.transform(Date.now(), "yyyy-MM-dd"));
      }
      else {
        this.refresh.emit();
        this.closed.emit('fp-add-sh-cat-closed');
      }

      this.alert.successToast(`Successfull Link ` + await this.labelPipe.transform('Certification') + ` to the ` + await this.labelPipe.transform('Employee') );
    }).finally(() => {
      this.showSpinner = false;
    })
  }
  async UpdateCertification() {

    if (this.isFromHistory) {
      this.showSpinner = true;
      var options = new EmployeeLinkCertification();
      options.certificationId = this.certId;
      options.issueDate = this.certificationnempLinkForm.get("certDate")?.value;
      options.renewalDate = this.certificationnempLinkForm.get("renewDate")?.value;
      options.expirationDate = this.certificationnempLinkForm.get("exptDate")?.value;
      options.certificationNumber = this.certificationnempLinkForm.get("certnumber")?.value;

      await this.empSrvc.updateCertificationInHistory(this.oldempCert.id, options).then(async (res: any) => {
        // this.saveSHCatHistory(res.id);

        this.refresh.emit();
        this.closed.emit('fp-add-sh-cat-closed');


        this.alert.successToast(`Successfully Update ${await this.transformTitle("Certification")} History Entry`);
      }).finally(() => {
        this.showSpinner = false;
      })
    }
    else {
      this.showSpinner = true;
      var options = new EmployeeLinkCertification();
      options.certificationId = this.certId;
      options.issueDate = this.certificationnempLinkForm.get("certDate")?.value;
      options.renewalDate = this.certificationnempLinkForm.get("renewDate")?.value;
      options.expirationDate = this.certificationnempLinkForm.get("exptDate")?.value;
      options.certificationNumber = this.certificationnempLinkForm.get("certnumber")?.value;

      await this.empSrvc.updateCertifications(this.oldempCert.id, options).then(async (res: any) => {
        // this.saveSHCatHistory(res.id);

        this.refresh.emit();
        this.closed.emit('fp-add-sh-cat-closed');


        this.alert.successToast(`Successfully Update ${await this.transformTitle("Certification")}`);
      }).finally(() => {
        this.showSpinner = false;
      })
    }


  }
  async fillCertificationData() {

    // await this.empSrvc.getEmployeeCertification(this.certId) .then((res: any) => {
    this.certificationnempLinkForm.patchValue({
      certDate: this.datePipe.transform(this.oldempCert.issueDate, "yyyy-MM-dd"),
      renewDate: this.datePipe.transform(this.oldempCert.renewalDate, "yyyy-MM-dd"),
      exptDate: this.datePipe.transform(this.oldempCert.expirationDate, "yyyy-MM-dd"),
      certnumber: this.oldempCert.certificationNumber
    })

    var cert = await this.certService.get(this.oldempCert.certificationId);
    this.renewalTimeFrame = cert.renewalInterval;

  }

  // to change date based on renewal years from certification side
  changeClient(value: any, efdate: any) {
    this.renewalTimeFrame = value;
    //let getExpDate = this.certificationnempLinkForm.get("renewDate")?.value;
    /* let date = new Date(getExpDate); */
    let getExpDate = this.datePipe.transform(Date.now(), "yyyy-MM-dd");
    let [year, month, day,] = getExpDate.split('-');
    var years = Number(year);
    let date = new Date(value + years, +month - 1, +day);
    if (this.datePipe.transform(efdate, "yyyy-MM-dd") === this.datePipe.transform("0001-01-01T00:00:00", "yyyy-MM-dd")) {
      efdate = Date.now;
    }
    //   this.certificationnempLinkForm.get("exptDate")?.setValue(this.datePipe.transform(aYearFromNow, "yyyy-MM-dd"));

    this.certificationnempLinkForm.get("exptDate")?.setValue(this.datePipe.transform(date, "yyyy-MM-dd"));
    this.certificationnempLinkForm.get("renewDate")?.setValue(this.datePipe.transform(date, "yyyy-MM-dd"));

    //  this.certificationnempLinkForm.get("certDate")?.setValue(this.datePipe.transform(efdate ?? Date.now(),"yyyy-MM-dd"));

    this.initialExpirationDate = (this.datePipe.transform(date, "yyyy-MM-dd"));
    this.checkEmployeeCertificateAndCertNumber(this.certificationnempLinkForm.get("certificate")?.value,this.certificationnempLinkForm.get("certnumber")?.value,this.oldempCert?.id)
  }

  selectRenewDateIncrease(y: any, m: any, d: any, date: any) {
    let getExpDate = date
    let [year, month, day,] = getExpDate.split('-');
    let date1 = new Date(Number(y) + Number(year), Number(m) + Number(month - 1), Number(d) + Number(day));
    this.certificationnempLinkForm.get("exptDate").setValue(this.datePipe.transform(date1, "yyyy-MM-dd"));
  }

  selectRenewDateDecrease(renewDate: any, date: any) {

    // let getExpDate = this.initialExpirationDate
    let getExpDate = date
    let [year, month, day,] = getExpDate.split('-');
    let date1 = new Date(Number(renewDate) + Number(year), Number(month - 1), Number(day));
    this.certificationnempLinkForm.get("exptDate").setValue(this.datePipe.transform(date1, "yyyy-MM-dd"));
  }
  getDiffDays() {
    //     debugger;
    //   /*   if(this.certificationnempLinkForm.get("renewDate")?.value > this.datePipe.transform(Date.now(),"yyyy-MM-dd")){ */
    //       var startDate = new Date(this.datePipe.transform(Date.now(),"yyyy-MM-dd"));
    //       var endDate = new Date(this.certificationnempLinkForm.get("renewDate")?.value);

    //       let Time:any;
    //       if(this.certificationnempLinkForm.get("renewDate")?.value < this.datePipe.transform(Date.now(),"yyyy-MM-dd")){
    //         /* Time = startDate.getTime() - endDate.getTime();
    //         var newTime = Time / (1000 * 3600 * 24);


    //         var years = newTime / 365
    //         var months = (newTime % 365) / 30
    //         var days = (newTime % 365) % 30
    //  */

    //          this.selectRenewDateDecrease(this.renewalTimeFrame,this.certificationnempLinkForm.get("renewDate")?.value)
    //       }else{
    //         Time = endDate.getTime() - startDate.getTime();
    //         var newTime = Time / (1000 * 3600 * 24);


    //         var years = newTime / 365
    //         var months = (newTime % 365) / 30
    //         var days = (newTime % 365) % 30



    //          this.selectRenewDateIncrease(Math.floor(years),Math.floor(months),Math.floor(days),this.initialExpirationDate)

    //       }



    let getExpDate = this.datePipe.transform(this.certificationnempLinkForm.get("renewDate")?.value, "yyyy-MM-dd");

    let [year, month, day,] = getExpDate?.split('-');

    let date1 = new Date(Number(this.renewalTimeFrame) + Number(year), Number(month) - 1, Number(day));

    this.certificationnempLinkForm.get("exptDate").setValue(this.datePipe.transform(date1, "yyyy-MM-dd"));

    // var renewalDate:Date = this.certificationnempLinkForm.get('renewDate')?.value;
    // this.certificationnempLinkForm.get('exptDate').setValue(renewalDate);
    // this.certificationnempLinkForm.updateValueAndValidity();



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

  async openFlyInPanel(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelSrvc.open(portal);
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  } 

  async getEmployeeCertifications(){
    this.employeeCertifications = await this.empSrvc.getEmployeeCertificationsByEmployeeId(this.empId);
  }

  checkEmployeeCertificateAndCertNumber(certId:any,value:string,empCertId?:string){
    this.isAlreadyPresentCertNumber = this.employeeCertifications.some((item)=>item.certificationNumber==value && item.certificationId==certId && (empCertId ? item.id !== empCertId : true));
  }

  onCertNumberChange(e:any){
    var value = e.target.value;
    var certId = this.certificationnempLinkForm.get("certificate")?.value || this.oldempCert.certificationId
    this.checkEmployeeCertificateAndCertNumber(certId,value,this.oldempCert?.id);
  }
}
