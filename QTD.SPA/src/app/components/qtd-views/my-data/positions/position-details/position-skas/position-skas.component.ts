import { SelectionModel } from '@angular/cdk/collections';
import { TemplatePortal } from '@angular/cdk/portal';
import { AfterViewInit, Component, EventEmitter, Input, OnDestroy, OnInit, Output, ViewChild, ViewContainerRef } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { ActivatedRoute } from '@angular/router';
import { EOWithCountOptions } from 'src/app/_DtoModels/EnablingObjective/EOWithCountOptions';
import { PositionOptions } from 'src/app/_DtoModels/Position/PositionOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { PositionsService } from 'src/app/_Services/QTD/positions.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink/dist/subsink';


export interface PeriodicElement {
  name: string;
  position: any;
  // options: string;
  weight: number;
  symbol: any;


}
const ELEMENT_DATA: PeriodicElement[] = [
  { position: '1.5.2', name: '(LSO/SO) Clear a Live Line Restriction', weight: 2, symbol: 24 },
  { position: '1.5.3', name: '(LSO/SO) Initiate a 34 kV Live Line Restriction', weight: 4, symbol: 10 },
  { position: '1.5.4', name: '(SO) Initiate a 34 kV Live Line Restriction', weight: 6, symbol: 32 },
  { position: '1.5.5', name: '(LSO/SO) Clear a Live Line Prescription', weight: 9, symbol: 1 },
];
@Component({
  selector: 'app-position-skas',
  templateUrl: './position-skas.component.html',
  styleUrls: ['./position-skas.component.scss']
})
export class PositionSkasComponent implements OnInit, OnDestroy, AfterViewInit {
  displayColumns: string[] = ['id', 'number', 'description', 'linkageCount'];
  DataSource: MatTableDataSource<any>;
  selection = new SelectionModel<any>(true, []);
  unlinkIds: any[] = [];
  subscription = new SubSink();
  posId = '';
  enablingObjectives: EOWithCountOptions[];
  linkedEos: any[] = [];
  eoIDs: any[] = [];
  title = '';
  eoId = '';
  unlinkDescription = '';
  srcList: any[] = [];
  @Input() isActive = true;
  @Input() posTitle:string;
  @Output() positionDeleteCheck  = new EventEmitter<any>();

  @ViewChild(MatSort) sort : MatSort;
  // @ViewChild(MatSort) set tblSort(sort: MatSort) {
  //   if (sort) this.DataSource.sort = sort;
  // }
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
    private labelPipe: LabelReplacementPipe) { }

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

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  } 

  sortData(sort: Sort) {
    this.DataSource.sort = this.sort;
  }


  async getLinkData() {
    this.linkedEos = [];
    this.enablingObjectives = await this.posService.getLinkedEOWithCount(
      this.posId
    );
    this.enablingObjectives.forEach((eo) => {
      this.linkedEos.push(eo.id);
    });

    this.DataSource = new MatTableDataSource(this.enablingObjectives);
    this.DataSource.paginator = this.tblPaging;
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
      this.unlinkIds.push(v.id);
    });
  }

  selectrow(row: any) {
    this.selection.toggle(row);
    this.unlinkIds = [];
    this.selection.selected.forEach((v, i) => {
      this.unlinkIds.push(v.id);
    });
  }
  openFlypanel(templateRef: any)
  {
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flypanelSrvc.open(portal);
  }

  async unlinkItemsModal(templateRef: any, id?: any) {
    
    this.unlinkDescription = 'You are selecting to unlink the selected SQs\n\n';
    this.eoIDs = [];
    if (id) {
      this.eoIDs.push(id);
      this.unlinkDescription +=
      this.enablingObjectives.find((x) => x.id == id)?.number + ' - ' + this.enablingObjectives.find((x) => x.id == id)?.description +
      '\n';
    } else {
      this.unlinkIds.forEach((d, i) => {
        this.eoIDs.push(d);
        this.unlinkDescription +=
        this.enablingObjectives.find((x) => x.id == d)?.number + ' - ' +
          this.enablingObjectives.find((x) => x.id == d)?.description +
          '\n';
      });
    }
    this.unlinkDescription += ' \n' + 'from  ' + await this.transformTitle('Position')+ this.posTitle + '.';
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }
  getData(e: any) {

    let options = new PositionOptions();
    options.EOIds = this.eoIDs;
    var data = JSON.parse(e);

    options.changeNotes = data['reason'];
    options.effectiveDate = data['effectiveDate'];
    this.posService.UnlinkEnablingObjective(this.posId, options).then(async (_) => {
      this.alert.successToast('Selected ' + await this.transformTitle('Enabling Objective') + ' Unlinked from SQ');
      //this.dataBroadcastService.refreshPositionData.next();
      this.refreshData();
    });
  }
  refreshData() {
    this.enablingObjectives = [];
    this.selection.clear();
    this.unlinkIds = [];
    this.eoIDs = [];
    this.getLinkData();
    this.positionDeleteCheck.emit(true);
  }
}
