import { TemplatePortal } from '@angular/cdk/portal';
import { AfterViewInit, Component, OnDestroy, OnInit, TemplateRef, ViewChild, ViewContainerRef } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { Router } from '@angular/router';
import { CKEditorComponent } from '@ckeditor/ckeditor5-angular';
import { Store } from '@ngrx/store';
import parse from 'node-html-parser/dist/parse';
import { TestItemOptions } from 'src/app/_DtoModels/TestItem/TestItemOptions';
import { TestItemStatsVM } from 'src/app/_DtoModels/TestItem/TestItemStatsVM';
import { TestItemService } from 'src/app/_Services/QTD/test-item.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { freezeMenu, sideBarBackDrop, sideBarClose, sideBarOpen } from 'src/app/_Statemanagement/action/state.menutoggle';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-test-question-bank',
  templateUrl: './test-question-bank.component.html',
  styleUrls: ['./test-question-bank.component.scss']
})
export class TestQuestionBankComponent implements OnInit, AfterViewInit, OnDestroy {

  selectedText = "Unlinked Test Questions";
  displayedColumns = ["number", "question", "type", "taxonomy", "status", "actions"];
  displayedColumnsWithSelected = ['selected', "number", "question", "type", "taxonomy", "status", "actions"];
  dataSource = new MatTableDataSource<any>();
  originalData = new MatTableDataSource<any>();
  filterString = "";
  selectedId: any = null;
  isEO = false;
  selectedViewByDescription = "";
  isLoading = false;
  typeFilter = "";
  statusFilter = "";
  stats = new TestItemStatsVM();
  statsLoader = false;
  viewByFilter: string = 'unlinked';
  taxonomyFilter = '';

  header = "";
  description = "";
  selectedRow: any;
  disableActions = false;
  questionMode: 'add' | 'copy' | 'edit' = 'add';
  myNavBarState = '';
  hasFreeze = false;
  isHTML = false;

  subscription = new SubSink();

  filterName: any[] = [];

  @ViewChild(MatSort, { static: false }) sort: MatSort;
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

  constructor(
    public flyPanelService: FlyInPanelService,
    private vcf: ViewContainerRef,
    private testItemsService: TestItemService,
    public dialog: MatDialog,
    private alert: SweetAlertService,
    private dataBroadcastService: DataBroadcastService,
    private router: Router,
    private store: Store<any>,
  ) { }

  ngOnInit(): void {
    this.readyData(this.viewByFilter);
    this.readyStatsData();

    // Used to apply multiple filter options in single go. Nested with &&.
    this.dataSource.filterPredicate = (row: any, filter: string) => {
      const filterObject = JSON.parse(filter);
      return ((
        (row.number === null ? false : row.number.toString().trim().toLowerCase().includes(filterObject.search)) ||
        row.question.toString().trim().toLowerCase().includes(filterObject.search)) &&

        (row.type.toString().trim().toLowerCase().includes(filterObject.type))) &&

        (row.taxonomy.toString().trim().toLowerCase().includes(filterObject.taxonomy)) &&

        (filterObject.status === '' || row.isActive.toString().trim().toLowerCase() === filterObject.status)
    }
  }

  clearFilterName() {
    this.filterName = [];
    this.refreshData();
  }

  ngAfterViewInit(): void {
    this.subscription.sink = this.dataBroadcastService.refreshTestItem.subscribe((res) => {
      if (res.close) {
        this.resetBackDropAndRestoreNavBar();
      }
      this.refreshData();
      this.readyStatsData();
    })
  }

  clearAppliedFilters(){
    this.taxonomyFilter = '';
    this.typeFilter = '';
    this.statusFilter = '';
    this.filterName = [];
    this.refreshData();
  }

  clearFilter() {
    this.filterString = '';
    // this.taxonomyFilter = '';
    // this.typeFilter = '';
    // this.statusFilter = '';
    // this.filterName = [];
    this.refreshData();
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  async readyStatsData() {
    this.statsLoader = true;
    this.stats = await this.testItemsService.getStats();
    this.statsLoader = false;
  }

  async readyData(option: string) {
    this.isLoading = true;

    var data = await this.testItemsService.getAllWithFilterOption(option, this.selectedId);
    var tempData: any[] = [];

    data.forEach((element, i) => {
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
      })
    });
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
    this.dataSource.data = tempData;
    this.originalData.data = tempData;
    this.isLoading = false;
  }

  async openFlyPanel(templateRef: TemplateRef<any>) {

    const portal = new TemplatePortal(templateRef, this.vcf);
    this.store.select('freezeMenu').subscribe((data: boolean) => {
      this.hasFreeze = data;
    }).unsubscribe();
    this.store.dispatch(freezeMenu({ doFreeze: false }))
    this.store.dispatch(sideBarBackDrop({ backdrop: true }));
    var state = this.store.select('toggle');
    state.subscribe((data) => {
      this.myNavBarState = data;
    }).unsubscribe();

    this.store.dispatch(sideBarClose());
    this.flyPanelService.open(portal);
  }

  moduleName: any;
  openFlyInPanelList(templateRef: any, name: any) {
    this.moduleName = name;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  filterData() {
    var filterObj = {
      search: this.filterString.trim().toLowerCase(),
      type: this.typeFilter.trim().toLowerCase(),
      status: this.statusFilter.trim().toLowerCase(),
      taxonomy: this.taxonomyFilter.trim().toLowerCase(),
    }
    this.dataSource.filter = JSON.stringify(filterObj);
    //this.filterName = [];
  }

  eoSelected(event: any) {
    this.closeFlyPanel();
    this.selectedId = event.id;
    this.isEO = true;
    this.selectedViewByDescription = event.description;
    this.readyData(this.viewByFilter);
  }

  ilaSelected(event: any) {
    this.closeFlyPanel();
    this.selectedId = event.id;
    this.isEO = false;
    this.selectedViewByDescription = event.description;
    this.readyData(this.viewByFilter);
  }

  openStatusDialog(templateRef: any, data: any) {
    this.isHTML = true;
    this.header = `Make Test Question ${data.status ? "Inactive" : "Active"}`;
    this.description = `You are selecting to make this Test Question ${data.number} - ${data.question} ${data.status ? "inactive" : "active"}`;
    this.selectedRow = data;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  async statusDialogConfirmed(event: any) {
    this.disableActions = true;
    var options = new TestItemOptions();
    options.actionType = this.selectedRow.status ? "inactive" : "active";
    options.testIds = [];
    options.testIds.push(this.selectedRow.id);
    var data = JSON.parse(event);
    options.changeNotes = data['reason'];
    options.effectiveDate = data['effectiveDate'];
    await this.testItemsService.changeStatus(this.selectedRow.id, options).then((_) => {
      this.alert.successToast(`Test Item Status Changed to ${this.selectedRow.status ? "Inactive" : "Active"}`);
      this.changeStatus();
    }).finally(() => {
      this.disableActions = false;
    })
  }

  changeStatus() {
    var index = this.dataSource.data.findIndex((data) => {
      return data.id === this.selectedRow.id;
    });

    this.dataSource.data[index].status = !this.selectedRow.status;
    this.dataSource.data[index].isActive = this.selectedRow.status ? "ACTIVE" : "INACTIVE";
    this.filterData();
    this.readyStatsData();
  }

  openDeleteDialog(templateRef: any, data: any) {
    this.isHTML = true;
    this.header = "Delete Test Item";
    var question = parse(data.question);
    this.description = `You are selecting to Delete the Test Question ${data.number} - ${question.innerText}. This cannot be Undone.`;
    this.selectedRow = data;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  async deleteDialogConfirmed(event: any) {
    this.disableActions = true;
    var options = new TestItemOptions();
    options.actionType = "delete";
    options.testIds = [];
    options.testIds.push(this.selectedRow.id);
    var data = JSON.parse(event);
    options.changeNotes = data['reason'];
    options.effectiveDate = data['effectiveDate'];
    await this.testItemsService.changeStatus(this.selectedRow.id, options).then((_) => {
      this.alert.successToast(`Test Item Deleted`);
      this.deleteItem();
    }).finally(() => {
      this.disableActions = false;
    })
  }

  deleteItem() {
    this.dataSource.data = this.dataSource.data.filter((data) => {
      return data.id !== this.selectedRow.id;
    });
    this.readyStatsData();
  }

  refreshData() {

    this.readyData(this.viewByFilter);
    this.filterData();
  }

  redirectToBulkEdit() {
    this.router.navigate(['dnd/edit/questions']);
  }

  closeFlyPanel() {
    this.flyPanelService.close();
    this.resetBackDropAndRestoreNavBar();
  }

  resetBackDropAndRestoreNavBar() {
    //this.store.dispatch(freezeMenu({doFreeze:false}))

    this.store.dispatch(sideBarBackDrop({ backdrop: false }));
    if (this.myNavBarState === 'open') {
      this.store.dispatch(freezeMenu({ doFreeze: this.hasFreeze }));
      this.store.dispatch(sideBarOpen());
    }
    else {
      this.store.dispatch(sideBarClose());
    }

  }

  filterNamesChange(name: string, type: 'type' | 'taxonomy' | 'status') {

    switch (type) {
      case 'type':
        this.filterName = this.filterName.filter((data) => {
          return data !== 'Multiple Choice Questions' && data !== 'Multiple Correct Answers'
            && data !== 'True / False' && data !== 'Match The Column'
            && data !== 'Fill in the Blank' && data !== 'Short Answers'
        })
        break;
      case 'taxonomy':
        this.filterName = this.filterName.filter((data)=>{
          return data !== 'Recall' && data !== 'Application'
                && data !== 'Analysis' && data !== 'Evaluate' &&
                data !== 'Create'
        })
        break;
      case 'status':
        this.filterName = this.filterName.filter((data)=>{
          return data !== 'Active' && data !== 'Inactive';
        })
        break;
    }
    this.filterName.push(name);
  }
}
