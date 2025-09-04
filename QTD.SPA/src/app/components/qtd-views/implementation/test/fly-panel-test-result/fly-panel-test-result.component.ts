import { DatePipe } from '@angular/common';
import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { SubmitTestModel } from '@models/Test/SubmitTestModel';
import { TestItemMatch } from '@models/TestItemMatch/TestItemMatch';
import { Store } from '@ngrx/store';
import { th } from 'date-fns/locale';
import { TestItemMcq } from 'src/app/_DtoModels/TestItemMcq/TestItemMcq';
import { TestItemTrueFalse } from 'src/app/_DtoModels/TestItemTrueFalse/TestItemTrueFalse';
import { EmployeesService } from 'src/app/_Services/QTD/employees.service';
import { sideBarBackDrop, sideBarDisableClose, sideBarMode, sideBarOpen } from 'src/app/_Statemanagement/action/state.menutoggle';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-fly-panel-test-result',
  templateUrl: './fly-panel-test-result.component.html',
  styleUrls: ['./fly-panel-test-result.component.scss']
})
export class FlyPanelTestResultComponent implements OnInit,OnDestroy {

  hideme:any = {};
  testId: number = 0;
  progress: number = 0;
  passingScore: number = 0;
  totalScore: number = 0;
  maxScore: number = 0;
  classId: number = 0;
  reviewTestInfo: SubmitTestModel[] = [];
  correctAnswers = ['A', 'B', 'C', 'D'];
  shotAnswerDescription: ''
  testInfoObject: any;
  datePipe = new DatePipe('en-us');
  timeSpent: string = '';
  todayDate: any;
  showDetails = true;
  currDate = new Date();
  @ViewChild('toPrint') print!: any;
  isPassed: boolean = false;
  subscriptions = new SubSink();
  constructor(private store: Store<{ toggle: string }>,
    private empService: EmployeesService,
    private _router: Router,
    private _sanitizer: DomSanitizer,
    private saveStore: Store<{ getTestInfo: any }>,
    private route: ActivatedRoute,) { }

  ngOnInit(): void {
    this.route.params.subscribe((params: any) => {
      if (params.hasOwnProperty('testId')) {
        this.testId = params['testId'];
        this.classId = params['classId'];
        this.subscriptions.sink = this.saveStore.select('getTestInfo').pipe().subscribe((res) => {
          if (res['saveData'] !== undefined && (res['tabIndex'] === 1 || res['tabIndex'] !== null)) {
            this.testInfoObject = res.saveData;
            // var data = localStorage.getItem("testInfoObj");
            // if(data === null || data === undefined){
            //   localStorage.setItem("testInfoObj",this.testInfoObject);
            // }
            // else{
            //   this.testInfoObject = data;
            // }
            this.getReviewTestData();
            this.todayDate = this.datePipe.transform(Date.now(), 'MM dd, yyyy');


          } else {
            this._router.navigate(['implementation/test/overview']);
          }
        })
      }
    });
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }

  showEntity(item) {
    Object.keys(this.hideme).forEach((h) => {
      this.hideme[h] = false;
    });
    this.hideme[item.testItem.id] = true;
  }

  transform(v: string): SafeHtml {
    return this._sanitizer.bypassSecurityTrustHtml(v);
  }

  async goBack() {
    this.store.dispatch(sideBarOpen());
    this._router.navigate(['implementation/test/overview']);
  }

  formCharCode(length) {
    return String.fromCharCode(65 + (length))
  }

  getReviewTestData() {
    if(this.testInfoObject !== null && this.testInfoObject.rosterId !== undefined){

      var todaysDate = new Date();
      var compDate = todaysDate.toUTCString();
      this.empService.submitTest(this.classId, this.testId,this.testInfoObject.rosterId,compDate).then((res) => {
        this.reviewTestInfo = res;

        this.isPassed = this.reviewTestInfo.find(x => x.passFailStatus !== null).passFailStatus === 'P' ? true : false;
        let passingScore = 0;
        let totalScore = 0;
        let maxScore = 0;
        if(this.reviewTestInfo.length >0){
          passingScore =this.reviewTestInfo[0].passingScore;
          totalScore = this.reviewTestInfo[0].totalScore;
          maxScore = this.reviewTestInfo[0].maximumScore;
        }
        this.passingScore = passingScore;
        this.totalScore = totalScore;
        this.maxScore = maxScore;
        this.timeSpent = this.formatTimeSpan(this.reviewTestInfo[0].totalTestDuration);



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
          }
        });
      });
    }
  }

  formatTimeSpan(timeSpan: string): string {
    const parts = timeSpan.split(':');
    if (parts.length < 3) {
      return 'Invalid TimeSpan';
    }
  
    const hours = parseInt(parts[0], 10);
    const minutes = parseInt(parts[1], 10);
    const seconds = parseFloat(parts[2]);
  
    return `${hours} Hours ${minutes} min ${Math.floor(seconds)} sec`;
  }

  getReviewMCQCorrect(mcq: TestItemMcq[], userAnswers: any) {
    if (userAnswers !== null) {
      return mcq.find(x => x.choiceDescription.trim().toLocaleLowerCase() == userAnswers.trim().toLocaleLowerCase())
    }
    else {
      return ''
    }
  }

  getReviewValueForCheckbox(mcq: any, userAnswers: any) {
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
  compareReviewItems(testItemMatch, matchValue) {
    if (this.reviewTestInfo.length > 0) {
      let object = matchValue.find(x => x.matchValue == testItemMatch?.matchValue)?.userValue;
      if (object !== undefined) {
        return object;
      }
    }
  }
  getCorrectAnswers(testItemMatches:TestItemMatch[]){
    return testItemMatches.map(x=>x.matchValue).sort((a,b)=> a.localeCompare(b));
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

  compareItems(testItemMatch,match,matches) {
      var value  = match.find((x) => { return x.correctIndex === testItemMatch.number })?.userValue;
      var userValue = matches.find(x=>x.originalMatchValue == value)?.matchValue ?? "";
      return userValue;
  }

}
