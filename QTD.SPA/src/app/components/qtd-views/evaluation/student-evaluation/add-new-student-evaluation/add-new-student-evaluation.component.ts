import { moveItemInArray } from '@angular/cdk/drag-drop';
import { BreakpointObserver } from '@angular/cdk/layout';
import { TemplatePortal } from '@angular/cdk/portal';
import { ChangeDetectorRef, Component, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatStepper, StepperOrientation } from '@angular/material/stepper';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { ActivatedRoute, Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import * as ckcustomBuild from 'src/app/ckcustomBuild/build/ckeditor.js';
import { StudentEvaluationCreateOptions } from 'src/app/_DtoModels/StudentEvaluation/StudentEvaluationCreateOptions';
import { StudentEvaluationHistoryCreateOptions } from 'src/app/_DtoModels/StudentEvaluationHistory/StudentEvaluationHistoryCreateOptions';
import { StudentEvaluation_Question_LinkCreateOptions } from 'src/app/_DtoModels/StudentEvaluationQuestions/StudentEvaluation_Question_LinkCreateOptions';
import { RatingScaleNewService } from 'src/app/_Services/QTD/rating-scale-new.service';
import { StudentEvaluationService } from 'src/app/_Services/QTD/student-evaluation.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { sideBarBackDrop, sideBarClose, sideBarOpen, sideBarToggle } from 'src/app/_Statemanagement/action/state.menutoggle';
import { MatSort } from '@angular/material/sort';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';

@Component({
  selector: 'app-add-new-student-evaluation',
  templateUrl: './add-new-student-evaluation.component.html',
  styleUrls: ['./add-new-student-evaluation.component.scss']
})

export class AddNewStudentEvaluationComponent implements OnInit {
  evaluationInfoForm: UntypedFormGroup;
  additionalOptionsForm: UntypedFormGroup = new UntypedFormGroup({});
  saveDraft: boolean;
  instructionsform: UntypedFormGroup = new UntypedFormGroup({});
  editor = ckcustomBuild;
  ratingScaleList: any[] = [];
  currentIndex: number = 0;
  studentEvaluationId: any;
  evalQuestionMode: 'Add' | 'Edit' | 'Copy' = 'Add';
  dataSource: MatTableDataSource<any>
  displayColumns: string[] = ['order', 'id', 'stem','unlink'];
  displayedColumns: string[] = ['id', 'question'];
  @ViewChild('stepper') stepper: MatStepper;
  stepperOrientation: Observable<StepperOrientation>;
  studentEvalObj: any;
  alreadyLinked: string[] = [];
  isReordered = false;
  studentEvalId;
  mode:string;
  @ViewChild(MatSort) tblSort: MatSort/*  ) {
    if (sort) this.dataSource.sort = sort;
  }  */

 /*  @ViewChild(MatPaginator) paginator: MatPaginator; *//*  MatPaginator) {
    if (paging) this.DataSource.paginator = paging;
  } */
  constructor(private fb: UntypedFormBuilder,
    private router: Router,
    private store: Store<{ toggle: string }>,
    public changeDetector: ChangeDetectorRef,
    public breakpointObserver: BreakpointObserver,
    public flyPanelService: FlyInPanelService,
    private vcf: ViewContainerRef,
    private _router: Router,
    private ratingScaleService: RatingScaleNewService,
    private studentEvalService: StudentEvaluationService,
    private alert: SweetAlertService,
    public dialog: MatDialog,
    private route: ActivatedRoute) {

    this.stepperOrientation = breakpointObserver
      .observe('(min-width: 800px)')
      .pipe(map(({ matches }) => (matches ? 'horizontal' : 'vertical')));

  }

  ngOnInit(): void {
    this.store.dispatch(sideBarClose());
    this.store.dispatch(sideBarBackDrop({ backdrop: false }));
    this.route.queryParams.subscribe(params => {
      this.studentEvalId = params['id'];
      if (this.studentEvalId) {
        this.mode = 'edit';
        this.loadStudentEvaluationFormData();
        this.studentEvaluationId=this.studentEvalId;
      } else {
        this.mode = 'create';
      }
    });
    this.ratingScaleService.getAll().then((res) => {
      this.ratingScaleList = res;
    });
    this.saveDraft = false;
    this.initializeEvaluationInfoForm();
    this.readyinstructionsForm();
    this.readyadditionaloptionsForm();
  }

  initializeEvaluationInfoForm() {
    this.evaluationInfoForm = this.fb.group({
      title: new UntypedFormControl('', [Validators.required]),
      ratingScale: new UntypedFormControl('', [Validators.required]),
    });
  }

  readyinstructionsForm() {
    this.instructionsform = this.fb.group({
      instructions: new UntypedFormControl('', Validators.required),
    });

  }
  readyadditionaloptionsForm() {
    this.additionalOptionsForm = this.fb.group({
      availableForAllIlas: new UntypedFormControl(false),
      availableForSelectedIlas: new UntypedFormControl(false),
      allowNAoption: new UntypedFormControl(false),
      includeComments: new UntypedFormControl(false),
    });
  }
  async goBack() {

    this.router.navigate(['evaluation/studentevaluation']);
  }

  async saveAsDraft(){
    this.saveDraft = true;
    this.SaveOrUpdateStudentEvaluation();
    this.goBack();
  }

  toggleMainMenu() {
    //this.databroadcastSrvc.ToggleMainMenu.next('');
    this.store.dispatch(sideBarToggle())
  }
  async selectedChanged(event: any) {
    if (event.selectedIndex === 0) {
      this.currentIndex = 0;

    }
    else if (event.selectedIndex === 1) {

      this.GetLinkedQuestions();
      this.currentIndex = 1;
    }
    else if (event.selectedIndex === 2) {
      this.studentEvalService.get(this.studentEvaluationId).then((res) => {
        this.studentEvalObj = res;
      })
      // this.GetLinkedQuestions();

      this.currentIndex = 2;
    }

  }
  async openFlyInPanel(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }
  SaveOrUpdateStudentEvaluation() {
    let createOpt: StudentEvaluationCreateOptions = {
      title: this.evaluationInfoForm.get('title')?.value,
      ratingScaleId: this.evaluationInfoForm.get('ratingScale')?.value,
      instructions: this.instructionsform.get('instructions')?.value,
      isAvailableForAllILAs: this.additionalOptionsForm.get('availableForAllIlas')?.value,
      isAvailableForSelectedILAs: this.additionalOptionsForm.get('availableForSelectedIlas')?.value,
      isIncludeCommentSections: this.additionalOptionsForm.get('includeComments')?.value,
      isAllowNAOption: this.additionalOptionsForm.get('allowNAoption')?.value,
    };
    if (this.studentEvaluationId === undefined) {
      this.studentEvalService.create(createOpt).then((res: any) => {
        this.alert.successToast('Student Evaluation Created successfully');
        this.studentEvaluationId = res.id;
        if(!this.saveDraft){
          this.stepper.next();
        }
      });
    }
    else //this is edit case
    {
      this.studentEvalService.update(this.studentEvaluationId, createOpt).then((res: any) => {
        this.alert.successToast('Student Evaluation Updated successfully');
        this.stepper.next();
      });
    }
  }
  GetLinkedQuestions() {
    this.studentEvalService.getLinkedQuestions(this.studentEvaluationId).then((res) => {
      this.dataSource = new MatTableDataSource(res);
      setTimeout(()=>{
        this.dataSource.sort = this.tblSort;
      },1) 
      this.alreadyLinked = [];
      this.dataSource.data.forEach((x) => {
        this.alreadyLinked.push(x.id);
      });
    })
  }
  async PreviewDetails() {

    if (this.isReordered) {
      let idsarray: any = [];
      for (let i = 0; i < this.dataSource.data.length; i++) {

        idsarray.push(this.dataSource.data[i].id)
      }
      var options = new StudentEvaluation_Question_LinkCreateOptions();
      options.questionIds = idsarray;
      options.studentEvaluationId = this.studentEvaluationId;
      options.isReordered = true

      this.studentEvalService.LinkQuestions(this.studentEvaluationId, options).then((res) => {
        this.GetLinkedQuestions()

      })
    }
    else {
      this.GetLinkedQuestions()
    }

    this.stepper.next();
  }
  PublishStudentEvaluationModal(templateRef: any) {

    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }
  PublishEvaluation(e: any) {
    var options = new StudentEvaluationHistoryCreateOptions();
    var data = JSON.parse(e);
    options.effectiveDate = data['effectiveDate'];
    options.studentEvaluationNotes = data['reason'];
    this.studentEvalService.publishEvaluation(this.studentEvaluationId, options).then((_) => {
      this.alert.successToast("Evaluation Published Successfully");
      this.router.navigate(['evaluation/studentevaluation']);
    })
  }
  dropTable(event: any) {
    const prevIndex = this.dataSource.data.findIndex(
      (d) => d === event.item.data
    );
    moveItemInArray(this.dataSource.data, prevIndex, event.currentIndex);
    this.dataSource = new MatTableDataSource(this.dataSource.data);
    this.isReordered = true;
  }

  loadStudentEvaluationFormData() {
    this.studentEvalService.get(this.studentEvalId).then((res)=>
    {
       this.evaluationInfoForm.get('title')?.patchValue(res.title);
       this.evaluationInfoForm.get('ratingScale')?.patchValue(res.ratingScaleN.id);
       this.instructionsform.get('instructions')?.patchValue(res.instructions);
       this.additionalOptionsForm.get('availableForAllIlas')?.patchValue(res.isAvailableForAllILAs);
       this.additionalOptionsForm.get('availableForSelectedIlas')?.patchValue(res.isAvailableForSelectedILAs);
       this.additionalOptionsForm.get('allowNAoption')?.patchValue(res.isAllowNAOption);
       this.additionalOptionsForm.get('includeComments')?.patchValue(res.isIncludeCommentSections);
    });
  }

  unlinkStudentEvaluationQuestions(questionId:any){
      var options = new StudentEvaluation_Question_LinkCreateOptions();
      options.questionIds = [questionId];
      options.studentEvaluationId = this.studentEvaluationId
      this.studentEvalService.UnLinkQuestions(this.studentEvaluationId,options).then((res: any) =>{
        this.alert.successToast('Student Evaluation Question Unlinked successfully');
        this.GetLinkedQuestions();
    });
  }
}
