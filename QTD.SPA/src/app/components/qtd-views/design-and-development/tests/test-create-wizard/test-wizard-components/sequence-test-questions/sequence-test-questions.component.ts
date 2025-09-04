import { SelectionModel } from '@angular/cdk/collections';
import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';
import { TemplatePortal } from '@angular/cdk/portal';
import { Component, Input, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup } from '@angular/forms';
import { MatSort, Sort } from '@angular/material/sort';
import { MatLegacyTable as MatTable, MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { ActivatedRoute, Router } from '@angular/router';
import { TestItemLinkeOptions } from 'src/app/_DtoModels/TestItemLink/TestItemLinkOptions';
import { Test_TestItem_LinkOptions } from 'src/app/_DtoModels/Test_TestItem_Link/Test_TestItem_LinkOptions';
import { TestItemService } from 'src/app/_Services/QTD/test-item.service';
import { TestsService } from 'src/app/_Services/QTD/tests.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

export interface TestPeriodicElement {
  number: any;
  testId: any;
  id: any;
  question: any;
  type: any;
  taxonomyLevel: any;
  taxonomyId:any;
  typeId:any;
}

const ELEMENT_DATA: TestPeriodicElement[] = [

];


@Component({
  selector: 'app-sequence-test-questions',
  templateUrl: './sequence-test-questions.component.html',
  styleUrls: ['./sequence-test-questions.component.scss']
})
export class SequenceTestQuestionsComponent implements OnInit {

  dragDisabled: boolean = true;
  dataSource: MatTableDataSource<TestPeriodicElement> = new MatTableDataSource();
  selection = new SelectionModel<any>(true, []);
  unlinkIds: any[] = [];
  unlinkSpinner: boolean = false;
  showSpinner: boolean = false;
  originalData = new MatTableDataSource<any>();
  selectedRow: any;
  @Input() testId: any;
  subscription = new SubSink();
  linkIds: any[] = [];
  isLoading: boolean = false;
  SeqTestFormGroup: UntypedFormGroup = new UntypedFormGroup({
    checkRandomTest: new UntypedFormControl(false),
    // checkRandomDistractor: new FormControl(false),
    checkRandomDistractor: new UntypedFormControl(false),
  });
  checkRandomDistractor: boolean = false;
  checkRandomTest: boolean = false;


  @ViewChild(MatSort) sort: MatSort/*(sort: MatSort)  {

    if (sort) this.DataSource.sort = sort;

  } */

  @ViewChild('table', { static: true }) table: MatTable<TestPeriodicElement>;



  displayColumns: string[] = ['cb', 'number', 'question', 'type', 'taxonomyLevel', 'del', 'id'];
  constructor(public flyPanelService: FlyInPanelService,
    private vcf: ViewContainerRef,
    private testItemService: TestItemService,
    private route: ActivatedRoute,
    private testService: TestsService,
    private alert: SweetAlertService,
    private fb: UntypedFormBuilder,
    private router: Router,) { }

  ngOnInit(): void {
  }

  async readyTestInformationForm() {
    var test = await this.testService.get(this.testId);
    var isRandom = await this.testService.checkRandom(this.testId);
    this.checkRandomDistractor = test.randomizeDistractors;
    this.checkRandomTest = isRandom;
    
    this.SeqTestFormGroup.patchValue({
      checkRandomTest: isRandom,
      // checkRandomDistractor: new FormControl(false),
      checkRandomDistractor: test.randomizeDistractors,
    });

  }

  ngAfterViewInit(): void {
    this.subscription.sink = this.route.params.subscribe((res: any) => {
      if (res.id !== undefined) {
        this.readyTestInformationForm();
        this.testId = res.id;
        this.getTestItems();
      }

    });


  }

  sortData(sort: Sort) {

    this.dataSource.sort = this.sort;

  }

  drop(event: CdkDragDrop<string>) {
    this.dragDisabled = true;
    const prevIndex = this.dataSource.data.findIndex((d) => d === event.item.data);
    moveItemInArray(this.dataSource.data, event.previousIndex, event.currentIndex);
    this.table.renderRows();
  }

  // drop(event: CdkDragDrop<string>) {
  //   // Return the drag container to disabled.
  //   this.dragDisabled = true;
  //   
  //   

  //   const previousIndex = this.dataSource.data.findIndex((d) => d === event.item.data);

  //   moveItemInArray(this.dataSource.data, previousIndex, event.currentIndex);
  //   this.table.renderRows();

  // }

  openFlyInPanel(templateRef: any) {

    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  openPreviewFlyInPanel(templateRef: any, row: any) {
    
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  close() {
    this.flyPanelService.close();
  }

  async getTestItems() {
    this.showSpinner = true;
    this.isLoading = true;
    this.dataSource = new MatTableDataSource<TestPeriodicElement>();
    this.testService.GetTestItem(this.testId)
      .then((res: any[]) => {
        var tempData: any[] = [];
        res.forEach((data) => {
          tempData.push({
            number: data.number,
            testId: data.testItemLinkId,
            id: data.testItemId,
            question: data.question,
            type: data.testItemType,
            taxonomyLevel: data.taxonomyLevel,
            taxonomyId: data.taxonomyLevelId,
            typeId: data.testItemTypeId,
          });
        });

        this.dataSource = new MatTableDataSource(tempData);
      })
      .finally(() => {
        this.isLoading = false;
        this.showSpinner = false;
      })
  }

  masterToggle() {
    this.isAllSelected()
      ? this.selection.clear()
      : this.dataSource.data.forEach((row) => this.selection.select(row));

    this.linkIds = [];
    this.selection.selected.forEach((v, i) => {
      this.linkIds.push(v.id);
    });
  }

  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.dataSource.data.length;
    return numSelected === numRows;
  }

  selectrow(row: any) {
    this.selection.toggle(row);
    this.linkIds = [];
    this.selection.selected.forEach((v, i) => {
      this.linkIds.push(v.id);
    });
  }

  deleteTestItem(testItem: any) {
    var testLink = new TestItemLinkeOptions();
    testLink.testId = this.testId;
    testLink.testItemIds = [];
    testLink.testItemIds[0] = testItem.id;
    this.testService.unlinkTestItem(testLink).then(() => {
      this.dataSource.data == undefined;
      this.getTestItems();
      this.alert.successToast("Successfully unlinked the test item");
    })
      .catch(() => {
        this.alert.errorToast("Error deleting test items");
      })
  }

  UpdateSequence() {
    let dist = this.checkRandomTest;
    let rand = this.SeqTestFormGroup.get('checkRandomTest')?.value;
    if (this.dataSource !== undefined) {
      this.dataSource.data.forEach((data) => {
        this.linkIds.push(data.id);
      })
      var testLink = new Test_TestItem_LinkOptions();
      testLink.testId = this.testId;
      testLink.testItemIds = [];
      testLink.testItemIds = this.linkIds;
      testLink.randomDistractor = this.checkRandomDistractor;
      testLink.itemSeq = this.checkRandomTest;
      this.testService.UpdateTestItemSequence(this.testId, testLink)
        .then(() => {
          this.alert.successToast("Successfully updated the sequence of linked test items");
          this.router.navigate([`/dnd/tests/publish/${this.testId}`]);
          //this.router.navigate(['/dnd/tests/edit/'+ this.testId]);
        })
        .finally(() => {
        });


    }
  }

  changeRandomTest(event: any) {
    
  }

}
