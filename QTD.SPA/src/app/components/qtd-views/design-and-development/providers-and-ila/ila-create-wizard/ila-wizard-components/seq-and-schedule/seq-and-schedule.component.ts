import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';
import { Component, EventEmitter, Input, OnDestroy, OnInit, Output, ViewChild } from '@angular/core';
import * as ckcustomBuild from 'src/app/ckcustomBuild/build/ckeditor.js';
import { UploadAdapter } from 'src/app/_Shared/Utils/CKEditor_UploadAdapter';
import { Observable } from 'rxjs';
import { StepperOrientation } from '@angular/cdk/stepper';
import { map } from 'rxjs/operators';
import { BreakpointObserver } from '@angular/cdk/layout';
import { SubSink } from 'subsink';
import { Store } from '@ngrx/store';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { MatSort } from '@angular/material/sort';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { IlaService } from 'src/app/_Services/QTD/ila.service';
import { ILA } from 'src/app/_DtoModels/ILA/ILA';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup } from '@angular/forms';
import { ILAEvalMethodVM } from '@models/ILA/ILAEvalMethodVM';
import { IlaEmpTqSettingsComponent } from './ila-emp-tq-settings/ila-emp-tq-settings.component';
import { IlaEmpEvalSettingsComponent } from './ila-emp-eval-settings/ila-emp-eval-settings.component';
import { IlaEmpTestSettingsComponent } from './ila-emp-test-settings/ila-emp-test-settings.component';
import { TrainingEnrollStudentCreationOptions } from '@models/SchedulesClassses/training-creation-options';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { ILADetailsVM } from '@models/ILA/ILADetailsVM';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { ILAUpdateOptions } from '@models/ILA/ILAUpdateOptions';

@Component({
  selector: 'app-seq-and-schedule',
  templateUrl: './seq-and-schedule.component.html',
  styleUrls: ['./seq-and-schedule.component.scss'],
})
export class SeqAndScheduleComponent implements OnInit, OnDestroy {
  @Output() seq_and_schedule = new EventEmitter<any>();
  @ViewChild('sort',{static:false}) sort!:MatSort;
  @ViewChild('paginator',{static:false}) paginator!:MatPaginator;
  @ViewChild('ilaEmpTqSettings') ilaEmpTqSettings!:IlaEmpTqSettingsComponent;
  @ViewChild('ilaEmpEvalSettings') ilaEmpEvalSettings!:IlaEmpEvalSettingsComponent;
  @ViewChild('ilaTestRelease') ilaTestRelease!:IlaEmpTestSettingsComponent;
  @Output() loadingEvent = new EventEmitter<boolean>();
  stepperOrientation: Observable<StepperOrientation>;
  segmentsList:any[];
  ila!:ILADetailsVM;
  trainingForm: UntypedFormGroup;
  linkedPositionToIla: any[] = [];
  evaluationForm: UntypedFormGroup = new UntypedFormGroup({})
  subscription = new SubSink();
  ilaItems: any[];
  ilaId: any = "";
  deliveryMethodName!:any;
  public Editor = ckcustomBuild;
  selectedTab: 'setting' | 'registration' = 'registration';
  selectedType = 'classroom';
  sci_placeholder =
    'Use this space to list special materials needed for class, whether lunch is provided, etc. Information entered into this textbox will be included in the registration email';
  allowSettings = false;
  editor = ckcustomBuild;
  isChecked: boolean;
  @Output() isPublicCourseCheckboxChecked = new EventEmitter<boolean>();
  @Output() isNickNameEmpty = new EventEmitter<{ isChecked: boolean; isEmpty: boolean }>();
  @Input() mode: string;

  constructor(
    public breakpointObserver: BreakpointObserver,
    private ilaService: IlaService,
    private fb: UntypedFormBuilder,
    private alert: SweetAlertService,
    private labelPipe: LabelReplacementPipe,
    private store: Store<{ saveIla: any }>,
    private dataBroadcastService: DataBroadcastService,

  ) {
    this.ilaItems = [
      {
        label: 'PreTest',
        heading: 'Intro to QTD',
        type: 'pretest',
      },
      {
        label: 'Segment 1',
        heading: 'Classroom',
        type: 'classroom',
      },
      {
        label: 'Segment 2',
        heading: 'Procedure Review',
        type: 'procedure_review',
      },
      {
        label: 'Test',
        heading: 'Intro to QTD',
        type: 'test',
      },
      {
        label: 'Retake 1',
        heading: 'Intro to QTD',
        type: 'retake',
      },
      {
        label: 'Segment 3',
        heading: 'Self Study',
        type: 'selfstudy',
      },
      {
        label: 'OJT Tasks',
        heading: '1.3.1 and 1.3.2',
        type: 'ojttasks',
      },
      {
        label: 'Task Qualification Tasks',
        heading: '1.3.1 and 1.3.2',
        type: 'tqt',
      },
    ];

    this.stepperOrientation = breakpointObserver
      .observe('(min-width: 800px)')
      .pipe(map(({ matches }) => (matches ? 'horizontal' : 'vertical')));
  }

  async ngOnInit(): Promise<void> {
    this.loadingEvent.emit(true);
    this.initializeTrainingForm();
    this.subscription.sink = this.store.select('saveIla').subscribe((data) => {
      if ((data['saveData'] !== undefined && data['saveData'] !== null) && (data['saveData']['result'] !== undefined && data['saveData']['result'] !== null)) {
        this.ilaId = data['saveData']['result']['id'];
        this.getILAData();
        this.deliveryMethodName = data['saveData']['result']['deliveryMethodName'];
      }
    })
    this.dataBroadcastService.publicClassEnabled.subscribe((x) => {
      this.isChecked = x;
    })
    this.initializeEvaluationForm();
    await this.readyObjectivesData();
    await this.readyEvalMethodData();
    this.loadingEvent.emit(false);
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  async readyILALinkedPositions(ilaId: any) {
    this.linkedPositionToIla = await this.ilaService.getLinkedPositions(ilaId);
  }

  initializeTrainingForm(){
    this.trainingForm = this.fb.group({
      selfRegPk: new UntypedFormControl(0),
      makeAvailableForSelfReg: new UntypedFormControl(false),
      requireAdminApproval: new UntypedFormControl(false),
      sendApprovedEmail: new UntypedFormControl(false),
      acknowledgeRegDisclaimer: new UntypedFormControl(false),
      regDisclaimer: new UntypedFormControl(''),
      limitForLinkedPositions: new UntypedFormControl(false),
      closeRegOnStartDate: new UntypedFormControl(false),
      enableWaitlist: new UntypedFormControl(false),
      classSize: new UntypedFormControl(30)
    })
    if(this.mode === 'view')
    {
      this.trainingForm.disable();
    }
  }
 initializeEvaluationForm(){
  this.evaluationForm = this.fb.group({
    useForEMP: new UntypedFormControl(false),
    IsPubliclyAvailableILA: new UntypedFormControl(false)    
  })
  if (this.mode === 'view') {
    this.evaluationForm.get('IsPubliclyAvailableILA')?.disable();
    this.evaluationForm.get('useForEMP')?.disable();
  }
 }
  async readyEvalMethodData(){
    var data = await this.ilaService.getEvalMethodData(this.ilaId);
    this.evaluationForm.patchValue({
      useForEMP:data.useForEMP,
      IsPubliclyAvailableILA: data.isPubliclyAvailableILA

    });
    this.allowSettings = data.useForEMP;
  }

  async saveEvalMethodDataAsync(){
    var options = new ILAEvalMethodVM();
    options.useForEMP = this.evaluationForm.get('useForEMP')?.value;
    options.isPubliclyAvailableILA = this.evaluationForm.get('IsPubliclyAvailableILA')?.value;
    await this.ilaService.saveEvalMethodData(this.ilaId,options);
  }

  async getILAData(){
    this.ila = await this.ilaService.get(this.ilaId);
    var sefReg = await this.ilaService.getSelfRegistrationOptionsSettingByILAIdAsync(this.ilaId);
    this.trainingForm.patchValue({
      makeAvailableForSelfReg: sefReg?.makeAvailableForSelfReg,
      requireAdminApproval: sefReg?.requireAdminApproval,
      sendApprovedEmail: sefReg?.sendApprovedEmail,
      acknowledgeRegDisclaimer:sefReg?.acknowledgeRegDisclaimer,
      regDisclaimer:sefReg?.regDisclaimer ?? '',
      limitForLinkedPositions: sefReg?.limitForLinkedPositions,
      closeRegOnStartDate:sefReg?.closeRegOnStartDate,
      enableWaitlist:sefReg?.enableWaitlist,
      classSize: sefReg?.classSize,
    }); 
    if(this.ila.isPubliclyAvailable){
      this.trainingForm.get('makeAvailableForSelfReg').setValue(true);
      this.trainingForm.get('makeAvailableForSelfReg').disable();
    }
  }

  useEmpForILA(e:any){
    if(!e.checked){
      this.selectedTab ="registration";
    }
    this.allowSettings = e.checked;
  }

  async readyObjectivesData(){
    this.segmentsList=await this.ilaService.getLinkedSegments(this.ilaId);
        this.segmentsList.forEach(element => {
          var data=element.segmentObjective_Link;
          element.segmentObjective_Link=new MatTableDataSource<any>();
          element.segmentObjective_Link.data = data;
          element.segmentObjective_Link.paginator = this.paginator;
          element.segmentObjective_Link.sort = this.sort;
    });
  }

  enableSendEMail(event) {
    if (event.checked) {
        this.trainingForm.get('sendApprovedEmail')?.setValue(true);
    } else {
        this.trainingForm.get('sendApprovedEmail')?.setValue(false);
    }
  }

  async saveUpdateEnrollStudent() {
    let createOpt: TrainingEnrollStudentCreationOptions = {
        ilaId: this.ilaId,
        makeAvailableForSelfReg: this.trainingForm.get('makeAvailableForSelfReg')
            ?.value,
        requireAdminApproval: this.trainingForm.get('requireAdminApproval')
            ?.value,
        sendApprovedEmail: this.trainingForm.get('sendApprovedEmail')?.value,
        acknowledgeRegDisclaimer: this.trainingForm.get(
            'acknowledgeRegDisclaimer'
        )?.value,
        regDisclaimer: this.trainingForm.get('regDisclaimer')?.value,
        limitForLinkedPositions: this.trainingForm.get('limitForLinkedPositions')
            ?.value,
        closeRegOnStartDate: this.trainingForm.get('closeRegOnStartDate')?.value,
        classSize: this.trainingForm.get('classSize')?.value,
        enableWaitlist: this.trainingForm.get('enableWaitlist')?.value,
    };
    await this.ilaService.createILA_SelfRegistrationAsync(createOpt).then(async (res) => {
      this.alert.successToast(await this.labelPipe.transform('ILA') + " Self Registration Updated successfully.");
    })
  }

  drop(event: CdkDragDrop<any[]>) {
    moveItemInArray(this.ilaItems, event.previousIndex, event.currentIndex);
  }

  onReady(editor: any) {
    editor.plugins.get('FileRepository').createUploadAdapter = function (
      loader: any
    ) {
      return new UploadAdapter(loader);
    };
  }

  selectedChanged(event: any) {

  }

  async saveSequenceAndScheduleDataAsync(){
    await this.saveEvalMethodDataAsync();
    await this.saveUpdateEnrollStudent();
  }
  
  async disableSelfRegCheckbox(e: any){
    var options = new ILAUpdateOptions();
    options.isPubliclyAvailableILA = this.evaluationForm.get('IsPubliclyAvailableILA')?.value;
    if(e.checked){
    this.trainingForm.get('makeAvailableForSelfReg').setValue(true);
    setTimeout(() => {
      this.trainingForm.get('makeAvailableForSelfReg')?.disable();
    });
    

    await this.ilaService.updateIspubliclyAvailableIla(this.ilaId, options);
    if(this.ila.nickName == null || this.ila.nickName == ''){
      this.isNickNameEmpty.emit({
      isChecked: e.checked,
      isEmpty: true
      });
    }
   }
   else{
    this.trainingForm.get('makeAvailableForSelfReg').setValue(false);
    this.trainingForm.get('makeAvailableForSelfReg')?.enable();
    await this.ilaService.updateIspubliclyAvailableIla(this.ilaId, options);
  }
  this.isPublicCourseCheckboxChecked.emit(e.checked);
}
}
