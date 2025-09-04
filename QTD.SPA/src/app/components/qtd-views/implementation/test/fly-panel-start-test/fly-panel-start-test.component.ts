import { tr } from 'date-fns/locale';
import { TestItem } from './../../../../../_DtoModels/TestItem/TestItem';
import { TestsService } from 'src/app/_Services/QTD/tests.service';
import { animate, group, query, style, transition, trigger } from '@angular/animations';
import { StepperOrientation } from '@angular/cdk/stepper';
import { AfterViewInit, Component, ElementRef, HostListener, OnDestroy, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatStepper } from '@angular/material/stepper';
import { ActivatedRoute, Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { Observable, Subscription, interval, timer } from 'rxjs';
import { freezeMenu, sideBarBackDrop, sideBarClose, sideBarDisableClose, sideBarMode, sideBarOpen } from 'src/app/_Statemanagement/action/state.menutoggle';
import { TestItemFillBlank } from 'src/app/_DtoModels/TestItemFillBlank/TestItemFillBlank';
import { TestItemMcq } from 'src/app/_DtoModels/TestItemMcq/TestItemMcq';
import { TestItemTrueFalse } from 'src/app/_DtoModels/TestItemTrueFalse/TestItemTrueFalse';
import { EmployeesService } from 'src/app/_Services/QTD/employees.service';
import { BlankIndexWithAnwer, EmpTestCreateOption, MatchValueWithCorrectValue } from 'src/app/_DtoModels/Employee/EmpTestCreateOption';
import { SubSink } from 'subsink';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { StartTestDialogComponent } from '../start-test-dialog/start-test-dialog.component';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { UntypedFormGroup } from '@angular/forms';
import { TestItemMatch } from 'src/app/_DtoModels/TestItemMatch/TestItemMatch';
import { map, share, takeWhile } from 'rxjs/operators';
import { getTestInfo } from 'src/app/_Statemanagement/action/state.componentcommunication';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { TestItemVM } from '@models/TestItem/TestItemVM';
import { EmployeeAnswerModel } from '@models/Test/EmployeeAnswerModel';
import { ReviewTestModel } from '@models/Test/ReviewTestModel';
import { ApiClassScheduleRosterTimeRecordService } from 'src/app/_Services/QTD/api.classschedule-roster-timerecord.service';
import { ClassScheduleRosterTimeRecordVM } from '@models/ClassScheduleRoster-TimeRecord/classScheduleRosterTimeRecord_VM';
const left = [
  query(':enter, :leave', style({ position: 'fixed', width: '50%', display: 'flex' }), { optional: true }),
  group([
    query(':enter', [style({ transform: 'translateX(-100%)' }), animate('.1s ease-out', style({ transform: 'translateX(0%)', opacity: 1 }))], {
      optional: true,
    }),
    query(':leave', [style({ transform: 'translateX(0%)' }), animate('.1s ease-out', style({ transform: 'translateX(100%)', opacity: 0 }))], {
      optional: true,
    }),
  ]),
];

const right = [
  query(':enter, :leave', style({ position: 'fixed', width: '50%', display: 'flex' }), { optional: true }),
  group([
    query(':enter', [style({ transform: 'translateX(100%)' }), animate('.1s ease-in', style({ transform: 'translateX(0%)', opacity: 1 }))], {
      optional: true,
    }),
    query(':leave', [style({ transform: 'translateX(0%)' }), animate('.1s ease-in', style({ transform: 'translateX(-100%)', opacity: 0 }))], {
      optional: true,
    }),
  ]),
];
@Component({
  selector: 'app-fly-panel-start-test',
  templateUrl: './fly-panel-start-test.component.html',
  styleUrls: ['./fly-panel-start-test.component.scss'],
  animations: [
    trigger('animSlider', [
      transition(':increment', right),
      transition(':decrement', left),
    ]),
  ],
})
export class FlyPanelStartTestComponent implements OnInit,OnDestroy {


  counter: number = 0;
  subscription = new SubSink();
  startTime = new Date();
  endTime = new Date();

  testId: number = 0;
  isReviewVisible: boolean = false;
  testItemsVM: TestItemVM[] = [];
  testInfoObject: any;
  intervalId;
  answersList: string[] = [];
  testItemTypeId: number;
  fillInTheBlankText: string;
  questionId: number;
  fillInTheBlankArray: BlankIndexWithAnwer[] = [];
  multipleSelectArray: MatchValueWithCorrectValue[] = [];
  multipleSelectArrayList: any[] = [];
  multipleAnswersArrayList: any[] = [];
  matchForm = new UntypedFormGroup({});
  getQuestionById: EmployeeAnswerModel = new EmployeeAnswerModel();
  reviewTestInfo: ReviewTestModel[] = [];
  isReviewed: boolean = false;
  correctAnswers = [];
  shotAnswerDescription:string = ''
  exitDescription: string;
  submitTestRef !: TemplateRef<any>;
  testType: any;
  pretestCorrectIncorrectAnswers: any;
  pretestSubmittedAnswers: any;
  testCorrectIncorrectAnswers: any;
  testSubmittedAnswers: any;
  testRetakeSubmittedAnswers: any;
  testRetakeCorrectIncorrectAnswers: any
  submitEnable: boolean = false;
  allowBack = true;
  isSubmitClick:boolean = false;

  constructor(private _router: Router,
    public dialog: MatDialog,
    private route: ActivatedRoute,
    private testService: TestsService,
    private empService: EmployeesService,
    private elementRef: ElementRef,
    private _sanitizer: DomSanitizer,
    private saveStore: Store<{ getTestInfo: any }>,
    private alert: SweetAlertService,
    private store: Store<{ toggle: string }>,
    private labelPipe: LabelReplacementPipe,
    private rosterTimeRecordSrvc:ApiClassScheduleRosterTimeRecordService
  ) { this.startTime = new Date() }

  answerCheck: boolean = false;

  currTime!: Date;
  async ngOnInit(): Promise<void> {
    this.route.params.subscribe((params: any) => {
      if (params.hasOwnProperty('testId')) {
        this.testId = params['testId'];
        this.getTestsByEmployee();
      }
    });
    this.subscription.sink = this.saveStore.select('getTestInfo').pipe().subscribe((res) => {
      ;
      if (res['saveData'] !== undefined && (res['tabIndex'] === 1 || res['tabIndex'] !== null)) {
        if (res.update) {
          this.testInfoObject = res.saveData;
        }
        else {
          this.testInfoObject = res.saveData.object;
        }

        this.startCountdown();
        this.currTime = new Date();
        this.currTime.setHours(this.testInfoObject.testHours);
        this.currTime.setMinutes(this.testInfoObject.testMinutes)
        // setInterval(()=>{
        //   this.currTime = this.currTime.getSeconds()
        // },1000)

        this.pretestSubmittedAnswers = this.testInfoObject.pretestSubmittedAnswers;
        this.testSubmittedAnswers = this.testInfoObject.testSubmittedAnswers;
        this.testRetakeSubmittedAnswers = this.testInfoObject.testRetakeSubmittedAnswers;
        this.pretestCorrectIncorrectAnswers = this.testInfoObject.pretestCorrectIncorrectAnswers;
        this.testCorrectIncorrectAnswers = this.testInfoObject.testCorrectIncorrectAnswers;
        this.testRetakeCorrectIncorrectAnswers = this.testInfoObject.testRetakeCorrectIncorrectAnswers;
        this.answerCheck = true;
      } else {
        this._router.navigate(['implementation/test/overview']);
      }
    })
    this.exitDescription = `You are choosing to exit the <Test Type>.
    No. of question answered: <enter number answered>
    No. of questions left: <enter number left>
    Contact ` + await this.labelPipe.transform('Instructor') + ` to retake the test
    `;
  }

  @HostListener('window:beforeunload', ['$event'])
  handleUnload(event: Event): void {
    if(!this.isSubmitClick){
      this.createRosterTimeRecordAsync();
    }
  }

  remainingTime: number = 0;

  startCountdown() {
    this.remainingTime = this.testInfoObject.testHours * 60 * 60 + this.testInfoObject.testMinutes * 60;

    this.subscription.sink = interval(1000)
      .pipe(
        takeWhile(() => this.remainingTime !== 0)
      )
      .subscribe(() => {
        if ((this.remainingTime - 1) === 0) {
          this.testResult();
        }
        this.remainingTime--;
      });
  }

  getMinutes() {
    return Math.floor((this.remainingTime % 3600) / 60);
  }

  getHours() {
    return Math.floor((this.remainingTime / 60) / 60);
  }

  async goBack() {
    var key: string = 'fromTest';
    localStorage.setItem("fromTest", "true")
    this.store.dispatch(sideBarOpen());
    this._router.navigate(['implementation/test/overview']);
    await this.createRosterTimeRecordAsync();
  }

  ngOnDestroy(): void {

    this.subscription.unsubscribe();
    this.store.dispatch(sideBarBackDrop({ backdrop: false }));
    this.store.dispatch(sideBarDisableClose({ disableClose: true }));
    this.store.dispatch(sideBarMode({ mode: 'side' }));
  }
  getTimeDifference(startTime, endTime) {


    var date1 = startTime;
    var date2 = endTime;

    var diff = date2.getTime() - date1.getTime();

    var msec = diff;
    var hh = `0${Math.floor(msec / 1000 / 60 / 60)}` as any;
    msec -= hh * 1000 * 60 * 60;

    var mm = `0${Math.floor(msec / 1000 / 60)}` as any;
    msec -= mm * 1000 * 60;

    var ss = `0${Math.floor(msec / 1000)}` as any;
    msec -= ss * 1000;

    return hh.slice(-2) + ' ' + "Hours" + ' ' + mm.slice(-2) + ' ' + "min" + ' ' + ss.slice(-2) + ' ' + 'sec';

  }
  getTestQuestionInformation(questionId: any) {
    this.empService.getQuestionInfo(this.testInfoObject.classScheduleId, this.testId, questionId, this.testInfoObject.rosterId).then((res) => {
      this.correctAnswers = [];
      this.getQuestionById = res;
      this.answersList = [];
      this.fillInTheBlankArray =[];
      this.multipleSelectArrayList=[];
      this.answersList.push(this.getQuestionById?.userAnswer);
      var question = this.testItemsVM.find((f) => {
        return f.id == questionId;
      })
      if (question !== null && question !== undefined) {
        if (question.testItemType.trim().toLowerCase() === "short answers") {
          this.shotAnswerDescription = this.getQuestionById?.userAnswer ?? '';
        }
        if (question.testItemType.trim().toLowerCase() === 'match the column') {
          question.testItemMatches.forEach((data) => {
            this.correctAnswers.push(data.matchValue);
            var userValue  = this.getQuestionById.matchValueWithCorrectValue.find((x) => { return x.correctIndex === data.number })?.userValue;
            var matchValue = question.testItemMatches.find(x=>x.originalMatchValue == userValue)?.matchValue ?? "";
            let obj: MatchValueWithCorrectValue = {
              matchValue: matchValue,
              userValue: userValue,
              correctIndex:data.number,
              id: data.id
            }
            this.multipleSelectArrayList.push(obj);
          })
        }
        if (question.testItemType.trim().toLowerCase() === 'multiple correct answers') {
          this.answersList = this.getQuestionById.multipleCorrectAnswer;
          this.multipleAnswersArrayList = this.answersList.map(a => {
            return {
                selectedAnswerId: 0,
                selectedAnswerValue: a
            };
        });
        }

        this.testItemsVM.forEach((element, i) => {
          if (element.testItemType.trim().toLowerCase() === 'fill in the blank' && questionId === element.id) {
            for (const ans of question.testItemFillBlanks) {
              let previewString = element.description;
              const inputString = previewString;
              var value = `<input  placeholder="fill in the blank" id="${ans.correctIndex}" name="${ans.correctIndex}" `
              const basicPattern = /value="([^"]*)"/;
              const pattern = new RegExp(value + basicPattern.source);
              const match = inputString.match(pattern);
              var myValue = "";
              if (match && match.length > 1) {
                const extractedSubstring = match[1];
                myValue = extractedSubstring;  // Output: ""
              }
              for (const ans of element.testItemFillBlanks) {
                previewString = previewString.replace(
                  `<input  placeholder="fill in the blank" id="${ans.correctIndex}" name="${ans.correctIndex}" value="${myValue}"  type="text"/>`,
                  `<input  placeholder="fill in the blank" id="${ans.correctIndex}" name="${ans.correctIndex}" value="${this.getQuestionById?.blankIndexWithAnswer.find((f) => { return f.correctIndex === ans.correctIndex })?.userValue ?? ''}"  type="text"/>`
                );
              };
              this.fillInTheBlankArray = question.testItemFillBlanks.map((data) => {
                return {
                  correctIndex: data.correctIndex,
                  userValue:this.getQuestionById?.blankIndexWithAnswer.find((f) => { return f.correctIndex === data.correctIndex })?.userValue ?? '',
                }
              })
              this.testItemsVM[i].description = previewString;
              this.testItemsVM[i].descriptionForFillBlanks = this._sanitizer.bypassSecurityTrustHtml(previewString);
            }

          }
        });
      }
    }).catch((res: any) => {

    })
  }
  formCharCode(length) {
    return String.fromCharCode(65 + (length))
  }
  onNext() {

    if (this.counter !== this.testItemsVM.length - 1) {
      this.counter++;
    }
    this.getTestQuestionInformation(this.testItemsVM[this.counter].id);
  }

  onPrevious() {

    if (this.counter > 0) {
      this.counter--;
    }
    this.getTestQuestionInformation(this.testItemsVM[this.counter].id)
  }
  async exitDialog(templateRef) {
    this.exitDescription = `You are choosing to exit the ${this.testInfoObject.testType}.\nNo. of question answered: ${this.counter}\nNo. of questions left: ${this.testItemsVM.length - this.counter}\nContact ` + await this.labelPipe.transform('Instructor') + ` to retake the test
    `;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  exitConformation($event: any) {
    this.empService.exitTest(this.testInfoObject.rosterId).then((res) => {
      if (res) {
        localStorage.setItem("fromTest", "true");
        this.goBack();
      }
      ;
    }).catch((res: any) => {

    })
  }

  reviewTest(testItem, isReview, name) {
    this.allowBack = true;
    this.testType = this.testInfoObject.testType

    this.testItemTypeId = testItem.testItemTypeId;
    this.questionId = testItem.id;
    this.isReviewed = isReview;
    let description = testItem.testItemType.trim().toLowerCase();
    if(description == 'short answers'){
      this.answersList = [this.shotAnswerDescription];
    }
    this.saveEmployeeTest();
    if (name === 'done') {
      this.submitEnable = false;
      this.isReviewVisible = true;
    }


  }

  getMCQCorrect(mcq: TestItemMcq[]) {
    if (Object.keys(this.getQuestionById).length !== 0) {
      return mcq.find(element => element.choiceDescription?.trim()?.toLocaleLowerCase() == this.getQuestionById.userAnswer?.trim()?.toLocaleLowerCase());
    }
    //todo correct this
    return null;
  }

  getTFCorrect(tfs: TestItemTrueFalse[]) {
    if (Object.keys(this.getQuestionById).length !== 0) {
      //this.answersList.push(this.getQuestionById.userAnswer);
      return tfs.find((element) => {
        return element.choices?.trim().toLocaleLowerCase() === this.getQuestionById.userAnswer?.trim().toLocaleLowerCase()
      });
    }
    //todo correct this
    return null;
  }
  checkValueForCheckbox(item) {

    if (Object.keys(this.getQuestionById).length !== 0) {

      return this.getQuestionById.multipleCorrectAnswer.includes(item.choiceDescription);
    }
    return false;
  }

  getReviewMCQCorrect(mcq: TestItemMcq[], userAnswers: any) {
    ;
    if (userAnswers !== null) {
      return mcq.find(x => x.choiceDescription.trim().toLocaleLowerCase() == userAnswers.trim()?.toLocaleLowerCase())
    }
    else {
      return ''
    }

  }

  getReviewValueForCheckbox(mcq: any, userAnswers: any) {
    ;
    if (userAnswers.length > 0) {
      return userAnswers.includes(mcq.choiceDescription);
    }
    else {
      return ''
    }

  }
  getReviewTFCorrect(mcq: TestItemTrueFalse[], userAnswers: any) {
    if (userAnswers !== null) {
      return mcq.find(x => x.choices.trim().toLocaleLowerCase() == userAnswers.trim().toLocaleLowerCase())
    }
    else {
      return ''
    }

  }
  getReviewTestData() {
    this.empService.reviewTest(this.testInfoObject.rosterId).then((res) => {
      this.reviewTestInfo = res;

      this.isReviewVisible = true;
      this.reviewTestInfo.forEach(element => {
        if (element.testItem.testItemType.trim().toLowerCase() === 'fill in the blank') {
          let previewString = element.testItem.description;
          let index=0;
          previewString = previewString.replace(/<u>(.*?)<\/u>/g, function(match) {
            let value = element.blankIndexWithAnswer.find(x => x.correctIndex === element.testItem.testItemFillBlanks[index]?.correctIndex);
            var replacement = value?.userValue == null  || value?.userValue == "" ? `<u>${"&nbsp;".repeat(5)}</u>` :  `<u> ${value.userValue} </u>`;
            index++
            return replacement;
          });
          element.testItem.description = previewString;
          element.testItem.descriptionForFillBlanks = this._sanitizer.bypassSecurityTrustHtml(previewString);
        }
      });
    }).catch((res: any) => {

    })
  }

  compareItems(testItemMatch,matches) {
    if (this.getQuestionById.matchValueWithCorrectValue?.length > 0) {
      var userValue  = this.getQuestionById.matchValueWithCorrectValue.find((x) => { return x.correctIndex === testItemMatch.number })?.userValue;
      var matchValue = matches.find(x=>x.originalMatchValue == userValue)?.matchValue ?? "";
      return matchValue;
    }
    return "";
  }

  compareReviewItems(testItemMatch, matchValue,matches) {
    if (this.reviewTestInfo.length > 0) {
      var userValue  = matchValue.find((x) => { return x.correctIndex === testItemMatch.number })?.userValue;
      var matchValue = matches.find(x=>x.originalMatchValue == userValue)?.matchValue ?? "";
      return matchValue;

    }
  }
  getCorrectAnswers(testItemMatches:TestItemMatch[]){
    return testItemMatches.map(x=>x.matchValue).sort((a,b)=> a.localeCompare(b));
  }

  getTestItem(testItem, doSave: boolean = false) {
    this.allowBack = true;
    this.testItemTypeId = testItem.testItemTypeId;
    this.questionId = testItem.id;
    let description = testItem.testItemType.trim().toLowerCase();
    switch (description) {
      case 'fill in the blank':
        this.saveEmployeeTest();
        break;
      case 'multiple choice questions':
        this.saveEmployeeTest();
        break;
      case 'true / false':
        this.saveEmployeeTest();
        break;
      case 'match the column':
        this.saveEmployeeTest();
        break;
      case 'short answers':
        this.answersList =[this.shotAnswerDescription];
        this.saveEmployeeTest();
        break;
      case 'multiple correct answers':
        this.saveEmployeeTest();
        break;
      default:
        break;
    }
  }
  radioChange(event, testItem) {

    this.answersList = [];
    if (event.value.choices !== undefined) {
      this.answersList.push(event.value.choices)
    }
    else {
      this.answersList.push(event.value.choiceDescription)
    }

  }

  getTestsByEmployee() {
    this.testService.GetTestItemVMLinkedToTest(this.testId).then((res) => {
      this.testItemsVM = res;

      this.testItemsVM.forEach(element => {
        if (element.testItemType.trim().toLowerCase() === 'fill in the blank') {
          let previewString = element.description;
          let index=0;
          previewString = previewString.replace(/<u>(.*?)<\/u>/g, function(match) {
          var replacement = `<input  placeholder="fill in the blank" id="${element.testItemFillBlanks[index]?.correctIndex}" name="${element.testItemFillBlanks[index]?.correctIndex}" value=""  type="text"/>`
          index++
          return replacement;
          });
          element.description = previewString;
          element.descriptionForFillBlanks = this._sanitizer.bypassSecurityTrustHtml(previewString);
        }
      });
      setTimeout(() => {

        if (this.testItemsVM.length > 0 && this.testInfoObject !== null && this.testInfoObject !== undefined) {
          this.getTestQuestionInformation(this.testItemsVM[0].id);
        }
      }, 10)
    }).catch((res: any) => {
      ;

    })
  }

  commentContentClicked(event, testItem: any) {
    let object: BlankIndexWithAnwer = {
      correctIndex: parseInt(event.srcElement.id),
      userValue: event.srcElement.value
    }
    const myInput1 = (<HTMLInputElement>document.getElementById(event.srcElement.id)).innerHTML;
    let index =this.fillInTheBlankArray.findIndex(x=>x.correctIndex == parseInt(event.srcElement.id))
    if(index == -1){
      this.fillInTheBlankArray.push(object);
    }
    else{
      this.fillInTheBlankArray[index]=object;
    }
    this.testItemsVM.forEach((element, i) => {
      if (element.testItemType.trim().toLowerCase() === 'fill in the blank' && testItem.id === element.id) {
        let previewString = element.description;
        const inputString = previewString;
        var value = `<input  placeholder="fill in the blank" id="${object.correctIndex}" name="${object.correctIndex}" `
        const basicPattern = /value="([^"]*)"/;
        const pattern = new RegExp(value + basicPattern.source);
        const match = inputString.match(pattern);
        var myValue = "";
        if (match && match.length > 1) {
          const extractedSubstring = match[1];
          myValue = extractedSubstring;  // Output: ""
        }
        for (const ans of element.testItemFillBlanks) {
          if (ans.correctIndex == object.correctIndex) {
            previewString = previewString.replace(
              `<input  placeholder="fill in the blank" id="${ans.correctIndex}" name="${ans.correctIndex}" value="${myValue}"  type="text"/>`,
              `<input  placeholder="fill in the blank" id="${object.correctIndex}" name="${object.correctIndex}" value="${object.userValue}"  type="text"/>`
            );
          }

        };
        this.testItemsVM[i].description = previewString;
        this.testItemsVM[i].descriptionForFillBlanks = this._sanitizer.bypassSecurityTrustHtml(previewString);
      }
    });



  }

  saveEmployeeTest() {
    let empTestObject: EmpTestCreateOption = {
      employeeId: this.testInfoObject.empId,
      testId: this.testId,
      testItemTypeId: this.testItemTypeId,
      testTypeId: this.testInfoObject.testTypeId,
      userAnswer: this.answersList,
      classScheduleId: this.testInfoObject.classScheduleId,
      questionId: this.questionId,
      blankIndexWithAnwer: this.fillInTheBlankArray,
      correctIndex: 1,
      rosterId: this.testInfoObject.rosterId,
      matchValue: '',
      matchValueWithCorrectValue: this.multipleSelectArrayList,
      shortAnswerDescription: this.shotAnswerDescription,
    }
    this.empService.saveEmployeeTest(empTestObject).then((res) => {
      ;
      this.answersList = [];
      this.fillInTheBlankArray = [];
      this.multipleSelectArray = [];
      this.multipleAnswersArrayList = [];
      this.multipleSelectArrayList = [];
      if (this.isReviewed) {
        this.getReviewTestData();
      }

      this.onNext();

      this.alert.successToast(`Answer has been created `, true);
    }).catch((res: any) => {

    })
  }
  onDropDownChange(event, object,matches) {
    let obj: MatchValueWithCorrectValue = {
      matchValue: event.value,
      userValue: matches.find(x=>x.matchValue == event.value).originalMatchValue,
      correctIndex:object.number,
      id: object.id
    }
      let index = this.multipleSelectArrayList.findIndex(n => n.id === object.id);
      if (index !== -1) {
        this.multipleSelectArrayList[index]=obj;
      }
      else{
        this.multipleSelectArrayList.push(obj);
      }
  }


  checkBoxChange(event, obj1) {
    if (this.multipleAnswersArrayList.length > 0) {
      let index = this.multipleAnswersArrayList.findIndex(n => n.selectedAnswerValue === obj1.choiceDescription);
      if (index !== -1) this.multipleAnswersArrayList.splice(index, 1);

    }
    if (event.checked) {
      let snapshot: any = {
        selectedAnswerId: obj1.id,
        selectedAnswerValue: obj1.choiceDescription
      };
      this.multipleAnswersArrayList.push(snapshot);
      this.answersList = this.multipleAnswersArrayList.map(x => x.selectedAnswerValue)
    }
    else {
      this.answersList = this.multipleAnswersArrayList.map(x => x.selectedAnswerValue)
    }

  }

   async testResult() {
    this.isSubmitClick = true;
    let testTime = this.getTimeDifference(this.startTime, new Date())
    this.saveStore.dispatch(getTestInfo({ saveData: this.testInfoObject, tabIndex: testTime, update: true }));
    this.store.dispatch(freezeMenu({ doFreeze: false }))
    this.store.dispatch(sideBarClose());
    this.store.dispatch(sideBarBackDrop({ backdrop: true }));
    this.store.dispatch(sideBarDisableClose({ disableClose: false }));
    this._router.navigate([`/implementation/test/test-result/${this.testInfoObject.classScheduleId}/${this.testId}`]);
    await this.createRosterTimeRecordAsync();
  }

  async createRosterTimeRecordAsync(){
    var timeRecordOptions = new ClassScheduleRosterTimeRecordVM();
    timeRecordOptions.classSchedule_RosterId = this.testInfoObject.rosterId;
    timeRecordOptions.startDateTime = this.startTime;
    timeRecordOptions.endDateTime = new Date();
    await this.rosterTimeRecordSrvc.createAsync(timeRecordOptions);
  }

  openSubmitDialogue() {
    const deRef = this.dialog.open(this.submitTestRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
    return;
  }

}

