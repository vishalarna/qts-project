import { SelectionModel } from '@angular/cdk/collections';
import { TemplatePortal } from '@angular/cdk/portal';
import { Component, EventEmitter, Input, OnInit, Output, ViewChild, ViewContainerRef } from '@angular/core';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { ActivatedRoute, Router } from '@angular/router';
import { TestItemFilter } from 'src/app/_DtoModels/TestItemLink/TestItemFilter';
import { Test_TestItem_LinkOptions } from 'src/app/_DtoModels/Test_TestItem_Link/Test_TestItem_LinkOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { TaxonomyLevelService } from 'src/app/_Services/QTD/taxonomy-level.service';
import { TestItemTypeService } from 'src/app/_Services/QTD/test-item-type.service';
import { TestsService } from 'src/app/_Services/QTD/tests.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-flypanel-randomly-select-by-individual-eo',
  templateUrl: './flypanel-randomly-select-by-individual-eo.component.html',
  styleUrls: ['./flypanel-randomly-select-by-individual-eo.component.scss']
})
export class FlypanelRandomlySelectByIndividualEoComponent implements OnInit {

  @Output() closed = new EventEmitter<any>();
  @Input() testId: any;
  generate_number:number = 0;
  taxonomy: any[] = [];
  question_type: any[] = [];
  isRandomize: boolean = false;
  selection = new SelectionModel<any>(true, []);
  linkIds: any[] = [];
  linkTestItemIds: any[] = [];
  isLinkingTestItem: boolean = false;
  isLoading: boolean = false;
  isPreview: boolean = false;
  dataSource = new MatTableDataSource<any>();
  eoDataSource = new MatTableDataSource<any>();
  originalData = new MatTableDataSource<any>();
  eo_List: any[] = [];
  isRandomizeLoading:  boolean =false;
  eo_ListOriginal: any[] = [];
  @ViewChild(MatSort, { static: false }) sort: MatSort;
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

  hasChild = (_: number, node: any) =>
    !!node.children && node.children.length > 0;

  selectedRow: any;
  filterString = "";
  typeFilter = "";
  statusFilter = "";
  taxonomyFilter = "";
  isRandomButton = false;
  isRandomizeCheck = false;
  displayedColumns = ["question", "type", "taxonomy", "status", "del"];
  eoDisplayColumns = ["cb"];
  constructor(private alert: SweetAlertService,
              private testItemTypeService: TestItemTypeService,
              private taxonomyLevelService: TaxonomyLevelService,
              private testService: TestsService,
              public flyPanelService: FlyInPanelService,
              private vcf: ViewContainerRef,
              private router:ActivatedRoute,
              private labelPipe: LabelReplacementPipe,) { }

  ngOnInit(): void {
    this.loadAsync();
  }
  
  async loadAsync(){
    await this.readyQuestionTypes();
    await this.readyTaxonomyLevels();
    if(this.testId !== undefined)
    {
      await this.getLinkedEOs();
    }
  }

  filterData() {
    if (this.filterString.length > 0) {
      var filterObj = this.filterString.trim().toLowerCase();
      this.eo_List = this.eo_ListOriginal.filter(x => x.description.match(filterObj));

      this.eo_List = this.eo_ListOriginal.filter(f => f.description.toLowerCase().match(String(this.filterString).toLowerCase()));
    }
    else {
      this.eo_List = this.eo_ListOriginal;
    }
  }

  async getLinkedEOs(){
    this.isLoading = true;
    this.testService.getLinkedEOs(this.testId)
      .then((res: any[]) => {
        var tempData: any[] = [];
        res.forEach((data) => {
          tempData.push({
            id: data.eoId,
            description: data.number + ' ' + data.description,
            eoNumber: data.number,
            eoType: data.type,
            eoDescription: data.description,
            checked: false,
            opened: false,
            generate_number: data.maximumNumber,
            maxNumber: data.maximumNumber,
            isDisabled: true,
            question_type: this.question_type,
            taxonomy: this.taxonomy,
          })

        });

        this.eoDataSource.data = tempData;
        this.eo_List = tempData;
        this.eo_ListOriginal = tempData;
      })
      .catch(async (err: any) => {
        this.alert.errorToast("Error fetching linked " + await this.transformTitle('Enabling Objective') + "s");
      })
      .finally(() =>{
        this.isLoading = false;
      })
  }


  close() {
    //this.flyPanelService.close();
    this.isPreview = false;
  }

  preview() {
    this.isPreview = true
  }

  IncreaseNumber(row: any) {
    this.eo_List.forEach((data) => {
      if(data.id === row.id)
      {
        if(row.generate_number == row.maxNumber)
        {
          row.isDisabled = true;
        }
        else
        {
          row.isDisabled = false;
          data.generate_number = row.generate_number + 1;
        }

      }

    });
    this.eo_ListOriginal = this.eo_List;
  }

  DecreaseNumber(row: any) {
    this.eo_List.forEach((data) => {
      if(data.id === row.id)
      {
        if(data.generate_number > 0){
          data.generate_number--;
        }
        else{
          data.generate_number=0;
          this.alert.errorToast('Random Number can not be below 0');
        }
        if(row.generate_number == row.maxNumber)
        {
          row.isDisabled = true;
        }
        else
        {
          row.isDisabled = false;
        }
      }

    });
    this.eo_ListOriginal = this.eo_List;
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

  Randomize(row: any) {
    let ques_filter = row.question_type.filter(i => i.checked == true);
    let tax_filter = row.taxonomy.filter(i => i.checked == true);
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
    testItemFilter.generate_number = row.generate_number;


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


          this.dataSource.paginator = this.paginator;
          this.dataSource.sort = this.sort;
          this.dataSource.data = tempData;
          this.originalData.data = tempData;

          this.linkTestItemIds = [];
          tempData.forEach((v, i) => {
            this.linkTestItemIds.push(v.id);
          });

          this.isRandomize = true;
        })
        .catch((err: any) => {
          this.alert.errorToast(err);
        })
        .finally(() => {

        })




  }

  RandomizeList() {
    this.isRandomizeLoading =true;
    this.isRandomizeCheck = true;
    var tempData: any[] =[];
    this.generate_number = 0;
    this.eo_List.forEach((row) => {
      if(row.checked === true)
      {
        this.generate_number = this.generate_number + row.generate_number;
      let ques_filter = row.question_type.filter(i => i.checked == true);
      let tax_filter = row.taxonomy.filter(i => i.checked == true);
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
      testItemFilter.generate_number = row.generate_number;
      testItemFilter.testId = this.testId;
      testItemFilter.eoId = row.id;
      this.testService.filterTestItems(testItemFilter)
        .then((res: any) => {
          

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

          this.linkTestItemIds = [];
          tempData.forEach((v, i) => {
            this.linkTestItemIds.push(v.id);
          });

          this.isRandomize = true;
        })
        .catch((err: any) => {
          this.alert.errorToast(err);
        })
        .finally(() => {

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

          this.linkTestItemIds = [];
          this.dataSource.data.forEach((v, i) => {
            this.linkTestItemIds.push(v.id);
          });
           this.isRandomizeLoading = false;
        })
      }
    })
  }

  async linkTestItems() {
    this.isLinkingTestItem = true;
    var testLink = new Test_TestItem_LinkOptions();
    testLink.testId = this.testId;
    testLink.testItemIds = [];
    testLink.testItemIds = this.linkTestItemIds;
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

  onCheck(event: any, id: any)
  {
    if(event.checked == true)
    {
      this.linkIds.push(id);
    }
    else {
      const index: number = this.linkIds.indexOf(id);
      if(index !== -1)
      {
        this.linkIds.splice(index, 1);
      }

    }

    this.eo_List.forEach((data) => {
      if(data.id === id)
      {
        if(event.checked == true)
        {
          data.checked = true;
        }
        else
        {
          data.checked = false;
        }
      }

    });

    this.eo_ListOriginal = this.eo_List;
    if(this.eo_List.filter(x => x.checked == true))
    {
      this.isRandomButton = true;
    }
    else
    {
      this.isRandomButton = false;
    }


  }

  toggle(row: any) {
    this.eo_List.forEach((data) => {
      if(data.id === row.id)
      {
        data.opened = !row.opened;
      }
      else {
        data.opened = false;
      }
    });
    this.eo_ListOriginal = this.eo_List;
  }

  deleteRow(row: any)
  {
    var index = this.dataSource.data.findIndex(x => x.id == row.id);
    var filter = this.dataSource.data.filter(x => x.id == row.id);

    this.dataSource.data.splice(index, 1);
    this.dataSource._updateChangeSubscription();

    this.linkTestItemIds = [];
          this.dataSource.data.forEach((v, i) => {
            this.linkTestItemIds.push(v.id);
          });
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }

}
