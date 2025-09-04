import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { procedureReviewEmpExitoptions } from '@models/Procedure/Procedure_review';
import { ProceduresService } from 'src/app/_Services/QTD/procedures.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { sideBarBackDrop, sideBarDisableClose, sideBarMode, sideBarOpen } from 'src/app/_Statemanagement/action/state.menutoggle';
import { SubmitProcedureReviewDto } from '@models/Procedure/Procedure_review/submitProcedureReviewDto';
import { LicenseHelperService } from 'src/app/_Shared/services/licenseHelper.service';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';


@Component({
  selector: 'app-fly-panel-start-procedure-review',
  templateUrl: './fly-panel-start-procedure-review.component.html',
  styleUrls: ['./fly-panel-start-procedure-review.component.scss']
})
export class FlyPanelStartProcedureReviewComponent implements OnInit {

  procedureReviewObject: any;
  isLoading: boolean = true;
  procedureReviewId: any;
  radioAnswer: string|null = null;
  comment: string = '';
  isItemsDisable: boolean = false;
  datePipe = new DatePipe('en-us');
  exitDescription: string;
  saveDescription: string;
  isLicenseValid:boolean=true;

  constructor(
    private alert: SweetAlertService,
    private store: Store<{ toggle: string }>,
    private _router: Router,
    private route: ActivatedRoute,
    public dialog: MatDialog,
    private procedureService: ProceduresService,
    private licenseHelper:LicenseHelperService,
    private labelPipe: LabelReplacementPipe,

  ) { }

  async ngOnInit(): Promise<void> {
    this.checkLicense();
    if(this.isLicenseValid){
    this.route.params.subscribe((params: any) => {
      
      if (params.hasOwnProperty('procedureReviewId')) {
        this.procedureReviewId = params['procedureReviewId'];
        this.getProcedureInfo();
      }
    });
  }
  else{
    this._router.navigate(['emp/dashboard']);
  }
  this.exitDescription = `You are selecting to Exit the ` + await this.transformTitle('Procedure') + ` Review without submitting your responses. Your progress will be saved.`;
  this.saveDescription = `You are choosing to submit and finalize your ` + await this.transformTitle('Procedure') + ` Review response.`;
  }
  checkLicense(){
    var license = this.licenseHelper.getLicenseData();
    if(!license?.deluxe){
      this.isLicenseValid = false;
    }    
  }


  getProcedureInfo() {
    
    this.procedureService.getProcedureInfoByIdInEmp(this.procedureReviewId).then((res) => {
      
      this.isLoading = false;
      this.procedureReviewObject = res;
      if((this.procedureReviewObject.file != '' && this.procedureReviewObject.file != null )|| (this.procedureReviewObject.hyperLink!='' && this.procedureReviewObject.hyperLink!=null))
      {
        this.isItemsDisable = true;
      }
      if (!this.procedureReviewObject.isEmployeeShowResponses) {
        this.isItemsDisable = false;

      }
      this.radioAnswer=res.response;
      this.comment=res.comments;
      
    }).catch((res: any) => {
      
    })

  }

  saveResponse() {
    const submitOptions = new SubmitProcedureReviewDto(this.procedureReviewId, this.radioAnswer, this.comment);
    this.procedureService.submitProcedureReviewInEmp(submitOptions).then(async (res) => {
      this.isLoading = false;
      this.alert.successToast(await this.transformTitle('Procedure') + ` Review updated.`,true);
      this.goBack();
    }).catch((res: any) => {

    })
  }
  cancelResponse() {
    let cancelProcedureReview: procedureReviewEmpExitoptions = {
      procedureReviewId:this.procedureReviewId,
      response:this.radioAnswer,
      comments:this.comment,
    }
    this.procedureService.cancelProcedureReviewInEmp(cancelProcedureReview).then(async (res) => {

      this.isLoading = false;
      this.alert.successToast(await this.transformTitle('Procedure') + ` Review Cancel.`,true);
      this.goBack();
    }).catch((res: any) => {
      
      
    })
    // this.procedureService.cancelProcedureReviewInEmp(this.procedureReviewId, this.radioAnswer, this.comment).then((res) => {
    //   
    //   this.isLoading = false;
    //   this.alert.successToast(`Procedure Review Cancel.`);
    //   this.goBack();

    // }).catch((res: any) => {
    //   
    //   
    // })
  }
  async goBack() {

    this.store.dispatch(sideBarOpen());
    this._router.navigate(['emp/procedure-review/overview']);
  }

  ngOnDestroy(): void {
    
    this.store.dispatch(sideBarBackDrop({ backdrop: false }));
    this.store.dispatch(sideBarDisableClose({ disableClose: true }));
    this.store.dispatch(sideBarMode({ mode: 'side' }));
  }
  async exitDialog(templateRef) {
    this.exitDescription = `You are selecting to Exit the ` + await this.transformTitle('Procedure') + ` Review without submitting your responses. Your progress will be saved.`;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }
  exitDialogUpDate(templateRef) {
   // this.exitDescription = `You are selecting to Exit the Procedure Review without submitting your responses. Your progress will be saved.`;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }
  saveDialog(templateRef) {
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  exitConformation($event: any) {
    
    this.cancelResponse();
  }

  viewResult() {
    this.isItemsDisable = true;
  }

  linkOPened(){
    this.isItemsDisable = false;
  }

  showPdf(event) {
    const linkSource = 'data:application/pdf;base64,' + event.file;
    const downloadLink = document.createElement("a");
    const fileName = event.fileName;

    downloadLink.href = linkSource;
    downloadLink.download = fileName;
    downloadLink.click();
    this.isItemsDisable = false;
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }
}
