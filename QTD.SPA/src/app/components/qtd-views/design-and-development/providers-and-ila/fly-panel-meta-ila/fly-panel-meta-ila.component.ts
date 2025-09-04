import { Version_MetaILACreateOptions } from './../../../../../_DtoModels/Version_MetaILA/Version_MetaILACreateOptions';
import { MetaILACreateOptions } from './../../../../../_DtoModels/MetaILA/MetaILACreateOptions';
import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';
import { StepperOrientation } from '@angular/cdk/stepper';
import { Component, Input, OnInit, TemplateRef, ViewChild, EventEmitter, Output, ViewContainerRef } from '@angular/core';
import {
  UntypedFormBuilder,
  UntypedFormControl,
  UntypedFormGroup,
  Validators,
} from '@angular/forms';
import { MatLegacyDialog as MatDialog, MatLegacyDialogRef as MatDialogRef } from '@angular/material/legacy-dialog';
import { Observable } from 'rxjs';
import * as ckcustomBuild from 'src/app/ckcustomBuild/build/ckeditor.js';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { UploadAdapter } from 'src/app/_Shared/Utils/CKEditor_UploadAdapter';
import { MetaILAConfigPublishOptionService } from 'src/app/_Services/QTD/meta-ila-config-publish-option.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { DatePipe } from '@angular/common';
import { MetaILAService } from 'src/app/_Services/QTD/meta-ila.service';
import { MetaILAMembersLinkOptions, MetaILAMembersListOptions } from 'src/app/_DtoModels/MetaILAMembersLink/MetaILAMembersLinkOptions';
import { VersionMetaILAService } from 'src/app/_Services/QTD/version-meta-ila.service';
import { MetaILAUpdateOptions } from 'src/app/_DtoModels/MetaILA/MetaILAUpdateOptions';
import { MatStep, MatStepper } from '@angular/material/stepper';
import { StudentEvaluationService } from 'src/app/_Services/QTD/student-evaluation.service';
import { MetaILA_SummaryTest_ViewModel } from 'src/app/_DtoModels/MetaILA_SummaryTest/MetaILA_SummaryTest_ViewModel';
import { MetaILASummaryTestService } from 'src/app/_Services/QTD/meta-ila-summary-test.service';
import { cloneDeep } from 'lodash';
import { MetaILAStatusService } from 'src/app/_Services/QTD/meta-ila-status.service';
import { MetaILAStatus } from 'src/app/_DtoModels/MetaILAStatus/MetaILAStatus';
import { MetaILAEmployeesLinkOptions } from 'src/app/_DtoModels/MetaILAEmployeesLink/MetaILAEmployeesLinkOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { MetaILAVM } from '@models/MetaILA/MetaILAVM';
import { MetaILAEmployeeVM } from '@models/MetaILAEmployeesLink/MetaILAEmployeeVM';
import { ProviderService } from 'src/app/_Services/QTD/provider.service';
import { Provider } from '@models/Provider/Provider';

@Component({
  selector: 'app-fly-panel-meta-ila',
  templateUrl: './fly-panel-meta-ila.component.html',
  styleUrls: ['./fly-panel-meta-ila.component.scss'],
})
export class FlyPanelMetaIlaComponent implements OnInit {
  @Input() selectedCheckboxes: any;
  @Input() deliveryMethods: any;
  @Output() flyPanelCloseCheck = new EventEmitter<any>();
  @Input() isCreateMode: boolean = false;
  @Input() mode: string;
  @Input() selectedMetaILAID: string;
  metaILA: MetaILAVM = new MetaILAVM();
  firstFormGroup: UntypedFormGroup;
  secondFormGroup: UntypedFormGroup;
  thirdFormGroup: UntypedFormGroup;
  fourthFormGroup: UntypedFormGroup;
  public Editor = ckcustomBuild;
  selectedILAConfig: string;
  metaILAform: UntypedFormGroup;
  dialogRef: MatDialogRef<any>;
  stepperOrientation: Observable<StepperOrientation>;
  datePipe = new DatePipe('en-us');
  selectedIndex: number;
  selectedAssessmentId: any;
  currentStepIndex: number;
  header: string;
  description: string;
  confirmText: string;
  ilaId: string;
  metaILAConfigArray: any[] = [];
  metaILAMemeberLinkCheck: boolean = false;
  configCheck: boolean = false;
  isFlyPanelMetaOpenRequirements: boolean = false;
  isFlyPanelMetaOpenSummary: boolean = false;
  isFlyPanelMetaOpenStudEval: boolean = false;
  isFlyPanelMetaOpenStudEvalEdit: boolean = false;
  isAddEmpFlyPanelOpen:boolean=false;
  selectedValues: { [key: number]: string } = {};
  studentEvaluations: any;
  ilaDataToRemove: any;
  clonedILAData: any;
  metaIlaStatuses: MetaILAStatus[];
  selectedStudentEvaluationId: string;
  selectedILAIds:string[]=[];
  metaILASummaryFinalTestVM:MetaILA_SummaryTest_ViewModel=new MetaILA_SummaryTest_ViewModel();
  metaILASummaryRetakeTestVM:MetaILA_SummaryTest_ViewModel=new MetaILA_SummaryTest_ViewModel();
  isSecondStepInteracted:boolean=false;
  isThirdStepInteracted:boolean=false;
  isFlyPanelLinkILAsToMetaILA:boolean=false;
  testTitle:string;
  testMode:string;

  ila: any[] = [];
  clonedIla: any[] = [];
  clonedMetaILAEmployees: MetaILAEmployeeVM[] = [];
  deletedILAIndex:number;
  onDemandReleaseId:number;
  onDemandSelectedDates: { [ilaid: number]: string } = {};
  onDemandSelectedTimes: { [ilaid: number]: string } = {};
  onDemandStartDates: { [ilaid: number]: Date } = {};
  testType:string;
  providers:Provider[] = [];
  isNERCProvider:boolean = false;
  isApplicationPageLoading:boolean = false;
  nercCertDetails:any[] = [];

  @ViewChild('publishMetaILA') publishMetaILA: TemplateRef<any>;
  @ViewChild('stepper') stepper: MatStepper;

  constructor(
    public flyPanelSrvc: FlyInPanelService,
    private fb: UntypedFormBuilder,
    public dialog: MatDialog,
    private vcr: ViewContainerRef,
    private metaILAConfigService: MetaILAConfigPublishOptionService,
    private alert: SweetAlertService,
    private metaILAService: MetaILAService,
    private version_metaILA: VersionMetaILAService,
    private studentEvaluationService: StudentEvaluationService,
    private metaILASummaryTestService: MetaILASummaryTestService,
    private metaIlaStatusService: MetaILAStatusService,
    private labelPipe:LabelReplacementPipe,
    private providerService:ProviderService
  ) { }

  async ngOnInit(): Promise<void> {
    this.readyForm();
    this.currentStepIndex = 0;
    this.settingDialogue();
    this.readyMetaILAConfigPublishOptionAsync();
    this.ila = this.selectedCheckboxes;
    this.clonedIla = cloneDeep(this.ila);
    this.selectedILAIds = this.selectedCheckboxes.map(x=>x.id);
    this.getStudentEvaluationsAsync();
    this.getMetaIlaStatusesAsync();
    this.setMetaILAModeAsync();
    this.getAllProvidersAsync();
  }

  readyForm() {
    this.firstFormGroup = this.fb.group({
      metaTitle: new UntypedFormControl('', Validators.compose([Validators.required])),
      metaDescription: new UntypedFormControl(''),
      provider:new UntypedFormControl('', Validators.compose([Validators.required]))
    });
    this.fourthFormGroup = this.fb.group({
      EffectiveDate: new UntypedFormControl(this.datePipe.transform(Date.now(), "yyyy-MM-dd"), Validators.compose([Validators.required])),
      RevisionNumber: new UntypedFormControl('')
    });
  }
  setFormValues(){
    this.firstFormGroup.patchValue({
      metaTitle : this.metaILA.name,
      metaDescription :this.metaILA.description,
      provider:this.metaILA.providerId
    });
    this.fourthFormGroup.patchValue({
      fourthFormGroup :this.datePipe.transform(this.metaILA.effectiveDate, "yyyy-MM-dd"),
      RevisionNumber :this.metaILA.reason
    });
  }

  async readyMetaILAConfigPublishOptionAsync() {
    await this.metaILAConfigService.getAll().then((res) => {
      res.forEach((i, index) => {
        this.metaILAConfigArray.push({
          description: i.description,
          id: i.id
        })
      })
      this.onDemandReleaseId = res.find(x => x.description === "On Demand")?.id;
    }).catch(async (err) => {
      this.alert.errorToast('Error fetching Meta ' + await this.labelPipe.transform('ILA') + ' Configuration Publish Options');
    })
  }

  async setMetaILAModeAsync(){
    if(this.mode=='edit' || this.mode == 'copy'){
      this.metaILA = await this.metaILAService.getMetaILAAsync(this.selectedMetaILAID);
      this.selectedILAIds = this.metaILA.metaILA_ILAMemberVM.map(x=>x.ilaId.toString());
      this.isCreateMode=false;
      if(this.mode =='copy'){
        this.metaILA.id=undefined;
        this.metaILA.name=this.metaILA.name + " - Copy"
        this.isCreateMode=true;
      }
      this.setFormValues();
      this.setMetaILAMember();    
      this.metaILA.metaILA_EmployeeVM.forEach(x=>{ this.clonedMetaILAEmployees.push(x)});
      this.metaILASummaryFinalTestVM=this.metaILA.metaILA_SummaryTest_FinalTestId? await this.metaILASummaryTestService.getAsync(this.metaILA.metaILA_SummaryTest_FinalTestId): new MetaILA_SummaryTest_ViewModel();
      this.metaILASummaryRetakeTestVM=this.metaILA.metaILA_SummaryTest_RetakeTestId? await this.metaILASummaryTestService.getAsync(this.metaILA.metaILA_SummaryTest_RetakeTestId): new MetaILA_SummaryTest_ViewModel();
      this.selectedStudentEvaluationId=this.metaILA?.studentEvaluationId;
      this.isNERCProvider = this.providers.find(m=>m.id==this.metaILA.providerId)?.isNERC;
    }
    else{
      this.metaILA = new MetaILAVM();
      this.isCreateMode=true;
    }
  }
  async saveMetaILA() {
    var options = new MetaILACreateOptions();
    options.name = this.firstFormGroup.get('metaTitle')?.value;
    options.description = this.firstFormGroup.get('metaDescription')?.value ?? "";
    options.effectiveDate = this.fourthFormGroup.get('EffectiveDate')?.value;
    options.metaILAStatusId = this.metaIlaStatuses.find((s) => s.name === "Draft").id;
    options.reason = this.fourthFormGroup.get('RevisionNumber')?.value;
    options.metaILA_SummaryFinalTestId=this.metaILA.metaILA_SummaryTest_FinalTestId;
    options.metaILA_SummaryRetakeTestId=this.metaILA.metaILA_SummaryTest_RetakeTestId;
    options.studentEvaluationId=this.metaILA.studentEvaluationId;
    options.providerId = this.firstFormGroup.get('provider').value;
    await this.metaILAService.create(options).then(async (res) => {
      this.alert.successToast('Meta ' + await this.labelPipe.transform('ILA') + ' Created Successfully');
      if (res) {
        this.makeMetaILAMemeberLinks(res.message.id);
        this.enrollStudents(res.message.id);
        this.metaILA = res.message;
      }
    }).catch(async (err) => {
      this.alert.errorToast('Error creating Meta ' + await this.labelPipe.transform('ILA') );
    })
  }

  makeMetaILAMemeberLinks(metaILAId: any) {
      var options = new MetaILAMembersListOptions();
      this.clonedIla.forEach((i, index) => {
        var option = new MetaILAMembersLinkOptions();
        option.metaILAID = metaILAId;
        option.iLAID = i.id;
        option.metaILAConfigPublishOptionID = i.release_method_id;
        option.sequenceNumber = index + 1;
        option.startDate = i.startDate;
        options.ilaMetaILAMembers.push(option);
      })
      this.metaILAService.createMetaILAMemeberLink(metaILAId, options).then(async (res) => {
        this.alert.successToast('Meta ' + await this.labelPipe.transform('ILA') + ' Member Links Created Successfully');
      this.versionMetaILA(metaILAId);
      }).catch(async (err) => {
        this.alert.errorToast('Error Creating Meta ' + await this.labelPipe.transform('ILA') + ' Member Links' + err);
      });
  }
  async enrollStudents(metaILAId:string) {
    var options = new MetaILAEmployeesLinkOptions()
    options.metaILAIDs.push(metaILAId);
    options.isComingFrom = "metaILAWizard";
    if(this.clonedMetaILAEmployees.length>0){
      options.employeeIDs = this.clonedMetaILAEmployees.map((data)=>{ return data.employeeId});
      await this.metaILAService.linkMetaILAEmployee(options).then((res)=>{
        this.metaILA.metaILA_EmployeeVM=res;
      })
    }
  }

  updateMetaILAMemberLinks(metaILAId: any) {
    var options = new MetaILAMembersLinkOptions();
      options.metaILAID = metaILAId;
      this.clonedIla.forEach((i, index) => {
        options.iLAID = i.id;
        options.metaILAConfigPublishOptionID = i.release_method_id;
        options.sequenceNumber = index + 1;
        options.startDate = i.release_method=='On Demand'? i.startDate : null;
        this.metaILAService.updateMetaILAMemberLink(options).then(async (res) => {
          this.alert.successToast('Meta ' + await this.labelPipe.transform('ILA') + ' Member Links Updated Successfully');
        this.versionMetaILA(metaILAId);
      }).catch(async (err) => {
        this.alert.errorToast('Error Updating Meta ' + await this.labelPipe.transform('ILA') + ' Member Links' + err);
      });
    })
  }

  versionMetaILA(metaILAId: any) {
    var options = new Version_MetaILACreateOptions();
    options.metaILAId = metaILAId;
    options.metaILAName = this.firstFormGroup.get('metaTitle')?.value;
    options.metaILADesc = this.firstFormGroup.get('metaDescription')?.value ?? "";
    options.metaILAStatusId = null;
    options.reason = this.fourthFormGroup.get('RevisionNumber')?.value;
    this.version_metaILA.create(options).then((res) => {
      //this.alert.successToast('Version Meta ILA Created Successfully');
     
    }).catch(async (err) => {
      this.alert.errorToast('Error Creating Version For Meta ' + await this.labelPipe.transform('ILA')  + err);
    });

  }

  onReady(editor: any) {
    //
    editor.plugins.get('FileRepository').createUploadAdapter = function (
      loader: any
    ) {
      return new UploadAdapter(loader);
    };
  }

  drop(event: CdkDragDrop<any[]>) {
    moveItemInArray(this.clonedIla, event.previousIndex, event.currentIndex);

    if (this.clonedIla.length > 0) {
      this.clonedIla[0].release_method = "On Demand";
    }
  }

  ilaConfigChanged(e: any) {
    this.selectedILAConfig = e.value;
  }
  setReleaseMethod(e: Event, configId: any, ilaid: any) {
    let value = (e.target as HTMLElement).innerText;
    let index = this.clonedIla.findIndex((x) => x.id == ilaid);
    this.clonedIla[index].release_method = value;
    this.clonedIla[index].release_method_id = configId;
    const allILAsHaveValues = this.clonedIla.every(item => item.release_method !== undefined && item.release_method !== null && item.release_method !== "");
    this.configCheck = allILAsHaveValues;
  }

   async openDialog(templateRef:any): Promise<Promise<void>> {
    this.header = "Publish Meta " +  await this.labelPipe.transform('ILA') ;
      this.description = "Publishing this Meta " + await this.labelPipe.transform('ILA') + " will save the configurations below and group all " + await this.labelPipe.transform('ILA') + "s to be deployed to " + await this.labelPipe.transform('Employee') + "s when enrolled in the Meta " + await this.labelPipe.transform('ILA');
      const dialogRef = this.dialog.open(templateRef, {
        width: '600px',
        height: 'auto',
        hasBackdrop: true,
        disableClose: true,
      });
  }

  async PublishMetaILA() {
    this.metaILA.metaILAStatusId = this.metaIlaStatuses.find((s) => s.name === "Published").id;
    await this.updateMetaILA();
    this.flyPanelCloseCheck.emit(true);
    this.flyPanelSrvc.close();
  }

  async selectedChanged() {
    if(this.isCreateMode===true && this.metaILA.id===undefined){
      await this.saveMetaILA();
    }else{
      await this.updateMetaILA();
    }
    setTimeout(()=>{
      this.stepper.next();
    },100);
  }

  async updateMetaILA() {
    var options = new MetaILAUpdateOptions();
    options.name = this.firstFormGroup.get('metaTitle')?.value;
    options.description = this.firstFormGroup.get('metaDescription')?.value ?? "";
    options.providerId = this.firstFormGroup.get('provider')?.value ?? "";
    options.effectiveDate = this.fourthFormGroup.get('EffectiveDate')?.value;
    if(this.mode === "edit"){
      options.metaILAStatusId = this.metaILA.metaILAStatusId;
    } else {
      options.metaILAStatusId = (this.isNERCProvider ? this.currentStepIndex == 5 :this.currentStepIndex === 4) ? 
      this.metaIlaStatuses.find((s) => s.name === "Published").id : 
      this.metaIlaStatuses.find((s) => s.name === "Draft").id;
    }
    options.metaILA_SummaryFinalTestId = this.metaILA.metaILA_SummaryTest_FinalTestId;
    options.metaILA_SummaryRetakeTestId = this.metaILA.metaILA_SummaryTest_RetakeTestId;
    options.studentEvaluationId = this.metaILA.studentEvaluationId;
    if(this.currentStepIndex == 4){
      options.reason = this.fourthFormGroup.get('RevisionNumber')?.value;
    }
    await this.metaILAService.updateAsync(this.metaILA.id, options).then(async (res) => {
      if(this.isNERCProvider ? this.currentStepIndex == 5 : this.currentStepIndex == 4){
        this.alert.successToast('Meta ' + await this.labelPipe.transform('ILA') + ' Published Successfully');
      }
      else{
        this.alert.successToast('Meta ' + await this.labelPipe.transform('ILA') + ' Updated Successfully');
      }
      if (res) {
        if(this.currentStepIndex == 1){
          this.updateMetaILAMemberLinks(res.id);
        }
        this.metaILA = res;
      }
    }).catch(async (err) => {
      this.alert.errorToast('Error updating Meta ' +  await this.labelPipe.transform('ILA'));
    })
  }

  async settingDialogue() {
    this.header = 'Publish Meta ' + await this.labelPipe.transform('ILA');
    this.description = 'Publishing this Meta ' + await this.labelPipe.transform('ILA') + ' will save the configurations below and group all ' + await this.labelPipe.transform('ILA') + 's' +
      'to be deployed to ' + await this.labelPipe.transform('Employee') + 's when enrolled In the Meta ' + await this.labelPipe.transform('ILA') + '.' +
      'Are you sure you want to continue?';
    this.confirmText = 'Yes';
  }

  openFlyPanelMetaILAILARequirements(ilaId: string) {
    this.ilaId = ilaId;
    this.isFlyPanelMetaOpenRequirements = true;
  }

  openFlyPanelCreateMetaILATest(mode: string,testType:string) {
    this.testMode=mode;
    this.testType = testType;
    this.isFlyPanelMetaOpenSummary = true;
  }

  openFlyPanelCreateMetaILAStudentEvaluation(event: any) {
    this.isFlyPanelMetaOpenStudEval = true;
  }

  openFlyPanelEditMetaILAStudentEvaluation(event: any) {
    this.isFlyPanelMetaOpenStudEvalEdit = true;
  }

  closeRequirementsFlyPanel(event: any) {
    this.isFlyPanelMetaOpenRequirements = event;
  }

  async closeSummaryFlyPanel(event: { metaILASummaryTestId: any; testType: string }) {
    this.isFlyPanelMetaOpenSummary = false;
    this.isSecondStepInteracted=true;
    if(event.metaILASummaryTestId){
      if(event.testType=='Final Test'){
        this.metaILA.metaILA_SummaryTest_FinalTestId=event.metaILASummaryTestId;
        this.metaILASummaryFinalTestVM=await this.metaILASummaryTestService.getAsync(event.metaILASummaryTestId)
      }
      if(event.testType=='Retake'){
        this.metaILA.metaILA_SummaryTest_RetakeTestId=event.metaILASummaryTestId;
        this.metaILASummaryRetakeTestVM=await this.metaILASummaryTestService.getAsync(event.metaILASummaryTestId)
      }
      this.updateMetaILA();
      this.isThirdStepInteracted=true;
    }
  }

  async closeStudEvalFlyPanel(studentEvaluationId: string) {
    this.isFlyPanelMetaOpenStudEval = false;
    this.isSecondStepInteracted=true;
    if (studentEvaluationId) {
      this.metaILA.studentEvaluationId = studentEvaluationId;
      await this.getStudentEvaluationsAsync();
      this.selectedStudentEvaluationId = studentEvaluationId;
      this.updateMetaILA();
    }
  }

  async closeStudEvalFlyPanelEdit(event: any) {
    this.isFlyPanelMetaOpenStudEvalEdit = false;
    this.isSecondStepInteracted=true;
    await this.getStudentEvaluationsAsync();
    this.selectedStudentEvaluationId = this.metaILA.studentEvaluationId;
  }

  async onSelectionChange(event: any) {
    this.currentStepIndex = event.selectedIndex;
    let stepDifference = event.selectedIndex - event.previouslySelectedIndex;
    if (stepDifference > 1) {
      let matSteps: MatStep[] = this.stepper.steps.toArray();
      for (let i = event.previouslySelectedIndex + 1; i < event.selectedIndex; i++) {
        matSteps[i].interacted = true;
        matSteps[i].completed = true;
      }
    }
   
    this.scrollToTop();
    if(event.selectedIndex==5){
      this.isApplicationPageLoading = true;
      this.nercCertDetails = await this.metaILAService.getMetaILANERCCertificationDetailsAsync(this.metaILA.id);
      this.isApplicationPageLoading = false;
    }
  }

  private scrollToTop() {
    const stepperContainer = document.querySelector('.mat-sidenav-content');
    if (stepperContainer) {
      stepperContainer.scrollTop = 0;
    }
  }

  async getStudentEvaluationsAsync() {
    this.studentEvaluations = await this.studentEvaluationService.getAll();
  }

  async getMetaIlaStatusesAsync(){
    this.metaIlaStatuses = await this.metaIlaStatusService.getAll();
  }

  onSelectionChangeStudentEval(event: any) {
    this.selectedStudentEvaluationId = event.value;
    this.metaILA.studentEvaluationId = this.selectedStudentEvaluationId;
  }

  getFilteredMetaILAConfigArray(index:number){
    let filteredMetaILAConfigArray=this.metaILAConfigArray;
    if(index==0){
      filteredMetaILAConfigArray = filteredMetaILAConfigArray.filter(x => x.description=="On Demand");
    }
    this.clonedIla[0].release_method = "On Demand";
    this.clonedIla[0].release_method_id =this.onDemandReleaseId;
      return filteredMetaILAConfigArray;
  }

  checkThirdStepInteraction(){
    if(this.stepper){
      if(this.isThirdStepInteracted){
        this.setStepsInteraction(2);
      }
      return this.stepper.steps.toArray()[2].interacted;
    }
    else{
      return this.isThirdStepInteracted;
    }
  }
  checkSecondStepInteraction(){
    if(this.stepper){
      if(this.isSecondStepInteracted){
        this.setStepsInteraction(1);
      }
      return this.stepper.steps.toArray()[1].interacted;
    }
    else{
      return this.isSecondStepInteracted;
    }
  }

  setStepsInteraction(index:number){
    for(let i=index; i>0;i--){
      this.stepper.steps.toArray()[index].interacted=true;
      this.stepper.steps.toArray()[index].completed=true;
    }
  }

  async removeILAMemberDialog(index:number,ila: any, templateRef:any): Promise<void> {
    this.ilaDataToRemove = ila;
    this.deletedILAIndex = index;
    this.clonedILAData = index === 0 && this.clonedIla.length > 1 ? this.clonedIla[index + 1] : null;
    this.header = "Remove " + await this.labelPipe.transform('ILA');
      this.description = index === 0 && this.clonedIla.length > 1
      ? `By Deleting <strong>${ila.title}</strong> you will force <strong>${this.clonedILAData.title}</strong> to change its release criteria to On Demand.`
      : `You are going to remove <strong>${ila.title}</strong>.`;
      const dialogRef = this.dialog.open(templateRef, {
        width: '600px',
        height: 'auto',
        hasBackdrop: true,
        disableClose: true,
      });
  }

  async confirmRemoveILAMember() {
    await this.metaILAService.removeMetaILAMemeberLink( this.metaILA.id,this.ilaDataToRemove.id).then(async (res)=>{
      if(res.status==200){
        this.clonedIla = this.clonedIla.filter(x => x.id != this.ilaDataToRemove.id);
        if (this.deletedILAIndex === 0 && this.clonedIla.length > 0) {
          this.clonedIla[0].release_method = "On Demand";
          this.clonedIla[0].release_method_id = this.onDemandReleaseId;
        }
        this.updateMetaILAMemberLinks(this.metaILA.id);
        this.alert.successToast("Meta " + await this.labelPipe.transform('ILA') + " Member Link Removed Successfully");
        const allILAsHaveValues = this.clonedIla.every(item => item.release_method !== undefined && item.release_method !== null && item.release_method !== "");
        this.configCheck = allILAsHaveValues;
      } 
    })
  }

   clearSelectedStudentEvaluation(): void{
    this.selectedStudentEvaluationId = null;
    this.metaILA.studentEvaluationId =this.selectedStudentEvaluationId;
  }

  clearSelectedSummaryTest(testType:string): void{
    if(testType=='Final Test'){
      this.metaILASummaryFinalTestVM = null;
      this.metaILA.metaILA_SummaryTest_FinalTestId = null;
    }
    if(testType=='Retake'){
      this.metaILASummaryRetakeTestVM = null;
      this.metaILA.metaILA_SummaryTest_RetakeTestId = null;
    }
  }
  openLinkILAFlyPanel(){
    this.isFlyPanelLinkILAsToMetaILA = true;
  }

  async closeMetaILAsLinkFlyPanelAsync(){
    this.isFlyPanelLinkILAsToMetaILA = false;
  }

  setMetaILAMember(){
    var ilaMetaILAMembers = this.metaILA.metaILA_ILAMemberVM;
    ilaMetaILAMembers = ilaMetaILAMembers.sort((a, b) => a.sequenceNumber - b.sequenceNumber);
    this.clonedIla = this.mapMetaILAMembers(ilaMetaILAMembers);
    this.getConfigChecks();
  }

  onDemandDateChange(e: any, ilaid: number) {
    this.onDemandSelectedDates[ilaid] = e.target.value;
  
    const time = this.onDemandSelectedTimes[ilaid] ?? '00:00';
  
    const computedDate = new Date(`${this.onDemandSelectedDates[ilaid]}T${time}`);
    this.onDemandStartDates[ilaid] = computedDate;
  
    const index = this.clonedIla.findIndex(x => x.id === ilaid);
    if (index !== -1) {
      this.clonedIla[index].startDate = computedDate;
    }
  }
  
  onDemandTimeChange(e: any, ilaid: number) {
    this.onDemandSelectedTimes[ilaid] = e.target.value;
  
    const date = this.onDemandSelectedDates[ilaid] || new Date().toISOString().split('T')[0];
  
    const computedDate = new Date(`${date}T${this.onDemandSelectedTimes[ilaid]}`);
    this.onDemandStartDates[ilaid] = computedDate;
  
    const index = this.clonedIla.findIndex(x => x.id === ilaid);
    if (index !== -1) {
      this.clonedIla[index].startDate = computedDate;
    }
  }

  get isOnDemandDatesValid(): boolean {
    return this.clonedIla.every(item => {
      if (item.release_method == 'On Demand') {
        return !!item.startDate;
      }
      return true;
    });
  }

  async getAllProvidersAsync(){
    this.providers = await this.providerService.getWihtoutIncludes();
  }

  onProviderChange(){
    var selectedProvider = this.firstFormGroup.get('provider').value;
    this.isNERCProvider = this.providers.find(x=>x.id == selectedProvider)?.isNERC;
  }

  getTotalValues(val:string){
    switch (val){
      case "ceh" :
        return this.nercCertDetails.map(x=>x.totalCEHHours).reduce((item,acc)=>item+acc,0);
      case "standard":
        return this.nercCertDetails.map(x=>x.totalStandardReqHour).reduce((item,acc)=>item+acc,0);
      case "simulation":
        return this.nercCertDetails.map(x=>x.totalSimulationReqHour).reduce((item,acc)=>item+acc,0);
      case "emOp":
        return this.nercCertDetails.map(x=>x.isEmergencyOpHours).every(Boolean)
      default:
        return 0;
    }
  }

  mapMetaILAMembers(array:any[]){
    return array.map((r,index) => ({
      active: r.ilaActive,
      deliveryMethode: r.deliveryMethodName,
      id: r.ilaId,
      image: `https://localhost:44329/${r.ilaImage|| 'null'}`,
      index: index,  
      isMeta: 'Yes',
      nickName: r.ilaNickName,
      num: r.ilaNumber,
      providerId: r.providerId,  
      providerName: r.providerName,
      title: r.ilaName,
      release_method : r.metaILAConfigPublishOptionDescription,
      release_method_id :r.metaILAConfigPublishOptionId,
      startDate:r.startDate ? (r.startDate+'Z').toLocaleString() : null
    }));
  }

  getAddedMetaILALinkedMembers(data:any){
    if (Array.isArray(data)) {
      var memberDatas = this.mapMetaILAMembers(data);
      memberDatas.forEach(item => this.clonedIla.push(item));
    }
    this.getConfigChecks();
  }

  getConfigChecks(){
    const allILAsHaveValues = this.clonedIla.every(item => item.release_method != undefined && item.release_method !== null && item.release_method !== "");
    this.configCheck = allILAsHaveValues;
  }

}


