import { Component, OnDestroy, OnInit } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ClassScheduleDetailVM } from '@models/SchedulesClassses/ClassScheduleDetailVM';
import { Store } from '@ngrx/store';
import { StudentEvaluation } from 'src/app/_DtoModels/StudentEvaluation/StudentEvaluation';
import { StudentEvaluationWithoutEmpCreateOptions } from 'src/app/_DtoModels/StudentEvaluation/StudentEvaluationWithoutEmpCreateOptions';
import { QuestionsWithCountOptions } from 'src/app/_DtoModels/StudentEvaluationQuestion/QuestionsWithCountOptions';
import { StudentEvaluationService } from 'src/app/_Services/QTD/student-evaluation.service';
import { TrainingService } from 'src/app/_Services/QTD/training.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { sideBarClose } from 'src/app/_Statemanagement/action/state.menutoggle';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-enter-eval-data',
  templateUrl: './enter-eval-data.component.html',
  styleUrls: ['./enter-eval-data.component.scss']
})
export class EnterEvalDataComponent implements OnInit, OnDestroy {
  subscription = new SubSink();
  type: 'individual' | 'aggregate';
  id: any = '';
  evalTitle = "Power System Protection Evaluation II";
  eval!: StudentEvaluation;

  pgNumber: number = 1;
  maxQuestions: number = 3;
  selectedData = 0;
  comments = "";

  data: QuestionVM[] = []

  splitData: any[] = [];

  classId = "";
  empId = "";
  class!: ClassScheduleDetailVM;
  questions!: QuestionsWithCountOptions[];
  loading = false;
  saving = false;
  evalForm = new UntypedFormGroup({});
  lowestDesc = "";
  highestDesc = ""

  constructor(
    private route: ActivatedRoute,
    private store: Store,
    private trService: TrainingService,
    private studentEvalService: StudentEvaluationService,
    private alert : SweetAlertService,
  ) { }

  ngOnInit(): void {
    this.loading = true;
    this.store.dispatch(sideBarClose());
    this.subscription.sink = this.route.params.subscribe((res: any) => {
      this.type = res.type;
      this.id = res.id;
      this.classId = res.classId;
      this.empId = res.empId;
      this.readyData();
    })
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  async readyData() {
    this.class = await this.trService.get(this.classId);
    this.questions = await this.studentEvalService.getLinkedQuestions(this.id);
    this.eval = await this.studentEvalService.getWithScale(this.id);
    
    var rating:any[] = [];
    this.eval.ratingScaleN.ratingScaleExpanded.forEach((x)=>{
      rating.push({value:x.ratings,description:x.description});
    })
    if(this.eval.isAllowNAOption){
      rating.push({value:null,description:"N/A"})
    }
    
    this.lowestDesc = this.eval.ratingScaleN.ratingScaleExpanded.reduce((prev,curr)=>{
      return prev.ratings < curr.ratings ? prev:curr;
    }).description;

    this.highestDesc = this.eval.ratingScaleN.ratingScaleExpanded.reduce((prev,curr)=>{
      return prev.ratings < curr.ratings ? curr:prev;
    }).description;
    this.data = [];
    this.questions.forEach((data, i) => {
      this.data.push({ number: (i + 1), question: data.stem, id: data.id, selected: 0,rating:rating });
    })
    this.splitQuestionData(1);

    

    this.readyForm();
  }

  readyForm() {
    switch (this.type.trim().toLowerCase()) {
      case 'individual':
        this.data.forEach((x,i) => {
          this.evalForm.addControl(`notes${x.number}`, new UntypedFormControl(""));
        })
        break;
      case 'aggregate':
        this.data.forEach((x, i) => {
          this.evalForm.addControl(`low${x.number}`, new UntypedFormControl(null, [Validators.pattern('^[0-9]\\d*(\\.\\d+)?$'), Validators.required]));
          this.evalForm.addControl(`average${x.number}`, new UntypedFormControl(null, [Validators.pattern('^[0-9]\\d*(\\.\\d+)?$'), Validators.required]));
          this.evalForm.addControl(`high${x.number}`, new UntypedFormControl(null, [Validators.pattern('^[0-9]\\d*(\\.\\d+)?$'), Validators.required]));
          this.evalForm.addControl(`notes${x.number}`, new UntypedFormControl(""));
        })
        break;
    }
    this.loading = false;
    this.subscription.sink =  this.evalForm.statusChanges.subscribe(()=>{

    })
  }

  goBack() {
    history.back();
  }

  splitQuestionData(pg: number) {
    this.pgNumber = pg;
    this.splitData = Object.assign([], this.data.slice(this.pgNumber * this.maxQuestions - this.maxQuestions, this.pgNumber * this.maxQuestions));
  }

  checkSelection() {
    this.selectedData = this.data.filter((que) => {
      return que.selected !== 0;
    }).length;
  }

  async saveData() {
    this.saving = true;
    switch (this.type.trim().toLowerCase()) {
      case 'individual':
        var options = new StudentEvaluationWithoutEmpCreateOptions();
        options.additionalComments = this.comments;
        options.classScheduleId = this.classId;
        options.dataMode = "Individual";
        options.studentEvaluationId = this.id;
        options.studentEvalData = [];
        options.empId = this.empId;
        var scale = this.data.map((f)=>{
          return f.selected;
        })
        this.data.forEach((data,i)=>{
          var notes = this.evalForm.get(`notes${data.number}`)?.value;
          options.studentEvalData.push({questionId:data.id, ratingScale:scale[i],notes:notes,high:0.0,average:0.0,low:0.0});
        })
        await this.trService.createEvalWithoutEMP(options).then((_)=>{
          this.alert.successToast("Evaluation Data Saved");
          history.back();
        }).finally(()=>{
          this.saving = false;
        })
        break;
      case 'aggregate':
        var options = new StudentEvaluationWithoutEmpCreateOptions();
        options.additionalComments = this.comments;
        options.classScheduleId = this.classId;
        options.dataMode = "Aggregate";
        options.studentEvaluationId = this.id;
        options.studentEvalData = [];
        options.empId = this.empId;
        this.data.forEach((data, i) => {
          var high = this.evalForm.get(`high${data.number}`)?.value;
          var average = this.evalForm.get(`average${data.number}`)?.value;
          var low = this.evalForm.get(`low${data.number}`)?.value;
          var notes = this.evalForm.get(`notes${data.number}`)?.value;
          options.studentEvalData.push({ questionId: data.id, ratingScale: null, high: high, average: average, low: low, notes })
        });
        
        await this.trService.createEvalWithoutEMP(options).then((_) => {
          this.alert.successToast("Evaluation Data Saved");
          history.back();
        }).finally(() => {
          this.saving = false;
        });
        break;
    }
  }

  checkCompleted(){
    var count = 0;
    this.data.forEach((x)=>{
      if(this.evalForm.get(`high${x.number}`)?.valid &&
      this.evalForm.get(`low${x.number}`)?.valid &&
      this.evalForm.get(`average${x.number}`)?.valid){
        count++;
      }
    })
    
    return count;
  }

}

export class QuestionVM {
  question!: string;
  number!: number;
  id!: any;
  selected!: number | null;
  rating:any[] = [];
}
