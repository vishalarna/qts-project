import { SelectionModel } from '@angular/cdk/collections';
import { TemplatePortal } from '@angular/cdk/portal';
import { Component, Input, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { ActivatedRoute, Router } from '@angular/router';
import { EO_LinkOptions } from 'src/app/_DtoModels/EnablingObjective/EO_LinkOptions';
import { Position } from 'src/app/_DtoModels/Position/Position';
import { TaskPositionWithCount } from 'src/app/_DtoModels/Task/TaskPositionWithCount';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { EnablingObjectivesService } from 'src/app/_Services/QTD/enabling-objectives.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-eo-positions',
  templateUrl: './eo-positions.component.html',
  styleUrls: ['./eo-positions.component.scss']
})
export class EoPositionsComponent implements OnInit {
  @Input() isActive = false;

  displayColumns: string[] = ['id','number', 'description', 'linkageCount'];
  DataSource: MatTableDataSource<any>;
  selection = new SelectionModel<any>(true, []);
  unlinkIds: any[] = [];
  unlinkDescription = '';
  srcList: TaskPositionWithCount[] = [];
  subscription = new SubSink();
  eoId = '';
  linkedPosIds: any[] = [];
  unlinkId: any;
  posId: any;
  Title: string;
  unlinkPosIds : any[] = [];
  spinner = false;

  selectedId = "";
  selectedTitle = "";
  selectedNumber = "";

  @ViewChild(MatSort) set tblSort(sort: MatSort) {
    if (sort) this.DataSource.sort = sort;
  }
  @ViewChild(MatPaginator) set tblPaging(paginator: MatPaginator) {
    if (paginator) this.DataSource.paginator = paginator;
  }
  constructor(
    public dialog: MatDialog,
    private activatedRoute: ActivatedRoute,
    private alert: SweetAlertService,
    public dataBroadcastService: DataBroadcastService,
    public flyPanelService: FlyInPanelService,
    public vcf: ViewContainerRef,
    private route: Router,
    private eoService : EnablingObjectivesService,
    private labelPipe: LabelReplacementPipe,
  ) { }

  ngOnInit(): void {
    this.subscription.sink = this.activatedRoute.params.subscribe((res) => {
      this.eoId = String(res.id).split('-')[1];
      this.getPositions();
    });
  }

  async getPositions(){
    this.selection.clear();
    this.unlinkIds = [];
    this.srcList = await this.eoService.getlinkedPositions(this.eoId);
    this.DataSource = new MatTableDataSource(this.srcList);
    this.linkedPosIds = this.srcList.map((data)=>{
      return data.position.id;
    })
    
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
      this.unlinkIds.push(v.position.id);
    });
  }

  selectrow(row: any) {
    this.selection.toggle(row);
    this.unlinkIds = [];
    this.selection.selected.forEach((v, i) => {
      this.unlinkIds.push(v.position.id);
    });
  }

  unlinkItemsModal(templateRef: any, id?: any) {
    this.unlinkDescription = 'You are selecting to unlink \n';
    this.unlinkPosIds = [];
    if (id) {
      this.unlinkDescription +=
        '1 - ' + this.srcList.find((x) => x.position.id == id)?.position.positionTitle;
      this.unlinkId = id;
      this.unlinkPosIds.push(id);
    } else {
      this.unlinkIds.forEach((d,i) => {
        this.unlinkPosIds.push(d);
        this.unlinkDescription +=
          `${(i+1)} - ` + this.srcList.find((x) => x.position.id == d)?.position.positionTitle + ' \n';
      });
    }
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }
  
  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }

  async getData(e:any){
    this.spinner = true;
    var options = new EO_LinkOptions();
    options.positionIds = this.unlinkPosIds;
    options.eoId = this.eoId;
    var data = JSON.parse(e);
    options.changeNotes = data['reason'];
    options.effectiveDate = data['effectiveDate'];
    await this.eoService.unlinkPositions(options).then(async (_)=>{
      this.alert.successToast("Selected "+ await this.transformTitle('Position') +"s unlinked from SQ");
      this.getPositions();
    }).finally(()=>{
      this.spinner = false;
    });
  }

  openFlyPanel(templateRef:any){
    const portal = new TemplatePortal(templateRef,this.vcf);
    this.flyPanelService.open(portal);
  }

}
