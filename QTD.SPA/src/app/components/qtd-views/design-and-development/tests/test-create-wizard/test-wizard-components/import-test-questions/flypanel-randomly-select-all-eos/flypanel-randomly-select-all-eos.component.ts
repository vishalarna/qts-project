import { SelectionModel } from '@angular/cdk/collections';
import { TemplatePortal } from '@angular/cdk/portal';
import { Component, EventEmitter, Input, OnInit, Output, ViewChild, ViewContainerRef } from '@angular/core';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { TestItemFilter } from 'src/app/_DtoModels/TestItemLink/TestItemFilter';
import { Test_TestItem_LinkOptions } from 'src/app/_DtoModels/Test_TestItem_Link/Test_TestItem_LinkOptions';
import { TaxonomyLevelService } from 'src/app/_Services/QTD/taxonomy-level.service';
import { TestItemTypeService } from 'src/app/_Services/QTD/test-item-type.service';
import { TestsService } from 'src/app/_Services/QTD/tests.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-flypanel-randomly-select-all-eos',
  templateUrl: './flypanel-randomly-select-all-eos.component.html',
  styleUrls: ['./flypanel-randomly-select-all-eos.component.scss']
})
export class FlypanelRandomlySelectAllEosComponent implements OnInit {

  @Output() closed = new EventEmitter<any>();
  @Input() testId: any;
  generate_number:number = 0;
  maximumNumber: number= 0;
  taxonomy: any[] = [];
  question_type: any[] = [];
  isRandomize: boolean = false;
  isDisabled: boolean =false;
  selection = new SelectionModel<any>(true, []);
  linkIds: any[] = [];
  isLinkingTestItem: boolean = false;
  isLoading: boolean = false;
  isRandLoading: boolean = false;
  isPreview: boolean = false;
  dataSource = new MatTableDataSource<any>();
  originalData = new MatTableDataSource<any>();
  @ViewChild(MatSort, { static: false }) sort: MatSort;
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  selectedRow: any;

  displayedColumns = ["question", "type", "taxonomy", "status", "del"];

  constructor(private alert: SweetAlertService,
              private testItemTypeService: TestItemTypeService,
              private taxonomyLevelService: TaxonomyLevelService,
              private testService: TestsService,
              public flyPanelService: FlyInPanelService,
              private vcf: ViewContainerRef,) { }

  ngOnInit(): void {
    this.readyQuestionTypes();
    this.readyTaxonomyLevels();
    this.readyMaximumCount();
  }

  async readyMaximumCount()
  {
    this.testService.getLinkedEOs(this.testId)
      .then((res: any[]) => {
        res.forEach(data => {
          this.maximumNumber = this.maximumNumber + data.maximumNumber;
        })
        this.generate_number = this.maximumNumber;
        this.isDisabled = true;
      })
      .catch((err: any) => {
        
      })
  }


  close() {
    
    //this.flyPanelService.close();
    this.isPreview = false;
  }

  preview() {
    this.isPreview = true
  }

  IncreaseNumber() {
    if(this.generate_number < this.maximumNumber)
    {
      this.isDisabled = false;
      this.generate_number = this.generate_number + 1;
    }
    else
    {
      this.isDisabled = true;

    }
  }

  DecreaseNumber() {
    if(this.generate_number > 0){
      this.generate_number--;
    }
    else{
      this.generate_number=0;
      this.alert.errorToast('Random Number can not be below 0');
    }

    if(this.generate_number < this.maximumNumber)
    {
      this.isDisabled = false;
    }
    else
    {
      this.isDisabled = true;
    }

  }

  async readyQuestionTypes() {
    await this.testItemTypeService.getAll().then((res: any) => {
      this.question_type = res;
      this.question_type.forEach((ques) => {
        ques.checked = true;
      })
      
    }).catch((err: any) => {
      
    })
  }

  async readyTaxonomyLevels(){
    await this.taxonomyLevelService.getAll().then((res:any)=>{
      this.taxonomy = res;
      this.taxonomy.forEach((tax) => {
        tax.checked = true;
      })
      
    }).catch((err: any) => {
      
    })
  }

  Randomize() {
    
    this.isRandLoading = true;
    let ques_filter = this.question_type.filter(i => i.checked == true);
    let tax_filter = this.taxonomy.filter(i => i.checked == true);
    var taxIds: any[] = [];
    var quesIds: any[] = [];
    tax_filter.forEach((tax) => {
      taxIds.push(tax.id);
    });

    ques_filter.forEach((ques) => {
      quesIds.push(ques.id);
    });


    var testItemFilter = new TestItemFilter();
    testItemFilter.testItemTypeIds = quesIds;
    testItemFilter.taxonomyIds = taxIds;
    testItemFilter.generate_number = this.generate_number;
    testItemFilter.testId = this.testId;


    this.testService.filterTestItems(testItemFilter)
        .then((res: any) => {
          
          var tempData: any[] =[];
          res.forEach((element) => {
            tempData.push({
              id: element.id,
              type: element.testItemType.description,
              typeId: element.testItemType.id,
              taxonomy: element.taxonomyLevel.description,
              taxonomyId: element.taxonomyLevel.id,
              question: element.description,
              isActive: element.active ? "ACTIVE" : "INACTIVE",
              number: element.number,
              status: element.active,
              eoId: element.eoId,
            })
          });

          const expected = new Set();
          const unique = tempData.filter(item => !expected.has(JSON.stringify(item)) ? expected.add(JSON.stringify(item)) : false);
          this.dataSource.paginator = this.paginator;
          this.dataSource.sort = this.sort;

          var taken: any[] = [];
          var List: any[] = [];
          var num = 0;
          let itemCount =unique.length>this.generate_number?this.generate_number:unique.length;
          
          while(num != itemCount)
          {
            var y = Math.floor(Math.random() * unique.length);
            var take = taken.indexOf(y);
            if(take == -1)
            {
              List.push(unique[y]);
              taken.push(y);
              num = num + 1;
            }
          }

          this.dataSource.data = List;
          this.originalData.data = List;

          this.linkIds = [];
          this.dataSource.data.forEach((v, i) => {
            this.linkIds.push(v.id);
          });


          // this.dataSource.paginator = this.paginator;
          // this.dataSource.sort = this.sort;
          // this.dataSource.data = tempData;
          // this.originalData.data = tempData;

          // this.linkIds = [];
          // tempData.forEach((v, i) => {
          //   this.linkIds.push(v.id);
          // });

          this.isRandomize = true;
        })
        .catch((err: any) => {
          this.alert.errorToast(err);
        })
        .finally(() => {
          this.isRandLoading= false;
        })




  }

  async linkTestItems() {
    
    this.isLinkingTestItem = true;
    var testLink = new Test_TestItem_LinkOptions();
    testLink.testId = this.testId;
    testLink.testItemIds = [];
    testLink.testItemIds = this.linkIds;
    this.testService.LinkTestItem(this.testId, testLink)
        .then(() => {
          this.alert.successToast("Successfully linked the test items");
          this.closed.emit();
        })
        .finally(() => {
          this.isLinkingTestItem = false;
        })
  }

  masterToggle() {
    this.isAllSelected()
      ? this.selection.clear()
      : this.dataSource.data.forEach((row) => this.selection.select(row));

    this.linkIds = [];
    this.selection.selected.forEach((v, i) => {
      this.linkIds.push(v.id);
    });
  }

  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.dataSource.data.length;
    return numSelected === numRows;
  }

  selectrow(row: any) {
    this.selection.toggle(row);
    this.linkIds = [];
    this.selection.selected.forEach((v, i) => {
      this.linkIds.push(v.id);
    });
  }

  openFlyInPanel(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);

  }

  openPreviewFlyInPanel(templateRef: any, row: any) {
    
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  deleteRow(row: any)
  {
    var index = this.dataSource.data.findIndex(x => x.id == row.id);
    var filter = this.dataSource.data.filter(x => x.id == row.id);

    this.dataSource.data.splice(index, 1);
    this.dataSource._updateChangeSubscription();

    this.linkIds = [];
          this.dataSource.data.forEach((v, i) => {
            this.linkIds.push(v.id);
          });
  }

}
