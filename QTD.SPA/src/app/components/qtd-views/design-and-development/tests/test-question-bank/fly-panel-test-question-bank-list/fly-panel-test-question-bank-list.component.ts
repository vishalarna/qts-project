import { TemplatePortal } from '@angular/cdk/portal';
import { NestedTreeControl } from '@angular/cdk/tree';
import { Component, Input, OnInit, TemplateRef, ViewChild, ViewContainerRef } from '@angular/core';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { TestItemService } from 'src/app/_Services/QTD/test-item.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-fly-panel-test-question-bank-list',
  templateUrl: './fly-panel-test-question-bank-list.component.html',
  styleUrls: ['./fly-panel-test-question-bank-list.component.scss']
})
export class FlyPanelTestQuestionBankListComponent implements OnInit {
  @Input() moduleName: any;
  list: any[] = [];
  tasktreeControl = new NestedTreeControl<any>(
    (node: any) => node.children
  );
  treedataSource = new MatTableDataSource<any>();
  displayedColumns = ["number", "question", "actions"];
  questionMode: any;
  selectedRow: any;
  spinner: boolean;

  @ViewChild(MatSort) sort!:MatSort;
  @ViewChild('eoPaging') eoPaging: MatPaginator;




  constructor(
    private testItemService: TestItemService,
    private alert: SweetAlertService,
    public flyInClose: FlyInPanelService,
    private vcf: ViewContainerRef,

  ) { }

  ngOnInit(): void {
    this.getNames();
  }

  name: any;
  async getNames() {

    switch (this.moduleName) {
      case 'Active':
        this.spinner = true;

        this.name = 'Active Test Items';
        var tempData: any[] = [];
        var data = await this.testItemService.getTestItemList(this.moduleName);
        data.forEach((element, i) => {
          tempData.push({
            id: element.id,
            question: element.description,
            number: element.number,
            eoId: element.eoId,
            typeId: element.testItemTypeId,
            taxonomyId: element.taxonomyId

          });
        });
        this.treedataSource.data = tempData;
        this.list = tempData;
        this.spinner = false;
        break;

      case 'Inactive':
        this.spinner = true;

        this.name = 'Inactive Test Items';
        var tempData: any[] = [];
        var data = await this.testItemService.getTestItemList(this.moduleName);
        data.forEach((element, i) => {
          tempData.push({
            id: element.id,
            question: element.description,
            number: element.number,
            eoId: element.eoId,
            typeId: element.testItemTypeId,
            taxonomyId: element.taxonomyId
          });
        });
        this.treedataSource.data = tempData;
        this.list = tempData;
        this.spinner = false;

        break;

      case 'Notlinked':
        this.spinner = true;

        this.name = 'Questions Not Linked to Test';
        var tempData: any[] = [];
        var data = await this.testItemService.getTestItemList(this.moduleName)
        data.forEach((element, i) => {
          tempData.push({
            id: element.id,
            question: element.description,
            number: element.number,
            eoId: element.eoId,
            typeId: element.testItemTypeId,
            taxonomyId: element.taxonomyId
          });
        });
        this.treedataSource.data = tempData;
        this.list = tempData;
        this.spinner = false;

        break;

      case 'Noteo':
        this.spinner = true;

        this.name = 'Questions Not Linked to EOs';
        var tempData: any[] = [];
        var data = await this.testItemService.getTestItemList(this.moduleName);
        data.forEach((element, i) => {
          tempData.push({
            id: element.id,
            question: element.description,
            number: element.number,
            eoId: element.eoId,
            typeId: element.testItemTypeId,
            taxonomyId: element.taxonomyId
          });
        });
        this.treedataSource.data = tempData;
        this.list = tempData;
        this.spinner = false;

        break;
    }
    setTimeout(()=>{
      this.treedataSource.sort = this.sort;
      this.treedataSource.paginator = this.eoPaging;

    },1)

  }

  async openFlyPanel(templateRef: TemplateRef<any>) {

    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyInClose.open(portal);
  }



}
