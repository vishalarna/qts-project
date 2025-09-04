import { SelectionModel } from '@angular/cdk/collections';
import { TemplatePortal } from '@angular/cdk/portal';
import { AfterViewInit, Component, EventEmitter, OnInit, Output,Input, ViewChild, ViewContainerRef } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { ActivatedRoute } from '@angular/router';
import { ILAWithCountOptions } from 'src/app/_DtoModels/ILA/ILAWithCountOptions';
import { RR_ILA_LinkOptions } from 'src/app/_DtoModels/RR_ILA/RR_ILA_LinkOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { RegulatoryRequirementService } from 'src/app/_Services/QTD/regulatory-requirement.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-rr-ila',
  templateUrl: './rr-ila.component.html',
  styleUrls: ['./rr-ila.component.scss'],
})
export class RRIlaComponent implements OnInit, AfterViewInit {
  @Output() regulationDeleteCheck  = new EventEmitter<any>();
  @Input() issuingAuthorityCheck:any;
  @Input() regTitle:any
  displayColumns: string[] = ['id', 'number', 'description', 'linkageCount'];
  DataSource: MatTableDataSource<any>;
  selection = new SelectionModel<any>(true, []);
  unlinkIds: any[] = [];
  unlinkDescription = '';
  subscription = new SubSink();
  srcList: any[] = [];
  linkedIds: any[] = [];
  iLAToUnlink:any[] = [];
  title = '';
  ILAIdToShow = '';
  ilaNumber : any;
  @ViewChild(MatSort) sort : MatSort/*(sort: MatSort)  {
    if (sort) this.DataSource.sort = sort;
  } */
  @ViewChild(MatPaginator) set tblPaging(paginator: MatPaginator) {
    if (paginator) this.DataSource.paginator = paginator;
  }
  rrId = "";

  constructor(
    public flypanelSrvc: FlyInPanelService,
    private vcf: ViewContainerRef,
    public dialog: MatDialog,
    private rrService: RegulatoryRequirementService,
    private route: ActivatedRoute,
    private alert: SweetAlertService,
    private labelPipe: LabelReplacementPipe
  ) {}

  ngOnInit(): void {
  }

  ngAfterViewInit(): void {
    this.subscription.sink = this.route.params.subscribe((res: any) => {
      this.rrId = res.id;
      this.getILALinkages();
    })
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

  async getILALinkages() {
    let tempSrc: any[] = [];
    this.linkedIds = [];
    await this.rrService
      .getLinkedILA(this.rrId)
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
        this.DataSource = new MatTableDataSource(tempSrc);
        this.DataSource.paginator = this.tblPaging;

        if(tempSrc.length > 0){
          this.regulationDeleteCheck.emit(true);
        }
        else{
          this.regulationDeleteCheck.emit(false);
        }
        //this.DataSource.sort = this.tblSort;
      })
      .catch(async (err: any) => {
        this.alert.errorToast('Error Fetching linked ' + await this.labelPipe.transform('ILA') + 's');
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

  async getData(e: any) {
    
    if (this.iLAToUnlink.length > 0) {
      var data = JSON.parse(e);
      var options = new RR_ILA_LinkOptions();
      options.regRequirementId = this.rrId;
      options.ilaIds = this.iLAToUnlink;
      options.changeNotes = data['reason'];
      options.effectiveDate = data['effectiveDate'];
      await this.rrService
        .unlinkILA(this.rrId, options)
        .then(async (res: any) => {
          this.selection.clear();
          //this.saveHistory(e);
          this.getILALinkages();
          this.unlinkIds = [];
          this.alert.successToast(
            'Successfully Unlinked ' + await this.labelPipe.transform('ILA') + '(s) from ' + await this.labelPipe.transform('Regulatory Requirement')
          );
        })
        .catch(async (err: any) => {
          this.alert.errorToast('Error Unlinking ' + await this.labelPipe.transform('ILA'));
        });
    }
  }

  async unlinkItemsModal(templateRef: any, id?: any) {
    this.unlinkDescription = 'You are selecting to unlink the following ' + await this.labelPipe.transform('ILA') +' \n';
    this.iLAToUnlink = [];
    if (id) {
      this.iLAToUnlink.push(id);
      this.unlinkDescription +=
      this.srcList.find((x) => x.id == id).number + ' - ' + this.srcList.find((x) => x.id == id).description  + ' \n';
    } else {
      this.unlinkIds.forEach((d,index) => {
        this.iLAToUnlink.push(d);
        this.unlinkDescription +=
        this.srcList.find((x) => x.id == d).number + ' - ' + this.srcList.find((x) => x.id == d).description + ' \n';
      });
      //this.unlinkDescription = this.removeLastWord(this.unlinkDescription);
    }
    this.unlinkDescription += ' \n' + 'from Regulation ' + this.regTitle;
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

  refreshILALinks(){
    this.selection.clear();
    this.unlinkIds = [];
    this.srcList = [];
    this.getILALinkages();
  }

  openRRLinkedViewFlyPanel(templateRef: any, data: any) {
    this.title = data.description;
    this.ILAIdToShow = data.id;
    this.ilaNumber = data.number;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flypanelSrvc.open(portal);
  }
}
