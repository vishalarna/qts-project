import {SelectionModel} from '@angular/cdk/collections';
import {BreakpointObserver} from '@angular/cdk/layout';
import {TemplatePortal} from '@angular/cdk/portal';
import {StepperSelectionEvent} from '@angular/cdk/stepper';
import {DatePipe} from '@angular/common';
import {Component, OnInit, TemplateRef, ViewChild} from '@angular/core';
import {UntypedFormControl, UntypedFormGroup, Validators} from '@angular/forms';
import {MatLegacyDialog as MatDialog} from '@angular/material/legacy-dialog';
import {MatSort} from '@angular/material/sort';
import {StepperOrientation} from '@angular/material/stepper';
import {MatLegacyTableDataSource as MatTableDataSource} from '@angular/material/legacy-table';
import {ActivatedRoute, Router} from '@angular/router';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {SubSink} from 'subsink';
import {AddEmployeeToProcedureReviewCreationOptions, CreateProcedureReview} from '@models/Procedure/Procedure_review';
import {IssuingAuthoritiesService } from 'src/app/_Services/QTD/issuing-authorities.service';
import {ProceduresService} from 'src/app/_Services/QTD/procedures.service';
import {DataBroadcastService} from 'src/app/_Shared/services/DataBroadcast.service';
import {FlyInPanelService} from 'src/app/_Shared/services/flyInPanel.service';
import {SweetAlertService} from 'src/app/_Shared/services/sweetalert.service';
import * as ckcustomBuild from 'src/app/ckcustomBuild/build/ckeditor.js';
import {ApiClientSettingsService} from '../../../../../_Services/QTD/ClientSettings/api.clientsettings.service';
import { ProcedureUpdateOptions } from '@models/Procedure/ProcedureUpdateOptions';
import { Procedure } from '@models/Procedure/Procedure';
import { LicenseHelperService } from 'src/app/_Shared/services/licenseHelper.service';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';


@Component({
  selector: 'app-fly-panel-add-procedure-review',
  templateUrl: './fly-panel-add-procedure-review.component.html',
  styleUrls: ['./fly-panel-add-procedure-review.component.scss'],
})
export class FlyPanelAddProcedureReviewComponent implements OnInit {
  procedureForm: UntypedFormGroup;
  stepperOrientation: Observable<StepperOrientation>;
  disableValidation = false;
  procedures_list: any[] = [];
  initialProcedures_List: any[] = [];
  authority_list: any[] = [];
  initialIssuingAuthorityList: any[] = [];
  procedureReviewId: string | null = null;
  datePipe = new DatePipe('en-us');
  releaseDateTime: Date;
  dueDateTime: Date;
  notificationEnabled = false;
  todayDate = this.datePipe.transform(new Date(), 'yyyy-MM-dd');
  confirmDialogDesc = 'Leaving this page will remove any unsaved changes.';
  isSpinner = true;
  isILALoading = false;
  noILAFound = false;
  noAuthFound = false;
  isProcedureReviewPublished = false;
  editor = ckcustomBuild;
  subscription = new SubSink();
  empDataSource = new MatTableDataSource<any>();
  selection = new SelectionModel<any>(true, []);
  stepIndex = 0;
  procedureReview;
  isLicenseValid:boolean=true;



  displayedColumns: string[] = [
    'index',
    'imageUrl',
    'empName',
    'position',
    'organization',
    'action',
  ];

  @ViewChild(MatSort)
  set tblSort(sort: MatSort) {
    if (sort) {
      this.empDataSource.sort = sort;
    }
  }

  constructor(
    public readonly dialog: MatDialog,
    public readonly breakpointObserver: BreakpointObserver,
    public readonly flyPanelSrvc: FlyInPanelService,
    private readonly issuingAuthoritiesService: IssuingAuthoritiesService,
    private readonly procedureService: ProceduresService,
    private readonly alert: SweetAlertService,
    private readonly route: ActivatedRoute,
    private readonly dataBroadcastService: DataBroadcastService,
    private readonly router: Router,
    private readonly clientSettingsService: ApiClientSettingsService,
    private licenseHelper:LicenseHelperService,
    private labelPipe: LabelReplacementPipe,
    )
  {
    this.stepperOrientation = breakpointObserver
      .observe('(min-width: 800px)')
      .pipe(map(({ matches }) => (matches ? 'horizontal' : 'vertical')));

    this.procedureForm = new UntypedFormGroup({
      issueAuthorityId: new UntypedFormControl(''),
      procedureId: new UntypedFormControl({value: null, disabled: this.isILALoading}, Validators.required),
      reviewTitle: new UntypedFormControl('', Validators.required),
      startDate: new UntypedFormControl('', Validators.required),
      startTime: new UntypedFormControl('', Validators.required),
      endDate: new UntypedFormControl('', Validators.required),
      endTime: new UntypedFormControl('', Validators.required),
      courseInstruction: new UntypedFormControl(''),
      acknowledgement:new UntypedFormControl(''), 
      isEmployeeShowResponses: new UntypedFormControl(false),
      procedureFileName: new UntypedFormControl({ value: '', disabled: true }),
      procedureLink: new UntypedFormControl({ value: '', disabled: true }),
      searchTxt: new UntypedFormControl(''),
      searchProcedureTxt: new UntypedFormControl(''),
      // To Send Server
      startDateTime: new UntypedFormControl(''),
      endDateTime: new UntypedFormControl(''),
    });
  }

  ngOnInit(): void {
    this.checkLicense();
    if(this.isLicenseValid){
    this.subscription.sink =
      this.dataBroadcastService.refreshProcedureReviewData.subscribe((_) => {
        this.fetchEmployees();
      });

    this.fetchAuthority();

    this.clientSettingsService.getNotificationByName('EMP Procedure Review').then(r => {
      this.notificationEnabled = r.enabled;
    });

    this.route.params.subscribe((params) => {
      if (params.hasOwnProperty('id')) {
        this.procedureReviewId = params['id'];
        this.disableValidation = true;

        this.fetchProcedureReview(this.procedureReviewId);
        this.fetchEmployees();
      }
    });

    this.route.queryParams.subscribe((params) => {
      this.stepIndex = Number.parseInt(params['index'] ?? 0);
    });
  }
  else{
    this.router.navigate(['procedure/overview']);
  }
  }
  checkLicense(){
    var license = this.licenseHelper.getLicenseData();
    if(!license?.deluxe || !license?.hasEmp){
      this.isLicenseValid = false;
    }
  }

  selectAuthority(event: any) {
    if (event !== undefined) {
      this.isILALoading = true;
      this.procedures_list = event.procedures.sort((a,b)=>a.number.localeCompare(b.number));
      this.initialProcedures_List = Object.assign(this.procedures_list);

      if (this.procedures_list.length == 0) {
        this.noILAFound = true;
        this.isILALoading = false;
      }
      else {
        this.noILAFound = false;
        this.isILALoading = false;
      }
    }
    else {
      this.fetchProcedureReview(this.procedureReviewId);
    }
  }

  selectedChanged(event: StepperSelectionEvent) {
    if (event.selectedIndex === 1 && event.previouslySelectedIndex === 0) {
      this.saveChanges();
    }

    this.stepIndex = event.selectedIndex;
  }

  openFlyInPanelAddEmployee(templateRef: TemplateRef<any>) {
    const portal = new TemplatePortal(templateRef, null);
    this.flyPanelSrvc.open(portal);
  }

  /** Whether the number of selected elements matches the total number of rows. */
  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.empDataSource.data.length;
    return numSelected === numRows;
  }

  /** Selects all rows if they are not all selected; otherwise clear selection. */
  toggleAllRows() {
    if (this.isAllSelected()) {
      this.selection.clear();
      return;
    }

    this.selection.select(...this.empDataSource.data);
  }

  /** The label for the checkbox on the passed row */
  checkboxLabel(row?: any): string {
    if (!row) {
      return `${this.isAllSelected() ? 'deselect' : 'select'} all`;
    }
    return `${this.selection.isSelected(row) ? 'deselect' : 'select'} row ${row.position + 1}`;
  }

  async openConfirmDialog(templateRef: TemplateRef<any>) {
    if (!this.procedureForm.valid) {
      this.confirmDialogDesc = 'Leaving this page will remove any unsaved changes.';
    }
    else if (!this.isProcedureReviewPublished) {
      this.confirmDialogDesc = 'Leaving this page will save the ' + await this.transformTitle('Procedure') + ' Review as a draft.';
    }
    else {
      this.confirmDialogDesc = 'Leaving this page will save the ' + await this.transformTitle('Procedure') + ' Review as published.';
    }

    this.openDialog(templateRef);
  }

  openDialog(templateRef: TemplateRef<any>) {
    this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  saveChanges() {
    if (!this.procedureForm.valid) {
      return;
    }

    if (this.procedureReviewId) {
      this.updateProcedureReview();
    }
    else {
      this.addProcedureReview();
    }
  }

  saveChangesAndExit() {
    this.saveChanges();
    this.router.navigate(['procedure/overview']);
  }

  selectProcedure(event: any) {
    if (!event) {
      return;
    }
    this.procedureForm.patchValue({
      procedureId: event,
      procedureLink: event.hyperlink,
      procedureFileName: event.fileName,      
    });
      if(!event.fileName && !event.hyperlink){
      this.procedureForm.get('isEmployeeShowResponses').disable();
    }
  }


  async removeEMP() {
    const options = new AddEmployeeToProcedureReviewCreationOptions();
    options.procedureReviewId = this.procedureReviewId;
    options.employeeIds = this.selection.selected.map((data) => data.empId);

    await this.procedureService.unLinkProcedureReviewEmp(options.procedureReviewId, options);

    this.fetchEmployees();
    this.selection.clear();
    this.dialog.closeAll();
    this.alert.successToast(await this.transformTitle('Procedure') + ' review ' + await this.labelPipe.transform('Employee') + ' has been deleted');
  }

  publishData() {
    this.procedureService
      .publishProcedureReview(this.procedureReviewId)
      .then(async (_) => {
        this.alert.successToast(await this.transformTitle('Procedure') + ' review Successfully Published');
        this.router.navigate(['procedure/overview']);
      });
  }

  issuingAuthoritySearch() {
    let filterString = this.procedureForm.get('searchTxt')?.value ?? '';

    if (filterString) {
      filterString = String(filterString).trim().toLowerCase();
    }

    this.authority_list = this.initialIssuingAuthorityList.filter((f) => {
      const title = `${f?.title}`;
      return title.toLowerCase().includes(filterString);
    });
  }

  procedureSearch() {
    let filterString = this.procedureForm.get('searchProcedureTxt')?.value ?? '';

    if (filterString) {
      filterString = String(filterString).trim().toLowerCase();
    }

    this.procedures_list = this.initialProcedures_List.filter((f) => {
      const title = `${f?.title}`;
      const number =`${f?.number}`;
      return title.toLowerCase().includes(filterString) || number.toLowerCase().includes(filterString) ;
    });
  }

  moveToEmployeeStep() {
    if (this.procedureReviewId) {
      this.updateProcedureReview();
      this.stepIndex = 1;
    }
    else {
      this.addProcedureReview(true);
    }
  }

  async moveToPublishStep() {
    if (this.empDataSource.data.length === 0) {
      this.alert.warningAlert(
        'Invalid Submit',
        'Please Add at least one ' + await this.labelPipe.transform('Employee') + ' to ' + await this.transformTitle('Procedure') + ' review'
      );
      return;
    }

    this.saveChanges();
    this.stepIndex = 2;
  }

  private startTimeChange() {
    if (this.procedureForm.get('startDate')?.value !== '') {
      const startDateTIme = `${this.procedureForm.get('startDate')?.value}T${this.procedureForm.get('startTime')?.value}`;

      this.procedureForm.patchValue({
        startDateTime: startDateTIme,
      });
    }
    else {
      this.procedureForm.patchValue({
        startDateTime: this.procedureForm.get('startDate')?.value,
      });
    }
  }

  private endTimeChange() {
    if (this.procedureForm.get('endDate')?.value !== '') {
      const endDateTime = `${this.procedureForm.get('endDate')?.value}T${this.procedureForm.get('endTime')?.value}`;

      this.procedureForm.patchValue({
        endDateTime: endDateTime,
      });
    }
    else {
      this.procedureForm.patchValue({
        endDateTime: this.procedureForm.get('endDate')?.value,
      });
    }
  }

  private addProcedureReview(navigateToEditPage = false) {
    this.startTimeChange();
    this.endTimeChange();

    const createProcedureReview: CreateProcedureReview = {
      endDateTime: this.convertLocalToUtc(this.procedureForm.get('endDateTime')?.value),
      isEmployeeShowResponses: this.procedureForm.get('isEmployeeShowResponses')?.value,
      procedureId: this.procedureForm.get('procedureId')?.value.id,
      procedureReviewInstructions: this.procedureForm.get('courseInstruction')?.value,
      procedureReviewTitle: this.procedureForm.get('reviewTitle')?.value,
      startDateTime: this.convertLocalToUtc(this.procedureForm.get('startDateTime')?.value),
      procedureReviewAcknowledgement:this.procedureForm.get('acknowledgement')?.value
    };
    this.procedureService.addProcedureReview(createProcedureReview)
      .subscribe(async (result) => {
        if (!result.isSuccess && result.error) {
          this.alert.errorToast(result.error);
          return;
        }

        if (navigateToEditPage) {
          this.router.navigateByUrl(`/procedure/edit/${result.data.id}?index=1`);
        }

        this.alert.successToast( await this.transformTitle('Procedure') + ' Review is in successfully added in Draft status !');
      });
  }

  private updateProcedureReview() {
    this.startTimeChange();
    this.endTimeChange();

    const createProcedureReview: CreateProcedureReview = {
      endDateTime: this.convertLocalToUtc(this.procedureForm.get('endDateTime')?.value),
      isEmployeeShowResponses: this.procedureForm.get('isEmployeeShowResponses')?.value,
      procedureId: this.procedureForm.get('procedureId')?.value.id,
      procedureReviewInstructions: this.procedureForm.get('courseInstruction')?.value,
      procedureReviewTitle: this.procedureForm.get('reviewTitle')?.value,
      startDateTime: this.convertLocalToUtc(this.procedureForm.get('startDateTime')?.value),
      procedureReviewAcknowledgement:this.procedureForm.get('acknowledgement')?.value
    };
    this.procedureService.updateProcedureReview(this.procedureReviewId, createProcedureReview)
      .subscribe(async (result) => {
        if (!result.isSuccess && result.error) {
          this.alert.errorToast(result.error);
          return;
        }

        // this.getStudentEvaluations();
        this.fetchProcedureReview(this.procedureReviewId);
        this.alert.successToast(await this.transformTitle('Procedure') + ` review is updated`);
      });
  }

  private fetchProcedureReview(id: string) {
    this.procedureService.getProcedureReviewById(id).subscribe((result) => {
      if (!result.isSuccess && result.error) {
        this.alert.errorToast(result.error);
        return;
      }

      this.procedureReview = result.data!;
      this.isProcedureReviewPublished = this.procedureReview.isPublished;
      this.patchFormValues(this.procedureReview);

      this.selectAuthority(
        this.authority_list.find((i) => i.procedures.some((x) => x.id === this.procedureReview.procedureId))
      );

      this.selectProcedure(
        this.procedures_list.find((i) => i.id === this.procedureReview.procedureId)
      );
    });
  }

  private fetchAuthority() {
    this.isSpinner = true;
    this.issuingAuthoritiesService
      .getAll()
      .then((res: any) => {
        this.authority_list = res;
        this.initialIssuingAuthorityList = Object.assign(this.authority_list);
        if (this.authority_list.length === 0) {
          this.noAuthFound = true;
        }
        else {
          this.noAuthFound = false;
          if (this.procedureReviewId) {
            this.fetchProcedureReview(this.procedureReviewId);
          }
        }
      })
      .catch((err) => {
        console.error(err);
      })
      .finally(() => {
        this.isSpinner = false;
      });
  }

  private fetchEmployees() {
    if (!this.procedureReviewId) {
      return;
    }

    this.isSpinner = true;
    this.procedureService
      .getLinkProcedureReviewEmp(this.procedureReviewId)
      .then((res: any) => {
        this.empDataSource.data = res;
      })
      .catch((err) => console.error(err))
      .finally(() => {
        this.isSpinner = false;
        this.empDataSource.sort = this.tblSort;
      });
  }

  private convertLocalToUtc(val: Date) {
    return new Date(val).toUTCString();
  }

  private convertUtcToLocalDate(val: Date): Date {
    const d = new Date(val); // val is in UTC
    const localOffset = d.getTimezoneOffset() * 60000;
    const localTime = d.getTime() - localOffset;

    d.setTime(localTime);
    return d;
  }

  private patchFormValues(procedureReview: any) {
    procedureReview.startDateTime = this.convertUtcToLocalDate(procedureReview.startDateTime);
    procedureReview.endDateTime = this.convertUtcToLocalDate(procedureReview.endDateTime);

    this.releaseDateTime = procedureReview.startDateTime;
    this.dueDateTime = procedureReview.endDateTime;

    this.procedureForm.patchValue({
      issueAuthorityId: this.authority_list.find((i) => i.procedures.some((x) => x.id === procedureReview.procedureId)),
      procedureId: this.procedures_list.find((i) => i.id === procedureReview.procedureId),
      reviewTitle: procedureReview.procedureReviewTitle,
      courseInstruction: procedureReview.procedureReviewInstructions,
      isEmployeeShowResponses: procedureReview.isEmployeeShowResponses,
      startDate: this.datePipe.transform(procedureReview.startDateTime, 'yyyy-MM-dd'),
      startTime: this.datePipe.transform(procedureReview.startDateTime, 'HH:mm'),
      endDate: this.datePipe.transform(procedureReview.endDateTime, 'yyyy-MM-dd'),
      endTime: this.datePipe.transform(procedureReview.endDateTime, 'HH:mm'),
      acknowledgement: procedureReview.procedureReviewAcknowledgement
      //procedureFileName:this.procedureForm.get('procedureId')?.value.fileName,
      //procedureLink:this.procedureForm.get('procedureId')?.value.hyperlink,
    });
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }
}
