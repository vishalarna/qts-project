import { SelectionModel } from '@angular/cdk/collections';
import { TemplatePortal } from '@angular/cdk/portal';
import {
  AfterViewInit,
  Component,
  EventEmitter,
  Input,
  OnDestroy,
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
import { Procedure_SaftyHazard_Link } from 'src/app/_DtoModels/Procedure_SaftyHazard_Link/Procedure_SaftyHazard_Link';
import { Procedure_SaftyHazard_LinkOptions } from 'src/app/_DtoModels/Procedure_SaftyHazard_Link/Procedure_SaftyHazard_LinkOptions';
import { SafetyHazardWithLinkCount } from 'src/app/_DtoModels/SaftyHazard/SafetyHazardWithLinkCount';
import { SaftyHazard } from 'src/app/_DtoModels/SaftyHazard/SaftyHazard';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { ProceduresService } from 'src/app/_Services/QTD/procedures.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-procedure-safety-hazard',
  templateUrl: './procedure-safety-hazard.component.html',
  styleUrls: ['./procedure-safety-hazard.component.scss'],
})
export class ProcedureSafetyHazardComponent
  implements OnInit, OnDestroy, AfterViewInit
{
  @Output() procedureDeleteCheck  = new EventEmitter<any>();
  @Input() isActive : any;
  @Input() procTitle:any;
  displayColumns: string[] = ['id', 'number', 'description', 'linkageCount'];
  DataSource: MatTableDataSource<any>;
  selection = new SelectionModel<any>(true, []);
  unlinkIds: any[] = [];
  procId = '';
  shIds: any[] = [];
  subscriptions = new SubSink();
  selectedType = 'Safety Hazard';
  title = '';
  shId = '';
  linkedIds: any = [];
  shNumber : any;

  unlinkDescription = '';
  srcList: any[] = [];

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
    public route: ActivatedRoute,
    public procService: ProceduresService,
    public alert: SweetAlertService,
    public dataBroadCastService: DataBroadcastService,
    private labelPipe:LabelReplacementPipe
  ) {}

  ngOnInit(): void {}

  ngAfterViewInit(): void {
    this.subscriptions.sink = this.route.params.subscribe((res: any) => {
      this.procId = res.id;
      this.getSHLinkages(this.procId);
    });

    this.subscriptions.sink =
      this.dataBroadCastService.updateProcSHLink.subscribe((res: any) => {
      //  this.DataSource = new MatTableDataSource<any>();
        this.getSHLinkages(this.procId);
      });
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
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

  async getSHLinkages(id: any) {
    let tempSrc: any[] = [];
    this.linkedIds = [];
    await this.procService
      .getLinkedSafetyHazard(this.procId)
      .then((res: SafetyHazardWithLinkCount[]) => {
        
        res.forEach((data: SafetyHazardWithLinkCount) => {
          this.linkedIds.push(data.id);
          tempSrc.push({
            number: data.number,
            description: data.title,
            linkageCount: data.linkCount,
            id: data.id,
            active: data.active,
          });
          //this.shNumber = data.number;
        });
      })
      .catch(async (err: any) => {
        this.alert.errorToast(
          'Error Fetching '+await this.labelPipe.transform("Safety Hazard") +' Linked to ' + await this.transformTitle('Procedure') + 's ' + err
        );
      });

    this.srcList = tempSrc;
    tempSrc = tempSrc.sort((a, b) => (a.number < b.number ? -1 : 1));
    this.DataSource = new MatTableDataSource(tempSrc);
    this.DataSource.paginator = this.tblPaging;
    if(tempSrc.length > 0){
      this.procedureDeleteCheck.emit(true);
    }
    else{
      this.procedureDeleteCheck.emit(false);
    }
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

  openViewLinkedProcedureFlyPanel(templateRef: any, row: any) {
    this.shId = row.id;
    this.title = row.description;
    this.shNumber = row.number;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flypanelSrvc.open(portal);
  }

  
 async unlinkItemsModal(templateRef: any, id?: any) {
  this.unlinkDescription = `You are selecting to unlink the following ${await this.labelPipe.transform("Safety Hazard") }s\n`;
  
  this.shIds = [];
  if (id) {
    this.shIds.push(id);
    this.unlinkDescription +=
    this.srcList.find((x) => x.id == id).number + ' - ' + this.srcList.find((x) => x.id == id).description;
  } else {
    this.unlinkIds.forEach((d,i) => {
      this.shIds.push(d);
      this.unlinkDescription +=
      this.srcList.find((x) => x.id == d).number + ' - ' + this.srcList.find((x) => x.id == d).description + '\n';
    });
    this.unlinkDescription += ' \n' + 'from ' + await this.transformTitle('Procedure') + 's ' + this.procTitle;
   // this.unlinkDescription = this.removeLastWord(this.unlinkDescription);
  }
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

  getData(e: any) {
    if (this.shIds.length > 0) {
      var data = JSON.parse(e);
      var options = new Procedure_SaftyHazard_Link();
      
      options.saftyHazardIds = this.shIds;
      options.procedureId = this.procId;
      options.effectiveDate = data['effectiveDate'];
      options.changeNotes = data['reason'];
      this.procService
        .unlinkSafetyHazard(this.procId, options)
        .then(async (res: any) => {
          this.alert.successToast(`Successfully Unlinked ${await this.labelPipe.transform("Safety Hazard") }(s)`);
          this.selection.clear();
          this.filterLinkedIds();
          this.unlinkIds = [];
          this.getSHLinkages(this.procId);
        })
        .catch(async (err: any) => {
          this.alert.errorToast(
            'Error Unlinking '+await this.labelPipe.transform("Safety Hazard") +' and saving history ' + err
          );
        });
    }
  }

  filterLinkedIds() {
    this.linkedIds = this.linkedIds.filter((data: any) => {
      return !this.shIds.includes(data);
    });
    
  }

  refreshLinkData() {
    this.getSHLinkages(this.procId);
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }
}
