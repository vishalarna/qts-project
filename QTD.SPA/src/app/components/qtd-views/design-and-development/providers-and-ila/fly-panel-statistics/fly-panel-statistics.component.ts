import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { DeliveryMethod } from 'src/app/_DtoModels/DeliveryMethod/DeliveryMethod';
import { Provider } from 'src/app/_DtoModels/Provider/Provider';
import { DeliveryMethodeService } from 'src/app/_Services/QTD/delivery-methode.service';
import { IlaService } from 'src/app/_Services/QTD/ila.service';
import { TopicService } from 'src/app/_Services/QTD/ila_topic.service';
import { ProviderService } from 'src/app/_Services/QTD/provider.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-fly-panel-statistics',
  templateUrl: './fly-panel-statistics.component.html',
  styleUrls: ['./fly-panel-statistics.component.scss'],
})
export class FlyPanelStatisticsComponent implements OnInit {
  @Input() draft_dev_check: boolean;
  @Input() published_check: boolean;
  @Input() providerCheck:boolean;
  @Input() moduleName:any;
  @Input() activeCheck:any;
  @Input() unlinkedTopicCheck:any;
  TableDataSource: MatTableDataSource<any>;
  ProviderDataSource: MatTableDataSource<any>;
  TopicDataSource: MatTableDataSource<any>;

  DataSource: any[] = [];
  deliverMethods: DeliveryMethod[] = [];
  displayedColumns: string[] = [
    'ilaNo',
    'ilaTitle',
    'NickName',
    'deliveryMethode',
  ];
  displayedColumns2: string[] = [
    'No',
    'Title'
  ];
  displayedColumns3: string[] = [
    'No',
    'Title'
  ];
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatSort) providersort: MatSort;
  @ViewChild('paginator') paginator: MatPaginator;
  spinner:boolean=false;
  constructor(
    public flyPanelSrvc: FlyInPanelService,
    private ilaSrvc: IlaService,
    private providerService: ProviderService,
    private alert : SweetAlertService,
    private deliveryMethodSrvc: DeliveryMethodeService,
    private topicService: TopicService
  ) {}

  ngOnInit(): void {
    if(this.providerCheck === false){
      if(this.activeCheck === true){
        this.getActiveILAs();
      }
      else if(this.unlinkedTopicCheck === true)
      {
          this.getILANotLinkedToTopics();
      }
      else{
        this.getDeliveryMethods().then(() => {
          if (this.draft_dev_check) this.getDraftILAs();
          else this.getPublishedILAs();
        });
  
      }
      
    }

    if(this.providerCheck === true){
      this.getNames();
    }
  }

  name:any;
  async getNames(){
    switch(this.moduleName){
      case 'Providers':
        this.name = "Providers";
        this.spinner = true;
        await this.providerService.getWihtoutIncludes().then((data)=>{
          
          this.ProviderDataSource = new MatTableDataSource(data);

        }).catch((err)=>{
          this.alert.errorToast('Error Fetching Provider Data');
        }).finally(()=>{
          this.spinner = false;
        });
        
    setTimeout(()=>{
      this.ProviderDataSource.sort = this.sort;
      this.ProviderDataSource.paginator = this.paginator;

    },1)
        break;

      case 'Topics':
        this.name = "Topics";
        this.spinner = true;
        await this.topicService.getAll().then((data)=>{
          
          this.TopicDataSource = new MatTableDataSource(data);
          if (data.length > 0) 
          {
            this.TopicDataSource.sort = this.providersort;
            this.TopicDataSource.paginator = this.paginator;
          }

        }).catch((err)=>{
          this.alert.errorToast('Error Fetching Topic Data');
        }).finally(()=>{
          this.spinner = false;
        });
        
      
    setTimeout(()=>{
      this.TopicDataSource.sort = this.providersort;
      this.TopicDataSource.paginator = this.paginator;

    },1)
        break;
    }

  }

  async getActiveILAs(){
    this.spinner = true;

    await this.ilaSrvc.getAllActive().then((res) => {
      this.DataSource = [];
      this.DataSource = res.map((item)=>{
        return{
          number:item.number,
          name:item.name,
          nickName:item.nickName,
          deliveryMethodName:item.deliveryMethodName
        }
      })
      this.TableDataSource = new MatTableDataSource(this.DataSource);
      if (this.DataSource.length > 0) {
        this.TableDataSource.sort = this.sort;
        this.TableDataSource.paginator = this.paginator;
      }
    }).finally(()=>{
      this.spinner = false;
    });;

    setTimeout(()=>{
      this.TableDataSource.sort = this.sort;
      this.TableDataSource.paginator = this.paginator;

    },1)
  }

  async getDeliveryMethods() {
    await this.deliveryMethodSrvc.getAll().then((res) => {
      this.deliverMethods = res;
    });
  }
  async getDraftILAs() {
    await this.ilaSrvc.getAllDraft().then((res) => {
      this.DataSource = [];
      this.DataSource = res.map((item)=>{
        return{
          number:item.number,
          name:item.name,
          nickName:item.nickName,
          deliveryMethodName:item.deliveryMethodName
        }
      })
      this.TableDataSource = new MatTableDataSource(this.DataSource);
      if (this.DataSource.length > 0) {
        this.TableDataSource.sort = this.sort;
        this.TableDataSource.paginator = this.paginator;
      }
    });
  }

  async getPublishedILAs() {
    await this.ilaSrvc.getAllPublished().then((res:any) => {
      this.DataSource = [];
      this.DataSource = res.map((item)=>{
        return{
          number:item.number,
          name:item.name,
          nickName:item.nickName,
          deliveryMethodName:item.deliveryMethodName
        }
      })
      this.TableDataSource = new MatTableDataSource(this.DataSource);
      if (this.DataSource.length > 0) {
        this.TableDataSource.sort = this.sort;
        this.TableDataSource.paginator = this.paginator;
      }
    });
  }

  sortProviderData(sort: Sort) {
    this.ProviderDataSource.sort = this.sort;
    const data = this.ProviderDataSource.data;
    if (!sort.active || sort.direction === '') {
      this.ProviderDataSource.data = data;
      return;
    }

    this.ProviderDataSource.data = data.sort((a, b) => {
      const isAsc = sort.direction === 'asc';
      switch (sort.active) {
        case 'No':
          return this.compare(a.No, b.No, isAsc);
        case 'Title':
          return this.compare(a.Title, b.Title, isAsc);
        default:
          return 0;
      }
    });
  }
  
  compare(a: number | string, b: number | string, isAsc: boolean) {
    return (a < b ? -1 : 1) * (isAsc ? 1 : -1);
  }

  sortTopicData(sort: Sort) {
    this.TopicDataSource.sort = this.sort;
    const data = this.TopicDataSource.data;
    if (!sort.active || sort.direction === '') {
      this.TopicDataSource.data = data;
      return;
    }

    this.TopicDataSource.data = data.sort((a, b) => {
      const isAsc = sort.direction === 'asc';
      switch (sort.active) {
        case 'No':
          return this.compare(a.No, b.No, isAsc);
        case 'Title':
          return this.compare(a.Title, b.Title, isAsc);
        default:
          return 0;
      }
    });
  }

  sortActiveILAsData(sort: Sort) {
    this.TableDataSource.sort = this.sort;
    const data = this.TableDataSource.data;
    if (!sort.active || sort.direction === '') {
      this.TableDataSource.data = data;
      return;
    }

    this.TableDataSource.data = data.sort((a, b) => {
      const isAsc = sort.direction === 'asc';
      switch (sort.active) {
        case 'ilaNo':
          return this.compare(a.ilaNo, b.ilaNo, isAsc);
        case 'ilaTitle':
          return this.compare(a.ilaTitle, b.ilaTitle, isAsc);

          case 'NickName':
          return this.compare(a.NickName, b.NickName, isAsc);
          case 'deliveryMethode':
            return this.compare(a.deliveryMethode, b.deliveryMethode, isAsc);
        default:
          return 0;
      }
    });
  }

  async getILANotLinkedToTopics() {
    await this.ilaSrvc.getILANotLinkedToTopic().then((res) => {
      this.DataSource = [];
      this.DataSource = res.map((item)=>{
        return{
          number:item.number,
          name:item.name,
          nickName:item.nickName,
          deliveryMethodName:item.deliveryMethodName
        }
      })
      this.TableDataSource = new MatTableDataSource(this.DataSource);
      if (this.DataSource.length > 0) {
        this.TableDataSource.sort = this.sort;
        this.TableDataSource.paginator = this.paginator;
      }
    });
  }

}
