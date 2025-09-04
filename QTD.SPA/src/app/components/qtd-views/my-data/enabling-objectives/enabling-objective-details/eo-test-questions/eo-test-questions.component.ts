import { SelectionModel } from '@angular/cdk/collections';
import { TemplatePortal } from '@angular/cdk/portal';
import { AfterViewInit, Component, Input, OnDestroy, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { ActivatedRoute } from '@angular/router';
import { TestItem } from 'src/app/_DtoModels/TestItem/TestItem';
import { TestItemOptions } from 'src/app/_DtoModels/TestItem/TestItemOptions';
import { TestItemWithLinkCount } from 'src/app/_DtoModels/TestItem/TestItemWithLinkCount';
import { EnablingObjectivesService } from 'src/app/_Services/QTD/enabling-objectives.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-eo-test-questions',
  templateUrl: './eo-test-questions.component.html',
  styleUrls: ['./eo-test-questions.component.scss']
})
export class EoTestQuestionsComponent implements OnInit, AfterViewInit, OnDestroy {

  spinner = false;
  DataSource: MatTableDataSource<any>;
  @Input() isActive = true;
  @Input() isMeta = false;
  @Input() eo : any;
  unlinkIds: any[] = [];
  selection = new SelectionModel<any>(true, []);
  srcList: any[] = [];
  displayColumns = ['id', 'number', 'description', 'count'];

  selectedNumber = '';
  selectedId = '';
  selectedTitle = "";

  subscription = new SubSink();
  eoId = "";
  questIds: any[] = [];
  unlinkDescription = "";
  alreadyLinked: any[] = [];
  testItems !: TestItemWithLinkCount[];

  header = "";
  description = "";
  unlinkTestIds: any[] = [];

  @ViewChild(MatSort) sort : MatSort/*(sort: MatSort)  {
    if (sort) this.DataSource.sort = sort;
  } */

  @ViewChild(MatPaginator) set tblPaging(paginator: MatPaginator) {
    if (paginator) this.DataSource.paginator = paginator;
  }
  constructor(
    public flyPanelSrvc: FlyInPanelService,
    private vcf: ViewContainerRef,
    private route: ActivatedRoute,
    public dialog: MatDialog,
    private eoService: EnablingObjectivesService,
    private alert: SweetAlertService,
    private dataBroadcastService: DataBroadcastService,
  ) { }

  ngOnInit(): void {
  }

  ngAfterViewInit(): void {
    //this.readyData();
    this.subscription.sink = this.route.params.subscribe(async (res: any) => {
      this.eoId = String(res.id).split('-')[1];
      this.isMeta = await this.eoService.checkisMeta(this.eoId);
      this.readyData();
    });

    this.subscription.sink = this.dataBroadcastService.refreshMeta.subscribe((_) => {
      this.readyData();
    })
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  sortData(sort: Sort) {
    this.DataSource.sort = this.sort;
  }

  async readyData() {
    if (this.eoId !== "") {
      this.selection.clear();
      this.unlinkIds = [];

      this.isMeta ? this.testItems =  await this.eoService.getTestItemsWithMetaEOCount(this.eoId)
        : this.testItems = await this.eoService.getTestItemsWithCount(this.eoId);
      this.srcList = this.testItems;
      this.DataSource = new MatTableDataSource(this.testItems);
      this.dataBroadcastService.refreshStats.next(null);
    }


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
      this.unlinkIds.push(v.id);
    });
  }

  selectrow(row: any) {
    this.selection.toggle(row);
    this.unlinkIds = [];
    this.selection.selected.forEach((v, i) => {
      this.unlinkIds.push(v.id);
    });
  }

  openFlyPanel(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelSrvc.open(portal);
  }

  async UnlinkTests(event: any) {
    
    this.spinner = true;
    var options = new TestItemOptions();
    options.testIds = this.unlinkTestIds;
    var data = JSON.parse(event);
    options.changeNotes = data['reason'];
    options.effectiveDate = data['effectiveDate'];

    await this.eoService.unlinkTests(this.eoId, options).then((_) => {
      this.alert.successToast("Tests Unlinked From EO");
      this.readyData();
      this.dataBroadcastService.refreshStats.next(null);
    }).finally(() => {
      this.spinner = false;
    })
  }

  openDialog(templateRef: any, id?: any) {
    this.unlinkTestIds = [];
    this.header = `Unlink Test Question`;
    if (id) {
      this.unlinkTestIds.push(id);
      var temp = this.DataSource.data.find((data) => {
        return data.id === id;
      });
      this.description = `You are selecting to unlink Test Question  \n 1 - ${temp.description.replace(/<[^>]*>/g, '')} \n\n from Selected EO ${this.eo} `;
    }
    else {
      this.description = `You are selecting to unlink following ${this.unlinkIds.length} Test Questions:\n`;
      this.unlinkIds.forEach((id, i) => {
        this.unlinkTestIds.push(id);
        var myData = this.DataSource.data.find((data) => {
          return data.id === id;
        });
        this.description = this.description + `${i + 1} - ${myData.description.replace(/<[^>]*>/g, '')} \n`;
      });
      this.description = this.description + `\nfrom Selected EO ` + this.eo;
    }
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }
}
