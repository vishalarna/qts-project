import { SelectionModel } from '@angular/cdk/collections';
import { TemplatePortal } from '@angular/cdk/portal';
import { DatePipe } from '@angular/common';
import { AfterViewInit, Component, EventEmitter, Input, OnDestroy, OnInit, Output, ViewChild, ViewContainerRef } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { ActivatedRoute } from '@angular/router';
import { AnyRecordWithTtl } from 'dns';
import { EmployeeWithCountOptions } from 'src/app/_DtoModels/Employee/EmployeeWithCountOptions';
import { EmployeePosition } from 'src/app/_DtoModels/EmployeePosition/EmployeePosition';
import { PositionOptions } from 'src/app/_DtoModels/Position/PositionOptions';
import { Position_Employee_LinkOptions } from 'src/app/_DtoModels/Position_Employee_Link/Position_Employee_LinkOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { PositionsService } from 'src/app/_Services/QTD/positions.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';
export interface PeriodicElement {
  name: string;
  position: any;
  // options: string;
  weight: any;
  symbol: any;
  pDate:any;

}
const ELEMENT_DATA: PeriodicElement[] = [
  {position: 'Leo morgan', name: 'System Operator', weight: '03-03-2021', symbol: 'Active',pDate:'02-03-2021'},
  {position: 'Dew morgan', name: 'Shift Supervisor', weight: '04-03-2021', symbol: 'In Active',pDate:'02-03-2021'},
  {position: 'Prabhu Sampathkumar', name: 'Generation Operator', weight: '05-03-2021', symbol: 'Active',pDate:'02-03-2021'},
  {position: 'Daniela Petrovic', name: 'Process Improvement Specialist',weight: '06-03-2021', symbol: 'In Active',pDate:'02-03-2021'},
];
@Component({
  selector: 'app-position-employees',
  templateUrl: './position-employees.component.html',
  styleUrls: ['./position-employees.component.scss']
})
export class PositionEmployeesComponent implements OnInit, OnDestroy, AfterViewInit {
  displayColumns: string[] = ['id','image', 'username','positionTitle','startDate','trainee','qualificationDate',];
  DataSource: MatTableDataSource<any>;
  selection = new SelectionModel<any>(true, []);
  unlinkIds: any[] = [];
  subscription = new SubSink();
  posId = '';
  employees: Array<any>;
  linkedEmps: any[] = [];
  empIDs: any[] = [];
  title = '';
  empId = '';
  unlinkDescription = '';
  srcList: any[] = [];
  datePipe = new DatePipe('en-us');
  @Input() isActive = true;
  @Output() positionDeleteCheck  = new EventEmitter<any>();
  @ViewChild(MatSort) sort : MatSort;
  @ViewChild(MatSort) set tblSort(sort: MatSort) {
    if (sort) this.DataSource.sort = sort;
  }
  @ViewChild(MatPaginator) set tblPaging(paginator: MatPaginator) {
    if (paginator) this.DataSource.paginator = paginator;
  }
  constructor(public flypanelSrvc: FlyInPanelService,
    private vcf: ViewContainerRef,
    public dialog: MatDialog,
    private posService: PositionsService,
    private route: ActivatedRoute,
    private alert: SweetAlertService,
    private dataBroadcastService: DataBroadcastService,
    private labelPipe: LabelReplacementPipe,) { }

  ngOnInit(): void { }

  ngAfterViewInit(): void {
    this.subscription.sink = this.route.params.subscribe((res) => {
      this.posId = String(res.id).split('-')[0];
      this.refreshData();
    });
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
  async getLinkData() {
    this.linkedEmps = [];
    this.employees = await this.posService.getLinkedEmployeeWithCount(
      this.posId
    );
    this.employees = this.employees.filter((data)=>{
      return data.active && (data.endDate === null ? true:(new Date(data.endDate) >= new Date(Date.now())))
    })
    this.employees.forEach((eo:any) => {
      this.linkedEmps.push(eo?.employeeId);
    });

    this.DataSource = new MatTableDataSource(this.employees);
    this.DataSource.sortingDataAccessor = (row, property) => {
        switch (property) {
            case 'username':
                return row.fullName;
            case 'positionTitle':
                return row.positionTitle;
            default:
                return row[property];
        }
    };
  }

  toPascalCase(string) {
    return `${string}`
      .toLowerCase()
      .replace(new RegExp(/[-_]+/, 'g'), ' ')
      .replace(new RegExp(/[^\w\s]/, 'g'), '')
      .replace(
        new RegExp(/\s+(.)(\w*)/, 'g'),
        ($1, $2, $3) => `${$2.toUpperCase() + $3}`
      )
      .replace(new RegExp(/\w/), s => s.toUpperCase());
  }

  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.DataSource.data.length;
    return numSelected === numRows;
  }

  /** Selects all rows if they are not all selected; otherwise clear selection. */
  /** Selects all rows if they are not all selected; otherwise clear selection. */
  masterToggle() {
    this.isAllSelected()
      ? this.selection.clear()
      : this.DataSource.data.forEach((row) => this.selection.select(row));

    this.unlinkIds = [];
    this.selection.selected.forEach((v, i) => {
      this.unlinkIds.push(v.employeeId);
    });

  }

  selectrow(row: any) {
    this.selection.toggle(row);
    this.unlinkIds = [];
    this.selection.selected.forEach((v, i) => {
      this.unlinkIds.push(v.employeeId);
    });
  }
  openFlypanel(templateRef: any)
  {
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flypanelSrvc.open(portal);
  }

  sortData() {
    this.DataSource.sort = this.sort;
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }

  async unlinkItemsModal(templateRef: any, id?: any) {
    debugger;
    this.unlinkDescription = 'You are selecting to unlink the selected ' + await this.labelPipe.transform('Employee') + '(s) from ' + await this.transformTitle('Position') +': \n';
    this.empIDs = [];
    if (id) {
      this.empIDs.push(id);
      this.unlinkDescription +=
        '1 - ' + this.employees.find((x) => x.id == id)?.description;
    } else {
      this.unlinkIds.forEach(async (d, i) => {
        this.empIDs.push(d);
        let employee = this.employees.find((x) => x.employeeId === d)?.fullName;
        this.unlinkDescription +=
          ` ${i + 1} - ` +
           await this.labelPipe.transform('Employee')  +
          '\n';
      });
    }
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  getData(e: any) {
    let options = new Position_Employee_LinkOptions();
    options.employeeIds = this.empIDs;
    options.positionId = this.posId;
    var data = JSON.parse(e);
    this.posService.UnlinkEmployees(this.posId, options).then(async (_) => {
      this.alert.successToast('Selected ' + await this.labelPipe.transform('Employee') + '(s) Unlinked from '+ await this.transformTitle('Position'));
      this.refreshData();
    });
  }


  refreshData() {
    this.employees = [];
    this.selection.clear();
    this.unlinkIds = [];
    this.empIDs = [];
    this.getLinkData();

  }

}
