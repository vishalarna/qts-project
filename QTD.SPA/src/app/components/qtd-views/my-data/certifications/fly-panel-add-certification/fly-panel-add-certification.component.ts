import { DatePipe, formatDate } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output, ViewContainerRef } from '@angular/core';
import { UntypedFormArray, UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { Certification } from 'src/app/_DtoModels/Certification/Certification';
import { CertificationCreateOptions } from 'src/app/_DtoModels/Certification/CertificationCreateOptions';
import { CertifyingBodiesService } from 'src/app/_Services/QTD/certifying-bodies.service';
import { CertificationService } from 'src/app/_Services/QTD/certification.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { Router } from '@angular/router';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
@Component({
  selector: 'app-fly-panel-add-certification',
  templateUrl: './fly-panel-add-certification.component.html',
  styleUrls: ['./fly-panel-add-certification.component.scss']
})
export class FlyPanelAddCertificationComponent implements OnInit {
  @Input() oldCertification: any;
  @Input() isCopy: any;
  @Input() mode : "Add" | "Edit" | "Copy" = "Add"
  isEdit = false;
  showSpinner = false;
  AddAnotherIssuingAuthority: boolean = false;
  dateError = false;
  issuingAuthorityTitle: any;
  certificationForm: UntypedFormGroup = new UntypedFormGroup({});
  datePipe = new DatePipe('en-us');
  certDesc = "";
  certNote = "";
  certifyingBodyList : any[] = [];
  CatNumber: number = 0;
  addCert: boolean = true;
  addIA: boolean = false;
  disableCertSettings: number;
  @Input() shouldNavigate = false;
  @Input() certCheck:boolean;



  showhide1: boolean;
  showhide2: boolean;
  showhide3: boolean;
  showhide4: boolean =false;

  selected = '';

  myForm: UntypedFormGroup;


  @Output() closed = new EventEmitter<any>();
  @Output() refresh = new EventEmitter<any>();
  @Output() updateCertDetails = new EventEmitter<any>();
  constructor(
    public flyPanelSrvc: FlyInPanelService,
    private vcf: ViewContainerRef,
    private fb: UntypedFormBuilder,
    private certService:CertificationService,
    private certbodyService:CertifyingBodiesService,
    private alert: SweetAlertService,
    private dataBroadcastService: DataBroadcastService,
    private router: Router,
    private labelPipe:LabelReplacementPipe
  ) { }

  ngOnInit(): void
  {
    this.getCertifyingBodiesList();
    this.readyCertificationForm();

    if (this.oldCertification !== undefined)
    {
      this.insertDataCertForm();
    }

    else
    {
      this.isEdit = false;
    }
  }

  whitespaceOnlyValidator(control) {
    let pattern = /\S/; // Regular expression to match whitespace-only

    if (pattern.test(control.value)) {
      return null;
    } else {
      return { whitespaceOnly: true };
    }
  }

  ngAfterViewInit(): void {

  }

  closeLocation() {

    this.closed.emit('IA_Proc closed');
  }

  closeIAPanel() {
    this.addIA = false;
    this.addCert = true;
  }

  openIAPanel() {
    this.addIA = true;
    this.addCert = false;
  }

  /* dateChanged(event: any) {
    if (event >= formatDate(Date.now(), 'yyyy-MM-dd', 'en')) {
      this.dateError = false;
    }
    else {
      this.dateError = true;
    }
  }
 */

  readyCertificationForm() {
    this.certificationForm = this.fb.group({

      name: new UntypedFormControl('', [Validators.required,this.whitespaceOnlyValidator]),
      certacronym: new UntypedFormControl('', [Validators.required,this.whitespaceOnlyValidator]),
      certdesc: new UntypedFormControl(''),
      certifyingbodyid : new UntypedFormControl('', Validators.required),
      renewaltimeframe: new UntypedFormControl(''),
      renewalinterval: new UntypedFormControl(''),
      credithrsreq: new UntypedFormControl(''),
      credithrs: new UntypedFormControl(''),
      certsubreq: new UntypedFormControl(''),
     /*  certsubreqname: new FormControl(''),
      certsubreqhours: new FormControl(''), */
      certmembernum: new UntypedFormControl(''),
      certifieddate: new UntypedFormControl(''),
      renewaldate: new UntypedFormControl(''),
      expirationdate: new UntypedFormControl(''),
      allowrolloverhours: new UntypedFormControl(''),
      additionalhours: new UntypedFormControl(''),
      test1: new UntypedFormControl(''),

      Notes: new UntypedFormControl('',[Validators.required,this.whitespaceOnlyValidator]),
      EffectiveDate: new UntypedFormControl(this.datePipe.transform(Date.now(), "yyyy-MM-dd")),
      AddAnother: new UntypedFormControl(false)
    });
    this.readySubRequirementForm();
  }

  readySubRequirementForm(){
    this.myForm = this.fb.group({
      certsubreqname: this.fb.array([]),
      certsubreqhours: this.fb.array([]),
      certificationSubRequirementId: this.fb.array([])
    });
    if(!this.oldCertification){
      this.addTextbox();

    }
  }

  addTextbox() {
    this.certsubreqname.push(this.fb.control(''));
    this.certsubreqhours.push(this.fb.control(''));
    this.certifyingBodyList.push(this.fb.control(''));
  }

  get certsubreqname(): UntypedFormArray {
    return this.myForm.get('certsubreqname') as UntypedFormArray;

  }

  get certsubreqhours(): UntypedFormArray {
    return this.myForm.get('certsubreqhours') as UntypedFormArray;
  }

  get certificationSubRequirementId(): UntypedFormArray {
    return this.myForm.get('certificationSubRequirementId') as UntypedFormArray;
  }

  async createNewCertification()
  {
    var transformedValue = await this.transformTitle("Certification")
    this.showSpinner = true;
    var options = new CertificationCreateOptions();
    options.CertifyingBodyId = this.certificationForm.get("certifyingbodyid")?.value;
    options.CertAcronym = this.certificationForm.get("certacronym")?.value;
    options.Name = this.certificationForm.get("name")?.value;
    options.CertDesc = this.certificationForm.get("certdesc")?.value;

    options.RenewalTimeFrame = this.certificationForm.get("renewaltimeframe")?.value;
    options.RenewalInterval = this.certificationForm.get("renewalinterval")?.value;

    options.CreditHrsReq = this.certificationForm.get("credithrsreq")?.value;
    options.CreditHrs = this.certificationForm.get("credithrs")?.value;

    options.CertSubReq = this.certificationForm.get("certsubreq")?.value;

    if(this.certsubreqname.length >=1 && this.certsubreqhours.length >=1) {
           this.certsubreqname.controls?.forEach((res)=>{
            if(res.value !== ""){
              options.CertSubReqName.push(res.value);
            }
          })
          this.certsubreqhours.controls?.forEach((res)=>{
            if(res.value !== ""){
                 options.CertSubReqHours.push(res.value);
            }
          })
      }



  /*   options.CertSubReqName = this.certificationForm.get("certsubreqname")?.value;
    options.CertSubReqHours = this.certificationForm.get("certsubreqhours")?.value; */

    options.CertMemberNum = this.certificationForm.get("certmembernum")?.value;
    options.CertifiedDate = this.certificationForm.get("certifieddate")?.value;
    options.RenewalDate = this.certificationForm.get("renewaldate")?.value;

    options.ExpirationDate = this.certificationForm.get("expirationdate")?.value;
    options.AllowRolloverHours = this.certificationForm.get("allowrolloverhours")?.value;

    options.AdditionalHours = this.certificationForm.get("additionalhours")?.value;

    options.EffectiveDate =  this.certificationForm.get('EffectiveDate')?.value;
    options.Notes = this.certificationForm.get('Notes')?.value;

     if (this.isCopy)
    {
      options.Name = this.oldCertification.name.trim().toLowerCase() == options.Name.trim().toLowerCase()
        ? options.Name + ("-Copy") : options.Name;
    }
    await this.certService.create(options).then((res: Certification) => {
      if (this.certificationForm.get('AddAnother')?.value) {
        this.certificationForm.reset();
        this.certificationForm.get('EffectiveDate')?.setValue(this.datePipe.transform(Date.now(), "yyyy-MM-dd"));
        this.selected = '';
      }
      else
      {
        if(this.certCheck){
          this.router.navigate([`/my-data/certifications/details/${res.id}`]);
        }
        this.closed.emit('fp-add-sh-cat-closed');
/*         this.refresh.emit();
        this.flyPanelSrvc.close(); */
      }

      if(this.shouldNavigate || this.isCopy){
        this.dataBroadcastService.navigateOnChange.next({type:"cert",data:res});
      }
      else{
        this.dataBroadcastService.updateMyDataNavBar.next(null);
      }

      this.alert.successToast(`Successfully Created ${transformedValue}`);

    }).finally(()=>{
      this.showSpinner = false;
    })
  }

  async copyCertification(){
    this.showSpinner = true;
    var options = new CertificationCreateOptions();
    options.CertifyingBodyId = this.certificationForm.get("certifyingbodyid")?.value;
    options.CertAcronym = this.certificationForm.get("certacronym")?.value;

    let name = this.certificationForm.get("name")?.value;
    if(name.toLowerCase() === this.oldCertification.name.toLowerCase()){
      options.Name + ("-Copy")
    }
    else{
      options.Name = name;
    }
   /*  options.Name = this.oldCertification.name == options.Name ? options.Name + ("-Copy") : options.Name; */
    options.CertDesc = this.certificationForm.get("certdesc")?.value;

    options.RenewalTimeFrame = this.certificationForm.get("renewaltimeframe")?.value;
    options.RenewalInterval = this.certificationForm.get("renewalinterval")?.value;

    options.CreditHrsReq = this.certificationForm.get("credithrsreq")?.value;
    options.CreditHrs = this.certificationForm.get("credithrs")?.value;

    options.CertSubReq = this.certificationForm.get("certsubreq")?.value;

    if(this.certsubreqname.length >=1 && this.certsubreqhours.length >=1) {
           this.certsubreqname.controls?.forEach((res)=>{
            if(res.value !== ""){
              options.CertSubReqName.push(res.value);
            }
          })
          this.certsubreqhours.controls?.forEach((res)=>{
            if(res.value !== ""){
                 options.CertSubReqHours.push(res.value);
            }
          })
      }



  /*   options.CertSubReqName = this.certificationForm.get("certsubreqname")?.value;
    options.CertSubReqHours = this.certificationForm.get("certsubreqhours")?.value; */

    options.CertMemberNum = this.certificationForm.get("certmembernum")?.value;
    options.CertifiedDate = this.certificationForm.get("certifieddate")?.value;
    options.RenewalDate = this.certificationForm.get("renewaldate")?.value;

    options.ExpirationDate = this.certificationForm.get("expirationdate")?.value;
    options.AllowRolloverHours = this.certificationForm.get("allowrolloverhours")?.value;

    options.AdditionalHours = this.certificationForm.get("additionalhours")?.value;

    options.EffectiveDate =  this.certificationForm.get('EffectiveDate')?.value;
    options.Notes = this.certificationForm.get('Notes')?.value;
    var transformedValue =await this.transformTitle("Certification")
    await this.certService.create(options).then((res: Certification) => {

      if(this.shouldNavigate){
        this.dataBroadcastService.navigateOnChange.next({type:"cert",data:res});
      }
      else{
        this.dataBroadcastService.updateMyDataNavBar.next(null);
      }
      this.closed.emit();
      this.alert.successToast(`Successfully Copied ${transformedValue}`);

    }).finally(()=>{
      this.showSpinner = false;
    })

  }

  removeSubRequirement(index: number) {
    const certsubreqnameArray = this.myForm.get('certsubreqname') as UntypedFormArray;
    const certsubreqhoursArray = this.myForm.get('certsubreqhours') as UntypedFormArray;
    const certIdArray = this.myForm.get('certificationSubRequirementId') as UntypedFormArray;
    certsubreqnameArray.removeAt(index);
    certsubreqhoursArray.removeAt(index);
    certIdArray.removeAt(index);
    this.myForm.setValue({
      certsubreqname: certsubreqnameArray.value,
      certsubreqhours: certsubreqhoursArray.value,
      certificationSubRequirementId:certIdArray.value
    });
    this.myForm.updateValueAndValidity();
  }

 async updateCertification()
  {
    var transformedValue = await this.transformTitle("Certification")
      this.showSpinner = true;
      var options = new CertificationCreateOptions();
      options.CertifyingBodyId = this.certificationForm.get("certifyingbodyid")?.value;
      options.CertAcronym = this.certificationForm.get("certacronym")?.value;
      options.Name = this.certificationForm.get("name")?.value;
      options.CertDesc = this.certificationForm.get("certdesc")?.value;

      options.RenewalTimeFrame = this.certificationForm.get("renewaltimeframe")?.value;
      options.RenewalInterval = this.certificationForm.get("renewalinterval")?.value;

      options.CreditHrsReq = this.certificationForm.get("credithrsreq")?.value;
      options.CreditHrs = this.certificationForm.get("credithrs")?.value;

      options.CertSubReq = this.certificationForm.get("certsubreq")?.value;
    /*   options.CertSubReqName = this.certificationForm.get("certsubreqname")?.value;
      options.CertSubReqHours = this.certificationForm.get("certsubreqhours")?.value; */
      const certsubreqnameArray = this.myForm.get('certsubreqname') as UntypedFormArray;
      const certsubreqhoursArray = this.myForm.get('certsubreqhours') as UntypedFormArray;
      const certIdArray = this.myForm.get('certificationSubRequirementId') as UntypedFormArray;

      options.CertMemberNum = this.certificationForm.get("certmembernum")?.value;
      options.CertifiedDate = this.certificationForm.get("certifieddate")?.value;
      options.RenewalDate = this.certificationForm.get("renewaldate")?.value;

      options.ExpirationDate = this.certificationForm.get("expirationdate")?.value;
      options.AllowRolloverHours = this.certificationForm.get("allowrolloverhours")?.value;

      options.AdditionalHours = this.certificationForm.get("additionalhours")?.value;

      options.Notes = this.certificationForm.get("Notes")?.value;
      options.EffectiveDate = this.certificationForm.get('EffectiveDate')?.value;

      if (certIdArray?.value && options.CertSubReq == true) {
        options.CertificationSubRequirementsIds = certIdArray.value.map((data: any) => data);
        options.CertSubReqName = certsubreqnameArray.value || [];
        options.CertSubReqHours = certsubreqhoursArray.value || [];
      } else {
        options.CertificationSubRequirementsIds = [];
        options.CertSubReqName = [];
        options.CertSubReqHours =  [];
      }
  
      this.certService.update(this.oldCertification.id,options).then((res: any) => {
        this.oldCertification = res;
        this.certificationForm.get("certsubreq")?.setValue(this.oldCertification.certSubReq);
        this.closed.emit('fp-add-ins-cat-closed');
        this.updateCertDetails.emit(this.oldCertification);
        this.refresh.emit();
        this.alert.successToast(transformedValue + " Successfully Updated");
      }).finally(()=>{
        this.showSpinner = false;
      })
  }


  async getCertifyingBodiesList()
  {
    await this.certbodyService
      .getAll()
      .then((res: any) => {

        if (res != null) {
          this.certifyingBodyList = res;
        }
      })
      .catch((err: any) => {
        this.alert.errorToast('Issuing Authority error');
      });
  }

  readyIANumber() {
    this.certService.getCount().then((res: number) => {
      this.CatNumber = res + 1;
    })
  }

  insertDataCertForm()
  {
    this.certificationForm.get('certifyingbodyid')?.setValue(this.oldCertification.certifyingBodyId);
    this.certificationForm.get('certacronym')?.setValue(this.oldCertification.certAcronym);
    this.certificationForm.get('name')?.setValue(this.oldCertification.name);
    this.certificationForm.get('certdesc')?.setValue(this.oldCertification.certDesc);
    this.certificationForm.get('renewaltimeframe')?.setValue(this.oldCertification.renewalTimeFrame);
    this.certificationForm.get('credithrsreq')?.setValue(this.oldCertification.creditHrsReq);
    this.showhide1 = this.oldCertification.creditHrsReq;
    if(this.oldCertification.renewalTimeFram !== false){
      this.selected = this.oldCertification.renewalInterval;
    }


    this.certificationForm.get('credithrs')?.setValue(this.oldCertification.creditHrs);
    this.certificationForm.get('certmembernum')?.setValue(this.oldCertification.certMemberNum);
    this.certificationForm.get('certifieddate')?.setValue(this.oldCertification.certifiedDate);
    this.certificationForm.get('expirationdate')?.setValue(this.oldCertification.expirationDate);
    this.certificationForm.get('renewaldate')?.setValue(this.oldCertification.renewalDate)

    this.certificationForm.get('renewalinterval')?.setValue(this.oldCertification.renewalInterval);
    this.selected = this.oldCertification.renewalInterval;

    this.certificationForm.get('allowrolloverhours')?.setValue(this.oldCertification.allowRolloverHours);
    this.showhide3 = this.oldCertification.allowRolloverHours;

    this.certificationForm.get('additionalhours')?.setValue(this.oldCertification.additionalHours);
    this.certificationForm.get('EffectiveDate')?.setValue(this.datePipe.transform(this.oldCertification.effectiveDate, "yyyy-MM-dd"));
    this.certificationForm.get("certsubreq")?.setValue(this.oldCertification.certSubReq);
    this.showhide2 = !!this.oldCertification.certSubReq;
    if(this.oldCertification.renewalTimeFrame){
      this.showhide4 = true;
    }
    this.certificationForm.get('EffectiveDate')?.setValue(this.datePipe.transform(Date.now(), "yyyy-MM-dd"));
  /*   this.certificationForm.get("certsubreqname")?.setValue(this.oldCertification.certSubReqName);
    this.certificationForm.get("certsubreqhours")?.setValue(this.oldCertification.certSubReqHours); */
    this.certificationForm.get('AddAnother')?.setValue(false);
    this.certificationForm.get('Notes')?.setValue(this.oldCertification.Notes);
    this.disableCertSettings = 0; //this.oldCertification.employeeCertifications.length;

    this.readySubRequirmentData(this.oldCertification.id);
  }

  onCertSubReqChange() {
    this.showhide2 = this.certificationForm.get('certsubreq')?.value;
  }

  readySubRequirmentData(id:any){
    let res:any
    this.certService.getSubRequirement(id).then((response)=>{

        response.forEach((res)=>{

          this.certsubreqname.push(this.fb.control( res.reqName));
          this.certsubreqhours.push(this.fb.control( res.reqHour));
          this.certificationSubRequirementId.push(this.fb.control( res.id));
        })

      })
       /*  this.myForm = this.fb.group({
          certsubreqname: res.reqName,
          certsubreqhours:res.reqHour,
        }) */


   /*  for(let obj of response){
      this.myForm= new FormGroup({
        certsubreqname: new FormControl(obj.reqName),
        certsubreqhours: new FormControl(obj.reqHour),
      })}*/
    }

    async transformTitle(title: string) {
      const labelName = await this.labelPipe.transform(title);
      return labelName;
    } 

    hasInvalidSubRequirement(): boolean {
      const certSubReqNames = this.myForm.get('certsubreqname')?.value || [];
      const certSubReqHours = this.myForm.get('certsubreqhours')?.value || [];
    
      return certSubReqNames.some((name: any) => !name || (typeof name === 'string' && !name.trim())) ||
             certSubReqHours.some((hours: any) => hours === null || hours === undefined || hours === '' || isNaN(hours) || hours === -1);
    }
    
}
