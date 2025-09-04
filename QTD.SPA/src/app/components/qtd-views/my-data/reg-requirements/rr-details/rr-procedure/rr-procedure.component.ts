import { DataSource, SelectionModel } from '@angular/cdk/collections';
import { TemplatePortal } from '@angular/cdk/portal';
import {
  AfterViewInit,
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
  ViewChild,
  ViewContainerRef,
} from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { ActivatedRoute } from '@angular/router';
import { RR_Procedure_LinkOptions } from 'src/app/_DtoModels/RegulatoryRequirements/RR_Procedure_LinkOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { RegulatoryRequirementService } from 'src/app/_Services/QTD/regulatory-requirement.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-rr-procedure',
  templateUrl: './rr-procedure.component.html',
  styleUrls: ['./rr-procedure.component.scss'],
})
export class RRProcedureComponent implements OnInit {
  @Output() regulationDeleteCheck  = new EventEmitter<any>();
  @Input() issuingAuthorityCheck:any;
  @Input() regTitle:any;
  displayColumns: string[] = ['id', 'number', 'description', 'linkageCount'];
  DataSource: MatTableDataSource<any>;
  selection = new SelectionModel<any>(true, []);
  unlinkIds: any[] = [];
  linkedIds:any[]=[];
  procIdtoUnlink: any;
  addTask: boolean = false;
  unlinkDescription = '';
  srcList: any[] = [];
  rrId: any;
  subscription = new SubSink();
  processing: boolean = false;
  procedureNumber:any;
  linkedItem: any;
  @ViewChild(MatSort) sort : MatSort/*(sort: MatSort)  {
    if (sort) this.DataSource.sort = sort;
  } */
  @ViewChild(MatPaginator) set tblPaging(paginator: MatPaginator) {
    if (paginator) this.DataSource.paginator = paginator;
  }
  constructor(
    public flypanelSrvc: FlyInPanelService,
    private vcf: ViewContainerRef,
    public dialog: MatDialog,
    private rrSrvc: RegulatoryRequirementService,
    public route: ActivatedRoute,
    private alert: SweetAlertService,
    private labelPipe: LabelReplacementPipe,
  ) {}

  ngOnInit(): void {
    this.subscription.sink = this.route.params.subscribe((res: any) => {

      this.rrId = res.id;
      this.getLinkedProcedures();
    });
  }

  sortData(sort: Sort) {
    this.DataSource.sort = this.sort;
  }

  /** Whether the number of selected elements matches the total number of rows. */
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

  getLinkedProcedures() {
    this.linkedIds = [];
    this.rrSrvc.getLinkedProcedures(this.rrId).then((res) => {

      let tempSrc: any[] = [];
      res.forEach((d) => {
        this.linkedIds.push(d.id);
        tempSrc.push({
          number: d.number,
          description: d.description,
          linkageCount: d.linkCount,
          id: d.id,
          active:d.active
        });
      });
      this.srcList = tempSrc;
      this.DataSource = new MatTableDataSource(tempSrc);
      this.DataSource.paginator = this.tblPaging;
      if(tempSrc.length > 0){
        this.regulationDeleteCheck.emit(true);
      }
      else{
        this.regulationDeleteCheck.emit(false);
      }
    });
  }

  selectrow(row: any) {
    this.selection.toggle(row);
    this.unlinkIds = [];
    this.selection.selected.forEach((v, i) => {
      this.unlinkIds.push(v.id);
    });
  }

  openFlypanel(templateRef: any, row?: any) {
    if (row) {
      this.linkedItem = row;
      this.procedureNumber = row.number;
    }

    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flypanelSrvc.open(portal);
  }

  getData(e: any) {
    
    let Ids: any[] = [];
    if (this.unlinkIds.length > 0) Ids = this.unlinkIds;
    if (this.procIdtoUnlink) Ids.push(this.procIdtoUnlink);
    Ids = [...new Set(Ids)];
    let data = JSON.parse(e);
    let options: RR_Procedure_LinkOptions = {
      procedureIds: Ids,
      regulatoryRequirementId: this.rrId,
      changeNotes: data['reason'],
      effectiveDate: data['effectiveDate'],
    };
    this.processing = true;
    this.rrSrvc
      .unlinkProcedures(this.rrId, options)
      .then(async (res) => {
        this.alert.successToast(await this.transformTitle('Procedure') + 's unlinked ');
        this.getLinkedProcedures();
      })
      .finally(() => {
        this.unlinkIds = [];
        this.selection.clear();
        this.processing = false;
      });
  }

  async unlinkItemsModal(templateRef: any, id?: any) {
    this.unlinkDescription = 'You are selecting to unlink the following ' + await this.transformTitle('Procedure') + 's\n';

    if (id) {
      this.procIdtoUnlink = id;
      this.unlinkDescription +=
      this.srcList.find((x) => x.id == id).number + ' - ' + this.srcList.find((x) => x.id == id).description + '\n';
    } else {
      this.unlinkIds.forEach((d,i) => {
        this.unlinkDescription +=
        this.srcList.find((x) => x.id == d).number + ' - ' + this.srcList.find((x) => x.id == d).description + '\n';
      });
      //this.unlinkDescription = this.removeLastWord(this.unlinkDescription);
    }
    this.unlinkDescription += ' \n' + 'from Regulations ' + this.regTitle;
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

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }
}
