import { SelectionModel } from '@angular/cdk/collections';
import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';
import { BreakpointObserver } from '@angular/cdk/layout';
import { StepperOrientation } from '@angular/cdk/stepper';
import { NestedTreeControl } from '@angular/cdk/tree';
import { Component, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { MatStepper } from '@angular/material/stepper';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Employee } from 'src/app/_DtoModels/Employee/Employee';
import { EmployeePosition } from 'src/app/_DtoModels/EmployeePosition/EmployeePosition';
import { EMPFilterOptions } from 'src/app/_DtoModels/FilterOptions/EMPFilterOptions';
import { TQEvaluatorWithCount } from 'src/app/_DtoModels/TaskQualification/TQEvaluatorWithCount';
import { DutyAreaService } from 'src/app/_Services/QTD/duty-area.service';
import { EmployeesService } from 'src/app/_Services/QTD/employees.service';
import { TaskRequalificationService } from 'src/app/_Services/QTD/task-requalification.service';
import { TasksService } from 'src/app/_Services/QTD/tasks.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { ReviewByOptions } from '../flypanel-release-task-qualification/flypanel-release-task-qualification.component';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { cloneDeep } from 'lodash';
import { TQReleaseByTaskAndSkillOptions } from '@models/TaskQualification/TQReleaseByTaskAndSkillOptions';
import { EO_LinkPositions } from '@models/EnablingObjective/EO_LinkPositions';
import { EOTree } from '@models/EnablingObjective/EOTree';

@Component({
  selector: 'app-flypanel-release-task-qual-by-emp',
  templateUrl: './flypanel-release-task-qual-by-emp.component.html',
  styleUrls: ['./flypanel-release-task-qual-by-emp.component.scss']
})
export class FlypanelReleaseTaskQualByEmpComponent implements OnInit {
  @Output() closed = new EventEmitter<any>();
  @Output() refresh = new EventEmitter<any>();
  treeControl = new NestedTreeControl<any>((node: any) => node.children);
  dataSource = new MatTreeNestedDataSource<any>();
  originalSource = new MatTreeNestedDataSource<any>();
  originalEOSource = new MatTreeNestedDataSource<EOTree>();
  stepperOrientation: Observable<StepperOrientation>;
  searchText:string='';
  shouldRefreshTasks = false;

  placeHolder = '/assets/img/ImageNotFound.jpg';
  alreadyLinked: any[] = [];
  hasChild = (_: number, node: TaskTree) =>
    !!node.children && node.children.length > 0;

  @ViewChild('stepper') stepper !: MatStepper;

  employees: Employee[] = [];
  originalEmployees: Employee[] = [];
  saveSpinner = false;

  filterTaskString = "";
  filterEMPString = "";
  evalData: any[] = [];

  filterTaskBy: 'Positions' | 'All' = 'All';
  filterBy: 'Positions' | 'All' = 'All';
  filterByEMP: 'All' | 'Positions' | 'Organisations' = 'All';
  filterActiveEMP = true;
  displayedColumns = ["cb", 'order', 'eval', 'pos'];
  displayedColumnsReview = ['emp', 'task', 'skill', 'eval'];
  filterEvalString = "";
  selectedPosTask = "";
  selectedPos:any = null;
  evaluators: TQEvaluatorWithCount[] = [];
  evalSelection = new SelectionModel<any>(true, []);

  dataSourceEval = new MatTableDataSource<any>();
  dataSourceReview = new MatTableDataSource<any>();

  empSelection = new SelectionModel<any>(true, []);
  positionSelection = new SelectionModel<any>(true, []);
  taskSelection = new SelectionModel<any>(true, []);
  releaseSelection = new SelectionModel<any>();
  whenToReleaseSelection = new SelectionModel<any>();

  reqSignOffs = 1;
  taskIds: any[] = [];
  posIds: any[] = [];

  selectedOrgEMP = "";
  selectedPosEMP = "";

  filterPanelOrg = false;
  filterPanelEMP = false;
  filterPanelTask = false;
  filterPanelSkill = false;
  showLoader = false;
  linkedEOIds: any[] = [];
  enablingObjectiveDataSource = new MatTreeNestedDataSource<EOTree>();
  EOTreeControl = new NestedTreeControl<EOTree>((node: any) => node.children);
  EOCheckListSelection = new SelectionModel<EOTree>(true);
  filterEOString = "";

  reviewForm = new UntypedFormGroup({});

  constructor(
    private breakpointObserver: BreakpointObserver,
    private empService: EmployeesService,
    private tqService: TaskRequalificationService,
    private alert: SweetAlertService,
    private dataBroadcastService : DataBroadcastService,
  ) {
    this.stepperOrientation = breakpointObserver
      .observe('(min-width: 800px)')
      .pipe(map(({ matches }) => (matches ? 'horizontal' : 'vertical')));
  }

  ngOnInit(): void {
    // Ready All the Temporary Data
    this.readyReviewForm();
    this.readyEMPData();
    this.readyEvalData();
    // this.readyReviewData();
  }
  onTabChange(event: any) {
    if (event.index === 1 && this.enablingObjectiveDataSource.data.length === 0) {
      this.showLoader = true;
      this.readyEnablingObjectiveTreeData();
    }
  }

  readyReviewForm() {
    this.reviewForm.addControl('specific', new UntypedFormControl(false));
    this.reviewForm.addControl('questions', new UntypedFormControl(false));
    this.reviewForm.addControl('release', new UntypedFormControl('', Validators.required));
    this.reviewForm.addControl('due', new UntypedFormControl('', Validators.required));
    this.reviewForm.addControl('startTime',new UntypedFormControl({ value: '', disabled: true }, Validators.required));
    this.setupTimeField('release', 'startTime');
  }

  private setupTimeField(dateControlName: string, timeControlName: string): void {
    const dateControl = this.reviewForm.get(dateControlName);
    const timeControl = this.reviewForm.get(timeControlName);
  
    dateControl?.valueChanges.subscribe(date => {
      if (date) {
        if (!timeControl?.value) {
          timeControl?.setValue('08:00');
        }
        timeControl?.enable();
      } else {
        timeControl?.reset();
        timeControl?.disable();
      }
    });
    const initialDate = dateControl?.value;
    if (initialDate) {
      if (!timeControl?.value) {
        timeControl?.setValue('08:00');
      }
      timeControl?.enable();
    }
  }


  stepChange(event: any) {
    if (event.selectedIndex === 2) {
      this.checkEvaluators(false);
      if (this.shouldRefreshTasks) {
        this.readyTasksTreeData();
      }
    }
    else if (event.selectedIndex === 3) {
      this.buildReviewByOptions();
    }
  }

  async readyEvalData() {
    this.evaluators = await this.tqService.getEvaluatorWithCount();
    this.evaluators.forEach((evalu, i) => {
      this.evalData.push(
        {
          evaluatorId: evalu.evaluatorId,
          evaluatorFName: evalu?.evaluatorFName,
          evaluatorLName: evalu?.evaluatorLName,
          evaluatorName: evalu?.evaluatorFName + " " + evalu?.evaluatorLName,
          positionTitle: evalu.positionTitle,
          count: evalu.count,
          number: i + 1,
        })
    });

    this.dataSourceEval.data = this.evalData;
    this.dataSourceEval.data = this.dataSourceEval.data.sort((a, b) =>
      a.evaluatorFName.localeCompare(b.evaluatorFName)
    )
  }

  async readyTasksTreeData() {
    var options = new EMPFilterOptions();
    options.positionIds = [...new Set(this.posIds)];
    options.type = "Some";
    var tasks = await this.tqService.getTaskTreeDataForPosition(options);
    this.makeTaskTreeDataSource(tasks);
    this.shouldRefreshTasks = false;
  }

  // Make a specialized and simplified data source for the mat tree structure
  makeTaskTreeDataSource(res: any) {
    var treeData: any[] = [];
    var index = 1;
    this.taskSelection.clear();
    this.taskIds = [];
    var result = cloneDeep(res);
    for (var data in result) {
      treeData[data] = {
        id: res[data]['id'],
        description: res[data]['letter'] + res[data]['number'] + ' - ' + res[data]['title'],
        children: cloneDeep(res[data]['subdutyAreas']),
        checkbox: true,
        checked: false,
        isTask: false,
        selected: false,
        indeterminate: false,
      };
      for (var data1 in treeData[data]['children']) {
        treeData[data]['children'][data1] = {
          id: res[data]['subdutyAreas'][data1]['id'],
          description: res[data]['letter'] + res[data]['number'] + '.' + res[data]['subdutyAreas'][data1]['subNumber'] + ' - ' + res[data]['subdutyAreas'][data1]['title'],
          children: res[data]['subdutyAreas'][data1]['tasks'],
          checkbox: true,
          checked: false,
          isTask: false,
          selected: false,
          indeterminate: false,
        };

        for (var data2 in treeData[data]['children'][data1]['children']) {
          //treeData[data]['children'][data1]['children'][data2]['description'] = index + '.' + childIndex + '.' + treeData[data]['children'][data1]['children'][data2]['number']
          treeData[data]['children'][data1]['children'][data2]['checkbox'] = true;
          treeData[data]['children'][data1]['children'][data2]['description'] = res[data]['letter'] + res[data]['number'] + '.' + res[data]['subdutyAreas'][data1]['subNumber'] + '.' + treeData[data]['children'][data1]['children'][data2]['number'] + ' - ' + treeData[data]['children'][data1]['children'][data2]['description'];
          treeData[data]['children'][data1]['children'][data2]['checked'] = false;
          treeData[data]['children'][data1]['children'][data2]['isTask'] = true;
          treeData[data]['children'][data1]['children'][data2]['selected'] = false;
          treeData[data]['children'][data1]['children'][data2]['indeterminate'] = false;
        }
      }
    }
    this.dataSource.data = treeData;
    this.originalSource.data = Object.assign(this.dataSource.data);
    Object.keys(this.dataSource.data).forEach((key: any) => {
      this.setParent(this.dataSource.data[key], undefined);
      this.setParent(this.originalSource.data[key], undefined);
    });

    this.filter();
  }

  filter() {
    let temparr: any[] = []
    if (this.selectedPosTask !== "") {
      temparr = [
        ...this.originalSource.data.map((element) => {
          return {
            ...element,
            children: element.children?.map((e) => {

              return {
                ...e,
                children: e.children?.filter((c) => {
                  return (c.description.toLowerCase().trim().includes(this.filterTaskString.trim().toLowerCase()) && c.position_Tasks.map((data) => data.positionId).includes(this.selectedPosTask));
                })
              }
            }
            ),
          };
        }),
      ]
    }
    else {
      temparr = [
        ...this.originalSource.data.map((element) => {
          return {
            ...element,
            children: element.children?.map((e) => {

              return {
                ...e,
                children: e.children?.filter((c) => {
                  return (c.description.toLowerCase().trim().includes(this.filterTaskString.trim().toLowerCase()));
                })
              }
            }
            ),
          };
        }),
      ]
    }
    this.dataSource.data = temparr;
    Object.keys(this.dataSource.data).forEach((key: any) => {
      this.setParent(this.dataSource.data[key], undefined);
    });

    this.dataSource.data.forEach((da) => {
      da.children?.forEach((sda) => {
        sda.children?.forEach((task) => {
          this.checkAllParents(task);
        })
      })
    })

    this.treeControl.dataNodes = temparr;

    this.filterTaskString.length > 0 ? this.treeControl.expandAll() : this.treeControl.collapseAll();
  }

  private setParent(node: TaskTree, parent: TaskTree | undefined) {
    node.parent = parent;
    if (node.children) {
      node.children.forEach((childNode) => {
        this.setParent(childNode, node);
      });
    }
  }

  private checkAllParents(node: TaskTree) {
    if (node.parent) {
      const descendants = this.treeControl.getDescendants(node.parent);
      node.parent.selected = descendants.every((child) => (child.selected));
      node.parent.indeterminate = descendants.some((child) => (child.selected));
      this.checkAllParents(node.parent);
    }
  }

  async readyEMPData() {
    this.employees = await this.empService.getEmpWithPosAndOrg();
    this.employees = this.employees.filter((x) =>{
      return x.active == true;
    })
    this.originalEmployees = Object.assign(this.employees);
  }

  evalInputChange(event:any) {
    const searchText = event.target.value.trim().toLowerCase();
    this.searchText = searchText;
    const filteredData = this.evalData.filter(evaluator=>{
      return evaluator?.evaluatorName?.trim()?.toLowerCase()?.includes(searchText) || evaluator?.positionTitle?.substring(0,evaluator.positionTitle.length-2)?.trim()?.toLowerCase()?.includes(searchText);
    });
    this.dataSourceEval = new MatTableDataSource(filteredData);
  }

  selectAllEmp(event: any) {
    this.employees.forEach((data: Employee) => {
      this.selectEMP(event, data);
    })
  }

  selectEMP(event: any, emp: Employee) {
    if (event.checked) {
      this.empSelection.select(emp.id);
      // this.empSelection.deselect(data.id);
      emp.employeePositions.forEach((pos: EmployeePosition) => {
        this.posIds.push(pos.positionId);
      })
    }
    else {
      this.empSelection.deselect(emp.id);
      emp.employeePositions.forEach((pos: EmployeePosition) => {
        var index = this.posIds.findIndex(x => x === pos.positionId);
        if (index > -1) {
          this.posIds.splice(index, 1);
        }
      })
    }

    this.shouldRefreshTasks = true;
  }

  onTaskChange(event: any, node: any) {
    if (event.checked) {
      node.selected = true;
      // this.taskSelection.select(node);
    } else {
      node.selected = false;
      // this.taskSelection.deselect(node);
    }
    this.itemToggle(event.checked, node);
  }

  itemToggle(checked: boolean, node: any) {
    node.selected = node.checkbox ? checked : false;
    if (node.children !== undefined) {
      node.children.forEach((child) => {
        this.itemToggle(checked, child);
      });
    } else {
      // Add TaskIds to list
      if (node.selected && node.checkbox) {
        this.taskIds.push(node.id);
        this.taskSelection.select(node);
      }
      else {
        this.taskSelection.deselect(node);
        var index1 = this.taskIds.indexOf(node.id);
        if (index1 > -1) {
          this.taskIds.splice(index1, 1);
        }
      }
    }
    this.taskIds = [...new Set(this.taskIds)];
    this.checkAllParents(node);
  }

  reorderEvaluator(event: CdkDragDrop<any[]>) {
    moveItemInArray(this.evalData, event.previousIndex, event.currentIndex);
    this.evalData.forEach((evalu, i) => {
      evalu.number = i + 1;
    });
    this.dataSourceEval.data = this.evalData;
  }

  selectAllEvaluators(event: any) {
    this.dataSourceEval.data.forEach((data) => {
      if (event.checked) {
        this.evalSelection.select(data.evaluatorId);
      }
      else {
        this.evalSelection.deselect(data.evaluatorId);
      }
    })
  }

  changeReleaseSelection() {
    this.releaseSelection.select(1);
    this.whenToReleaseSelection.clear();
  }

  checkEvaluators(stepNext: boolean = true) {

    if ((this.releaseSelection.selected[0] !== 2 || this.evalSelection.selected.length >= this.reqSignOffs)) {
      stepNext ? this.stepper.next() : '';
    }
    else {
      this.alert.errorAlert("Select Required Number of Evaluators", `The total number of required sign offs is set at ${this.reqSignOffs}, the total number of Evaluators is less than this value. Assign additional Evaluators`);
    }
  }

  buildReviewByOptions() {
    var reviewOptions: ReviewByOptions[] = [];
    var selectedTasks = this.taskSelection.selected;
    var selectedSkills = this.EOCheckListSelection.selected;
    selectedTasks.forEach((task) => {
      var reviewOption = new ReviewByOptions();
      reviewOption.empIds = this.empSelection.selected;
      reviewOption.empName = '';
      var selectedEmployees = Object.assign(this.employees.filter((emp: Employee) => {
        return this.empSelection.selected.includes(emp.id);
      }))
      selectedEmployees.forEach((emp: Employee) => {
        reviewOption.empName += emp.person.firstName + ' ' + emp.person.lastName + ', ';
      });

      if (reviewOption.empName.length > 0) {
        reviewOption.empName = reviewOption.empName.substring(0, reviewOption.empName.length - 2);
      }
      else {
        reviewOption.empName = "N/A";
      }


      // reviewOption.evaluatorIds = this.evalSelection.selected;
      var selectedEvals = this.dataSourceEval.data.filter((evalu) => {
        return this.evalSelection.selected.includes(evalu.evaluatorId);
      });
      reviewOption.evaluatorName = "";
      selectedEvals.forEach((evalu) => {
        reviewOption.evaluatorName += evalu.evaluatorFName + ' ' + evalu.evaluatorLName + ', ';
        var number = 0;
        if (this.releaseSelection.selected[0] === 2 && this.whenToReleaseSelection.selected[0] === 2) {
          number = evalu.number;
        }
        reviewOption.evaluatorOptions.push({ evaluatorId: evalu.evaluatorId, evaluatorNumber: number });
      });

      if (reviewOption.evaluatorName.length > 0) {
        reviewOption.evaluatorName = reviewOption.evaluatorName.substring(0, reviewOption.evaluatorName.length - 2);
      }
      else {
        reviewOption.evaluatorName = "N/A";
      }

      reviewOption.taskId = task.id;
      reviewOption.taskDescription = task.description;
      reviewOptions.push(reviewOption);
    });
    selectedSkills.forEach((skill) => {
      var reviewOption = new ReviewByOptions();
      reviewOption.empIds = this.empSelection.selected;
      reviewOption.empName = '';
      var selectedEmployees = Object.assign(this.employees.filter((emp: Employee) => {
        return this.empSelection.selected.includes(emp.id);
      }))
      selectedEmployees.forEach((emp: Employee) => {
        reviewOption.empName += emp.person.firstName + ' ' + emp.person.lastName + ', ';
      });

      if (reviewOption.empName.length > 0) {
        reviewOption.empName = reviewOption.empName.substring(0, reviewOption.empName.length - 2);
      }
      else {
        reviewOption.empName = "N/A";
      }

      var selectedEvals = this.dataSourceEval.data.filter((evalu) => {
        return this.evalSelection.selected.includes(evalu.evaluatorId);
      });
      reviewOption.evaluatorName = "";
      selectedEvals.forEach((evalu) => {
        reviewOption.evaluatorName += evalu.evaluatorFName + ' ' + evalu.evaluatorLName + ', ';
        var number = 0;
        if (this.releaseSelection.selected[0] === 2 && this.whenToReleaseSelection.selected[0] === 2) {
          number = evalu.number;
        }
        reviewOption.evaluatorOptions.push({ evaluatorId: evalu.evaluatorId, evaluatorNumber: number });
      });

      if (reviewOption.evaluatorName.length > 0) {
        reviewOption.evaluatorName = reviewOption.evaluatorName.substring(0, reviewOption.evaluatorName.length - 2);
      }
      else {
        reviewOption.evaluatorName = "N/A";
      }
      reviewOption.enablingObjectiveId = skill.id; 
      reviewOption.enablingObjectiveDescription = skill.description; 
      reviewOptions.push(reviewOption);
    });
    this.dataSourceReview.data = reviewOptions;
  }

  async releaseTQ() {
    this.saveSpinner = true;
    var releaseOptions: TQReleaseByTaskAndSkillOptions[] = [];
    let startDateTime = `${this.reviewForm.get('release')?.value}T${this.reviewForm.get('startTime')?.value}`;
    this.dataSourceReview.data.forEach((data: ReviewByOptions) => {
      var releaseOption = new TQReleaseByTaskAndSkillOptions();
      releaseOption.dueDate = new Date(this.reviewForm.get('due')?.value);
      releaseOption.releaseDate = new Date(startDateTime);
      releaseOption.empIds = data.empIds;
      releaseOption.evaluatorOptions = data.evaluatorOptions;
      releaseOption.oneReq = this.releaseSelection.selected[0] === 1;
      releaseOption.dueDate = new Date(releaseOption.dueDate.toUTCString());
      releaseOption.releaseDate = new Date(releaseOption.releaseDate.toUTCString());
      if (releaseOption.oneReq) {
        releaseOption.multiReq = 0;
        releaseOption.orderMatters = false;
      }
      else {
        releaseOption.multiReq = this.reqSignOffs;
        releaseOption.orderMatters = this.whenToReleaseSelection.selected[0] === 2;
      }

      releaseOption.taskId = data.taskId;
      releaseOption.enablingObjectiveId = data.enablingObjectiveId;
      releaseOption.showQuestions = this.reviewForm.get('questions')?.value;
      releaseOption.showSuggestions = this.reviewForm.get('specific')?.value;
      releaseOptions.push(releaseOption);
    })
    releaseOptions.forEach(async (releaseOption: TQReleaseByTaskAndSkillOptions, i) => {
      this.saveSpinner = true;
      await this.tqService.createAndRelease(releaseOption).finally(() => {
        this.saveSpinner = false;
      });
      if (i === releaseOptions.length - 1) {
        this.alert.successToast("Task & Skill Qualifications Successfully Released");
        this.refresh.emit();
        this.dataBroadcastService.refreshTQStats.next(null);
      }
    })
  }

  filterEMP() {
    if (this.selectedPosEMP !== '') {
      this.empSelection.clear();
      this.posIds = [];
      this.positionSelection.clear();
      this.employees = this.originalEmployees.filter((emp: Employee) => {
        return emp?.employeePositions?.map((data) => data.positionId)?.includes(this.selectedPosEMP)
          && (emp?.person?.firstName?.trim()?.toLowerCase()?.includes(this.filterEMPString.trim().toLowerCase())
            || emp?.person?.lastName?.trim()?.toLowerCase()?.includes(this.filterEMPString.trim().toLowerCase())
            || emp?.person?.username?.trim()?.toLowerCase()?.includes(this.filterEMPString.trim().toLowerCase())
            || ((emp?.person?.firstName?.trim()?.toLowerCase() ?? '') + " " + (emp?.person?.lastName?.trim()?.toLowerCase() ?? '')).includes(this.filterEMPString.trim().toLowerCase()));
      })
    }
    else if (this.selectedOrgEMP !== '') {
      this.empSelection.clear();
      this.posIds = [];
      this.positionSelection.clear();
      this.employees = this.originalEmployees.filter((emp: Employee) => {
        return emp?.employeeOrganizations?.map((data) => data.organizationId)?.includes(this.selectedOrgEMP)
          && (emp?.person?.firstName?.trim()?.toLowerCase()?.includes(this.filterEMPString.trim().toLowerCase())
            || emp?.person?.lastName?.trim()?.toLowerCase()?.includes(this.filterEMPString.trim().toLowerCase())
            || emp?.person?.username?.trim()?.toLowerCase()?.includes(this.filterEMPString.trim().toLowerCase())
            || ((emp?.person?.firstName?.trim()?.toLowerCase() ?? '') + " " + (emp?.person?.lastName?.trim()?.toLowerCase() ?? '')).includes(this.filterEMPString.trim().toLowerCase()));
      })
    }
    else {
      this.employees = this.originalEmployees.filter((emp: Employee) => {
        return (emp?.person?.firstName?.trim()?.toLowerCase()?.includes(this.filterEMPString.trim().toLowerCase())
          || emp?.person?.lastName?.trim()?.toLowerCase()?.includes(this.filterEMPString.trim().toLowerCase())
          || emp?.person?.username?.trim()?.toLowerCase()?.includes(this.filterEMPString.trim().toLowerCase())
          || ((emp?.person?.firstName?.trim()?.toLowerCase() ?? '') + " " + (emp?.person?.lastName?.trim()?.toLowerCase() ?? '')).includes(this.filterEMPString.trim().toLowerCase()))
      })
    }
  }
onEOChange(event: any, node: any) {
    if (event.checked) {
      this.EOCheckListSelection.select(node);
    }
    else {
      this.EOCheckListSelection.deselect(node);
    }
    this.itemToggleEO(event.checked, node);
  }

  async readyEnablingObjectiveTreeData() {
    this.showLoader = true;
    var option = new EMPFilterOptions();
    if (this.selectedPos != null) {
      option.positionIds.push(this.selectedPos);
      option.type = "pos";
    }
    else{
      option.type = "All";
    }
    var skillData = await this.tqService.getEOTreeDataForPosition(option);
    this.readyEOTreeData(skillData);
    this.showLoader = false;
  }

  readyEOTreeData(res: any[]) {
    if (!res || res.length === 0) {
      this.enablingObjectiveDataSource.data = [];
      return;
    }

    let treeData: EOTree[] = [];

    res.forEach((eo) => {
      const cat = eo.enablingObjective_Category;
      const subCat = eo.enablingObjective_SubCategory;
      const topic = eo.enablingObjective_Topic;

      let catNode = treeData.find((c) => c.id === cat.id);
      if (!catNode) {
        catNode = {
          children: [],
          description: `${cat.number}. ${cat.title}`,
          id: cat.id,
          IsEO: false,
          position_SQs: eo.position_SQs,
        };
        treeData.push(catNode);
      }

      let subNode = catNode.children?.find((s) => s.id === subCat.id);
      if (!subNode) {
        subNode = {
          children: [],
          description: `${cat.number}.${subCat.number} ${subCat.title}`,
          id: subCat.id,
          IsEO: false,
          position_SQs: eo.position_SQs
        };
        catNode.children?.push(subNode);
      }

      let parentNode = subNode;
      if (topic) {
        let topicNode = subNode.children?.find((t) => t.id === topic.id);
        if (!topicNode) {
          topicNode = {
            children: [],
            description: `${cat.number}.${subCat.number}.${topic.number} ${topic.title}`,
            id: topic.id,
            IsEO: false,
            position_SQs: eo.position_SQs
          };
          subNode.children?.push(topicNode);
        }
        parentNode = topicNode;
      }

      parentNode.children?.push({
        children: [],
        description: `${eo.fullNumber} ${eo.description}`, 
        id: eo.id,
        active: eo.active,
        checkbox: !this.linkedEOIds.includes(eo.id),
        IsEO: true,
        position_SQs: eo.position_SQs.filter((sq: any) => sq.eoId === eo.id)
      });
    });

    this.EOTreeControl.dataNodes = [...treeData];
    this.enablingObjectiveDataSource.data = [...treeData];
    this.originalEOSource.data = [...treeData];

    Object.keys(this.enablingObjectiveDataSource.data).forEach((key: any) => {
      this.setParent(this.enablingObjectiveDataSource.data[key], undefined);
      this.setParent(this.originalEOSource.data[key], undefined);
    });

    this.filterDataNew();
  }

  filterDataNew() {
    let temparr = [
      ...this.originalEOSource.data.map((element) => {
        return {
          ...element,
          children: element.children?.map((e) => {
            return {
              ...e,
              children: e.children?.map((c) => {
                if (c.IsEO) {
                  if (c.description.toLowerCase().match(String(this.filterEOString).toLowerCase()) && c.active === true) {
                    return {
                      ...c,
                      selected: this.EOCheckListSelection.selected.map((x) => { return x.id }).includes(c.id),
                      children: [],
                    }
                  }
                  else {
                    return {
                      description: "",
                      children: [],
                      id: "",
                      IsEO: true,
                    }
                  }
                }
                else {
                  return {
                    ...c,
                    selected: this.EOCheckListSelection.selected.map((x) => { return x.id }).includes(c.id),
                    children: c.children?.filter((f) => {
                      return f.description.toLowerCase().match(String(this.filterEOString).toLowerCase()) && f.active === true;
                    })
                  }
                }

              })
            }
          }
          ),
        };
      }),
    ];

    temparr = [
      ...temparr.map((element) => {
        return {
          ...element,
          children: element.children.map((x) => {
            return {
              ...x,
              children: x.children.filter((x) => {
                return x.description !== "";
              })
            }
          })
        }
      })
    ]
    this.enablingObjectiveDataSource.data = Object.assign(temparr);
    this.EOTreeControl.dataNodes = Object.assign(temparr);
    Object.keys(this.enablingObjectiveDataSource.data).forEach((key: any) => {
      this.setParent(this.enablingObjectiveDataSource.data[key], undefined);
    });

    this.enablingObjectiveDataSource.data.forEach((cat) => {
      cat.children?.forEach((subCat) => {
        subCat.children?.forEach((topic) => {
          if (topic.IsEO) {
            this.EOCheckListSelection.selected.map((x) => x.id ).includes(topic.id) ? topic.selected = true : "";
            this.checkAllParents(topic);
          }
          topic.children?.forEach((eo) => {
            this.checkAllParents(eo);
          });
        });
      });
    });
    
    this.filterEOString.length > 0 ? this.EOTreeControl.expandAll() : this.EOTreeControl.collapseAll();
  }
  itemToggleEO(checked: boolean, node: any) {
    node.selected = node.checkbox ? checked : false;
    if (node.children !== undefined && node.children?.length>0) {
      node.children.forEach((child) => {
        this.itemToggleEO(checked, child);
      });
    } else {
        if (node.checkbox) {
        if (checked) {
          node.selected=true;
          this.EOCheckListSelection.select(node);
          node.position_SQs.forEach((data: EO_LinkPositions) => {
            this.posIds.push(data.positionId);
          })
        }
        else {
          node.selected=false;
          this.EOCheckListSelection.deselect(node);
          node.position_SQs.forEach((data: EO_LinkPositions) => {
            var index2 = this.posIds.indexOf(node.id);
            if (index2 > -1) {
              this.posIds.splice(index2, 1);
            }
          })
        }
      }
    }
    this.linkedEOIds = this.EOCheckListSelection.selected.map(x => x.id);
    this.checkAllParents(node);
  }
  get hasAnySelection(): boolean {
    return this.taskIds.length > 0 || this.linkedEOIds.length > 0;
  }
}

export class TaskTree {
  id: any;
  description: string;
  children?: TaskTree[];
  checkbox?: boolean;
  active?: boolean;
  selected?: boolean;
  checked?: boolean;
  indeterminate?: boolean;
  parent?: TaskTree;
  letter?: string;
}
