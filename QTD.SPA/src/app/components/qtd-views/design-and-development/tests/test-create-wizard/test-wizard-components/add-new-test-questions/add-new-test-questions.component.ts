import { SelectionModel } from '@angular/cdk/collections';
import { TemplatePortal } from '@angular/cdk/portal';
import { AfterViewInit, Component, Input, OnDestroy, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { TestItemService } from 'src/app/_Services/QTD/test-item.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { TestItem } from 'src/app/_DtoModels/TestItem/TestItem';
import { MatSort, Sort } from '@angular/material/sort';
import { ActivatedRoute, Router } from '@angular/router';
import { SubSink } from 'subsink';
import { TestsService } from 'src/app/_Services/QTD/tests.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { Test_TestItem_LinkOptions } from 'src/app/_DtoModels/Test_TestItem_Link/Test_TestItem_LinkOptions';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';

@Component({
  selector: 'app-add-new-test-questions',
  templateUrl: './add-new-test-questions.component.html',
  styleUrls: ['./add-new-test-questions.component.scss']
})
export class AddNewTestQuestionsComponent implements OnInit, AfterViewInit, OnDestroy {

  dataSource: MatTableDataSource<any> = new MatTableDataSource<any>();
  selection = new SelectionModel<any>(true, []);
  unlinkIds: any[] = [];
  unlinkSpinner: boolean = false;
  showSpinner: boolean = false;
  originalData = new MatTableDataSource<any>();
  selectedRow: any;
  @Input() testId: any;
  subscription = new SubSink();
  previousQuestion!: any;

  mode: 'add' | 'edit' = 'add';

  @ViewChild(MatSort) sort: MatSort/*(sort: MatSort)  {

    if (sort) this.DataSource.sort = sort;

  } */

  @ViewChild(MatPaginator) paginator!:MatPaginator;


  displayColumns: string[] = ['number', 'question', 'type', 'taxonomyLevel', 'id'];
  constructor(public flyPanelService: FlyInPanelService,
    private vcf: ViewContainerRef,
    private testItemService: TestItemService,
    private route: ActivatedRoute,
    private testService: TestsService,
    private dataBroadcastService: DataBroadcastService,
    private router: Router,
  ) { }

  ngOnInit(): void {
  }

  ngAfterViewInit(): void {

    this.subscription.sink = this.route.params.subscribe((res: any) => {
      
      if (res.id !== undefined) {
        this.testId = res.id;
        this.getTestItems();
      }

    });

  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  sortData(sort: Sort) {

    this.dataSource.sort = this.sort;

  }

  openFlyInPanel(templateRef: any) {

    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  openPreviewFlyInPanel(templateRef: any, row: any) {
    
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  close() {
    this.flyPanelService.close();
  }

  async getTestItems() {
    this.showSpinner = true;
    this.testService.GetTestItem(this.testId)
      .then((res: any[]) => {
        var tempData: any[] = [];
        res.forEach((data) => {
          tempData.push({
            number: data.number,
            testId: data.testItemLinkId,
            id: data.testItemId,
            question: data.question,
            type: data.testItemType,
            taxonomyLevel: data.taxonomyLevel,
            taxonomyId: data.taxonomyLevelId,
            typeId: data.testItemTypeId,
            eoId: data.enablingObjectiveId,
          });
        });

        this.dataSource.data = tempData;
      })
      .finally(() => {
        this.showSpinner = false;
        setTimeout(()=>{
          this.dataSource.paginator = this.paginator;
        },1)
      })
  }

  saveQuestion(id: any) {
    if (this.mode === 'add') {
      
      
      var options = new Test_TestItem_LinkOptions();
      options.testId = this.testId;
      options.testItemIds.push(id);
      this.testService.LinkTestItem(options.testId, options).then((_) => {
        this.getTestItems();
      });
    }
    else{
      this.getTestItems();
    }
  }

  openEditFlyPanel(templateRef: any, data: any) {
    this.selectedRow = data;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal)
  }

  // async getTestItems() {
  //   this.showSpinner = true;
  //   this.testItemService.getAll()
  //         .then((res: TestItem[]) => {
  //           
  //           var tempData:any[] = [];
  //           res.forEach((data) => {
  //             tempData.push({
  //               number: data.number,
  //               id: data.id,
  //               question: data.description,
  //               type: data.testItemType.description,
  //               taxonomyLevel: data.taxonomyLevel.description
  //             });
  //           });

  //           this.dataSource = new MatTableDataSource(tempData);
  //           this.originalData.data = tempData;
  //         })
  //         .finally(() => {
  //           this.showSpinner = false;
  //         })
  // }
}
