import { SelectionModel } from '@angular/cdk/collections';
import { TemplatePortal } from '@angular/cdk/portal';
import { AfterViewInit, Component, EventEmitter, Input, OnDestroy, OnInit, Output, ViewChild, ViewContainerRef } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { ActivatedRoute } from '@angular/router';
import { ILAWithCountOptions } from 'src/app/_DtoModels/ILA/ILAWithCountOptions';
import { SH_ILA_LinkOptions } from 'src/app/_DtoModels/SH_ILA_Link/SH_ILA_LinkOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { SafetyHazardsService } from 'src/app/_Services/QTD/safety-hazards.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-sh-ila',
  templateUrl: './sh-ila.component.html',
  styleUrls: ['./sh-ila.component.scss'],
})
export class ShIlaComponent implements OnInit, AfterViewInit, OnDestroy {
  displayColumns: string[] = ['id', 'number', 'description', 'linkageCount'];
  DataSource: MatTableDataSource<any>;
  selection = new SelectionModel<any>(true, []);
  @Output() taskDeleteCheck  = new EventEmitter<any>();
  unlinkIds: any[] = [];
  unlinkDescription = '';
  srcList: any[] = [];
  ilaNumber:any;
  
  @ViewChild(MatSort) sort : MatSort/*(sort: MatSort)  {
    if (sort) this.DataSource.sort = sort;
  } */
  @ViewChild(MatPaginator) set tblPaging(paginator: MatPaginator) {
    if (paginator) this.DataSource.paginator = paginator;
  }
  @Input() isActive : boolean = true;
  @Input() shTitles : any;
  subscription = new SubSink();
  shId = "";
  linkedIds: any[] = [];
  iLAIds: any[] = [];
  unlinkSpinner = false;
  title = '';
  shTitle = '';
  ILAIdToShow = '';

  constructor(
    public flypanelSrvc: FlyInPanelService,
    private vcf: ViewContainerRef,
    public dialog: MatDialog,
    private route: ActivatedRoute,
    private shService: SafetyHazardsService,
    private alert: SweetAlertService,
    private labelPipe:LabelReplacementPipe
  ) {}

  ngOnInit(): void {
  }

  ngAfterViewInit(): void {
    this.subscription.sink = this.route.params.subscribe((res: any) => {
      this.shId = res.id;
      this.getILALinkages();
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
    
    await this.shService
      .getLinkedILA(this.shId)
      .then((res: ILAWithCountOptions[]) => {
        
        res.forEach((data: ILAWithCountOptions) => {
          this.linkedIds.push(data.id);
          tempSrc.push({
            number: data.number,
            description: data["title"],
            linkageCount: data.linkCount,
            id: data.id,
            active: data.active,
          });
          this.shTitle = data["title"];
        });

        this.srcList = tempSrc;
        this.DataSource = new MatTableDataSource(tempSrc);
        //  this.DataSource.sort = this.tblSort;
          this.DataSource.paginator = this.tblPaging;
        if(res.length > 0){
          this.taskDeleteCheck.emit(true);
        }
        else if(res.length == 0){
          this.taskDeleteCheck.emit(false);
        }
      
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

  getData(e: any) {
    this.unlinkSpinner = true;
    var data = JSON.parse(e);
    var options = new SH_ILA_LinkOptions();
    options.safetyHazardId = this.shId;
    options.iLAIds = this.iLAIds;
    options.changeNotes = data['reason'];
    options.effectiveDate = data['effectiveDate'];
    this.shService.unlinkILA(this.shId, options).then(async (res: any) => {
      this.alert.successToast(`Selected ` + await this.labelPipe.transform('ILA') +`(s) Unlinked From ${await this.labelPipe.transform("Safety Hazard")}`);
      this.unlinkIds = [];
      this.iLAIds = [];
      this.selection.clear();
      this.getILALinkages();
    }).finally(() => {
      this.unlinkSpinner = false;
    })
  }

  async unlinkItemsModal(templateRef: any, id?: any) {
    this.unlinkDescription = 'You are selecting to unlink the following ' + await this.labelPipe.transform('ILA') + 's \n';
    this.iLAIds = [];
    if (id) {
      this.iLAIds.push(id);
      this.unlinkDescription +=
      this.srcList.find((x) => x.id == id).number + ' - ' + this.srcList.find((x) => x.id == id).description  + '\n';
    } else {
      this.unlinkIds.forEach((d, i) => {
        this.iLAIds.push(d);
        this.unlinkDescription +=
        this.srcList.find((x) => x.id === d).number + ' - ' + this.srcList.find((x) => x.id === d).description + '\n';
      });
      // this.unlinkDescription = this.removeLastWord(this.unlinkDescription);
    }
    this.unlinkDescription += ' \n' + 'from '+await this.labelPipe.transform("Safety Hazard") +' ' +this.shTitles;
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

  refreshData() {
    this.selection.clear();
    this.unlinkIds = [];
    this.getILALinkages();
  }

  openSHLinkedViewFlyPanel(templateRef: any, data: any) {
    
    this.title = data.description;
    this.ILAIdToShow = data.id;
    this.ilaNumber = data.number;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flypanelSrvc.open(portal);
  }
}
