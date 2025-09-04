import { TemplatePortal } from '@angular/cdk/portal';
import {
  Component,
  Input,
  OnInit,
  ViewChild,
  ViewContainerRef,
} from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { ActivatedRoute } from '@angular/router';
import { IDPReviewUpdateOptions } from 'src/app/_DtoModels/Employee/IDPReviewUpdateOptions';
import { IDPVM } from 'src/app/_DtoModels/IDP/IDPVM';
import { TrainingStudentCreationOptions } from 'src/app/_DtoModels/SchedulesClassses/training-creation-options';
import { EmployeesService } from 'src/app/_Services/QTD/employees.service';
import { IdpService } from 'src/app/_Services/QTD/idp.service';
import { IlaService } from 'src/app/_Services/QTD/ila.service';
import { TrainingService } from 'src/app/_Services/QTD/training.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';
import {
  animate,
  state,
  style,
  transition,
  trigger,
} from '@angular/animations';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-idp-detail',
  templateUrl: './idp-detail.component.html',
  styleUrls: ['./idp-detail.component.scss'],
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
export class IdpDetailComponent implements OnInit {
  displayedColumns: string[] = [
    'ilaNumber',
    'ilaTitle',
    'ilaStatus',
    'deliveryMethod',
    'testLinked',
    'cbtLinked',
    'selfRegistrationEnabled',
    'hasEnrolledInAnyClass',
    'id',
  ];
  displayedExpandedColumns: string[] = ['plannedDate', 'startDate', 'endDate','grade','score','id',];
  dataSource = new MatTableDataSource<IDPVM>();
  selectedIDPvm: IDPVM;
  yearnow = new Date().getFullYear();
  range: number[] = [];
  panelOpenState = true;
  selected = this.yearnow;
  empId = '';
  toggle = false;
  selectedILDId: any;
  subscription = new SubSink();
  showSpinner: boolean = false;
  filterOptions: any = null;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort: MatSort;

  LinkedIds: any[] = [];
  @Input() employeeName: any;
  @Input() idpInformation?: string;
  expandedData: any | null;
  expandedTableDataSource: MatTableDataSource<any> | undefined;
  expandedDataSource: any[] | undefined = [];
  tempILAId:any = "";
  datePipe = new DatePipe('en-us');

  constructor(
    public flyPanelSrvc: FlyInPanelService,
    private vcf: ViewContainerRef,
    public dialog: MatDialog,
    private ilaService: IlaService,
    private idpService: IdpService,
    private route: ActivatedRoute,
    private alert: SweetAlertService,
    public trService: TrainingService,
    private empService: EmployeesService,
    private labelPipe:LabelReplacementPipe
  ) {}

  unlinkDescription: string;
  sortCol = 'title';
  sortOrder = 'asc';

  ngOnInit(): void {
    this.getyearsdropdown();
    this.subscription.sink = this.route.params.subscribe((res: any) => {
      this.empId = res.id;
      this.readyData();
    });
  }

  async FilterEmployeesEvent(filterData: any) {
    this.filterOptions = filterData;
    let data = await this.idpService.getAllIdps(this.empId, this.selected);
    this.LinkedIds = data.map((x) => {
      return x.ilaId;
    });
    const selectedYear = new Date(this.selected).getFullYear();

    data = data.filter(x => {
      const idpYearYear = new Date(x.idpYear).getFullYear();
      return idpYearYear === selectedYear;
    });

   // data = data.filter((x) => x.idpYear === selectedYear);
    

    if (this.filterOptions !== null) {
      //filter by active
      if (filterData.status !== null && filterData.status !== undefined)
        data = data.filter((x) => x.active == filterData.status);

    //filter by planedDateEnd  //filter by planedDateStart
          if (
            filterData.planedDateStart !== null &&
            filterData.planedDateStart !== undefined &&
            filterData.planedDateEnd !== null &&
            filterData.planedDateEnd !== undefined
        ) {
            const startDate = new Date(filterData.planedDateStart).setHours(0, 0, 0, 0);
            const endDate = new Date(filterData.planedDateEnd).setHours(23, 59, 59, 999);
        
            data = data.filter((x) => {
                const plannedDate = x.plannedDate ? new Date(x.plannedDate).setHours(0, 0, 0, 0) : null;
                return plannedDate !== null && plannedDate >= startDate && plannedDate <= endDate;
            });
        }
        
    
      // //filter by startDateStart
      if (
        filterData.startDateStart !== null &&
        filterData.startDateStart !== undefined
      )
        data = data.filter(
          (x) =>
            new Date(x.startDate).setHours(0, 0, 0, 0) >=
              new Date(filterData.startDateStart).setHours(0, 0, 0, 0) ||
            x.classScheduleId === null ||
            x.classScheduleId === ''
        );
      //filter by startDateEnd
      if (
        filterData.startDateEnd !== null &&
        filterData.startDateEnd !== undefined
      )
        data = data.filter(
          (x) =>
            new Date(x.startDate).setHours(0, 0, 0, 0) <=
              new Date(filterData.startDateEnd).setHours(0, 0, 0, 0) ||
            x.classScheduleId === null ||
            x.classScheduleId === ''
        );

       //filter by endDateStart
      if (
        filterData.endDateStart !== null &&
        filterData.endDateStart !== undefined
      )
        data = data.filter(
          (x) =>
            new Date(x.endDate).setHours(0, 0, 0, 0) >=
              new Date(filterData.endDateStart).setHours(0, 0, 0, 0) ||
            x.classScheduleId === null ||
            x.classScheduleId === ''
        );
      //filter by endDateEnd`
      if (filterData.endDateEnd !== null && filterData.endDateEnd !== undefined)
        data = data.filter(
          (x) =>
            new Date(x.endDate).setHours(0, 0, 0, 0) <=
              new Date(filterData.endDateEnd).setHours(0, 0, 0, 0) ||
            x.classScheduleId === null ||
            x.classScheduleId === ''
        );

      //filter by Grade
      let gradeList = filterData.GradeSelected;
      if (gradeList.find((x) => x === 'No')) {
        data = data.filter(
          (x) =>
            x.grade === null ||
            x.grade === undefined ||
            x.grade === '' ||
            x.classScheduleId === null ||
            x.classScheduleId === ''
        );
      } else if (!gradeList.find((x) => x === 'All')) {
        data = data.filter(
          (x) =>
            gradeList.find((y) => y === x.grade) ||
            x.classScheduleId === null ||
            x.classScheduleId === ''
        );
      }

      // //filter by selfRegister
      if (filterData.SelfRegisteredEnabled !== null)
        data = data.filter(
          (x) =>
            x.selfRegister === filterData.SelfRegisteredEnabled ||
            x.classScheduleId === null ||
            x.classScheduleId === ''
        );
    }
    this.dataSource.data = data;

    this.dataSource.paginator = this.paginator;
    setTimeout(() => {
      this.dataSource.sort = this.sort;
    }, 1000);
  }

  async getLinkedSchedulingClasses(id: any) {
    this.expandedDataSource = undefined;
    this.expandedTableDataSource = undefined;
    await this.idpService
      .getAllLinkedSchedulingClassesIdps(id, this.empId)
      .then(async (res) => {
        this.expandedDataSource = res.map((item)=>{
        return{...item, plannedDateLocal: item.plannedDate ? this.covertUtcToLocalTime(item.plannedDate) : null, startDateLocal: item.startDate ? this.covertUtcToLocalTime(item.startDate) : null, completionDateLocal: item.completionDate ? this.covertUtcToLocalTime(item.completionDate) : null,}});
        if (this.expandedTableDataSource) {
          this.expandedTableDataSource.data = this.expandedDataSource;
        } else {
          this.expandedTableDataSource = new MatTableDataSource(
            this.expandedDataSource
          );
        }
      });
  }
 
  sortInnerColumn(col: string) {
    this.sortCol = col;
    this.sortOrder = this.sortOrder === 'asc' ? 'desc' : 'asc';
  }

  async readyData() {
    this.FilterEmployeesEvent(this.filterOptions);
  }

  getyearsdropdown() {
    var i = 0;
    while (this.range[this.range.length - 1] !== 2005) {
      this.range.push(this.yearnow + 1 - i);
      i++;
    }
  }

  async openFlyInPanel(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelSrvc.open(portal);
  }

  async openFlyInPanelObjectives(templateRef: any, id: any) {
    this.selectedILDId = id;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelSrvc.open(portal);
  }

  refresh() {
    this.readyData();
  }

  async unlinkEmployee(templateRef: any) {
    this.unlinkDescription = `You are selecting to <b>Unenroll</b> ` + await this.labelPipe.transform('Employee') + `, <b>${this.employeeName}</b>, from the selected ` + await this.labelPipe.transform('ILA') +`, <b>${this.selectedIDPvm.ilaTitle}</b>.This will remove the ` + await this.labelPipe.transform('ILA') +` from the ` + await this.labelPipe.transform('Employee') + `’s IDP and the Class Roster.  If a grade and/or score were awarded, it would also be removed.`;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  async SelectedYearValueChanged(event: any) {
    this.selected = event;
    this.FilterEmployeesEvent(this.filterOptions);
  }

  async Unlink(e: any) {
    this.showSpinner = true;
    var options = new TrainingStudentCreationOptions();
    options.employeeIds = [];
    options.employeeIds.push(this.selectedIDPvm.empId);
    await this.trService
      .unLinkedEmployees(this.selectedIDPvm.classScheduleId, options)
      .then(async (_) => {
        this.alert.successToast( await this.labelPipe.transform('Employee') + ' Unlinked Successfully.');
        await this.readyData();
        this.showSpinner = false;
      });
    // let data=await this.ilaService.UnEnrollEmployeeIDP(this.selectedIDPvm.ilaId,this.selectedIDPvm.empId);
    //     this.alert.successToast("Employee Unlinked Successfully.");
    //     await this.readyData();
    //   this.showSpinner=false;
  }

  saveSpinner = false;

  async saveIDPInfo() {
    this.saveSpinner = true;
    var options = new IDPReviewUpdateOptions();
    options.idpReviewInformation = this.idpInformation;
    await this.empService
      .updateIDPInfo(this.empId, options)
      .then(async (_) => {
        this.alert.successToast(await this.labelPipe.transform('Employee') + ' Specific IDP Information Updated');
      })
      .finally(() => {
        this.saveSpinner = false;
      });
  }

  async deleteAsync(id: string){
    await this.idpService.deleteAsync(id).then((_) => {
      this.alert.successAlert('IDP deleted successfully');
      this.readyData();
    })
  }

  covertUtcToLocalTime(datetime: any): Date {
    var startDateString = this.datePipe.transform( datetime, 'yyyy-MM-dd hh:mm a');
    const utcStartDateTime = new Date(startDateString.toString() + ' UTC');
    const localstartDateTimeString = utcStartDateTime.toLocaleString();
    var newdatetime = new Date(Date.parse(localstartDateTimeString));
    return newdatetime;
  }
  
}

export interface IDP {
  ILANum: string;
  ILATitle: string;
  deliverymethod: string;
  plannedDate: string;
  startDate: string;
  endDate: string;
  grade: string;
  score: string;
  testLinked: string;
  cbtLinked: string;
  selfRegistrationEnabled: string;
}
