import { IlaDetailsComponent } from './../../../../design-and-development/providers-and-ila/ila-create-wizard/ila-wizard-components/ila-details/ila-details.component';
import { SelectionModel } from '@angular/cdk/collections';
import { TemplatePortal } from '@angular/cdk/portal';
import { AfterViewInit, Component, EventEmitter, Input, OnDestroy, OnInit, Output, ViewChild, ViewContainerRef } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { ActivatedRoute } from '@angular/router';
import { RegulatoryRequirementWithLinkCount } from 'src/app/_DtoModels/RegulatoryRequirements/RegulatoryRequirementWithLinkCount';
import { SaftyHazard_RR_LinkOptions } from 'src/app/_DtoModels/SaftyHazard_RR_Link/SaftyHazard_RR_LinkOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { SafetyHazardsService } from 'src/app/_Services/QTD/safety-hazards.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-sh-rr',
  templateUrl: './sh-rr.component.html',
  styleUrls: ['./sh-rr.component.scss'],
})
export class ShRrComponent implements OnInit, AfterViewInit, OnDestroy {
  displayColumns: string[] = ['id', 'number', 'description', 'linkageCount'];
  DataSource: MatTableDataSource<any>;
  @Output() taskDeleteCheck  = new EventEmitter<any>();
  selection = new SelectionModel<any>(true, []);
  unlinkIds: any[] = [];
  shId = "";
  rrIds :any[] = [];
  subscription = new SubSink();
  alreadyLinked: any[] = [];
  unlinkSpinner = false;
  selectedRRId:any;
  rrTitle:any;
  rrNumber:any;
  @Input() shTitle:any;

  unlinkDescription = '';
  srcList: any[] = [];
  @ViewChild(MatSort) sort : MatSort/*(sort: MatSort)  {
    if (sort) this.DataSource.sort = sort;
  } */
  @ViewChild(MatPaginator) set tblPaging(paginator: MatPaginator) {
    if (paginator) this.DataSource.paginator = paginator;
  }

  @Input() isActive = true;

  constructor(
    public flypanelSrvc: FlyInPanelService,
    private vcf: ViewContainerRef,
    public dialog: MatDialog,
    private route: ActivatedRoute,
    private shService: SafetyHazardsService,
    private alert : SweetAlertService,
    private labelPipe:LabelReplacementPipe
  ) { }

  ngOnInit(): void {

  }

  ngAfterViewInit(): void {
    this.subscription.sink = this.route.params.subscribe((res: any) => {
      this.shId = res.id;
      this.getRRLinkages();
    })
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
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
  masterToggle() {
    this.isAllSelected()
      ? this.selection.clear()
      : this.DataSource.data.forEach((row) => this.selection.select(row));

    this.unlinkIds = [];
    this.selection.selected.forEach((v, i) => {
      this.unlinkIds.push(v.id);
    });
  }

  getRRLinkages() {
    let tempSrc: any[] = [];
    this.alreadyLinked =[];
    this.shService.getLinkedRR(this.shId).then((res: RegulatoryRequirementWithLinkCount[]) => {
      
      res.forEach((data: RegulatoryRequirementWithLinkCount) => {
        tempSrc.push({ id: data.id, description: data.description, linkageCount: data.linkCount, number: data.number,active:data.active });
        this.alreadyLinked.push(data.id);
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

  openFlypanelSHWithRR(templateRef: any,row:any) {
    this.selectedRRId = row.id;
    this.rrTitle = row.description;
    this.rrNumber = row.number;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flypanelSrvc.open(portal);
  }

  async unlinkItemsModal(templateRef: any, id?: any) {
    this.unlinkDescription = `You are selecting to unlink the following ` + await this.labelPipe.transform('Regulatory Requirement') +`s \n`;
    this.rrIds = [];
    if (id) {
      this.rrIds.push(id);
      this.unlinkDescription +=
        this.srcList.find((x) => x.id == id).number + ' - ' + this.srcList.find((x) => x.id == id).description + ' \n';
    } else {
      this.unlinkIds.forEach((d,i) => {
        this.rrIds.push(d);
        this.unlinkDescription +=
        this.srcList.find((x) => x.id == d).number + ' - ' +this.srcList.find((x) => x.id == d).description + ' \n';
      });
    
      //this.unlinkDescription = this.removeLastWord(this.unlinkDescription);
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

  getData(e: any) {
    this.unlinkSpinner = true;
    var options = new SaftyHazard_RR_LinkOptions();
    options.regulatoryRequirementIds = this.rrIds;
    options.safetyHazardId = this.shId;
    var data = JSON.parse(e);
    options.effectiveDate = data['effectiveDate'];
    options.changeNotes = data['reason'];
    this.shService.unlinkRR(this.shId,options).then((res:any)=>{
      this.alert.successToast("Selected Regulations Successfully Unlinked");
      this.refreshData();
    }).finally(()=>{
      this.unlinkSpinner = false;
    });
  }

  refreshData(){
    this.selection.clear();
    this.unlinkIds = [];
    this.getRRLinkages();
  }
}
