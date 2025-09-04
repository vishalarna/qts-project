import { SelectionModel } from '@angular/cdk/collections';
import { TemplatePortal } from '@angular/cdk/portal';
import { Component, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { ActivatedRoute, Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { Subscription } from 'rxjs';
import { take } from 'rxjs/operators';
import { TestItemStatsVM } from 'src/app/_DtoModels/TestItem/TestItemStatsVM';
import { Test_TestItem_LinkOptions } from 'src/app/_DtoModels/Test_TestItem_Link/Test_TestItem_LinkOptions';
import { TestItemService } from 'src/app/_Services/QTD/test-item.service';
import { TestsService } from 'src/app/_Services/QTD/tests.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { sideBarClose } from 'src/app/_Statemanagement/action/state.menutoggle';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-select-unlinked-questions',
  templateUrl: './select-unlinked-questions.component.html',
  styleUrls: ['./select-unlinked-questions.component.scss']
})
export class SelectUnlinkedQuestionsComponent implements OnInit {

  selectedText = "Unlinked Test Questions";
  displayedColumns = ["cb", "number", "question", "type", "taxonomy", "status"];
  displayedColumnsWithSelected = ["number", "question", "type", "taxonomy", "status"];
  dataSource = new MatTableDataSource<any>();
  originalData = new MatTableDataSource<any>();
  filterString = "";
  selectedId = null;
  isEO = false;
  selectedViewByDescription = "";
  isLoading = false;
  typeFilter = "";
  statusFilter = "";
  stats = new TestItemStatsVM();
  statsLoader = false;
  viewByFilter: string = 'unlinked';
  taxonomyFilter = '';
  selection = new SelectionModel<any>(true, []);
  linkIds: any[] = [];
  isLinkingTestItem: boolean = false;
  showSpinner: boolean = false;

  header = "";
  description = "";
  selectedRow: any;
  disableActions = false;
  questionMode: 'add' | 'copy' | 'edit' = 'add';
  myNavBarState = '';
  subscription = new SubSink();
  testId: any;

  @ViewChild(MatSort, { static: false }) sort: MatSort;
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  editCheck: boolean = false;
  copyCheck: boolean = false;

  constructor(public flyPanelService: FlyInPanelService,
    private vcf: ViewContainerRef,
    private testItemsService: TestItemService,
    public dialog: MatDialog,
    private route: ActivatedRoute,
    private alert: SweetAlertService,
    private dataBroadcastService: DataBroadcastService,
    private router: Router,
    private store: Store<any>,
    private testService: TestsService,) { }

  ngOnInit(): void {
    this.store.dispatch(sideBarClose());


    // Used to apply multiple filter options in single go. Nested with &&.
    this.dataSource.filterPredicate = (row: any, filter: string) => {
      const filterObject = JSON.parse(filter);
      return ((
        row.number.toString().trim().toLowerCase().includes(filterObject.search) ||
        row.question.toString().trim().toLowerCase().includes(filterObject.search)) &&

        (row.type.toString().trim().toLowerCase().includes(filterObject.type))) &&

        (row.taxonomy.toString().trim().toLowerCase().includes(filterObject.taxonomy)) &&

        (filterObject.status === '' || row.isActive.toString().trim().toLowerCase() === filterObject.status)
    }
  }

  ngAfterViewInit(): void {
    this.subscription.sink = this.route.params.subscribe((res: any) => {
      if (res.id !== undefined) {
        this.testId = res.id;
        this.readyData(this.testId);
      }

    });

    this.route.queryParams.subscribe((params: any) => {
      
      if (params.param1 === "edit") {
        this.editCheck = true;
      }
      else if (params.param1 === 'copy') {
        this.copyCheck = true;
      }
    })
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  filterData() {
    var filterObj = {
      search: this.filterString.trim().toLowerCase(),
      type: this.typeFilter.trim().toLowerCase(),
      status: this.statusFilter.trim().toLowerCase(),
      taxonomy: this.taxonomyFilter.trim().toLowerCase(),
    }
    this.dataSource.filter = JSON.stringify(filterObj);
  }

  async readyData(id: any) {
    this.isLoading = true;
    await this.testService.getUnlinkedQuestions(id)
      .then((res: any[]) => {
        
        var tempData: any[] = [];
        res.forEach((element, i) => {
          tempData.push({
            id: element.id,
            type: element.testItemType.description,
            typeId: element.testItemType.id,
            taxonomy: element.taxonomyLevel.description,
            taxonomyId: element.taxonomyLevel.id,
            question: element.description,
            isActive: element.active ? "ACTIVE" : "INACTIVE",
            number: element.number,
            status: element.active,
            eoId: element.eoId,
          });

          this.dataSource.paginator = this.paginator;
          this.dataSource.sort = this.sort;
          this.dataSource.data = tempData;
          this.originalData.data = tempData;
          //this.isLoading = false;
        })


      })
      .finally(() => {
        this.isLoading = false;
      });


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

  async goBack() {
    if (this.editCheck || this.copyCheck) {
      history.back();
    } else {
      localStorage.setItem('stepper', "1");
      this.router.navigate(['dnd/tests/edit/' + this.testId]);
    }
  }
}
