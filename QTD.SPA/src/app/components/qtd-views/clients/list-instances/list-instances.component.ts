import {
  AfterViewInit,
  Component,
  OnDestroy,
  OnInit,
  ViewChild,
} from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { TranslateService } from '@ngx-translate/core';
import { DataTableDirective } from 'angular-datatables';
import { Subject } from 'rxjs';
import { ModifyTokenModel } from 'src/app/_DtoModels/Auth/ModifyTokenModel';
import { Instance } from 'src/app/_DtoModels/Instance/Instance';
import { AuthService } from 'src/app/_Services/Auth/auth.service';
import { ClientService } from 'src/app/_Services/Auth/client.service';
import { InstanceService } from 'src/app/_Services/Auth/instance.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { CustomClaimTypes } from 'src/app/_Shared/Utils/CustomClaims';
import { jwtAuthHelper } from 'src/app/_Shared/Utils/jwtauth.helper';
import { AddEditClientComponent } from '../list-clients/add-edit-client/add-edit-client.component';
import { AddEditInstanceComponent } from './add-edit-instance/add-edit-instance.component';
import { DatePipe } from '@angular/common';
import { LicenseHelperService } from 'src/app/_Shared/services/licenseHelper.service';
import { DateFormatPipe } from 'src/app/_Pipes/date-format.pipe';
import { ApiClientSettingsService } from 'src/app/_Services/QTD/ClientSettings/api.clientsettings.service';

@Component({
  selector: 'app-list-instances',
  templateUrl: './list-instances.component.html',
  styleUrls: ['./list-instances.component.scss'],
})
export class ListInstancesComponent implements OnInit {
  instances: Instance[] = [];
  clientInstance: Instance[] = [];
  selectClientInstance: boolean = false;
  retunUrl: string = '';
  clientName: string = '';

  instanceDataSource: MatTableDataSource<any>;
  displayedColumns: string[] = ['name', 'db_v', 'active', 'obj'];
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  datePipe = new DatePipe('en-us');
  dateFormatPipe=new DateFormatPipe(this.clientSettingsService);
  constructor(
    private translate: TranslateService,
    private instanceSrvc: InstanceService,
    private dialog: MatDialog,
    private alert: SweetAlertService,
    private authService: AuthService,
    private databroadcastSrvc: DataBroadcastService,
    private activateRoute: ActivatedRoute,
    private route: Router,
    private client: ClientService,
    private licenseHelperService:LicenseHelperService,
    public clientSettingsService: ApiClientSettingsService
  ) {

    const browserLang = localStorage.getItem('lang') ?? 'en';
    this.translate.use(browserLang);

    this.clientName = this.route.url.substring(
      this.route.url.lastIndexOf('/') + 1
    );

    /*
    this.selectClientInstance =
      jwtAuthHelper.unPackJWTToken[CustomClaimTypes.InstanceName] == undefined
        ? true
        : false;

    if (!this.selectClientInstance) this.getClientsInstances();
    else {
      this.getUserClientInstances();
    }
    */
    this.getClientsInstances();
    this.retunUrl = this.activateRoute.snapshot.queryParams.returnUrl;
  }

  ngOnInit(): void {
    this.databroadcastSrvc.selectInstance.subscribe((val) => {
      this.selectClientInstance = val == undefined;
      this.instances = [];
      if (!this.selectClientInstance) this.getClientsInstances();
      else {
        this.getUserClientInstances();
      }
    });
  }

  //Get the all instances assigned to a logged in user
  async getUserClientInstances() {
    await this.client
      .getUserClientInstances(jwtAuthHelper.LoggedInUser)
      .then((res) => {
        this.clientInstance = [];
        this.clientInstance = res;
      });
  }

  async getClientsInstances() {
    await this.client.getClientInstances(this.clientName).then((res) => {
      this.instances = [];
      this.instances = res;
      let tempSrc: any[] = [];
      this.instances?.forEach((i) => {
        tempSrc.push({
          name: i.name,
          db_v: 'v 1.0',
          active: i.active,
          obj: i,
        });
      });
      this.instanceDataSource = new MatTableDataSource(tempSrc);
      this.instanceDataSource.sort = this.sort;
      this.instanceDataSource.paginator = this.paginator;
    });
  }

  open(name?: any) {
    const dialogRef = this.dialog.open(AddEditInstanceComponent, {
      hasBackdrop: true,
      disableClose: true,
    });

    if (name) {
      dialogRef.componentInstance.editID = name;
    }
    dialogRef.componentInstance.clientName = this.clientName;
    dialogRef.afterClosed().subscribe((result) => {

      this.getClientsInstances();
    });
  }

  delete(name: string) {
    this.alert.confirmAlert('You want to delete?').then(async (result) => {
      if (result.isConfirmed) {
        await this.instanceSrvc.delete(name).then(
          (res) => this.alert.successToast(this.translate.instant('L.' + res)),

          () => {
            this.getClientsInstances();
          }
        );
      }
    });
  }

  async setClient(instance: string) {


    let model: ModifyTokenModel = {
      setInstanceName: instance,
      setUserName: null,
      username: jwtAuthHelper.LoggedInUser,
    };
    //
    await this.authService.modifyToken(model).then(async (res) => {
      if (res) {

        var licenseSettingData =await this.licenseHelperService.setLicenseData();
        if (licenseSettingData != null) {
          var licenseActiveStatus = licenseSettingData.active
          var licenseexpiration = this.datePipe.transform(licenseSettingData.expiration, "yyyy-MM-dd")
          var todaysDate = this.datePipe.transform(Date.now(), "yyyy-MM-dd");

          if (licenseActiveStatus === true && licenseexpiration > todaysDate) {
              localStorage.removeItem('dateFormat');
              this.dateFormatPipe.transform(new Date());
            this.alert.successToast(`${instance} Selected`);
            if (this.retunUrl) this.route.navigate([this.retunUrl]);
            // else
            //this.databroadcastSrvc.selectInstance.next(instance);
            //this.route.navigate(['/home/dashboard'])
          }
          else {
            let model: ModifyTokenModel = {
              setInstanceName: '',
              setUserName: null,
              username: jwtAuthHelper.LoggedInUser,
            };
            //
            await this.authService.modifyToken(model).then((res) => {
              if (res) {

                this.alert.errorAlert("Your license is expired. Please Contact Administrator")
              }
            });

          }
        }
        else {
          let model: ModifyTokenModel = {
            setInstanceName: '',
            setUserName: null,
            username: jwtAuthHelper.LoggedInUser,
          };
          //
          await this.authService.modifyToken(model).then((res) => {
            if (res) {

              this.alert.errorAlert("Unable to select instance . You do not have activated License. Please Contact Administrator")
            }
          });
        }

      }
    });
  }

  changeClientInstance() {
    this.databroadcastSrvc.selectInstance.next(undefined);
  }

  filterData(e: Event) {
    let filter = (e.target as HTMLInputElement).value;
    this.instanceDataSource.filter = filter;
  }
}
