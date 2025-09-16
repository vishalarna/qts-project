import { SelectionModel } from '@angular/cdk/collections';
import {
  Component,
  EventEmitter,
  OnDestroy,
  OnInit,
  Output,
  ViewChild,
  ViewContainerRef,
} from '@angular/core';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { TemplatePortal } from '@angular/cdk/portal';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { Router } from '@angular/router';

import {
  animate,
  state,
  style,
  transition,
  trigger,
} from '@angular/animations';
import { IlaProviderDeleteComponent } from './ila-provider-delete/ila-provider-delete.component';
import { IlaTopicDeleteComponent } from './ila-topic-delete/ila-topic-delete.component';
import { IlaTopicInactiveComponent } from './ila-topic-inactive/ila-topic-inactive.component';
import { ProviderService } from 'src/app/_Services/QTD/provider.service';
import { DeliveryMethodeService } from 'src/app/_Services/QTD/delivery-methode.service';
import { DeliveryMethod } from 'src/app/_DtoModels/DeliveryMethod/DeliveryMethod';
import { TopicService } from 'src/app/_Services/QTD/ila_topic.service';
import { Provider } from 'src/app/_DtoModels/Provider/Provider';
import { IlaService } from 'src/app/_Services/QTD/ila.service';
import { ILAStatsVM } from 'src/app/_DtoModels/ILA/ILAStatsVM';
import { ILAOptions } from 'src/app/_DtoModels/ILA/ILAOptions';
import { SubSink } from 'subsink';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { ProviderOptions } from 'src/app/_DtoModels/Provider/ProviderOptions';
import { ILA_TopicOptions } from 'src/app/_DtoModels/ILA_Topic/ILA_TopicOptions';
import { MetaILAOptions } from 'src/app/_DtoModels/MetaILA/MetaILAOptions';
import { MetaILAService } from 'src/app/_Services/QTD/meta-ila.service';
import { te } from 'date-fns/locale';
import { ProviderFilterVM } from 'src/app/_DtoModels/Provider/ILA_ProviderVM';
import { FilterByOptions } from 'src/app/_DtoModels/ILA/FilterByOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { HttpResponse } from '@angular/common/http';

@Component({
  selector: 'app-list-ila',
  templateUrl: './list-ila.component.html',
  styleUrls: ['./list-ila.component.scss'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({ height: '0px', maxHeight: '0px' })),
      state('expanded', style({ height: '*' })),
      state('collapsed, void', style({ height: '0px', minHeight: '0' })),
      transition(
        'expanded <=> collapsed',
        animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')
      ),
    ]),
  ],
})
export class ListIlaComponent implements OnInit, OnDestroy {
  @Output() editCheck = new EventEmitter();
  dataSource: MatTableDataSource<any>;
  providerData: ProviderFilterVM[];
  originalProviders: any[];
  expandedTableDataSource: MatTableDataSource<any> | undefined;
  metaTableDataSource: MatTableDataSource<any> = new MatTableDataSource();
  topicTableDataSource: MatTableDataSource<any>;
  topic_edit_mode: boolean = false;
  provider_edit_mode: boolean;
  provider_change_mode: boolean;
  provider_copy_mode: boolean;
  topic_copy_mode: boolean;
  providerFilterIds : any[];
  sortCol = 'title';
  sortOrder = 'asc';
  statusFilter = 'active';
  priorityFilter = '';
  includeEmpty: 'include' | 'exclude' = 'include';
  viewSelected = 'provider';
  change_topic: boolean;
  edit_topic: boolean;
  draft_dev_check: boolean = false;
  published_check: boolean = false;
  unlinked_topic_check: boolean = false;
  changeProviderId: any;
  changeTopicId: any;
  ILAIdforChange: any;
  subscriptions = new SubSink();
  ilacount: number = 0;
  selectedCheckboxesBulk: any[] = [];
  displayedColumns: string[] = [
    'index',
    'provider',
    'ilaCount',
    'priority',
    'status',
    'id',
  ];

  ilaIdForVersion!: any;
  filterName: any;

  displayedColumnsMeta: string[] = [
    'index',
    'name',
    'ilaCount',
    'status',
    'id',
  ];

  displayedColumnsTopic: string[] = [
    'index',
    'topic',
    'count',
    'priority',
    'status',
    'id',
  ];

  displayedExpandedColumns: string[] = [
    'index',
    'num',
    'ilaTitle',
    'NickName',
    'ProviderName',
    'IsMeta',
    'creditHours',
    'deliveryMethode',
    'Status',
  ];

  toggle = false;
  columnsToDisplayWithExpand = [...this.displayedColumns];
  expandedData: any | null;
  provider_delete: boolean;
  provider_inactive: boolean;
  provider_ila_check: boolean = false;
  ila_inactive: any;
  topic_inactive: any;
  header: string;
  description: string;
  confirmText: string;
  cancelText: string;
  expandedDataSource: any[] | undefined = [];
  filterValue: any = '';
  changeStatusILAId: any;
  showLoader = false;
  providerRow: any;
  flyPanelCloseCheck: any;
  filterName1: any[] = [];
  metaILAPanelMode: string;
  selectedMetaILAID:string;
  linkedTopicIds:string[]=[];
  //sort and pagination for provider
  @ViewChild('providerSort') set sort(sorting: MatSort) {
    if (sorting) this.dataSource.sort = sorting;
  }

  @ViewChild('providerPaginator') set paginator(paging: MatPaginator) {
    if (paging) this.dataSource.paginator = paging;
  }

  //sort and pagination for meta
  @ViewChild('metaSort') set metaSort(sorting: MatSort) {
    if (sorting) this.metaTableDataSource.sort = sorting;
  }

  @ViewChild('metaPaginator') set metaPaginator(paging: MatPaginator) {
    if (paging) this.metaTableDataSource.paginator = paging;
  }

  //sort and pagination for Topic
  @ViewChild('topicSort') set topicSort(sorting: MatSort) {
    if (sorting) this.topicTableDataSource.sort = sorting;
  }

  @ViewChild('topicPaginator') set topicPaginator(paging: MatPaginator) {
    if (paging) this.topicTableDataSource.paginator = paging;
  }

  selection = new SelectionModel<any>(true, []);
  selection_topic = new SelectionModel<any>(true, []);
  selection_topic_second = new SelectionModel<any>(true, []);
  selection_provider = new SelectionModel<any>(true, []);
  deliveryMethods: DeliveryMethod[] = [];
  ILAStats!: ILAStatsVM;
  selectedCheckboxes: any[] = [];
  selectedCheckboxesMetaILA: any[] = [];
  ilaTitleSearch: string = '';
  isLoading = false;
  ilaActive: 'active' | 'inactive' = 'active';
  providerName: any[] = [];
  metaILaValues: any;
  totalIlaData:any[] = [];
  tempIlaData:any[] = [];
  showMoreIlaButton:boolean = false;
  currentIlaCount:number;
  filteredILAData:any[] = [];
  delayedFilterDataSource: () => void;
  basicIla = false;

  constructor(
    public flyPanelSrvc: FlyInPanelService,
    private vcr: ViewContainerRef,
    private dialog: MatDialog,
    private providerSrvc: ProviderService,
    private deliveryMethodSrvc: DeliveryMethodeService,
    private ilatopicSrvc: TopicService,
    private ilaSrvc: IlaService,
    private alert: SweetAlertService,
    private router: Router,
    public metaILAService: MetaILAService,
    private labelPipe:LabelReplacementPipe
  ) {

    this.delayedFilterDataSource = this.delay(() => {
      this.filterDataSource();
    }, 250);

  }

  async ngOnInit() {
    this.deliveryMethods = await this.deliveryMethodSrvc.getAll();

    this.getILAStats();
    this.getProviders(this.ilaTitleSearch);
    this.getMetaILAID();

    if (this.flyPanelCloseCheck === true) {
      this.getMetaILAID();
    }
    this.originalProviders = [];
    this.providerFilterIds = [];

  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }

  clearFilter() {
    this.filterValue = '';
    this.ilaTitleSearch = '';
    this.getProviders(this.ilaTitleSearch);
    this.getTopics();
  }

  clearFilterFunction() {
    this.filterName = '';
    this.priorityFilter = '';
    this.statusFilter = 'active';
    this.includeEmpty = 'include';
   this.filterValue = '';
   this.ilaTitleSearch = '';
    this.providerData = [];
    this.dataSource.filter = null;
    this.providerName = [];
    this.filterName1 = [];
    this.ilaActive='active';
    this.providerFilterIds=[];
    if (this.viewSelected === 'provider') {
      this.getProviders('');
    }
    if (this.viewSelected === 'topic') {
      this.getTopics();
    }
  }

  getILAStats() {
    this.ilaSrvc.getILAStats().then((res) => {
      this.ILAStats = res;
    });
  }

  setPriorityFilter(filter: string) {
    const newFilterName = filter === 'yes' ? 'Priority Yes' : 'Priority No';
    this.filterName1 = this.filterName1.filter(name => name !== 'Priority Yes' && name !== 'Priority No');
    this.filterName1.push(newFilterName);
    this.priorityFilter = filter;
    this.ToggleILAGroupBy(this.viewSelected);
    this.filterName = newFilterName;
  }

  applyStatusFilter(filter: string) {
    const newFilterName = filter === 'active' ? 'Active' : 'Inactive';
    this.filterName1 = this.filterName1.filter(name => name !== 'Active' && name !== 'Inactive');
    this.filterName1.push(newFilterName);
    this.statusFilter = filter;
    this.ToggleILAGroupBy(this.viewSelected);
    this.filterName = newFilterName;
  }

  applyILAFilter(filter:'active' | 'inactive') {
    const newFilterName = filter === 'active' ? 'ILA Active' : 'ILA Inactive';
    this.filterName1 = this.filterName1.filter(name => name !== 'ILA Active' && name !== 'ILA Inactive');
    this.filterName1.push(newFilterName);
    this.ilaActive = filter;
    this.ToggleILAGroupBy(this.viewSelected);
    var data = this.toggleILAFilter(this.totalIlaData);
    this.getInitialILAData(data);
    this.filterName = newFilterName;
  }

  filterDataStatus() {
    if (this.statusFilter === 'active') {
    } else {
    }
  }

  filterDataPriority() {}

  FilterData: any;
  async getProviderLinkedILAs(id: any) {
    this.expandedDataSource = undefined;
    this.expandedTableDataSource = undefined;
    this.totalIlaData = [];
    this.currentIlaCount = 20;
    const response = await this.ilaSrvc.getByProvider(id);
    this.totalIlaData = response.map((ila, index) => ({
      index: index,
      num: ila.number,
      title: ila.name,
      nickName: ila.nickName,
      providerName: ila.providerName,
      isMeta: 'No',
      creditHours: ila.totalHours,
      deliveryMethod: ila.deliveryMethodName,
      status: ila.isPublished ? 'Published' : 'Draft',
      active: ila.active,
      disableDelete: ila.classScheduleEmpCount > 0,
      image: this.ilaSrvc.baseUrlForImage + ila.image,
      providerId: ila.providerId,
      id: ila.id,
      topicIds: Array.from(new Set(ila.topicIds)),
      updateId: id,
      isNerc:ila.isNerc
    })); 
    this.filteredILAData  = this.toggleILAFilter(this.totalIlaData);
    if(this.filteredILAData?.length>20){
      this.showMoreIlaButton = true;
    }else{
      this.showMoreIlaButton = false;
    }
    this.getInitialILAData(this.filteredILAData);
  }

  getInitialILAData(data:any){
    this.tempIlaData = [];
    var ilas = data.slice(0,20);
    this.tempIlaData = [...this.tempIlaData,...ilas];
    this.makeILASourceTree(this.tempIlaData);
  }

  makeILASourceTree(data:any){
    this.expandedTableDataSource = new MatTableDataSource(data);
  }

  getMoreIlas(){
    if(this.currentIlaCount < this.filteredILAData.length){
      var temp = this.filteredILAData?.slice(this.currentIlaCount,this.currentIlaCount+20)
      temp.map((item)=>{
        this.tempIlaData?.push(item)
      })
      this.makeILASourceTree(this.tempIlaData)
    }
    if((this.currentIlaCount + 20) >= this.filteredILAData.length){
      this.showMoreIlaButton = false;
    }
    this.currentIlaCount += 20;
  }

  async getTopicLinkedILAs(id: any) {
    this.expandedDataSource = undefined;
    this.expandedTableDataSource = undefined;
    this.totalIlaData = [];
    this.currentIlaCount = 20;
    const response = await this.ilaSrvc.getByTopic(id);
    this.totalIlaData = response.map((ila, index) => ({
      index: index,
      num: ila.number,
      title: ila.name,
      nickName: ila.nickName,
      providerName: ila.providerName,
      isMeta: 'No',
      creditHours: ila.totalHours,
      deliveryMethod: ila.deliveryMethodName,
      status: ila.isPublished ? 'Published' : 'Draft',
      active: ila.active,
      disableDelete: ila.classScheduleEmpCount > 0,
      image: this.ilaSrvc.baseUrlForImage + ila.image,
      providerId: ila.providerId,
      id: ila.id,
      topicIds: Array.from(new Set(ila.topicIds)),
      updateId: id
    }));
    this.filteredILAData  = this.toggleILAFilter(this.totalIlaData);
    if(this.filteredILAData?.length>20){
      this.showMoreIlaButton = true;
    }else{
      this.showMoreIlaButton = false;
    }
    this.getInitialILAData(this.filteredILAData);
  }

  toggleButtonClicked(data: any) {
    this.toggle = data;
  }

  applyEmptyFilter(doInclude: 'include' | 'exclude') {
    const newFilterName = (doInclude === 'include') ? 'Include Unlinked' : 'Exclude Unlinked';
    this.filterName1 = this.filterName1.filter(name => name !== 'Include Unlinked' && name !== 'Exclude Unlinked');
    this.filterName1.push(newFilterName);
    this.includeEmpty = doInclude;
    this.filterName=newFilterName;
    switch (this.viewSelected.trim().toLowerCase()) {
      case 'provider':
        this.getProviders(this.ilaTitleSearch);
        break;
      case 'topic':
        this.getTopics();
        break;
      default:
        break;
    }
  }

  checkEmpty(event: any) {
    if (this.ilaTitleSearch === '') {
      this.filterDataSource();
    }
  }

  delay<T extends (...args: any[]) => void>(fn: T, ms: number) {
    let timer: ReturnType<typeof setTimeout>; // Ensures compatibility in both environments

    return function (...args: Parameters<T>) {
      clearTimeout(timer);
      timer = setTimeout(() => fn(...args), ms);
    };
  }

  filterDataSource() {
    this.filterValue = '';
    if (this.viewSelected === 'provider') {
        this.filterValue = this.ilaTitleSearch.toLowerCase();
        this.getProviders(this.ilaTitleSearch);

    } else if (this.viewSelected === 'meta') {
      if (this.expandedData) {
        this.expandedTableDataSource.filter = this.ilaTitleSearch
          .trim()
          .toLowerCase();
      } else {
        this.metaTableDataSource.filter = this.ilaTitleSearch
          .trim()
          .toLowerCase();
      }
    } else {
      if (this.expandedData  || this.expandedTableDataSource) {
        this.expandedTableDataSource.filter = this.ilaTitleSearch
          .trim()
          .toLowerCase();
      }
        this.filterValue = this.ilaTitleSearch.toLowerCase();
        this.getTopics();
    }
  }

  async getProviders(filter: string) {
    this.isLoading = true;
    this.expandedDataSource = undefined;
    var filterOptions = new FilterByOptions();
    filterOptions.filter = filter;
    filterOptions.doInclude = this.includeEmpty;
    filterOptions.providerIds = this.providerFilterIds;
    if(this.statusFilter === 'active'){
      filterOptions.activeStatus=true;
    }
    else if(this.ilaActive === 'inactive'){
      filterOptions.activeStatus=false;
    }
      if (this.ilaActive === 'active') {
          filterOptions.activeILAStatus = true;
      } else if (this.ilaActive === 'inactive') {
          filterOptions.activeILAStatus = false;
    }
    await this.providerSrvc
      .getAllWithFilterAndILACount(filterOptions)
      .then((res) => {
        let tempSrc: any[] = [];
        let tempFilter: any[] = [];
        for (const [index, data] of res.entries()) {
          tempSrc.push({
            index: index,
            id: data.id,
            provider: data.name,
            ilaCount: data.ilaCount,
            priority: data.isPriority,
            status: data.active ? 'active' : 'inactive',
            expanded: false,
            isNerc: data.isNerc,
          });
          tempFilter.push({
            id: data.id,
            provider: data.name,
            providerNumber: data.providerNumber,
            checked: this.providerData?.find((x) => x.id === data.id)?.checked,
            status: data.active ? 'active' : 'inactive',
          });
        }
       tempFilter = tempFilter.filter((x) => x.status === this.statusFilter);
        this.providerData = tempFilter;
        //apply active/inactive filter on tempsrc
        tempSrc = tempSrc.filter((x) => x.status === this.statusFilter);
        //apply priority filter on tempsrc
        if (this.priorityFilter === 'yes' || this.priorityFilter === 'no') {
          tempSrc = tempSrc.filter(
            (x) => x.priority === (this.priorityFilter === 'yes' ? true : false)
          );
        }

        let providerFilters = this.providerData
          .filter((x) => x.checked === true)
          .map((x) => x.id);
        if (providerFilters.length > 0) {
          tempSrc = tempSrc.filter((x) => providerFilters.includes(x.id));
        }
        this.dataSource = new MatTableDataSource(tempSrc);
        this.dataSource.filter = '';
        this.dataSource.sort = this.sort;
        if(this.providerFilterIds?.length === 0){
          this.originalProviders = [...this.providerData];
        }
      })
      .finally(() => {
        this.isLoading = false;
      });
  }

  ApplyProviderFilter(data: any) {
    this.providerData = data;
    if(data?.length !== this.providerFilterIds.length){
      this.originalProviders = data;
    }
    this.ToggleILAGroupBy(this.viewSelected);
  }

  async getTopics() {
    this.isLoading = true;
    this.expandedDataSource = undefined;
    var filterOptions = new FilterByOptions();
    filterOptions.filter = this.ilaTitleSearch;
    filterOptions.doInclude = this.includeEmpty;
    if(this.statusFilter === 'active'){
      filterOptions.activeStatus=true;
    }
    else if(this.ilaActive === 'inactive'){
      filterOptions.activeStatus=false;
    }

    if (this.ilaActive === 'active') {
        filterOptions.activeILAStatus = true;
    } else if (this.ilaActive === 'inactive') {
        filterOptions.activeILAStatus = false;
    }
    this.ilatopicSrvc
      .getAllWithFilterAndILACount(filterOptions)
      .then((res) => {
        let topicTableData: any[] = [];
        for (const [index, data] of res.entries()) {
          topicTableData.push({
            index: index,
            id: data.id,
            topic: data.name,
            count: data.ilaCount,
            priority: data.isPriority ? 'YES' : 'NO',
            status: data.active ? 'ACTIVE' : 'IN-ACTIVE',
          });
        }
        //apply active inactive filter
        topicTableData = topicTableData.filter(
          (x) =>
            x.status ===
            (this.statusFilter === 'active' ? 'ACTIVE' : 'IN-ACTIVE')
        );
        //apply priority filter on tempsrc
        if (this.priorityFilter === 'yes' || this.priorityFilter === 'no') {
          topicTableData = topicTableData.filter(
            (x) => x.priority === (this.priorityFilter === 'yes' ? 'YES' : 'NO')
          );
        }

        if (this.topicTableDataSource) {
          this.topicTableDataSource.data = topicTableData;
        } else {
          this.topicTableDataSource = new MatTableDataSource(topicTableData);
        }
        this.topicTableDataSource.sort = this.topicSort;

        this.topicTableDataSource.filter = '';
        this.topicTableDataSource.sort = this.sort;
      })
      .finally(() => {
        this.isLoading = false;
      });
  }

  ToggleILAGroupBy(groupBy: string) {
    switch (groupBy) {
      case 'meta':
        this.getMetaILAID();
        break;
      case 'topic':
        this.getTopics();
        break;
      case 'provider':
      default:
        this.getProviders(this.ilaTitleSearch);
        break;
    }

    this.viewSelected = groupBy;
  }

  getILAsGroupByMeta() {
    //populate meta table data source
    let metaTabledata = [
      {
        description: 'Power System Initial Training',
        linked: 3,
        status: 'active',
      },
      {
        description: 'Quality Training',
        linked: 8,
        status: 'active',
      },
      {
        description: 'Professional Transmission Training',
        linked: 14,
        status: 'active',
      },
      {
        description: 'Utility Institute',
        linked: 8,
        status: 'active',
      },
    ];

    metaTabledata = [];

    this.metaTableDataSource.data = metaTabledata;

    //populate expanded list table data source
    this.expandedDataSource = [];
    this.expandedTableDataSource = new MatTableDataSource(
      this.expandedDataSource
    );

    var data = this.toggleILAFilter(metaTabledata);
    this.getInitialILAData(data)
  }

  /** Whether the number of selected elements matches the total number of rows. */
  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.dataSource.data.length;
    return numSelected === numRows;
  }

  /** Selects all rows if they are not all selected; otherwise clear selection. */
  masterToggle() {
    this.isAllSelected()
      ? this.selection.clear()
      : this.dataSource.data.forEach((row) => this.selection.select(row));
  }

  //provider View List
  /** Whether the number of selected elements matches the total number of rows. */
  isAllSelectedProviderList() {
    const numSelectedProvider = this.selection_provider.selected.length;
    const numRowsProvider = this.expandedTableDataSource?.data.length;
    return numSelectedProvider === numRowsProvider;
  }

  /** Selects all rows if they are not all selected; otherwise clear selection. */
  masterToggleProviderList() {
    this.isAllSelectedProviderList()
      ? this.selection_provider.clear()
      : this.expandedTableDataSource?.data.forEach((row) =>
          this.selection_provider.select(row)
        );
  }

  openFlyPanel(templateRef: any) {
    this.change_topic = false;
    this.edit_topic = true;
    this.changeTopicId = undefined;
    let selectedEmployeeNumbers = [];
    for (let item of this.selection_topic.selected) {
      this.selectedCheckboxesBulk.push(item);
    }
    const portal = new TemplatePortal(templateRef, this.vcr);
    this.flyPanelSrvc.open(portal);
  }

  openFlyPanelTopicEdit(templateRef: any, row: any, name: string) {
    switch (name) {
      case 'Edit':
        this.change_topic = false;
        this.edit_topic = true;
        this.topic_copy_mode = false;
        this.changeTopicId = row;

        break;
      case 'Copy':
        this.change_topic = false;
        this.edit_topic = true;
        this.topic_copy_mode = true;
        this.changeTopicId = row;
        break;
    }

    const portal = new TemplatePortal(templateRef, this.vcr);
    this.flyPanelSrvc.open(portal);
  }

  openFlyInPanelChangeTopic(templateRef: any, topicIds: string[], ilaId: any) {
    this.change_topic = true;
    this.edit_topic = false;
    this.topic_copy_mode = false;
    this.linkedTopicIds = topicIds;
    this.ILAIdforChange = ilaId;
    const portal = new TemplatePortal(templateRef, this.vcr);
    this.flyPanelSrvc.open(portal);
  }

  openFlyPanelProviderEdit(templateRef: any, row: any, name: string) {
    switch (name) {
      case 'Edit':
        this.provider_edit_mode = true;
        this.provider_change_mode = false;
        this.provider_copy_mode = false;
        this.changeProviderId = row.id;
        this.providerRow = row;

        break;

      case 'Copy':
        this.provider_edit_mode = true;
        this.provider_change_mode = false;
        this.provider_copy_mode = true;
        this.changeProviderId = row;
        break;
    }

    const portal = new TemplatePortal(templateRef, this.vcr);
    this.flyPanelSrvc.open(portal);
  }

  openFlyPanelMetaILA(templateRef: any, mode: string,id?:string) {
    this.metaILAPanelMode = mode;
    if(this.metaILAPanelMode == 'edit' || this.metaILAPanelMode == 'copy'){
      this.selectedMetaILAID = id;
    }
    else{
      const uniqueIds = new Set(this.selectedCheckboxes.map(item => item.id));
      for (let item of this.selection_provider.selected) {
        if (!uniqueIds.has(item.id)) {
        this.selectedCheckboxes.push(item);
        uniqueIds.add(item.id);
        }
      }
    }
    const portal = new TemplatePortal(templateRef, this.vcr);
    this.flyPanelSrvc.open(portal);
  }

  expandRow() {}

  openFlyInPanelProvider(templateRef: any) {
    this.provider_edit_mode = true;
    this.provider_change_mode = false;
    this.changeProviderId = undefined;
    const portal = new TemplatePortal(templateRef, this.vcr);
    this.flyPanelSrvc.open(portal);
  }

  openFlyInPanelChangeProvider(templateRef: any, providerId: any, ilaId: any) {
    this.provider_edit_mode = false;
    this.provider_change_mode = true;
    this.changeProviderId = providerId;
    this.ILAIdforChange = ilaId;
    const portal = new TemplatePortal(templateRef, this.vcr);
    this.flyPanelSrvc.open(portal);
  }

  openFlyInPanelMetaChangeProvider(templateRef: any) {
    this.topic_edit_mode = true;
    const portal = new TemplatePortal(templateRef, this.vcr);
    this.flyPanelSrvc.open(portal);
  }

  openFlyInPanelVersionHistory(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcr);
    this.flyPanelSrvc.open(portal);
  }

  providerCheck: boolean = false;
  moduleName: any;
  activeCheck: boolean;
  openFlyInPanelStatistics(templateRef: any, name: string) {
    if (name === 'Draft/Dev') {
      this.draft_dev_check = true;
      this.published_check = false;
      this.providerCheck = false;
      this.activeCheck = false;
      this.unlinked_topic_check = false;
    } else if (name === 'Published') {
      this.published_check = true;
      this.draft_dev_check = false;
      this.providerCheck = false;
      this.activeCheck = false;
      this.unlinked_topic_check = false;
    } else if (name === 'Providers' || name === 'Topics') {
      this.draft_dev_check = false;
      this.published_check = false;
      this.providerCheck = true;
      this.activeCheck = false;
      this.unlinked_topic_check = false;
    } else if (name === 'Active') {
      this.activeCheck = true;
      this.draft_dev_check = false;
      this.published_check = false;
      this.providerCheck = false;
      this.unlinked_topic_check = false;
    } else if (name === 'Unlinked') {
      this.activeCheck = false;
      this.draft_dev_check = false;
      this.published_check = false;
      this.providerCheck = false;
      this.unlinked_topic_check = true;
    }
    this.moduleName = name;
    const portal = new TemplatePortal(templateRef, this.vcr);
    this.flyPanelSrvc.open(portal);
  }


  async OnDelete(deleteTopic: any, name: string, data: any) {
    this.changeStatusILAId = data;
    if (name == 'Topic') {
      var options = new ILA_TopicOptions();
      options.actionType = 'delete';
      if (data.count === 0) {
        (this.header = 'Delete Topic'),
          (this.description = `You are selecting to Delete the selected ` + await this.labelPipe.transform('ILA') +` Topic ${data.topic}.`),
          (this.cancelText = 'No'),
          (this.confirmText = 'Yes');
        const dialogRef = this.dialog.open(deleteTopic, {
          width: '380px',
          hasBackdrop: true,
          disableClose: true,
        });

        this.subscriptions.sink = dialogRef
          .afterClosed()
          .subscribe((res: any) => {
            if (res) {
              this.ilatopicSrvc
                .delete(data.id, options)
                .then((res: any) => {
                  this.alert.successToast(
                    `Topic ${data.topic} deleted successfully`
                  );
                  this.getTopics();
                  this.getILAStats();
                })
                .catch((err: any) => {});
            }
          });
      } else {
        this.alert.errorToast('Can not delete Topic because ' + await this.labelPipe.transform('ILA') +'s are attached');
      }
    } else if (name == 'ILA') {
      var isFromIWB = await this.ilaSrvc.isILACreatedFromInstructorWorkbook(data.id);
      (this.header = 'Delete '+ await this.labelPipe.transform('ILA')),
      isFromIWB ? (this.description = `Are you sure you want to delete this ` + await this.labelPipe.transform('ILA') +`? This ` + await this.labelPipe.transform('ILA') +` was created in the Instructor Workbook, deleting it will also delete the record from the Instructor Workbook`) : (this.description = `You are selecting to delete the selected ` + await this.labelPipe.transform('ILA') +` ${data.title}. This cannot be undone. Are you sure you want to continue?`),
        (this.cancelText = 'No'),
        (this.confirmText = 'Yes');
      const dialogRef = this.dialog.open(deleteTopic, {
        width: '380px',
        hasBackdrop: true,
        disableClose: true,
      });
      this.subscriptions.sink = dialogRef
        .afterClosed()
        .subscribe((res: any) => {
          if (res) {
            this.ilaSrvc
              .delete(data.id)
              .then(async (res: any) => {
                this.getProviderLinkedILAs(data.providerId);
                this.getILAStats();
                this.alert.successToast(await this.labelPipe.transform('ILA') +` deleted successfully`);
              })
              .catch((err: any) => {});
          }
        });
    } else if (name == 'Provider') {
      var options = new ProviderOptions();
      if (data.ilaCount === 0) {
        options.actionType = 'delete';
        (this.header = 'Delete Provider'),
          (this.description = `You are selecting to Delete the selected ` + await this.labelPipe.transform('ILA') +` Provider ${data.provider}. Are you sure you want to continue?`),
          (this.cancelText = 'No'),
          (this.confirmText = 'Yes');
        const dialogRef = this.dialog.open(deleteTopic, {
          width: '380px',
          hasBackdrop: true,
          disableClose: true,
        });

        this.subscriptions.sink = dialogRef
          .afterClosed()
          .subscribe((res: any) => {
            if (res === true) {
              this.providerSrvc
                .delete(data.id, options)
                .then((res: any) => {
                  this.alert.successToast(
                    `Provider ${data.provider} deleted successfully`
                  );
                  this.getProviders(this.ilaTitleSearch);
                  this.getILAStats();
                })
                .catch((err: any) => {});
            }
          });
      } else {
        this.alert.errorToast(
          'Can not delete Provider because ' + await this.labelPipe.transform('ILA') + 's are attached'
        );
      }
    }
    else if (name == 'Meta') {
      var metaILAOptions = new MetaILAOptions();
      metaILAOptions.actionType = 'delete';
      (this.header = 'Delete Meta ' + await this.labelPipe.transform('ILA')),
        (this.description = `You are selecting to delete Meta ` + await this.labelPipe.transform('ILA') +` ${this.changeStatusILAId.name} are you sure you want to continue? Deleting a Meta ` + await this.labelPipe.transform('ILA') +` will not delete ` + await this.labelPipe.transform('Employee') + ` training completion records for the individual ` + await this.labelPipe.transform('ILA') +`s. Deleting a Meta ` + await this.labelPipe.transform('ILA') +` will only delete the grouping.`),
        (this.cancelText = 'No'),
        (this.confirmText = 'Yes');
      const dialogRef = this.dialog.open(deleteTopic, {
        width: '380px',
        hasBackdrop: true,
        disableClose: true,
      });
      this.subscriptions.sink = dialogRef
        .afterClosed()
        .subscribe((res: any) => {
          if (res) {
            this.metaILAService
              .deleteMetaILAAsync(data.id,metaILAOptions)
              .then(async (res: any) => {
                this.getMetaILAID()
                this.getTopics();
                this.getILAStats();

                this.alert.successToast(`Meta ` + await this.labelPipe.transform('ILA') +` deleted successfully`);
              })
              .catch((err: any) => {});
          }
        });
    }
  }

  async onMakeInactive(deleteTopic: any, name: string, id: any) {
    this.changeStatusILAId = id;
    if (name == 'Topic') {
      (this.header = 'Make Topic Inactive'),
        (this.description =
          'You are selecting to make the Topic inactive. This will remove the Topic as an option when creating an ' + await this.labelPipe.transform('ILA') + ' , and will remove the Topic name from existing ' + await this.labelPipe.transform('ILA') + 's. Are you sure you want to continue?'),
        (this.cancelText = 'No'),
        (this.confirmText = 'Yes');
    } else if (name == 'Meta') {
      (this.header = 'Make Meta Inactive'),
        (this.description =
          'You are selecting to make the Meta inactive. This will remove the Meta ' + await this.labelPipe.transform('ILA') +  ' as an option when creating an ' + await this.labelPipe.transform('ILA') + ', and will remove the Meta ' + await this.labelPipe.transform('ILA') + 'name from existing ' + await this.labelPipe.transform('ILA') + 's. Are you sure you want to continue?'),
        (this.cancelText = 'No'),
        (this.confirmText = 'Yes');
    } else if (name == 'ILA') {
      (this.header = 'Make ' + await this.labelPipe.transform('ILA') +  ' Inactive'),
        (this.description =
          'You are selecting to make this ' + await this.labelPipe.transform('ILA') +  ' inactive. All ' + await this.labelPipe.transform('ILA') +  ' history and ' + await this.labelPipe.transform('Employee') + ' records will be retained. Are you sure you want to continue?'),
        (this.cancelText = 'No'),
        (this.confirmText = 'Yes');
    } else if (name == 'Grid') {
      (this.header = 'Make ' + await this.labelPipe.transform('ILA') +  ' Inactive'),
        (this.description =
          'You are selecting to make this ' + await this.labelPipe.transform('ILA') +  ' inactive. All ' + await this.labelPipe.transform('ILA') +  ' history and ' + await this.labelPipe.transform('Employee') + ' records will be retained. Are you sure you want to continue?'),
        (this.cancelText = 'No'),
        (this.confirmText = 'Yes');
    } else if (name == 'Provider') {
      (this.header = 'Make Provider Inactive'),
        (this.description =
          ' This Provider, has active ' + await this.labelPipe.transform('ILA') +  's. Making this Provider Inactive will inactivate all linked ' + await this.labelPipe.transform('ILA') +  's. Are you sure you want to continue? '),
        (this.cancelText = 'No'),
        (this.confirmText = 'Yes');
    }

    const dialogRef = this.dialog.open(deleteTopic, {
      width: '380px',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  async onMakeILAInactive(dialog: any, data: any) {
    var ilaEmpReleaseList = await this.ilaSrvc.canILAbeDeactivated(data.id);
    if(ilaEmpReleaseList.length == 0){
    var options = new ILAOptions();
    (this.header = `Make ` + await this.labelPipe.transform('ILA') +` ${data.status ? 'Inactive' : 'Active'}`),
      (this.description =
        'You are selecting to make this ' + await this.labelPipe.transform('ILA') +  ' inactive. All ' + await this.labelPipe.transform('ILA') +  ' history and ' + await this.labelPipe.transform('Employee') + ' records will be retained. Are you sure you want to continue?'),
      (this.cancelText = 'No'),
      (this.confirmText = 'Yes');
    options.actionType = data.active ? 'inactive' : 'active';
    const dialogRef = this.dialog.open(dialog, {
      width: '380px',
      hasBackdrop: true,
      disableClose: true,
    });

    this.subscriptions.sink = dialogRef.afterClosed().subscribe((res: any) => {
      if (res === true) {
        this.ilaSrvc
          .changeStatus(data.id, options)
          .then(async (res: any) => {
            this.alert.successToast(
               await this.labelPipe.transform('ILA')  + ` Status changed to ${options.actionType}`
            );
            if (this.viewSelected === 'provider') {
              this.getProviderLinkedILAs(data.providerId);
            } else if (this.viewSelected === 'topic') {
              this.getTopicLinkedILAs(data.providerId);
            }
          })
          .catch(async (err: any) => {
            this.alert.errorToast(
              `Error changing ` + await this.labelPipe.transform('ILA') +` Status changed to ${options.actionType}`
            );
          });
      }
    });
  }else{
    const empList = ilaEmpReleaseList.map(item => `• ${item}`).join('\n');
    const verb = ilaEmpReleaseList.length > 1 ? 'are' : 'is';
    this.alert.warningAlert(`This ${await this.labelPipe.transform('ILA')} can't be deactivated as:\n${empList} \n ${verb} currently deployed to EMP`);
  }
  }

  async onMakeProviderInactive(dialog: any, data: any) {
    var options = new ProviderOptions();
    options.actionType = data.status === 'active' ? 'inactive' : 'active';
    (this.header = `Make Provider ${options.actionType}`),
      (this.description = `This Provider, has active ` + await this.labelPipe.transform('ILA') +`s. Making this Provider ${options.actionType} will ${options.actionType} all linked ` + await this.labelPipe.transform('ILA') +`s. Are you sure you want to continue? `),
      (this.cancelText = 'No'),
      (this.confirmText = 'Yes');
    const dialogRef = this.dialog.open(dialog, {
      width: '380px',
      hasBackdrop: true,
      disableClose: true,
    });

    this.subscriptions.sink = dialogRef.afterClosed().subscribe((res: any) => {
      if (res === true) {
        this.providerSrvc
          .changeStatus(data.id, options)
          .then((res: any) => {
            this.alert.successToast(
              `Provider Status changed to ${options.actionType}`
            );
            this.getProviders(this.ilaTitleSearch);
          })
          .catch((err: any) => {
            this.alert.errorToast(
              `Error changing Provider Status changed to ${options.actionType}`
            );
          });
      }
    });
  }

  async onMakeTopicInactive(dialog: any, data: any) {
    var options = new ILA_TopicOptions();
    options.actionType = data.status === 'ACTIVE' ? 'inactive' : 'active';
    (this.header = `Make Topic ${options.actionType}`),
      (this.description = `You are selecting to make the Topic ${options.actionType}. This will remove the Topic as an option if it is inactive when creating an ` + await this.labelPipe.transform('ILA') +` , and will remove the Topic name from existing ` + await this.labelPipe.transform('ILA') +`s. Are you sure you want to continue?`),
      (this.cancelText = 'No'),
      (this.confirmText = 'Yes');
    const dialogRef = this.dialog.open(dialog, {
      width: '380px',
      hasBackdrop: true,
      disableClose: true,
    });
    this.subscriptions.sink = dialogRef.afterClosed().subscribe((res: any) => {
      if (res === true) {
        this.ilatopicSrvc
          .changeStatus(data.id, options)
          .then((res: any) => {
            this.getTopics();
            this.alert.successToast(
              `Topic Status changed to ${options.actionType}`
            );
          })
          .catch((err: any) => {
            this.alert.errorToast(
              `Error changing Topic Status changed to ${options.actionType}`
            );
          });
      }
    });
  }

  async onMakeMetaInactive(dialog: any, data: any) {
    var options = new MetaILAOptions();
    options.actionType = data.status === 'active' ? 'Inactive' : 'Active';
    (this.header = `Make ${options.actionType}`),
      (this.description = `Are you sure you want to make Meta-` + await this.labelPipe.transform('ILA') +` ${data.name} ${options.actionType} ?`),
      (this.cancelText = 'No'),
      (this.confirmText = 'Yes');
    const dialogRef = this.dialog.open(dialog, {
      width: '380px',
      hasBackdrop: true,
      disableClose: true,
    });
    this.subscriptions.sink = dialogRef.afterClosed().subscribe((res: any) => {
      if (res) {
        this.metaILAService
              .deleteMetaILAAsync(data.id,options)
              .then(async (res: any) => {
                this.getMetaILAID()
                this.getTopics();
                this.getILAStats();

                this.alert.successToast(`Meta ` + await this.labelPipe.transform('ILA') +` ${data.name} ${options.actionType} successfully`);
              })
              .catch((err: any) => {});
      }
    });
  }

  sortInnerColumn(col: string) {
    this.sortCol = col;
    this.sortOrder = this.sortOrder === 'asc' ? 'desc' : 'asc';
  }

  OnEditILA(row: any) {
    this.editCheck.emit(true);

    this.router.navigate(['/dnd/ila/create'], {
      queryParams: { data: row.id },
    });
  }

  OnClickCopy(row: any) {
    this.expandedDataSource = undefined;
    this.expandedTableDataSource = undefined;
    this.ilaSrvc
      .CopyIlaById(row.id)
      .then(async () => {
        this.alert.successToast( await this.labelPipe.transform('ILA') +  ' copy created successfully.');

        this.getProviderLinkedILAs(row.providerId);
        const index = this.dataSource.data.find(
          (x) => x.id === row.providerId
        ).index;
        this.getTopics();
        this.dataSource.data[index].ilaCount =
          this.dataSource.data[index].ilaCount + 1;
        this.getILAStats();
      })
      .catch(async (err) => {
        this.alert.errorToast('Error while Copying ' + await this.labelPipe.transform('ILA'));
      });
  }

  async getMetaILAID() {
    let tempSrc: any[] = [];
    await this.metaILAService
      .getAll()
      .then((res: any) => {
        for (const [index, data] of res.entries()) {
          tempSrc.push({
            index: index,
            name: data.name,
            isPriority: data.isPriority,
            ilaCount: data.metaIlaCount,
            status: data.active ? 'active' : 'inactive',
            id: data.id,
            isDeleteAllowed: data.isDeleteAllowed,
            isReleasedToEmployees: data.isReleasedToEmployees
          });
        }
        tempSrc = tempSrc.filter((x) => x.status === this.statusFilter);
        if (this.priorityFilter === 'yes' || this.priorityFilter === 'no') {
          tempSrc = tempSrc.filter(
            (x) => x.isPriority === (this.priorityFilter === 'yes' ? true : false)
          );
        }
        this.metaTableDataSource.data = tempSrc;
      })
      .catch((err) => {});
  }
  deleteILA() {
    this.dialog.closeAll();
  }
  changeILAStatus() {}

  toggleILAFilter(datasource:any) {
    this.filteredILAData = null;
    this.expandedTableDataSource = null;
    var data = datasource?.filter((data) => {
      if (this.ilaActive === 'active') {
        if(this.ilaTitleSearch.trim() != ""){
          return data.active === true && (data.num.trim().toLowerCase().includes(this.ilaTitleSearch
            .toLowerCase()) || data.title.trim().toLowerCase().includes(this.ilaTitleSearch
              .toLowerCase()))
        }else{
          return data.active === true;
        }
      }
      if(this.ilaActive === 'inactive') {
        if(this.ilaTitleSearch.trim() != ""){
          return data.active === false && (data.num.trim().toLowerCase().includes(this.ilaTitleSearch
            .toLowerCase()) || data.title.trim().toLowerCase().includes(this.ilaTitleSearch
              .toLowerCase()))
        }else{
          return data.active === false;
        }
      }
    });
    return data;
  }

  providerNameFunction(e: any) {
    if (e.length > 0) {
      if (!this.filterName1.includes('Provider')) {
          this.filterName1.push('Provider');
      }
      this.filterName = 'Provider';
      this.providerName = [...e];
  } else {
      const providerIndex = this.filterName1.indexOf('Provider');
      if (providerIndex !== -1) {
          this.filterName1.splice(providerIndex, 1);
      }
  }
  }

  getProviderIdList(val:any){
    this.providerFilterIds = val;
  }

  isILASelected(row : any){
    return (this.selection_provider.selected.findIndex(x=>x.id == row.id) != -1);
  }

    async downloadAsCSV(id:any) {
       var response=await this.ilaSrvc.exportAsCSVAsync(id);
       this.handleFileDownload(response);
    }
  
    private handleFileDownload(response: HttpResponse<Blob>) {
      const contentDispositionHeader = response.headers.get('content-disposition');
  
      const fileName = contentDispositionHeader
        ? contentDispositionHeader.split(';')[1].trim().split('=')[1].replace(/["']/g, "")
        : 'downloaded-file.csv';
  
      const blob = new Blob([response.body!], { type: 'application/octet-stream' });
      const url = window.URL.createObjectURL(blob);
  
      const link = document.createElement('a');
      link.href = url;
      link.download = fileName;
      document.body.appendChild(link);
      link.click();
      document.body.removeChild(link);
    }

    openBasicILAPanel(templateRef: any) {
      const portal = new TemplatePortal(templateRef, this.vcr);
      this.flyPanelSrvc.open(portal);
    }  

}
