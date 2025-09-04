import { Component, OnInit } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { Store } from '@ngrx/store';
import parse from 'node-html-parser';
import { Test } from 'src/app/_DtoModels/Test/Test';
import { TestOptions } from 'src/app/_DtoModels/Test/TestOptions';
import { TestItem } from 'src/app/_DtoModels/TestItem/TestItem';
import { TestItemFillBlank } from 'src/app/_DtoModels/TestItemFillBlank/TestItemFillBlank';
import { TestItemMcq } from 'src/app/_DtoModels/TestItemMcq/TestItemMcq';
import { TestItemTrueFalse } from 'src/app/_DtoModels/TestItemTrueFalse/TestItemTrueFalse';
import { TestsService } from 'src/app/_Services/QTD/tests.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { sideBarClose } from 'src/app/_Statemanagement/action/state.menutoggle';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-preview-and-publish',
  templateUrl: './preview-and-publish.component.html',
  styleUrls: ['./preview-and-publish.component.scss']
})
export class PreviewAndPublishComponent implements OnInit {
  testList: any[] = [];
  subscription = new SubSink();
  showSpinner = false;
  testId = "";
  testItems: TestItem[] = [];
  test!: Test;
  description = "";
  showPublishButton = true;

  constructor(
    private store: Store<any>,
    private testService: TestsService,
    private route: ActivatedRoute,
    private alert: SweetAlertService,
    public dialog: MatDialog,
    private router : Router,
  ) { }

  ngOnInit(): void {
    this.store.dispatch(sideBarClose());
    this.subscription.sink = this.route.params.subscribe((res: any) => {

      this.testId = res.id;
      if(res.publish !== undefined && res.publish === "false"){
        this.showPublishButton = false;
      }
      this.readyData();
    })
  }

  async readyData() {
    this.testItems = await this.testService.GetTestItemLinkedToTest(this.testId);
    this.test = await this.testService.get(this.testId);
    this.description = `You are selecting to publish ${this.test.testTitle} Test.`;


  }

  goBack() {
    history.back();
  }

  async publishTest(event: any) {

    this.showSpinner = true;
    var options = new TestOptions();
    options.testIds.push(this.testId);
    options.actionType = "publish";
    var data = JSON.parse(event);
    options.changeNotes = data['reason'];
    options.effectiveDate = data['effectiveDate'];
    await this.testService.delete(options).then((res: any) => {
      this.alert.successToast("Test Published Successfully");
      this.router.navigate(['/dnd/tests/overview']);
    }).finally(() => {
      this.showSpinner = false;
    })
  }

  getCorrectMCQ(mcqs: TestItemMcq[]) {
    return mcqs.find((data) => {
      return data.isCorrect;
    })?.id;
  }

  getCorrectTF(tfs: TestItemTrueFalse[]) {
    return tfs.find((data) => {
      return data.isCorrect;
    })?.id;
  }

  processFIB(description: string, fib: TestItemFillBlank[]) {
    var previewString = description;
    for (const ans of fib) {
      previewString = previewString.replace(
        '<u>'+ans.correct+'</u>',
        `<u>${'&nbsp'.repeat(15)}</u>`
      );
    }

    return previewString

  }

  openDialog(templateRef: any) {
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

}
