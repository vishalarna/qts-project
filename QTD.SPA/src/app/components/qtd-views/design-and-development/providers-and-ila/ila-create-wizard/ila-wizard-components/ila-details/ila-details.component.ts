import { pipe } from 'rxjs';
import { saveILA } from 'src/app/_Statemanagement/action/state.componentcommunication';
import * as ckcustomBuild from 'src/app/ckcustomBuild/build/ckeditor.js';
import { IlaService } from 'src/app/_Services/QTD/ila.service';
import { MatStepper } from '@angular/material/stepper';
import { TemplatePortal } from '@angular/cdk/portal';
import { Component, EventEmitter, Output, Input, OnInit, ViewChild, ViewContainerRef, OnDestroy } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators, FormArray, UntypedFormControl } from '@angular/forms';
import { TranslateService } from '@ngx-translate/core';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { Router } from '@angular/router';
import { props, Store } from '@ngrx/store';
import { NercStandardService } from 'src/app/_Services/QTD/nerc-standard.service';
import { ILANercStandard_LinkOptions } from 'src/app/_DtoModels/ILANercStandard_Link/ILANercStandard_LinkOptions';
import { SubSink } from 'subsink';
import { ILA } from 'src/app/_DtoModels/ILA/ILA';
import { SaftyHazard } from 'src/app/_DtoModels/SaftyHazard/SaftyHazard';
import { RegulatoryRequirement } from 'src/app/_DtoModels/RegulatoryRequirements/RegulatoryRequirement';
import { Position } from 'src/app/_DtoModels/Position/Position';
import { CertificationService } from 'src/app/_Services/QTD/certification.service';
import { CertificationILAVM, SubRequirementVM } from 'src/app/_DtoModels/CertifyingBody/CertifyingBodyCompactOptions';
import { ILACreditHourVM } from 'src/app/_DtoModels/ILA/ILACreditHourVM';
import { ILAPrerequisitesOptions } from 'src/app/_DtoModels/ILA/ILAPrerequisitesOptions';
import { SelectionModel } from '@angular/cdk/collections';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { ILAPrerequisitesLinkOptions } from '@models/ILA_Prerequisites_Link/ILA_Prerequisites_LinkOptions';
import {  CertifyingBodyWithSubRequirementsVM } from '@models/CertifyingBody/CertifyingBodyWithSubRequirementsVM';
import { CertifyingBodiesService } from 'src/app/_Services/QTD/certifying-bodies.service';
import { CertifyingBodyCEHUpdateOptions } from '@models/CertifyingBody/CertifyingBodyCEHUpdateOptions';
import { SubRequirementUpdateOptions } from '@models/Certification/SubRequirementUpdateOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { ILADetailsVM } from '@models/ILA/ILADetailsVM';

@Component({
  selector: 'app-ila-details',
  templateUrl: './ila-details.component.html',
  styleUrls: ['./ila-details.component.scss']
})
export class IlaDetailsComponent implements OnInit, OnDestroy {
  public Editor = ckcustomBuild;
  @Output() ila_details = new EventEmitter<any>();
  @Output() formChanges = new EventEmitter<any>();
  @Output() dynamicName = new EventEmitter<any>();
  @Output() nerc_check = new EventEmitter<any>();
  @Output() loadingEvent = new EventEmitter<boolean>();
  @Input() editIlaCheck: any;
  CertificatesList: CertificationListVM[] = [];
  certifyingBodiesList: CertifyingBodyWithSubRequirementsVM[] = [];
  LinkedCertificatesList: string[] = [];
  certificationLoader = true;
  certifyingBodyLoader:boolean=true;
  NERCForm!: UntypedFormGroup;
  NERCFormForCertifyingBody!: UntypedFormGroup;
  PJMForm!: UntypedFormGroup;
  numberRegex = /^-?(0|[1-9]\d*)(\.\d+)?$/;
  imageInBase64: string | undefined = ""
  uploadedImage: string;
  url_display: any;
  selected = -1;
  safteyHazard_array: any;
  regrequirement_array: any;
  subrequirements: any[];
  certifyingBodySubRequirements: SubRequirementVM[];
  selection = new SelectionModel<any>(true, []);
  //provider
  provider: string = '';
  providerConditions: boolean = false;

  //topic
  topic: string = '';
  topicConditions: boolean = false;

  //offical name
  offical_name: string = 'Intro to QTD-QTS_001_01';
  officalNameConditions: boolean = false;
  TotalCreditHourForm = new UntypedFormGroup({'TotalCreditHours':new UntypedFormControl('',Validators.pattern(this.numberRegex))});
  PrerequisitesForm = new UntypedFormGroup({'Prerequisites':new UntypedFormControl()});
  prerequisites:string;
  //nickname
  nick_name: any;
  isDeleteAllowed: boolean = false;
  //number
  numberConditions: boolean = false;
  number: string = '8932345';

  //ILA Description
  ILADescriptionConditions: boolean = false;
  ILA_Description: string = 'Continously monitor all pretinent conditions on POWERCO and neighbouring test systems, identify actual or potenial problems, and determine need for corrective actions.';

  //Delievry Methdods
  methodConditions: boolean = false;
  delivery_methods: any[] = [];

  //positions
  positions: Position[] = [];

  //credit_hours
  credit_hours: any[] = [];
  value_selected: string = "";
  certifyingBodySelected: string = "";
  NERC_checkboxes: any[] = [];
  Save_Clicked: boolean = false;
  Save_PJM_Clicked: boolean = false;
  edit_credit_hours: boolean = false;
  dropdown: boolean = false;
  nercMembers: any[] = [];
  //selected = -1;

  //fly panels
  message!: RegulatoryRequirement[];
  message1!: any[];
  message2: any;
  message3!: SaftyHazard[];
  message4!: ILADetailsVM[];
  ilatableDataSource: MatTableDataSource<any> = new MatTableDataSource<any>();
  procedures_edit: boolean = false;
  safetyhazards_edit: boolean = false;
  regulatoryrequirements_edit: boolean = false;
  prerequisites_edit: boolean = false;
  unlinkPrerequisitesIDs:any[] = []
  nercStandardMembers: any[] = [];
  pjmStandardMembers: any[] = [];
  ILAID: any;
  isNercCheck: boolean;
  isCertifyingBodyNercCheck: boolean;

  pjmArray: ILANercStandard_LinkOptions = new ILANercStandard_LinkOptions();
  nercArray: ILANercStandard_LinkOptions = new ILANercStandard_LinkOptions();


  displayedColumns: string[] = ['index', 'number', 'name','action'];
  // Allowed Image types
  allowedTypes = ['image/jpg', 'image/jpeg', 'image/bmp', 'image/png', 'image/svg'];
  unlinkPrereqId :any;
  unlinkPrerequisiteDescription = ""
  url: any;
  img = '';
  image_name: string = "";
  subscription = new SubSink();
  mainSpinner = false;
  originalCredValue?:number;
  isChanged = false;
  creditHoursView:string='ByCertification';
  uniqueCertifyingBodyMessages: string[] = [];

  constructor(
    public dialog: MatDialog,
    private alert: SweetAlertService,
    public flyPanelService: FlyInPanelService,
    private vcf: ViewContainerRef,
    private fb: UntypedFormBuilder,
    private translate: TranslateService,
    private router: Router,
    private saveStore: Store<{ saveIla: any }>,
    private nercStandardService: NercStandardService,
    private ilaService: IlaService,
    private certService: CertificationService,
    private certBodyService: CertifyingBodiesService,
    private labelPipe:LabelReplacementPipe
  ) {
  }

  ngOnInit(): void {      
    this.loadingEvent.emit(true);
    this.mainSpinner = true;
    this.subscription.sink = this.saveStore.select('saveIla').pipe().subscribe((res) => {
      
      if (res['saveData']['result'] !== undefined && res['tabIndex'] === 1) {
        this.delivery_methods = [
          { id: res['saveData']?.result?.deliveryMethodId, value: res['saveData']?.result?.deliveryMethodName }
        ]
        
        this.mainSpinner = true;

        this.ILA_Description = res['saveData']['result']['description'];

        this.offical_name = res['saveData']['result']['name'];

        this.nick_name = res['saveData']['result']['nickName'];

        this.number = res['saveData']['result']['number'];

        this.url = this.ilaService.baseUrlForImage+res['saveData']['result']['image'];

        this.provider = res['saveData']['result']['providerName'];

        this.ILAID = res['saveData']['result']['id'];

        this.readyTotalTrainingHours();

        this.readyPrerequisites();

        this.readyPositions();
        this.readyILATopics();

        this.isNercCheck = res['saveData']['result'].isProviderNERC;

        this.dynamicName.emit(this.offical_name + ' - ' + this.number);

        this.fetchAllCertifications();
        this.fetchCertifyingBodiesAsync();

        this.readyEditProcedures(this.ILAID);
        this.readyEditRegRequirement(this.ILAID);
        this.readyEditSafteyHazrads(this.ILAID);
        this.getPrerequisitesColumnData(this.ILAID);
      }
    })

    //array for credit hours dropdown
    if (this.editIlaCheck === true && this.ILAID !== undefined) {
      this.readyEditProcedures(this.ILAID);
      this.readyEditRegRequirement(this.ILAID);
      this.readyEditSafteyHazrads(this.ILAID);
    }
    this.loadingEvent.emit(false);
  }
  SelectedCertificationChanged() {
    var certificateId = this.value_selected;
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  async readyTotalTrainingHours(){
    
    var value = await this.ilaService.getTotalTrainingHours(this.ILAID)
    this.originalCredValue = value;
    this.TotalCreditHourForm.get('TotalCreditHours')?.setValue(value);
    this.TotalCreditHourForm.updateValueAndValidity();
  }

 

  async readyPositions() {
    this.positions = await this.ilaService.getLinkedPositions(this.ILAID);
    this.mainSpinner = false;
  }

  async readyILATopics(){
    var ilaTopics = await this.ilaService.getLinkedILATopicsAsync(this.ILAID);
    this.topic = ilaTopics.map(x=>x.name).join(", ");
  }

  unlinkPrerequisitesModal(templateRef: any, id?: any) {
    
     this.unlinkPrerequisiteDescription = 'You are selecting to unlink the following Prerequisite\n';
    if(id){
      this.unlinkPrereqId = id
      this.unlinkPrerequisiteDescription += this.message4.find((x:any)=>x.id==id).name + '\n';
    }else{
      this.unlinkPrerequisitesIDs.forEach((d,i)=>{
        this.unlinkPrerequisiteDescription += this.message4.find((x:any)=>x.id==d).name + '\n';
      })
    }
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  async unlinkPrerequisite(e:any){

    let option:ILAPrerequisitesLinkOptions = new ILAPrerequisitesLinkOptions()
    option.PreRequisiteIds = []
    if(this.unlinkPrereqId){
      option.PreRequisiteIds?.push(this.unlinkPrereqId)
    }else{
      option.PreRequisiteIds = this.unlinkPrerequisitesIDs
    }    
    option.ILAId = this.ILAID
    await this.ilaService.UnlinkPreRequisites(this.ILAID, option).then((res) => {
      this.alert.successToast('Prerequisites unlinked successfully');
      this.selection.clear();
      this.unlinkPrerequisitesIDs = [];
      this.unlinkPrereqId = undefined;            
    });
    this.readyPrerequisites()
  }

  selectrow(row: any) {
    this.selection.toggle(row);
    this.unlinkPrerequisitesIDs = [];
    this.selection.selected.forEach((v, i) => {
      this.unlinkPrerequisitesIDs.push(v.id);
    });
  }

  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.ilatableDataSource.data.length;
    return numSelected === numRows;
  }

  masterToggle() {
    this.isAllSelected()
      ? this.selection.clear()
      : this.ilatableDataSource.data.forEach((row) => this.selection.select(row));

    this.unlinkPrerequisitesIDs = [];
    this.selection.selected.forEach((v, i) => {
      this.unlinkPrerequisitesIDs.push(v.id);
    });
  }

  isCertificateNerc = false;
  CertificationSelectionChange() {
    
    var certId = this.value_selected;
    var isNercCheck = this.isNercCheck;
    this.isCertificateNerc = this.CertificatesList.find((x => x.certificationId === certId)).IsNerc;
    this.certificationLoader=true;
    this.certService.getCertificationDataWithILA(certId, this.ILAID)
      .then((res) => {
        this.isNercCheck = res.isNerc;
        this.isDeleteAllowed = res.isAlreadySaved;
        this.subrequirements = res.certificationSubRequirements;
        const Nercgroup = this.fb.group({});

        this.subrequirements.forEach((sub) => {
          Nercgroup.addControl(sub.subRequirementId, this.fb.control(sub.reqHour, [Validators.required,Validators.pattern(this.numberRegex)]));
        });
        Nercgroup.addControl("IncludeSimulation", this.fb.control(res.isIncludeSimulation));
        Nercgroup.addControl("EmergencyOPHours", this.fb.control(res.isEmergencyOpHours));
        Nercgroup.addControl("PartialCredit", this.fb.control(res.isPartialCreditHours));
        Nercgroup.addControl("cehHours", this.fb.control(res.cehHours,[Validators.pattern(this.numberRegex)]));

        this.NERCForm = Nercgroup;
        this.certificationLoader=false;

        //have to load credit hours on the bases of isNerc
        //have to load sub requirement table on the bases of  certification data
        // have to populate data on the bases of res data available in links
      });
  }
  certifyingBodySelectionChange(event:any){
    this.certifyingBodySubRequirements = [];
    this.certifyingBodySelected=event.value;
    var selectedCertifyingBody = this.certifyingBodiesList.find(x=>x.certifyingBody?.id == event.value);
    this.certifyingBodyLoader=true;
    if(selectedCertifyingBody != null){
      this.isCertifyingBodyNercCheck = selectedCertifyingBody.certifyingBody?.isNERC;
      this.isNercCheck = selectedCertifyingBody.certifyingBody?.isNERC;
      const Nercgroup = this.fb.group({});
      selectedCertifyingBody.certificationSubRequirements?.map((item)=>{
        this.certifyingBodySubRequirements.push(item)
      })
      this.certifyingBodySubRequirements?.forEach((sub) => {
        Nercgroup.addControl(sub.reqName, this.fb.control(sub.reqHour, [Validators.required,Validators.pattern(this.numberRegex)]));
      });
      Nercgroup.addControl("IncludeSimulation", this.fb.control(selectedCertifyingBody.isIncludeSimulation));
      Nercgroup.addControl("EmergencyOPHours", this.fb.control(selectedCertifyingBody.isEmergencyOpHours));
      Nercgroup.addControl("PartialCredit", this.fb.control(selectedCertifyingBody.isPartialCreditHours));
      Nercgroup.addControl("cehHours", this.fb.control(selectedCertifyingBody.cehHours,[Validators.pattern(this.numberRegex)]));
      this.NERCFormForCertifyingBody = Nercgroup;
      this.certifyingBodyLoader=false;
    }
  }
  async fetchAllCertifications() {
    this.CertificatesList = [];
    this.LinkedCertificatesList=[];
    this.certService.getCertCategoryWithCert()
      .then((res) => {
        res.forEach((x) => {
          x.certificationCompactOptions.forEach((certificate) => {
            if (this.isNercCheck === true && (x.certifyingBody.isNERC === true || x.certifyingBody.isNERC === null) && x.certifyingBody.enableCertifyingBodyLevelCEHEditing === false) {
              var cer = new CertificationListVM();
              cer.certificationId = certificate.id;
              cer.certificationName = certificate.name;
              cer.IsNerc = x.certifyingBody.isNERC;
              this.CertificatesList.push(cer);
            } else if (x.certifyingBody.isNERC === false && x.certifyingBody.enableCertifyingBodyLevelCEHEditing === false) {

              var cer = new CertificationListVM();
              cer.certificationId = certificate.id;
              cer.certificationName = certificate.name;
              cer.IsNerc = x.certifyingBody.isNERC;
              this.CertificatesList.push(cer);
            }
          });
        });
        this.certificationLoader = false;
      });

      await this.getLinkedCertificationsAsync();
  }
  async fetchCertifyingBodiesAsync(){
    this.LinkedCertificatesList=[];
    this.certifyingBodiesList =[];
    this.certifyingBodiesList = await this.certBodyService.getCertifyingBodiesByLevelEditingAsync(this.ILAID,true);
    if(this.isNercCheck){
      this.certifyingBodiesList = this.certifyingBodiesList.filter(x=>(x.certifyingBody?.isNERC === true || x.certifyingBody?.isNERC === null))
    }
    else{
      this.certifyingBodiesList = this.certifyingBodiesList.filter(x=>x.certifyingBody?.isNERC == false);
    }
    this.certifyingBodyLoader=false;
    await this.getLinkedCertificationsAsync();
    this.hasCertifyingBodyInconsistencies;
    await this.prepareUniqueCertifyingBodyMessages();
  }

  async prepareUniqueCertifyingBodyMessages() {
    const messages = new Set<string>();
    this.certifyingBodiesList?.forEach(body => {
      body.certifyingBodyConsistencyResults?.forEach(result => {
        result.certifyingBodyInconsistencies?.forEach(inconsistency => {
          messages.add(inconsistency.message);
        });
      });
    });
    this.uniqueCertifyingBodyMessages = Array.from(messages);
  }

  get hasCertifyingBodyInconsistencies(): boolean {
    return this.certifyingBodiesList?.some(body => body.certifyingBodyConsistencyResults.some(result => result.isConsistent !== true)) ?? false;
  }
  
  async getLinkedCertificationsAsync(){
    this.LinkedCertificatesList = await this.certService.getLinkedCertifications(this.ILAID);
  }
  procedureLoader = false;
  async readyEditProcedures(id: any) {
    let tempSrc: any = [];
    this.procedureLoader = true;
    await this.ilaService.getProceduresLink(id).then((res) => {
      res.forEach((i) => {
        tempSrc.push({
          id: i.id,
          category_id: i.issuingAuthorityId,
          description: i.title,
          checked: true,
          number: i.number,
          active: i.active
        })
      });
      this.message1 = tempSrc;
      this.procedures_edit = this.message1.length > 0;
    }).finally(() => {
      this.procedureLoader = false;
    })
  }

  shLoader = false;
  async readyEditSafteyHazrads(id: any) {
    this.shLoader = true;
    let tempSrc: any = [];
    await this.ilaService.getSafteyHazardLink(id).then((res) => {
      res.forEach((i) => {
        tempSrc.push({
          id: i.id,
          category_id: i.saftyHazardCategoryId,
          description: i.title,
          checked: true,
          number: i.number,
        })
      });
      this.message3 = tempSrc;
      this.safetyhazards_edit = this.message3.length > 0;
    }).finally(() => {
      this.shLoader = false;
    })
  }

  rrLoader = false;
  async readyEditRegRequirement(id: any) {
    this.rrLoader = true;
    let tempSrc: any = [];
    await this.ilaService.getRegRequirementLink(id).then((res) => {
      res.forEach((i) => {
        tempSrc.push({
          id: i.id,
          category_id: i.issuingAuthorityId,
          description: i.title,
          checked: true,
          number: i.number,
        })
      });
      this.message = tempSrc;
      this.regulatoryrequirements_edit = this.message.length > 0;
    }).finally(() => {
      this.rrLoader = false;
    });
  }

  async readyEditCreditHours(id: any) {
    let tempSrc: any = [];
    let creditHoursArray: any[] = [];
    await this.ilaService.getNercStandardLink(id).then((res) => {
      res.forEach((i) => {
        tempSrc = i;
        if (tempSrc['ilA_NercStandard_Links'].length > 0) {
          tempSrc.ilA_NercStandard_Links.forEach((data) => {
            creditHoursArray.push(data);
          })
        }
      });
      let result = creditHoursArray.filter(o1 => this.nercStandardMembers.some(o2 => o1.stdId === o2.id));
      for (var x of this.pjmStandardMembers) {
        this.PJMForm.get(x.label)?.setValue(creditHoursArray[0].creditHoursByStd);
      }
      for (var ch of result) {
        var x = this.nercStandardMembers.find((f) => f.memberId === ch.nercStdMemberId);
        this.NERCForm.get(x.label)?.setValue(ch.creditHoursByStd);
      }
    }).catch((err) => {
      
    })
  }

  returnData(data: any) {
    return data.id === this.credit_hours[0].id;
  }

  async readyCreditHours() {
    await this.nercStandardService.getAll().then((res) => {
      this.credit_hours = [];
      this.nercStandardMembers = [];
      this.pjmStandardMembers = [];
      const Nercgroup = this.fb.group({});
      const PJMGroup = this.fb.group({});
      PJMGroup.addControl("cehHours", this.fb.control(standard.cehHours,[Validators.pattern(this.numberRegex)]));
      for (var standard of res) {
        this.credit_hours.push({ id: standard.id, value: standard.name, label: standard.name.replace(/\s/g, ""), members: standard.nercStandardMembers });
        this.credit_hours.splice(2, 2);
        for (var member of standard.nercStandardMembers) {
          this.nercMembers.push({
            memberId: member.id,
            id: member.stdId,
            name: member.name,
            label: member.name.replace(/\s/g, ""),
            type: member.type,
          })
        }

        var cbType = this.nercMembers.filter((data) => { return data.type === "cb" });
        //this.selected = cbType.filter((data) => { return data === })
      }
      if (this.isNercCheck) {
        this.nercStandardMembers = this.nercMembers.filter((data) => { return data.id === this.credit_hours[1].id && data.type === 'if' });
        this.value_selected = 'NERC';
      } else {
        this.pjmStandardMembers = this.nercMembers.filter((data) => { return data.id === this.credit_hours[0].id });
        this.value_selected = 'PJM';
      }
      this.NERC_checkboxes = [];

      this.nercStandardMembers.forEach((data: any) => {
        if (data.label !== 'cb') {
          Nercgroup.addControl(data.label, this.fb.control('', [Validators.required]));
        }

      })

      this.pjmStandardMembers.forEach((data: any) => {
        PJMGroup.addControl(data.label, this.fb.control('', [Validators.required]));
      });

      this.NERCForm = Nercgroup;
      this.PJMForm = PJMGroup;
      
      
      
      this.readyContinueButtonEnableLogic();
    }).catch((err) => {
      
    })
  }

  selectFile(event: any) {
    //Angular 11, for stricter type
    if (!event.target.files[0] || event.target.files[0].length == 0) {
      this.img = 'You must select an image';
      return;
    }
    var mimeType = event.target.files[0].type;
    if (mimeType.match(/image\/*/) == null) {
      this.img = 'Only images are supported';
      return;
    }
    if (!this.allowedTypes.includes(event.target.files[0].type.toLowerCase())) {
      this.img = "Inavild Image type selected";
      this.alert.errorToast("Please select valid image type (jpg,jpeg,bnp,png,svg)");
      return;
    }

    let file = event.target.files[0];
    let reader = new FileReader();
    reader.readAsDataURL(file);
    // Reads and converts image to base64 type string
    reader.onloadend = () => {
      this.url = reader.result?.toString()
    };
    reader.onload = (_event) => {
      this.img = '';
      this.image_name = event.target.value.substring(12);
    };
    reader.onerror = function (error) {
      
    };
  }

  //NERC
  async SaveNERC() {
    if (!this.NERCForm.valid) {
      this.alert.errorAlert(
        'Error',
        this.translate.instant('L.Required')
      );
      return;
    }
    else if (this.NERCForm.valid && this.TotalCreditHourForm.valid) {
      var options: CertificationILAVM = new CertificationILAVM();
      options.certificationId = this.value_selected;
      options.ilaId = this.ILAID;
      // options.totalHours = this.TotalCreditHourForm.get("TotalCreditHours")?.value;
      options.isPartialCreditHours = this.NERCForm.get("PartialCredit")?.value;
      options.isEmergencyOpHours = this.NERCForm.get("EmergencyOPHours")?.value;
      options.isIncludeSimulation = this.NERCForm.get("IncludeSimulation")?.value;
      options.cehHours = this.NERCForm.get('cehHours')?.value;
      var subRequirementsList: SubRequirementVM[] = [];
      this.subrequirements.forEach((sub) => {
        var subRequirement = new SubRequirementVM();
        subRequirement.subRequirementId = sub.subRequirementId;
        subRequirement.reqHour = this.NERCForm.get(sub.subRequirementId)?.value;
        subRequirement.reqName = sub.reqName;
        subRequirementsList.push(subRequirement);
      });
      options.certificationSubRequirements = subRequirementsList;

      await this.certService.SaveCertificationDataWithILA(options).then((res) => {
        this.alert.successToast("NERC Credit Hours Saved Successfully");
        if (this.isNercCheck === true) {
          this.nerc_check.emit(true);
        }
        this.value_selected = "";
      }).catch((err) => {
        
      })
  await this.fetchAllCertifications() ;

    }
  }

  async saveNERCByCertifyingBodyAsync(){
    if (!this.NERCFormForCertifyingBody.valid) {
      this.alert.errorAlert(
        'Error',
        this.translate.instant('L.Required')
      );
      return;
    }
    else if (this.NERCFormForCertifyingBody.valid) {
      var options: CertifyingBodyCEHUpdateOptions = new CertifyingBodyCEHUpdateOptions();
      options.isPartialCreditHours = this.NERCFormForCertifyingBody.get("PartialCredit")?.value ?? false;
      options.isEmergencyOpHours = this.NERCFormForCertifyingBody.get("EmergencyOPHours")?.value ?? false;
      options.isIncludeSimulation = this.NERCFormForCertifyingBody.get("IncludeSimulation")?.value ?? false;
      options.cehHours = this.NERCFormForCertifyingBody.get('cehHours')?.value;
      var subRequirementsList: SubRequirementUpdateOptions[] = [];
      this.certifyingBodySubRequirements?.forEach((sub) => {
        var subRequirement = new SubRequirementUpdateOptions();  
        subRequirement.reqHour = this.NERCFormForCertifyingBody.get(sub?.reqName)?.value;
        subRequirement.reqName = sub?.reqName;
        subRequirementsList.push(subRequirement);
      });
      options.subRequirements = subRequirementsList;
      await this.ilaService.saveILACertificationByCertifyingBodyAsync(this.ILAID,this.certifyingBodySelected,options).then((res) => {
        this.alert.successToast("Credit Hours Saved Successfully");
        if (this.isCertifyingBodyNercCheck === true) {
          this.nerc_check.emit(true);
        }
        this.certifyingBodySelected = "";
      }).catch((err) => { });

      await this.fetchCertifyingBodiesAsync() ;
    }
  }

  async DeleteLinks() {
    var transformedValue = await this.transformTitle("Certification");
    await this.certService.DeleteCertificationDataWithILA(this.ILAID, this.value_selected).then((res) => {
      this.alert.successToast(`Linked ${transformedValue} Removed Successfully`);
      this.value_selected = "";
    }).catch((err) => {
      
    })
    await this.fetchAllCertifications() ;

  }

  async DeleteLinksByCertifyingBody(){
    var transformedValue = await this.transformTitle("Certification");
    await this.ilaService.deleteILACertificationByCertifyingBodyAsync(this.ILAID, this.certifyingBodySelected).then((res) =>{
      this.alert.successToast(`Linked ${transformedValue} Removed Successfully`);
      this.certifyingBodySelected = "";
    }).catch((err) => {
      
    });
    await this.fetchCertifyingBodiesAsync() ;
  }


  async SavePJM() {
    if (!this.PJMForm.valid) {
      this.alert.errorAlert(
        'Error',
        this.translate.instant('L.Required')
      );
      return;
    }

    else if (this.PJMForm.valid) {
      var linkOptions: ILANercStandard_LinkOptions = new ILANercStandard_LinkOptions();
      linkOptions.nercStdValues = [];
      linkOptions.iLAId = this.ILAID;
      linkOptions.stdId = this.pjmStandardMembers[0].id;
      linkOptions.cehHours = this.PJMForm.get('cehHours')?.value;
      for (let res of this.pjmStandardMembers) {
        linkOptions.nercStdValues.push({
          nERCStdMemberId: res.memberId,
          creditHoursByStd: this.PJMForm.get(res.label)?.value
        })
      }

      this.pjmArray = linkOptions;
      await this.ilaService.LinkNercStandardLink(this.ILAID, linkOptions).then((res) => {
        this.alert.successToast("PJM Credit Hours Saved Successfully");
      }).catch((err) => {
        
      })


      this.Save_PJM_Clicked = true;
      this.dropdown = false;
      this.value_selected = '';
      /*  linkOptions.StdId = this.nercStandardMembers. */
      /* this.ilaService.LinkNercStandardLink(this.ILAID,) */
    await this.fetchAllCertifications() ;

    }
  }

  EditCreditHours() {
    this.edit_credit_hours = true;
    this.dropdown = false;
    
  }

  //edit buttons
  async SaveNERCChanges() {
    if (!this.NERCForm.valid) {
      this.alert.errorAlert(
        'Error',
        this.translate.instant('L.Required')
      );
      return;
    }
    else if (this.NERCForm.valid) {
      var linkOptions: ILANercStandard_LinkOptions = new ILANercStandard_LinkOptions();
      linkOptions.nercStdValues = [];
      linkOptions.iLAId = this.ILAID;
      linkOptions.stdId = this.nercStandardMembers[0].id;
      linkOptions.cehHours = this.NERCForm.get('cehHours')?.value;
      for (let res of this.nercStandardMembers) {
        linkOptions.nercStdValues.push({
          nERCStdMemberId: res.memberId,
          creditHoursByStd: this.NERCForm.get(res.label)?.value
        })
      }

      //first delete and then link again
      await this.ilaService.UnLinkNercStandardLink(this.ILAID, this.nercArray).then((res) => {
      }).catch((err) => {
        
      })

      await this.ilaService.LinkNercStandardLink(this.ILAID, linkOptions).then((res) => {
        this.alert.successToast('NERC Credit Hours Updated Successfully');
      }).catch((err) => {
        
      })
      this.dropdown = false;
      this.edit_credit_hours = false;
    }
  }

  async SavePJMChanges() {
    if (!this.PJMForm.valid) {
      this.alert.errorAlert(
        'Error',
        this.translate.instant('L.Required')
      );
      return;
    }
    else if (this.PJMForm.valid) {
      var linkOptions: ILANercStandard_LinkOptions = new ILANercStandard_LinkOptions();
      linkOptions.nercStdValues = [];
      linkOptions.iLAId = this.ILAID;
      linkOptions.stdId = this.pjmStandardMembers[0].id;
      for (let res of this.pjmStandardMembers) {
        linkOptions.nercStdValues.push({
          nERCStdMemberId: res.memberId,
          creditHoursByStd: this.PJMForm.get(res.label)?.value
        })
      }

      //first delete and then link again
      // await this.ilaService.UnLinkNercStandardLink(this.ILAID, this.pjmArray).then((res) => {
      //   
      // }).catch((err) => {
      //   
      // })

      await this.ilaService.LinkNercStandardLink(this.ILAID, linkOptions).then((res) => {
        this.alert.successToast('PJM Credit Hours Updated Successfully');
      }).catch((err) => {
        
      })

      this.dropdown = false;
      this.edit_credit_hours = false;
    }
  }

  async DeleteNERCChanges() {
    this.alert.confirmAlert('You want to delete?').then(async (result) => {
      if (result.isConfirmed) {
        this.NERCForm.reset();
        await this.ilaService.UnLinkNercStandardLink(this.ILAID, this.nercArray).then((res) => {
          this.alert.successToast('NERC Credit Hours Deleted Successfully');
        }).catch((err) => {
          
        })
        this.dropdown = false;
        this.edit_credit_hours = false;
        this.value_selected = '';
      }
    })
  }

  async DeletePJMChanges() {
    this.alert.confirmAlert('You want to delete?').then(async (result) => {
      if (result.isConfirmed) {
        this.PJMForm.reset();
        await this.ilaService.UnLinkNercStandardLink(this.ILAID, this.pjmArray).then((res) => {
          this.alert.successToast('PJM Credit Hours Deleted Successfully');
        }).catch((err) => {
          
        })

        this.dropdown = false;
        this.edit_credit_hours = false;
        this.value_selected = '';
      }
    })
  }

  async getPrerequisitesColumnData(id:any) {
    this.prerequisites =  await this.ilaService.getPreRequisitesAsync(id);
    if(this.prerequisites)
    this.PrerequisitesForm.get('Prerequisites').setValue(this.prerequisites);
  }

  //continue button logic
  readyContinueButtonEnableLogic() {
    // this.NERCForm.statusChanges.subscribe((res) => {
    //   if ((this.PJMForm.valid && res === "VALID")) {
    //     this.formChanges.emit(res);
    //   }
    //   else{
    //     this.formChanges.emit("INVALID");
    //   }
    // })

    //  this.PJMForm.statusChanges.subscribe((res => {
    //   if(this.NERCForm.valid && res === "VALID"){
    //     this.formChanges.emit(res);
    //   }
    //   else{
    //     this.formChanges.emit("INVALID");
    //   }
    // }))
  }

  //fly panels and linking of checkboxes

  openFlyInPanelPrerequisites(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  openFlyInPanelProcedures(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  openFlyInPanelSafetyHazard(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  openFlyInPanelRegulatoryRquirements(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }


  receiveRequirementRegulatory(event: any) {
    this.readyEditRegRequirement(this.ILAID);
  }

  receiveProcedures(event: any) {
    this.readyEditProcedures(this.ILAID);
  }

  receivePrerequisites(event: any) {
    this.message2 = event[0];
    this.message2 = this.message2.concat(event[1]);
    
    this.prerequisites_edit = true;
  }

  receiveSafetyHazard(event: any) {
    this.readyEditSafteyHazrads(this.ILAID);
  }

  recievepreqList(event: any) {
    this.readyPrerequisites();
  }

  prereqLoader = false;
  async readyPrerequisites() {
    this.prereqLoader = true;
    this.message4 = await this.ilaService.GetPrerequisitesData(this.ILAID);
    this.ilatableDataSource = new MatTableDataSource<any>(this.message4);
    this.prerequisites_edit = this.message4.length > 0;
    this.prereqLoader = false;
  }

  trHourSpinner =false;
  async saveTrainingHours(){
    this.trHourSpinner = true;
    var options = new ILACreditHourVM();
    options.totalCreditHours = this.TotalCreditHourForm.get('TotalCreditHours')?.value;
    await this.ilaService.UpdateTrainngHours(this.ILAID,options).then((_)=>{
      this.originalCredValue = options.totalCreditHours;
      this.isChanged = false;
      this.alert.successToast("Credit Hour data updated");
    }).finally(()=>{
      this.trHourSpinner = false;
    });
  }

  async savePrerequisitesAsync(ilaId:any){
    this.prerequisites = this.PrerequisitesForm.get('Prerequisites')?.value;
    var options = new ILAPrerequisitesOptions();
    options.preRequisites = this.prerequisites;
    if(ilaId){
      await this.ilaService.updatePrerequisitesAsync(ilaId,options).then((_)=>{
        this.alert.successToast("Prerequisites data updated");
        this.loadingEvent.emit(false);
      })
    }
  }

  checkInput(){
    var value = this.TotalCreditHourForm.get('TotalCreditHours')?.value;
    if(value === this.originalCredValue){
      this.isChanged = false;
    }
    else{
      this.isChanged = true;
    }
  }

  changeCreditHoursView(event) {
    this.creditHoursView=event;
    this.certifyingBodySelected="";
    this.value_selected="";
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  } 
}


class CertificationListVM {
  certificationName!: string;
  certificationId!: string;
  IsNerc!: boolean
}
