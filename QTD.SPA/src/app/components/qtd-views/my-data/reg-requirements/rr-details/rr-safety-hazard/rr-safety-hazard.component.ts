import { SelectionModel } from '@angular/cdk/collections';
import { TemplatePortal } from '@angular/cdk/portal';
import { AfterViewInit, Component, EventEmitter, Input ,OnDestroy, OnInit, Output, ViewChild, ViewContainerRef } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { ActivatedRoute } from '@angular/router';
import { RR_SaftyHazard_LinkOptions } from 'src/app/_DtoModels/RR_SaftyHazard_Link/RR_SaftyHazard_LinkOptions';
import { SafetyHazardWithLinkCount } from 'src/app/_DtoModels/SaftyHazard/SafetyHazardWithLinkCount';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { RegulatoryRequirementService } from 'src/app/_Services/QTD/regulatory-requirement.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-rr-safety-hazard',
  templateUrl: './rr-safety-hazard.component.html',
  styleUrls: ['./rr-safety-hazard.component.scss'],
})
export class RRSafetyHazardComponent implements OnInit, AfterViewInit, OnDestroy {
  @Output() regulationDeleteCheck  = new EventEmitter<any>();
  @Input() issuingAuthorityCheck:any;
  @Input() regTitle:any;
  displayColumns: string[] = ['id', 'number', 'description', 'linkageCount'];
  DataSource: MatTableDataSource<any>;
  selection = new SelectionModel<any>(true, []);
  unlinkIds: any[] = [];
  linkedIds: any[] = [];
  subscription = new SubSink();
  shToUnlink:any[] = [];
  selectedShId = "";
  shTitle = ""
  shNumber:any;

  rrId = "";

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
    private alert: SweetAlertService,
    private dataBroadcastService: DataBroadcastService,
    private rrService: RegulatoryRequirementService,
    private route: ActivatedRoute,
    private labelPipe:LabelReplacementPipe
  ) { }

  ngOnInit(): void {
  }

  ngAfterViewInit(): void {
    this.subscription.sink = this.route.params.subscribe((res: any) => {
      this.rrId = res.id;
      this.getSHLinkages();
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

  getSHLinkages() {
    let tempSrc: any[] = [];
    this.linkedIds = [];
    this.rrService.getLinkedSH(this.rrId).then((res: SafetyHazardWithLinkCount[]) => {
      res.forEach((sh: SafetyHazardWithLinkCount) => {
        this.linkedIds.push(sh.id);
        tempSrc.push({ number: sh.number, description: sh.title, id: sh.id, linkageCount: sh.linkCount, active:sh.active });
       // this.shNumber = sh.number;
      });
      Object.assign(this.srcList, tempSrc);
      this.DataSource = new MatTableDataSource(tempSrc);
      this.DataSource.paginator = this.tblPaging;
    })


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
    this.unlinkDescription = `You are selecting to unlink the following ${await this.labelPipe.transform("Safety Hazard") }s\n`;
    this.shToUnlink = [];
    if (id) {
      this.shToUnlink.push(id);
      this.unlinkDescription +=
      this.srcList.find((x) => x.id == id).number + ' - ' + this.srcList.find((x) => x.id == id).description + ' \n';
    } else {
      this.unlinkIds.forEach((d,index) => {
        this.shToUnlink.push(d);
        this.unlinkDescription +=
        this.srcList.find((x) => x.id == d).number + ' - ' + this.srcList.find((x) => x.id == d).description + ' \n';
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

  getData(e: any) {
    
    if(this.shToUnlink.length > 0){
      var data = JSON.parse(e);
      var options = new RR_SaftyHazard_LinkOptions();
      options.changeNotes = data['reason'];
      options.effectiveDate = data['effectiveDate'];
      options.regulatoryRequirementId = this.rrId;
      options.safetyHazardIds = this.shToUnlink;
      this.rrService.unlinkSH(this.rrId,options).then(async (res:any)=>{
        this.alert.successToast(`${await this.labelPipe.transform("Safety Hazard") }s unlinked from ` + await this.labelPipe.transform('Regulatory Requirement'));
        this.refreshSHLinks();
      })
    }
  }

  refreshSHLinks(){
    this.selection.clear();
    this.unlinkIds = [];
    this.srcList = [];
    this.getSHLinkages();
  }

  openRRLinkedSHFlyPanel(templateRef:any,row:any){
    this.shTitle = row.description;
    this.shNumber = row.number;
    this.selectedShId = row.id;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flypanelSrvc.open(portal);
  }
}
