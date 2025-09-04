import { SelectionModel } from '@angular/cdk/collections';
import { TemplatePortal } from '@angular/cdk/portal';
import { Component, EventEmitter, OnInit, Output,Input, ViewChild, ViewContainerRef } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { ActivatedRoute } from '@angular/router';
import { RegulatoryRequirementWithLinkCount } from 'src/app/_DtoModels/RegulatoryRequirements/RegulatoryRequirementWithLinkCount';
import { RR_EO_LinkOptions } from 'src/app/_DtoModels/RR_EnablingObjective/RR_EO_LinkOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { RegulatoryRequirementService } from 'src/app/_Services/QTD/regulatory-requirement.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-rr-enabling-objective',
  templateUrl: './rr-enabling-objective.component.html',
  styleUrls: ['./rr-enabling-objective.component.scss'],
})
export class RREnablingObjectiveComponent implements OnInit {
  @Output() regulationDeleteCheck  = new EventEmitter<any>();
  @Input() issuingAuthorityCheck:any;
  @Input() regTitle:any;
  displayColumns: string[] = ['id', 'number', 'description', 'linkageCount'];
  DataSource: MatTableDataSource<any>;
  selection = new SelectionModel<any>(true, []);
  unlinkIds: any[] = [];

  unlinkDescription = '';
  srcList: any[] = [];
  linkedIds: any[] = [];
  eoId: any[] = [];
  subscriptions = new SubSink();
  rrId = '';
  EOIdToShow = '';
  eoNumber:any;

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
    public rrService: RegulatoryRequirementService,
    private labelPipe: LabelReplacementPipe
  ) {}

  ngOnInit(): void {}

  ngAfterViewInit(): void {
    this.subscriptions.sink = this.route.params.subscribe((res: any) => {
      this.rrId = res.id;
      this.getEOLinkages(this.rrId);
    });

    // To refresh tasks when we link from fly panel.
    this.subscriptions.sink =
      this.dataBroadCastService.updateProcEOLink.subscribe((res: any) => {
        this.DataSource = new MatTableDataSource<any>();
        this.getEOLinkages(this.rrId);
      });
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
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

  async getEOLinkages(id: any) {
    let tempSrc: any[] = [];

    this.srcList = tempSrc;
   // this.DataSource = new MatTableDataSource(tempSrc);

    this.linkedIds = [];
    await this.rrService
      .getLinkedEnablingObjectives(this.rrId)
      .then((res: RegulatoryRequirementWithLinkCount[]) => {
        res.forEach((data: RegulatoryRequirementWithLinkCount) => {
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
        this.DataSource = new MatTableDataSource(tempSrc);
        this.DataSource.paginator = this.tblPaging;

        if(tempSrc.length > 0){
          this.regulationDeleteCheck.emit(true);
        }
        else{
          this.regulationDeleteCheck.emit(false);
        }
       // this.DataSource.sort = this.tblSort;
      })
      .catch((err: any) => {
        this.alert.errorToast('Error Fetching linked EOs ');
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
    
    this.unlinkDescription = 'You are selecting to unlink the following ' + await this.transformTitle('Enabling Objective') + 's \n';
    this.eoId = [];
    if (id) {
      this.eoId.push(id);
      this.unlinkIds = this.unlinkIds.filter((myId:any)=>{
        return myId !== id;
      });
      this.unlinkDescription +=
      this.srcList.find((x) => x.id == id).number + ' - ' + this.srcList.find((x) => x.id == id).description + "\n";
    } else {
      this.unlinkIds.forEach((d:any,idx:number) => {
        this.eoId.push(d);
        this.unlinkDescription +=
        this.srcList.find((x) => x.id == d).number + ' - '  +this.srcList.find((x) => x.id == d).description + "\n";
      });
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

  async getData(e: any) {
    

    if (this.eoId.length > 0) {
      var data = JSON.parse(e);
      var options = new RR_EO_LinkOptions();
      options.regulatoryRequirementId = this.rrId;
      options.EOIds = this.eoId;
      options.changeNotes = data['reason'];
      options.effectiveDate = data['effectiveDate'];
      await this.rrService
        .UnlinkEOs(this.rrId, options)
        .then(async (res: any) => {
          this.selection.clear();
          //this.saveHistory(e);
          this.getEOLinkages(this.rrId);
          this.unlinkIds = [];
          this.alert.successToast(
            'Successfully Unlinked ' + await this.transformTitle('Enabling Objective') + '(s) from ' + await this.labelPipe.transform('Regulatory Requirement') + 's'
          );
        })
        .catch(async (err: any) => {
          this.alert.errorToast('Error Unlinking '+ await this.transformTitle('Task') + err);
        });
    }
  }

  // async saveHistory(e: any) {
  //   var options = new Procedure_StatusHistoryCreateOptions();
  //
  //   
  //   var data = JSON.parse(e);
  //   options.changeEffectiveDate = data['effectiveDate'];
  //   options.changeNotes = data['reason'];
  //   options.oldStatus = true;
  //   options.newStatus = false;
  //   options.procedureIds.push(this.procId);
  //   await this.procService
  //     .saveStatusHistory(options)
  //     .then((res: Procedure_StatusHistory) => {
  //       this.alert.successAlert(
  //         'Enabling Objective Unlinked And History Saved'
  //       );
  //     })
  //     .catch((err: any) => {
  //       this.alert.errorToast('Error Saving Procedure Status History ' + err);
  //     });
  // }

  openProcedureLinkedViewFlyPanel(templateRef: any, data: any) {
    
    this.title = data.description;
    this.EOIdToShow = data.id;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flypanelSrvc.open(portal);
  }

  openRRLinkedViewFlyPanel(templateRef: any, data: any) {
    
    this.title = data.description;
    this.EOIdToShow = data.id;
    this.eoNumber = data.number;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flypanelSrvc.open(portal);
  }

  refreshLinkData() {
    this.getEOLinkages(this.rrId);
  }

}
