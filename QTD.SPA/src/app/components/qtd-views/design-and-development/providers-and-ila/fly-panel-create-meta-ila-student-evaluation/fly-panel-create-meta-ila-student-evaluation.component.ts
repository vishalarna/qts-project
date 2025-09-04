import { moveItemInArray } from '@angular/cdk/drag-drop';
import { BreakpointObserver } from '@angular/cdk/layout';
import { TemplatePortal } from '@angular/cdk/portal';
import { ChangeDetectorRef, Component, EventEmitter, Input, OnInit, Output, ViewChild, ViewContainerRef } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatSort } from '@angular/material/sort';
import { MatStep, MatStepper, StepperOrientation } from '@angular/material/stepper';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import * as ckcustomBuild from 'src/app/ckcustomBuild/build/ckeditor.js';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { StudentEvaluationCreateOptions } from 'src/app/_DtoModels/StudentEvaluation/StudentEvaluationCreateOptions';
import { StudentEvaluationHistoryCreateOptions } from 'src/app/_DtoModels/StudentEvaluationHistory/StudentEvaluationHistoryCreateOptions';
import { StudentEvaluation_Question_LinkCreateOptions } from 'src/app/_DtoModels/StudentEvaluationQuestions/StudentEvaluation_Question_LinkCreateOptions';
import { RatingScaleNewService } from 'src/app/_Services/QTD/rating-scale-new.service';
import { StudentEvaluationService } from 'src/app/_Services/QTD/student-evaluation.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { sideBarToggle } from 'src/app/_Statemanagement/action/state.menutoggle';

@Component({
  selector: 'app-fly-panel-create-meta-ila-student-evaluation',
  templateUrl: './fly-panel-create-meta-ila-student-evaluation.component.html',
  styleUrls: ['./fly-panel-create-meta-ila-student-evaluation.component.scss']
})
export class FlyPanelCreateMetaIlaStudentEvaluationComponent implements OnInit {
  @Output() closed = new EventEmitter<any>();
  @Input() mode : string='create';
  evaluationInfoForm: UntypedFormGroup;
  additionalOptionsForm: UntypedFormGroup = new UntypedFormGroup({});
  instructionsform: UntypedFormGroup = new UntypedFormGroup({});
  editor = ckcustomBuild;
  ratingScaleList: any[] = [];
  currentIndex: number = 0;
  studentEvaluationId: any;
  evalQuestionMode: 'Add' | 'Edit' | 'Copy' = 'Add';
  dataSource: MatTableDataSource<any>
  displayColumns: string[] = ['order', 'id', 'stem'];
  displayedColumns: string[] = ['id', 'question'];
  @ViewChild('stepper') stepper: MatStepper;
  stepperOrientation: Observable<StepperOrientation>;
  studentEvalObj: any;
  alreadyLinked: string[] = [];
  isReordered = false;
  @ViewChild(MatSort) tblSort: MatSort;
  isFlyPanelAddNewQuestions:boolean = false;
  isMainFlyPanelOpen:boolean=true;
  isFlyPanelImportExistingQuestions:boolean=false;
  continueButtonClicked:boolean=false;

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
    public dialog: MatDialog,) {

    this.stepperOrientation = breakpointObserver
      .observe('(min-width: 800px)')
      .pipe(map(({ matches }) => (matches ? 'horizontal' : 'vertical')));
  }

  ngOnInit(): void {
    this.ratingScaleService.getAll().then((res) => {
      this.ratingScaleList = res;
    });
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

  toggleMainMenu() {
    this.store.dispatch(sideBarToggle())
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
        this.studentEvalObj=res;
      });
    }
    else //this is edit case
    {
      this.studentEvalService.update(this.studentEvaluationId, createOpt).then((res: any) => {
        this.alert.successToast('Student Evaluation Updated successfully');
        this.studentEvalObj = res;
      });
    }
  }

  GetLinkedQuestions() {
    this.studentEvalService.getLinkedQuestions(this.studentEvaluationId).then((res) => {
      this.dataSource = new MatTableDataSource(res);
      setTimeout(() => {
        this.dataSource.sort = this.tblSort;
      }, 1)
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
    this.currentIndex=2;
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
      this.closeFlyPanel();
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

  closeFlyPanel() {
    this.closed.emit(this.studentEvaluationId);
  }

  async initialContinueClickAsync(){
    this.continueButtonClicked=true;
    this.currentIndex=1;
    await this.SaveOrUpdateStudentEvaluation();
  }

  async onSelectionChange(event: any) {
    if(!this.continueButtonClicked){

      this.currentIndex=event.selectedIndex;
      let stepDifference= event.selectedIndex - event.previouslySelectedIndex;
      if(stepDifference>1){
        let matSteps:MatStep[] = this.stepper.steps.toArray();
        for(let i=event.previouslySelectedIndex+1;i<event.selectedIndex;i++){
          matSteps[i].interacted=true;
          matSteps[i].completed=true;
        }
      }
      if(event.previouslySelectedIndex!=2){
        await this.SaveOrUpdateStudentEvaluation();
      }
      this.scrollToTop();
    }
    this.continueButtonClicked=false;
  }

  private scrollToTop() {
    const stepperContainer = document.querySelector('.mat-sidenav-content');
    if (stepperContainer) {
      stepperContainer.scrollTop = 0;
    }
  }
}
