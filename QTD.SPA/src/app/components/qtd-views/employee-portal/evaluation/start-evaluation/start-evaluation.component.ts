import { group, query, style, animate, trigger, transition } from '@angular/animations';
import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { Store } from '@ngrx/store';
import { EmpEvaluationVM } from 'src/app/_DtoModels/EmpEvaluation/EmpEvaluationVM';
import { RatingScale } from 'src/app/_DtoModels/RatingScale/RatingScale';
import { StudentEvaluation } from 'src/app/_DtoModels/StudentEvaluation/StudentEvaluation';
import { StudentEvaluationService } from 'src/app/_Services/QTD/student-evaluation.service';
import { sideBarClose } from 'src/app/_Statemanagement/action/state.menutoggle';
import { SubSink } from 'subsink';
import { QuestionVM } from '../../../implementation/schedulingclasses/scheduling-classes-overview/view-edit-grades/enter-eval-data/enter-eval-data.component';
import { StudentEvaluation_SaveQuestion } from 'src/app/_DtoModels/StudentEvaluation/StudentEvaluation_SaveQuestion';
import { RostersService } from 'src/app/_Services/QTD/rosters.service';
import { StudentEvaluationWithoutEmp } from 'src/app/_DtoModels/StudentEvaluation/StudentEvaluationWithoutEmp';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { StudentEvaluationSubmitOptions } from 'src/app/_DtoModels/StudentEvaluation/StudentEvaluationSubmitOptions';
import { DatePipe } from '@angular/common';

const left = [
  query(':enter, :leave', style({ opacity: '0' }), { optional: true }),
  group([
    query(':enter', [style({ transform: 'translateX(-100%)', opacity: '1' }), animate('.3s ease-out', style({ transform: 'translateX(0%)' }))], {
      optional: true,
    }),
    query(':leave', [style({ transform: 'translateX(0%)', opacity: '0', display: 'none' }), animate('.3s ease-out', style({ transform: 'translateX(100%)' }))], {
      optional: true,
    }),
  ]),
];

const right = [
  query(':enter, :leave', style({ opacity: '0' }), { optional: true }),
  group([
    query(':enter', [style({ transform: 'translateX(100%)', opacity: '1' }), animate('.3s ease-out', style({ transform: 'translateX(0%)' }))], {
      optional: true,
    }),
    query(':leave', [style({ transform: 'translateX(0%)', opacity: '0', display: 'none' }), animate('.3s ease-out', style({ transform: 'translateX(-100%)' }))], {
      optional: true,
    }),
  ]),
];

@Component({
  selector: 'app-start-evaluation',
  templateUrl: './start-evaluation.component.html',
  styleUrls: ['./start-evaluation.component.scss'],
  animations: [
    trigger('animSlider', [
      transition(':increment', right),
      transition(':decrement', left),
    ])
  ],
})
export class StartEvaluationComponent implements OnInit, OnDestroy {

  showSpinner = false;
  nextSpinner = false;
  subscription = new SubSink();
  currDate = Date.now();
  detailsInfo!: EmpEvaluationVM;
  scaleData: any[] = [];
  additionalScale: any[] = []

  questions: any[] = [];
  datePipe = new DatePipe('en-us');
  currentRating: number = 0;
  lowestDesc = "";
  total = 0;
  highestDesc = "";
  page = 0;
  ratingForm = new UntypedFormGroup({});
  showForm = false;
  preview = false;
  eval!: StudentEvaluation;
  rating: RatingScale[] = [];
  data: QuestionVM[] = [];
  exitNumber:any;
  solvedQuestions: StudentEvaluationWithoutEmp[] = [];
  submitDescription = "You are choosing to submit and finalize your evaluation feedback."
  exitDescription = "You are selecting to Exit the Student Evaluation without submitting your responses. Exiting the student evaluation will save your responses."
  @ViewChild('toPrint') print!: any;

  constructor(
    private store: Store<any>,
    private studentEvalService: StudentEvaluationService,
    public dialog: MatDialog,
    private rosterService: RostersService,
    private alert: SweetAlertService,
  ) { }

  ngOnInit(): void {
    this.store.dispatch(sideBarClose());
    this.subscription.sink = this.store.select('evalData').subscribe((x: any) => {
      if (x['evalData'] === undefined) {
        var temp: any = {};
        this.detailsInfo = JSON.parse(localStorage.getItem('evalData') ?? temp) as EmpEvaluationVM;
      }
      else {
        localStorage.setItem('evalData', JSON.stringify(x.evalData));
        this.detailsInfo = x.evalData;
      }
      this.prepareQuestions();
    })
  }

  ngOnDestroy(): void {
    localStorage.removeItem('evalData');
    this.subscription.unsubscribe();
  }

  async prepareQuestions() {
    this.questions = await this.studentEvalService.getLinkedQuestions(this.detailsInfo.evaluationId);
    this.eval = await this.studentEvalService.getWithScale(this.detailsInfo.evaluationId);
    var rating: any[] = [];
    this.scaleData = Object.assign([], this.eval.ratingScaleN.ratingScaleExpanded);
    this.scaleData.sort((a,b) => a.ratings - b.ratings);
    this.eval.ratingScaleN.ratingScaleExpanded.forEach((x) => {
      rating.push({ value: x.ratings, description: x.description });
    });
    this.lowestDesc = this.sortRatings(rating).reduce((prev, curr) => {
      return prev.ratings > curr.ratings ? prev : curr;
    }).description;
    this.highestDesc = this.sortRatings(rating).reduce((prev, curr) => {
      return prev.ratings < curr.ratings ? curr : prev;
    }).description;
    if (this.eval.isAllowNAOption) {
      rating.push({ value: null, description: 'N/A' })
    }
    this.questions.forEach((data, i) => {
      this.data.push({ number: (i + 1), question: data.stem, id: data.id, selected: 0, rating: rating });
      this.exitNumber = i+1;
    })
    this.readyForm();
  }

  readyForm() {
    this.data.forEach((q) => {
      this.ratingForm.addControl(`note${q.number}`, new UntypedFormControl(null, Validators.required));
      this.ratingForm.addControl(`rating${q.number}`, new UntypedFormControl(undefined, Validators.required));
    });
    this.readyAlreadySolvedData();
  }

  async readyAlreadySolvedData() {
    this.solvedQuestions = await this.studentEvalService.getSavedQuestions(this.detailsInfo.evaluationId, this.detailsInfo.classScheduleId, this.detailsInfo.employeeId);
    this.solvedQuestions.forEach((data) => {
      var found = this.data.find((x) => {
        return x.id === data.questionId;
      })
      if (found != undefined) {
        var foundRating = {};
        foundRating = found.rating.find((f) => {
         return f.value == data.ratingScaleExpanded?.ratings;
      })
        this.ratingForm.patchValue({
          [`rating${found.number}`]: foundRating,
          [`note${found.number}`]: data.notes,
        })
      }
    })

    this.showForm = true;
  }

  sortRatings(ratings: any[]): any[] {
    return ratings.sort((a, b) => {
      if (a.value === null|| a.value === 'N/A') {
        return 1;
      } else if (b.value === null || b.value === 'N/A') {
        return -1;
      } else {
        return a.value - b.value;
      }
    });
  }

  goBack() {
    history.back();
  }

  increaseNumber() {
    // this.showForm = false;
    if (this.page < (this.data.length - 1)) {
      this.page = this.page + 1;
    }
    // setTimeout(()=>{
    //   this.showForm = true;
    // },1);
  }

  decreaseNumber() {
    // this.showForm = false;
    if (this.page > 0) {
      this.page = this.page - 1;
    }
    // setTimeout(()=>{
    //   this.showForm = true;
    // },1)
  }

  async saveQuestion() {
    this.nextSpinner = true;
    var options = new StudentEvaluation_SaveQuestion();
    options.classId = this.detailsInfo.classScheduleId;
    options.employeeId = this.detailsInfo.employeeId;
    options.evaluationId = this.detailsInfo.evaluationId;
    options.questionId = this.data[this.page].id;
    options.rating = this.ratingForm.get(`rating${this.data[this.page].number}`)?.value?.value;
    options.notes = this.ratingForm.get(`note${this.data[this.page].number}`)?.value;
    await this.studentEvalService.saveQuestions(options).then((_) => {
      this.increaseNumber();
      this.nextSpinner = false;
    });
  }

  openSubmitDialogue(templateRef: any) {
    this.dialog.open(templateRef, {
      width: '680px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    })
  }

  openBackDialogue(templateRef: any) {
    this.dialog.open(templateRef, {
      width: '680px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    })
  }

  async exitEval() {
    this.showSpinner = true;
    var options = new StudentEvaluationSubmitOptions();
    options.classId = this.detailsInfo.classScheduleId;
    options.evaluationId = this.detailsInfo.evaluationId;
    options.employeeId = this.detailsInfo.employeeId;
    if (this.questions.length < 1 || this.ratingForm.get(`rating${this.data[this.page].number}`)?.hasError('required')) {
      this.goBack();
      this.showSpinner = false;
    }
    else {
      await this.saveQuestion().then(async (_) => {
        this.goBack();
      }).finally(() => {
        this.showSpinner = false;
      });
    }
  }

  async submitEvaluation() {
    this.showSpinner = true;
    var options = new StudentEvaluationSubmitOptions();
    options.classId = this.detailsInfo.classScheduleId;
    options.evaluationId = this.detailsInfo.evaluationId;
    options.employeeId = this.detailsInfo.employeeId;
    if (this.questions.length < 1 || this.ratingForm.get(`rating${this.data[this.page].number}`)?.hasError('required')) {
      await this.rosterService.submitEvaluation(options).then((_) => {
        this.alert.successToast("Evaluation Submitted Successfully",true);
        this.goBack();
      }).finally(() => {
        this.showSpinner = false;
      })
    }
    else {
      await this.saveQuestion().then(async (_) => {
        await this.rosterService.submitEvaluation(options).then((_) => {
          this.alert.successToast("Evaluation Submitted Successfully",true);
          this.goBack();
        }).finally(() => {
          this.showSpinner = false;
        })
      }).finally(() => {
        this.showSpinner = false;
      })
    }
  }

  printData() {
    const WindowPrt = window.open('', '', 'left=0,top=0,width=900,height=900,toolbar=0,scrollbars=0,status=0');
    WindowPrt?.document.write(`<html>
    <head>
      <link rel="stylesheet" type="text/css" href="start-evaluation.component.css">
      <style>
      .checked {
        background-color: rgb(92, 155, 49) !important;
        /*inner circle color change*/
      }

      .outerCheck{
        border-color:rgb(92, 155, 49) !important;
      }
      </style>
    </head>
    <body>
      ${this.print.nativeElement.innerHTML}
    </body>`
    );
    WindowPrt?.document.close();
    WindowPrt?.focus();
    WindowPrt?.print();
    WindowPrt?.close();
  }

}
