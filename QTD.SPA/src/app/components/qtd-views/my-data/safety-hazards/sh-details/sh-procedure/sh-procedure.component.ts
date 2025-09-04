import { SelectionModel } from '@angular/cdk/collections';
import { TemplatePortal } from '@angular/cdk/portal';
import { AfterViewInit, Component, EventEmitter, Input, OnDestroy, OnInit, Output, ViewChild, ViewContainerRef } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { ActivatedRoute } from '@angular/router';
import { ProcedureWithLinkCount } from 'src/app/_DtoModels/Procedure/ProcedureWithLinkCount';
import { SaftyHazard_ProcedureLinkOptions } from 'src/app/_DtoModels/SaftyHazard_ProcedureLink/SaftyHazard_ProcedureLinkOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { SafetyHazardsService } from 'src/app/_Services/QTD/safety-hazards.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-sh-procedure',
  templateUrl: './sh-procedure.component.html',
  styleUrls: ['./sh-procedure.component.scss'],
})
export class ShProcedureComponent implements OnInit, AfterViewInit, OnDestroy {
  @Input() shTitle:any;
  displayColumns: string[] = ['id', 'number', 'description', 'linkageCount'];
  DataSource: MatTableDataSource<any>;
  selection = new SelectionModel<any>(true, []);
  unlinkIds: any[] = [];
  addTask: boolean = false;
  unlinkDescription = '';
  srcList: any[] = [];
  procedureNumber:any;

  alreadyLinked: any[] = [];
  shId = "";
  subscription = new SubSink();
  procIds: any[] = [];
  unlinkSpinner = false;
  selectedProcedureId:any;
  procedureTitle:any;
  @Output() taskDeleteCheck  = new EventEmitter<any>();

  @ViewChild(MatSort) sort : MatSort/*(sort: MatSort)  {
    if (sort) this.DataSource.sort = sort;
  } */
  @ViewChild(MatPaginator) set tblPaging(paginator: MatPaginator) {
    if (paginator) this.DataSource.paginator = paginator;
  }

  @Input() isActive : boolean = true;

  constructor(
    public flypanelSrvc: FlyInPanelService,
    private vcf: ViewContainerRef,
    public dialog: MatDialog,
    private route: ActivatedRoute,
    private shService: SafetyHazardsService,
    private alert: SweetAlertService,
    private labelPipe:LabelReplacementPipe
  ) { }

  ngOnInit(): void {

  }

  ngAfterViewInit(): void {
    this.subscription.sink = this.route.params.subscribe((res: any) => {
      this.shId = res.id;
      this.getProcedureLinkage();
    })
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  /** Whether the number of selected elements matches the total number of rows. */
  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.DataSource.data.length;
    return numSelected === numRows;
  }

  sortData(sort: Sort) { 
    this.DataSource.sort = this.sort; 
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

  async getProcedureLinkage() {
    let tempSrc: any[] = [];
    this.alreadyLinked = [];
    await this.shService.getLinkedProcedures(this.shId).then((res: ProcedureWithLinkCount[]) => {
      res.forEach((data: ProcedureWithLinkCount) => {
        this.alreadyLinked.push(data.id);
        tempSrc.push({ number: data.number, description: data.description, linkageCount: data.linkCount, id: data.id, active:data.active });
      });
      this.srcList = tempSrc;
      if(res.length > 0){
        this.taskDeleteCheck.emit(true);
      }
      else if(res.length == 0){
        this.taskDeleteCheck.emit(false);
      }
      this.DataSource = new MatTableDataSource(tempSrc);
      this.DataSource.paginator = this.tblPaging;
    }).finally(() => {

    });


  }

  selectrow(row: any) {
    this.selection.toggle(row);
    this.unlinkIds = [];
    this.selection.selected.forEach((v, i) => {
      this.unlinkIds.push(v.id);
    });
  }

  openFlypanel(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flypanelSrvc.open(portal);
  }

  openFlypanelSHWithProcedure(templateRef: any,row){
    this.selectedProcedureId = row.id;
    this.procedureTitle = row.description;
    this.procedureNumber = row.number;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flypanelSrvc.open(portal);
  }

  getData(e: any) {
    this.unlinkSpinner = true;
    var data = JSON.parse(e);
    var options = new SaftyHazard_ProcedureLinkOptions();
    options.procedureIds = this.procIds;
    options.changeNotes = data['reason'];
    options.effectiveDate = data['effectiveDate'];
    this.shService.unlinkProcedure(this.shId, options).then(async (res: any) => {
      this.alert.successToast("Selected " + await this.transformTitle('Procedure') + "s Unlinked From " + await this.transformTitle('Safety Hazard'));
      this.unlinkIds = [];
      this.procIds = [];
      this.selection.clear();
      this.getProcedureLinkage();
    }).finally(() => {
      this.unlinkSpinner = false;
    })
  }

  async unlinkItemsModal(templateRef: any, id?: any) {
    this.unlinkDescription = 'You are selecting to unlink the following ' + await this.transformTitle('Procedure') + 's \n';
    this.procIds = [];
    if (id) {
      this.procIds.push(id);
      this.unlinkDescription +=
      this.srcList.find((x) => x.id == id).number + ' - ' + this.srcList.find((x) => x.id == id).description + '\n';
    } else {
      this.unlinkIds.forEach((d, i) => {
        this.procIds.push(d);
        
        this.unlinkDescription +=
        this.srcList.find((x) => x.id === d).number + ' - ' + this.srcList.find((x) => x.id === d).description + '\n';
      });
      // this.unlinkDescription = this.removeLastWord(this.unlinkDescription);
    }
    this.unlinkDescription += ' \n' + 'from '+await this.labelPipe.transform("Safety Hazard")+' ' + this.shTitle;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }


  removeLastWord(str: string) {
    const lastIndexOfSpace = str.lastIndexOf(' ');

    if (lastIndexOfSpace === -1) {
      return str;
    }
    return str.substring(0, lastIndexOfSpace);
  }

  refreshData() {
    this.selection.clear();
    this.unlinkIds = [];
    this.getProcedureLinkage();
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }
}
