import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup } from '@angular/forms';
import { DutyArea } from 'src/app/_DtoModels/DutyArea/DutyArea';
import { Employee } from 'src/app/_DtoModels/Employee/Employee';
import { EnablingObjective } from 'src/app/_DtoModels/EnablingObjective/EnablingObjective';
import { EnablingObjective_Category } from 'src/app/_DtoModels/EnablingObjective_Category/EnablingObjective_Category';
import { EnablingObjective_SubCategory } from 'src/app/_DtoModels/EnablingObjective_SubCategory/EnablingObjective_SubCategory';
import { EnablingObjective_Topic } from 'src/app/_DtoModels/EnablingObjective_Topic/EnablingObjective_Topic';
import { Position } from 'src/app/_DtoModels/Position/Position';
import { SubdutyArea } from 'src/app/_DtoModels/SubdutyArea/SubdutyArea';
import { Task } from 'src/app/_DtoModels/Task/Task';
import { TaskWithNumberVM } from 'src/app/_DtoModels/Task/TaskWithNumberVM';
import { TaskQualificationFilterOptions } from 'src/app/_DtoModels/TaskQualificationFilter/TaskQualificationFilterOptions';
import { TrainingGroup } from 'src/app/_DtoModels/TrainingGroup/TrainingGroup';
import { DutyAreaService } from 'src/app/_Services/QTD/duty-area.service';
import { EmployeesService } from 'src/app/_Services/QTD/employees.service';
import { EnablingObjectivesCategoryService } from 'src/app/_Services/QTD/enabling-objectives-category.service';
import { EnablingObjectivesTopicService } from 'src/app/_Services/QTD/enabling-objectives-topic.service';
import { EnablingObjectivesService } from 'src/app/_Services/QTD/enabling-objectives.service';
import { PositionsService } from 'src/app/_Services/QTD/positions.service';
import { TasksService } from 'src/app/_Services/QTD/tasks.service';
import { TrainingGroupService } from 'src/app/_Services/QTD/training-group.service';

@Component({
  selector: 'app-flypanel-task-requal-filter',
  templateUrl: './flypanel-task-requal-filter.component.html',
  styleUrls: ['./flypanel-task-requal-filter.component.scss']
})
export class FlypanelTaskRequalFilterComponent implements OnInit {
  @Output() closed = new EventEmitter<any>();
  @Output() filter = new EventEmitter<TaskQualificationFilterOptions>();
  @Input() filterUsed!: TaskQualificationFilterOptions | null;
  filterOptions!: TaskQualificationFilterOptions | null;
  filterForm = new UntypedFormGroup({});
  positions: Position[] = [];
  dutyAreas: DutyArea[] = [];
  subDutyArea: SubdutyArea[] = [];
  tasks: Task[] = [];
  sqs: EnablingObjective[] = [];
  trainingGroups: TrainingGroup[] = [];
  categories: EnablingObjective_Category[] = [];
  subCategory: EnablingObjective_SubCategory[] = [];
  topics: EnablingObjective_Topic[] = [];
  emps: Employee[] = [];
  evals: any[] = [];
  filterBy = "";
  isLoading: boolean = false;

  initialValues!: any;


  constructor(
    private posService: PositionsService,
    private dutyAreaService: DutyAreaService,
    private taskService: TasksService,
    private eoService: EnablingObjectivesService,
    private categoryService: EnablingObjectivesCategoryService,
    private topicService: EnablingObjectivesTopicService,
    private empService: EmployeesService,
    private trainingGroupService: TrainingGroupService,
  ) { }

  ngOnInit(): void {
    this.readyForm();
    this.readyAllData();
  }

  readyForm() {
    this.filterForm.addControl('position', new UntypedFormControl(null));
    this.filterForm.addControl('da', new UntypedFormControl(null));
    this.filterForm.addControl('sda', new UntypedFormControl(null));
    this.filterForm.addControl('task', new UntypedFormControl(null));
    this.filterForm.addControl('category', new UntypedFormControl(null));
    this.filterForm.addControl('subCategory', new UntypedFormControl(null));
    this.filterForm.addControl('topic', new UntypedFormControl(null));
    this.filterForm.addControl('sq', new UntypedFormControl(null));
    this.filterForm.addControl('group', new UntypedFormControl(null));
    this.filterForm.addControl('emp', new UntypedFormControl(null));
    this.filterForm.addControl('eval', new UntypedFormControl(null));
    this.filterForm.addControl('rrt', new UntypedFormControl(false));
    this.filterForm.addControl('meta', new UntypedFormControl(false));
    this.filterForm.addControl('dateRange', new UntypedFormControl(false));
    this.initialValues = Object.assign({}, this.filterForm.value);
    if (this.filterUsed) {
      this.setFormValues();
    }
  }

  setFormValues() {
    this.filterForm.patchValue({
      position: this.filterUsed?.posId,
      da: this.filterUsed?.daId,
      sda: this.filterUsed?.sdaId,
      task: this.filterUsed?.taskId,
      category: this.filterUsed?.sqCatId,
      subCategory: this.filterUsed?.sqSubCatId,
      topic: this.filterUsed?.sqTopicId,
      sq: this.filterUsed?.sqTopicId,
      emp: this.filterUsed?.empId,
      eval: this.filterUsed?.evalId,
      rrt: this.filterUsed?.rrtOnly,
      meta: this.filterUsed?.metaOnly,
      dateRange: this.filterUsed?.specificDateRange,
    });
    this.filterForm.updateValueAndValidity();
    this.loadSDA(false);
    this.loadTasks(false);
    this.filterOptions = this.filterUsed;

  }


  async readyAllData() {
    this.isLoading = true;
    this.positions = await this.posService.getAllOrderBy('name');
    this.dutyAreas = await this.dutyAreaService.getAllOrderBy('num');
    // this.subDutyArea = await this.dutyAreaService.getAllSubDutyAreas();
    // this.tasks = await this.taskService.getAll();
    this.categories = await this.eoService.getAllSQSOrderBy('num');
    // this.subCategory = await this.categoryService.getAllSubCategories();
    // this.topics = await this.topicService.getAllTopics();
    //this.sqs = await this.eoService.getAllSQSOrderBy('num');
    //this.emps = await this.empService.getAll();
    this.evals = await this.empService.getAllEvaluatorsNamesOnly();
    this.trainingGroups = await this.trainingGroupService.getAll();
    this.isLoading = false;
  }

  sdaLoader = false;

  async loadSDA(shouldClear = true) {
    if (shouldClear) {
      this.filterForm.get('sda')?.setValue(null);
      this.filterForm.get('task')?.setValue(null);
      this.filterForm.updateValueAndValidity();
    }
    var daId = this.filterForm.get('da')?.value;
    if (daId !== null && daId !== 0) {
      this.sdaLoader = true;
      this.subDutyArea = await this.dutyAreaService.getSdasWithNumByDAID(daId);
      this.sdaLoader = false;
    }
  }

  taskLoader = false;
  async loadTasks(shouldClear = true) {
    this.tasks = [];
    if(shouldClear){
      this.filterForm.get('task')?.setValue(null);
    }
    this.filterForm.updateValueAndValidity();
    var sdaId = this.filterForm.get('sda')?.value;

    if (sdaId !== null && sdaId !== 0) {
      this.taskLoader = true;
      var taskWithNum: TaskWithNumberVM[] = await this.taskService.getTasksBySDAId(sdaId);
      taskWithNum.forEach((data) => {
        data.task.description = data.letter + data.daNumber + "." + data.sdaNumber + "." + data.task.number + " - " + data.task.description;
        this.tasks.push(data.task);
      })
      this.taskLoader = false;
    }
  }

  onReset(keys: string[]) {
    var data: any = Object.assign({}, this.initialValues);
    keys.forEach((element) => {
      data[element] = this.filterForm.get(element)?.value;
    });
    this.filterForm.reset(data);
    if(!keys.includes('task')){
      this.subDutyArea = [];
      this.tasks = [];
    }

    if(!keys.includes('sq')){
      this.subCategory = [];
      this.topics = [];
      this.sqs = [];
    }
  }

  isValid() {
    var val = Object.values(this.filterForm.value).every((val) => {
      return (val === null || val === false)
    });
    return val;
  }

  emitFilterValues() {
    var formData = this.filterForm.value;
    this.filterOptions = {
      posId: formData['position'],
      daId: formData['da'],
      sdaId: formData['sda'],
      taskId: formData['task'],
      sqCatId: formData['category'],
      sqSubCatId: formData['subCategory'],
      sqTopicId: formData['topic'],
      sqId: formData['sq'],
      empId: formData['emp'],
      evalId: formData['eval'],
      rrtOnly: formData['rrt'],
      metaOnly: formData['meta'],
      specificDateRange: formData['dateRange'],
      taskTGId: formData['group'],
      filterBy: (this.filterBy === "" ? (this.filterUsed?.filterBy ?? "") : this.filterBy),
    }
    this.filter.emit(this.filterOptions);
  }

  subCatLoader = false;
  async getSubCat(shouldClear = true){
    this.subCatLoader = true;
    if(shouldClear){
      this.filterForm.get('subCategory')?.setValue(null);
      this.filterForm.get('topic')?.setValue(null);
      this.filterForm.get('sq')?.setValue(null);
      this.filterForm.updateValueAndValidity();
    }
    var catId = this.filterForm.get('category')?.value;
    this.subCategory = await this.categoryService.getSubCategoriesWithNumber(catId);

    this.subCatLoader = false;
  }

  topicLoader = false;
  eoLoader = false;
  async getSQAndTopics(shouldClear = true){
    this.topicLoader = true;
    this.eoLoader = true;
    if(shouldClear){
      this.filterForm.get('topic')?.setValue(null);
      this.filterForm.get('sq')?.setValue(null);
      this.filterForm.updateValueAndValidity();
    }

    var subCatId = this.filterForm.get('subCategory')?.value;
    this.topics = await this.categoryService.getTopicWithNumber(subCatId);
    var options = new SQWithNumberVM();
    options.subCatId = subCatId;
    options.topicId = null;
    this.sqs = await this.eoService.getSqWithNumber(options);

    this.topicLoader = false;
    this.eoLoader = false;
  }

  async getSQ(shouldClear = true){
    this.eoLoader = true;
    if(shouldClear){
      this.filterForm.get('sq')?.setValue(null);
      this.filterForm.updateValueAndValidity();
    }
    var subCatId = this.filterForm.get('subCategory')?.value;
    var topicId = this.filterForm.get('topic')?.value;
    var options = new SQWithNumberVM();
    options.subCatId = subCatId;
    options.topicId = topicId;
    this.sqs = await this.eoService.getSqWithNumber(options);
    this.eoLoader = false;
  }
}

export class SQWithNumberVM{
  subCatId!:any;
  topicId?:any;
}
