import { SelectionModel } from '@angular/cdk/collections';
import { Component, EventEmitter, Input, OnInit, Output, ViewChild, ViewContainerRef } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup } from '@angular/forms';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { TrainingIssue_DataElement_VM } from '@models/TrainingIssues/TrainingIssue_DataElement_VM';
import { TaxonomyLevelService } from 'src/app/_Services/QTD/taxonomy-level.service';
import { TestItemTypeService } from 'src/app/_Services/QTD/test-item-type.service';
import { TestItemService } from 'src/app/_Services/QTD/test-item.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';

@Component({
  selector: 'app-fly-panel-testitem-data-element',
  templateUrl: './fly-panel-testitem-data-element.component.html',
  styleUrls: ['./fly-panel-testitem-data-element.component.scss']
})
export class FlyPanelTestItemDataElementComponent implements OnInit {

  @Input() inputTrainingIssueDataElementVM: TrainingIssue_DataElement_VM = new TrainingIssue_DataElement_VM();
  @Output() linkDataElement = new EventEmitter<TrainingIssue_DataElement_VM>();
  @Output() closed = new EventEmitter<any>();
  checkListSelection = new SelectionModel<any>(false);
  @ViewChild(MatSort) sort: MatSort;
  displayedColumns:string[];
  loader: boolean = false;
  linkedId:string;
  filterSearchString: string = '';
  testItemData:any[];
  originalTestItemData:any[];
  dataSource:MatTableDataSource<any>;
  isPreviewFlyPanelOpen:boolean = false;
  selectedRow:any;
  @ViewChild('testItemPaging') set testItemPaging(paging: MatPaginator) {
    if (paging) this.dataSource.paginator = paging;
  }
  taxonomyList:any[] = [];
  testItemTypeList:any[] = [];
  filterForm: UntypedFormGroup;
  constructor(
    public flyPanelService: FlyInPanelService,
    public testItemService:TestItemService,
    private vcf: ViewContainerRef,
    private taxonomyLeverService:TaxonomyLevelService,
    private testItemTypeService:TestItemTypeService,
    private fb: UntypedFormBuilder
  ) { }

  ngOnInit(): void {
    this.linkedId = this.inputTrainingIssueDataElementVM?.dataElementId;
    this.displayedColumns = ["cb", "number", "question", "type", "taxonomy", "status"];
    this.dataSource = new MatTableDataSource();
    this.getAllTestItem();
    this.getAllTaxonomyLevels();
    this.getAllTestItemTypes();
    this.initializeFilterForm();
  }

  initializeFilterForm() {
    this.filterForm = this.fb.group({
      testItemTypeId: [null],
      taxonomyLevelId: [null],
      status: [true] 
    });
  
    this.filterForm.valueChanges.subscribe(() => {
      this.filterTestItem();
    });
  }

  onChangeTestItem(selected: boolean, tp: any) {
    this.checkListSelection.clear();
    if (selected) {
      this.checkListSelection.select(tp);
      this.linkedId = tp.id;
    }
  }

  async getAllTestItem() {
    this.loader = true;
    try {
      const res = await this.testItemService.getAll();
      const tempData = [];
      if (res && Array.isArray(res)) {
        res.forEach((element)=>{
          tempData.push({
              id: element.id,
              type: element.testItemType.description,
              typeId: element.testItemType.id,
              taxonomy: element.taxonomyLevel.description,
              taxonomyId: element.taxonomyLevel.id,
              question: element.description,
              isActive: element.active,
              number: element.number,
              status: element.active,
              eoId: element.eoId,
          })
        })
        this.testItemData = tempData;
        this.dataSource.sort = this.sort;
        this.dataSource = new MatTableDataSource(this.testItemData);
        this.originalTestItemData = Object.assign(this.testItemData);
        this.filterTestItem();
        this.loader = false;
      } else {
        this.loader = false;
      }
    } catch (error) {
      this.loader = false;
    } 
  }
  

  searchFilter(event: any) {
    this.filterSearchString = event?.target?.value ?? "";
    this.filterTestItem();
  }

  clearSearchString() {
    this.filterSearchString = "";
    this.filterTestItem();
  }

  filterTestItem() {
    const searchTerm = this.filterSearchString.trim().toLowerCase();
    const { status, taxonomyLevelId, testItemTypeId } = this.filterForm.value;
  
    this.testItemData = this.originalTestItemData.filter(item => {
      const matchesSearch = !searchTerm ||
        item?.question?.toLowerCase().includes(searchTerm) ||
        item?.type?.toLowerCase().includes(searchTerm);
  
      const matchesStatus = status == null || item?.isActive == status;
  
      const matchesTestItemType = testItemTypeId == null || item?.typeId == testItemTypeId;
  
      const matchesTaxonomy = taxonomyLevelId == null || item?.taxonomyId == taxonomyLevelId;
  
      return matchesSearch && matchesStatus && matchesTestItemType && matchesTaxonomy;
    });
    this.dataSource.data = this.testItemData;
  }

  testItemSort(sort: Sort) {
    this.dataSource.sort = this.sort;
    const data = this.dataSource.data;
    if (!sort.active || sort.direction === '') {
      this.dataSource.data = data;
      return;
    }

    this.dataSource.data = data.sort((a, b) => {
      const isAsc = sort.direction === 'asc';
      switch (sort.active) {
        case 'number':
          return this.compare(a.number, b.number, isAsc);
        case 'question':
          return this.compare(a.question, b.question, isAsc);
        case 'type':
          return this.compare(a.type, b.type, isAsc);
        case 'taxonomy':
          return this.compare(a.taxonomy, b.taxonomy, isAsc);
        default:
          return 0;
      }
    });
  }

  compare(a: number | string | Date, b: number | string | Date, isAsc: boolean) {
    return (a < b ? -1 : 1) * (isAsc ? 1 : -1);
  }

  linkTestItems(){
    this.inputTrainingIssueDataElementVM.dataElementId = this.linkedId;
    this.linkDataElement.emit(this.inputTrainingIssueDataElementVM);
    this.closed.emit();
  }

  async getAllTaxonomyLevels(){
    this.taxonomyList = await this.taxonomyLeverService.getAll();
  }

  async getAllTestItemTypes(){
    this.testItemTypeList = await this.testItemTypeService.getAll();
  }

  clearTestItemType() {
    this.filterForm.patchValue({ testItemTypeId: null});
  }
  
  clearTaxonomyLevel() {
    this.filterForm.patchValue({ taxonomyLevelId: null });
  }
  
  clearStatus() {
    this.filterForm.patchValue({ status: null });
  }

}
