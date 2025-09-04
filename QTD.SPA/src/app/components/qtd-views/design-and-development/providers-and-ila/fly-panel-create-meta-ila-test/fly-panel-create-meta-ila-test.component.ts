import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { MatStep, MatStepper } from '@angular/material/stepper';
import { StepperOrientation } from '@angular/cdk/stepper';
import { Observable } from 'rxjs';
import { BreakpointObserver } from '@angular/cdk/layout';
import { map } from 'rxjs/operators';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { MetaILA_SummaryTest_ViewModel } from 'src/app/_DtoModels/MetaILA_SummaryTest/MetaILA_SummaryTest_ViewModel';
import { MetaILASummaryTestService } from 'src/app/_Services/QTD/meta-ila-summary-test.service';
import { Test } from 'src/app/_DtoModels/Test/Test';
import { TestCreateOptions } from 'src/app/_DtoModels/Test/TestCreateOptions';
import { TestsService } from 'src/app/_Services/QTD/tests.service';
import { TestItemService } from 'src/app/_Services/QTD/test-item.service';
import { TestItem } from 'src/app/_DtoModels/TestItem/TestItem';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { Test_TestItem_LinkOptions } from 'src/app/_DtoModels/Test_TestItem_Link/Test_TestItem_LinkOptions';
import { TestOptions } from 'src/app/_DtoModels/Test/TestOptions';
import { AddAndSequenceTestQuestionsComponent } from './fly-panel-create-meta-ila-test-components/add-and-sequence-test-questions/add-and-sequence-test-questions.component';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { MetaILAVM } from '@models/MetaILA/MetaILAVM';


@Component({
  selector: 'app-fly-panel-create-meta-ila-test',
  templateUrl: './fly-panel-create-meta-ila-test.component.html',
  styleUrls: ['./fly-panel-create-meta-ila-test.component.scss'],
})
export class FlyPanelCreateMetaILATestComponent implements OnInit {
  @Input() mode : string='create';
  @Input() metaILASummaryTestId:string;
  @Output() closed = new EventEmitter<{ metaILASummaryTestId: any; testType: string }>();
  @Input() metaILADetails: MetaILAVM;
  @Input() testType;
  metaILASummaryTestVM:MetaILA_SummaryTest_ViewModel;
  isFlyPanelAddTestItemOpen:boolean=false;
  isFlyPanelImportTestItemOpen:boolean=false;
  isMainFlyPanelOpen:boolean=true;
  currentStepIndex :number=0;
  isAddTestFormValid:boolean=false;
  questionsTableData: MatTableDataSource<TestItem> = new MatTableDataSource<TestItem>();
  testItemsToPreview:TestItem[]=[];
  stepperOrientation: Observable<StepperOrientation>;
  description = "";
  @ViewChild('stepper') stepper: MatStepper;
  @ViewChild('addSequenceTestQues') addSequenceTestQues!: AddAndSequenceTestQuestionsComponent;
  constructor(
    private breakpointObserver: BreakpointObserver,
    public flyPanelSrvc: FlyInPanelService,
    public metaILASummaryTestService: MetaILASummaryTestService,
    private alert: SweetAlertService,
    private testService: TestsService,
    private testItemService: TestItemService,
    public dialog: MatDialog,
    private labelPipe: LabelReplacementPipe
  ) {
    this.stepperOrientation = breakpointObserver
    .observe('(min-width: 800px)')
    .pipe(map(({ matches }) => (matches ? 'horizontal' : 'vertical')));
   }

   async ngOnInit(): Promise<void> {
    if(this.mode =='create'){
      this.metaILASummaryTestVM=new MetaILA_SummaryTest_ViewModel();
      this.metaILASummaryTestVM.test=new Test();
    }
    else{
     await this.metaILASummaryTestService.getAsync(this.metaILASummaryTestId).then(res=>
      {
        this.metaILASummaryTestVM=res;
      });
      await this.readyPreviewTestData();
      this.questionsTableData= new MatTableDataSource(this.testItemsToPreview);
    }
  }

  async continueClickAsync(e:any){
    if(this.currentStepIndex == 0 && this.metaILASummaryTestVM.id === undefined){
      var test = new TestCreateOptions();
      test.testTitle= this.metaILASummaryTestVM.test.testTitle;
      await this.testService.create(test).then((res: Test) => {
        if(res != null){
          this.metaILASummaryTestVM.test=res;
          this.metaILASummaryTestService.createAsync(this.metaILASummaryTestVM)
          .then(async res=>{
            this.metaILASummaryTestVM = res;
            setTimeout(() => {
              this.stepper.next();
            }, 1);

            this.alert.successToast("Meta " + await this.labelPipe.transform('ILA') + " Summary Test Saved as Draft");
          });
        }
      });
    }
    else{
      var test = new TestCreateOptions();
      test.testTitle= this.metaILASummaryTestVM.test.testTitle;
      await this.testService.update(this.metaILASummaryTestVM.test.id,test).then((res: Test) => {
        if(res != null){
          this.metaILASummaryTestVM.test=res;
          this.metaILASummaryTestService.updateAsync(this.metaILASummaryTestVM.id,this.metaILASummaryTestVM)
          .then(async res=>{
            this.metaILASummaryTestVM = res;
            setTimeout(() => { this.stepper.next();}, 1);
            this.alert.successToast("Meta " + await this.labelPipe.transform('ILA') + " Summary Test updated successfully");
          });
        }
      });
    }
    if(this.currentStepIndex==1){
      this.stepper.next();
    }
    this.scrollToTop();
  }
  onSelectionChange(event: any) {
    this.currentStepIndex = event.selectedIndex ;
    let stepDifference= event.selectedIndex - event.previouslySelectedIndex;
    if(stepDifference>1){
      let matSteps:MatStep[] = this.stepper.steps.toArray();
      for(let i=event.previouslySelectedIndex+1;i<event.selectedIndex;i++){
        matSteps[i].interacted=true;
        matSteps[i].completed=true;
      }
    }
    if(event.previouslySelectedIndex == 1 ){
      this.updateTestItemLinks()
    }
    else if(event.selectedIndex == 2){
      this.readyPreviewTestData();
    }

    this.scrollToTop();
  }
  async updateTestItemLinks(){
      await this.testService.UnlinkAllTestItems(this.metaILASummaryTestVM.test.id);
      var questionLinks = new Test_TestItem_LinkOptions();
      questionLinks.itemSeq = this.addSequenceTestQues.testItemSequence;
      questionLinks.testId = this.metaILASummaryTestVM.test.id;
      questionLinks.testItemIds = this.questionsTableData.data.map(x=>x.id);
      this.testService.LinkTestItem(this.metaILASummaryTestVM.test.id, questionLinks).then(async res=>{
        questionLinks.randomDistractor = this.addSequenceTestQues.randomDistractor;
        //this.testService.UpdateTestItemSequence(this.metaILASummaryTestVM.test.id, questionLinks)
        if(this.currentStepIndex>1){
          this.alert.successToast("Meta " + await this.labelPipe.transform('ILA') + " Summary Test Items updated successfully");
        }
        this.readyPreviewTestData();
      });
  }

  private scrollToTop() {
    const stepperContainer = document.querySelector('.mat-sidenav-content');
    if (stepperContainer) {
      stepperContainer.scrollTop = 0;
    }
  }

  closeFlyPanel() {
    this.closed.emit({ metaILASummaryTestId: this.metaILASummaryTestVM.id, testType: this.testType });
  }
  async questionAdded(event: any) {
    let updatedQuestionsList = await this.updateQuestionsData([event]);
    this.questionsTableData= new MatTableDataSource(updatedQuestionsList);
  }

  async questionsImported(event: any[]) {
    let updatedQuestionsList = await this.updateQuestionsData(event);
    this.questionsTableData= new MatTableDataSource(updatedQuestionsList);
  }
  async updateQuestionsData(questionIDs:string[]):Promise<TestItem[]>{
    let updatedQuestionsList = this.questionsTableData.data;
    var ids =updatedQuestionsList.map((data) => data.id);
    for (const id of questionIDs) {
      const question = await this.testItemService.get(id);
      const questionIndex = ids.findIndex((x) => x == id);
      if (questionIndex === -1) {
        updatedQuestionsList.push(question);
      } else {
        updatedQuestionsList[questionIndex] = question;
      }
    }
    return updatedQuestionsList;
  }

  deleteTestItem(id:string) {
    let testItemIndex= this.questionsTableData.data.findIndex(x=>x.id==id);
    if(testItemIndex != -1){
      this.questionsTableData.data.splice(testItemIndex,1);
    }
    this.questionsTableData= new MatTableDataSource(this.questionsTableData.data);
  }
  openDialog(templateRef: any) {
    this.description = `You are selecting to publish <b>${this.metaILASummaryTestVM.test.testTitle}</b> Test.`;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }
  async publishTest(event: any) {
    var options = new TestOptions();
    options.testIds.push(this.metaILASummaryTestVM.test.id);
    options.actionType = "publish";
    var data = JSON.parse(event);
    options.changeNotes = data['reason'];
    options.effectiveDate = data['effectiveDate'];
    await this.testService.delete(options).then((res: any) => {
      this.alert.successToast("Test Published Successfully");
      this.closed.emit({ metaILASummaryTestId: this.metaILASummaryTestVM.id, testType: this.testType });
    })
  }
  async readyPreviewTestData() {
    this.testItemsToPreview = await this.testService.GetTestItemLinkedToTest(this.metaILASummaryTestVM.test.id);
  }

}
