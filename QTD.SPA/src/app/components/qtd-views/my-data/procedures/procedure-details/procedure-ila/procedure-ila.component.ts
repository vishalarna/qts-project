import { SelectionModel } from '@angular/cdk/collections';
import { TemplatePortal } from '@angular/cdk/portal';
import {
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
import { ILA } from 'src/app/_DtoModels/ILA/ILA';
import { ILAWithCountOptions } from 'src/app/_DtoModels/ILA/ILAWithCountOptions';
import { Procedure_ILA_LinkOptions } from 'src/app/_DtoModels/Procedure/Procedure_ILA_Link/Procedure_ILA_LinkOptions';
import { Procedure_StatusHistory } from 'src/app/_DtoModels/Procedure_StatusHistory/Procedure_StatusHistory';
import { Procedure_StatusHistoryCreateOptions } from 'src/app/_DtoModels/Procedure_StatusHistory/Procedure_StatusHistoryCreateOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { ProceduresService } from 'src/app/_Services/QTD/procedures.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-procedure-ila',
  templateUrl: './procedure-ila.component.html',
  styleUrls: ['./procedure-ila.component.scss'],
})
export class ProcedureIlaComponent implements OnInit {
  @Output() procedureDeleteCheck  = new EventEmitter<any>();
  @Input() procTitle:any;
  @Input() isActive : any;
  displayColumns: string[] = ['id', 'number', 'description', 'linkageCount'];
  DataSource: MatTableDataSource<any>;
  selection = new SelectionModel<any>(true, []);
  ILAIdToShow = '';
  title = '';
  unlinkIds: any[] = [];
  unlinkDescription = '';
  srcList: any[] = [];
  subscription = new SubSink();
  iLAId: any[] = [];
  linkedIds: any[] = [];
  ilaNumber:any;
  @ViewChild(MatSort) sort : MatSort
  /* @ViewChild(MatSort) set tblSort(sort: MatSort) {
    if (sort) this.DataSource.sort = sort;
  } */
  @ViewChild(MatPaginator) set tblPaging(paginator: MatPaginator) {
    if (paginator) this.DataSource.paginator = paginator;
  }

  @Input() id = '';
  constructor(
    public flypanelSrvc: FlyInPanelService,
    private vcf: ViewContainerRef,
    public dialog: MatDialog,
    public procService: ProceduresService,
    private alert: SweetAlertService,
    private route: ActivatedRoute,
    private dataBroadcastService: DataBroadcastService,
    private labelPipe: LabelReplacementPipe
  ) {}

  ngOnInit(): void {}

  ngAfterViewInit(): void {
    // To Get ID from route parameter
    this.subscription.sink = this.route.params.subscribe((res: any) => {
      this.id = res.id;
      this.getILALinkages();
    });
   

    // To refresh tasks when we link from fly panel.
    this.subscription.sink =
      this.dataBroadcastService.updateProcILALink.subscribe((res: any) => {
        this.getILALinkages();
      });

  } 
  
  sortData(sort: Sort) { 
    this.DataSource.sort = this.sort; 
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

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }
  
  async getILALinkages() {
    let tempSrc: any[] = [];
    this.linkedIds = [];
    await this.procService
      .getLinkedILA(this.id)
      .then((res: ILAWithCountOptions[]) => {
        res.forEach((data: ILAWithCountOptions) => {
          this.linkedIds.push(data.id);
          tempSrc.push({
            number: data.number,
            description: data.description,
            linkageCount: data.linkCount,
            id: data.id,
            active: data.active,
          });
        });

        this.srcList = tempSrc;
        tempSrc = tempSrc.sort((a, b) => (a.number < b.number ? -1 : 1));
        this.DataSource = new MatTableDataSource(tempSrc);
       // this.DataSource.sort = this.tblSort;
        this.DataSource.paginator = this.tblPaging;
      })
      .catch(async (err: any) => {
        this.alert.errorToast('Error Fetching linked '+ await this.transformTitle('Task') + 's ');
      });
      if(tempSrc.length > 0){
        this.procedureDeleteCheck.emit(true);
      }
      else{
        this.procedureDeleteCheck.emit(false);
      }
  }

  removeFromLinked() {
    this.linkedIds = this.linkedIds.filter((id: any) => {
      return !this.iLAId.includes(id);
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

  openProcedureLinkedViewFlyPanel(templateRef: any, data: any) {
    this.title = data.description;
    this.ILAIdToShow = data.id;
    this.ilaNumber = data.number;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flypanelSrvc.open(portal);
  }

  async getData(e: any) {
    if (this.iLAId.length > 0) {
      var options = new Procedure_ILA_LinkOptions();
      options.procedureId = this.id;
      options.iLAIds = this.iLAId;
      await this.procService
        .UnlinkILAs(this.id, options)
        .then(async (res: any) => {
          this.selection.clear();
          this.unlinkIds = [];
          this.saveHistory(e);
          this.getILALinkages();
          this.alert.successToast(
            'Successfully Unlinked ' + await this.labelPipe.transform('ILA') + '(s) from ' + await this.transformTitle('Procedure'));
        })
        .catch(async (err: any) => {
          this.alert.errorToast('Error Unlinking ' + await this.labelPipe.transform('ILA') + err);
        });
    }
  }

  async saveHistory(e: any) {
    var options = new Procedure_StatusHistoryCreateOptions();
    var data = JSON.parse(e);
    options.changeEffectiveDate = data['effectiveDate'];
    options.changeNotes = data['reason'];
    options.oldStatus = true;
    options.newStatus = false;
    options.procedureIds.push(this.id);
    await this.procService
      .saveStatusHistory(options)
      .then(async (res: Procedure_StatusHistory) => {
        this.alert.successAlert(await this.labelPipe.transform('ILA')  + ' Unlinked And History Saved');
      })
      .catch(async (err: any) => {
        this.alert.errorToast('Error Saving ' + await this.transformTitle('Procedure') + ' Status History ' + err);
      });
  }

  async unlinkItemsModal(templateRef: any, id?: any) {
    
    this.unlinkDescription = 'You are selecting to unlink the following ' + await this.labelPipe.transform('ILA') + '\n';
    this.iLAId = [];
    if (id) {
      this.iLAId.push(id);
      this.unlinkDescription +=
      this.srcList.find((x) => x.id == id).number + ' - '+ this.srcList.find((x) => x.id == id).description + '\n';
    } else {
      this.unlinkIds.forEach((d,i) => {
        this.iLAId.push(d);
        this.unlinkDescription +=
        this.srcList.find((x) => x.id == d).number + ' - '+ this.srcList.find((x) => x.id == d).description + '\n';
      });
   //   this.unlinkDescription = this.removeLastWord(this.unlinkDescription);
    }
    this.unlinkDescription +=' \n' +  'from ' + await this.transformTitle('Procedure') + ' ' + this.procTitle;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
    dialogRef.afterClosed().subscribe((res: any) => {
    });
  }

  removeLastWord(str: string) {
    const lastIndexOfSpace = str.lastIndexOf(' ');

    if (lastIndexOfSpace === -1) {
      return str;
    }
    return str.substring(0, lastIndexOfSpace);
  }

}
