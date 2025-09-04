import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { IlaService } from 'src/app/_Services/QTD/ila.service';
import { DeliveryMethod } from '@models/DeliveryMethod/DeliveryMethod';
import { Provider } from '@models/Provider/Provider';
import { MatSort, Sort } from '@angular/material/sort';
import { SelectionModel } from '@angular/cdk/collections';
import { MetaILAMembersLinkOptions, MetaILAMembersListOptions } from '@models/MetaILAMembersLink/MetaILAMembersLinkOptions';
import { MetaILAService } from 'src/app/_Services/QTD/meta-ila.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { ILA } from '../../../../training-map/training-map-design/training-map-design.component';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { ProviderService } from 'src/app/_Services/QTD/provider.service';
import { DeliveryMethodeService } from 'src/app/_Services/QTD/delivery-methode.service';
import { MetaILAVM } from '@models/MetaILA/MetaILAVM';


@Component({
  selector: 'app-fly-panel-link-ilas-to-meta-ila',
  templateUrl: './fly-panel-link-ilas-to-meta-ila.component.html',
  styleUrls: ['./fly-panel-link-ilas-to-meta-ila.component.scss']

})
export class FlyPanelLinkILAsToMetaILAComponent implements OnInit {
  @Input() metaILA: MetaILAVM;
  @Input() metaILAId: string;
  @Input() mode: string;
  @Input() ilaLinkedData: any[];
  @Output() closed = new EventEmitter<any>();
  @Output() metaILALinkedMembers = new EventEmitter<any>();
  displayedColumns: string[] = ['index', 'number', 'title', 'nickName', 'provider', 'isMeta', 'creditHours', 'deliveryMethods', 'status'];
  dataSource: MatTableDataSource<any> | undefined;
  deliveryMethods: DeliveryMethod[] = [];
  providers: Provider[] = [];
  tempDataSource: any[] | undefined = [];
  selection = new SelectionModel<ILA>(true, []);
  searchString = "";
  spinner: boolean = true;
  ILAsIds: any[] = [];
  providerId: string;
  deliveryMethodId: string;

  constructor(private ilaSrvc: IlaService,
    private metaILAService: MetaILAService,
    private alert: SweetAlertService,
    private labelPipe: LabelReplacementPipe,
    private providerService:ProviderService,
    private deliveryMethodService: DeliveryMethodeService) {

  }

  @ViewChild(MatSort, { static: false }) sort: MatSort;

  async ngOnInit() {
    this.getAllActiveILAs();
    this.getAllProviders();
    this.getAllDeliveryMethods();
  }

  sortData(sort: Sort) {
    this.dataSource.sort = this.sort;
  }

  masterToggle() {
    const currentRows = this.dataSource.data.filter(row => !this.ILAsIds.includes(row.id));
    
    if (currentRows.every(row => this.selection.isSelected(row))) {
      currentRows.forEach(row => this.selection.deselect(row));
    } else {
      currentRows.forEach(row => this.selection.select(row));
    }
  }
  
  isAllSelected() {
    const currentRows = this.dataSource.data.filter(row => !this.ILAsIds.includes(row.id));
    return currentRows.length > 0 && currentRows.every(row => this.selection.isSelected(row));
  }


  getAllActiveILAs() {
    this.spinner = true;
    this.ilaSrvc.getAllActiveDetails().then((res) => {
      let tempSrc: any[] = [];
      for (const [index, data] of res.entries()) {
        const isLinked = this.ilaLinkedData.some(linkedData => linkedData.id === data.id);
        tempSrc.push({
          index: index,
          id: data.id,
          number: data.number,
          title: data.name,
          nickName: data.nickName,
          provider: data.providerName,
          isMeta: data.isMeta,
          creditHours: data.creditHours,
          deliveryMethods: data.deliveryMethodName,
          status: data.status,
          active: data.active,
          checked: isLinked,
          disabled: isLinked,
          deliveryMethodId: data.deliveryMethodId,
          providerId: data.providerId
        });
      }
      this.tempDataSource = tempSrc;
      this.dataSource = new MatTableDataSource(this.tempDataSource);
      this.spinner = false;
    })
  }

  searchILAs(event: any) {
    this.searchString = event.target.value;
    this.filterILAs(this.searchString.trim().toLowerCase());
  }

  filterILAs(searchTerm: string) {
    let data = this.tempDataSource.filter(item => {
      const matchesSearch = 
        item?.number?.trim().toLowerCase().includes(searchTerm) ||
        item?.title?.trim().toLowerCase().includes(searchTerm) ||
        item?.nickName?.trim().toLowerCase().includes(searchTerm) ||
        item?.provider?.trim().toLowerCase().includes(searchTerm);
  
      const matchesProvider = this.providerId ? (item.providerId === this.providerId) : true;
  
      const matchesDelivery = this.deliveryMethodId ? (item.deliveryMethodId === this.deliveryMethodId) : true;
  
      return matchesSearch && matchesProvider && matchesDelivery;
    });
    this.dataSource = new MatTableDataSource(data);
  }

  async getAllProviders(){
    this.providers = await this.providerService.getWihtoutIncludes();
  }
  
  onProviderChange(id: any): void {
    this.providerId= id;
    this.filterILAs(this.searchString.trim().toLowerCase());
   }
  
   onDeliveryMethodChange(id: any): void {
    this.deliveryMethodId = id;
    this.filterILAs(this.searchString.trim().toLowerCase());
   }

  async getAllDeliveryMethods(){
    this.deliveryMethods = await this.deliveryMethodService.getAll()
  }
  async linkILAs() {
    this.spinner = true;
    var options = new MetaILAMembersListOptions();
    var ilaToLink = this.selection.selected.map((data) => {
      return data.id
    });
    ilaToLink.forEach(async(i, index) => {
      var option = new MetaILAMembersLinkOptions()
      option.metaILAID = this.metaILA.id;
      option.iLAID = i;
      option.metaILAConfigPublishOptionID = null;
      option.sequenceNumber = this.ilaLinkedData.length + index + 1;
      options.ilaMetaILAMembers.push(option);
    })
      await this.metaILAService.createMetaILAMemeberLink(this.metaILA.id, options).then(async (res) => {
        this.alert.successToast('Meta ' + await this.labelPipe.transform('ILA') + ' Member Links Successfully');
        this.metaILALinkedMembers.emit(res);
        this.closed.emit();
      }).catch(async (err) => {
        this.alert.errorToast('Error Creating Meta ' + await this.labelPipe.transform('ILA') + ' Member Links' + err);
      });

  }
}
