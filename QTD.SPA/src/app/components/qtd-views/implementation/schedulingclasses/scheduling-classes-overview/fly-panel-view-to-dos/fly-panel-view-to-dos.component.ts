import { animate, state, style, transition, trigger } from '@angular/animations';
import { SelectionModel } from '@angular/cdk/collections';
import { DatePipe } from '@angular/common';
import { ChangeDetectorRef, Component, OnInit, QueryList, ViewChild, ViewChildren } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTable as MatTable, MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { ILA } from 'src/app/_DtoModels/ILA/ILA';
import { ClassScheduleEnrollOptions } from 'src/app/_DtoModels/SchedulesClassses/ClassScheduleEnrollOptions';
import { ClassSchedule_Employee } from 'src/app/_DtoModels/SchedulesClassses/ClassSchedule_Employee';
import { ClassSchedules } from 'src/app/_DtoModels/SchedulesClassses/ClassSchedules';
import { ClassRoasterUpdateOptions } from 'src/app/_DtoModels/SchedulesClassses/Rosters/ClassRosterUpdateOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { RostersService } from 'src/app/_Services/QTD/rosters.service';
import { TrainingService } from 'src/app/_Services/QTD/training.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { sideBarClose } from 'src/app/_Statemanagement/action/state.menutoggle';

@Component({
  selector: 'app-fly-panel-view-to-dos',
  templateUrl: './fly-panel-view-to-dos.component.html',
  styleUrls: ['./fly-panel-view-to-dos.component.scss'],
  animations: [
    trigger("detailExpand", [
      state("collapsed", style({ height: "0px", minHeight: "0" })),
      state("expanded", style({ height: "*" })),
      transition(
        "expanded <=> collapsed",
        animate("225ms cubic-bezier(0.4, 0.0, 0.2, 1)")
      )
    ])
  ]
})
export class FlyPanelViewToDosComponent implements OnInit {
  currentIndex: number = 0;
  @ViewChild('outerSort', { static: true }) sort: MatSort;
  @ViewChildren('innerSort') innerSort: QueryList<MatSort>;
  @ViewChildren('subSort') subSort: QueryList<MatSort>;
  @ViewChildren('innerTables') innerTables: QueryList<MatTable<Address>>;
  @ViewChildren('subTables') subTables: QueryList<MatTable<Block>>;

  dataSource: MatTableDataSource<any> = new MatTableDataSource();
  usersData: User[] = [];
  columnsToDisplay = ['select', 'ILA No.', 'Title', 'action'];
  innerDisplayedColumns = ['select', 'Class Date', 'Time', 'Location', 'Instructor', 'Seats Available'];
  subBlockDisplayedColumns = ['select', 'First Name', 'Last Name', 'Position', 'Action'];
  expandedElement: ModifiedILA | null;
  expandedSubElement: ModifiedCS | null;
  selection = new SelectionModel<ILA>(true, []);
  title = "";
  isILA = false;
  type!: 'self' | 're' | 'ila' | 'wait';
  loader = false;
  ilas!: ILA[];
  showEnrollButtons = false;
  showReleaseButton = false;
  showRetakeButton = false;
  isWaitlistPage:boolean=false;

  constructor(
    private cd: ChangeDetectorRef,
    private router: Router,
    private trService: TrainingService,
    private alert: SweetAlertService,
    private store: Store,
    private rosterService : RostersService,
    private labelPipe:LabelReplacementPipe
  ) {
  }

  ngOnInit() {
    this.store.dispatch(sideBarClose());
    this.readyType();
  }
  doShowMainExpandable = false;

  async readyType() {
    this.expandedElement = null;
    this.expandedSubElement = null;
    this.loader = true;
    switch (this.router.url) {
      case '/implementation/sc/self-registration':
        this.title = "Self-Registrations needing Approval";
        this.columnsToDisplay = ['ILA No.', 'Title'];
        this.innerDisplayedColumns = ['Class Date', 'Time', 'Location', 'Instructor', 'Seats Available'];
        this.subBlockDisplayedColumns = ['First Name', 'Last Name', 'Position', 'Action'];
        this.showEnrollButtons = true;
        this.showReleaseButton = false;
        this.showRetakeButton = false;
        this.doShowMainExpandable = true;
        this.readySelfRegData();
        break;
      case '/implementation/sc/re-release':
        this.loader = true;
        this.title = "Paused Tests needing to be Re-released";
        this.columnsToDisplay = ['ILA No.', 'Title'];
        this.innerDisplayedColumns = ['Class Date', 'Time', 'Location', 'Instructor','Seats Available'];
        this.subBlockDisplayedColumns = ['Test ID','First Name', 'Last Name', 'Action'];
        this.showEnrollButtons = false;
        this.showReleaseButton = true;
        this.showRetakeButton = false;
        this.doShowMainExpandable = true;
        this.readyTestToReRelease();
        //this.loader = false;
        break;
      case '/implementation/sc/ila-classes':
        this.isILA = true;
        this.title = await this.labelPipe.transform('ILA') +"s to be Scheduled";
        this.columnsToDisplay = ['ILA No.', 'Title'];
        this.innerDisplayedColumns = [];
        this.subBlockDisplayedColumns = [];
        this.showEnrollButtons = true;
        this.showReleaseButton = false;
        this.showRetakeButton = false;
        this.doShowMainExpandable = false;
        this.readyILAtoScheduleData();
        break;

      case '/implementation/sc/waitlist':
        this.isILA = false;
        this.title = "Manage Waitlist";
        this.columnsToDisplay = ['ILA No.', 'Title'];
        this.innerDisplayedColumns = ['Class Date', 'Time', 'Location', 'Instructor', 'Seats Available','Total Seats'];
        this.subBlockDisplayedColumns = ['First Name', 'Last Name', 'Position', 'Action'];
        this.showEnrollButtons = true;
        this.showReleaseButton = false;
        this.showRetakeButton = false;
        this.doShowMainExpandable = true;
        this.isWaitlistPage=true;
        this.readyManageWaitlistData();
        break;
      case '/implementation/sc/retake':
        this.isILA = false;
        this.title = "Release test to be released to the" + await this.labelPipe.transform('Employee') + "s";
        this.columnsToDisplay = ['ILA No.', 'Title'];
        this.innerDisplayedColumns = ['Class Date', 'Time', 'Location', 'Instructor', 'Seats Available'];
        this.subBlockDisplayedColumns = ['First Name', 'Last Name', 'Test ID', 'Action'];
        this.showEnrollButtons = false;
        this.showReleaseButton = false;
        this.showRetakeButton = true;
        this.doShowMainExpandable = true;
        this.readyRetakeData();
        break;
      default:
        this.loader = false;
        break;
    }
  }

  async readyTestToReRelease() {
    this.ilas = await this.trService.getTestToReRelease();
    
    this.readyTestDataSource(this.ilas);
  }

  async readyRetakeData(){
    var data = await this.trService.readyRetakeData();
    this.ilas = data;
    
    this.readyRetakeDataSource(data);
  }

  async readySelfRegData() {
    this.ilas = await this.trService.getPendingSelfReg();
    
    this.readyDataSource(this.ilas, true, false, false);
  }

  async readyILAtoScheduleData() {
    this.ilas = await this.trService.getILAsToSchedule();
    this.readyDataSource(this.ilas, true, false, false);
  }

  async readyManageWaitlistData() {
    this.ilas = await this.trService.getWaitlistedData();
    this.readyDataSource(this.ilas, false, true, false);
  }

  datePipe = new DatePipe('en-us');

  readyRetakeDataSource(ilas:any[]){
    var myData:ModifiedILA[] = [];
    var enrolled = 0;
    ilas.forEach((ila,i)=>{
      myData.push({
        ilaId:ila.id,
        addresses:[],
        ['ILA No.']:ila.number,
        ['Title']:ila.name,
      });

      ila.classSchedules.forEach((cs,j)=>{
        var convertedDateTime = this.covertUtcToLocalTime(cs.startDateTime);

        (myData[i].addresses as ModifiedCS[]).push({
          classId: cs.id,
          blocks: [],
          ['Class Date']:this.datePipe.transform(convertedDateTime, 'yyyy-MM-dd'),
          ['Time']:this.datePipe.transform(convertedDateTime, 'h:mm a'),
          ['Location']:cs.location,
          ['Instructor']:cs.instructor,
          ['Seats Available']:cs.seatsAvailable,
        });
        cs.classSchedule_Employee.forEach((csEmp:any)=>{
          (myData[i].addresses[j].blocks as ModifiedEmp[]).push({
            classId: cs.id,
            empId: csEmp.employeeId,
            Position: "",
            isEnrolled: false,
            isWaitlisted: false,
            waitListEnabled: false,
            testId: csEmp?.testId,
            ['Test ID']:csEmp?.testTitle,
            ['First Name']:csEmp.firstName,
            ['Last Name']:csEmp.lastName,
          })
        })
      })
    });
    this.dataSource.data = myData;
    this.loader = false;
  }

  async releaseRetake(element:any){
    var options = new ClassRoasterUpdateOptions();
    options.testType = "Retake";
    options.testId = element.testId;
    options.classId = element.classId;
    options.empId = element.empId;
    options.score = 0;
    await this.rosterService.releaseTest(options.empId,options).then((_)=>{
      this.alert.successToast("Retake Released Successfully");
      this.readyType();
    })
  }

  readyTestDataSource(ilas: any[]) {
    var myData: ModifiedILA[] = [];
    ilas.forEach((ila, i) => {
      myData.push({
        ilaId: ila.id,
        ['ILA No.']:ila.number,
        ['Title']:ila.name,
        addresses: [],
      });
      ila.classSchedules.forEach((cs, j) => {

         var convertedDateTime = this.covertUtcToLocalTime(cs.startDateTime);
        (myData[i].addresses as ModifiedCS[]).push({
          classId: cs.id,
          ['Class Date']:this.datePipe.transform(convertedDateTime, 'yyyy-MM-dd'),
          ['Instructor']: cs.instructor ?? 'N/A',
          Location: cs?.location ?? 'N/A',
          Time: this.datePipe.transform(convertedDateTime, 'h:mm a'),
          blocks: [],
          ['Seats Available']:cs.seatsAvailable,
        });
        cs.classSchedule_Employee.forEach((roster) => {
          (myData[i].addresses[j].blocks as ModifiedEmp[]).push({
            classId: cs.id,
            empId: roster.employeeId,
            ['First Name']:roster.firstName ?? "N/A",
            ['Last Name']:roster.lastName ?? "N/A",
            Position: roster.employee?.employeePositions[0]?.position?.positionTitle ?? "",
            isEnrolled: false,
            isWaitlisted: false,
            waitListEnabled: false,
            testId: roster.testId,
            ['Test ID']:roster.testTitle
          })
        })
      })
    });
    this.dataSource.data = myData;
    
    this.loader = false;
  }

  readyDataSource(ilas: any[], doCheck: boolean, showInwaiting: boolean, showEnrolled: boolean) {
    
    var enrolled = 0;
    var myData: ModifiedILA[] = [];
    ilas.forEach((ila, i) => {
      myData.push({
        ilaId: ila.id,
        ['ILA No.']:ila.number,
        ['Title']:ila.name,
        addresses: [],
      })
      ila.classSchedules?.forEach((schedule, j) => {
        enrolled = 0;
        enrolled = schedule.classSchedule_Employee.filter((f) => {
          return f.isEnrolled === true;
        }).length;

        var convertedDate = this.covertUtcToLocalTime(schedule.startDateTime);
        (myData[i].addresses as ModifiedCS[]).push({
          classId: schedule.id,
          ['Class Date']:this.datePipe.transform(convertedDate, 'yyyy-MM-dd'),
          ['Instructor']: schedule.instructor,
          Location: schedule.location,
          ['Seats Available']:schedule.seatsAvailable,
          ['Total Seats']:schedule.classSize,
          Time: this.datePipe.transform(convertedDate, 'h:mm a'),
          blocks: [],
        })
        schedule.classSchedule_Employee.forEach((emp) => {
          if (doCheck || (emp.isEnrolled === showEnrolled && emp.isWaitListed === showInwaiting)) {
            myData[i].addresses[j].blocks.push({
              classId: emp.classScheduleId,
              empId: emp.employeeId,
              ['First Name']: emp.firstName ?? "N/A",
              ['Last Name']: emp.lastName ?? "N/A",
              Position: emp.employee?.employeePositions[0]?.position?.positionTitle ?? "",
              isEnrolled: emp.isEnrolled,
              isWaitlisted: emp.isWaitListed,
              waitListEnabled: schedule.waitListEnabled,
            })
          }
        })
      })
    })
    this.dataSource.data = myData;
    this.loader = false;
  }

  /** Whether the number of selected elements matches the total number of rows. */
  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.dataSource.data.length;
    return numSelected === numRows;
  }

  /** Selects all rows if they are not all selected; otherwise clear selection. */
  toggleAllRows() {
    if (this.isAllSelected()) {
      this.selection.clear();
      return;
    }

    this.selection.select(...this.dataSource.data);
  }

  /** The label for the checkbox on the passed row */
  checkboxLabel(row?: ILA): string {
    if (!row) {
      return `${this.isAllSelected() ? 'deselect' : 'select'} all`;
    }
    return `${this.selection.isSelected(row) ? 'deselect' : 'select'} row ${row.number + 1}`;
  }
  async goBack() {

    //this.router.navigate(['implementation/sc/overview']);
    history.back();
  }

  expandedSource = new MatTableDataSource<ModifiedCS>();
  toggleRow(element: ModifiedILA) {
    var myVal: any;
    element.addresses ? (myVal = (this.expandedElement && this.expandedElement?.ilaId === element?.ilaId) ? null : element)
      : null;

    if (myVal === null) {
      this.expandedElement = null;
    }
    else {
      this.expandedElement = Object.assign(myVal);
    }
    if (this.expandedElement && (this.expandedElement.addresses as MatTableDataSource<ModifiedCS>).data === undefined) {
      this.expandedElement.addresses = Object.assign(new MatTableDataSource<ModifiedCS>(this.expandedElement.addresses as ModifiedCS[]));
    }

    this.cd.detectChanges();
    this.innerTables.forEach(
      (table, index) =>
      ((table.dataSource as MatTableDataSource<Address>).sort =
        this.innerSort.toArray()[index])
    );
  }

  toggleSubRow(element: ModifiedCS) {
    var myVal: any;
    element.blocks
      ? (myVal =
        (this.expandedSubElement && this.expandedSubElement?.classId === element.classId ? null : element))
      : null;
    if (myVal === null) {
      this.expandedSubElement = null;
    }
    else {
      this.expandedSubElement = Object.assign(myVal);
    }
    if (this.expandedSubElement && (this.expandedSubElement.blocks as MatTableDataSource<ModifiedEmp>).data === undefined) {
      this.expandedSubElement.blocks = Object.assign(new MatTableDataSource<ModifiedEmp>(this.expandedSubElement.blocks as ModifiedEmp[]));
    }
    this.cd.detectChanges();
    this.subTables.forEach(
      (table, index) =>
      ((table.dataSource as MatTableDataSource<Block>).sort =
        this.subSort.toArray()[index])
    );
  }

  applyFilter(filterValue: string) {
    this.innerTables.forEach(
      (table, index) =>
      ((table.dataSource as MatTableDataSource<Address>).filter = filterValue
        .trim()
        .toLowerCase())
    );
  }

  async addToWaitList(empId: any, classId: any) {
    
    var options = new ClassScheduleEnrollOptions();
    options.classId = classId;
    options.employeeId = empId;
    await this.trService.waitlistStudent(options).then((_) => {
      this.alert.successToast("Student Added in Class Waitlist");
      this.readyType();
    })
  }

  async enrollStudent(empId: any, classId: any) {
    var options = new ClassScheduleEnrollOptions();
    options.classId = classId;
    options.employeeId = empId;
    if(this.isWaitlistPage){
      await this.trService.enrollStudentWithClassSizeByPass(options).then((_) => {
        this.alert.successToast("Student Enrolled in Class");
        this.readyType();
      });
    }else{
      await this.trService.enrollStudent(options).then((_) => {
        this.alert.successToast("Student Enrolled in Class");
        this.readyType();
      });
    }
  }

  async declineEmployee(empId: any, classId: any) {
    var options = new ClassScheduleEnrollOptions();
    options.classId = classId;
    options.employeeId = empId;
    await this.trService.declineEmployee(options).then((_) => {
      this.alert.successToast("Student Declined Enrollment");
      this.readyType();
    })
  }

  async releaseTest(element:ModifiedEmp){
    
    var options:ReReleaseOptions = {
      testId:element.testId,
      classId:element.classId,
      empId:element.empId,
    }
    this.trService.reReleaseTest(options).then((_)=>{
      this.alert.successToast("Test Re-released successfully");
      this.readyType();
    })
  }
  covertUtcToLocalTime(datetime:any) : Date
  {

    var startDateString = this.datePipe.transform(
      datetime,
      'yyyy-MM-dd hh:mm a'
    );
    const utcStartDateTime = new Date(startDateString.toString() + ' UTC');
    // Convert UTC date and time to local time
    const localstartDateTimeString = utcStartDateTime.toLocaleString();
    var newdatetime = new Date(Date.parse(localstartDateTimeString));
    //
    return newdatetime;
  }
}

export interface ReReleaseOptions{
  testId:any;
  classId:any;
  empId:any;
}

export interface User {
  ilaNo: string;
  title: string;
  addresses?: Address[] | MatTableDataSource<Address>;
}

export interface Address {
  classDate: string;
  time: string;
  location: string;
  instructor: string;
  seatAvailable: string;
  blocks?: Block[] | MatTableDataSource<Block>;
}

export interface Block {
  firstName: string;
  lastName: string;
  position: string;
}

export interface UserDataSource {
  name: string;
  email: string;
  phone: string;
  addresses?: MatTableDataSource<Address>;
}

export class ModifiedILA {
  ilaId!: any;
  ['ILA No.']?:string;
  ['Title']?:string;
  addresses: ModifiedCS[] | MatTableDataSource<ModifiedCS>;
}

export interface ModifiedCS {
  classId: any;
  ['Class Date']?:any;
  ['Time']?:any;
  ['Location']?:string;
  ['Instructor']?:string;
  ['Seats Available']?:number;
  ['Total Seats']?:number;
  blocks: ModifiedEmp[] | MatTableDataSource<ModifiedEmp>;
}

export class ModifiedEmp {
  empId: any;
  classId: any;
  Position: string;
  isEnrolled: boolean;
  isWaitlisted: boolean;
  waitListEnabled: boolean;
  testId?:any;
  ['First Name']?:string;
  ['Last Name']?:string;
  ['Test ID']?:string;
}

const USERS: User[] = [
  {
    ilaNo: 'Qts_036',
    title: 'Migrate Database',
    addresses: [
      {
        classDate: '12-12-2022',
        instructor: 'Sarah Johnson',
        location: 'Training Hall',
        seatAvailable: '3',
        time: '3:00PM',
        blocks: [
          {
            firstName: 'zeeshan',
            lastName: 'abid',
            position: 'SSE',

          },
          {
            firstName: 'Ali',
            lastName: 'Ahmmad',
            position: 'SSE',
          },
        ],
      },
    ],
  },
  {
    ilaNo: 'Qts_036',
    title: 'Migrate Database',
    addresses: [
      {
        classDate: '12-12-2022',
        instructor: 'Sarah Johnson',
        location: 'Training Hall',
        seatAvailable: '3',
        time: '3:00PM',
      },
      {
        classDate: '12-12-2022',
        instructor: 'Sarah Johnson',
        location: 'Training Hall',
        seatAvailable: '3',
        time: '3:00PM',
      },
    ],
  },
];
