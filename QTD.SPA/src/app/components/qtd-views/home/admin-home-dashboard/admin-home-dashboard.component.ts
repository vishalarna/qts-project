import { TemplatePortal } from '@angular/cdk/portal';
import { DatePipe } from '@angular/common';
import { Component, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { Router } from '@angular/router';
import { CertificationDataVM } from '@models/Certification/CertificationDataVM';
import { AdminMessageQTDUserUpdateOptions } from '@models/AdminMessage/AdminMessageQTDUserUpdateOptions';
import { AdminMessageVM } from '@models/AdminMessage/AdminMessageVM';
import { Store } from '@ngrx/store';
import { take } from 'rxjs/operators';
import { ScheduleClassesStats } from 'src/app/_DtoModels/SchedulesClassses/ScheduleClassesStats';
import { DateFormatPipe } from 'src/app/_Pipes/date-format.pipe';
import { TaskSortPipePipe } from 'src/app/_Pipes/task-sort-pipe.pipe';
import { AdminMessageService } from 'src/app/_Services/QTD/admin-message.service';
import { EmployeesService } from 'src/app/_Services/QTD/employees.service';
import { PublicClassScheduleRequestService } from 'src/app/_Services/QTD/public-class-schedule-request.service';
import { TaskRequalificationService } from 'src/app/_Services/QTD/task-requalification.service';
import { TrainingService } from 'src/app/_Services/QTD/training.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import {
  sideBarBackDrop,
  sideBarClose,
  sideBarDisableClose,
  sideBarToggle,
} from 'src/app/_Statemanagement/action/state.menutoggle';
import { cloneDeep } from 'lodash';
import { AdminMessageCreateOptions } from '@models/AdminMessage/AdminMessageCreateOptions';
import { jwtAuthHelper } from 'src/app/_Shared/Utils/jwtauth.helper';

@Component({
  selector: 'app-admin-home-dashboard',
  templateUrl: './admin-home-dashboard.component.html',
  styleUrls: ['./admin-home-dashboard.component.scss'],
})
export class AdminHomeDashboardComponent implements OnInit {
  isChecked: boolean;
  publicClassRequestStats:number;
  adminMessages: AdminMessageVM[];
  visibleMessages: AdminMessageVM[];
  constructor(
    private store: Store<any>,
    private _router: Router,
    private trainingSevc: TrainingService,
    private alert: SweetAlertService,
    private empService: EmployeesService,
    private taskReQualService: TaskRequalificationService,
    private _taskPipe: TaskSortPipePipe,
    private dataBroadcastService: DataBroadcastService,
    private publicClassScheduleRequestService: PublicClassScheduleRequestService,
    public flyPanelSrvc: FlyInPanelService,
    private vcf: ViewContainerRef,
    private adminMessageService: AdminMessageService
  ) {}

  dataSource = new MatTableDataSource<any>();
  dataSourceTq = new MatTableDataSource<any>();
  displayColumnsExpiringCertifications: string[] = [
    'EmployeeName',
    'Position',
    'CertificationType',
    'ExpirationDate',
  ];
  displayColumnsTaskQualifications: string[] = [
    'EmployeeName',
    'Evaluator',
    'taskNumber',
    'DueDate',
    'CriteriaMet',
    'ReQuaificatioStatus',
    'TotalRequiredTq'
  ];
  stats!: ScheduleClassesStats;
  datePipe = new DatePipe('en-us');
  userName: string = '';
  url = 'Dashboard';
  @ViewChild('certificationExpiredPaging') expiredCertPaging: MatPaginator;
  @ViewChild('taskQualPaging') taskQualPaging: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort: MatSort;
  // @ViewChild(MatSort) sort: MatSort;
  appliedCertificationFilters:any[];
  cerificationData:CertificationDataVM[] = [];
  originalCerificationData:CertificationDataVM[] = [];
  readonly LOCAL_STORAGE_KEY = 'certificateFilters';
  messageLoader:boolean = false;
  ngOnInit(): void {
    this.loadFiltersFromLocalStorage();
    this.getStatistics();
    this.getexpiredCertifications();
    this.getTaskQualifications();
    this.getUserName();
    this.loadAdminMessage();
    this.dataBroadcastService.publicClassEnabled.subscribe((x) => {
      this.isChecked = x;
    })
  }
  
  async loadAdminMessage(){
    this.messageLoader = true;
    await this.postAdminMessages();
    await this.getAdminMessages();
    this.messageLoader = false;
  }
  async getStatistics() {
    this.trainingSevc
      .GetStats()
      .then((res) => {
        this.stats = res;
      })
      .catch((res: any) => {
        this.alert.errorToast(res);
      });
    this.publicClassRequestStats = await this.publicClassScheduleRequestService.getRequestStatsAsync();
  }

  async NavigateToEdit(empId: any) {
    this._router.navigate(['/implementation/employee/edit/' + empId], {
      queryParams: { navigate: 'certifications' },
    });
  }

  async getTaskQualifications() {
    this.taskReQualService
      .getRecentTaskQuals()
      .then((res) => {
        this.dataSourceTq = new MatTableDataSource<any>(res);
        setTimeout(() => {
          this.dataSourceTq.paginator = this.taskQualPaging;
        }, 1);
      })

      .catch((res: any) => {
        this.alert.errorToast(res);
      });
  }
  async getexpiredCertifications() {
    this.isLoadingCertifications = true;
    this.empService
      .getEmployeesExpiredCertifications()
      .then((res) => {
        this.cerificationData = res;
        this.originalCerificationData = cloneDeep(this.cerificationData);
        //this.dataSource = new MatTableDataSource<any>(res);
        this.isLoadingCertifications = false;
        this.dataSource.data = res;
        setTimeout(() => {
          this.dataSource.paginator = this.expiredCertPaging;
          // this.dataSource.sort = this.sort;
        }, 1);
      })
      .catch((res: any) => {
        this.alert.errorToast(res);
      })
      .finally(() => {
        this.isLoadingCertifications = false;
        this.applyFilters();
      });
  }

  isLoading: boolean = false;
  isLoadingCertifications: boolean = false;
  isLoadingTQ: boolean = false;

  hideBar() {
    this.store
      .select('freezeMenu')
      .pipe(take(1))
      .subscribe((data: boolean) => {
        if (!data) {
          this.store.dispatch(sideBarToggle());
        }
      });
  }

  selfRegistration() {
    this.store.dispatch(sideBarClose());
    this.store.dispatch(sideBarBackDrop({ backdrop: false }));
    this.store.dispatch(sideBarDisableClose({ disableClose: false }));
    this._router.navigate(['/implementation/sc/self-registration']);
  }

  waitList() {
    this.store.dispatch(sideBarClose());
    this.store.dispatch(sideBarBackDrop({ backdrop: false }));
    this.store.dispatch(sideBarDisableClose({ disableClose: false }));
    this._router.navigate(['/implementation/sc/waitlist']);
  }
  openPublicRequest(){
    this._router.navigate(['/home/public-request']);
  }

  openClassSchedules() {
    this._router.navigate(['/implementation/sc/overview']);
  }
  async getUserName() {
    this.userName = await this.empService.getUserName();
  }

  sortExpirationData(sort: Sort) {
    this.dataSource.sort = this.sort;
    const data = this.dataSource.data;
    if (!sort.active || sort.direction === '') {
      this.dataSource.data = data;
      return;
    }

    this.dataSource.data = data.sort((a, b) => {
      const isAsc = sort.direction === 'asc';
      switch (sort.active) {
        case 'EmployeeName':
          return this.compare(a.EmployeeName, b.EmployeeName, isAsc);
        case 'Position':
          return this.compare(a.Position, b.Position, isAsc);
        case 'CertificationType':
          return this.compare(a.CertificationType, b.CertificationType, isAsc);
        case 'ExpirationDate':
          return this.compare(a.ExpirationDate, b.ExpirationDate, isAsc);
        default:
          return 0;
      }
    });
  }

  compare(a: number | string, b: number | string, isAsc: boolean) {
    return (a < b ? -1 : 1) * (isAsc ? 1 : -1);
  }

  sortTQData(sort: Sort) {
    this.dataSourceTq.sort = this.sort;
    const data = this.dataSourceTq.data;
    if(sort.active=='taskNumber' && sort.direction !== ''){
      this.dataSourceTq.data = (this._taskPipe.transform(data, sort.direction,'number')).data;
    }
    if (!sort.active || sort.direction === '') {
      this.dataSourceTq.data = data;
      return;
    }
    this.dataSourceTq.data = data.sort((a, b) => {
      const isAsc = sort.direction === 'asc';
      switch (sort.active) {
        case 'EmployeeName':
          return this.compare(a.EmployeeName, b.EmployeeName, isAsc);
        case 'Evaluator':
          return this.compare(a.Evaluator, b.Evaluator, isAsc);
        case 'DueDate':
          return this.compare(a.DueDate, b.DueDate, isAsc);
        case 'CriteriaMet':
          return this.compare(a.CriteriaMet, b.CriteriaMet, isAsc);
          case 'ReQuaificatioStatus':
            return this.compare(a.ReQuaificatioStatus, b.ReQuaificatioStatus, isAsc);
            case 'TotalRequiredTq':
              return this.compare(a.TotalRequiredTq, b.TotalRequiredTq, isAsc);

        default:
          return 0;
      }
    });
  }

  async getAdminMessages(){
    this.adminMessages = await this.adminMessageService.getAdminMessagesAsync();
    this.visibleMessages = this.adminMessages;
  }

  async handleDismiss(messageId: string){
    this.visibleMessages = this.visibleMessages?.filter(m => m.id !=messageId);
    let adminMessageQTDUserUpdateOption: AdminMessageQTDUserUpdateOptions = {
      adminMessage_QTDUserId: messageId
    }
    await this.adminMessageService.updateAdminMessageQTDUserAsync(adminMessageQTDUserUpdateOption);
  }

    openFlyInPanel(templateRef: any) {
        const portal = new TemplatePortal(templateRef, this.vcf);
        this.flyPanelSrvc.open(portal);
    }

    getAppliedFilter(filters:any[]){
      this.appliedCertificationFilters = filters;
      localStorage.setItem(this.LOCAL_STORAGE_KEY, JSON.stringify(this.appliedCertificationFilters));
      this.applyFilters();
    }

    applyFilters() {
      if (this.appliedCertificationFilters.includes("All") && this.appliedCertificationFilters.length == 1) {
        this.cerificationData = [...this.originalCerificationData];
        this.dataSource.data = this.cerificationData;
        return;
      }
    
      const today = new Date();
      const sixMonthsFromNow = new Date(today);
      sixMonthsFromNow.setMonth(today.getMonth() + 6);
    
      this.cerificationData = this.originalCerificationData.filter(cert => {
        const expDate = new Date(cert.expirationDate);
        const expirationPlusOneYear = new Date(expDate);
        expirationPlusOneYear.setFullYear(expDate.getFullYear() + 1);
    
        return this.appliedCertificationFilters.some(filter => {
          switch (filter) {
            case "Pending Expiration":
              return expDate >= today && expDate <= sixMonthsFromNow;
    
            case "Suspended":
              return cert.certificationType === "NERC"
                && today > expDate
                && today <= expirationPlusOneYear;
    
            case "Expired":
              if (cert.certificationType === "NERC") {
                return today > expirationPlusOneYear;
              } else {
                return today > expDate;
              }
    
            default:
              return true;
          }
        });
      });
      this.dataSource.data = this.cerificationData;
    }
    

    loadFiltersFromLocalStorage() {
      const stored = localStorage.getItem(this.LOCAL_STORAGE_KEY);
      if (stored) {
        this.appliedCertificationFilters = JSON.parse(stored);
      } else {
        this.appliedCertificationFilters = ["All"];
      }
    }

    clearFilters() {
      this.appliedCertificationFilters = ["All"];
      localStorage.setItem(this.LOCAL_STORAGE_KEY, JSON.stringify(this.appliedCertificationFilters));
      this.applyFilters();
    }

    getCertStatus(row): string {
      const today = new Date();
      const expirationDate = new Date(row.expirationDate);
      const expirationPlusOneYear = new Date(expirationDate);
      expirationPlusOneYear.setFullYear(expirationDate.getFullYear() + 1);
    
      if (row.certificationType === "NERC") {
        if (today <= expirationDate) {
          return "";
        } else if (today > expirationDate && today <= expirationPlusOneYear) {
          return "Suspended";
        } else if (today > expirationPlusOneYear) {
          return "Expired";
        }
      } else {
        if (today > expirationDate) {
          return "Expired";
        }
      }
      return ""; 
    }

    async postAdminMessages(){
    let messageCreateOption:AdminMessageCreateOptions={
              username: jwtAuthHelper.LoggedInUser,
              instance: jwtAuthHelper.SelectedInstance
            }
      await this.adminMessageService.createAdminMessageAsync(messageCreateOption);
  }
    
}
