import { Component, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { IlaService } from 'src/app/_Services/QTD/ila.service';
import { ILA } from 'src/app/_DtoModels/ILA/ILA';
import { TestsService } from 'src/app/_Services/QTD/tests.service';
import { SelectionModel } from '@angular/cdk/collections';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { Router } from '@angular/router';
import { TestStatsVM } from 'src/app/_DtoModels/Test/TestStatsVM';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { TestSortPipe } from './test-sort.pipe';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { TestOptions } from 'src/app/_DtoModels/Test/TestOptions';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { Store } from '@ngrx/store';
import { sideBarOpen } from 'src/app/_Statemanagement/action/state.menutoggle';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { TemplatePortal } from '@angular/cdk/portal';
import { isConstructorDeclaration } from 'typescript';
import { TestFilterOptions } from 'src/app/_DtoModels/Test/TestFilterOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { ILADetailsVM } from '@models/ILA/ILADetailsVM';

@Component({
  selector: 'app-tests-overview',
  templateUrl: './tests-overview.component.html',
  styleUrls: ['./tests-overview.component.scss'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({ height: '0px', maxHeight: '0px' })),
      state('expanded', style({ height: '*' })),
      state('collapsed, void', style({ height: '0px', minHeight: '0' })),
      transition(
        'expanded <=> collapsed',
        animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')
      ),
    ]),
  ],
})
export class TestsOverviewComponent implements OnInit {

  selectedText = "Unlinked Test";
  displayedColumns = ["expand", "provName", "ilaNum", "ilaTitle", "nickname", "credithours", "deliverymethod", "isMeta"];
  displayedExpandedColumns: string[] = [
    'testID',
    'testTitle',
    'numOfQuestion',
    'testType',
    'testStatus',
    'actions',
  ];
  sortCol = 'testNum';
  sortOrder = 'asc';
  toggle = false;
  testByILAdataSource = new MatTableDataSource<any>();
  originalData = new MatTableDataSource<any>();
  filterString = "";
  viewSelected = "testByILA";
  filterValue: any = '';
  expandedData: any | null;
  isLoading: boolean = false;
  expandedDataSource: any[] = [];
  expandedTableDataSource: MatTableDataSource<any> = new MatTableDataSource();
  originalExpandedTableDataSource: MatTableDataSource<any> = new MatTableDataSource();
  selection_test = new SelectionModel<any>(true, []);
  selection_ila = new SelectionModel<any>(true, []);
  selection = new SelectionModel<any>(true, []);
  testStats !: TestStatsVM;
  testId = "";
  status: 'active' | 'inactive' | 'none' = 'none';
  FilterProviderILAData:any;

  innerLoader = false;

  testPipe: TestSortPipe;

  //private paginator: MatPaginator;
  private sort: MatSort;
  testFilterOptions : TestFilterOptions= new TestFilterOptions();

  header = "";
  description = "";
  selectedRow: any;
  filterProvider:boolean=false;


  @ViewChild('providerSort') set metaSort(sorting: MatSort) {
    this.sort = sorting;
    this.setDataSourceAttributes()
  }

  @ViewChild(MatPaginator) set tblPaging(paginator: MatPaginator) {
    if (paginator) this.testByILAdataSource.paginator = paginator;
  }
  constructor(
    private ilaService: IlaService,
    private testService: TestsService,
    public router: Router,
    public dialog: MatDialog,
    private alert: SweetAlertService,
    private store: Store,
    public flyPanelService: FlyInPanelService,
    private vcf: ViewContainerRef,
    private labelPipe:LabelReplacementPipe
  ) { }

  ngOnInit(): void {
    this.store.dispatch(sideBarOpen());
    this.testFilterOptions.onlyWithLink=true;
    this.readyData(this.filterString);
    this.readyStats();
    this.setInitialTestStatus();
    // this.expandedTableDataSource.filterPredicate = (row: any, filter: string) => {
    //   
    //   const filterObject = JSON.parse(filter);
    //   return ((
    //     row.testTitle.toString().trim().toLowerCase().includes(filterObject.search)))
    // }
  }

  setDataSourceAttributes() {
    this.testByILAdataSource.sort = this.sort;
  }

  clearFilter(){
    this.filterString = '';
    this.filterName1 = '';
    this.readyData("");
    this.applyStatusFilter('none');
  }

  async readyStats() {
    this.testStats = await this.testService.getStats();
    
  }

  async readyData(filter:string) {
    this.isLoading = true;
    this.testFilterOptions.filter=filter;
    await this.ilaService.getWithLinks(this.testFilterOptions).then((res: ILADetailsVM[]) => {
      var tempData: any[] = [];
      res.forEach((data) => {
        tempData.push({
          id: data.id,
          ilaNum: data.number,
          ilaTitle: data.name,
          provName:data?.providerName ?? "N/A",
          nickname: data.nickName,
          credithours: data.ilaTraineeEvaluationCount,
          deliverymethod: data.deliveryMethodName ? data.deliveryMethodName:"N/A",
          isMeta: 'No',
          status: "Published"
        });
      });

      this.testByILAdataSource = new MatTableDataSource(tempData);
      //this.testByILAdataSource.data = tempData;
      this.originalData.data = tempData;
    })
      .finally(() => {
        this.isLoading = false;
      })




  }

  filterData() {
    this.readyData(this.filterString);
  }

  filterDataSource(e: Event) {
    let filter = (e.target as HTMLInputElement).value;
    this.filterValue = filter;
    if (this.viewSelected === 'testByILA') {
      this.testByILAdataSource.filter = filter;
    }
  }

  getTestLinkedILAs(id: any) {
    this.innerLoader = true;
    this.expandedTableDataSource.data = [];
    this.testService.getTestLinkedtoILA(id).then((res: any) => {
      let temp: any[] = [];
      res.forEach((data) => {
        temp.push({
          id: data.id,
          testTitle: data.testTitle,
          testType: data.testType,
          testNum: Number(data.testNum),
          numOfQuestion: data.numberOfQuestions,
          testStatus: data.testStatus,
          active: data.active,
          isPublished: data.isPublished,
          isReleased : data.isReleased
        });
      })

      this.expandedDataSource = Object.assign(temp);
      this.expandedTableDataSource.data = Object.assign(this.expandedDataSource);
      this.originalExpandedTableDataSource.data = Object.assign(this.expandedDataSource);
      // this.testPipe = new TestSortPipe();
      // this.expandedTableDataSource = this.testPipe.transform(this.expandedTableDataSource, [this.sortCol, this.sortOrder]);
      this.innerLoader = false;
      this.applyStatusFilter(this.status);
    });
  }


  masterToggleILAList() {
    this.isAllSelectedILAList()
      ? this.selection_ila.clear()
      : this.expandedTableDataSource?.data.forEach((row) =>
        this.selection_ila.select(row)
      );
  }

  isAllSelectedILAList() {
    
    const numSelectedProvider = this.selection_ila.selected.length;
    const numRowsProvider = this.expandedTableDataSource?.data.length;
    return numSelectedProvider === numRowsProvider;
  }

  sortInnerColumn(col: string) {
    this.sortCol = col;
    this.sortOrder = this.sortOrder === 'asc' ? 'desc' : 'asc';
  }

  masterToggle() {
    this.isAllSelected()
      ? this.selection.clear()
      : this.testByILAdataSource.data.forEach((row) => this.selection.select(row));
  }

  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.testByILAdataSource.data.length;
    return numSelected === numRows;
  }
  redirectToBulkEdit() {
    this.router.navigate(['dnd/edit/test']);
  }

  routeWithData(mode: string, id: any) {
    this.router.navigate([`dnd/${mode}/tests/${id}`]);
  }

  deleteTest(data: any, templateRef: any) {
    this.header = "Delete Test";
    this.description = `You are selecting to delete Test ${data.testNum} - ${data.testTitle}. This cannot be undone.`;
    this.selectedRow = Object.assign(data);
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  async changeTestStatus(data: any, templateRef: any) {
    this.header = `Make Test ${data.active ? "Inactive" : "Active"}`;
    if(data.active){
      this.description = `You are selecting to Make the Test ${data.testNum} - ${data.testTitle} Inactive. All Test history and ` + await this.labelPipe.transform('Employee') + ` records will be retained.`;
    }else{
      this.description = `You are selecting to Make the Test ${data.testNum} - ${data.testTitle} Active`;
    }
    this.selectedRow = Object.assign(data);
    
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  async performDelete(event: any) {
    var options = new TestOptions();
    options.actionType = "Delete";
    options.testIds.push(this.selectedRow.id);
    var data = JSON.parse(event);
    options.changeNotes = data['reason'];
    options.effectiveDate = data['effectiveDate'];
    await this.testService.delete(options).then((_) => {
      this.alert.successToast(`Test Successfully Deleted`);
      this.getTestLinkedILAs(this.expandedData.id);
      this.readyStats();
    })
  }

  async performStatusChange(event: any) {
    var options = new TestOptions();
    options.actionType = `${this.selectedRow.active ? "Inactive" : "Active"}`;
    options.testIds.push(this.selectedRow.id);
    var data = JSON.parse(event);
    options.changeNotes = data['reason'];
    options.effectiveDate = data['effectiveDate'];
    
    await this.testService.delete(options).then((_) => {
      this.alert.successToast(`Test Successfully Made ${options.actionType}`);
      this.changeStatusFromSource();
      this.readyStats();
    })
  }

  removeTestFromSource() {
    
    var data = this.expandedTableDataSource.data.filter((data) => {
      
      return data.id !== this.selectedRow.id;
    });
    this.expandedTableDataSource = new MatTableDataSource(data);
  }

  changeStatusFromSource() {
    
    this.expandedTableDataSource.data = this.expandedTableDataSource.data.map((data) => {
      if (data.id === this.selectedRow.id) {
        data.active = this.selectedRow.active ? false : true;
        data.testStatus = `${this.selectedRow.active ? (this.selectedRow.isPublished ? `Published` : `In Development`) : `Inactive`}`;
      }
      return data;
    })

    this.applyStatusFilter(this.status);
  }

  ilaChanged() {
    if (this.expandedData) {
      this.getTestLinkedILAs(this.expandedData.id);
    }
  }

  openFlyPanel(templateRef: any,name:any) {
    if(name === 'filterProvider'){
      this.filterProvider = true;
    }else if(name === 'changeILA'){
      this.filterProvider=false;
    }
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  filterName:any []=[];
  filterName1:any;
  applyStatusFilter(status: 'active' | 'inactive' | 'none') {
    this.status = status;
    if (this.expandedData) {
      if (this.status !== 'none') {
        var filter = this.status === 'active' ? true : false;
        
        this.expandedTableDataSource.data = this.originalExpandedTableDataSource.data.filter((data) => {
          return data.active === filter;
        });
      }
      else {
        this.expandedTableDataSource.data = this.originalExpandedTableDataSource.data;
      }

      this.testPipe = new TestSortPipe();
      this.expandedTableDataSource = this.testPipe.transform(this.expandedTableDataSource, [this.sortCol, this.sortOrder]);
    }
    this.filterName.push(status)
  }

  outputProvider:any;
  GetFilterData(event:any){
    this.FilterProviderILAData = event;    
    let tempData: any[] = [];
    this.FilterProviderILAData.forEach((data) => {
      tempData.push({
        id: data.id,
        ilaNum: data.number,
        ilaTitle: data.name,
        nickname: data.nickName,
        credithours: data.ilaTraineeEvaluations.length,
        deliverymethod: data.deliveryMethod ? data.deliveryMethod.name:"N/A",
        isMeta: 'No',
        status: "Published"
      });
    });

    this.testByILAdataSource = new MatTableDataSource(tempData);
  }


  moduleName:any;
  openFlyInPanel(templateRef: any,name:any) {
    this.moduleName = name;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  setInitialTestStatus(){
    this.filterName1 = 'Active';
    this.applyStatusFilter('active');
  }
}
