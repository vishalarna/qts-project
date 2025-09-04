import { DatePipe } from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { Router } from '@angular/router';
import { ModifyTokenModel } from '@models/Auth/ModifyTokenModel';
import { Client } from '@models/Client/ClientViewModel';
import { Instance } from '@models/Instance/Instance';
import { DateFormatPipe } from 'src/app/_Pipes/date-format.pipe';
import { AuthService } from 'src/app/_Services/Auth/auth.service';
import { ClientService } from 'src/app/_Services/Auth/client.service';
import { ApiClientSettingsService } from 'src/app/_Services/QTD/ClientSettings/api.clientsettings.service';
import { jwtAuthHelper } from 'src/app/_Shared/Utils/jwtauth.helper';
import { LicenseHelperService } from 'src/app/_Shared/services/licenseHelper.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import {cloneDeep} from 'lodash';

@Component({
  selector: 'app-instance-selection',
  templateUrl: './instance-selection.component.html',
  styleUrls: ['./instance-selection.component.scss'],
})
export class InstanceSelectionComponent implements OnInit {
  returnUrl: string = '';
  clientDataSource: MatTableDataSource<Client>;
  tempDataSource: Client[] | undefined = [];
  clientDisplayColumn: string[] = ['name', 'obj'];
  instanceDataSource: MatTableDataSource<Instance>;
  instanceDisplayColumns: string[] = ['name', 'obj'];
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  expandedData: any | null =null;
  datePipe = new DatePipe('en-us');
  dateFormatPipe = new DateFormatPipe(this.clientSettingsService);
  searchString: string = '';
  isAdminUser: boolean;

  constructor(
    private _client: ClientService,
    private alert: SweetAlertService,
    private route: Router,
    private authService: AuthService,
    private licenseHelperService: LicenseHelperService,
    public clientSettingsService: ApiClientSettingsService,
  ) {}

  ngOnInit(): void {
    this.isAdminUser = jwtAuthHelper.IsAdminUser;
    this.getClients();
  }

  async getClients() {
    await this._client.getClients().then((res) => {
      this.tempDataSource = res;
      this.clientDataSource = new MatTableDataSource(res);
      this.clientDataSource.sort = this.sort;
      this.clientDataSource.data =  this.clientDataSource.data.sort((a,b)=> a?.name.localeCompare(b?.name));
      this.clientDataSource.paginator = this.paginator;
    });
  }

  getClientsInstances(client: Client) {
       this.instanceDataSource = new MatTableDataSource(client.instances);
       this.instanceDataSource.sort = this.sort;
       this.instanceDataSource.paginator = this.paginator;
  }

  async setClient(instance: string) {
    let model: ModifyTokenModel = {
      setInstanceName: instance,
      setUserName: null,
      username: jwtAuthHelper.LoggedInUser,
    };
    await this.authService.modifyToken(model).then(async (res) => {
      if (res) {
        var licenseSettingData =
          await this.licenseHelperService.setLicenseData();
        if (licenseSettingData != null) {
          var licenseActiveStatus = licenseSettingData.active;
          var licenseexpiration = this.datePipe.transform( licenseSettingData.expiration, 'yyyy-MM-dd' );
          var todaysDate = this.datePipe.transform(Date.now(), 'yyyy-MM-dd');

          if (licenseActiveStatus === true && licenseexpiration > todaysDate) {
            localStorage.removeItem('dateFormat');
            this.dateFormatPipe.transform(new Date());
            this.alert.successToast(`${instance} Selected`);
            if (this.returnUrl) this.route.navigate([this.returnUrl]);
          } else {
            this.alert.errorAlert('Your license is expired. Please Contact Administrator');
          }
        } else {
          let model: ModifyTokenModel = {
            setInstanceName: '',
            setUserName: null,
            username: jwtAuthHelper.LoggedInUser,
          };

          await this.authService.modifyToken(model).then((res) => {
            if (res) {
              this.alert.errorAlert('Unable to select instance . You do not have activated License. Please Contact Administrator' );
            }
          });
        }
      }
    });
  }

  openClientWizard(id:any){
    const queryParams = {
      clientId:id,
    };
    this.route.navigate(['admin/instance-setup'], { queryParams });
  }

  openInstanceWizard(row:any){
    const queryParams = {
      instanceId:row.id,
      clientId:row.clientId
    };
    this.route.navigate(['admin/instance-setup'], { queryParams });
  }
  
  inputSearchString(event: any) {
    this.searchString = event.target.value;
    this.filterData();
  }

  filterData() {
    const searchTerm = this.searchString.toLowerCase();
    var filteredData = cloneDeep(this.tempDataSource);
    filteredData.forEach(x=>x.instances = x.instances.filter(instance => {
      return instance.name?.toLowerCase().includes(searchTerm);
    }));
    filteredData = filteredData.filter(x=>x.instances.length > 0);
    this.clientDataSource.data = filteredData;
  }

  clearSearchString(){
    this.searchString = "";
    this.filterData();
  }

}
