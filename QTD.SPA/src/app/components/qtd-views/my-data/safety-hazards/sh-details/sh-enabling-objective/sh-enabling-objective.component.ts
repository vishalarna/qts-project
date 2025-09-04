import { SelectionModel } from '@angular/cdk/collections';
import { TemplatePortal } from '@angular/cdk/portal';
import { Component, OnInit, ViewChild, ViewContainerRef,Input, EventEmitter, Output } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { ActivatedRoute } from '@angular/router';
import { EOWithCountOptions } from 'src/app/_DtoModels/EnablingObjective/EOWithCountOptions';
import { SH_EO_LinkOptions } from 'src/app/_DtoModels/SH_EO_Link/SH_EO_LinkOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { SafetyHazardsService } from 'src/app/_Services/QTD/safety-hazards.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-sh-enabling-objective',
  templateUrl: './sh-enabling-objective.component.html',
  styleUrls: ['./sh-enabling-objective.component.scss']
})
export class ShEnablingObjectiveComponent implements OnInit {
  @Input() isActive : boolean = true;
  displayColumns: string[] = ['id', 'number', 'description', 'linkageCount'];
  DataSource: MatTableDataSource<any>;
  @Output() taskDeleteCheck  = new EventEmitter<any>();
  selection = new SelectionModel<any>(true, []);
  unlinkIds: any[] = [];
  linkedIds: any[] = [];
  eoIds :any[] = [];
  eoNumber:any;
  @Input() shTitle:any;

  unlinkDescription = '';
  srcList: any[] = [];
  unlinkSpinner = false;

  @ViewChild(MatSort) sort : MatSort/*(sort: MatSort)  {
    if (sort) this.DataSource.sort = sort;
  } */
  @ViewChild(MatPaginator) set tblPaging(paginator: MatPaginator) {
    if (paginator) this.DataSource.paginator = paginator;
  }
  subscription = new SubSink();
  shId = "";
  eoTitle = "";
  selectedEOId = "";

  constructor(
    public flypanelSrvc: FlyInPanelService,
    private vcf: ViewContainerRef,
    public dialog: MatDialog,
    private route: ActivatedRoute,
    private shService: SafetyHazardsService,
    private alert:SweetAlertService,
    private labelPipe:LabelReplacementPipe
  ) {}

  ngOnInit(): void {
    
  }

  ngAfterViewInit(): void {
    
    this.subscription.sink = this.route.params.subscribe((res: any) => {
      this.shId = res.id;
      this.getEOLinkages(this.shId);
    });
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
  /*   this.DataSource = new MatTableDataSource(tempSrc);
    this.DataSource.paginator = this.tblPaging;
 */
    this.linkedIds = [];
    await this.shService
      .getLinkedEOs(this.shId)
      .then((res: EOWithCountOptions[]) => {
        res.forEach((data: EOWithCountOptions) => {
          this.linkedIds.push(data.id);
          tempSrc.push({
            number: data.number,
            description: data["title"],
            linkageCount: data.linkCount,
            id: data.id,
            active: data.active,
          });
        });

        this.srcList = tempSrc;
        if(res.length > 0){
          this.taskDeleteCheck.emit(true);
        }
        else if(res.length == 0){
          this.taskDeleteCheck.emit(false);
        }
        this.DataSource = new MatTableDataSource(tempSrc);
        //this.DataSource.sort = this.tblSort;
        this.DataSource.paginator = this.tblPaging;
      })
      .catch((err: any) => {
        this.alert.errorToast('Error Fetching linked EOs ');
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
    this.eoIds =[];
    if (id) {
      this.eoIds.push(id);
      this.unlinkDescription +=
      this.srcList.find((x) => x.id == id).number + ' - ' + this.srcList.find((x) => x.id == id).description  + '\n';
    } else {
      this.unlinkIds.forEach((d,i) => {
        this.eoIds.push(d);

        this.unlinkDescription +=
        this.srcList.find((x) => x.id == d).number + ' - ' + this.srcList.find((x) => x.id === d).description  + '\n';
      });
      // this.unlinkDescription = this.removeLastWord(this.unlinkDescription);
    }
    this.unlinkDescription += ' \n' + 'from  '+await this.labelPipe.transform("Safety Hazard") +' '+ this.shTitle;
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
    var data = JSON.parse(e);
    var options = new SH_EO_LinkOptions();
    options.eOIds = this.eoIds;
    options.safetyHazardId = this.shId;
    options.changeNotes = data['reason'];
    options.effectiveDate = data['effectiveDate'];
    this.shService.unlinkEOs(this.shId,options).then(async (res:any)=>{
      this.alert.successToast("Selected " + await this.transformTitle('Enabling Objective') + "(s) " + " Unlinked From " + await this.transformTitle('Safety Hazard'));
      this.unlinkIds = [];
      this.eoIds = [];
      this.selection.clear();
      this.getEOLinkages(this.shId);
    }).finally(()=>{
      this.unlinkSpinner = false;
    })
  }

  refreshData(){
    this.selection.clear();
    this.unlinkIds = [];
    this.getEOLinkages(this.shId);
  }

  openLinkedToSHFlyPanel(templateRef:any, row:any){

    this.selectedEOId = row.id;
    this.eoTitle = row.description;
    this.eoNumber = row.number;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flypanelSrvc.open(portal);
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }
}
