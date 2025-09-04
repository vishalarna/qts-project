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
import { Procedure_RegulatoryRequirement_LinkOptions } from 'src/app/_DtoModels/Procedure_RegulatoryRequirement_Link/Procedure_RegulatoryRequirement_LinkOptions';
import { RegulatoryRequirement } from 'src/app/_DtoModels/RegulatoryRequirements/RegulatoryRequirement';
import { RegulatoryRequirementWithLinkCount } from 'src/app/_DtoModels/RegulatoryRequirements/RegulatoryRequirementWithLinkCount';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { ProceduresService } from 'src/app/_Services/QTD/procedures.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-procedure-reg-requirement',
  templateUrl: './procedure-reg-requirement.component.html',
  styleUrls: ['./procedure-reg-requirement.component.scss'],
})
export class ProcedureRegRequirementComponent
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
  subscription = new SubSink();
  rrIds: any[] = [];
  unlinkDescription = '';
  srcList: any[] = [];
  unlinkSpinner = false;
  title = '';
  rrId = '';
  linkedIds: any[] = [];
  regNumber:any

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
    public procService: ProceduresService,
    public route: ActivatedRoute,
    public alert: SweetAlertService,
    private dataBroadcastService: DataBroadcastService,
    private labelPipe: LabelReplacementPipe,
  ) {}

  ngOnInit(): void {}

  ngAfterViewInit(): void {
    this.subscription.sink = this.route.params.subscribe((res: any) => {
      this.procId = res.id;
      this.getRRLinkages();
    });

    this.subscription.sink =
    this.dataBroadcastService.updateProcRRLink.subscribe((res: any) => {
      this.getRRLinkages();
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
  masterToggle() {
    this.isAllSelected()
      ? this.selection.clear()
      : this.DataSource.data.forEach((row) => this.selection.select(row));

    this.unlinkIds = [];
    this.selection.selected.forEach((v, i) => {
      this.unlinkIds.push(v.id);
    });
  }

  async getRRLinkages() {
    let tempSrc: any[] = [];
    this.linkedIds = [];
    await this.procService
      .getLinkedRR(this.procId)
      .then((res: RegulatoryRequirementWithLinkCount[]) => {
        res.forEach((item: RegulatoryRequirementWithLinkCount) => {
          this.linkedIds.push(item.id);
          tempSrc.push({
            number: item.number,
            description: item.description,
            linkageCount: item.linkCount,
            id: item.id,
            active: item.active,
          });
        });
      })
      .catch(async (err: any) => {
        this.alert.errorToast(
          'Error Getting Linked ' + await this.labelPipe.transform('Regulatory Requirement') + 's ' + err
        );
      });
    this.srcList = tempSrc;
    tempSrc = tempSrc.sort((a, b) => (a.number < b.number ? -1 : 1));
    this.DataSource = new MatTableDataSource(tempSrc);
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

  openViewLinkedProceduresFlyPanel(templateRef: any, data: any) {
    this.title = data.description;
    this.rrId = data.id;
    this.regNumber = data.number;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flypanelSrvc.open(portal);
  }

  getData(e: any) {
    if (this.rrIds.length > 0) {
      this.unlinkSpinner = true;
      var options = new Procedure_RegulatoryRequirement_LinkOptions();
      var data = JSON.parse(e);
      options.procedureId = this.procId;
      options.regulatoryRequirementIds = this.rrIds;
      options.effectiveDate = data['effectiveDate'];
      options.changeNotes = data['reason'];
      this.procService
        .unlinkRR(this.procId, options)
        .then(async (res: any) => {
          this.selection.clear();
          this.unlinkIds = [];
          this.getRRLinkages();
          this.unlinkSpinner = false;
          this.alert.successToast(
            'Successfully Unlinked ' + await this.labelPipe.transform('Regulatory Requirement') + 's from ' + await this.transformTitle('Procedure') 
          );
        })
        .catch(async (err: any) => {
          this.unlinkSpinner = false;
          this.alert.errorToast(
            'Error Unlinking' + await this.labelPipe.transform('Regulatory Requirement') + 's ' + err
          );
        });
    }
  }

  refreshLinkedIds() {
    this.linkedIds = this.linkedIds.filter((data: any) => {
      return !this.rrId.includes(data);
    });
  }

  async unlinkItemsModal(templateRef: any, id?: any) {
    this.unlinkDescription = 'You are selecting to unlink the following ' + await this.labelPipe.transform('Regulatory Requirement') + 's \n';
    this.rrIds = [];
    if (id) {
      this.rrIds.push(id);
      this.unlinkDescription +=
      this.srcList.find((x) => x.id == id).number + ' - ' + this.srcList.find((x) => x.id == id).description  + '\n';
    } else {
      this.unlinkIds.forEach((d,i) => {
        this.rrIds.push(d);
        this.unlinkDescription +=
        this.srcList.find((x) => x.id == d).number + ' - ' + this.srcList.find((x) => x.id == d).description + '\n';
      });
      //this.unlinkDescription = this.removeLastWord(this.unlinkDescription);
    }
    this.unlinkDescription += ' \n' + 'from ' + await this.transformTitle('Procedure') + 's ' + this.procTitle;
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

  refreshLinks() {
    this.getRRLinkages();
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }
}
