import { SelectionModel } from '@angular/cdk/collections';
import { moveItemInArray } from '@angular/cdk/drag-drop';
import { TemplatePortal } from '@angular/cdk/portal';
import { AfterViewInit, Component, ElementRef, OnDestroy, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { ActivatedRoute, Router } from '@angular/router';
import { saveAs } from 'file-saver';
import { asBlob } from 'html-docx-js-typescript';
import html2canvas from 'html2canvas';
import jsPDF from 'jspdf';
import { EnablingObjective } from 'src/app/_DtoModels/EnablingObjective/EnablingObjective';
import { EnablingObjectiveOptions } from 'src/app/_DtoModels/EnablingObjective/EnablingObjectiveOption';
import { EOLatestActivityVM } from 'src/app/_DtoModels/EnablingObjective/EOLatestActivityVM';
import { EOLinkStats } from 'src/app/_DtoModels/EnablingObjective/EOLinkStats';
import { EOWithAllDataVM } from 'src/app/_DtoModels/EnablingObjective/EOWithAllDataVM';
import { EO_MetaEO_LinkOptions } from 'src/app/_DtoModels/EnablingObjective_MetaEO_Link/EO_MetaEO_LinkOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { EnablingObjectivesService } from 'src/app/_Services/QTD/enabling-objectives.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-enabling-objective-details',
  templateUrl: './enabling-objective-details.component.html',
  styleUrls: ['./enabling-objective-details.component.scss']
})
export class EnablingObjectiveDetailsComponent implements OnInit, OnDestroy, AfterViewInit {
  displayColumns: string[] = ['order', 'id', 'sequence', 'number', 'description', 'unlink']
  isActive = true;
  isReordered = false;
  isLoading = false;
  editView = false;
  eo: EnablingObjective;
  eoId = "";
  subscription = new SubSink();
  DataSource: MatTableDataSource<any>;
  unlinkIds: any[] = [];
  unlinkDescription = '';
  selection = new SelectionModel<any>(true, []);
  alreadyLinkedEOs: any[] = [];
  editLoader = false;

  unlinkEOIds: any[] = [];

  hasLinks = false;

  allEOData !: EOWithAllDataVM;

  spinner = false;
  unlinkSpinner = false;

  srcList: EnablingObjective[] = [];



  @ViewChild(MatSort) set tblSort(sort: MatSort) {
    if (sort) this.DataSource.sort = sort;
  }
  @ViewChild(MatPaginator) set tblPaging(paginator: MatPaginator) {
    if (paginator) this.DataSource.paginator = paginator;
  }

  @ViewChild('pdf') pdf: HTMLElement;


  header = "";
  description = "";
  showReason = false;
  action = "";
  makeCopy = false;
  changeCatSubTopic = false;
  eoStats: EOLinkStats;
  histStats: { modified_by: string, modified_date: any } = { modified_by: "", modified_date: Date.now() };
  cannotMakeEOInactive = true;

  constructor(
    private EOService: EnablingObjectivesService,
    private route: ActivatedRoute,
    public dataBroadcastService: DataBroadcastService,
    private router: Router,
    public dialog: MatDialog,
    private alert: SweetAlertService,
    public flyPanelService: FlyInPanelService,
    private vcf: ViewContainerRef,
    private labelPipe: LabelReplacementPipe,
  ) { }

  ngOnInit(): void {
  }

  ngAfterViewInit(): void {
    this.subscription.sink = this.route.params.subscribe(async (res: any) => {
      this.eoId = String(res.id).split('-')[1];
      if(this.eoId && this.eoId !== undefined){
        this.cannotMakeEOInactive = await this.EOService.CheckDeactivePossible(this.eoId);
        this.getEOData();
      }
      if (this.editView) {
        this.editLoader = true;
        this.getAllEOData();
      }
    });
    this.subscription.sink = this.dataBroadcastService.refreshStats.subscribe(async (_) => {
      this.getLinkStats();
      this.cannotMakeEOInactive = await this.EOService.CheckDeactivePossible(this.eoId);
    });
    this.subscription.sink = this.dataBroadcastService.refreshStats.subscribe((_) => {
      this.getHistStats();
    });
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  changeView() {
    this.editLoader = true;
    this.editView = true;
    this.getAllEOData();
  }

  async getAllEOData() {
    await this.EOService.getAllEOData(this.eoId).then((res) => {
      this.allEOData = res;
    }).finally(() => {
      this.editLoader = false;
    });
  }

  async getLinkedEOs() {
    this.selection.clear();
    this.unlinkIds = [];
    this.srcList = await this.EOService.getEOLinkedToMetaEO(this.eoId);

    this.isReordered = false;
    this.alreadyLinkedEOs = this.srcList.map((data) => {
      return data.id;
    });
    this.getLinkStats();
    this.getHistStats();
    this.DataSource = new MatTableDataSource(this.srcList);
  }

  async getHistStats() {
    await this.EOService.getLatestHistoryStats(this.eoId).then((res: EOLatestActivityVM[]) => {


      this.histStats.modified_by = res[0]?.modifiedBy !== null ? res[0]?.modifiedBy : (res[0]?.createdBy ?? "NO CREATOR");
      //this.histStats.modified_date = res[0].modifiedDate !== null ? res[0].modifiedDate : res[0].createdDate;
      this.histStats.modified_date = this.eo.effectiveDate;
    }).finally(() => {

    })
  }

  async getLinkStats() {
    this.eoStats = this.eo.isMetaEO ? await this.EOService.getMetaEOStats(this.eoId)
      : await this.EOService.getStats(this.eoId);
    var count = 0;
    count = Object.values(this.eoStats).reduce((a, b) => a + b, 0);
    this.hasLinks = this.eo.isMetaEO ? (count + this.DataSource.data.length) > 0 : count > 0;
  }

  async getEOData() {
    this.eo = await this.EOService.getWithCatSubCatAndtopic(this.eoId);
    this.isActive = (this.eo.enablingObjective_Category.active && this.eo.enablingObjective_SubCategory.active && (this.eo.enablingObjective_Topic?.active ?? true) && this.eo.active);

    if (this.eo.isMetaEO) {
      this.getLinkedEOs()
    }
    else {
      // this.getLinkStats();
      // this.getHistStats();
    }
    this.getLinkStats();
    this.getHistStats();

  }

  async deleteModal(templateRef: any) {
    this.header = "Delete " + await this.transformTitle('Enabling Objective');
    this.description = `You are selecting to delete ` + await this.transformTitle('Enabling Objective') + `, "${this.eo.number} - ${this.eo.description}".`;
    this.showReason = false;
    this.action = "Delete"
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  async activeModal(templateRef: any, makeActive: boolean) {
    this.header = `Make ${makeActive ? "Active" : "Inactive"}`;
    this.action = `${makeActive ? "Active" : "Inactive"}`;
    this.showReason = true;
    this.description = `You are selecting to make ` + await this.transformTitle('Enabling Objective') + ` "${this.eo.number} ${this.eo.description}" ${makeActive ? "Active" : "Inactive"}.`
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  async getDataWithReason(e: any) {
    var data = JSON.parse(e);
    var options = new EnablingObjectiveOptions();
    options.actionType = this.action;
    options.changeNotes = data['reason'];
    options.effectiveDate = data['effectiveDate'];
    options.eoIds = [];
    options.eoIds.push(this.eoId);
    await this.EOService.delete(this.eoId, options).then(async (_) => {
      this.alert.successToast(await this.transformTitle('Enabling Objective') + ` Successfully Made ${this.action}`);
      if(this.action === "Delete"){
        this.router.navigate(['my-data/enabling-objectives/overview']);
      }
      else{
        this.getEOData();
      }
      this.dataBroadcastService.updateMyDataNavBar.next(null);
    }).finally(() => {
    });
  }

  async getData() {
    var options = new EnablingObjectiveOptions();
    options.actionType = "delete";
    options.eoIds = [];
    options.eoIds.push(this.eoId);
    await this.EOService.delete(this.eoId, options).then(async (_) => {
      this.alert.successToast(await this.transformTitle('Enabling Objective') + " Deleted");
      this.dataBroadcastService.updateMyDataNavBar.next(null);
      this.router.navigate(['my-data/enabling-objectives/overview']);
    }).finally(() => {

    });
  }

  openEditCopyEO(templateRef: any, copy: boolean, change: boolean) {
    this.makeCopy = copy;
    this.changeCatSubTopic = change;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  masterToggle() {
    this.isAllSelected()
      ? this.selection.clear()
      : this.DataSource.data.forEach((row) => this.selection.select(row));

    this.unlinkIds = [];
    this.selection.selected.forEach((v, i) => {
      this.unlinkIds.push(v.id);
    });
  }

  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.DataSource.data.length;
    return numSelected === numRows;
  }

  selectrow(row: any) {
    this.selection.toggle(row);
    this.unlinkIds = [];
    this.selection.selected.forEach((v, i) => {
      this.unlinkIds.push(v.id);
    });
  }

  dropTable(event: any) {
    const prevIndex = this.DataSource.data.findIndex(
      (d) => d === event.item.data
    );
    moveItemInArray(this.DataSource.data, prevIndex, event.currentIndex);
    this.DataSource = new MatTableDataSource(this.DataSource.data);

    this.isReordered = true;
  }

  openFlyPanel(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  async linkMetaEOTOEOs() {
    this.spinner = true;
    var options = new EO_MetaEO_LinkOptions();
    options.metaEOId = this.eoId;
    options.eOIDs = this.DataSource.data.map((data) => {
      return data.id;
    });
    await this.EOService.reorderMetaEoLinks(options).then((_) => {
      this.alert.successToast("Reordering Successfull");
      this.getLinkedEOs();
    }).finally(() => {
      this.spinner = false;
    })
  }

  async openUnlinkDialog(templateRef: any, id?: any) {
    this.unlinkEOIds = [];
    const transformedTitle = await this.transformTitle('Enabling Objective');
    this.header = `Unlink ${id ? ` + ${transformedTitle} +` : 'Multiple EOs From Meta EO'}`;
    if (id) {
      this.unlinkEOIds.push(id);
      var temp = this.DataSource.data.find((data) => {
        return data.id === id;
      });
      this.description = `You are selecting to unlink EO'\n''\n' ${temp.number} ${temp.description} from Meta EO ${this.eo.number} ${this.eo.description}`;
    }
    else {
      this.description = `You are selecting to unlink following ${this.unlinkIds.length} EOs:\n\n`;
      this.unlinkIds.forEach((id) => {
        this.unlinkEOIds.push(id);
        var myData = this.DataSource.data.find((data) => {
          return data.id === id;
        });
        this.description = this.description + `${myData.number} - ${myData.description}\n`;
      });
      this.description = this.description + `\nfrom Meta EO ${this.eo.number} - ${this.eo.description}`;
    }
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  async UnlinkEOs(e: any) {
    this.unlinkSpinner = true;
    var options = new EO_MetaEO_LinkOptions();
    options.metaEOId = this.eoId;
    options.eOIDs = this.unlinkEOIds;
    var data = JSON.parse(e);
    options.changeNotes = data['reason'];
    options.effectiveDate = data['effectiveDate'];

    await this.EOService.unlinkEOsFromMetaEO(options).then((_) => {
      this.alert.successToast("EOs unlinked from Meta EO");
      this.getLinkedEOs();
      this.dataBroadcastService.refreshMeta.next(null);
    }).finally(() => {
      this.unlinkSpinner = false;
    })
  }

  /// This function will be used to convert html to pdf
  async downloadPdf() {
    let doc = new jsPDF('p', 'pt', 'letter');
    doc.setFontSize(10);
    doc.text(`${this.eo.number} ${this.eo.description}`, 20, 20);
    doc.html(this.pdf['nativeElement'], {
      autoPaging: 'text',
      margin: [12, 8, 15, 8],
      html2canvas: {
        scale: 0.9
      }
    }).then((res: any) => {
      doc.save('test.pdf');
    })
  }

  async downloadDocs() {

    const opt = {
      margin: {
        top: 100
      },
      orientation: 'landscape' as const // type error: because typescript automatically widen this type to 'string' but not 'Orient' - 'string literal type'
    }
    var doc = await asBlob(String(this.pdf['nativeElement']['innerHTML']), opt) as Blob;
    saveAs(doc, "test.docx");
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }
}
