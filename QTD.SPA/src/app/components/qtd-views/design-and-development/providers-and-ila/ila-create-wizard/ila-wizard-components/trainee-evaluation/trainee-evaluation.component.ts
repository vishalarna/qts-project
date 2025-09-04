import { SimulatorScenarioILA_LinkOptions } from 'src/app/_DtoModels/SimulatorScenarioILA_Link/SimulatorScenarioILA_LinkOptions';
import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';
import { TemplatePortal } from '@angular/cdk/portal';
import {
  UntypedFormArray,
  UntypedFormBuilder,
  UntypedFormControl,
  UntypedFormGroup,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import {
  AfterViewInit,
  Component,
  EventEmitter,
  Input,
  OnDestroy,
  OnInit,
  Output,
  ViewChild,
  ViewContainerRef,
} from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import * as ckcustomBuild from 'src/app/ckcustomBuild/build/ckeditor.js';
import { DutyArea } from 'src/app/_DtoModels/DutyArea/DutyArea';
import { NavBarMenuItem } from 'src/app/_DtoModels/NavBarMenuItem';
import { DutyAreaService } from 'src/app/_Services/QTD/duty-area.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { DeleteDialogComponent } from './delete-dialog/delete-dialog.component';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { MatSort } from '@angular/material/sort';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { SelectionModel } from '@angular/cdk/collections';
import { TraineeEvaluationService } from 'src/app/_Services/QTD/trainee-evaluation.service';
import { SubSink } from 'subsink';
import { TestTypeService } from 'src/app/_Services/QTD/test-type.service';
import { TestItemTypeService } from 'src/app/_Services/QTD/test-item-type.service';
import { IlaService } from 'src/app/_Services/QTD/ila.service';
import { skip } from 'rxjs/operators';
import { select, Store } from '@ngrx/store';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { TestsService } from 'src/app/_Services/QTD/tests.service';
import { IlaTraineeEvaluationService } from 'src/app/_Services/QTD/ila-trainee-evaluation.service';
import { TestStatusCreateOptions } from 'src/app/_DtoModels/TestStatus/TestStatusCreateOptions';
import { TestCreateOptions } from 'src/app/_DtoModels/Test/TestCreateOptions';
import { ILATraineeEvaluationCreateOptions } from 'src/app/_DtoModels/ILATraineeEvaluation/ILATraineeEvaluationCreateOptions';
import { TestItemLinkCreateOptions } from 'src/app/_DtoModels/TestItemLink/TestItemLinkCreateOptions';
import { DiscussionQuestionCreateOptions } from 'src/app/_DtoModels/DiscussionQuestion/DiscussionQuestionCreateOptions';
import { ILATraineeEvaluation, IlaTraineeEvaluationList, IlaTraineeEvaluationStatusOption } from 'src/app/_DtoModels/ILATraineeEvaluation/ILATraineeEvaluation';
import { SimulatorScenarioService } from 'src/app/_Services/QTD/simulator-scenario.service';
import { TestItemService } from 'src/app/_Services/QTD/test-item.service';
import { TestItem } from 'src/app/_DtoModels/TestItem/TestItem';
import { Test_TestItem_LinkOptions } from 'src/app/_DtoModels/Test_TestItem_Link/Test_TestItem_LinkOptions';
import { CoverSheet } from 'src/app/_DtoModels/CoverSheet/CoverSheet';
import { TaskWithCountOptions } from 'src/app/_DtoModels/Task/TaskWithCountOptions';
import { TraineeEvaluationType } from 'src/app/_DtoModels/TraineeEvaluationType/TraineeEvaluationType';
import { DiscussionQuestion } from 'src/app/_DtoModels/DiscussionQuestion/DiscussionQuestion';
import { TraineeEvaluationTypeOptions } from 'src/app/_DtoModels/TraineeEvaluationType/TraineeEvaluationTypeOptions';
import { TestStatusService } from 'src/app/_Services/QTD/test-status.service';
import { TaxonomyLevelService } from 'src/app/_Services/QTD/taxonomy-level.service';
import { ILA_PerformTraineeEvalCreateOptions } from 'src/app/_DtoModels/ILA_PerformTraineeEval/ILA_PerformTraineeEvalCreateOptions';
import { ILATaskObjectiveLinkOption } from 'src/app/_DtoModels/ILA_TaskObjective_Link/ILA_TaskObjective_LinkOptions';
import { ILAEvalMethodVM } from 'src/app/_DtoModels/ILA/ILAEvalMethodVM';
import { MatLegacyRadioGroup as MatRadioGroup } from '@angular/material/legacy-radio';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';

@Component({
  selector: 'app-trainee-evaluation',
  templateUrl: './trainee-evaluation.component.html',
  styleUrls: ['./trainee-evaluation.component.scss'],
})
export class TraineeEvaluationComponent implements OnInit, OnDestroy, AfterViewInit {
  TraineeEvaluationForm!: UntypedFormGroup;
  @Input() simulation_status: boolean;
  @Input() editIlaCheck: any;
  @Input() contorlCheckSimulation: any;

  @Output() create_new = new EventEmitter<any>();
  @Input() show_simulations: boolean;
  @Input() title_text: any[] = [];
  @Output() formEvent = new EventEmitter<any>();
  @Output() loadingEvent = new EventEmitter<boolean>();
  useCoverFor: 'Perform' | 'Discuss' | 'Simulations';
  selectedQuestion!: any;
  addedQuestions: TestItem[] = [];
  mode: 'add' | 'edit' = 'add';
  View_Scenario_text: any;
  create_new_simulation: boolean = false;
  IsTestSettingsDisabled: boolean = true;
  evaluationTypeId: any = '';

  writtenSaveSpinner = false;
  discussSaveSpinner = false;
  simulationSaveSpinner = false;

  evaluationForm: UntypedFormGroup = new UntypedFormGroup({})

  @Output() previewEvent = new EventEmitter<string>();
  dataTransfered: boolean = false;

  DataSource: MatTableDataSource<any>;
  editInstruction: boolean;
  @ViewChild(MatSort) set sort(sorting: MatSort) {
    if (sorting) this.DataSource.sort = sorting;
  }
  @ViewChild(MatPaginator) set paginator(paging: MatPaginator) {
    if (paging) this.DataSource.paginator = paging;
  }

  selection = new SelectionModel<any>(true, []);
  taskSelection = new SelectionModel<any>(true);
  displayColumns: string[] = ['id', 'TaskNumber', 'Description', 'RoutePath'];


  // Evaluation Type Status
  WrittenStatus: boolean = false;
  PerformStatus: boolean = false;
  DiscussStatus: boolean = false;
  SimulationStatus: boolean = false;

  view_data: boolean = false;
  use_existing: boolean = false;
  on_select: boolean = false;
  selected_data_array: any[] = [];
  powerData_array: any[] = [];
  testItemTypeArray:any=[];

  Title: string;
  WrittenTitle: string;
  SimulationTitle: string;
  DiscussTitle: string;

  Instruction: string = '';

  TimeLimitHours: string;
  TimeLimitMins: string;
  //answer_array: any;
  selectAllTasks: boolean = false;

  answer_array: any[] = [];

  simulator_array: any[] = [];
  recieve_power_data: boolean = false;
  index: any;

  questionId = '';
  showSpinner = false;
  editId: string | null = null;
  copyId!:any;
  testQuestionRemovalDescription = '';

  public Editor = ckcustomBuild;
  public PerformEditor = ckcustomBuild;
  public DiscussQuestionEditor = ckcustomBuild;
  public DiscussAnswerEditor = ckcustomBuild;
  allEvaluations: IlaTraineeEvaluationList[] = [];
  ilaEvaluations: any[] = [];


  public configCKEditor = {
    toolbar: [
      'bold',
      'italic',
      '|',
      'link',
      '|',
      'bulletedList',
      'numberedList',
    ],
  };

  testType: any[] = [];
  allEvaluationType: any[] = [];
  listDA: DutyArea[] = [];
  taskList: any[] = [];
  @Input() navList: NavBarMenuItem[] = [];
  spinner: boolean = false;
  allSelected = false;
  activeTab: any;
  teststatus: any[] = [];
  edit = false;
  taxonomyArray:any =[];
  isCreateTestVisible: boolean = false;
  isShowCreateTestVisible: boolean = false;
  //For discuss questions
  discussQuestionsList: any[] = [];
  selectedType: string;
  evalTypes: TraineeEvaluationType[] = [];
  disableButton: any[] = [];

  subscriptions = new SubSink();

  WrittenFormGroup: UntypedFormGroup = new UntypedFormGroup({});
  initialWritteFormValues!: any;
  initialDiscussFormValues!: any;
  DiscussFormGroup: UntypedFormGroup = new UntypedFormGroup({});
  SimulationFormGroup: UntypedFormGroup = new UntypedFormGroup({});
  QuestionAnswerform: UntypedFormGroup = new UntypedFormGroup({});
  assessmentTypeForm: UntypedFormGroup;
  check: boolean = false;
  discussionQuestionId: any;
  simArray: any = [];


  ILAID: any;
  deleteId!:any;
  deleteType!:any;
  performSpinner = false;
  constructor(
    public flyPanelService: FlyInPanelService,
    private vcf: ViewContainerRef,
    private fb: UntypedFormBuilder,
    private _DutyAreaService: DutyAreaService,
    private route: Router,
    private alert: SweetAlertService,
    public dialog: MatDialog,
    private TrEvalService: TraineeEvaluationService,
    private testTypeService: TestTypeService,
    private ilaService: IlaService,
    private saveStore: Store<{ saveIla: any }>,
    private dataBroadCastService: DataBroadcastService,
    private testStatusService: TestStatusService,
    private testService: TestsService,
    private ilaTraineeEvalService: IlaTraineeEvaluationService,
    private simScenarioService: SimulatorScenarioService,
    private testItemService: TestItemService,
    private testItemTypeService:TestItemTypeService,
    private taxonomyLevelService:TaxonomyLevelService,
    private labelPipe: LabelReplacementPipe
  ) { }

  // editorData = {};
  ngOnInit(){
    this.loadAsync();
  }

  async loadAsync() {
    try {
      this.loadingEvent.emit(true); 
      this.readyTraineeEvaluationForm();
      this.evaluationForm.addControl('eval', new UntypedFormControl(''));

      await new Promise<void>((resolve, reject) => {
        const subscription = this.saveStore.pipe(select('saveIla')).subscribe({
          next: async (res: any) => {
            try {
              if (res?.saveData?.result !== undefined) {
                this.ILAID = res.saveData.result.id;
                this.ilaEvaluations = [];
                await this.getAllEvaluations();
                await this.readyEvalMethodData();
              } else if (res.update === true) {
                this.ILAID = res.saveData?.id;
                await this.getAllEvaluations();
                await this.readyTaxonomyLevel();
                await this.readyTestItemType();
              }
              resolve(); 
            } catch (error) {
              reject(error); 
            }
          },
          error: (err) => {
            reject(err);
          },
        });
  
        this.subscriptions.sink = subscription;
      });

      this.subscriptions.sink = this.dataBroadCastService.refreshTaskLinks.subscribe({
        next: async () => {
          await this.readyTasks();
        },
        error: (err) => {
        },
      });

      this.readyWrittenForm();
      this.readyDiscussForm();
      this.readySimulationForm();
      await this.readyTrEvalTypes();
      await this.ReadyTestType();
      this.initializeAssessmentType();
      this.setAssessmentTypeForm();
      this.allEvTypes();
      await this.getDutyAreas();
      this.Instruction = '';
    } catch (error) {
      this.loadingEvent.emit(false);
    } finally {
      this.loadingEvent.emit(false); 
    }
  }

  ngAfterViewInit(): void {
    this.subscriptions.sink = this.dataBroadCastService.updateAnswerList.subscribe((res: any) => {
      this.answer_array.map((data: any) => {
        if (data.id === res.id) {
          data.description = res.description;
          data.description = data.description.replace('<p>', '');
          data.description = data.description.replace('</p>', '');
        }
      })
    })
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }

  async readyEvalMethodData(){
    var data = await this.ilaService.getEvalMethodData(this.ILAID);
    this.evaluationForm.patchValue({
      eval:data.evaluationMethod,
    });
  }

  evalMethodSpinner = false;
  async updateEvalMethod(){
    this.evalMethodSpinner = true;
    var options = new ILAEvalMethodVM();
    options.evaluationMethod = this.evaluationForm.get('eval')?.value;
    await this.ilaService.updateEvalMethod(this.ILAID,options).then((_)=>{
      this.alert.successToast("Training Evaluation Method Data Saved Successfully");
    }).finally(()=>{
      if(this.WrittenStatus==true){
        if(this.WrittenFormGroup.valid){
          this.saveWrittenEval();
        }
      }
      else if(this.PerformStatus == true){
        this.savePerformData();
      }
      this.evalMethodSpinner = false;
      this.getAllEvaluations();
    })
  }

  async readyTestItemType(){
    await this.testItemTypeService.getAll().then((res) => {
      this.testItemTypeArray = res;
    }).catch((err: any) => {

    })
  }

  initializeAssessmentType(){
    this.assessmentTypeForm = this.fb.group({
      assessmentType: new UntypedFormControl(null)
    })
  }

  setAssessmentTypeForm(){
    this.assessmentTypeForm.get("assessmentType").enable();
    this.assessmentTypeForm.get("assessmentType").setValue(null);
    this.isCreateTestVisible = true;
    this.isShowCreateTestVisible = false;
    this.WrittenFormGroup.reset();
    this.SimulationFormGroup.reset();
    this.DiscussFormGroup.reset();
    this.answer_array = [];
    this.WrittenStatus = false;
    this.PerformStatus = false;
    this.DiscussStatus = false;
    this.SimulationStatus = false;
  }

  async readyTaxonomyLevel(){
    await this.taxonomyLevelService.getAll().then((res) => {
      this.taxonomyArray = res;

    }).catch((err: any) => {
      this.alert.errorToast("Error fetching Taxonomy Level");
    })
  }
  async ChangeStatus(data:IlaTraineeEvaluationList){
    var options=new IlaTraineeEvaluationStatusOption();
    options.type=data.evaluationMethodType;
    options.evaluationId=data.evaluationId;
    options.status=!data.isActive;
    var result=await this.ilaTraineeEvalService.changeTraineeEvaluationStatus(options);
    if (result) {
      this.alert.successToast("Status updated successfully");
      await this.getAllEvaluations();
    }
  }
  evalTypesSpinner = false;
  async readyTrEvalTypes() {
    this.evalTypesSpinner = true;
    await this.TrEvalService.getAll().then((res: any) => {
      this.evalTypes = res;

      this.disableButton = Array(this.evalTypes.length).fill(false);

    }).finally(()=>{
      this.evalTypesSpinner = false;
    })
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }

  async readyTasks() {
    this.taskList = [];
    if (this.ILAID !== undefined) {
      await this.ilaService.getLinkedTaskObjectives(this.ILAID).then((res: TaskWithCountOptions[]) => {
        var selected = res.filter(x => x.isUsedForTQ).map((data) => { return data.id});
        selected.forEach((data)=>{
          this.taskSelection.select(data);
        });

        res.forEach((element: TaskWithCountOptions, index: any) => {
          this.taskList.push({ isInUse:element.isUsedForTQ,id:element.id,description: element.description, number: element.number, checked: false, RoutePath: `/my-data/tasks/detail/${element.id}-${element.letter}_${element.number}` });
        });


      }).catch(async (error: any) => {
        this.alert.errorToast("Error Fetching "+ await this.transformTitle('Task'));
      })
    }
  }

  createTestForILA(){
    this.WrittenStatus = true;
    this.isShowCreateTestVisible = false;
  }

  inputChange(event: any) {
    var charCode = event.which ? event.which : event.keyCode;
    // Only Numbers 0-9
    if (charCode < 48 || charCode > 57) {
      event.preventDefault();
      return false;
    } else {
      return true;
    }
  }

  readySimulationForm() {
    this.SimulationFormGroup.addControl('Title', new UntypedFormControl('', Validators.required));
    this.SimulationFormGroup.addControl('Instruction', new UntypedFormControl(''));
    this.SimulationFormGroup.addControl('TimeLimitHours', new UntypedFormControl('0'));
    this.SimulationFormGroup.addControl('TimeLimitMins', new UntypedFormControl('0'));
  }

  readyDiscussForm() {
    this.DiscussFormGroup = this.fb.group({
      Title: ['', [Validators.required]],
      Instruction: ['', [Validators.required]]
    })

    this.QuestionAnswerform = this.fb.group({
      quesAns: this.fb.array([])
    });

    for (let i = 0; i < 3; i++) {
      this.addArray();
    }

    this.initialDiscussFormValues = this.DiscussFormGroup.value;

  }
  addArray() {
    this.quesAns.push(this.buildArray());
  }

  buildArray(): UntypedFormGroup {
    return this.fb.group({
      questionText: '',
      answerText: ''
    });
  }

  get quesAns(): UntypedFormArray {
    return this.QuestionAnswerform.get('quesAns') as UntypedFormArray;
  }

  // set quesAns(data: FormArray) {
  //   this.QuestionAnswerform.get('quesAns')?.setValue(data);
  // }

  AddNewDiscussQuestion() {
    this.addArray();
  }

  readyWrittenForm() {
    this.WrittenFormGroup.addControl('title', new UntypedFormControl('', Validators.required));
    this.WrittenFormGroup.addControl('radio', new UntypedFormControl('', Validators.required));
    this.WrittenFormGroup.addControl('Instruction', new UntypedFormControl(''));
    this.WrittenFormGroup.addControl('TimeLimitHours', new UntypedFormControl(0));
    this.WrittenFormGroup.addControl('TimeLimitMins', new UntypedFormControl(0));
    this.initialWritteFormValues = this.WrittenFormGroup.value;
    this.subscriptions.sink = this.WrittenFormGroup.statusChanges.subscribe((res: any) => {
      this.formEvent.emit(res);
    });
  }


  readyTraineeEvaluationForm() {
    this.TraineeEvaluationForm = this.fb.group({
      Title: new UntypedFormControl(this.Title, [Validators.required]),

      configCKEditor: new UntypedFormControl(''),
      Instruction: new UntypedFormControl(''),
      TimeLimitHours: new UntypedFormControl(''),
      WrittenTitle: new UntypedFormControl(this.WrittenTitle, [Validators.required]),
      TimeLimitMins: new UntypedFormControl(''),
      SimulationTitle: new UntypedFormControl(this.SimulationTitle, [
        Validators.required,
      ]),
      DiscussTitle: new UntypedFormControl(this.DiscussTitle, [Validators.required]),
    });
  }

  ShowType(data: any, id: any) {
    this.isCreateTestVisible = true;
    this.evaluationTypeId = id;
    //it is geeting the selected Evlauation type id like Written, Perform, Discuss and Simulation

    this.disableButton = Array(this.evalTypes.length).fill(false);
    var disable = this.evalTypes.map((data) => { return data.name }).indexOf(data);
    this.disableButton[disable] = true;
    if (data === 'Written') {
      this.WrittenType();
    }
    else if (data === 'Perform') {
      this.PerformType();
    }
    else if (data === 'Discuss') {
      this.DiscussType();
    }
    else if (data === 'Simulations') {
      this.SimulationType();
    }
  }

  CreateNewEvaluation() {
    var evType = this.evalTypes.find(x => x.id == this.evaluationTypeId);
    switch (evType.name.trim().toLowerCase()) {
      case "written":
        if (this.WrittenFormGroup.valid) {
          this.SaveEvaluation('Written');
        } else {
          this.alert.errorAlertRedirect('Please Fill out all the required information')
        }
        break;

      case "discuss":
        if (this.DiscussFormGroup.valid) {
          this.saveDiscussionType(true);
        } else {
          this.alert.errorAlertRedirect('Please Fill out all the required information')
        }
        break;

      case "simulation":
        if (this.SimulationFormGroup.valid) {
          this.SaveEvaluation('Simulations');
        } else {
          this.alert.errorAlertRedirect('Please Fill out all the required information')
        }
        break;

      case "perform":
        break;
    }
    this.TraineeEvaluationForm!.reset();
  }

  getDutyAreas() {
    this._DutyAreaService.getAll().then(async (res) => {
      this.listDA = res;
      this.navList = [];
      await res.forEach((da) => {
        da.subdutyAreas.forEach((sda) => {
          let child = new NavBarMenuItem();
          child.Title = da.number + '.' + sda.subNumber + ' ' + sda.description;
          child.RoutePath = '';
          sda.tasks.forEach((t) => {
            child.HasChildren = true;
            child.Children?.push({
              Title:
                da.number +
                '.' +
                sda.subNumber +
                ' ' +
                t.number +
                ' ' +
                t.description,
              RoutePath: '/analysis/jta/task-detail/',
              RouteParams: { taskId: t.id },
              disabled:false,
              isVisible:true
            });
            // this.taskList.push({
            //   TaskNumber: da.number + '.' + sda.subNumber + '.' + t.number,
            //   Description: t.description,
            //   RoutePath: '/analysis/jta/task-detail/',
            //   taskId: t.id,
            //   checked: false,
            //   RouteParams: { taskId: t.id },
            // });
          });
        });
      });
    });

    this.DataSource = new MatTableDataSource(this.taskList);
  }

  taskChange(event: any, id: string) {
    if(event.checked){
      this.taskSelection.select(id);
    }
    else{
      this.taskSelection.deselect(id);
    }
  }

  checkAllTasks(event:any){
    if(event.checked){
      var taskIds = this.taskList.map((data)=>{ return data.id});
      this.taskSelection.clear();
      taskIds.forEach((data)=>{
        this.taskSelection.select(data);
      })
    }
    else{
      this.taskSelection.clear();
    }

  }

  isAllTasksCheckBoxChecked() {
    return this.taskList.every((p) => p.checked);
  }

  checkAllTasksCheckBox(event: any) {
     // Angular 13
    this.taskList.forEach((x) => (x.checked = event.checked));
    // this.taskChange(event);
  }

  

  async ReadyTestType() {
    // this.testType = [
    //   { id: 1, type: 'Pretest' },
    //   { id: 2, type: 'Final Test' },
    //   { id: 3, type: 'Retake' },
    // ];
    await this.testTypeService.getAll().then((res: any) => {
      this.testType = res;
      this.testType.splice(-2, 2);

    }).catch((err) => {
      this.alert.errorToast("Failed To Fetch Test Types");
    })
  }

  allEvTypes() {
    this.allEvaluationType = [
      { Title: 'Evaluation 1', Type: 'Written' },
      { Title: 'Evaluation 2', Type: 'Perform' },
      { Title: 'Evaluation 3', Type: 'Discuss' },
      { Title: 'Evaluation 4', Type: 'Simulations' },
    ];
  }

  OnDelete(templateRef:any) {
    this.dialog.open(templateRef, {
      width: '450px',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  async copyTraineeEval(){
    await this.ilaTraineeEvalService.copyEvaluation(this.copyId).then((_)=>{
      this.alert.successToast("Trainee Evaluation Copied");
      this.getAllEvaluations();
    })
  }

  async deleteTREval(){
    if (this.deleteType==="Perform") {
      await this.ilaTraineeEvalService.deletePerformType(this.ILAID).then(()=>{
        this.alert.successToast("Trainee Evaluation Deleted Successfully");
        this.getAllEvaluations();
        this.editId = null;
      });
    }else{
      var options = new TraineeEvaluationTypeOptions();
      options.actionType = "delete";
      await this.ilaTraineeEvalService.delete(this.deleteId,options).then(()=>{
        this.alert.successToast("Trainee Evaluation Deleted Successfully");
        this.getAllEvaluations();
        this.editId = null;
      });
    }
  }

  OnCreateNewClick() {
    this.create_new.emit(true);
  }

  UseExistingScenario() {
    this.use_existing = !this.use_existing;
    this.create_new_simulation = true;
    this.recieve_power_data = false;
    this.readySimulatorScenarios();
  }

  async readySimulatorScenarios() {
    await this.simScenarioService.getAll().then((res) => {
      res.forEach((i, j) => {
        this.simulator_array.push({
          id: i.id,
          text: i.simScenarioTitle,
          checked: false,
          description: i.simScenarioDesc
        })
      })
      this.showSpinner = false;
    }).catch((err) => {

    })
  }

  OnSelect(i: any) {

    this.on_select = true;
    this.selected_data_array.push(i.text);
    this.index = this.simulator_array.findIndex((j) => j.id == i.id);
    this.simulator_array[this.index].checked = true;

    var options: SimulatorScenarioILA_LinkOptions = new SimulatorScenarioILA_LinkOptions();
    options.iLAID = this.ILAID;
    options.simulatorScenarioID = i.id;
    this.simScenarioService.linkILA(i.id, options).then(async (res) => {
      this.alert.successToast('Simulator Scenario Linked To ' + await this.labelPipe.transform('ILA') + ' Sucessfully');
    });
  }

  async openFlyInPanel(templateRef: any) {
    this.mode = 'add';
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.view_data = false;
    this.edit = false;
    this.flyPanelService.open(portal);
  }

  openFlyInPanelView(templateRef: any, item: any) {
    var question = this.addedQuestions.find((x) => {
      return x.id == item.id;
    })
    if (question !== undefined) {
      this.selectedQuestion = {
        eoId: question.eoId,
        typeId: question.testItemTypeId,
        taxonomyId: question.taxonomyId,
        id: question.id,
        question: question.description,
      };
    }
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.questionId = item.id;
    this.view_data = true;
    this.edit = false;
    this.flyPanelService.open(portal);
  }

  openWithEditFlyPanelView(templateRef: any, item: any) {
    var question = this.addedQuestions.find((x) => {
      return x.id === item.id;
    });
    if (question !== undefined) {
      this.selectedQuestion = {
        eoId: question.eoId,
        typeId: question.testItemTypeId,
        taxonomyId: question.taxonomyId,
        id: question.id,
        question: question.description,
      };
    }
    this.mode = 'edit';
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.questionId = item.id;
    this.view_data = false;
    this.edit = true;
    this.flyPanelService.open(portal);
  }

  openRemoveTestQuestionDialogue(templateRef: any, testQuestion: any){
    this.questionId = testQuestion.id;

    this.testQuestionRemovalDescription = `You are selecting to remove the following Test Question from this Trainee Evaluation, this will not be undone.<p><br>Question:<br><i>${testQuestion.description}</i><br><br>Type:<br><i>${testQuestion.quesType}</i><br><br>Taxonomy Level:<br><i>${testQuestion.taxType}</i></p>`;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  removeTestQuestion(){
    this.addedQuestions = this.addedQuestions.filter((question) =>{
      return question.id !== this.questionId
    });
    this.answer_array = this.answer_array.filter((question) =>{
      return question.id !== this.questionId
    });

    this.dataTransfered = this.answer_array.length > 0
  }

  openFlyInPanelPowerData(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
    this.use_existing = false;
    this.create_new_simulation = true;
  }

  openFlyInPanelViewScenario(templateRef: any, item: any) {
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);

    this.View_Scenario_text = item;
  }

  recieveItems(d: any) {

    this.answer_array = d;
    this.dataTransfered = true;
  }

  recieveQuestion(data: any) {

    data.description = data.description.replace("<p>", '').replace(/<[^>]+>/g, '');
    data.description = data.description.replace("</p>", '').replace(/<[^>]+>/g, '');
    this.answer_array.push(data);

    this.dataTransfered = true;
  }

  recievePowerData(d: any) {
    this.powerData_array = d;

    this.recieve_power_data = true;
  }

  drop(event: CdkDragDrop<string[]>) {
    moveItemInArray(this.answer_array, event.previousIndex, event.currentIndex);
    moveItemInArray(this.addedQuestions, event.previousIndex, event.currentIndex);

  }

  WrittenType() {
    // this.spinner = true;
    this.isShowCreateTestVisible = true;
    this.PerformStatus = false;
    this.DiscussStatus = false;
    this.SimulationStatus = false;
    this.IsTestSettingsDisabled = false;
    // setTimeout(()=>{
    //   this.spinner = false;
    // },2500);
  }

  async PerformType() {
    if(this.ILAID !== null && this.ILAID != ''){
     var perform =  await this.ilaService.getPerformEval(this.ILAID);
    }
    this.WrittenStatus = false;
    this.PerformStatus = true;
    this.DiscussStatus = false;
    this.SimulationStatus = false;
    this.IsTestSettingsDisabled = true;
    this.isShowCreateTestVisible = false;
    this.readyTasks();
  }

  DiscussType() {
    this.WrittenStatus = false;
    this.PerformStatus = false;
    this.DiscussStatus = true;
    this.SimulationStatus = false;
    this.IsTestSettingsDisabled = false;
    this.isShowCreateTestVisible = false;
  }

  SimulationType() {
    this.WrittenStatus = false;
    this.PerformStatus = false;
    this.DiscussStatus = false;
    this.SimulationStatus = true;
    this.IsTestSettingsDisabled = true;
    this.isShowCreateTestVisible = false;
  }

  openFlyInPanelQuestionBank(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  receiveAllItems(d: any) {
    d.forEach((i: any) => {
      this.answer_array.push({
        id: i.id,
        description: i.ans.replace("&nbsp;", ''),
        taxType: i.tax,
        quesType: i.type,
      });
    });
    this.answer_array = [...new Set(this.answer_array)];

    this.dataTransfered = true;
  }

  openFlyInPanelTestSettings(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  /** Whether the number of selected elements matches the total number of rows. */
  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.DataSource.data.length;
    return numSelected === numRows;
  }

  /** Selects all rows if they are not all selected; otherwise clear selection. */
  masterToggle() {
    this.isAllSelected()
      ? this.selection.clear()
      : this.DataSource.data.forEach((row) => this.selection.select(row));
  }

  removeAnswers() {
    this.answer_array = [];
    this.dataTransfered = false;
  }

  async SaveEvaluation(name: string) {
    switch (name) {
      case 'Written':
        this.writtenSaveSpinner = true;
        this.saveWrittenEval(true);
        break;

      case 'Discuss':
        this.discussSaveSpinner = true;
        var options = new TestStatusCreateOptions();
        options.description = "In Development";
        break;

      case 'Simulations':
        this.simulationSaveSpinner = true;
        var options = new TestStatusCreateOptions();
        options.description = "In Development";
        this.saveTest(null, name);
        break;
    }
  }
  async saveTest(statusId: any | null, name: string) {
    switch (name) {
      case 'Written':
        var options = new TestCreateOptions();
        options.testStatusId = statusId;
        options.testTitle = this.WrittenFormGroup.get("title")?.value;

        this.testService.create(options).then((res: any) => {
          this.saveTestItemLinks(res.id);
          this.saveILATraineeEvaluation(res.id, 'Written');
        }).catch((err: any) => {
          this.writtenSaveSpinner = false;
          this.alert.errorToast("Failed to save Written Test Data");
        })
        break;

      case 'Discuss':
        this.discussSaveSpinner = true;
        var options = new TestCreateOptions();
        /* options.testNotes = this.WrittenFormGroup.get("Instruction")?.value; */
        options.testStatusId = statusId;
        options.testTitle = this.DiscussFormGroup.get('Title')?.value;

        this.testService.create(options).then((res: any) => {
          this.saveILATraineeEvaluation(res.id, 'Discuss');
        }).catch((err: any) => {
          this.discussSaveSpinner = false;
          this.alert.errorToast("Failed to save Discuss Test Data");
        })
        break;

      case 'Simulations':
        this.simulationSaveSpinner = true;
        var options = new TestCreateOptions();
        /* options.testNotes = this.WrittenFormGroup.get("Instruction")?.value; */
        options.testStatusId = statusId;
        options.testTitle = this.SimulationFormGroup.get('Title')?.value;
        if (this.editId === null) {
          var test = await this.testService.create(options);
          this.saveILATraineeEvaluation(test.id, 'Simulations');
        }
        else {
          var test = await this.testService.update(this.editId, options);
          this.saveILATraineeEvaluation(test.id, 'Simulations');
        }
        break;
    }//switch statment

  }

  async saveTestItemLinks(testId: any) {
    var index = 0;
    this.answer_array.forEach((data: any) => {
      var options = new TestItemLinkCreateOptions();
      options.testId = testId;
      options.testItemId = data.id;
      this.testService.createTestItemLink(testId, options).then((res: any) => {

        this.writtenSaveSpinner = false;
      }).catch((err: any) => {
        this.writtenSaveSpinner = false;
        this.alert.errorToast("Error Creating Test Item Link");
      })
    })
  }

  async saveILATraineeEvaluation(testId: any, name: string) {
    switch (name) {
      case 'Written':
        var options = new ILATraineeEvaluationCreateOptions();
        options.evaluationTypeId = this.evaluationTypeId;
        options.ilaId = this.ILAID;
        options.testId = testId;
        options.testInstruction = this.WrittenFormGroup.get('Instruction')?.value;
        options.testTimeLimitHours = this.WrittenFormGroup.get('TimeLimitHours')?.value;
        options.testTimeLimitMinutes = this.WrittenFormGroup.get('TimeLimitMins')?.value;
        options.testTitle = this.WrittenFormGroup.get('title')?.value;
        options.testTypeId = (this.WrittenFormGroup.get('radio')?.value).id;
        options.trainingEvaluationMethod = this.evaluationForm.get('eval')?.value;
        options.editId = this.editId;
        this.ilaTraineeEvalService.create(options).then((res: any) => {
          this.alert.successToast("Evaluation Saved Successfully");
          this.writtenSaveSpinner = false;
          this.ilaEvaluations = [];
          this.getAllEvaluations();
          //this.ilaEvaluations = [];
          this.WrittenFormGroup.reset();
          this.removeAnswers();
        }).catch((err: any) => {
          this.writtenSaveSpinner = false;
          this.alert.errorToast("Error Saving Trainee Evaluation Data");
        })
        break;

      case 'Discuss':
        this.discussSaveSpinner = true;
        for (let i of this.quesAns.value) {
          if (i.questionText === '' && i.answerText === '') {
            this.check = true
          }
          else {
            this.check = false;
          }
        }
        var options = new ILATraineeEvaluationCreateOptions();
        options.evaluationTypeId = this.evaluationTypeId;
        options.testId = testId;
        options.ilaId = this.ILAID;
        options.testTitle = this.DiscussFormGroup.get('Title')?.value;
        options.testInstruction = this.DiscussFormGroup.get('Instruction')?.value;
        this.ilaTraineeEvalService.create(options).then((res: any) => {
          this.alert.successToast("Evaluation Saved Successfully");
          if (this.check === false) {
            this.saveDiscussQuestion(res.id);
          }
          this.discussSaveSpinner = false;
          this.DiscussFormGroup.reset();
          this.ilaEvaluations = [];
          this.getAllEvaluations();
          //this.ilaEvaluations = [];
        }).catch((err: any) => {
          this.discussSaveSpinner = false;
          this.alert.errorToast("Error Saving Trainee Evaluation Data");
        })

        break;

      case 'Simulations':
        var options = new ILATraineeEvaluationCreateOptions();
        options.evaluationTypeId = this.evaluationTypeId;
        options.testId = testId;
        options.ilaId = this.ILAID;
        options.testTitle = this.SimulationFormGroup.get('Title')?.value;
        options.testInstruction = this.SimulationFormGroup.get('Instruction')?.value;
        options.testTimeLimitHours = this.SimulationFormGroup.get('TimeLimitHours')?.value;
        options.testTimeLimitMinutes = this.SimulationFormGroup.get('TimeLimitMins')?.value;
        this.ilaTraineeEvalService.create(options).then((res: any) => {
          this.alert.successToast("Evaluation Saved Successfully");
          this.simulationSaveSpinner = false;
          this.SimulationFormGroup.reset();
          this.ilaEvaluations = [];
          this.getAllEvaluations();
          this.setAssessmentTypeForm();
          //this.ilaEvaluations = [];
        }).catch((err: any) => {
          this.simulationSaveSpinner = false;

          this.alert.errorToast("Error Saving Trainee Evaluation Data");
        })
        break;
    }//switch statement

  }

  async saveDiscussQuestion(id: any) {
    var discussOptions = new DiscussionQuestionCreateOptions();

    discussOptions.iLATraineeEvaluationId = id;
    this.quesAns.controls.forEach(async (element, index) => {


      discussOptions.questionText = element.value.questionText;
      discussOptions.answerKeywords = element.value.answerText;
      await this.ilaTraineeEvalService.createDiscussion(discussOptions).then((res: any) => {
        this.QuestionAnswerform.reset();
        this.DiscussFormGroup.reset();
        this.alert.successToast("Discussion Question saved Successfully");
        this.discussSaveSpinner = false;
      }).catch((err: any) => {
        this.discussSaveSpinner = false;

        this.alert.errorToast("Error Saving Discussion Question Data");
      })

    });
  }

  onReady(e: any) {

  }

  async getAllEvaluations() {
    await this.ilaTraineeEvalService.getByIla(this.ILAID).then((res) => {

      this.allEvaluations = res;
    }).catch((err: any) => {
    })

    this.ilaEvaluations.forEach((i) => {
      this.evalTypes.forEach((j) => {
        if (i.evaluationTypeId === j.id) {
          i.evaluationTypeId = j.name;
        }
      })
    })


  }

  recieveQuestionBankItems(d: any) {
    d.forEach(async (i: any) => {
      var question = await this.testItemService.get(i);
      var ids = this.answer_array.map((data) => data.id);
      if (!ids.includes(question.id)) {
        this.answer_array.push({
          id: question.id,
          description: question.description,
          taxType: question.taxonomyLevel.description,
          quesType: question.testItemType.description,
        })
        this.addedQuestions.push(question);
      }
      else {
        this.answer_array = this.answer_array.map((data) => {
          if (data.id === question.id) {
            data.description = question.description;
            data.taxType = question.taxonomyLevel.description;
            data.quesType = question.testItemType.description;
          }
          return data;
        });
        this.addedQuestions = this.addedQuestions.map((data) => {
          if (data.id === question.id) {
            data = question;
          }
          return data;
        })
      }
    });
    this.dataTransfered = true;
  }

  async questionAdded(event: any) {
    this.dataTransfered = true;
    //this.flyPanelService.close();
    var question = await this.testItemService.get(event);
    var ids = this.answer_array.map((data) => data.id);
    if (!ids.includes(question.id)) {
      this.answer_array.push({
        id: question.id,
        description: question.description,
        taxType: question.taxonomyLevel.description,
        quesType: question.testItemType.description,
      })
      this.addedQuestions.push(question);
    }
    else {
      this.answer_array = this.answer_array.map((data) => {
        if (data.id === question.id) {
          data.description = question.description;
          data.taxType = question.taxonomyLevel.description;
          data.quesType = question.testItemType.description;
        }
        return data;
      });
      this.addedQuestions = this.addedQuestions.map((data) => {
        if (data.id === question.id) {
          data = question;
        }
        return data;
      })
    }
  }

  async saveWrittenEval(shouldReset: boolean = true) {
    this.spinner = true;
    var testOptions = new TestCreateOptions();
    testOptions.testStatusId = null;
    testOptions.testTitle = this.WrittenFormGroup.get('title')?.value;
    if (this.editId === null) {
      var test = await this.testService.create(testOptions);
    }
    else {
      var test = await this.testService.update(this.editId, testOptions);
    }
    var options = new ILATraineeEvaluationCreateOptions();
    options.testTitle = this.WrittenFormGroup.get('title')?.value;
    options.evaluationTypeId = this.evaluationTypeId;
    options.ilaId = this.ILAID;
    options.testTypeId = this.WrittenFormGroup.get('radio')?.value;
    options.testInstruction = this.WrittenFormGroup.get('Instruction')?.value;
    options.testTimeLimitHours = this.WrittenFormGroup.get('TimeLimitHours')?.value;
    options.testTimeLimitMinutes = this.WrittenFormGroup.get('TimeLimitMins')?.value;
    options.testId = test.id;
    if (options.testTimeLimitHours === null) {
      options.testTimeLimitHours = 0;
    }
    if (options.testTimeLimitMinutes === null) {
      options.testTimeLimitMinutes = 0;
    }
    await this.ilaTraineeEvalService.create(options).then(async (_) => {
      var testLink = await this.testService.UnlinkAllTestItems(test.id);
      if (this.addedQuestions.length > 0) {
        var questionLinks = new Test_TestItem_LinkOptions();
        questionLinks.testId = test.id;
        this.addedQuestions.forEach((data) => {
          questionLinks.testItemIds.push(data.id);
        });
        this.testService.LinkTestItem(test.id, questionLinks).then((_) => {
          this.ilaEvaluations = [];
          this.getAllEvaluations();
          this.alert.successToast(`Written Evaluation ${this.editId === null ? 'Saved' : 'Updated'} Successfully`);
          this.setAssessmentTypeForm();
          this.editId = null;
          if (shouldReset) {
            this.dataTransfered = false;
            this.WrittenFormGroup.reset(this.initialWritteFormValues);
            this.answer_array = [];
            this.addedQuestions = [];
          }
        }).finally(() => {
          this.spinner = false;
        })
      }
      else {
        if (shouldReset) {
          this.dataTransfered = false;
          this.WrittenFormGroup.reset(this.initialWritteFormValues);
          this.answer_array = [];
          this.addedQuestions = [];
        }
        this.setAssessmentTypeForm();
        this.alert.successToast("Written Evaluation Saved Successfully");
        this.ilaEvaluations = [];
        this.getAllEvaluations();
        this.spinner = false;
      }
    })
  }

  async saveDiscussionType(shouldReset: boolean = true) {
    this.discussSaveSpinner = true;
    var testOptions = new TestCreateOptions();
    testOptions.testStatusId = null;
    testOptions.testTitle = this.DiscussFormGroup.get('Title')?.value;
    if (this.editId === null) {
      var test = await this.testService.create(testOptions);
    }
    else {
      var test = await this.testService.update(this.editId, testOptions);
      var message = await this.ilaTraineeEvalService.DeleteAllQuestions(this.editId);
    }
    var options = new ILATraineeEvaluationCreateOptions();
    options.evaluationTypeId = this.evaluationTypeId;
    options.testId = test.id;
    options.ilaId = this.ILAID;
    options.testTitle = this.DiscussFormGroup.get('Title')?.value;
    options.testInstruction = this.DiscussFormGroup.get('Instruction')?.value;
    var trEval = await this.ilaTraineeEvalService.create(options);
    this.getAllEvaluations();
    var discussOptions = new DiscussionQuestionCreateOptions();

    discussOptions.iLATraineeEvaluationId = trEval.id;
    this.quesAns.controls.forEach(async (element, index) => {


      discussOptions.questionText = element.value.questionText;
      discussOptions.answerKeywords = element.value.answerText;
      if (discussOptions.questionText && discussOptions.answerKeywords && discussOptions.questionText !== '' && discussOptions.answerKeywords !== '') {
        await this.ilaTraineeEvalService.createDiscussion(discussOptions).finally(() => {
          this.discussSaveSpinner = false;
        })
      }
    });
    this.alert.successToast("Discussion Evaluation Created Successfully");
    this.setAssessmentTypeForm();
    if (shouldReset) {
      this.DiscussFormGroup.reset(this.initialDiscussFormValues);
      this.QuestionAnswerform.reset();
      // this.quesAns = new FormArray([]);
      // var formArray = new FormArray([]);
      //this.QuestionAnswerform.get('quesAns')?.setValue(this.fb.array([]));
      if(this.quesAns.controls.length > 3){
       for(let i = this.quesAns.controls.length - 1;i>3;i--){
        this.quesAns.removeAt(i)
       }
      }
      // for (let i = 0; i < 3; i++) {
      //   this.addArray();
      // }
      // this.quesAns = formArray;
      this.editId = null;
      this.quesAns.updateValueAndValidity();
      this.QuestionAnswerform.updateValueAndValidity();
      this.DiscussFormGroup.updateValueAndValidity();
    }

    //this.getAllEvaluations();
  }

  selectSheet(cover: CoverSheet, type: 'Perform' | 'Discuss' | 'Simulations') {
    switch (type) {
      case 'Discuss':
        this.DiscussFormGroup.get('Instruction')?.setValue(cover.coversheetInstructions)
        this.DiscussFormGroup.updateValueAndValidity();
        break;
      case 'Perform':
        break;
    }
  }

  async editTest(type:any,data: any) {
    this.isCreateTestVisible = true;
    this.WrittenStatus = true;
    this.assessmentTypeForm.get('assessmentType').setValue(type);
    this.assessmentTypeForm.get("assessmentType").disable();
    if (type==="Perform") {
    var evId = this.evalTypes.find(x => x.name ==="Perform");
    this.ShowType("Perform",evId);
    }else{
      this.disableButton = Array(this.evalTypes.length).fill(false);
      var disable = this.evalTypes.map((data) => { return data.name }).indexOf(data.traineeEvaluationType.name);
      this.disableButton[disable] = true;
      switch (data.traineeEvaluationType.name.trim().toLowerCase()) {
        case 'written':
          this.answer_array = [];
          this.addedQuestions = [];
          this.evaluationTypeId = data.evaluationTypeId;
          this.WrittenType();
          this.isShowCreateTestVisible = false;
          this.editId = data.testId;
          this.WrittenFormGroup.patchValue({
            title: data.testTitle,
            Instruction: data.testInstruction,
            radio: data.testTypeId,
            TimeLimitHours: data.testTimeLimitHours,
            TimeLimitMins: data.testTimeLimitMinutes,
          });
          var questions = await this.testService.GetTestItemLinkedToTest(data.testId);
          questions.forEach((question) => {
            var ids = this.answer_array.map((data) => data.id);
            if (!ids.includes(question.id)) {
              this.answer_array.push({
                id: question.id,
                description: question.description,
                taxType: question.taxonomyLevel.description,
                quesType: question.testItemType.description,
              })
              this.addedQuestions.push(question);
            }
            else {
              this.answer_array = this.answer_array.map((data) => {
                if (data.id === question.id) {
                  data.description = question.description;
                  data.taxType = question.taxonomyLevel.description;
                  data.quesType = question.testItemType.description;
                }
                return data;
              });
              this.addedQuestions = this.addedQuestions.map((data) => {
                if (data.id === question.id) {
                  data = question;
                }
                return data;
              })
            }
          });
          if(this.addedQuestions.length>0){
            this.dataTransfered = true;
          }
          break;
        case 'discuss':
          this.editId = data.testId;
          this.DiscussType();
          this.evaluationTypeId = data.evaluationTypeId;
          this.DiscussFormGroup.reset(this.initialDiscussFormValues);
          this.QuestionAnswerform.reset();
          if(this.quesAns.controls.length > 3){
            for(let i = this.quesAns.controls.length - 1;i>3;i--){
             this.quesAns.removeAt(i)
            }
           }
          this.quesAns.updateValueAndValidity();
          this.QuestionAnswerform.updateValueAndValidity();
          this.DiscussFormGroup.updateValueAndValidity();
          this.DiscussFormGroup.patchValue({
            Title: data.testTitle,
            Instruction: data.testInstruction,
          })
          this.DiscussFormGroup.updateValueAndValidity();
          var discussQuestions = await this.ilaTraineeEvalService.getDiscussionQuestions(data.id);

          discussQuestions.forEach((que, i) => {
            if (i > 2) {
              this.addArray();
            }
            this.quesAns.controls[i].patchValue({
              questionText:que.questionText,
              answerText:que.answerKeywords,
            });
          });
          break;
        case 'simulations':
          this.editId = data.testId;
          this.SimulationType();
          this.evaluationTypeId = data.evaluationTypeId;
          this.SimulationFormGroup.patchValue({
            Title: data.testTitle,
            Instruction: data.testInstruction,
            TimeLimitHours: data.testTimeLimitHours,
            TimeLimitMins: data.testTimeLimitMinutes,
          })
          break;
      }
    }
  }

  //edit ila written
  //for written's create new test item fly panel
  //show the table and then use edit button
  OnEditTraineeEvaluation(ev:any){
    this.editInstruction = true;
    let temp =  this.evalTypes.find(t=>t.name ===ev.evaluationTypeId);
    this.ShowType(ev.evaluationTypeId, temp.id);
    this.readyEditEvaluationForms(ev);
  }

  async readyEditEvaluationForms(ev:any){
    switch(ev.evaluationTypeId){
      case 'Written':
        let temp = this.testType.find(t=>t.id ===ev.testTypeId);
        this.WrittenFormGroup.patchValue({
          title : ev.testTitle,
          radio : temp.id,
          Instruction : ev.testInstruction,
          TimeLimitHours : ev.testTimeLimitHours,
          TimeLimitMins : ev.testTimeLimitMinutes,
        });
        this.evaluationForm.get('eval')?.setValue(ev.trainingEvaluationMethod);

        let tempsrc : any = [];
        await this.testService.getTestItemLinked(ev.testId).then((res)=>{
          tempsrc = res;
          tempsrc.forEach((i)=>{
            this.answer_array.push({
              id: i.id,
              description : i.description.replace("&nbsp;+",'').replace(/<[^>]+>/g, ''),
              taxType : i.taxonomyId,
              quesType : i.testItemTypeId
            })
          })
        }).catch((err)=>{

        });
        this.readyCreateNewTestEditTable(this.answer_array);

        break;

      case 'Discuss':
        this.DiscussFormGroup.patchValue({
          Title : ev.testTitle,
          Instruction : ev.testInstruction
        });

        //to do questionAnswer
        let tempSrc : any = [];
        await this.ilaTraineeEvalService.getDiscussionQuestions(ev.id).then((res)=>{
          tempSrc = res;
        }).catch((err)=>{

        });
        break;

      case 'Simulations':
        this.SimulationFormGroup.patchValue({
          Title : ev.testTitle,
          Instructions : ev.testInstruction,
          TimeLimitHours : ev.testTimeLimitHours,
          TimeLimitMins : ev.testTimeLimitMinutes,
        });
        break;

      case 'Perform':
        break;
    }
  }

   readyCreateNewTestEditTable(answerArray : any){
    answerArray.forEach((i)=>{
      this.taxonomyArray.forEach((element:any) => {
        if(element.id === i.taxType)
        i.taxType = element.description;
      });

      this.testItemTypeArray.forEach((element:any) => {
        if(element.id === i.quesType){
          i.quesType = element.description;
        }
      });
    })
   this.dataTransfered = true;
  }

  async savePerformData(){
    // this.performSpinner = true;
    // var options = new ILA_PerformTraineeEvalCreateOptions();
    // await this.ilaService.createPerformEval(this.ILAID,options).then(res =>{
    //   this.performSpinner = false;
    // });
    // await this.getAllEvaluations();

    this.performSpinner = true;
    var options = new ILA_PerformTraineeEvalCreateOptions();
    var data = await this.ilaService.createPerformEval(this.ILAID,options);
    var linkOptions = new ILATaskObjectiveLinkOption();
    linkOptions.ilaId = this.ILAID;
    linkOptions.taskIds = this.taskSelection.selected;
    await this.ilaService.UpdateTObjUsedForTQ(this.ILAID,linkOptions).then(async (_)=>{
      this.alert.successAlert(await this.transformTitle('Task') + " Objectives Created as Perform Evaluation","In Order to Use these Objectives as"+ await this.transformTitle('Task') +"Qualification Please Fill out the TQ Release Settings in Segment Review & EMP Settings Tab.");
    }).finally(()=>{
      this.performSpinner = false;
    })
    await this.getAllEvaluations();
    this.setAssessmentTypeForm();
  }

}
