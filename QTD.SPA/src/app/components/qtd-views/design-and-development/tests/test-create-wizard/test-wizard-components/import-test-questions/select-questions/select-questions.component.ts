import { SelectionModel } from '@angular/cdk/collections';
import { TemplatePortal } from '@angular/cdk/portal';
import { AfterViewInit, Component, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { MatLegacyTable as MatTable, MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { ActivatedRoute, Router } from '@angular/router';
import { TestItemByEoVM } from '@models/TestItem/TestItemByEoVM';
import { Store } from '@ngrx/store';
import { debug } from 'console';
import { Subscription } from 'rxjs';
import { take } from 'rxjs/operators';
import { TestItem } from 'src/app/_DtoModels/TestItem/TestItem';
import { TestItemByEoOptions } from 'src/app/_DtoModels/TestItem/TestItemByEoOptions';
import { Test_TestItem_LinkOptions } from 'src/app/_DtoModels/Test_TestItem_Link/Test_TestItem_LinkOptions';
import { TestItemService } from 'src/app/_Services/QTD/test-item.service';
import { TestsService } from 'src/app/_Services/QTD/tests.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { sideBarClose } from 'src/app/_Statemanagement/action/state.menutoggle';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-select-questions',
  templateUrl: './select-questions.component.html',
  styleUrls: ['./select-questions.component.scss']
})
export class SelectQuestionsComponent implements OnInit, AfterViewInit {

  showSpinner: boolean = false;
  isTestItemLoading: boolean = false;
  linkedEOIds: any[] = [];
  alreadylinkedEOIds: any[] = [];
  dataSource: MatTableDataSource<any> = new MatTableDataSource();
  selection = new SelectionModel<any>(true, []);
  selectedRow: any;
  linkIds: any[] = [];
  isLinkingTestItem: boolean = false;
  subscription = new SubSink();
  sortSubscription = new SubSink();
  testId: any;
  originalData = new MatTableDataSource<any>();
  editCheck: boolean = false;
  copyCheck: boolean = false;


  // @ViewChild(MatSort, { static: false }) sort: MatSort;
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild('sort', { static: false }) sort!: MatSort;
  @ViewChild('table', { static: false }) table!: MatTable<any>;

  filterString = "";
  typeFilter = "";
  statusFilter = "";
  taxonomyFilter = "";

  displayColumns: string[] = ['eoName', 'cb', 'question', 'type', 'taxonomyLevel', 'id'];
  constructor(private router: Router,
    public flyPanelService: FlyInPanelService,
    private vcf: ViewContainerRef,
    private testItemService: TestItemService,
    private route: ActivatedRoute,
    private testService: TestsService,
    private alert: SweetAlertService,
    private store: Store<any>,
    private dataBroadcastService: DataBroadcastService,) { }

  ngOnInit(): void {
    this.store.dispatch(sideBarClose());
    this.dataSource.filterPredicate = (row: any, filter: string) => {
      const filterObject = JSON.parse(filter);
      return (row.eoName.toString().trim().toLowerCase().includes(filterObject.search) ||
        row.question.toString().trim().toLowerCase().includes(filterObject.search)) &&
        row.taxonomyLevel.trim().toLowerCase().includes(filterObject.taxonomy) &&
        row.type.trim().toLowerCase().includes(filterObject.type)
    }
  }

  ngAfterViewInit(): void {
    // this.subscription.sink = this.route.url.subscribe((url)=>{
    //   if(url.includes('a')){

    //   }
    // })
    this.subscription.sink = this.route.params.subscribe(async (res: any) => {
      
      this.testId = res.id;
      var links = await this.testService.getLinkedEOForILAInTest(this.testId);
      if (links.length > 0) {
        var eoIds = links.map((data) => {
          return data.enablingObjectiveId;
        });
        this.ViewLinkedEOs(eoIds);
      }
    });

    this.subscription.sink = this.route.queryParams.subscribe((params: any) => {
      
      if (params.param1 === "edit") {
        this.editCheck = true;
      }
      else if (params.param1 === 'copy') {
        this.copyCheck = true;
      }
    })
  }

  async goBack() {
    if (this.editCheck || this.copyCheck) {
      history.back();
    } else {
      localStorage.setItem('stepper', "1");
      this.router.navigate(['dnd/tests/edit/' + this.testId]);
    }

  }

  openFlyInPanel(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);

  }

  openPreviewFlyInPanel(templateRef: any, row: any) {
    
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  ViewLinkedEOs(event: any) {
    this.linkedEOIds = event;
    this.getLinkedItems();
  }

  async getLinkedItems() {
    this.isTestItemLoading = true;
    this.sortSubscription.unsubscribe();
    var tempData: any[] = [];
    var options: TestItemByEoOptions = new TestItemByEoOptions();
    options.eoIds = this.linkedEOIds;
    this.testItemService.getItemsForEOs(options).then((res: TestItemByEoVM[]) => {
      
      tempData = this.buildInitialData(res);

      // this.dataSource = new MatTableDataSource(tempData);

      this.dataSource.data = Object.assign(tempData);
      this.originalData.data = Object.assign(tempData);
      this.filterData();

      setTimeout(() => {
        this.dataSource.sort = this.sort;
        this.originalData.sort = this.sort;
      }, 1)
      // this.dataSource.sort = this.sort;
      //this.originalData.data = tempData;
    })
      .finally(() => {
        this.isTestItemLoading = false;
      })

  }

  buildInitialData(data: TestItemByEoVM[]) {
    var tempData: any[] = [];
    data.forEach((data1, i) => {
      var check = i > 0 ? tempData[(i - 1)].eoId !== data1.eoId : true
      //var check = tempData.find((x) => { return x.eoId === data1.eoId }) ? false : true;
      tempData.push({
        id: data1.id,
        taxonomyLevel: data1.taxonomyDescription,
        type: data1.testItemtypeDescription,
        question: data1.questionDescription,
        number: data1.number,
        taxonomyId: data1.taxonomyId,
        typeId: data1.testItemTypeId,
        eoName: data1.eoNumber + ' ' + data1.eoDescription,
        eoId: data1.eoId,
        colspan: data.length,
        shouldUse: check,
      });
    });
    
    return tempData
  }

  buildAfterFilter(data: any[]) {
    var tempData: any[] = [];
    data.forEach((data1, i) => {
      
      var check = i > 0 ? tempData[(i - 1)].eoId !== data1.eoId : true
      //var check = tempData.find((x) => { return x.eoId === data1.eoId }) ? false : true;
      tempData.push({
        id: data1.id,
        taxonomyLevel: data1.taxonomyLevel,
        type: data1.type,
        question: data1.question,
        number: data1.number,
        taxonomyId: data1.taxonomyId,
        typeId: data1.typeId,
        eoName: data1.eoName,
        eoId: data1.eoId,
        colspan: data.length,
        shouldUse: check,
      });
    });
    
    return tempData
  }

  close() {
    this.flyPanelService.close();
  }

  /** Whether the number of selected elements matches the total number of rows. */
  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.dataSource.data.length;
    return numSelected === numRows;
  }

  /** Selects all rows if they are not all selected; otherwise clear selection. */
  masterToggle() {
    this.isAllSelected()
      ? this.selection.clear()
      : this.dataSource.data.forEach((row) => this.selection.select(row));

    this.linkIds = [];
    this.selection.selected.forEach((v, i) => {
      this.linkIds.push(v.id);
    });
  }

  selectrow(row: any) {
    this.selection.toggle(row);
    this.linkIds = [];
    this.selection.selected.forEach((v, i) => {
      this.linkIds.push(v.id);
    });
  }

  async linkTestItems() {
    if (this.copyCheck) {
      this.dataBroadcastService.insertQuestionWhileCopying.pipe(take(1)).subscribe((data: any[]) => {
        if (data && data.length > 0) {
          var toEmit = [...new Set([...data, ...this.linkIds])];
          this.dataBroadcastService.insertQuestionWhileCopying.next(toEmit);
          history.back();
        }
        else {
          this.dataBroadcastService.insertQuestionWhileCopying.next(this.linkIds);
          history.back();
        }
      }).unsubscribe()
    }
    else {
      this.isLinkingTestItem = true;
      var testLink = new Test_TestItem_LinkOptions();
      testLink.testId = this.testId;
      testLink.testItemIds = [];
      testLink.testItemIds = this.linkIds;
      this.testService.LinkTestItem(this.testId, testLink)
        .then(() => {
          this.alert.successToast("Successfully linked the test items");
          this.goBack();
        })
        .finally(() => {
          this.isLinkingTestItem = false;
        })
    }
  }

  filterData() {
    // if (this.filterString.length > 0) {
    //   var filterObj = this.filterString.trim().toLowerCase();
    //   this.dataSource.data = this.originalData.data.filter(x => x.question.match(filterObj));

    //   this.dataSource.data = this.originalData.data.filter(f => f.question.toLowerCase().match(String(this.filterString).toLowerCase()));
    // }
    // else {
    //   this.dataSource.data = this.originalData.data;
    // }
    var filterObj = {
      search: this.filterString.trim().toLowerCase(),
      type: this.typeFilter.trim().toLowerCase(),
      taxonomy: this.taxonomyFilter.trim().toLowerCase(),
    }
    this.dataSource.data = this.originalData.data;
    this.dataSource.filter = JSON.stringify(filterObj);
    //this.originalData.filter = JSON.stringify(filterObj);
    setTimeout(() => {
      var data = this.buildAfterFilter(this.dataSource.filteredData);
      
      this.dataSource.data = data;
    }, 1);
  }


  getRowSpan(row: any) {
    // var idx = this.dataSource.data.findIndex((data)=>{
    //   return data.id === row.id;
    // })
    // var len = 0;
    // this.dataSource.data.forEach((data,i)=>{
    //   if(idx >= i && data.eoId === row.eoId){
    //     len++;
    //   }
    // })
    var len = (this.dataSource.data.filter((data) => {
      return data.eoId === row.eoId;
    }).length);
    if (len === 0) {
      this.dataSource.data = this.dataSource.data.map((data) => {
        data.shouldUse = false;
        data.rowSpan = 0
        return data;
      })
    }
    return len;
  }

  shouldRowBeUsed(row: any) {
    var pos = this.dataSource.data.filter((data) => {
      return data.eoId === row.eoId;
    })

    var index = pos.findIndex((element) => {
      return element.id === row.id;
    });
    
    return index === 0 ? true : false;
  }
}

