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
import { EnablingObjective } from 'src/app/_DtoModels/EnablingObjective/EnablingObjective';
import { EOWithCountOptions } from 'src/app/_DtoModels/EnablingObjective/EOWithCountOptions';
import { Procedure_EnablingObjective_Link } from 'src/app/_DtoModels/Procedure_EnablingObjective_Link/Procedure_EnablingObjective_Link';
import { Procedure_StatusHistory } from 'src/app/_DtoModels/Procedure_StatusHistory/Procedure_StatusHistory';
import { Procedure_StatusHistoryCreateOptions } from 'src/app/_DtoModels/Procedure_StatusHistory/Procedure_StatusHistoryCreateOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { ProceduresService } from 'src/app/_Services/QTD/procedures.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-procedure-enabling-objective',
  templateUrl: './procedure-enabling-objective.component.html',
  styleUrls: ['./procedure-enabling-objective.component.scss'],
})
export class ProcedureEnablingObjectiveComponent implements OnInit {
  @Output() procedureDeleteCheck  = new EventEmitter<any>();
  @Input() procTitle:string;
  @Input() isActive : any;
  displayColumns: string[] = ['id', 'number', 'description', 'linkageCount'];
  DataSource: MatTableDataSource<any>;
  selection = new SelectionModel<any>(true, []);
  unlinkIds: any[] = [];
  eoNumber:any;

  unlinkDescription = '';
  srcList: any[] = [];
  linkedIds: any[] = [];
  eoId: any[] = [];
  subscriptions = new SubSink();
  procId = '';
  EOIdToShow = '';

  @ViewChild(MatSort) sort : MatSort/*(sort: MatSort)  {
    if (sort) this.DataSource.sort = sort;
  } */
  @ViewChild(MatPaginator) set tblPaging(paginator: MatPaginator) {
    if (paginator) this.DataSource.paginator = paginator;
  }

  title = '';
  constructor(
    public flypanelSrvc: FlyInPanelService,
    private vcf: ViewContainerRef,
    public dialog: MatDialog,
    public route: ActivatedRoute,
    public alert: SweetAlertService,
    public dataBroadCastService: DataBroadcastService,
    public procService: ProceduresService,
    private labelPipe: LabelReplacementPipe,
  ) {}

  ngOnInit(): void {}

  ngAfterViewInit(): void {
    this.subscriptions.sink = this.route.params.subscribe((res: any) => {
      this.procId = res.id;
      this.getEOLinkages(this.procId);
    });

    // To refresh tasks when we link from fly panel.
    this.subscriptions.sink =
      this.dataBroadCastService.updateProcEOLink.subscribe((res: any) => {
        this.DataSource = new MatTableDataSource<any>();
        this.getEOLinkages(this.procId);
      });
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
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

  async getEOLinkages(id: any) {
    let tempSrc: any[] = [];

    this.srcList = tempSrc;
    this.DataSource = new MatTableDataSource(tempSrc);

    this.linkedIds = [];
    await this.procService
      .getLinkedEnablingObjectives(this.procId)
      .then((res: EOWithCountOptions[]) => {
        res.forEach((data: EOWithCountOptions) => {
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
        tempSrc = this.sortEnablingObjectives(tempSrc);
        this.DataSource = new MatTableDataSource(tempSrc);
        /* this.DataSource.sort = this.tblSort; */
        this.DataSource.paginator = this.tblPaging;
      });
      if(tempSrc.length > 0){
        this.procedureDeleteCheck.emit(true);
      }
      else{
        this.procedureDeleteCheck.emit(false);
      }
  }

  sortEnablingObjectives(inputArr : any[]){
    return  inputArr.sort((a, b) => {
       const inputA = a.number.split('.').map(Number);
       const inputB = b.number.split('.').map(Number);
     
       for (let i = 0; i < Math.max(inputA.length, inputB.length); i++) {
         const partA = inputA[i] || 0;
         const partB = inputB[i] || 0;
     
         if (partA !== partB) {
           return partA - partB;
         }
       }
     
       return 0;
     });
   }

  removeFromLinked() {
    this.linkedIds = this.linkedIds.filter((id: any) => {
      return !this.eoId.includes(id);
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

  async unlinkItemsModal(templateRef: any, id?: any) {
    
    this.unlinkDescription = 'You are selecting to unlink the following ' + await this.transformTitle('Enabling Objective') + 's\n';
    this.eoId = [];
    if (id) {
      this.eoId.push(id);
      this.unlinkDescription +=
      this.srcList.find((x) => x.id == id).number + ' - ' + this.srcList.find((x) => x.id == id).description  + '\n';
    } else {
      
      this.unlinkIds.forEach((d,i) => {
        this.eoId.push(d);
        this.unlinkDescription +=
         this.srcList.find((x) => x.id == d).number + ' - ' + this.srcList.find((x) => x.id == d).description + '\n';
      });
     // this.unlinkDescription = this.removeLastWord(this.unlinkDescription);
    }
    this.unlinkDescription +='\n' + 'from ' + await this.transformTitle('Procedure') + ' ' + this.procTitle;
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

  async getData(e: any) {
    if (this.eoId.length > 0) {
      var options = new Procedure_EnablingObjective_Link();
      options.procedureId = this.procId;
      options.EOIds = this.eoId;
      await this.procService
        .UnlinkEOs(this.procId, options)
        .then(async (res: any) => {
          this.selection.clear();
          this.saveHistory(e);
          this.getEOLinkages(this.procId);
          this.unlinkIds = [];
          this.alert.successToast(
            'Successfully Unlinked ' + await this.transformTitle('Enabling Objective') + '(s) from ' + await this.transformTitle('Procedure'));
        })
        .catch(async (err: any) => {
          this.alert.errorToast('Error Unlinking ' + await this.labelPipe.transform('Task') + err);
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
    options.procedureIds.push(this.procId);
    await this.procService
      .saveStatusHistory(options)
      .then(async (res: Procedure_StatusHistory) => {
        this.alert.successAlert(
           await this.transformTitle('Enabling Objective') + ' Unlinked And History Saved'
        );
      })
      .catch(async (err: any) => {
        this.alert.errorToast('Error Saving ' + await this.transformTitle('Procedure') + ' Status History ' + err);
      });
  }

  openProcedureLinkedViewFlyPanel(templateRef: any, data: any) {
    
    this.title = data.description;
    this.EOIdToShow = data.id;
    this.eoNumber = data.number;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flypanelSrvc.open(portal);
  }

  refreshLinkData() {
    this.getEOLinkages(this.procId);
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }
}
