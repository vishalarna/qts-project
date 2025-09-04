import { moveItemInArray } from '@angular/cdk/drag-drop';
import { TemplatePortal } from '@angular/cdk/portal';
import { Component, OnDestroy, OnInit, ViewContainerRef } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { NavigationExtras, Router } from '@angular/router';
import { Store } from '@ngrx/store';
import parse from 'node-html-parser';
import { take } from 'rxjs/operators';
import { ILA } from 'src/app/_DtoModels/ILA/ILA';
import { ILATraineeEvaluationCreateOptions } from 'src/app/_DtoModels/ILATraineeEvaluation/ILATraineeEvaluationCreateOptions';
import { Provider } from 'src/app/_DtoModels/Provider/Provider';
import { Test } from 'src/app/_DtoModels/Test/Test';
import { TestCreateOptions } from 'src/app/_DtoModels/Test/TestCreateOptions';
import { ReorderTestItemOptions } from 'src/app/_DtoModels/TestItem/ReorderTestItemOptions';
import { TestItem } from 'src/app/_DtoModels/TestItem/TestItem';
import { TestItemCopyOptions } from 'src/app/_DtoModels/TestItem/TestItemCopyOptions';
import { TestItemFillBlank } from 'src/app/_DtoModels/TestItemFillBlank/TestItemFillBlank';
import { TestItemLinkeOptions } from 'src/app/_DtoModels/TestItemLink/TestItemLinkOptions';
import { TestItemMcq } from 'src/app/_DtoModels/TestItemMcq/TestItemMcq';
import { TestItemTrueFalse } from 'src/app/_DtoModels/TestItemTrueFalse/TestItemTrueFalse';
import { TestType } from 'src/app/_DtoModels/TestType/TestType';
import { Test_TestItem_LinkOptions } from 'src/app/_DtoModels/Test_TestItem_Link/Test_TestItem_LinkOptions';
import { IlaTraineeEvaluationService } from 'src/app/_Services/QTD/ila-trainee-evaluation.service';
import { IlaService } from 'src/app/_Services/QTD/ila.service';
import { ProviderService } from 'src/app/_Services/QTD/provider.service';
import { TestItemService } from 'src/app/_Services/QTD/test-item.service';
import { TestTypeService } from 'src/app/_Services/QTD/test-type.service';
import { TestsService } from 'src/app/_Services/QTD/tests.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { freezeMenu, sideBarBackDrop, sideBarClose } from 'src/app/_Statemanagement/action/state.menutoggle';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-test-edit-or-update',
  templateUrl: './test-edit-or-update.component.html',
  styleUrls: ['./test-edit-or-update.component.scss']
})
export class TestEditOrUpdateComponent implements OnInit, OnDestroy {
  newMode: 'edit' | 'copy' = 'edit';
  itemMode: 'add' | 'copy' | 'edit' = 'edit';
  showSpinner = false;
  testId = "";
  testTypes: TestType[] = [];
  providers: Provider[] = [];
  ILAs: any[] = [];
  test: Test;
  testItems: TestItem[] = [];
  questionLoader = false;
  selectedTestItem: any;
  header = "";
  description = "";
  isNavigating = false;


  testForm = new UntypedFormGroup({});
  subscription = new SubSink();
  seqTestFormGroup: UntypedFormGroup = new UntypedFormGroup({});

  constructor(
    private router: Router,
    private store: Store,
    private testTypeService: TestTypeService,
    private ilaService: IlaService,
    private testService: TestsService,
    private providerService: ProviderService,
    public flyPanelSrvc: FlyInPanelService,
    private dataBroadcastService: DataBroadcastService,
    private vcf: ViewContainerRef,
    public dialog: MatDialog,
    private alert: SweetAlertService,
    private ilaTraineeEvalService: IlaTraineeEvaluationService,
    private testItemService: TestItemService,
  ) { }

  ngOnInit(): void {
    this.store.dispatch(freezeMenu({ doFreeze: false }))
    this.store.dispatch(sideBarClose());
    this.store.dispatch(sideBarBackDrop({ backdrop: true }));
    this.readyForm();
    this.readyMode();
    this.readyData();
    this.subscription.sink = this.dataBroadcastService.refreshTestItem.subscribe(async (res: any) => {
      if (res.id !== undefined) {
        if (this.newMode === 'edit') {
          if (this.itemMode === 'add') {
            var options = new Test_TestItem_LinkOptions();
            options.testId = this.testId;
            options.testItemIds.push(res.id);
            await this.testService.LinkTestItem(this.testId, options).then((_) => {
              this.readyTestQuestionData();
            });
          }
          else {
            this.readyTestQuestionData();
          }
        }
        else {
          if (this.itemMode === 'add') {
            this.inserDataInTestQuestions(res.id);
          }
          else {
            this.readyTestQuestionData();
          }
        }
      }
    })
  }

  ngOnDestroy(): void {
    
    if (!this.isNavigating) {
      this.dataBroadcastService.insertQuestionWhileCopying.next([]);
    }
    this.store.dispatch(sideBarBackDrop({ backdrop: false }));
    this.subscription.unsubscribe();
  }

  async inserDataInTestQuestions(id: any) {
    var question = await this.testItemService.getItemWithData(id);
    this.testItems.push(question);
  }

  readyForm() {
    this.testForm.addControl('title', new UntypedFormControl('', Validators.required));
    this.testForm.addControl('instruction', new UntypedFormControl(''));
    this.testForm.addControl('timeHour', new UntypedFormControl(''));
    this.testForm.addControl('timeMin', new UntypedFormControl(''));
    this.testForm.addControl('type', new UntypedFormControl('', Validators.required));
    this.testForm.addControl('provider', new UntypedFormControl('', Validators.required));
    this.testForm.addControl('ila', new UntypedFormControl('', Validators.required));
    this.seqTestFormGroup.addControl('checkRandomTest', new UntypedFormControl(false));
    this.seqTestFormGroup.addControl('checkRandomDistractor', new UntypedFormControl(false));
  }

  testItemIdsForCopy: any[] = [];

  async readyData() {
    this.subscription.sink = this.dataBroadcastService.insertQuestionWhileCopying.pipe(take(1)).subscribe((data: any[]) => {
      if (data && data.length > 0) {
        this.testItemIdsForCopy = data;
      }
    })
    await this.testService.get(this.testId).then((res: Test) => {
      
      this.test = res;
      this.readyTypeData();
      this.readyProviderData();
      this.readyTestQuestionData();
      this.testForm.patchValue({
        title: this.test.testTitle,
        instruction: this.test.ilaTraineeEvaluations[0].testInstruction,
        timeHour: this.test.ilaTraineeEvaluations[0].testTimeLimitHours,
        timeMin: this.test.ilaTraineeEvaluations[0].testTimeLimitMinutes,
      })
      
      this.seqTestFormGroup.patchValue({
        checkRandomDistractor:this.test.randomizeDistractors,
        checkRandomTest:this.test.randomizeQuestionsSequence
      })
    });
  }

  async readyTestQuestionData() {
    this.questionLoader = true;
    this.testItems = await this.testService.GetTestItemLinkedToTest(this.test.id);
    this.testItemIdsForCopy = this.testItemIdsForCopy.filter((data) => {
      return !this.testItems.map((x) => { return x.id }).includes(data.id)
    });
    if (this.testItemIdsForCopy && this.testItemIdsForCopy.length > 0) {
      var options = new TestItemCopyOptions();
      options.testItemIds = this.testItemIdsForCopy;
      options.testId = this.testId;
      var items = await this.testService.getTestItemsForCopyMode(options);
      this.testItems = this.testItems.concat(items);
    }
    this.questionLoader = false;
    this.isReordered = false;
  }


  async readyTypeData() {
    this.testTypes = await this.testTypeService.getAll();
    this.testTypes = this.testTypes.splice(0, 3);
    var type = this.testTypes.find((type) => {
      return type.id === this.test.ilaTraineeEvaluations[0].testTypeId;
    });
    this.testForm.patchValue({
      type: type?.id,
    })
  }

  async readyProviderData() {
    this.providers = await this.providerService.getAll();
    this.ilaService.get(this.test.ilaTraineeEvaluations[0].ilaId).then(async (res: any) => {
      var prov = this.providers.find((pro) => {
        return pro.id === res.providerId;
      });
      this.testForm.patchValue({
        provider: prov?.id,
        ila: res.id
      });
      this.ILAs = await this.ilaService.getByProvider(res.providerId);
      //this.selectProvider(res.id);
    });
  }

  readyMode() {
    var modeFromRoute = this.router.url.split('/');
    this.selectMode(modeFromRoute);
  }

  selectMode(modeFromRoute: string[]) {
    switch (modeFromRoute[2]) {
      case 'edit':
        this.newMode = 'edit';
        this.testId = modeFromRoute[4];
        break;
      case 'copy':
        this.newMode = 'copy';
        this.testId = modeFromRoute[4];
        break;
    }
  }

  goBack() {
    history.back();
  }

  inputChange(event: any) {
    var charCode = event.which ? event.which : event.keyCode;
    // Only Numbers 0-9
    if (charCode < 48 || charCode > 57) {
      event.preventDefault();
      return false;
    } else {
      return true;
    }
  }

  async selectProvider(data: any) {
    this.ILAs = await this.ilaService.getByProvider(data);
    var ila = this.ILAs.find((data) => {
      return data.id === this.test.ilaTraineeEvaluations[0].ilaId;
    });
    this.testForm.patchValue({
      ila: ila?.id,
    })
  }

  getMCQCorrect(mcq: TestItemMcq[]) {
    return mcq.find((data) => {
      return data.isCorrect;
    })?.id;
  }

  getTFCorrect(tfs: TestItemTrueFalse[]) {
    return tfs.find((tf) => {
      return tf.isCorrect
    })?.id;
  }

  openFlyPanel(templateRef: any, testItem: TestItem) {
    this.selectedTestItem = {
      id: testItem.id,
      type: testItem.testItemType.description,
      typeId: testItem.testItemType.id,
      taxonomy: testItem.taxonomyLevel.description,
      taxonomyId: testItem.taxonomyLevel.id,
      question: testItem.description,
      isActive: testItem.active ? "ACTIVE" : "INACTIVE",
      number: testItem.number,
      status: testItem.active,
      eoId: testItem.eoId,
    }
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelSrvc.open(portal);
  }

  openAddFlyPanel(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelSrvc.open(portal);
  }

  openDialog(templateRef: any, testItem: TestItem) {
    this.selectedTestItem = {
      id: testItem.id,
      type: testItem.testItemType.description,
      typeId: testItem.testItemType.id,
      taxonomy: testItem.taxonomyLevel.description,
      taxonomyId: testItem.taxonomyLevel.id,
      question: testItem.description,
      isActive: testItem.active ? "ACTIVE" : "INACTIVE",
      number: testItem.number,
      status: testItem.active,
      eoId: testItem.eoId,
    }

    this.header = `Delete Test Question`;
    this.description = `You are selecting to Delete Test Question ${testItem.number} - ${parse(testItem.description).innerText}. This cannot be undone.`;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  async deleteTestItem(event: any) {
    if (this.newMode === 'copy') {
      this.testItems = this.testItems.filter((data) => {
        return data.id !== this.selectedTestItem.id;
      })
    }
    else {
      var options = new TestItemLinkeOptions();
      options.testId = this.testId;
      options.testItemIds.push(this.selectedTestItem.id);
      var data = JSON.parse(event);
      options.changeNotes = data['reason'];
      options.effectiveDate = data['effectiveDate'];
      await this.testService.unlinkTestItem(options).then((_) => {
        this.alert.successToast("Test Question Removed");
        this.testItems = this.testItems.filter((data) => {
          return data.id !== this.selectedTestItem.id;
        })
      })
    }
  }

  async updateTest(publishEdit: boolean) {
    this.showSpinner = true;
    var options = new TestCreateOptions();
    if (publishEdit === true) {
      options.mode = "publish";
    }
    options.testStatusId = this.test.testStatusId;
    options.testTitle = this.testForm.get('title')?.value;
    options.randomizeDistractors = this.seqTestFormGroup.get('checkRandomDistractor').value
    options.randomizeQuestionsSequence = this.seqTestFormGroup.get('checkRandomTest').value
    await this.testService.update(this.testId, options).then(async (res: Test) => {
      this.showSpinner = true;
      var evalData = new ILATraineeEvaluationCreateOptions();
      evalData.evaluationTypeId = this.test.ilaTraineeEvaluations[0].evaluationTypeId;
      evalData.ilaId = this.testForm.get('ila')?.value;
      evalData.testId = '';
      evalData.testInstruction = this.testForm.get('instruction')?.value;
      evalData.testTimeLimitHours = this.testForm.get('timeHour')?.value;
      evalData.testTimeLimitMinutes = this.testForm.get('timeMin')?.value;
      evalData.testTitle = this.testForm.get('title')?.value;
      evalData.testTypeId = this.testForm.get('type')?.value;
      evalData.trainingEvaluationMethod = '';
      evalData.testId = this.testId;
      if (evalData.testTimeLimitHours === null) {
        evalData.testTimeLimitHours = 0;
      }
      if (evalData.testTimeLimitMinutes === null) {
        evalData.testTimeLimitMinutes = 0;
      }
      await this.ilaTraineeEvalService.update(this.test.ilaTraineeEvaluations[0].id, evalData).then((_) => {
        if (publishEdit === true) {
          this.alert.successToast("Test Data Updated and Published");
        } else {
          this.alert.successToast("Test Data Updated");
        }
        history.back();
      }).finally(() => {
        this.showSpinner = false;
      })
    }).finally(() => {
      this.showSpinner = false;
    })
  }

  async copyTest(isPublish: boolean) {
    this.showSpinner = false;
    var testOptions = new TestCreateOptions();
    testOptions.testTitle = this.testForm.get('title')?.value;
    if (this.newMode === 'copy' && testOptions.testTitle.trim().toLowerCase() === this.test.testTitle.trim().toLowerCase()) {
      testOptions.testTitle = testOptions.testTitle + ' - Copy';
    }
    testOptions.testStatusId = null;
    this.testService.create(testOptions).then(async (res: Test) => {
      this.test = res;
      this.showSpinner = true;
      var evalData = new ILATraineeEvaluationCreateOptions();
      evalData.evaluationTypeId = null;
      evalData.ilaId = this.testForm.get('ila')?.value;
      evalData.testId = '';
      evalData.testInstruction = this.testForm.get('instruction')?.value;
      evalData.testTimeLimitHours = this.testForm.get('timeHour')?.value;
      evalData.testTimeLimitMinutes = this.testForm.get('timeMin')?.value;
      evalData.testTitle = this.testForm.get('title')?.value;
      evalData.testTypeId = this.testForm.get('type')?.value;
      evalData.trainingEvaluationMethod = '';
      evalData.testId = res.id;
      if (evalData.testTimeLimitHours === null) {
        evalData.testTimeLimitHours = 0;
      }
      if (evalData.testTimeLimitMinutes === null) {
        evalData.testTimeLimitMinutes = 0;
      }
      await this.ilaTraineeEvalService.create(evalData).then(async (_) => {
        this.showSpinner = true;
        var options = new Test_TestItem_LinkOptions();
        options.testId = this.testId;
        options.testItemIds = this.testItems.map((data) => {
          return data.id;
        });
        options.itemSeq = true;
        await this.testService.LinkTestItem(res.id, options).then(async (_) => {
          if(!isPublish){
            this.alert.successToast(`Test And Related Questions Copied Successfully`);
            history.back();
          }
        }).finally(() => {
          this.showSpinner = false;
        });
      }).finally(() => {
        this.showSpinner = false;
      });
    }).finally(() => {
      this.showSpinner = false;
    })
  }

  async copyAndPublishTest(){
    await this.copyTest(true);
    this.showSpinner = true;
    var options = new TestCreateOptions();
    options.mode = "publish";
    options.testStatusId = this.test.testStatusId;
    options.testTitle = this.testForm.get('title')?.value;
    await this.testService.update(this.test.id, options).then(async (_) => {
      this.alert.successToast(`Test And Related Questions Copied & Published Successfully`);
      history.back();
    }).finally(() => {
      this.showSpinner = false;
    });
  }

  processFIB(description: string, fib: TestItemFillBlank[]) {
    var previewString = description;
    for (const ans of fib) {
      previewString = previewString.replace(
        ans.correct,
        `<u>${'&nbsp'.repeat(15)}</u>`
      );
    }
    return previewString
  }

  async openFlyInPanel(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelSrvc.open(portal);
  }

  SelectEOByQuestion() {
    this.isNavigating = true;
    this.router.navigate(['/dnd/tests/selectQuestion/' + this.testId], { queryParams: { param1: this.newMode } });
  }

  SelectUnlinkedQuestion() {
    this.isNavigating = true;
    this.router.navigate(['dnd/tests/selectUnlinkedQuestion/' + this.testId], { queryParams: { param1: this.newMode } });
  }

  isReordered: boolean = false;
  drop(event) {
    moveItemInArray(this.testItems, event.previousIndex, event.currentIndex);
    this.isReordered = true;
  }

  reorderLoader: boolean = false;
  // async reorderItems() {
  //   this.reorderLoader = true;
  //   var reorderOptions = new ReorderTestItemOptions();
  //   for (let i = 0; i < this.testItems.length; i++) {
  //     reorderOptions.testItemOrder.push({ itemId: this.testItems[i].id, order: (i + 1) });
  //   }

  //   await this.testService.ReorderItems(this.testId, reorderOptions).then((_) => {
  //     this.alert.successToast("Test Items Reordered Successfully");
  //     this.isReordered = false;
  //   }).finally(() => {
  //     this.reorderLoader = false;
  //   });
  // }

  async updateSequence() {
    this.reorderLoader = true;
    var testLink = new Test_TestItem_LinkOptions();
    testLink.testId = this.testId;
    testLink.testItemIds = [];
    testLink.testItemIds = this.testItems.map(m=>m.id);
    testLink.randomDistractor = this.seqTestFormGroup.get('checkRandomDistractor').value;
    testLink.itemSeq = this.seqTestFormGroup.get('checkRandomTest').value;
    this.testService.UpdateTestItemSequence(this.testId, testLink)
      .then(() => {
        this.alert.successToast("Successfully updated the sequence of linked test items");
        this.isReordered = false;
      })
      .finally(() => {
        this.reorderLoader = false;
        this.readyTestQuestionData();
      });
  }
}


const params = {
  param1: 'edit'
};
const navigationExtras: NavigationExtras = {
  queryParams: params
};


