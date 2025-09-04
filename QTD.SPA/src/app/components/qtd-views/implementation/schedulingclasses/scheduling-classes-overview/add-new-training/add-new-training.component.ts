import {TasksService} from './../../../../../../_Services/QTD/tasks.service';
import {BreakpointObserver} from '@angular/cdk/layout';
import {TemplatePortal} from '@angular/cdk/portal';
import {StepperOrientation} from '@angular/cdk/stepper';
import {
  ChangeDetectorRef,
  Component,
  EventEmitter,
  Inject,
  Input,
  LOCALE_ID,
  OnDestroy,
  OnInit,
  Output,
  ViewChild,
  ViewContainerRef,
  inject,
} from '@angular/core';
import {
  AbstractControl,
  UntypedFormArray,
  UntypedFormControl,
  UntypedFormGroup,
  ValidationErrors,
  Validators,
} from '@angular/forms';
import {MatLegacyDialog as MatDialog} from '@angular/material/legacy-dialog';
import {MatStepper} from '@angular/material/stepper';
import {ActivatedRoute, Router, RoutesRecognized} from '@angular/router';
import {select, Store} from '@ngrx/store';
import * as ckcustomBuild from 'src/app/ckcustomBuild/build/ckeditor.js';
import {Observable, Subscription} from 'rxjs';
import {filter, map, pairwise, skip} from 'rxjs/operators';
import {FlyInPanelService} from 'src/app/_Shared/services/flyInPanel.service';
import {SweetAlertService} from 'src/app/_Shared/services/sweetalert.service';
import {
  sideBarClose,
  sideBarBackDrop,
  sideBarToggle,
  sideBarOpen,
} from 'src/app/_Statemanagement/action/state.menutoggle';
import {MatLegacyTableDataSource as MatTableDataSource} from '@angular/material/legacy-table';
import {CdkDragDrop, moveItemInArray} from '@angular/cdk/drag-drop';
import {UploadAdapter} from 'src/app/_Shared/Utils/CKEditor_UploadAdapter';
import {SelectionModel} from '@angular/cdk/collections';
import {DatePipe} from '@angular/common';
import {ProviderService} from 'src/app/_Services/QTD/provider.service';
import {Provider} from 'src/app/_DtoModels/Provider/Provider';
import {IlaService} from 'src/app/_Services/QTD/ila.service';
import {Training} from 'src/app/_DtoModels/SchedulesClassses/training';
import {InstructorService} from 'src/app/_Services/QTD/instructor.service';
import {LocationService} from 'src/app/_Services/QTD/location.service';
import {Location} from 'src/app/_DtoModels/Locations/Location';
import {TrainingService} from 'src/app/_Services/QTD/training.service';
import {
  EMPSettingCBTCreationOptions,
  EMPSettingEvaluationCreationOptions,
  EMPSettingsTQTaskEvaluation,
  EMPSettingStudentEval,
  EMPSettingTestReleaseCreationOptions,
  EMPSettingTQCreationOptions,
  TrainingCreationOptions,
  TrainingStudentCreationOptions,
} from 'src/app/_DtoModels/SchedulesClassses/training-creation-options';
import {TestsService} from 'src/app/_Services/QTD/tests.service';
import {intervalToDuration} from 'date-fns';
import {
  EMPSettingStudentEvaluationCreationOption,
  EMPSettingStudentEvaluationUpdateOption
} from 'src/app/_DtoModels/StudentEvaluationQuestions/StudentEvaluation_Question_LinkCreateOptions';
import {StudentEvaluationService} from 'src/app/_Services/QTD/student-evaluation.service';
import {ClassSchedules} from 'src/app/_DtoModels/SchedulesClassses/ClassSchedules';
import {ClassSchedule_Employee} from 'src/app/_DtoModels/SchedulesClassses/ClassSchedule_Employee';
import {SubSink} from 'subsink';
import {DataBroadcastService} from 'src/app/_Shared/services/DataBroadcast.service';
import {ILAEvalMethodVM} from 'src/app/_DtoModels/ILA/ILAEvalMethodVM';
import {StudentEvaluationAudiencesService} from 'src/app/_Services/QTD/student-evaluation-audiences.service';
import {ClassScheduleService} from 'src/app/_Services/QTD/class-schedule.service';
import {ClassSchedule_SelfRegCreateOptions} from '@models/SchedulesClassses/ClassSchedule_SelfRegCreateOptions';
import { IlaEmpTestSettingsComponent } from 'src/app/components/qtd-views/design-and-development/providers-and-ila/ila-create-wizard/ila-wizard-components/seq-and-schedule/ila-emp-test-settings/ila-emp-test-settings.component';
import { IlaEmpTqSettingsComponent } from 'src/app/components/qtd-views/design-and-development/providers-and-ila/ila-create-wizard/ila-wizard-components/seq-and-schedule/ila-emp-tq-settings/ila-emp-tq-settings.component';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { EmpSettingsReleaseTypeVM } from '@models/EmpSettingsReleaseType/EmpSettingsReleaseTypeVM';
import { EmpSettingsReleaseTypeService } from 'src/app/_Services/QTD/empSettingsReleaseType.service';
import { ApiClassScheduleTestReleaseSettingService } from 'src/app/_Services/QTD/ClassScheduleTestReleaseSettings/api.classScheduleTestReleaseSetting.service';
import { ApiClassScheduleTqReleaseSettingService } from 'src/app/_Services/QTD/ClassScheduleTestReleaseSettings/api.classScheduleTqReleaseSetting.service';
import { ILADetailsVM } from '@models/ILA/ILADetailsVM';
import { MatLegacyCheckboxChange as MatCheckboxChange } from '@angular/material/legacy-checkbox';
import { MatLegacySelect as MatSelect } from '@angular/material/legacy-select';

export interface AllValidationErrors {
  control_name: string;
  error_name: string;
  error_value: any;
  control_modified: string;
}

export interface FormGroupControls {
  [key: string]: AbstractControl;
}

export function getFormValidationErrors(
  controls: FormGroupControls
): AllValidationErrors[] {
  let errors: AllValidationErrors[] = [];
  Object.keys(controls).forEach((key) => {
    const control = controls[key];
    if (control instanceof UntypedFormGroup) {
      errors = errors.concat(getFormValidationErrors(control.controls));
      control.markAsTouched({
        onlySelf: true,
      });
    } else if (control instanceof UntypedFormArray) {
      let i: number = 0;
      for (const arrayControl of control.controls) {
        if (arrayControl instanceof UntypedFormGroup) {
          errors = errors.concat(
            getFormValidationErrors(arrayControl.controls)
          );
        } else {
          const obj = {};
          obj[key + '-' + i] = arrayControl;
          i++;
          errors = errors.concat(getFormValidationErrors(obj));
        }
      }
    }

    const controlErrors: ValidationErrors = controls[key].errors;
    if (controlErrors !== null) {
      Object.keys(controlErrors).forEach((keyError) => {
        errors.push({
          control_name: key,
          error_name: keyError,
          control_modified: beautifyControl(key),
          error_value: controlErrors[keyError],
        });
      });
    }
  });
  return errors;
}

function beautifyControl(key: string): string {
  let result: string[] = [];
  const splitters = ['-', '_'] as const;

  if (key.includes(splitters[0])) result = key.split(splitters[0]);
  else if (key.includes(splitters[1])) result = key.split(splitters[1]);
  else result = key.replace(/([a-z])([A-Z])/g, '$1 $2').split(' ');

  return [
    ...result.map((e: string, i: number) => e[0].toUpperCase() + e.slice(1)),
  ].join(' ');
}

export interface Schedule {
  startDate: string;
  endDate: string;
  location: string;
  instructor: string;
}

export interface Task {
  tasknumber: string;
  type: string;
  desc: string;
}

export interface TaskEval {
  name: string;
  position: string;
}

export interface Employee {
  name: string;
  position: string;
  organization: string;
}

const ELEMENT_DATA_Task: Task[] = [
  {tasknumber: '1.1', type: 'Task', desc: 'Task Description 1'},
  {tasknumber: '1.2', type: 'Task', desc: 'Task Description 2'},
];
const ELEMENT_DATA_Employee: Employee[] = [
  {
    name: 'Lauren Box',
    position: 'Senior System Operator',
    organization: 'QTS',
  },
  {name: 'Faran Jones', position: 'System Operator', organization: 'QTS'},
];

@Component({
  selector: 'app-add-new-training',
  templateUrl: './add-new-training.component.html',
  styleUrls: ['./add-new-training.component.scss'],
})
export class AddNewTrainingComponent implements OnInit, OnDestroy {
  @ViewChild('stepper') stepper: MatStepper;
  @ViewChild('ilaTestRelease') ilaTestRelease!:IlaEmpTestSettingsComponent;
  @ViewChild('ilaEmpTqSettings') ilaEmpTqSettings!:IlaEmpTqSettingsComponent;
  stepperOrientation: Observable<StepperOrientation>;
  currentIndex: number = 0;
  evalSelection = new SelectionModel(false);
  ilaId: string = '';
  isDateTimeValid: boolean;
  linkedPositionToIla: any[] = [];
  lsegs!: Array<any>;
  trainingObject: Training = new Training();
  provider_list: any[] = [];
  provider_list_original: Provider[] = [];
  ila_list: any[] = [];
  ila_list_original: any[] = [];
  instructor_list: any[] = [];
  recurrence_list: any[] = [];
  employeeIds: string[] = [];

  ilA_Segment_Links_List: any[] = [];
  TestLinkedILAsPreTest_List: any[] = [];
  TestLinkedILAsReTakeTest_List: any[] = [];
  TestLinkedILAsTest_List: any[] = [];
  numberOfTimeRetake: any[] = [];
  selectedRetakeId: any[] = [];

  student_Evaluation_List: any[] = [];

  testReleaseCheckNUll: boolean = false;
  cbtReleaseCheckNUll: boolean = false;
  evaluationReleaseCheckNUll: boolean = true;
  selfRegistrationCheckNUll: boolean = false;

  tqReleaseCheckNUll: boolean = false;
  recurrenceType: any;
  recurrenceDescription: any;
  endDate: any;
  recursEveryNumber: any;
  selectedStudentEvaluationId: number = 0;
  classes_List: any[] = [];
  linked_classes_List: any[] = [];

  location_list: Location[] = [];

  trainingId: string = '';
  deliveryMethodName: string = '';
  isSelfPaced: boolean = false;
  ilaName: string = '';
  providerId: number;
  qualInfo: any;

  disableValidation: boolean = false;
  isIlaUseForEMP: any;
  isSpinner: boolean = true;
  isILALoading: boolean = false;
  noILAFound: boolean = false;
  noProviderFound: boolean = false;
  isOpen = false;
  dateInformation: any;
  trainingStartDateTime: any
  trainingEndtDateTime: any;
  isTimeEmpty: any = false;
  trainingForm: UntypedFormGroup;
  classSize: number;
  isPubliclyAvailable: boolean;
  setCheckboxValue: boolean;
  evalForm = new UntypedFormGroup({
    setAvailabilityTimeEvaluate: new UntypedFormControl(null),

    evaluationPK: new UntypedFormControl(''),
    evaluationRequired: new UntypedFormControl(false),
    evaluationUsedToDeployStudentEvaluation: new UntypedFormControl(false),
    evaluationAvailableOnStartDate: new UntypedFormControl(false),
    evaluationAvailableOnEndDate: new UntypedFormControl(false),
    finalGradeRequired: new UntypedFormControl(false),
    releaseOnSpecificTimeAfterClassEndDate: new UntypedFormControl(false),
    releaseAfterEndTime: new UntypedFormControl(false),
    evaluationDueDate: new UntypedFormControl(1, Validators.required),
    empSettingsReleaseType: new UntypedFormControl("", Validators.required),
    releasePrior: new UntypedFormControl(false),
    releaseAfterGradeAssigned: new UntypedFormControl(false)
  });

  editor = ckcustomBuild;
  dataSource = new MatTableDataSource<any>();
  dataSourceTask = new MatTableDataSource<any>();
  dataSourceSegment = new MatTableDataSource<any>();
  dataSourceEmployees: MatTableDataSource<any>[] = [];
  dataSourceTaskEval = new MatTableDataSource<any>();
  dataSourceTQEval = new MatTableDataSource<any>();
  dataSourceEvalReleaseStudent = new MatTableDataSource<any>();
  isCoursePubliclyAvailable: boolean;

  /// TQ From
  spinner = false;
  tqSelection = new SelectionModel<string>(true, []);
  originalInitialValues: any;
  TQForm = new UntypedFormGroup({
    setAvailabilityTimeTQ: new UntypedFormControl(null),

    tqPK: new UntypedFormControl(null),
    tqRequired: new UntypedFormControl(false),
    releaseAtOnce: new UntypedFormControl(false),
    releaseOneAtTime: new UntypedFormControl(false),
    releaseOnClassStart: new UntypedFormControl(false),
    releaseOnClassEnd: new UntypedFormControl(false),
    specificTime: new UntypedFormControl(null),
    priorToSpecificTime: new UntypedFormControl(false),
    oneSignOffRequired: new UntypedFormControl(false),
    multipleSignOffRequired: new UntypedFormControl(0),
    multipleSignOffRequiredCheck: new UntypedFormControl(false),
    tqDueDate: new UntypedFormControl(0, Validators.required),
  });
  displayedColumns: string[] = [
    'startDate',
    'endDate',
    'location',
    'instructor',
    'action',
  ];
  displayedColumnsTask: string[] = ['order', 'tasknumber', 'desc'];
  displayedColumnsTaskEvaluators: string[] = ['name', 'position'];
  displayedColumnsTQEvaluators: string[] = ['name', 'dropdown'];

  displayedColumnsStudentEvaluators: string[] = ['id', 'date'];
  displayedColumnsSegments: string[] = ['tasknumber', 'type', 'desc'];
  displayedColumnsEmployees: string[] = [
    'select',
    'name',
    'position',
    'organization',
    'action',
  ];
  startDateEdit: any;
  @Output() seq_and_schedule = new EventEmitter<any>();
  employeeIndex: 0;
  recurringIndex: any;
  recurringIdToDelete: any;
  viewSelected = 'Segments Review';
  ilaItems: any[];
  evalValues: any = {};
  updatedEvalValues: any = {};
  testAvailabiilityTime: any[];
  testEval: any[];
  audience: any[];
  rscId: any;
  public Editor = ckcustomBuild;
  selectedType = 'classroom';
  sci_placeholder =
    'Use this space to list special materials needed for class, whether lunch is provided, etc. Information entered into this textbox will be included in the registration email';
  isReordered = false;
  selection = new SelectionModel<any>(true, []);
  unlinkIds: any = [];
  deleteDescription: any;
  reviewData: any;
  useILAForEmp: any;
  empSettingsReleaseTypes : EmpSettingsReleaseTypeVM[];
  defaultEmpSettingReleaseTypeId : string = "";
  isChecked: boolean;
  publicClassStartdate: Date;
  currentDate = new Date();
  @ViewChild('select', { static: false }) select!: MatSelect;
  @ViewChild('selectIla', { static: false }) selectIla!: MatSelect;

  
  constructor(
    private router: Router,
    private store: Store<{ toggle: string }>,
    public changeDetector: ChangeDetectorRef,
    public breakpointObserver: BreakpointObserver,
    public flyPanelService: FlyInPanelService,
    private vcf: ViewContainerRef,
    private instructorService: InstructorService,
    private providerSrvc: ProviderService,
    private locationSevc: LocationService,
    private ilaService: IlaService,
    private studentEvaluationService: StudentEvaluationService,
    private taskService: TasksService,
    public dialog: MatDialog,
    private alert: SweetAlertService,
    private trainingSevc: TrainingService,
    private scheduleService: ClassScheduleService,
    private route: ActivatedRoute,
    private testService: TestsService,
    private studentEvaluationAudiencesService: StudentEvaluationAudiencesService,
    private dataBroadcastService: DataBroadcastService,
    public datePipe: DatePipe,
    private labelPipe:LabelReplacementPipe,
    private empSettingsReleaseTypeService :EmpSettingsReleaseTypeService,
    private classTestEmpSettingService:ApiClassScheduleTestReleaseSettingService,
    private classTQReleaseSettingService:ApiClassScheduleTqReleaseSettingService,
  ) {

    this.datePipe = new DatePipe('en-us');
    this.stepperOrientation = breakpointObserver
      .observe('(min-width: 800px)')
      .pipe(map(({matches}) => (matches ? 'horizontal' : 'vertical')));

    this.ilaItems = [
      {
        label: 'PreTest',
        heading: 'Intro to QTD',
        type: 'pretest',
      },
      {
        label: 'Segment 1',
        heading: 'Classroom',
        type: 'classroom',
      },
      {
        label: 'Segment 2',
        heading: 'Procedure Review',
        type: 'procedure_review',
      },
      {
        label: 'Test',
        heading: 'Intro to QTD',
        type: 'test',
      },
      {
        label: 'Retake 1',
        heading: 'Intro to QTD',
        type: 'retake',
      },
      {
        label: 'Segment 3',
        heading: 'Self Study',
        type: 'selfstudy',
      },
      {
        label: 'OJT Tasks',
        heading: '1.3.1 and 1.3.2',
        type: 'ojttasks',
      },
      {
        label: 'Task Qualification Tasks',
        heading: '1.3.1 and 1.3.2',
        type: 'tqt',
      },
    ];

    this.testAvailabiilityTime = [
      {
        timeSpan: '60 minutes prior to class end time',
        time: 60,
        prior: true,
      },
      {
        timeSpan: '60 minutes prior to class end time',
        time: 60,
        prior: true,
      },
      {
        timeSpan: '30 minutes prior to class end time',
        time: 30,
        prior: true,
      },
      {
        timeSpan: '15 minutes prior to class end time',
        time: 15,
        prior: true,
      },
      {
        timeSpan: '15 minutes after class end time',
        time: 15,
        prior: false,
      },
      {
        timeSpan: '30 minutes after class end time',
        time: 30,
        prior: false,
      },
      {
        timeSpan: '60 minutes after class end time',
        time: 60,
        prior: false,
      },
    ];

    this.testEval = [
      {
        timeSpan: 'Final grade required to release Evaluation',
        time: 0,
        prior: true,
      },
      {
        timeSpan: 'Final grade not required to release Evaluation',
        time: 0,
        prior: true,
      },
      {
        timeSpan: '60 minutes prior to class end time',
        time: 60,
        prior: true,
      },
      {
        timeSpan: '30 minutes prior to class end time',
        time: 30,
        prior: true,
      },
      {
        timeSpan: '15 minutes prior to class end time',
        time: 15,
        prior: true,
      },
      {
        timeSpan: '15 minutes after class end time',
        time: 15,
        prior: false,
      },
      {
        timeSpan: '30 minutes after class end time',
        time: 30,
        prior: false,
      },
      {
        timeSpan: '60 minutes after class end time',
        time: 60,
        prior: false,
      },
    ];
  }

  subscription = new SubSink();
  selectedValue: any;

  ngOnInit(): void {
    this.store.dispatch(sideBarOpen());
    this.subscription.sink = this.dataBroadcastService.updateTime.subscribe(
      (_) => {
        this.EnrollStudentSelected();
      }
    );
    this.subscription.sink =
      this.dataBroadcastService.refreshEvalQualification.subscribe((_) => {
        // this.getTQTaskEvaluations();
        this.getClassTQEvaluators();
      });
    this.router.events
      .pipe(
        filter((e: any) => e instanceof RoutesRecognized),
        pairwise()
      ).subscribe((e: any) => {
      // previous url
    });
    this.dataBroadcastService.publicClassEnabled.subscribe((x) => {
      this.isChecked = x;
    })
    
    this.isDateTimeValid = false;
    this.evalValues = this.evalForm.value;
    this.getProviders();
    this.getInstructors();
    this.getLocations();
    this.readyStudentAudience();
    this.store.dispatch(sideBarClose());
    this.store.dispatch(sideBarBackDrop({backdrop: false}));
    this.dataSource.data;
    this.dataSourceTask.data = ELEMENT_DATA_Task;
    this.route.queryParams.subscribe(params => {
      if (params['providerId']) this.providerId = params['providerId'];
      if (params['ilaId']) this.ilaId = params['ilaId'];
      if (params['employeeId']) {
        if (typeof params['employeeId'] === 'string') {
          this.employeeIds.push(params['employeeId']);
        } else if (typeof params['employeeId'] === 'object') {
          this.employeeIds = params['employeeId'];
        }
      }
    });
    this.initializeTrainingForm();
    if (this.providerId) {
      this.selectProvider(this.providerId);
    }
    this.route.params.subscribe((params: any) => {
      if (params.hasOwnProperty('id') || params.hasOwnProperty('index')) {
        this.trainingId = params['id'];
        this.disableValidation = true;
        if (params['index']) {
          this.getInfo(this.trainingId);
          this.check();
          setTimeout(() => {
            if (this.stepper) {
              this.currentIndex = 0;
              this.stepper.selectedIndex = 0;
            }
          }, 1);
        } else {
          ;
          this.getInfo(this.trainingId);
          this.check();
          setTimeout(() => {
            if (this.stepper) {
              this.currentIndex = 0;
              this.stepper.selectedIndex = 0;
            }
          }, 1);
        }
      }
    });
    
  }

  initializeTrainingForm() {
    this.trainingForm = new UntypedFormGroup({
      //for student linked to evaluation error
      // changeClass: new FormControl(''),
      providerId: new UntypedFormControl(this.providerId, Validators.required),
      ilaId: new UntypedFormControl(this.ilaId, Validators.required),
      locationId: new UntypedFormControl('', Validators.required),
      instructorId: new UntypedFormControl('', Validators.required),
      startDate: new UntypedFormControl('', Validators.required),
      startTime: new UntypedFormControl('', Validators.required),
      endDate: new UntypedFormControl('', Validators.required),
      endTime: new UntypedFormControl('', Validators.required),
      courseInstruction: new UntypedFormControl(''),
      webLink: new UntypedFormControl(''),
      searchTxt: new UntypedFormControl(''),
      ilaSearch: new UntypedFormControl(''),
      //marker
      classSize: new UntypedFormControl(30, [Validators.required]),
      // To Send Server
      startDateTime: new UntypedFormControl(''),
      endDateTime: new UntypedFormControl(''),

      /// self registration
      selfRegPk: new UntypedFormControl(0),
      makeAvailableForSelfReg: new UntypedFormControl(false),
      requireAdminApproval: new UntypedFormControl(false),
      sendApprovedEmail: new UntypedFormControl(false),
      acknowledgeRegDisclaimer: new UntypedFormControl(false),
      regDisclaimer: new UntypedFormControl(''),
      limitForLinkedPositions: new UntypedFormControl(false),
      closeRegOnStartDate: new UntypedFormControl(false),
      enableWaitlist: new UntypedFormControl(false),

      /// EMP Test Release

      setAvailabilityTimeTest: new UntypedFormControl(),

      testPK: new UntypedFormControl(0),
      finalTestId: new UntypedFormControl(0),
      preTestId: new UntypedFormControl(0),
      usePreTestAndTest: new UntypedFormControl(false),
      preTestRequired: new UntypedFormControl(false),
      preTestAvailableOnEnrollment: new UntypedFormControl(false),
      preTestAvailableOneStartDate: new UntypedFormControl(false),
      showStudentSubmittedPreTestAnswers: new UntypedFormControl(false),
      showCorrectIncorrectPreTestAnswers: new UntypedFormControl(false),
      makeAvailableBeforeDays: new UntypedFormControl(0),
      finalTestPassingScore: new UntypedFormControl(''),
      makeFinalTestAvailableImmediatelyAfterStartDate: new UntypedFormControl(false),
      makeFinalTestAvailableOnClassEndDate: new UntypedFormControl(false),
      makeFinalTestAvailableAfterCBTCompleted: new UntypedFormControl(false),
      makeFinalTestAvailableOnSpecificTime: new UntypedFormControl(0),
      finalTestSpecificTimePrior: new UntypedFormControl(false),
      finalTestDueDate: new UntypedFormControl(0),
      showStudentSubmittedFinalTestAnswers: new UntypedFormControl(false),
      showStudentSubmittedRetakeTestAnswers: new UntypedFormControl(false),
      showCorrectIncorrectFinalTestAnswers: new UntypedFormControl(false),
      showCorrectIncorrectRetakeTestAnswers: new UntypedFormControl(false),
      autoReleaseRetake: new UntypedFormControl(false),
      retakeEnabled: new UntypedFormControl(false),
      numberOfRetakes: new UntypedFormControl(0),
      preTestScore: new UntypedFormControl(0),
      //retakesTestIds: number[];

      // EMP CBT Release
      cbtPK: new UntypedFormControl(0),
      cbtRequiredForCource: new UntypedFormControl(false),
      releaseCBTLearningContract: new UntypedFormControl(false),
      cbtLearningContractInstructions: new UntypedFormControl(''),
      makeAvailableOnClassStartDate: new UntypedFormControl(false),
      makeAvailableOnClassEndDate: new UntypedFormControl(false),
      makeAvailableAfterPretestCompleted: new UntypedFormControl(false),
      cbtDueDate: new UntypedFormControl(0),

      // EMP Evaluation Release
      setAvailabilityTimeEvaluate: new UntypedFormControl(null),

      evaluationPK: new UntypedFormControl(0),
      evaluationRequired: new UntypedFormControl(false),
      evaluationUsedToDeployStudentEvaluation: new UntypedFormControl(false),
      evaluationAvailableOnStartDate: new UntypedFormControl(false),
      evaluationAvailableOnEndDate: new UntypedFormControl(false),
      finalGradeRequired: new UntypedFormControl(false),
      releaseOnSpecificTimeAfterClassEndDate: new UntypedFormControl(null),
      releaseAfterEndTime: new UntypedFormControl(0),
      ////
      releasePrior: new UntypedFormControl(false),
      releaseAfterGradeAssigned: new UntypedFormControl(false),
      evaluationDueDate: new UntypedFormControl(1),
      empSettingsReleaseType: new UntypedFormControl(""),
      isPubliclyAvailableClass: new UntypedFormControl(false),
      
    });
    this.disableCheckbox();
  }

  disableCheckbox() {
  this.trainingForm.valueChanges.subscribe(() => {
    const startDate = new Date(this.trainingForm.get('startDate')?.value);
    const today = new Date();
    const startDateOnly = new Date(startDate.getFullYear(), startDate.getMonth(), startDate.getDate());
    const todayOnly = new Date(today.getFullYear(), today.getMonth(), today.getDate());
    this.setCheckboxValue = startDateOnly < todayOnly;
  });
}

  error: any;

  formValid() {
    return this.TQForm.valid;
  }

  classSizeMoreThanEnrolled(){
    return true;
  }

  check() {
    if (!this.formValid()) {
      const error: AllValidationErrors = getFormValidationErrors(
        this.TQForm.controls
      ).shift();
      if (error) {
        let text;
        switch (error.error_name) {
          case 'required':
            text = `${error.control_name} is required!`;
            break;
          case 'pattern':
            text = `${error.control_name} has wrong pattern!`;
            break;
          case 'email':
            text = `${error.control_name} has wrong email format!`;
            break;
          case 'minlength':
            text = `${error.control_name} has wrong length! Required length: ${error.error_value.requiredLength}`;
            break;
          case 'areEqual':
            text = `${error.control_name} must be equal!`;
            break;
          default:
            text = `${error.control_name}: ${error.error_name}: ${error.error_value}`;
        }
        this.error = text;
      }
      return;
    }
  }

  signOff(event: any, type: string) {
    if (event.checked) {
      switch (type) {
        case 'oneSignOffRequired':
          this.TQForm.get('multipleSignOffRequiredCheck')?.setValue(false);
          this.TQForm.get('multipleSignOffRequired')?.setValue(null);
          this.TQForm.get('multipleSignOffRequired')?.clearValidators();
          this.TQForm.get('multipleSignOffRequired')?.setErrors(null);
          break;
        case 'multipleSignOffRequiredCheck':
          this.TQForm.get('oneSignOffRequired')?.setValue(false);
          this.TQForm.get('multipleSignOffRequired')?.setValidators([
            Validators.required,
          ]);
          this.TQForm.get('multipleSignOffRequired')?.setErrors({
            required: true,
          });
          break;
      }
      this.tqSelection.select('signOff');
    } else {
      this.TQForm.get('multipleSignOffRequired')?.setValue(null);
      this.TQForm.get('multipleSignOffRequired')?.clearValidators();
      this.TQForm.get('multipleSignOffRequired')?.setErrors(null);
      this.tqSelection.deselect('signOff');
    }
    this.TQForm.updateValueAndValidity();
  }

  readyStudentAudience() {
    this.studentEvaluationAudiencesService.getAll().then((res) => {
      this.audience = res;
    }).catch((err) => {

    });
  }

  preTestRequiredChangeed(event: any) {
    if (event.checked === false) {
      this.trainingForm.reset('preTestId');
      this.trainingForm.reset('preTestAvailableOnEnrollment');
      this.trainingForm.reset('preTestAvailableOneStartDate');
      this.trainingForm.reset('makeAvailableBeforeDays');
      this.trainingForm.reset('showStudentSubmittedPreTestAnswers');
      this.trainingForm.reset('showCorrectIncorrectPreTestAnswers');
    } else {
      this.trainingForm.get('preTestRequired')?.setValue(true);
    }
  }

  usePreTestChanged(event: any) {
    this.trainingForm.get('usePreTestAndTest')?.setValue(event.checked);
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  async getTestAvailabilityTime(event) {
    this.trainingForm.patchValue({
      makeFinalTestAvailableOnSpecificTime: event.value.time,
      finalTestSpecificTimePrior: event.value.prior,
    });
  }

  async getEvalAvailabilityTime(event) {
    this.evalForm.patchValue({
      releaseAfterEndTime: event.value.time,
      releasePrior: event.value.prior,
    });
  }

  async getTQAvailabilityTime(event) {
    this.TQForm.patchValue({
      specificTime: event.value.time,
      priorToSpecificTime: event.value.prior,
    });
  }

  async getQBTAvailabilityTime(event) {
    this.trainingForm.patchValue({
      releaseAfterEndTime: event.value.time,
      releasePrior: event.value.prior,
    });
  }

  async getClassRecurrence() {
    await this.trainingSevc
      .getClassScheduleRecurrence(this.trainingId)
      .then((res) => {
        this.recurrence_list = res.map((x) => x);

        this.recurrence_list = res.map((obj) => {
          let classDate = new Date(obj.startDateTime);
          var startDateString = this.datePipe.transform(
            obj.startDateTime,
            'yyyy-MM-dd hh:mm a'
          );
          const utcStartDateTime = new Date(startDateString.toString() + ' UTC');
          // Convert UTC date and time to local time
          const localstartDateTimeString = utcStartDateTime.toLocaleString();
          obj.startDateTime = new Date(Date.parse(localstartDateTimeString));
          var endDateString = this.datePipe.transform(
            obj.endDateTime,
            'yyyy-MM-dd hh:mm a'
          );
          //const utcendtDateTimeString = res.startDateTime.toDateString();
          const utcendtDateTime = new Date(endDateString.toString() + ' UTC');
          // Convert UTC date and time to local time
          const localendDateTimeString = utcendtDateTime.toLocaleString();
          obj.endDateTime = new Date(Date.parse(localendDateTimeString));
          return {
            ...obj,
            _endDate: obj.endDateTime,
            _startDate: obj.startDateTime,
            instructorName: this.instructor_list.find(
              (x) => x.id === this.trainingForm.get('instructorId')?.value
            )?.instructorName,
            locationName: this.location_list.find(
              (x) => x.id === this.trainingForm.get('locationId')?.value
            )?.locName,
            isClassPubliclyAvailable: obj.isPubliclyAvailable,
            disableCheckbox: classDate < this.currentDate
          }
        });
        this.dataSource.data = this.recurrence_list;
      })
      .catch((err) => {
        console.error(err);
      })
      .finally(() => {
        this.isSpinner = false;
      });
  }

  async getInstructors() {
    await this.instructorService
      .getInstructor()
      .then((res) => {
        this.instructor_list = res.filter((x) => x.active == true);
      })
      .catch((err) => {
        console.error(err);
      })
      .finally(() => {
        this.isSpinner = false;
      });
  }

  async getLocations() {
    await this.locationSevc
      .getLocation()
      .then((res) => {
        this.location_list = res.filter((x) => x.active == true);
      })
      .catch((err) => {
        console.error(err);
      })
      .finally(() => {
        this.isSpinner = false;
      });
  }

  ila!: ILADetailsVM;

  async getProviders() {
    this.isSpinner = true;
    this.providerSrvc
      .getActiveProviders()
      .then((res: any) => {
        this.provider_list = res;
        this.provider_list_original = Object.assign(this.provider_list);
        if (this.provider_list.length === 0) {
          this.noProviderFound = true;
        } else {
          this.noProviderFound = false;
        }
        ;
        if (this.trainingId !== '') {
          if (this.ilaId !== '') {
            this.ilaService.get(this.ilaId).then((res) => {
              ;
              this.ila = res;
              var currProv = this.provider_list.find((data) => {
                return data.id === res.providerId;
              });
              this.trainingForm.patchValue({
                providerId: currProv.id,
              });
              this.selectProvider(currProv.id);
            });
          }
        }
      })
      .catch((err) => {
        console.error(err);
      })
      .finally(() => {
        this.isSpinner = false;
      });
  }

  selectProvider(event: any) {
    this.ila_list = [];
    this.isILALoading = true;
    this.ilaService
      .getByProvider(event, true)
      .then(async (res: any[]) => {
        this.ila_list = res;
        this.ila_list_original = Object.assign(this.ila_list);
        if (this.ila_list.length == 0) {
          this.noILAFound = true;
        } else {
          this.noILAFound = false;
          this.ila_list = this.ila_list.filter(
            (x) => x.active === true && x.isPublished === true
          );
        }
        if (this.trainingId !== '') {
          this.selectILA(this.trainingForm.get('ilaId')?.value);
        }

        var item = localStorage.getItem("stepNext");
        if (item === "true" && this.stepper !== null) {
          this.stepper.next();
        }
        localStorage.removeItem("stepNext");
      })
      .finally(() => {
        this.isILALoading = false;
      });
  }

  ilaNumber: any;

  selectILA(event: any) {
    this.deliveryMethodName = this.ila_list.find(x => x.id == event).deliveryMethodName?? "N/A";
    this.isSelfPaced = this.ila_list.find(x => x.id == event).isSelfPaced
    this.isIlaUseForEMP = this.ila_list.find(x => x.id == event);
    this.isPubliclyAvailable = this.ila_list.find(x => x.id == event).isPubliclyAvailable;
    this.isCoursePubliclyAvailable = this.ila_list.find(x => x.id == event).isPubliclyAvailable;
    if(!this.classSize)
    {
      this.classSize = this.ila_list.find(x => x.id == event).classSize;
    }
    this.useILAForEmp = this.isIlaUseForEMP.useForEMP;
    this.trainingForm.get('classSize')?.setValue(this.classSize);
    if(this.trainingId == ''){
      this.trainingForm.get('isPubliclyAvailableClass')?.setValue(this.isPubliclyAvailable);
    }
    if (this.isIlaUseForEMP.useForEMP && this.trainingId === '') {
      this.trainingForm.get('startTime')?.setValidators([Validators.required]);
      this.trainingForm
        .get('startTime')
        ?.setErrors({
          required:
            this.trainingForm.get('startTime')?.value !== '' ? false : true,
        });
      this.trainingForm.get('endTime')?.setValidators([Validators.required]);
      this.trainingForm
        .get('endTime')
        ?.setErrors({
          required:
            this.trainingForm.get('endTime')?.value !== '' ? false : true,
        });
    }

    if (this.isIlaUseForEMP.isSelfPaced) {
      this.trainingForm
        .get('instructorId')
        ?.clearValidators();
      this.trainingForm
        .get('locationId')
        ?.clearValidators();
      this.trainingForm.get('instructorId')?.setErrors(null);
      this.trainingForm.get('locationId')?.setErrors(null);
    } else {
      this.trainingForm.get('instructorId')?.setValidators(Validators.required);
      this.trainingForm.get('locationId')?.setValidators(Validators.required);
      if (this.trainingForm.get('instructorId').value === '') {
        this.trainingForm.get('instructorId')?.setErrors({required: true});
      }
      if (this.trainingForm.get('locationId').value === '') {
        this.trainingForm.get('locationId')?.setErrors({required: true});
      }
    }

    this.trainingForm.updateValueAndValidity();
    this.ilA_Segment_Links_List = this.ila_list.find(x => x.id === event).ilA_Segment_Links;
    this.ilaName = this.ila_list.find(x => x.id === event).name + ' - ' + this.ila_list.find(x => x.id === event).number

    this.recurringTableAddColumn(this.isPubliclyAvailable)
  }

  recurringTableAddColumn(isCheckboxVisible: boolean){
    const columnIndex = 4;
    const columnKey = 'isClassPubliclyAvailable';
    const indexInArray = this.displayedColumns.indexOf(columnKey);

    if(isCheckboxVisible){
      if(indexInArray == -1){
        this.displayedColumns.splice(columnIndex, 0, columnKey)
      }
    }
    else{
      if(indexInArray != -1){
        this.displayedColumns.splice(indexInArray, 1)
      }
    }
  }

  async changeIla(completed: boolean, ilaInfo: any) {
    if (completed) {
      var startTime = this.trainingForm.get('startTime').value;
      if (startTime === '') {
        Validators.required(startTime);
        this.trainingForm
          .get('startTime')
          ?.setErrors({
            required:
              this.trainingForm.get('startTime')?.value !== '' ? false : true,
          });
      }
      var endTime = this.trainingForm.get('startTime').value;
      //this.trainingForm.get('startTime')?.setValidators([Validators.required]);
      if (endTime === '') {

        Validators.required(endTime);
        this.trainingForm
          .get('endTime')
          ?.setErrors({
            required:
              this.trainingForm.get('endTime')?.value !== '' ? false : true,
          });
      }

      //this.trainingForm.get('endTime')?.setErrors({ required: true });
    } else {
      this.trainingForm.get('startTime')?.clearValidators();
      this.trainingForm.get('startTime')?.setErrors(null);

      this.trainingForm.get('endTime')?.clearValidators();
      this.trainingForm.get('endTime')?.setErrors(null);

    }
    this.trainingForm.updateValueAndValidity();
    var invalidControls = this.findInvalidControls();
    var options = new ILAEvalMethodVM();
    options.evaluationMethod = ilaInfo.trainingEvalMethods;
    options.useForEMP = completed;
    await this.ilaService
      .saveEvalMethodData(this.trainingForm.get('ilaId')?.value, options)
      .then((_) => {
        this.alert.successToast(
          'Training Evaluation Method and EMP Usage Data Saved Successfully'
        );
      })
      .finally(() => {
      });
  }

  async goBack() {
    var dashboard = localStorage.getItem('Dashboard');
    if (dashboard === '1') {
      this.router.navigate(['home/dashboard']);
    } else {
      this.router.navigate(['implementation/sc']);
    }
  }

  async stepNext() {
    this.currentIndex = 1;
    this.stepper.selectedIndex = 1;
  }

  toggleMainMenu() {
    //this.databroadcastSrvc.ToggleMainMenu.next('');
    this.store.dispatch(sideBarToggle());
  }

  async selectedChanged(event: any) {
    if (event.selectedIndex === 0) {
      if (this.trainingId !== '') {
        this.getInfo(this.trainingId);
      }

      this.currentIndex = 0;
    } else if (event.selectedIndex === 1) {
      if (this.trainingId !== '') {
        this.getInfo(this.trainingId);
      } else {
        await this.saveInfo();
      }
      this.currentIndex = 1;
     
    } else if (event.selectedIndex === 2) {
      if (this.trainingId !== '') {
        this.getInfo(this.trainingId);
        this.getSelfRegistrationILAId();
      }
      // this.stepper.next();
      this.currentIndex = 2;
    } else if (event.selectedIndex === 3) {

      this.getReviewData();
      this.currentIndex = 3;
    }
  }

  selectedClassId: any = '';

  async openFlyInPanel(templateRef: any) {
    this.qualInfo = this.dataSourceTaskEval.data;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  classData!: any;

  openEnrollPanel(flyPanelRef: any, dialogtpRef: any) {
    if (this.allSchedules && this.allSchedules.length > 1) {
      const dialogRef = this.dialog.open(dialogtpRef, {
        width: '600px',
        height: 'auto',
        hasBackdrop: true,
        disableClose: true,
      });
    } else {
      var date = this.allSchedules
        ? `${this.datePipe.transform(
          this.allSchedules[0].startDateTime,
          'MM-dd-yy'
        )} - ${this.datePipe.transform(
          this.allSchedules[0].endDateTime,
          'MM-dd-yy'
        )}`
        : '';
      this.classData = {id: this.trainingId, date: date, classSize: this.classSize};
      const portal = new TemplatePortal(flyPanelRef, this.vcf);
      this.flyPanelService.open(portal);
    }
  }

  drop(event: CdkDragDrop<any[]>) {
    moveItemInArray(this.ilaItems, event.previousIndex, event.currentIndex);
  }

  onReady(editor: any) {
    editor.plugins.get('FileRepository').createUploadAdapter = function (
      loader: any
    ) {
      return new UploadAdapter(loader);
    };
  }

  ToggleClassesGroupBy(groupBy: string) {
    switch (groupBy) {
      case 'EMP Settings':
        this.getTestLinkedILAs(this.trainingForm.get('ilaId')?.value);
        
        break;
      case 'Segments Review':
        break;
      default:
        break;
    }

    this.viewSelected = groupBy;
  }

  dropTable(event: any) {
    const prevIndex = this.dataSource.data.findIndex(
      (d) => d === event.item.data
    );
    moveItemInArray(this.dataSource.data, prevIndex, event.currentIndex);
    this.dataSource = new MatTableDataSource(this.dataSource.data);
    this.isReordered = true;
  }

  isAllSelected(idx: any) {
    const numSelected = this.selection.selected.length;
    const numRows = this.dataSourceEmployees[idx].data.length;
    return numSelected === numRows;
  }

  selectrow(row: any, index: any) {
    this.empSelections[index].toggle(row);
    this.selection.selected.forEach((v, i) => {
      this.unlinkIds.push(v);
    });
  }

  EnrollStudentSelected() {
    this.getSelfRegistrationEmp();
    this.flyPanelService.close();
  }

  tqSelected(event: any) {
    this.dataSourceTaskEval.data = event;
    if (this.dataSourceTaskEval.data.length > 0) {
      this.addTQTaskEvaluation();
    }
  }

  recurrenceCreated(event: any) {
    const ClassInfo = event.map(classItem => ({
    ...classItem,
    disableCheckbox: classItem._startDate < this.currentDate
  }));
    this.dataSource.data = ClassInfo;
  }

  tqStudentEvalSelected(event: any) {
    this.dataSourceTQEval.data = event;
    if (this.dataSourceTQEval.data.length > 0) {
      this.addStudentEvaluation();
    }
  }

  addStudentEvaluation() {
    let createStudentEvaluation: EMPSettingStudentEval = {
      ilaId: this.trainingForm.get('ilaId')?.value,
      isAllQuestionMandatory: false,
      studentEvalAudienceID: null,
      studentEvalAvailabilityID: null,
      studentEvalFormIDs: this.dataSourceTQEval.data.map((x) => x.id),
    };
    this.ilaService
      .createStudentEvaluationRelease(
        this.trainingForm.get('ilaId')?.value,
        createStudentEvaluation
      )
      .then((res) => {
        this.getStudentEvaluations();
        this.alert.successToast(`Evaluations Linked Successfully.`);
      })
      .catch((res: any) => {

        this.alert.errorToast(res);
      });
  }

  // getTQTaskEvaluations() {
  //   this.ilaService
  //     .getTQTaskEvaluations(this.trainingForm.get('ilaId')?.value)
  //     .then((res) => {
  //       this.dataSourceTaskEval.data = res;
  //     })
  //     .catch((res: any) => {
  //       this.alert.errorToast(res);
  //     });
  // }

  getStudentEvaluations() {
    this.ilaService
      .getStudentEvaluationRelease(this.trainingForm.get('ilaId')?.value)
      .then((res) => {

        this.student_Evaluation_List = res;
      })
      .catch((res: any) => {
        this.alert.errorToast(res);
      });
  }

  onTabChanged(event) {
    switch (event.index) {
      case 0:
        this.getTestLinkedILAs(this.trainingForm.get('ilaId')?.value);
        break;
        case 1:
          this.getCBTReleaseByILAId();
          break;
          case 2:
        this.getLinkedTaskObjectives();
         break;
      case 3:
        this.getEvalReleaseByILAId();
        this.getStudentEvaluations();
        this.getClassesByIlaId();

        break;
      case 4:
        break;

      default:
        break;
    }
  }

  masterToggle(idx: any) {
    this.isAllSelected(idx)
      ? this.selection.clear()
      : this.dataSourceEmployees[idx].data.forEach((row) =>
        this.selection.select(row)
      );

    this.unlinkIds = [];
    this.selection.selected.forEach((v, i) => {
      this.unlinkIds.push(v.id);
    });
  }

  selectedEMPId = '';

  deleteTraining(templateRef: any, row, id: any, index: any) {
    this.employeeIndex = row;
    this.selectedIDX = index;
    // this.unlinkIds.push(this.dataSourceEmployees[idx].data[this.employeeIndex]);
    var person: any;
    // for(let i = 0; i<this.dataSourceEmployees.length;i++){
    //
    //   var emp = this.dataSourceEmployees[i].data.find((emp)=>{
    //     return emp.id === id;
    //   });
    //   if(emp !== undefined){
    //     person = emp.person;
    //     break;
    //   }
    // }
    if (this.allSchedules) {
      person = this.allSchedules[index].classSchedule_Employee.find((data) => {
        return data.employeeId === id;
      })?.employee.person;
    }
    var ila: any = this.ila_list.find((f: any) => {
      return f.id === this.trainingForm.get('ilaId')?.value;
    });
    var schedule = this.allSchedules ? this.allSchedules[index] : null;
    if (schedule) {
      this.selectedClassId = schedule.id;
      this.selectedEMPId = id;
    }
    this.deleteDescription = `You are selecting to remove ${person.firstName} ${person.lastName
    } from Training ${ila?.number} ${ila?.name
    } scheduled from ${this.datePipe.transform(
      schedule?.startDateTime,
      'MM-dd-yy, h:mm a'
    )}  to ${this.datePipe.transform(
      schedule?.endDateTime,
      'MM-dd-yy, h:mm a'
    )} .`;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  selectedIDX = 0;

  Delete(e: any) {
    let deleteEmp: TrainingStudentCreationOptions =
      new TrainingStudentCreationOptions();
    deleteEmp.classScheduleId = this.selectedClassId;
    deleteEmp.employeeIds = [];
    deleteEmp.employeeIds.push(this.selectedEMPId);
    // this.dataSourceEmployees[this.selectedIDX].data[this.employeeIndex];
    this.trainingSevc
      .unLinkedEmployees(this.selectedClassId, deleteEmp)
      .then(async (res: any) => {
        this.alert.successToast(await this.labelPipe.transform('Employee') + ' Removed from Class');
        this.getSelfRegistrationEmp();
      });
  }

  async deleteMultipleEmployees(templateRef: any, idx: any) {
    var ila: any = this.ila_list.find((f: any) => {
      return f.id === this.trainingForm.get('ilaId')?.value;
    });
    this.deleteDescription = `You are selecting to remove the following ` + await this.labelPipe.transform('Employee') + `s from Training ${ila.number
    } ${ila.name} from ${this.datePipe.transform(
      this.allSchedules ? this.allSchedules[idx].startDateTime : '',
      'MM/dd/yy, h:mm a'
    )} ${this.datePipe.transform(
      this.allSchedules ? this.allSchedules[idx].endDateTime : '',
      'MM/dd/yy, h:mm a'
    )}`;
    this.selectedIDX = idx;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
      data: {dialogData: this.unlinkIds},
    });
  }

  startTimeChange() {
    if (this.trainingForm.get('startTime')?.value !== '') {
      /*   let DateWithZeroTime = new Date();
          DateWithZeroTime.setHours(0);
          DateWithZeroTime.setMinutes(0);
          DateWithZeroTime.setMilliseconds(0);

          let startDateTIme = new Date(this.trainingForm.get('startDate')?.value);
    /*       let startTime = this.trainingForm.get('startTime')?.value === '' ? '0:00' : this.trainingForm.get('startTime')?.value;
     */
      /*  let startTime = this.trainingForm.get('startTime')?.value;
          const [hours, minutes] = startTime.split(':');
          startDateTIme.setHours(Number(hours));
          startDateTIme.setMinutes(Number(minutes)); */

      let startDateTIme = `${this.trainingForm.get('startDate')?.value}T${this.trainingForm.get('startTime')?.value
      }`;
      this.trainingForm.patchValue({
        startDateTime: startDateTIme,
      });
    } else {
      this.trainingForm.patchValue({
        startDateTime: this.trainingForm.get('startDate')?.value,
      });
    }
  }

  endTimeChange() {
    if (this.trainingForm.get('endTime')?.value !== '') {
      /*     let DateWithZeroTime = new Date();
        DateWithZeroTime.setHours(0);
        DateWithZeroTime.setMinutes(0);
        DateWithZeroTime.setMilliseconds(0);
        let endDateTIme = new Date(this.trainingForm.get('endDate')?.value);

        //let endTime = this.trainingForm.get('endTime')?.value === '' ? '0:00' : this.trainingForm.get('endTime')?.value;
        let endTime =  this.trainingForm.get('endTime')?.value;
        const [hours, minutes] = endTime.split(':');
        endDateTIme.setHours(Number(hours));
        endDateTIme.setMinutes(Number(minutes)); */
      let startDateTIme = `${this.trainingForm.get('endDate')?.value}T${this.trainingForm.get('endTime')?.value
      }`;

      this.trainingForm.patchValue({
        endDateTime: startDateTIme,
      });
    } else {
      this.trainingForm.patchValue({
        endDateTime: this.trainingForm.get('endDate')?.value,
      });
    }
  }

  StartDateTime = new Date();
  endDateTime = new Date();

  patchFormValues(values) {
    var startTime;
    var endTime;
    if (values.isStartAndEndTimeEmpty === true) {
      startTime = ''
      endTime = ''
    } else {
      startTime = new Date(values.startDateTime).getHours() === 0
        ? ''
        : this.datePipe.transform(values.startDateTime, 'HH:mm')

      endTime = new Date(values.endDateTime).getHours() === 0
        ? ''
        : this.datePipe.transform(values.endDateTime, 'HH:mm')
    }
    //this.onStartDateSelect();
    //this.dayEvent = this.getWeekDayFromDate(values.startDate);
    this.trainingForm.patchValue({
      providerId: values.providerId,
      ilaId: values.ilaId,
      locationId: values.locationId,
      instructorId: values.instructorId,
      courseInstruction: values.specialInstruction??"",
      startDate: this.datePipe.transform(values.startDateTime, 'yyyy-MM-dd'),
      startTime: startTime,
      endDate: this.datePipe.transform(values.endDateTime, 'yyyy-MM-dd'),
      endTime: endTime,
      classSize: values.classSize,
      webLink: values.webinarLink??"",
      isPubliclyAvailableClass: values.isPubliclyAvailable

    });

    this.getProviders();
    this.StartDateTime = new Date(values.startDateTime);
    this.endDateTime = new Date(values.endDateTime);
    //this.trainingForm.updateValueAndValidity();
    // this.StartDateTime = this.formatTime(values.startDateTime)
    // this.endDateTime = this.formatTime(values.endDateTime)
  }

  patchSelfRegistration(values) {
    this.trainingForm.patchValue({
      selfRegPk: values.id,
      makeAvailableForSelfReg: values.makeAvailableForSelfReg,
      sendApprovedEmail: values.sendApprovedEmail,
      requireAdminApproval: values.requireAdminApproval,
      acknowledgeRegDisclaimer: values.acknowledgeRegDisclaimer,
      regDisclaimer: values.regDisclaimer,
      limitForLinkedPositions: values.limitForLinkedPositions,
      closeRegOnStartDate: values.closeRegOnStartDate,
      classSize: values.classSize,
      enableWaitlist: values.enableWaitlist,
    });
  }

  previewTest(testId) {
    this.router.navigate(['/dnd/tests/publish/' + testId]);
  }

  recurrenceId: boolean = false;

  async getInfo(id) {


    await this.trainingSevc
      .get(id)
      .then((res) => {
        this.useILAForEmp = res?.useForEmp ?? false;

        if( res.classSize) this.classSize = res.classSize;
        var startDateString = '';
        var endDateString = '';
        if (res.isStartAndEndTimeEmpty === true) {

          startDateString = this.datePipe.transform(
            res.startDateTime,
            'yyyy-MM-dd'
          );
          endDateString = this.datePipe.transform(
            res.endDateTime,
            'yyyy-MM-dd'
          );

          this.trainingStartDateTime = startDateString;
          this.trainingEndtDateTime = endDateString;
          this.isTimeEmpty = true;
        } else {
          startDateString = this.datePipe.transform(
            res.startDateTime,
            'yyyy-MM-dd hh:mm a'
          );

          const utcStartDateTime = new Date(startDateString.toString() + ' UTC');
          // Convert UTC date and time to local time
          const localstartDateTimeString = utcStartDateTime.toLocaleString();


          res.startDateTime = new Date(Date.parse(localstartDateTimeString));


          endDateString = this.datePipe.transform(
            res.endDateTime,
            'yyyy-MM-dd hh:mm a'
          );
          //const utcendtDateTimeString = res.startDateTime.toDateString();
          const utcendtDateTime = new Date(endDateString.toString() + ' UTC');
          // Convert UTC date and time to local time
          const localendDateTimeString = utcendtDateTime.toLocaleString();
          res.endDateTime = new Date(Date.parse(localendDateTimeString));

          this.trainingStartDateTime = this.datePipe.transform(
            res.startDateTime,
            'yyyy-MM-dd hh:mm a'
          );
          this.trainingEndtDateTime = this.datePipe.transform(
            res.endDateTime,
            'yyyy-MM-dd hh:mm a'
          );

        }
        //res.startDateTime = this.changeStartSateForEdit(res.startDateTime);
        //res.endDateTime = this.changeEndSateForEdit(res.endDateTime);
        this.ilaId = res.ilaId;
        this.recurrenceId = res.isRecurring;
        this.dateInformation = {
          duration: intervalToDuration({
            start: new Date(res.startDateTime),
            end: new Date(res.endDateTime),
          }),
          startTime: res.startDateTime,
          endTime: res.endDateTime,
          startDateTime: res.startDateTime,
          endDateTime: res.endDateTime,
          ILAId: this.trainingForm.get('ilaId')?.value,
          instructorName: this.instructor_list.find(
            (x) => x.id === res.instructorId
          )?.instructorName,
          locationName: this.location_list.find((x) => x.id === res.locationId)
            ?.locName,
          isClassPubliclyAvailable: res.isPubliclyAvailable
        };
        this.patchFormValues(res);
        if (this.recurrenceId) {
          this.getClassRecurrence();
        }

        this.getSegments();
      })
      .catch((res: any) => {
        this.alert.errorToast(res);
      });
  }

  async saveInfo() {
    var isStartEndDateEmpty = false;
    if (this.trainingForm.get('startTime')?.value === '' && this.trainingForm.get('endTime')?.value === '') {
      isStartEndDateEmpty = true;
    }
    this.startTimeChange();
    this.endTimeChange();
    let createOpt: TrainingCreationOptions = {
      providerID: this.trainingForm.get('providerId')?.value,
      ilaid: this.trainingForm.get('ilaId')?.value,
      locationId: this.trainingForm.get('locationId')?.value,
      instructorId: this.trainingForm.get('instructorId')?.value,
      specialInstructions: this.trainingForm.get('courseInstruction')?.value,
      startDateTime: this.trainingForm.get('startDateTime')?.value,
      endDateTime: this.trainingForm.get('endDateTime')?.value,
      webinarLink: this.trainingForm.get('webLink')?.value,
      classSize: this.trainingForm.get('classSize')?.value,
      isPubliclyAvailable: this.trainingForm.get('isPubliclyAvailableClass')?.value,
      isStartAndEndTimeEmpty: isStartEndDateEmpty,
      recurringOptions: this.dataSource.data.map((data) => {
        var startDateRec = new Date(data._startDate);
        data._startDate = startDateRec.toUTCString();
        var endDateRec = new Date(data._endDate);
        data._endDate = endDateRec.toUTCString();
        return {
          startDate: data._startDate,
          endDate: data._endDate,
          isPubliclyAvailable: data.isClassPubliclyAvailable
        };
      }),
    };
    var startDate = new Date(createOpt.startDateTime);
    createOpt.startDateTime = startDate.toUTCString();
    var endDate = new Date(createOpt.endDateTime);
    createOpt.endDateTime = endDate.toUTCString();
    if (this.trainingId !== '') {
      this.trainingSevc
        .update(this.trainingId, createOpt)
        .then((res) => {
          this.stepper.selected?.completed ? true : undefined;
          if (this.stepper) {
            setTimeout(() => {
              this.stepNext()
            }, 1);
          }
          this.alert.successToast(`Class has been updated `);
        })
        .catch((res: any) => {
          this.alert.errorToast(res);
        });
    } else {
      await this.trainingSevc
        .create(createOpt)
        .then(async (res) => {
          localStorage.setItem("stepNext", "true");
          if (this.employeeIds) {
            var options: TrainingStudentCreationOptions = new TrainingStudentCreationOptions()
            options.classScheduleId = res.classData.id;
            options.employeeIds = this.employeeIds;
            await this.trainingSevc.createEmployees(options);
          }
          this.trainingId = res.classData.id;
          await this.createClassTestReleaseByILA();
          await this.createTQReleaseByILA();
          await this.createTQClassEvaluatorsByILA();
          this.router.navigate([
            '/implementation/sc/editTraining/' + res.classData.id,
          ]);
          this.stepper.selected?.completed ? true : undefined;
          this.alert.successToast(`Class has been created `);
        });
    }
  }

  async saveUpdateEnrollStudent() {
    let createOpt: ClassSchedule_SelfRegCreateOptions = {
      classScheduleId: this.trainingId,
      makeAvailableForSelfReg: this.trainingForm.get('makeAvailableForSelfReg')
        ?.value,
      requireAdminApproval: this.trainingForm.get('requireAdminApproval')
        ?.value,
      sendApprovedEmail: this.trainingForm.get('sendApprovedEmail')?.value,
      acknowledgeRegDisclaimer: this.trainingForm.get(
        'acknowledgeRegDisclaimer'
      )?.value,
      regDisclaimer: this.trainingForm.get('regDisclaimer')?.value,
      limitForLinkedPositions: this.trainingForm.get('limitForLinkedPositions')
        ?.value,
      closeRegOnStartDate: this.trainingForm.get('closeRegOnStartDate')?.value,
      classSize: this.trainingForm.get('classSize')?.value,
      enableWaitlist: this.trainingForm.get('enableWaitlist')?.value,
      updateRecurrences :(this.dataSource?.data.length ?? 0) > 0
    };

    this.scheduleService
      .CreateClassScheduleSelfRegistrationAsync(createOpt)
      .then((res) => {
        this.alert.successToast(`Self Registration  has been created `);
      })
      .catch((res: any) => {
        this.alert.errorToast(res);
      });

  }

  printStartDateTime(startDateTime, EndDateTime) {
  }

  async getSegments() {
    await this.ilaService
      .getLinkedSegments(this.trainingForm.get('ilaId')?.value)
      .then((res: any) => {
        this.lsegs = res;

      })
      .catch((err: any) => {
        console.error(err);
      });
  }
  onTogglePubliclyAvailable(event: MatCheckboxChange, element: any) {
  element.isClassPubliclyAvailable = event.checked;
  if (event.checked) {
    this.selection.select(element);
  } else {
    this.selection.deselect(element);
  }
}

  async patchTestRelease(values) {
    this.trainingForm.patchValue({
      testPK: values.id,
      finalTestId: values.finalTestId,
      preTestId: values.preTestId,
      usePreTestAndTest: values.usePreTestAndTest,
      preTestRequired: values.preTestRequired,
      preTestAvailableOnEnrollment: values.preTestAvailableOnEnrollment,
      preTestAvailableOneStartDate: values.preTestAvailableOneStartDate,
      showStudentSubmittedPreTestAnswers:
      values.showStudentSubmittedPreTestAnswers,
      showCorrectIncorrectPreTestAnswers:
      values.showCorrectIncorrectPreTestAnswers,
      makeAvailableBeforeDays: values.daysOrweeks === 1 ? 'Days' : values.daysOrweeks === 2 ? "Weeks" : 0,
      weeks: values.makeAvailableBeforeWeeks,
      days: values.makeAvailableBeforeDays,
      finalTestPassingScore: values.finalTestPassingScore,
      makeFinalTestAvailableImmediatelyAfterStartDate:
      values.makeFinalTestAvailableImmediatelyAfterStartDate,
      makeFinalTestAvailableOnClassEndDate:
      values.makeFinalTestAvailableOnClassEndDate,
      makeFinalTestAvailableAfterCBTCompleted:
      values.makeFinalTestAvailableAfterCBTCompleted,
      makeFinalTestAvailableOnSpecificTime:
      values.makeFinalTestAvailableOnSpecificTime,
      finalTestSpecificTimePrior: values.finalTestSpecificTimePrior,
      finalTestDueDate: values.finalTestDueDate,
      showStudentSubmittedFinalTestAnswers:
      values.showStudentSubmittedFinalTestAnswers,
      showStudentSubmittedRetakeTestAnswers:
      values.showStudentSubmittedRetakeTestAnswers,
      showCorrectIncorrectFinalTestAnswers:
      values.showCorrectIncorrectFinalTestAnswers,
      showCorrectIncorrectRetakeTestAnswers:
      values.showCorrectIncorrectRetakeTestAnswers,
      autoReleaseRetake: values.autoReleaseRetake,
      retakeEnabled: values.retakeEnabled,
      numberOfRetakes: values.numberOfRetakes,
    });
    let setValue = this.testAvailabiilityTime.find(
      (x) =>
        x.time ==
        this.trainingForm.get('makeFinalTestAvailableOnSpecificTime')
          ?.value &&
        x.prior == this.trainingForm.get('finalTestSpecificTimePrior')?.value
    );
    this.trainingForm.controls['setAvailabilityTimeTest'].setValue(setValue);
  }

  async patchCBTRelease(values) {
    ;
    this.trainingForm.patchValue({
      cbtPK: values.id,
      cbtRequiredForCource: values.cbtRequiredForCource,
      releaseCBTLearningContract: values.releaseCBTLearningContract,
      cbtLearningContractInstructions: values.cbtLearningContractInstructions,
      makeAvailableOnClassStartDate: values.makeAvailableOnClassStartDate,
      makeAvailableOnClassEndDate: values.makeAvailableOnClassEndDate,
      makeAvailableAfterPretestCompleted:
      values.makeAvailableAfterPretestCompleted,
      cbtDueDate: values.cbtDueDate,
    });
  }

  async patchEvalRelease(values) {
    this.evalForm.patchValue({
      evaluationPK: values.id,
      evaluationRequired: values.evaluationRequired,
      evaluationUsedToDeployStudentEvaluation:
      values.evaluationUsedToDeployStudentEvaluation,
      evaluationAvailableOnStartDate: values.evaluationAvailableOnStartDate,
      evaluationAvailableOnEndDate: values.evaluationAvailableOnEndDate,
      finalGradeRequired: values.finalGradeRequired,
      releaseOnSpecificTimeAfterClassEndDate:
      values.releaseOnSpecificTimeAfterClassEndDate,
      releaseAfterEndTime: values.releaseAfterEndTime,
      ////
      releasePrior: values.releasePrior,
      releaseAfterGradeAssigned: values.releaseAfterGradeAssigned,
      evaluationDueDate: values.evaluationDueDate,
      empSettingsReleaseType : values.empSettingsReleaseTypeId ?? this.defaultEmpSettingReleaseTypeId,
    });

    let setValue = this.testEval.find(
      (x) =>
        x.time == this.evalForm.get('releaseAfterEndTime')?.value &&
        x.prior == this.evalForm.get('releasePrior')?.value
    );
    this.evalForm.controls['setAvailabilityTimeEvaluate'].setValue(setValue);
    this.updatedEvalValues = this.evalForm.value;
    this.evaluationReleaseCheckNUll = false;
    Object.keys(values).forEach((key) => {
      if (this.evalForm.contains(key)) {
        if (
          key !== 'releasePrior' &&
          key !== 'evaluationRequired' &&
          key !== 'evaluationUsedToDeployStudentEvaluation'
        ) {
          values[key] === true ? this.evalSelection.select(key) : null;
        }
      }
    });
  }

  async saveInfoCBTRelease() {
    let createOpt: EMPSettingCBTCreationOptions = {
      ilaId: this.trainingForm.get('ilaId')?.value,
      cbtDueDate: this.trainingForm.get('cbtDueDate')?.value,
      cbtLearningContractInstructions: this.trainingForm.get(
        'cbtLearningContractInstructions'
      )?.value,
      cbtRequiredForCource: this.trainingForm.get('cbtRequiredForCource')
        ?.value,
      makeAvailableAfterPretestCompleted: this.trainingForm.get(
        'makeAvailableAfterPretestCompleted'
      )?.value,
      makeAvailableOnClassEndDate: this.trainingForm.get(
        'makeAvailableOnClassEndDate'
      )?.value,
      makeAvailableOnClassStartDate: this.trainingForm.get(
        'makeAvailableOnClassStartDate'
      )?.value,
      releaseCBTLearningContract: this.trainingForm.get(
        'releaseCBTLearningContract'
      )?.value,
    };
    if (!this.cbtReleaseCheckNUll) {
      this.ilaService
        .updateIlaCBT(this.trainingForm.get('cbtPK')?.value, createOpt)
        .then((res) => {
          this.patchCBTRelease(res);
          this.alert.successToast(`cbt has been updated `);
        })
        .catch((res: any) => {
          this.alert.errorToast(res);
        });
    } else {
      this.ilaService
        .createIlaCBT(createOpt)
        .then((res) => {
          this.patchCBTRelease(res);
          this.alert.successToast(`Cbt has been created `);
        })
        .catch((res: any) => {
          this.alert.errorToast(res);
        });
    }
  }

  async saveInfoEvalRelease() {
    this.evalForm;
    let createOpt: EMPSettingEvaluationCreationOptions = {
      ilaId: this.trainingForm.get('ilaId')?.value,
      evaluationAvailableOnEndDate: this.evalForm.get(
        'evaluationAvailableOnEndDate'
      )?.value,
      evaluationAvailableOnStartDate: this.evalForm.get(
        'evaluationAvailableOnStartDate'
      )?.value,
      evaluationDueDate: this.evalForm.get('evaluationDueDate')?.value,
      empSettingsReleaseTypeId:this.evalForm.get('empSettingsReleaseType')?.value,
      evaluationRequired: this.evalForm.get('evaluationRequired')?.value,
      evaluationUsedToDeployStudentEvaluation: this.evalForm.get(
        'evaluationUsedToDeployStudentEvaluation'
      )?.value,
      finalGradeRequired: this.evalForm.get('finalGradeRequired')?.value,
      releaseAfterEndTime: this.evalForm.get('setAvailabilityTimeEvaluate')
        ?.value?.time,
      releaseAfterGradeAssigned: this.evalForm.get('releaseAfterGradeAssigned')
        ?.value,
      releaseOnSpecificTimeAfterClassEndDate: this.evalForm.get(
        'releaseOnSpecificTimeAfterClassEndDate'
      )?.value,
      releasePrior:
        this.evalForm.get('setAvailabilityTimeEvaluate')?.value?.prior ?? false,
    };

    if (!this.evaluationReleaseCheckNUll) {
      this.ilaService
        .updateTestEvaluation(
          this.evalForm.get('evaluationPK')?.value,
          createOpt
        )
        .then((res) => {
          this.patchEvalRelease(res);
          this.alert.successToast(`Evaluation has been updated `);
        })
        .catch((res: any) => {
          this.alert.errorToast(res);
        });
    } else {
      this.ilaService
        .createTestEvaluation(createOpt)
        .then((res) => {
          this.patchEvalRelease(res);
          this.alert.successToast(`Evaluation has been created `);
        })
        .catch((res: any) => {
          this.alert.errorToast(res);
        });
    }
  }

  getTestReleaseByILAId() {
    this.ilaService
      .getTestRelease(this.trainingForm.get('ilaId')?.value)
      .then((res: any) => {
        if (res !== null) {
          this.patchTestRelease(res);
        } else {
          this.testReleaseCheckNUll = true;
        }
      });
  }

  getCBTReleaseByILAId() {
    this.ilaService
      .getIlaCBT(this.trainingForm.get('ilaId')?.value)
      .then((res: any) => {
        if (res !== undefined) {
          this.patchCBTRelease(res);
        } else {
          this.cbtReleaseCheckNUll = true;
        }
      });
  }

  getEvalReleaseByILAId() {
    this.empSettingsReleaseTypeService.getEmpSettingsReleaseTypes().then(res=>{
      this.empSettingsReleaseTypes = res;
      this.defaultEmpSettingReleaseTypeId = this.empSettingsReleaseTypes.find(x=> x.typeName == "Days")?.typeId;
      this.evalForm.get('empSettingsReleaseType')?.setValue(this.defaultEmpSettingReleaseTypeId);
    })
    this.ilaService
      .getTestEvaluation(this.trainingForm.get('ilaId')?.value)
      .then((res: any) => {
        if (res !== null) {
          this.patchEvalRelease(res);
          this.evaluationReleaseCheckNUll = false;
        } else {
          this.evaluationReleaseCheckNUll = true;
        }
      });
  }

  async getSelfRegistrationILAId() {
    await this.scheduleService.GetClassScheduleSelfRegistrationAsync(this.trainingId)
      .then((res: any) => {
        this.getSelfRegistrationEmp();
        this.readyILALinkedPositions(this.ilaId);
        if (res !== null) {

          this.patchSelfRegistration(res);
        } else {
          this.selfRegistrationCheckNUll = true;
        }
      });
  }

  async readyILALinkedPositions(ilaId: any) {
    this.linkedPositionToIla = await this.ilaService.getLinkedPositions(ilaId);
  }

  getLinkedTaskObjectives() {
    this.taskService
      .getTasksLinkedToILA(this.trainingForm.get('ilaId')?.value)
      .then((res: any) => {
        if (res !== null) {
          this.dataSourceTask.data = res;
          this.evaluationReleaseCheckNUll = false;
        } else {
          this.evaluationReleaseCheckNUll = true;
        }
      });
  }

  allSchedules!: ClassSchedules[] | null;
  empSelections: any[] = [];

  getSelfRegistrationEmp() {
    this.trainingSevc
      .getRecurrenceEmployees(this.trainingId)
      .then((res: ClassSchedules[]) => {
        if (res !== null) {
          this.allSchedules = res;

          this.dataSourceEmployees = [];
          this.empSelections = [];
          for (let i = 0; i < this.allSchedules.length; i++) {
            var startDateString = this.datePipe.transform(
              this.allSchedules[i].startDateTime,
              'yyyy-MM-dd hh:mm a'
            );

            const utcStartDateTime = new Date(startDateString.toString() + ' UTC');
            // Convert UTC date and time to local time
            const localstartDateTimeString = utcStartDateTime.toLocaleString();
            this.allSchedules[i].startDateTime = new Date(Date.parse(localstartDateTimeString));
            var endDateString = this.datePipe.transform(
              this.allSchedules[i].endDateTime,
              'yyyy-MM-dd hh:mm a'
            );
            //const utcendtDateTimeString = res.startDateTime.toDateString();
            const utcendtDateTime = new Date(endDateString.toString() + ' UTC');
            // Convert UTC date and time to local time
            const localendDateTimeString = utcendtDateTime.toLocaleString();
            this.allSchedules[i].endDateTime = new Date(Date.parse(localendDateTimeString));
            // this.allSchedules[i].startDateTime = this.changeStartSateForEdit(this.allSchedules[i].startDateTime);
            // this.allSchedules[i].endDateTime = this.changeStartSateForEdit(this.allSchedules[i].endDateTime);
          }
          // this.allSchedules[0].startDateTime = this.changeStartSateForEdit(
          //   this.allSchedules[0].startDateTime
          // );
          // this.allSchedules[0].endDateTime = this.changeStartSateForEdit(
          //   this.allSchedules[0].endDateTime
          // );
          res.forEach((data) => {
            this.empSelections.push(new SelectionModel(true));

            data.classSchedule_Employee.forEach(emp => {
              emp.employee.employeePositions = emp.employee.employeePositions.filter(r => r.active);
            })
            var source = new MatTableDataSource(data.classSchedule_Employee.filter(r => r.isEnrolled));
            this.dataSourceEmployees.push(source);
          });
        }
      });
  }

  getTestLinkedILAs(id: any) {
    this.testService.getTestLinkedtoILA(id).then((res: any) => {
      let temp: any[] = [];
      res.forEach((data) => {
        temp.push({
          id: data.id,
          testTitle: data.testTitle,
          testType: data.testType,
          testNum: Number(data.testNum),
          numOfQuestion: data.numberOfQuestions,
          testStatus: data.testStatus,
          active: data.active,
          isPublished: data.isPublished,
        });
      });
      this.TestLinkedILAsReTakeTest_List = temp.filter(
        (x) => x.testType === 'Retake'
      );
      this.TestLinkedILAsPreTest_List = temp.filter(
        (x) => x.testType === 'Pretest'
      );
      this.TestLinkedILAsTest_List = temp.filter(
        (x) => x.testType === 'Final Test'
      );
    });
  }

  keyPressNumbers(event: any) {
    var charCode = event.which ? event.which : event.keyCode;
    // Only Numbers 0-9
    if (charCode < 48 || charCode > 57) {
      event.preventDefault();
      return false;
    } else {
      return true;
    }
  }

  keyPressNumbersRetake(event: any) {
    ;
    this.numberOfTimeRetake = Array.from({length: event.key}, (x, i) => i);
    var charCode = event.which ? event.which : event.keyCode;
    // Only Numbers 0-9
    if (charCode < 48 || charCode > 57) {
      event.preventDefault();
      return false;
    } else {
      if (this.numberOfTimeRetake.length > 3) {
        this.numberOfTimeRetake = [];
        return false;
      } else {
        return true;
      }
    }
  }

  updateReTake(item) {
    return 'Retake ' + (item + 1);
  }

  getTestReTakeId(event) {
    ;
    this.selectedRetakeId.push(event);
  }

  async getClassesByIlaId() {
    await this.trainingSevc
      .getClassesByIla(this.trainingForm.get('ilaId')?.value)
      .then((res) => {
        this.classes_List = res;
        this.getStudentEvaluations();
      });
  }

  async goNext() {
    if(this.currentIndex === 1){
      if(this.useILAForEmp && this.ilaTestRelease && typeof this.ilaTestRelease.saveInfoTestRelease === 'function'){
        await this.ilaTestRelease.saveInfoTestRelease();
      }
      if(this.useILAForEmp && this.ilaEmpTqSettings && typeof this.ilaEmpTqSettings.saveTQRelease === 'function'){
        await this.ilaEmpTqSettings.saveTQRelease();
      }  
    }
    if(this.currentIndex === 2){
      await this.saveUpdateEnrollStudent();
    }
    this.currentIndex = 3;
    this.stepper.next();
  }

  async getLinkedStudentEvaluation() {
    await this.studentEvaluationService
      .getStudentEvaluationClasses(this.trainingForm.get('ilaId')?.value)
      .then((res) => {
        this.linked_classes_List = res;
        this.dataSourceEvalReleaseStudent.data = this.linked_classes_List;
      });
  }

  changeClass(event, studentEvaluationId) {
    let createOpt: EMPSettingStudentEvaluationUpdateOption = {
      ilaId: this.trainingForm.get('ilaId')?.value,
      evalId: studentEvaluationId,
      audienceId: event,
    };
    this.studentEvaluationService
      .studentEvaluationLinkUpdate(createOpt)
      .then((res) => {
        this.getStudentEvaluations();
        //this.trainingForm.get('changeClass').setValue(event);

        this.alert.successToast(`Evaluation has been updated. `);
      })
      .catch((res: any) => {
        this.alert.errorToast(res);
      });
  }

  SelectedClassIds(event) {
    let createOptSecond: EMPSettingStudentEvaluationCreationOption = {
      ilaId: this.trainingForm.get('ilaId')?.value,
      selection: 'multiple',
      classScheduleIds: event.map((x) => x.id),
    };
    this.studentEvaluationService
      .studentEvaluationAddClasses(
        this.selectedStudentEvaluationId,
        createOptSecond
      )
      .then((res) => {
        this.getStudentEvaluations();
        this.alert.successToast(`Evaluation has been created `);
      })
      .catch((res: any) => {
        this.alert.errorToast(res);
      });
  }

  dataSelcted(data: any, templateRef: any) {
    this.classData = data;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  DeleteMultiple() {
    var csEMPs = this.empSelections[this.selectedIDX]
      .selected as ClassSchedule_Employee[];
    var empIds = csEMPs.map((data) => {
      return data.employeeId;
    });
    var scheduleId = this.allSchedules
      ? this.allSchedules[this.selectedIDX].id
      : '';
    let deleteEmp: TrainingStudentCreationOptions =
      new TrainingStudentCreationOptions();
    deleteEmp.classScheduleId = scheduleId;
    deleteEmp.employeeIds = empIds;
    this.trainingSevc
      .unLinkedEmployees(scheduleId, deleteEmp)
      .then(async (res: any) => {
        this.alert.successToast(await this.labelPipe.transform('Employee') + 's Removed from Class');
        this.getSelfRegistrationEmp();
      });
  }

  async deleteTR(id: any) {
    var getCurrentScheduleToDelete = this.allSchedules.find(
      (x) => x.id === this.rscId
    );
    //recurrenceId
    if (
      getCurrentScheduleToDelete.recurrenceId !== null &&
      getCurrentScheduleToDelete.recurrenceId !== ''
    ) {
      await this.trainingSevc.delete(this.rscId).then((_) => {
        this.alert.successToast('Training Removed');
        this.EnrollStudentSelected();
      });
    } else {
      this.alert.errorAlert('Unable to Delete main Training Class');
    }
  }

  uncheckOthers(title: string, event: any) {
    this.updatedEvalValues = this.evalForm.value;
    if (event.checked) {
      this.evalSelection.select(title);
      this.evalForm.reset(this.evalValues);
      this.evalForm.patchValue({
        [title]: this.updatedEvalValues[title],
      });
      this.evalForm.updateValueAndValidity();
      switch (title) {
        case 'evaluationAvailableOnStartDate':
          this.evalForm.patchValue({
            finalGradeRequired: this.updatedEvalValues['finalGradeRequired'],
          });
          break;
        case 'evaluationAvailableOnEndDate':
          this.evalForm.patchValue({
            finalGradeRequired: this.updatedEvalValues['finalGradeRequired'],
          });
          break;
      }
    } else {
      this.evalSelection.clear();
      this.evalForm.reset(this.evalValues);
      switch (title) {
        case 'evaluationAvailableOnStartDate':
          this.evalForm.patchValue({
            finalGradeRequired: false,
          });
          break;
        case 'evaluationAvailableOnEndDate':
          this.evalForm.patchValue({
            finalGradeRequired: false,
          });
          break;
      }
    }

    this.evalForm.patchValue({
      evaluationDueDate: this.updatedEvalValues['evaluationDueDate'],
      empSettingsReleaseType: this.updatedEvalValues['empSettingsReleaseType'],
      evaluationPK: this.updatedEvalValues['evaluationPK'],
    });
  }

  enableSendEMail(event) {
    ;
    if (event.checked) {
      this.trainingForm.get('sendApprovedEmail')?.setValue(true);
    } else {
      this.trainingForm.get('sendApprovedEmail')?.setValue(false);
    }
  }

  /// TQ///

  patchTQRelease(values) {
    if (values !== null) {
      this.TQForm.patchValue({
        tqPK: values?.id,
        tqRequired: values.tqRequired,
        releaseAtOnce: values.releaseAtOnce,
        releaseOneAtTime: values.releaseOneAtTime,
        releaseOnClassStart: values.releaseOnClassStart,
        releaseOnClassEnd: values.releaseOnClassEnd,
        specificTime: values.specificTime,
        priorToSpecificTime: values.priorToSpecificTime,
        oneSignOffRequired: values.oneSignOffRequired,
        multipleSignOffRequired: values.multipleSignOffRequired,
        tqDueDate: values.tqDueDate,
      });
      let setValue = this.testAvailabiilityTime.find(
        (x) =>
          x.time == this.TQForm.get('specificTime')?.value &&
          x.prior == this.TQForm.get('priorToSpecificTime')?.value
      );
      this.TQForm.controls['setAvailabilityTimeTQ'].setValue(setValue);
      if (this.TQForm.get('multipleSignOffRequired')?.value > 0) {
        this.TQForm.get('multipleSignOffRequiredCheck')?.setValue(true);
      }
      this.TQForm.updateValueAndValidity();
    }
    if (!this.TQForm.get('tqRequired')?.value) {
      this.tqSelection.select('order');
      this.tqSelection.select('release');
      this.tqSelection.select('signOff');
    }
  }

  release(event: any, type: string) {
    if (event.checked) {
      switch (type) {
        case 'releaseOnClassStart':
          this.TQForm.get('releaseOnClassEnd')?.setValue(false);
          this.TQForm.get('specificTime')?.setValue(false);
          this.TQForm.get('setAvailabilityTimeTQ')?.setValue(null);
          this.TQForm.get('setAvailabilityTimeTQ')?.clearValidators();
          this.TQForm.get('setAvailabilityTimeTQ')?.setErrors(null);
          break;
        case 'releaseOnClassEnd':
          this.TQForm.get('releaseOnClassStart')?.setValue(false);
          this.TQForm.get('specificTime')?.setValue(false);
          this.TQForm.get('setAvailabilityTimeTQ')?.setValue(null);
          this.TQForm.get('setAvailabilityTimeTQ')?.clearValidators();
          this.TQForm.get('setAvailabilityTimeTQ')?.setErrors(null);
          break;
        case 'specificTime':
          this.TQForm.get('releaseOnClassStart')?.setValue(false);
          this.TQForm.get('releaseOnClassEnd')?.setValue(false);
          this.TQForm.get('setAvailabilityTimeTQ')?.setValidators([
            Validators.required,
          ]);
          this.TQForm.get('setAvailabilityTimeTQ')?.setErrors({
            required: true,
          });
          break;
      }
      this.tqSelection.select('release');
    } else {
      this.TQForm.get('setAvailabilityTimeTQ')?.setValue(null);
      this.TQForm.get('setAvailabilityTimeTQ')?.clearValidators();
      this.TQForm.get('setAvailabilityTimeTQ')?.setErrors(null);
      this.tqSelection.deselect('release');
    }
    this.TQForm.updateValueAndValidity();
  }

  orderOfCompletion(event: any, type: string) {
    if (event.checked) {
      switch (type) {
        case 'releaseOneAtTime':
          this.TQForm.get('releaseAtOnce')?.setValue(false);
          break;
        case 'releaseAtOnce':
          this.TQForm.get('releaseOneAtTime')?.setValue(false);
          break;
      }
      this.TQForm.updateValueAndValidity();
      this.tqSelection.select('order');
    } else {
      this.tqSelection.deselect('order');
    }
  }

  masterChange(event: any) {
    ;
    if (event.checked) {
      this.TQForm.clearValidators();
      var dueDate = this.TQForm.get('tqDueDate')?.value;
      this.TQForm.reset(this.originalInitialValues);
      this.TQForm.get('tqDueDate')?.setValue(dueDate);
      this.TQForm.get('tqRequired')?.setValue(true);
      // this.TQForm.get('tqDueDate')?.setValidators([Validators.required]);
      // this.TQForm.get('tqDueDate')?.setErrors({required:true});
      this.TQForm.get('multipleSignOffRequired')?.clearValidators();
      this.TQForm.get('multipleSignOffRequired')?.setErrors(null);
      this.TQForm.get('setAvailabilityTimeTQ')?.clearValidators();
      this.TQForm.get('setAvailabilityTimeTQ')?.setErrors(null);
      this.TQForm.updateValueAndValidity();
      this.tqSelection.clear();
    }
  }

  addTQTaskEvaluation() {
    let createStudentEvaluation: EMPSettingsTQTaskEvaluation = {
      ilaId: this.trainingForm.get('ilaId')?.value,
      evaluatorIds: this.dataSourceTaskEval.data.map((x) => x.id),
    };
    this.ilaService
      .createTQTaskEvaluations(createStudentEvaluation)
      .then((res) => {
        this.alert.successToast(`Link has been created `);
      })
      .catch((res: any) => {
        this.alert.errorToast(res);
      });
  }

  onTimeSelected() {
    var startTime = this.trainingForm.get('startTime').value;
    var endTime = this.trainingForm.get('endTime').value;
    var startDate = this.trainingForm.get('startDate').value;
    var endDate = this.trainingForm.get('endDate').value;
    if (startTime && endTime) {
      if (startDate == endDate) {
        if (startTime < endTime) {
          this.isDateTimeValid = false;
        } else {
          this.isDateTimeValid = true;
        }
      } else if (startDate < endDate) {
        this.isDateTimeValid = false;

      } else {
        this.isDateTimeValid = true;
      }
    }
  }

  // onEndTimeSelected()
  // {
  //   var startTime =  this.trainingForm.get('startTime').value;
  //   //var endTime = this.trainingForm.get('endTime').value;

  //   if (startTime && endTime && startTime <= endTime)
  //    {
  //     this.alert.errorAlert("End Time Cannot be less or equal to start time")
  //     return;
  //    }

  // }

  async deleteClass(templateRef: any, id: any) {
    var getCurrentScheduleToDelete = this.allSchedules.find((x) => x.id === id);
    this.rscId = id;

    var ilaName = 'ILA';
    // var ilaName = getCurrentScheduleToDelete.ila
    // if(ilaName.name != null)
    // {
    //   ilaName = getCurrentScheduleToDelete.ila.name
    // }
    var startDT = this.datePipe.transform(
      getCurrentScheduleToDelete.startDateTime,
      'yyyy-MM-dd hh:mm a'
    );
    this.deleteDescription = `You are selecting to remove class for ${startDT.toString()}. This will un-enroll all ` + await this.labelPipe.transform('Employee') + `s currently enrolled in the class.`;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  receivedRecurrenceInformation(event: any) {
    this.recurrenceType = event.recurrenceType
    this.recurrenceDescription = event.recurrenceDescription;
    this.recursEveryNumber = event.recursEveryNumber;
    this.endDate = event.endDate;
  }

  deleteSingleRecurrTraining(templateRef: any, row, id: any) {
    // var startDate = this.changeStartSateForEdit(row._startDate);
    // var endDate = this.changeStartSateForEdit(row._endDate);
    var ilaId = this.trainingForm.get('ilaId').value;
    var ilaName = this.ila_list.find((x) => x.id == ilaId).name;
    if (row.id === undefined) {
      //it means it is in add case
      this.recurringIndex = this.dataSource.data.indexOf(row);
    } else {
      this.recurringIndex = -1;
      this.recurringIdToDelete = row.id;
    }

    this.deleteDescription = `You are selecting to delete Training for ${ilaName}, ${this.datePipe.transform(
      row._startDate,
      'MM/dd/yy, H:mm'
    )} and ${this.datePipe.transform(row._endDate, 'MM/dd/yy, H:mm')}.`;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  deleterecurrTraining(row: any) {
    if (this.recurringIndex > -1) {
      this.dataSource.data.splice(this.recurringIndex, 1);
      this.dataSource._updateChangeSubscription();
      this.alert.successToast('Recurring Class Deleted Successfully');
    } else {
      this.trainingSevc.delete(this.recurringIdToDelete).then((res: any) => {
        this.alert.successToast('Recurring Class Deleted Successfully');
        this.getClassRecurrence();
      });
    }


    // this.route.params.subscribe((params: any) =>
    // {
    //   if (params.hasOwnProperty('id') || params.hasOwnProperty('index'))
    //   {
    //
    //     this.trainingId = params['id'];
    //   }
    //   else
    //   {

    //     const index = this.dataSource.data.indexOf(row);
    //     if (index > -1) {
    //       this.dataSource.data.splice(index, 1);
    //       this.dataSource._updateChangeSubscription();
    //     }

    //   }
    // });
  }

  formatTime(time: any) {

    // Add logic to convert the time to the desired format (AM/PM)
    // For example, if your time is in the format "hh:mm", you can do the following:
    if (time) {
      const [hours, minutes] = time.split(':');
      let formattedTime = `${hours}:${minutes}`;
      const hoursNum = parseInt(hours, 10);
      const amPm = hoursNum >= 12 ? 'PM' : 'AM';
      const formattedHours = hoursNum % 12 || 12;
      formattedTime = `${formattedHours}:${minutes} ${amPm}`;
      //this.selectedTime = formattedTime;
      return time;
    }
  }

  onStartDateSelect() {

    // this.dayEvent = this.getWeekDayFromDate(this.trainingForm.get('startDate')?.value);
    this.onTimeSelected();
  }

  onEndDateSelect() {

    // this.dayEvent = this.getWeekDayFromDate(this.trainingForm.get('endDate')?.value);
    this.onTimeSelected();
  }

  getWeekDayFromDate(dateString: string): string {
    const date = new Date(dateString);
    const daysOfWeek = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
    return daysOfWeek[date.getDay()];
  }

  async changeRecurrence(templateRef: any) {
    this.dateInformation = {
      startDate: this.trainingForm.get('startDate')?.value,
      endDate: this.trainingForm.get('endDate')?.value,
      startTime: this.trainingForm.get('startTime')?.value,
      endTime: this.trainingForm.get('endTime')?.value,
      instructorName: this.instructor_list.find(
        (x) => x.id === this.trainingForm.get('instructorId')?.value
      )?.instructorName,
      locationName: this.location_list.find(
        (x) => x.id === this.trainingForm.get('locationId')?.value
      )?.locName,
      isClassPubliclyAvailable: this.trainingForm.get('isPubliclyAvailableClass')?.value
    };
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  getInstructorNameById(instructorId: string): string | null {
    const selectedInstructor = this.instructor_list.find(instructor => instructor.id === instructorId);
    return selectedInstructor ? selectedInstructor.instructorName : null;
  }

   getLocationNameById(locationId: string): string | null {
    const selectedLocation = this.location_list.find(location => location.id === locationId);
    return selectedLocation ? selectedLocation.locName : null;
  }

  clearLocation(event: Event) {
    event.stopPropagation();
    this.trainingForm.get('locationId').reset();
  }

  clearInstructor(event: Event) {
    event.stopPropagation();
    this.trainingForm.get('instructorId').reset();
  }
  
  getReviewData() {
    //this.trainingSevc.getRevieWData(this.trainingId,this.ilaId);
    this.trainingSevc.getRevieWData(this.trainingId, this.ilaId).then((res: any) => {
      this.reviewData = res;
    });
  }

  public findInvalidControls() {
    const invalid = [];
    const controls = this.trainingForm.controls;
    for (const name in controls) {
      if (controls[name].invalid) {
        invalid.push(name);
      }
    }
    return invalid;
  }

  checkILARequiredForEmp(checked: boolean) {
    if (checked) {
      var startTime = this.trainingForm.get('startTime').value;
      if (startTime === '') {
        Validators.required(startTime);
      }
      //this.trainingForm.get('startTime')?.setValidators([Validators.required]);
      this.trainingForm
        .get('startTime')
        ?.setErrors({
          required:
            this.trainingForm.get('startTime')?.value !== '' ? false : true,
        });
      this.trainingForm
        .get('endTime')
        ?.setErrors({
          required:
            this.trainingForm.get('endTime')?.value !== '' ? false : true,
        });
      this.trainingForm.get('endTime')?.setErrors({required: true});
    } else {
      this.trainingForm.get('startTime')?.clearValidators();
      this.trainingForm.get('startTime')?.setErrors({required: false});
      this.trainingForm.get('endTime')?.clearValidators();
      this.trainingForm.get('endTime')?.setErrors({required: false});
    }
    this.trainingForm.updateValueAndValidity();
    var invalidControls = this.findInvalidControls();

  }

  providerSearch() {
    var filterString = this.trainingForm.get('searchTxt')?.value;

    if (filterString !== undefined && filterString !== null) {
      filterString = String(filterString).trim().toLowerCase();
    } else {
      filterString = "";
    }
    this.provider_list = this.provider_list_original.filter((f) => {
      return f.name.toLowerCase().trim().includes(filterString);
    });
  }

  ilaSearch() {
    var filterString = this.trainingForm.get('ilaSearch')?.value;

    if (filterString !== undefined && filterString !== null) {
      filterString = String(filterString).trim().toLowerCase();
    } else {
      filterString = "";
    }
    this.ila_list = this.ila_list_original.filter((f) => {
      return f.name.trim().toLowerCase().includes(filterString) || f.number?.trim().toLowerCase().includes(filterString);
    });
  }


  async createClassTestReleaseByILA(){
    await this.classTestEmpSettingService.createTestReleaseSettings(this.trainingId).then((res: any) => {
      if (res !== null) {
        this.patchTestRelease(res);
      } else {
        this.testReleaseCheckNUll = true;
      }
    })
  }

  async createTQReleaseByILA() {
    await this.classTQReleaseSettingService
      .createClassScheduleTQEMPSettings(this.trainingId)
      .then((res: any) => {
        if (res !== null) {
          this.patchTQRelease(res);
        } else {
          this.tqReleaseCheckNUll = true;
        }
      });
  }

  async createTQClassEvaluatorsByILA() {
    await this.classTQReleaseSettingService
      .linkEvaluatorsFromILAEvaluator(this.trainingId)
      .then((res) => {
        this.dataSourceTaskEval.data = res;
      })
      .catch((res: any) => {
        this.alert.errorToast(res);
      });
  }

  getClassTQEvaluators() {
    this.classTQReleaseSettingService
      .getClassScheduleTQEvaluators(this.trainingId)
      .then((res) => {
        this.dataSourceTaskEval.data = res;
      })
      .catch((res: any) => {
        this.alert.errorToast(res);
      });
  }

  resetProviderSearch(){
    setTimeout(() => {
      this.trainingForm.get('searchText')?.setValue('');
      this.providerSearch();
    }, 500);
  }

  resetIlaSearch(){
    setTimeout(() => {
      this.trainingForm.get('ilaSearch')?.setValue('');
      this.ilaSearch();
    }, 500);
  }

  handleKeydown(event: KeyboardEvent) {
    this.selectIla._handleKeydown = (event: KeyboardEvent) => {
      if (event.key === 'SPACE') return;
    };
    this.select._handleKeydown = (event: KeyboardEvent) => {
      if (event.key === 'SPACE') return;
    };
  }

}


