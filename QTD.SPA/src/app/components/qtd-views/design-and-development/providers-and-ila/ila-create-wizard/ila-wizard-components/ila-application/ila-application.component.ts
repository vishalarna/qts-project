import { NercTargetAudienceService } from './../../../../../../../_Services/QTD/nerc-target-audience.service';
import { Component, Input, OnInit, Renderer2 } from '@angular/core';
import {
  UntypedFormBuilder,
  UntypedFormControl,
  UntypedFormGroup,
  ValidatorFn,
  Validators,
} from '@angular/forms';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { Router } from '@angular/router';
import { Output, EventEmitter } from '@angular/core';
import { MaximumOneCheckBox, atleastOneCheckBox } from './atleast-one-checkbox.validator';
import { NERCTargetAudience } from 'src/app/_DtoModels/NERCTargetAudience/NERCTargetAudience';
import { TrainingTopics } from 'src/app/_DtoModels/NERCTargetAudience/TrainingTopics';
import { AssesmentTool } from 'src/app/_DtoModels/NERCTargetAudience/AssesmentTool';
import { IlaService } from 'src/app/_Services/QTD/ila.service';
import { Store } from '@ngrx/store';
import { SubSink } from 'subsink';
import { IlaApplicationSaveModel } from 'src/app/_DtoModels/ILA/IlaApplicationSaveModel';
import { DatePipe } from '@angular/common';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { HttpResponse } from '@angular/common/http';
import { ReportSkeleton } from '@models/ReportSkeleton/ReportSkeleton';
import { ReportUpdateOptions } from '@models/Report/ReportUpdateOptions';
import { ReportSkeletonColumn } from '@models/ReportSkeleton/ReportSkeletonColumn';
import { ApiReportsService } from 'src/app/_Services/QTD/Reports/api.reports.service';
import { ReportFilterOption } from '@models/Report/ReportFilterOption';
import { ReportExportOptions, ReportExportType } from '@models/Report/ReportExportOptions';

@Component({
  selector: 'app-ila-application',
  templateUrl: './ila-application.component.html',
  styleUrls: ['./ila-application.component.scss'],
})
export class IlaApplicationComponent implements OnInit {
  ILAApplicationForm!: UntypedFormGroup;
  ilaId: any;
  assessmentCheckBoxes = new UntypedFormGroup(
    {
      assessment_tool: new UntypedFormControl(''),
      others: new UntypedFormControl(false),
      writtenOrOnline:new UntypedFormControl(false),
      perform:new UntypedFormControl(false),
    }
  );

  TRainingTopicForm = new UntypedFormGroup({
    others: new UntypedFormControl(false),
  });
  subscription = new SubSink();



  NercTargetAudienceForm = new UntypedFormGroup({
    others: new UntypedFormControl(false),
    othersData: new UntypedFormControl('')
  });
  icon: boolean = false;
  preview_application: boolean = false;
  nercTargetArray: any[]

  pilot = [
    { id: 1, value: 'This activity conforms to the criteria for System Operator Credential Maintenance' },
    { id: 2, value: 'Pilot Data is attached. Select N/A if not applicable' },
    { id: 3, value: 'N/A' }
  ]
  datePipe = new DatePipe('en-us');

  @Input() assessment_tool!: string;
  @Input() mode: string;
  @Output() previewEvent = new EventEmitter<string>();
  @Output() loadingEvent = new EventEmitter<boolean>();
  accordion_titles: TrainingTopics[];
  assesmentTools: AssesmentTool[];
  ilaNercCertDetails:any[];
  reportSkeleton: ReportSkeleton;
  reportSkeletonName: string;
  reportCreateorUpdate:ReportUpdateOptions;
  displayColumns:ReportSkeletonColumn[];
  spinner:boolean = false;
  constructor(
    private fb: UntypedFormBuilder,
    private dialog: MatDialog,
    private router: Router,
    private renderer: Renderer2,
    private nercTargetService: NercTargetAudienceService,
    private _ilaService: IlaService,
    private store: Store<{ saveIla: any }>,
    private alert: SweetAlertService,
    private apireportService: ApiReportsService,
  ) { }

  ngOnInit(): void {
    this.loadingEvent.emit(true);
    this.subscription.sink = this.store.select('saveIla').subscribe((data) => {
      if ((data['saveData'] !== undefined && data['saveData'] !== null) && (data['saveData']['result'] !== undefined && data['saveData']['result'] !== null)) {
        this.ilaId = data['saveData']['result']['id'];
      }
    });
    this.readyILAApplicationForm();
    this.readyNercTargetAudience();
    this.readyTrainingTopics();
    this.readyAssesmentTools();
    this.loadingEvent.emit(false);
    this.readyCourseDetails();
    this.reportSkeletonName = 'NERC ILA Application - Report Version';
    this.getReportSkeletonData();
    if (this.mode === 'view') {
      this.disableForms();
    }  
  }
  disableForms() {
    this.ILAApplicationForm.disable();
    this.assessmentCheckBoxes.disable();
    this.TRainingTopicForm.disable();
    this.NercTargetAudienceForm.disable();
  }

  saveSpinner = false;

  saveApplicationInfo() {
    this.saveSpinner = true;
    var model = new IlaApplicationSaveModel()
    model.ilaId = this.ilaId;
    model.startDate = this.ILAApplicationForm.get('startDate')?.value;
    model.applicationSubmissionDatertDate = this.ILAApplicationForm.get('applicationSubmissionDate')?.value;
    model.approvalDate = this.ILAApplicationForm.get('approvalDate')?.value;
    model.expirationDate = this.ILAApplicationForm.get('expirationDate')?.value;
    model.performAssessmentTool = this.assessmentCheckBoxes.get('perform')?.value;
    model.wriitenOrOnlineAssessmentTool = this.assessmentCheckBoxes.get('writtenOrOnline')?.value;
    //pilot data
    model.doesActivityConform = this.ILAApplicationForm.get('doesActivityConform')?.value;
    model.hasPilotData = this.ILAApplicationForm.get(['pilotCheckBoxes', 'pilot2'])?.value ?? false;
    model.hasPilotDataNA = this.ILAApplicationForm.get(['pilotCheckBoxes', 'pilot3'])?.value ?? false;
    model.otherAssesmentTool = this.assessmentCheckBoxes.get('assessment_tool')?.value;
    //Tageted Audience
    var audienceIds: any[] = [];

    this.nercTargetArray?.forEach(element => {
      if (this.NercTargetAudienceForm.get('nercTarget' + element.id)?.value === true) {
        audienceIds.push(element.id);
      }
    });
    model.nercTargetIds = audienceIds;
    model.otherNercTargetAudience = this.NercTargetAudienceForm.get('othersData')?.value;
    //ready training topics
    var topicIds: any[] = [];
    this.accordion_titles?.forEach(element => {
      element.trainingTopics?.forEach(e2 => {
        if (this.TRainingTopicForm.get(e2.id + 'ttopic')?.value === true) {
          topicIds.push(e2.id);
        }
      });
    });
    model.trainingTopicIds = topicIds;

    this._ilaService.updateIlaApplicationData(model).then((res: any) => {
      this.alert.successToast("Application Details Updated Successfully");

    }).finally(()=>{
      this.saveSpinner = false;
    });

  }

  readyILAApplicationForm() {
    this.ILAApplicationForm = this.fb.group({
      startDate: new UntypedFormControl(''),
      applicationSubmissionDate: new UntypedFormControl(''),
      approvalDate: new UntypedFormControl(''),
      expirationDate: new UntypedFormControl(''),
      doesActivityConform:new UntypedFormControl(false),
      pilotCheckBoxes: new UntypedFormGroup(
        {
          pilot2: new UntypedFormControl(false),
          pilot3: new UntypedFormControl(false)
        },
      ),
    });
  }
  pilotChanged(event: any, formval: any) {
    if (event.checked) {
      this.ILAApplicationForm.get(['pilotCheckBoxes', 'pilot2'])?.setValue(false);
      this.ILAApplicationForm.get(['pilotCheckBoxes', 'pilot3'])?.setValue(false);
      this.ILAApplicationForm.get(['pilotCheckBoxes', formval])?.setValue(true);
    }
  }
  readyNercTargetAudience() {
    this.nercTargetService.getAll().then((res: any) => {
      res.forEach(element => {
        this.NercTargetAudienceForm?.addControl(
          'nercTarget' + element.id,
          new UntypedFormControl(false)
        );
      });
      this.nercTargetArray = res;
      if (this.mode === 'view') {
        this.NercTargetAudienceForm.disable();
      }

    })
  }

  readyTrainingTopics() {
    this.nercTargetService.getAllTrainingTopics().then((res: any) => {

      res.forEach(element => {
        element.trainingTopics?.forEach(e2 => {
          this.TRainingTopicForm?.addControl(
            e2.id + 'ttopic',
            new UntypedFormControl(false)
          );
        });

      });
      this.accordion_titles = res;
      if (this.mode === 'view') {
        this.TRainingTopicForm.disable();
      }

    })
  }

  loader = false;

  readyAssesmentTools() {
    this.loader = true;
      this._ilaService.getAllilaApplicationData(this.ilaId).then((res: any) => {
        this.ILAApplicationForm.get('startDate')?.setValue(this.datePipe.transform(res.startDate, "yyyy-MM-dd"));
        this.ILAApplicationForm.get('applicationSubmissionDate')?.setValue(this.datePipe.transform(res.applicationSubmissionDate, "yyyy-MM-dd"));
        this.ILAApplicationForm.get('approvalDate')?.setValue(this.datePipe.transform(res.approvalDate, "yyyy-MM-dd"));
        this.ILAApplicationForm.get('expirationDate')?.setValue(this.datePipe.transform(res.expirationDate, "yyyy-MM-dd"));
        this.assessmentCheckBoxes.get('assessment_tool')?.setValue(res.otherAssesmentTool);
        this.NercTargetAudienceForm.get('othersData')?.setValue(res.otherNercTargetAudience);
        this.NercTargetAudienceForm.get('others')?.setValue(res.otherNercTargetAudience === null ? false : true);
        this.assessmentCheckBoxes.get('others')?.setValue(res.otherAssesmentTool === null || res.otherAssesmentTool === undefined || res.otherAssesmentTool === '' ? false : true);
        this.ILAApplicationForm.get(['pilotCheckBoxes', 'pilot3'])?.setValue(res.hasPilotDataNA ?? false);
        this.ILAApplicationForm.get(['pilotCheckBoxes', 'pilot2'])?.setValue(res.hasPilotData ?? false);
        this.ILAApplicationForm.get('doesActivityConform')?.setValue(res.doesActivityConform ?? false);
        this.assessmentCheckBoxes.get('writtenOrOnline')?.setValue(res.wriitenOrOnlineAssessmentTool ?? false);
        this.assessmentCheckBoxes.get('perform')?.setValue(res.performAssessmentTool ?? false);
        //Tageted Audience
        res.targetedAudienceIds.forEach(element => {
          this.NercTargetAudienceForm.get('nercTarget' + element)?.setValue(true);
        });

        //ready training topics
        res.operatorsTrainingTopics.forEach(element => {
          this.TRainingTopicForm.get(element.trTopicId + 'ttopic')?.setValue(true);
        });
      }).finally(()=>{
        this.loader = false;
      });
    // })
  }


  async readyCourseDetails(){
    if(this.ilaId){
      this.ilaNercCertDetails = await this._ilaService.getILANERCCertificationDetailAsync(this.ilaId);
    }
  }


  async downloadAsCSV() {
     var response=await this._ilaService.exportAsCSVAsync(this.ilaId);
     this.handleFileDownload(response);
  }

  private handleFileDownload(response: HttpResponse<Blob>) {
    const contentDispositionHeader = response.headers.get('content-disposition');

    const fileName = contentDispositionHeader
      ? contentDispositionHeader.split(';')[1].trim().split('=')[1].replace(/["']/g, "")
      : 'downloaded-file.csv';

    const blob = new Blob([response.body!], { type: 'application/octet-stream' });
    const url = window.URL.createObjectURL(blob);

    const link = document.createElement('a');
    link.href = url;
    link.download = fileName;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
  }

  public async downloadILAApplicationReport(){
    this.spinner = true;
    this.getCreateUpdateOptions();
    var reportExportOption = new ReportExportOptions();
    reportExportOption.exportType = ReportExportType.Pdf;
    reportExportOption.options = this.reportCreateorUpdate;
    await this._ilaService.generateNERCILAApplicationReportAsync(reportExportOption).then((res) => {
          this.handleFileDownload(res);
    });
    this.spinner = false;
  }

  async getReportSkeletonData() {
    this.reportSkeleton = await this.apireportService.getReportSkeletonByNameAsync(this.reportSkeletonName);
    this.displayColumns =  Object.assign(this.reportSkeleton?.displayColumns);
  }

  getCreateUpdateOptions(){
      var reportCreateOptions = new ReportUpdateOptions();
      this.displayColumns.map(item=>{
        reportCreateOptions.getDisplayColumns(item.columnName)
      })
      var reportFilters = Array<ReportFilterOption>();
      var ilaFilter  = this.reportSkeleton?.availableFilters?.find(x=>x.name.toLowerCase() =='select ila');
      const ilaIdFilter = new ReportFilterOption(ilaFilter.name,this.ilaId);
      reportFilters.push(ilaIdFilter);
      reportCreateOptions.filters = reportFilters;
      reportCreateOptions.reportSkeletonId = this.reportSkeleton?.id;
      reportCreateOptions.internalReportTitle = this.reportSkeletonName;
      this.reportCreateorUpdate = reportCreateOptions;
    }

}
