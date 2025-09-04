import { EmployeesService } from './../../../../_Services/QTD/employees.service';
import { SelectionModel } from '@angular/cdk/collections';
import { BreakpointObserver } from '@angular/cdk/layout';
import { StepperOrientation } from '@angular/cdk/stepper';
import { NestedTreeControl } from '@angular/cdk/tree';
import { DatePipe, TitleCasePipe } from '@angular/common';
import {
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
  ViewChild,
  SimpleChanges,
} from '@angular/core';
import {
  UntypedFormGroup,
  UntypedFormBuilder,
  UntypedFormControl,
  Validators,
} from '@angular/forms';
import { MatSort } from '@angular/material/sort';
import { MatStepper } from '@angular/material/stepper';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { ActivatedRoute, Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { InstructorCategoryCompactOptions } from 'src/app/_DtoModels/Instructor_Category/InstructorCategoryCompactOptions';
import { Instructor_HistoryCreateOptions } from 'src/app/_DtoModels/Instructor_History/Instructor_HistoryCreateOptions';
import { PositionOption } from 'src/app/_DtoModels/Position/PositionOption';
import { PositionOptions } from 'src/app/_DtoModels/Position/PositionOptions';
import { ProcedureOptions } from 'src/app/_DtoModels/Procedure/ProcedureOptions';
import { Procedure_StatusHistoryCreateOptions } from 'src/app/_DtoModels/Procedure_StatusHistory/Procedure_StatusHistoryCreateOptions';
import { RegulatoryRequirementOptions } from 'src/app/_DtoModels/RegulatoryRequirements/RegulatoryRequirementOptions';
import { RegulatoryRequirementsCompact } from 'src/app/_DtoModels/RegulatoryRequirements/RegulatoryRequirementsCompact';
import { RR_IssuingAuthorityCompact } from 'src/app/_DtoModels/RR_IssuingAuthority/RR_IssuingAuthorityCompact';
import { SaftyHazardCompactOption } from 'src/app/_DtoModels/SaftyHazard/SaftyHazardCompactOptions';
import { SaftyHazardOptions } from 'src/app/_DtoModels/SaftyHazard/SaftyHazardOptions';
import { SaftyHazard_CategoryCompactOptions } from 'src/app/_DtoModels/SaftyHazard_Category/SaftyHazard_CategoryCompactOptions';
import { TaskOptions } from 'src/app/_DtoModels/Task/TaskOptions';
import { DutyAreaService } from 'src/app/_Services/QTD/duty-area.service';
import { InstructorService } from 'src/app/_Services/QTD/instructor.service';
import { IssuingAuthoritiesService } from 'src/app/_Services/QTD/issuing-authorities.service';
import { PositionsService } from 'src/app/_Services/QTD/positions.service';
import { ProceduresService } from 'src/app/_Services/QTD/procedures.service';
import { RegulatoryRequirementService } from 'src/app/_Services/QTD/regulatory-requirement.service';
import { RRIssuingAuthorityService } from 'src/app/_Services/QTD/rr-issuing-authority.service';
import { SafetyHazardCategoryService } from 'src/app/_Services/QTD/safety-hazard-category.service';
import { SafetyHazardsService } from 'src/app/_Services/QTD/safety-hazards.service';
import { TasksService } from 'src/app/_Services/QTD/tasks.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { LocationService } from 'src/app/_Services/QTD/location.service';
import { LocationCategoryCompactOptions } from 'src/app/_DtoModels/Location_Category/LocationCategoryCompactOptions';
import { Location_HistoryCreateOptions } from 'src/app/_DtoModels/Location_History/Location_HistoryCreateOptions';

import { CertificationService } from 'src/app/_Services/QTD/certification.service';
import { CertifyingBodyCompactOptions } from 'src/app/_DtoModels/CertifyingBody/CertifyingBodyCompactOptions';
import { Certification_HistoryCreateOptions } from 'src/app/_DtoModels/Certification_History/Certification_HistoryCreateOptions';


import {
  sideBarClose,
  sideBarDisableClose,
  sideBarOpen,
  sideBarToggle,
} from 'src/app/_Statemanagement/action/state.menutoggle';

import { threadId } from 'worker_threads';
import { EnablingObjectivesService } from 'src/app/_Services/QTD/enabling-objectives.service';
import { EnablingObjective } from 'src/app/_DtoModels/EnablingObjective/EnablingObjective';
import { EnablingObjectiveOptions } from 'src/app/_DtoModels/EnablingObjective/EnablingObjectiveOption';
import { EmployeeOptions } from 'src/app/_DtoModels/Employee/EmployeeOptions';
import { ProviderService } from 'src/app/_Services/QTD/provider.service';
import { IlaService } from 'src/app/_Services/QTD/ila.service';
import { ILABulkOptions } from 'src/app/_DtoModels/ILA/ILABulkOptions';
import { EOCatTreeVM, EOTreeVM } from 'src/app/_DtoModels/TreeVMs/EOTreeVM';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { DynamicLabelReplacementPipe } from 'src/app/_Pipes/dynamic-label-replacement.pipe';

@Component({
  selector: 'app-bulk-edit',
  templateUrl: './bulk-edit.component.html',
  styleUrls: ['./bulk-edit.component.scss'],
  providers: [TitleCasePipe]
})
export class BulkEditComponent implements OnInit {
  moduleName: string;

  @Output() closed = new EventEmitter<any>();
  showSpinner: boolean = false;
  stepperOrientation: Observable<StepperOrientation>;
  stepHistoryForm: UntypedFormGroup;
  linkedIds: any[] = [];
  optionsList: any[] = [];
  DataSource: MatTableDataSource<any>;
  showActive: boolean = true;
  filterString: string = "";
  actionId: any;
  selection = new SelectionModel<TreeData>(true);
  dataSource = new MatTreeNestedDataSource<TreeData>();
  originalDataSource = new MatTreeNestedDataSource<TreeData>();
  treeControl = new NestedTreeControl<TreeData>((node: any) => node.children);
  displayColumns = ['number', 'description'];
  hasChild = (_: number, node: TreeData) =>
    !!node.children && node.children.length > 0;
  filterProcString: any;
  datePipe = new DatePipe('en-us');
  disabledContinue: boolean = false;

  reviewArray: any[] = [];
  // @ViewChild(MatSort) set tblSort(sort: MatSort) {
  //   if (sort) this.DataSource.sort = sort;
  // }

  @ViewChild('stepper') stepper: MatStepper;
  constructor(
    private fb: UntypedFormBuilder,
    public titleCasePipe: TitleCasePipe,
    private proc_issuSrvc: IssuingAuthoritiesService,
    public breakpointObserver: BreakpointObserver,
    private router: Router,
    private store: Store<{ toggle: string }>,
    private activatedRoute: ActivatedRoute,
    private procedureService: ProceduresService,
    private alert: SweetAlertService,
    private rrIAService: RRIssuingAuthorityService,
    private rrService: RegulatoryRequirementService,
    private shService: SafetyHazardsService,
    private insService: InstructorService,
    private dutyAreaService: DutyAreaService,
    private taskSrvc: TasksService,
    private locService: LocationService,
    private positionService: PositionsService,
    private eoService: EnablingObjectivesService,
    private employeeService: EmployeesService,
    private providerSrvc: ProviderService,
    private ilaSrvc: IlaService,
    private certService: CertificationService,
    private labelPipe: LabelReplacementPipe,
    private dynamicLabelReplacementPipe: DynamicLabelReplacementPipe
  ) {
    this.stepperOrientation = breakpointObserver
      .observe('(min-width: 800px)')
      .pipe(map(({ matches }) => (matches ? 'horizontal' : 'vertical')));
    this.activatedRoute.params.subscribe((res) => {
      this.moduleName = res?.name.replace('-', ' ');
    });
  }

  async ngOnInit(): Promise<void> {
    this.readyStepHistoryForm();

    this.toggleMainMenu();

    this.getTreeList();
    const transformedName = this.titleCasePipe.transform(this.moduleName);
    this.moduleName = this.moduleName.toLowerCase() !== "ilas" ? transformedName : this.moduleName;
    var module = await this.dynamicLabelReplacementPipe.transform(this.moduleName);
    this.optionsList = [
      { id: 1, description: 'Change Active/Inactive Status' },
      { id: 2, description: 'Delete ' + module },
    ];
  }

  toggleMainMenu() {
    //this.databroadcastSrvc.ToggleMainMenu.next('');
    this.store.dispatch(sideBarClose());
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }

  getTreeList() {
    this.selection.clear();
    this.linkedIds = [];
    this.reviewArray = [];
    switch (this.moduleName.toLowerCase()) {
      case 'procedure':
        this.getProcedures();
        break;
      case 'regulation':
        this.getRegulations();
        break;
      case 'safety hazard':
        this.getSafetyHazards();
        break;
      case 'instructor':
        this.getInstructors();
        break;
      case 'location':
        this.getLocations();
        break;
      case 'task':
        this.getTask();
        break;
      case 'position':
        this.getPositions();
        break;
      case 'enabling objective':
        this.getEOs();
        break;
      case 'employees':
        this.getEmployees();
        break;
      case 'certification':
        this.getCertifications();
        break;
      case 'ilas':
        this.getILAwithProviders();
        break;
    }
  }
  async getILAwithProviders() {
    this.showSpinner = true;
    this.dataSource.data = [];
    this.originalDataSource.data = [];
    // var index = 1;
    // var childIndex = 1;

    await this.shService.getProviderWithILAs().then(async (res: any[]) => {
      var treeData: TreeData[] = [];
      res.forEach((prov, i) => {
        treeData.push({
          id: prov.id,
          description: prov.name,
          active: prov.active,
          children: [],
          checkbox: true,
          selected: false,
          shouldSelect: null,
        });
        prov.ilAs.forEach((ila) => {
          treeData[i].children.push({
            id: ila.id,
            description: ila.number + " - " + ila.name,
            number: ila.number,
            active: ila.active,
            checkbox: true,
            selected: false,
            shouldSelect: 'ilas',
          });
        })
      });


      this.dataSource.data = Object.assign(treeData);
      this.originalDataSource.data = Object.assign(this.dataSource.data);
      this.alterDataSource();
      Object.keys(this.dataSource.data).forEach((key: any) => {
        this.setParent(this.dataSource.data[key], undefined);
        this.setParent(this.originalDataSource.data[key], undefined);
      });
      this.treeControl.dataNodes = Object.assign(this.dataSource.data);
    })
      .finally(() => {
        this.showSpinner = false;
      });
  }

  async getEmployees() {
    this.showSpinner = true;
    this.dataSource.data = [];
    this.originalDataSource.data = [];
    await this.employeeService.getAllSimplifiedEmployees().then((res) => {
      let treedata: TreeData[] = [];
      let tempArray = res;
      for (let i = 0; i < tempArray.length; i++) {
        let tree = new TreeData();
        tree.id = res[i].id;
        tree.description = res[i].firstName + ' ' + res[i].lastName;
        tree.checkbox = true;
        tree.selected = false;
        tree.children = [];
        tree.active = res[i].active;
        tree.number = i;
        tree.shouldSelect = "Other";
        treedata.push(tree);
      }
      this.dataSource.data = treedata;
      this.DataSource = new MatTableDataSource(treedata);
      this.originalDataSource.data = this.dataSource.data;
      this.alterDataSource();
      Object.keys(this.dataSource.data).forEach((key: any) => {
        this.setParent(this.dataSource.data[key], undefined);
        this.setParent(this.originalDataSource.data[key], undefined);
      });
    }).finally(() => {
      this.showSpinner = false;
    });
  }

  async getPositions() {
    this.showSpinner = true;
    this.dataSource.data = [];
    this.originalDataSource.data = [];
    await this.positionService.getAllWithoutIncludes().then((res) => {
      let treedata: TreeData[] = [];
      let tempArray = res;
      for (let i = 0; i < tempArray.length; i++) {
        let tree = new TreeData();
        tree.id = res[i].id;
        tree.description = res[i].positionTitle;
        tree.checkbox = true;
        tree.selected = false;
        tree.children = [];
        tree.active = res[i].active;
        tree.shouldSelect = "Other",
          treedata.push(tree);
      }
      this.dataSource.data = treedata;
      Object.assign(this.originalDataSource.data, this.dataSource.data);
    })
      .finally(() => {
        this.showSpinner = false;
      });
  }

  async getSafetyHazards() {
    this.showSpinner = true;
    /*   await this.shService
        .getSHCategoryWithSH()
        .then((res: SaftyHazard_CategoryCompactOptions[]) => {
          let treedata: TreeData[] = [];
          for (let i = 0; i < res.length; i++) {
            if (res[i].saftyHazard_Category.active) {
              let tree = new TreeData();
              tree.id = res[i].saftyHazard_Category.id;
              tree.description = res[i].saftyHazard_Category.title;
              tree.checkbox = true;
              tree.selected = false;
              tree.children = [];
              tree.shouldSelect = null;
              res[i].saftyHazardCompactOptions.forEach((d) => {
                if (tree.children) {
                  tree.children.push({
                    id: d.id,
                    description: d.title,
                    selected: false,
                    checkbox: true,
                    active: d.active,
                    number: d.number,
                    shouldSelect:"Other",
                  });
                }
              });

              treedata.push(tree);
            }
          }

          this.dataSource.data = [];
          this.dataSource.data = treedata;
          this.originalDataSource.data = this.dataSource.data;
          this.alterDataSource();
          Object.keys(this.dataSource.data).forEach((key: any) => {
            this.setParent(this.dataSource.data[key], undefined);
            this.setParent(this.originalDataSource.data[key], undefined);
          });
        })
        .finally(() => {
          this.showSpinner = false;
        }); */


    this.shService.getSHCategoryWithSHList().then((category) => {
      let treedata: TreeData[] = [];
      category.forEach((sh, index) => {
        if (sh.active) {
          treedata.push({
            id: sh.id,
            description: sh.title,
            checkbox: true,
            selected: false,
            children: [],
            shouldSelect: null
          })
          sh.saftyHazards.forEach((res, index1) => {
            if (res.procedure_SaftyHazard_Links.length === 0 && res.safetyHazard_ILA_Links.length === 0 && res.saftyHazard_RR_Links.length === 0
              && res.safetyHazard_EO_Links.length === 0) {
              treedata[index].children.push({
                id: res.id,
                description: res.number + " - " + res.title,
                selected: false,
                checkbox: true,
                active: res.active,
                number: res.number,
                shouldSelect: "Other"
              })

              //  let tree = new TreeData();
              /* tree.id =sh.id;
              tree.description = sh.title;
              tree.checkbox = true;
              tree.selected = false;
              tree[index].children = [];
              tree.shouldSelect = null;

                if (tree.children) {
                  tree[index].children.push({
                    id: res.id,
                    description: res.title,
                    selected: false,
                    checkbox: true,
                    active: res.active,
                    number: res.number,
                    shouldSelect:"Other",
                  });
                } */
            }
          })
        }
      })
      this.dataSource.data = [];
      this.dataSource.data = treedata;
      this.toFilter = treedata;
      this.originalDataSource.data = this.dataSource.data;
      this.alterDataSource();
      Object.keys(this.dataSource.data).forEach((key: any) => {
        this.setParent(this.dataSource.data[key], undefined);
        this.setParent(this.originalDataSource.data[key], undefined);
      });
    }).finally(() => {
      this.showSpinner = false;
    });
  }

  async getRegulations() {
    this.showSpinner = true;
    this.dataSource.data = [];
    this.originalDataSource.data = [];
    await this.rrIAService
      .GetRRWithIA()
      .then((res: RR_IssuingAuthorityCompact[]) => {
        let treedata: TreeData[] = [];
        let tempArray = [
          ...res.map((ia) => {
            return {
              ...ia,
              regulatoryRequirementCompacts: ia.regulatoryRequirementCompacts,
            };
          }),
        ];
        tempArray.forEach((data: RR_IssuingAuthorityCompact, index: any) => {
          treedata.push({
            id: data.id,
            description: data.title,
            checkbox: true,
            selected: false,
            children: [],
            shouldSelect: null,
          });
          data.regulatoryRequirementCompacts.forEach(
            (rr: RegulatoryRequirementsCompact) => {
              treedata[index].children?.push({
                description:rr.number + " - " + rr.title,
                id: rr.id,
                checkbox: true,
                selected: false,
                active: rr.active,
                number: rr.number,
                shouldSelect: "Other",
              });
            }
          );
        });
        this.dataSource.data = treedata;
        this.toFilter = treedata;
        Object.assign(this.originalDataSource.data, this.dataSource.data);
        this.alterDataSource();
        Object.keys(this.dataSource.data).forEach((key: any) => {
          this.setParent(this.dataSource.data[key], undefined);
          this.setParent(this.originalDataSource.data[key], undefined);
        });
      })
      .finally(() => {
        this.showSpinner = false;
      });
  }

  async getProcedures() {
    this.showSpinner = true;
    this.dataSource.data = [];
    this.originalDataSource.data = [];
    await this.proc_issuSrvc
      .getAll()
      .then((res) => {
        let treedata: TreeData[] = [];
        let tempArray = res.map((ia) => {
          return {
            ...ia,
            procedures: ia.procedures,
          };
        });
        for (let i = 0; i < tempArray.length; i++) {
          if (tempArray[i].procedures.length > 0) {
            let tree = new TreeData();
            tree.id = res[i].id;
            tree.description = res[i].title;
            tree.checkbox = true;
            tree.selected = false;
            tree.children = [];
            tree.shouldSelect = null;
            tempArray[i].procedures.forEach((d) => {
              if (tree.children) {
                tree.children.push({
                  id: d.id,
                  description:d.number + " - " + d.title,
                  selected: false,
                  checkbox: true,
                  active: d.active,
                  number: d.number,
                  shouldSelect: "Other",
                });
              }
            });
            treedata.push(tree);
          }
        }
        this.dataSource.data = treedata;
        this.toFilter = treedata;
        Object.assign(this.originalDataSource.data, this.dataSource.data);
        this.alterDataSource();
        Object.keys(this.dataSource.data).forEach((key: any) => {
          this.setParent(this.dataSource.data[key], undefined);
          this.setParent(this.originalDataSource.data[key], undefined);
        });
      })
      .finally(() => {
        this.showSpinner = false;
      });
  }

  async getTask() {
    this.showSpinner = true;
    this.dataSource.data = [];
    this.originalDataSource.data = [];
    // var index = 1;
    // var childIndex = 1;
    await this.dutyAreaService
      .getMinimizedDataForTree()
      .then((dutyArea) => {
        // var index = 1;
        // var childIndex = 1;
        var treeData: TreeData[] = [];

        dutyArea.forEach((da, i) => {
          treeData.push({
            id: da.id,
            description: da.letter + da.number + " - " + da.title,
            children: [],
            checkbox: true,
            selected: false,
            shouldSelect: null,
            active: da.active,
          })
          da.subdutyAreas.forEach((sda, j) => {
            treeData[i].children.push({
              id: sda.id,
              description: da.letter + da.number + "." + sda.subNumber + " - " + sda.title,
              children: [],
              checkbox: true,
              selected: false,
              shouldSelect: null,
              active: sda.active
            });
            sda.tasks.forEach((task) => {
              if (task.isMeta) {
                treeData[i].children[j].children.push({
                  id: task.id,
                  description:da.letter + da.number + "." + sda.subNumber + "." + task.number + " - " + task.description,
                  checkbox: true,
                  selected: false,
                  shouldSelect: "Meta",
                  active: task.active,
                  number: da.letter + da.number + "." + sda.subNumber + "." + task.number,
                })
              }
              else {
                treeData[i].children[j].children.push({
                  id: task.id,
                  description:da.number + "." + sda.subNumber + "." + task.number + " - " + task.description,
                  checkbox: true,
                  selected: false,
                  shouldSelect: "Task",
                  active: task.active,
                  number: da.number + "." + sda.subNumber + "." + task.number,
                })
              }
            })
          })
        })

        // for (var data in dutyArea) {
        //
        //   treeData[data] = {
        //     id: dutyArea[data]['id'],
        //     description: index + ' - ' + dutyArea[data]['title'],
        //     children: dutyArea[data]['subdutyAreas'],
        //     checkbox: true,
        //     selected: false,
        //     shouldSelect:"Task"
        //   };
        //
        //   for (var data1 in treeData[data]['children']) {
        //
        //     treeData[data]['children'][data1] = {
        //       id: dutyArea[data]['subdutyAreas'][data1]['id'],
        //       description: index + '.' + childIndex + ' - ' + dutyArea[data]['subdutyAreas'][data1]['title'],
        //       children: dutyArea[data]['subdutyAreas'][data1]['tasks'],
        //       checkbox: true,
        //     };
        //     for (var data2 in treeData[data]['children'][data1]['children']) {
        //       treeData[data]['children'][data1]['children'][data2]['checkbox'] = true;
        //       treeData[data]['children'][data1]['children'][data2]['description'] = index + '.' + childIndex + '.' + treeData[data]['children'][data1]['children'][data2]['number'] + ' - ' + treeData[data]['children'][data1]['children'][data2]['description'];

        //     }
        //     childIndex++;
        //   }
        //   index++;
        //   childIndex = 1;
        // }


        this.dataSource.data = Object.assign(treeData);
        this.originalDataSource.data = Object.assign(this.dataSource.data);
        this.alterDataSource();
        Object.keys(this.dataSource.data).forEach((key: any) => {
          this.setParent(this.dataSource.data[key], undefined);
          this.setParent(this.originalDataSource.data[key], undefined);
        });
        this.treeControl.dataNodes = Object.assign(this.dataSource.data);

        this.filterData(this.dataSource.data, "");
      })
      .finally(() => {
        this.showSpinner = false;
      });
  }

  notTopicEOs = 0;

  filterSearch:boolean=false;
  async getEOs() {
    this.showSpinner = true;
    this.dataSource.data = [];
    this.originalDataSource.data = [];
    await this.eoService.getMinimizedForTree().then((res: EOCatTreeVM[]) => {
      if (res.length == 0) {
        this.dataSource.data = [];
      } else {
        var treeData: TreeData[] = [];
        res.forEach((cat, i) => {
          treeData.push({
            children: [],
            description: cat['number'] + ". " + cat['title'],
            id: cat.id,
            checkbox: true,
            selected: false,
            shouldSelect: null,
            active: cat.active,
          })
          cat['enablingObjective_SubCategories'].forEach((subCat, j) => {
            treeData[i].children?.push({
              children: [],
              description: `${cat['number']}.${subCat['number']} ` + subCat['title'],
              id: subCat.id,
              checkbox: true,
              selected: false,
              shouldSelect: null,
              active: subCat.active,
            });
            subCat.enablingObjectives.forEach((eo: EOTreeVM) => {
              if (eo.isMetaEO) {
                treeData[i].children[j].children?.push({
                  children: [],
                  description:`${cat['number']}.${subCat['number']}.0.${eo['number']} - ` + `${eo['description']}`,
                  id: eo.id,
                  active: eo.active,
                  checkbox: true,
                  shouldSelect: "Meta",
                  number: `${cat['number']}.${subCat['number']}.0.${eo['number']}`,
                })
                this.notTopicEOs++;
              }
              else if (eo.isSkillQualification) {
                treeData[i].children[j].children?.push({
                  children: [],
                  description:`${cat['number']}.${subCat['number']}.0.${eo['number']} - `+ `${eo['description']}`,
                  id: eo.id,
                  active: eo['active'],
                  checkbox: true,
                  shouldSelect: "SQ",
                  number: `${cat['number']}.${subCat['number']}.0.${eo['number']}`,
                })
                this.notTopicEOs++;
              }
              else {
                treeData[i].children[j].children?.push({
                  children: [],
                  description:`${cat['number']}.${subCat['number']}.0.${eo['number']} - ` + `${eo['description']}`,
                  id: eo.id,
                  active: eo['active'],
                  checkbox: true,
                  shouldSelect: "EO",
                  number: `${cat['number']}.${subCat['number']}.0.${eo['number']}`,
                })
                this.notTopicEOs++;
              }
            });
            subCat['enablingObjective_Topics'].forEach((topic, k) => {
              treeData[i]?.children[j]?.children?.push({
                children: [],
                description: `${cat['number']}.${subCat['number']}.${topic['number']} ${topic['title']}`,
                id: topic.id,
                checkbox: true,
                selected: false,
              });
              topic['enablingObjectives'].forEach((eo: EOTreeVM, l) => {
                if (eo.isMetaEO) {
                  treeData[i].children[j]?.children[k + this.notTopicEOs]?.children?.push({
                    children: [],
                    description:`${cat['number']}.${subCat['number']}.${topic['number']}.${eo['number']} - ` + `${eo['description']}`,
                    active: eo['active'],
                    id: eo['id'],
                    checkbox: true,
                    shouldSelect: "Meta",
                    number: `${cat['number']}.${subCat['number']}.${topic['number']}.${eo['number']} `,
                  })
                }
                else if (eo.isSkillQualification) {
                  treeData[i].children[j]?.children[k + this.notTopicEOs]?.children?.push({
                    children: [],
                    description:`${cat['number']}.${subCat['number']}.${topic['number']}.${eo['number']} - ` + `${eo['description']}`,
                    active: eo['active'],
                    id: eo['id'],
                    checkbox: true,
                    shouldSelect: "SQ",
                    number: `${cat['number']}.${subCat['number']}.${topic['number']}.${eo['number']} `,
                  })
                }
                else {
                  treeData[i].children[j]?.children[k + this.notTopicEOs]?.children?.push({
                    children: [],
                    description:`${cat['number']}.${subCat['number']}.${topic['number']}.${eo['number']} - ` + `${eo['description']}`,
                    active: eo['active'],
                    id: eo['id'],
                    checkbox: true,
                    shouldSelect: "EO",
                    number: `${cat['number']}.${subCat['number']}.${topic['number']}.${eo['number']} `,
                  })
                };
              });
            });
            this.notTopicEOs = 0;
          });
        })

        this.dataSource.data = Object.assign([], treeData);
        this.originalDataSource.data = Object.assign([], treeData);
        Object.keys(this.dataSource.data).forEach((key: any) => {
          this.setParent(this.dataSource.data[key], undefined);
          this.setParent(this.originalDataSource.data[key], undefined);
        });

        this.filterSearch = true;
        this.filterEOS(this.showActive);
      }
    }).finally(() => {
      this.showSpinner = false;
    });
  }

  alterDataSource() {
    switch (this.moduleName.toLowerCase()) {
      case 'procedure':
        this.alterCommonDataSource();
        break;
      case 'regulation':
        this.alterCommonDataSource();
        break;
      case 'safety hazard':
        this.alterCommonDataSource();
        break;
      case 'task':
        this.alterTaskDataSource();
        break;
      case 'position':
        this.alterPositionDataSource();
        break;
      case 'enabling objective':
        this.filterEOS(this.showActive);
        break;
      case 'employees':
        this.alterPositionDataSource();
        break;
      case 'ilas':
        this.alterIlasDataSource();
        break;

    }
  }
  alterIlasDataSource() {
    this.dataSource.data = [
      ...this.originalDataSource.data.map((n) => {
        return {
          ...n,
          children: n.children?.filter((x) => x.active == this.showActive),
        };
      }),
    ];
  }

  alterPositionDataSource() {
    this.dataSource.data = [
      ...this.originalDataSource.data.filter((c) => {
        return c.active === this.showActive
      })
    ]
  }

  alterCommonDataSource() {
    this.dataSource.data = [
      ...this.originalDataSource.data.map((n) => {
        return {
          ...n,
          children: n.children?.filter((c) => c.active === this.showActive),
        };
      }),
    ];
  }

  alterTaskDataSource() {
    this.dataSource.data = [
      ...this.originalDataSource.data.map((n) => {
        return {
          ...n,
          children: n.children?.map((ch) => {
            return {
              ...ch,
              children: ch.children?.filter((x) => x.active == this.showActive),
            };
          }),
        };
      }),
    ];
  }

  private setParent(node: TreeData, parent: TreeData | undefined) {
    node.parent = parent;
    if (node.children) {
      node.children.forEach((childNode) => {
        this.setParent(childNode, node);
      });
    }
  }
  selectedChanged(event: any) {
    if (event.selectedIndex === 1) {
    }
  }

  filterActive(active: boolean) {
    this.showActive = active;
    this.dataSource.data = [];
    this.getTreeList();
  }

  // filterProcIsActive(active: boolean) {
  //   this.showActive = !!active;
  //   this.getTreeList();
  // }

  // filterRRIsActive(active: boolean) {
  //   this.dataSource.data = [
  //     ...this.originalDataSource.data.map((n) => {
  //       return {
  //         ...n,
  //         children: n.children?.filter((c) =>
  //           c.active === active,
  //         ),
  //       };
  //     }),
  //   ];
  //   this.showActive = !!active;
  // }

  readyStepHistoryForm() {
    this.stepHistoryForm = this.fb.group({
      reason: new UntypedFormControl('', Validators.required),
      EffectiveDate: new UntypedFormControl(
        this.datePipe.transform(Date.now(), 'yyyy-MM-dd'),
        Validators.required
      ),
    });
  }

  onCheckChange(event: any, node: any) {
    if (event.checked && node.shouldSelect !== undefined && node.shouldSelect !== null) {
      this.selection.select(node);
      this.disabledContinue = true;
    } else {
      this.selection.deselect(node);
    }
    this.itemToggle(event.checked, node);
  }

  itemToggle(checked: boolean, node: TreeData) {
    if (node.shouldSelect !== undefined && node.shouldSelect !== null) {
      node.selected = checked;
    }
    if (node.children?.length! > 0) {
      node.children?.forEach((child) => {
        this.itemToggle(checked, child);
      });
      /* this.linkedIds.push(node.id);
      this.reviewArray.push(node.description); */
    }
    else {
      if (node.selected && node.shouldSelect !== undefined && node.shouldSelect !== null) {
        this.linkedIds.push(node.id);
        this.reviewArray.push({
          number: node.number,
          description: node.description
        });
      } else if (!node.selected && node.shouldSelect !== undefined && node.shouldSelect !== null) {
        const linkIdx = this.linkedIds.indexOf(node.id);
        const descIdx = this.reviewArray.findIndex(x => x.description === node.description);
        linkIdx > -1 ? this.linkedIds.splice(linkIdx, 1) : '';
        descIdx > -1 ? this.reviewArray.splice(descIdx, 1) : '';
      }
    }
    this.linkedIds = [...new Set(this.linkedIds)];

    this.reviewArray = [...new Set(this.reviewArray)];
    //this.reviewArray = [...new Set(this.reviewArray)];
    this.checkAllParents(node);
  }

  private checkAllParents(node: TreeData) {
    if (node.parent) {
      const descendants = this.treeControl.getDescendants(node.parent);
      node.parent.selected = descendants.every((child) => child.selected);
      node.parent.indeterminate = descendants.some((child) => child.selected);
      this.checkAllParents(node.parent);
    }
  }

  filtered(node: any) {
    return node.description.includes(this.filterProcString);
  }

  filterData(data: any, toFilter: any) {
    switch (this.moduleName.toLowerCase()) {
      case 'procedure':
        this.commonFilter();
        break;
      case 'regulation':
        this.commonFilter();
        break;
      case 'safety hazard':
        this.commonFilter();
        break;
      case 'instructor':
        this.commonFilter();
        break;
      case 'location':
        this.commonFilter();
        break;
      case 'task':
        this.filterTask();
        break;
      case 'position':
        this.commoPositionFilter();
        break;
      case 'enabling objective':
        this.filterEOS(this.showActive);
        break;
      case 'employees':
        this.commoPositionFilter();
        break;
      case 'ilas':
        this.filterIlas();
        break;
      case 'certification':
        this.commonFilter();
        break;
    }
  }

  filterIlas() {
    var tempArr = Object.assign([], [
      ...this.originalDataSource.data.map((element) => {
        return {
          ...element,
          children: element.children?.filter((e) => {
            return (e.description
              .toLowerCase()
              .includes(String(this.filterString).toLowerCase())) && (e.active === this.showActive)
          }),
        };
      }),
    ]);
    this.dataSource.data = tempArr;
    this.treeControl.dataNodes = this.dataSource.data;
    this.filterString.length > 0 ? this.treeControl.expandAll() : this.treeControl.collapseAll();
  }

  filterEOS(filterActive: boolean) {
    this.showActive = filterActive;
    let temparr = [
      ...this.originalDataSource.data.filter((x)=>{
        return x.active === this.showActive || x.children?.some((s)=>{
          return s.active === this.showActive || s.children?.some((y)=>{
            return y.active === this.showActive || y.children.some((z)=>{
              return z.active === this.showActive;
            });
          })
        })
      }).map((element) => {
        return {
          ...element,
          children: element.children?.filter((r)=>{
            return r.active === this.showActive || r.children?.some((s)=>{
              return s.active === this.showActive || s.children.some((y)=>{
                return y.active === this.showActive;
              });
            })
          })?.map((e) => {
            return {
              ...e,
              children: e.children?.filter((s)=>{
                return s.active === this.showActive || s.children.some((k)=>{
                  return k.active === this.showActive;
                });
              })?.map((c) => {
                if (c.shouldSelect) {
                  if (c.description.toLowerCase().match(String(this.filterString).toLowerCase()) && c.active === this.showActive) {
                    return {
                      ...c,
                      children: [],
                    }
                  }
                  else {
                    return {
                      id:"",
                      description: "",
                      children: [],
                    }
                  }
                }
                else {
                  return {
                    ...c,
                    children: c.children?.filter((f) => {
                      return f.description.toLowerCase().match(String(this.filterString).toLowerCase()) && f.active === this.showActive;
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

    if(this.filterString.length > 0){
      debugger;
      this.dataSource.data = temparr.filter((x) => {
        return (
          x.children !== null &&
          x.children !== undefined &&
          x.children.length > 0 &&
          x.children.some((y) => {
            return (
              y?.children !== null &&
              y?.children !== undefined &&
              y?.children.length > 0 &&
              y?.children.some((z) => {
                return z?.children !== null  &&
                z?.children !== undefined &&
                z?.children.length > 0 || z?.shouldSelect=== 'EO' || z?.shouldSelect === 'SQ'
              })
            );
          })
        );
      });
      this.treeControl.dataNodes = temparr;
    }else{
      this.dataSource.data = temparr;
    }
 
    this.filterString.length !== 0 ? this.treeControl.expandAll() : this.treeControl.collapseAll();

    // if (this.filter.length > 0) {
    //   this.treeControl.expandAll();
    //   this.hasSearchWord = true;
    // } else {
    //   this.treeControl.collapseAll();
    //   this.hasSearchWord = false;
    // }
  }

  commoPositionFilter() {
    if (this.filterString.length > 0) {
      this.dataSource.data = [
        ...this.originalDataSource.data.filter((c) => {
          return c.description
            .toLowerCase()
            .includes(String(this.filterString).toLowerCase())
        })
      ]
    } else {
      this.dataSource.data = this.originalDataSource.data;
    }
  }

  commonFilter() {
debugger;
    this.toFilter = [
      ...this.originalDataSource.data.filter((x) => {
        return x.active === this.showActive || x.children?.some((child) => {
          return child.active === this.showActive && child.description
            .toLowerCase()
            .includes(String(this.filterString).toLowerCase())
        })
      }).map((n) => {
        return {
          ...n,
          children: n.children?.filter((c) =>
            c.description
              .toLowerCase()
              .includes(String(this.filterString).toLowerCase())  && c.active === this.showActive,
              
          ),
        };
      }),
    ];

    this.dataSource.data = this.toFilter.filter((x) => {return x.children !== null && x.children !== undefined && x.children.length > 0});
    this.treeControl.dataNodes = this.dataSource.data;
    this.filterString.length > 0 ? this.treeControl.expandAll() : this.treeControl.collapseAll();
  }

  filterTask() {
    this.dataSource.data = [
      ...this.originalDataSource.data.filter((f) => {
        return f.active === this.showActive || f.children?.some((s) => {
          return s.active === this.showActive || s.children?.some((x) => {
            return x.active === this.showActive;
          });
        })
      })?.map((n) => {
        return {
          ...n,
          children: n.children?.filter((f) => {
            return f.active === this.showActive || f.children?.some((k) => {
              return k.active === this.showActive;
            })
          })?.map((s) => {
            return {
              ...s,
              children: s.children?.filter((c) => {
                return (
                  c.description.toLowerCase()
                    .trim()
                    .includes(String(this.filterString).toLowerCase().trim()) ||
                    c.number?.toString().toLowerCase().includes(String(this.filterString).toLowerCase())
                     && c.active === this.showActive
                );
              }),
            };
          }),
        };
      }),
    ];

    this.dataSource.data = this.dataSource.data.filter((x) => { return x.children !== null && x.children !== undefined && x.children.length > 0 && (x.children.some((y) => { return y?.children !== null && y?.children !== undefined && y?.children.length > 0 })) })
    this.treeControl.dataNodes = this.dataSource.data;
    this.filterString.length !== 0 ? this.treeControl.expandAll() : this.treeControl.collapseAll();

    // } else {
    //   this.dataSource.data = this.originalDataSource.data;
    // }
  }

  filterRR() {
    if (this.filterString.length > 0) {
      this.dataSource.data = [
        ...this.originalDataSource.data.map((n) => {
          return {
            ...n,
            children: n.children?.filter((c) =>
              c.description
                .toLowerCase()
                .includes(String(this.filterString).toLowerCase())
            ),
          };
        }),
      ];
    } else {
      this.dataSource.data = this.originalDataSource.data;
    }
  }

  continueClicked() {
    if (this.stepper.selectedIndex == 1) {
      this.DataSource = new MatTableDataSource(this.reviewArray);
    }
    this.stepper.next();
  }

  OnBulkEdit() {
    switch (this.moduleName.toLowerCase()) {
      case 'procedure':
        this.ProcedureBulkEdit();
        break;
      case 'regulation':
        this.RRBulkEdit();
        break;
      case 'safety hazard':
        this.SHBulkEdit();
        break;
      case 'instructor':
        this.InstructorBulkEdit();
        break;
      case 'location':
        this.LocationBulkEdit();
        break;
      case 'task':
        this.TaskBulkEdit();
        break;
      case 'enabling objective':
        this.EOsBulkEdit();
        break;
      case 'position':
        this.PositionBulkEdit();
        break;
      case 'employees':
        this.EmployeeBulkEdit();
        break;
      case 'certification':
        this.CertificationBulkEdit();
        break;
      case 'ilas':
        this.ILAsBulkEdit();
        break;
    }
  }

  async ILAsBulkEdit() {
    this.showSpinner = true;
    var options = new ILABulkOptions();
    if (this.actionId === 1) {
      options.actionType = this.showActive ? 'inactive' : 'active';
    }
    else {
      options.actionType = 'delete';
    }
    options.iLaIds = this.linkedIds;
    options.effectiveDate = this.stepHistoryForm.get('EffectiveDate')?.value;
    options.changeNotes = this.stepHistoryForm.get('reason')?.value;
    await this.ilaSrvc.bulkEditIlas(options).then(async (_) => {
      this.alert.successToast(
        `The ${options.actionType} operation was successfully performed on your ` + await this.labelPipe.transform('ILA') +`s.`
      );
      this.resetData();
    }).finally(() => {
      this.showSpinner = false;
    })
  }


  async EOsBulkEdit() {
    this.showSpinner = true;
    var options = new EnablingObjectiveOptions();
    if (this.actionId === 1) {
      options.actionType = this.showActive ? 'inactive' : 'active';
    }
    else {
      options.actionType = 'delete';
    }
    options.eoIds = this.linkedIds;
    options.effectiveDate = this.stepHistoryForm.get('EffectiveDate')?.value;
    options.changeNotes = this.stepHistoryForm.get('reason')?.value;
    await this.eoService.delete(options.eoIds[0], options).then(async (_) => {
      this.alert.successToast(
        `Selected ` + await this.transformTitle('Enabling Objective') + `s are now ${options.actionType}`
      );
      this.resetData();
    }).finally(() => {
      this.showSpinner = false;
    })
  }

  async EmployeeBulkEdit() {
    this.showSpinner = true;
    let options: EmployeeOptions = new EmployeeOptions();
    if (this.actionId === 1) {
      options.actionType = this.showActive ? 'inactive' : 'active';
    } else if (this.actionId === 2) {
      options.actionType = 'delete';
    }
    options.employeeIds = this.linkedIds;
    options.changeEffectiveDate = this.stepHistoryForm.get('EffectiveDate')?.value;
    options.changeNotes = this.stepHistoryForm.get('reason')?.value;
    this.employeeService
      .deleteEmployee(options)
      .then((res) => {
        this.alert.successToast(`Selected Employees are now ${options.actionType}`);
        this.resetData();
      })
      .finally(() => {
        this.showSpinner = false;
      });
  }

  async SHBulkEdit() {
    this.showSpinner = true;
    var options = new SaftyHazardOptions();
    if (this.actionId === 1) {
      options.actionType = this.showActive ? 'inactive' : 'active';
    } else if (this.actionId === 2) {
      options.actionType = 'delete';
    }
    options.saftyHazardIds = this.linkedIds;
    options.effectiveDate = this.stepHistoryForm.get('EffectiveDate')?.value;
    options.changeNotes = this.stepHistoryForm.get('reason')?.value;

    await this.shService
      .delete(options.saftyHazardIds[0], options)
      .then(async (res: any) => {
        this.alert.successToast(
          `Selected ${await this.labelPipe.transform("Safety Hazard")}s are now ${options.actionType}`
        );
        this.resetData();
      })
      .finally(() => {
        this.showSpinner = false;
      });
  }

  async RRBulkEdit() {
    this.showSpinner = true;
    var options = new RegulatoryRequirementOptions();
    if (this.actionId === 1) {
      options.actionType = this.showActive ? 'inactive' : 'active';
    } else if (this.actionId === 2) {
      options.actionType = 'delete';
    }
    options.regulatoryRequirementIds = this.linkedIds;
    options.effectiveDate = this.stepHistoryForm.get('EffectiveDate')?.value;
    options.changeNotes = this.stepHistoryForm.get('reason')?.value;
    this.rrService
      .delete(options.regulatoryRequirementIds[0], options)
      .then(async (res: any) => {
        this.alert.successToast(
          await this.labelPipe.transform('Regulatory Requirement') + ' Bulk ' + options.actionType + ' completed'
        );
        this.resetData();
      })
      .finally(() => {
        this.showSpinner = false;
      });
  }

  async ProcedureBulkEdit() {
    this.showSpinner = true;
    let options = new ProcedureOptions();
    if (this.actionId === 1) {
      options.actionType = this.showActive ? 'inactive' : 'active';
    } else if (this.actionId === 2) {
      options.actionType = 'delete';
    }
    options.procedureIds = this.linkedIds;
    options.changeEffectiveDate =
      this.stepHistoryForm.get('EffectiveDate')?.value;
    options.changeNotes = this.stepHistoryForm.get('reason')?.value;
    await this.procedureService
      .delete(options.procedureIds[0], options)
      .then((res) => {
        this.SaveProcedureHistory().then(async (_) => {
          this.alert.successToast(
            await this.transformTitle('Procedure') + ' Bulk ' + options.actionType + ' completed'
          );
          this.resetData();
        });
      })
      .finally(() => (this.showSpinner = false));
  }
  async InstructorBulkEdit() {
    this.showSpinner = true;
    var options = new Instructor_HistoryCreateOptions();
    if (this.actionId === 1) {
      options.ActionType = this.showActive ? 'inactive' : 'active';
    } else if (this.actionId === 2) {
      options.ActionType = 'delete';
    }
    options.instructorIds = this.linkedIds;
    options.EffectiveDate = this.stepHistoryForm.get('EffectiveDate')?.value;
    options.InstructorNotes = this.stepHistoryForm.get('reason')?.value;
    await this.insService
      .makeActiveInactiveOrDelete(options)
      .then(async (res: any) => {
        this.alert.successToast(
          `Selected ` + await this.labelPipe.transform('Instructor') + `s are now ${options.ActionType}`
        );
        this.resetData();
      })
      .finally(() => {
        this.showSpinner = false;
      });
  }


  async LocationBulkEdit() {
    this.showSpinner = true;
    var options = new Location_HistoryCreateOptions();
    if (this.actionId === 1) {
      options.ActionType = this.showActive ? 'inactive' : 'active';
    } else if (this.actionId === 2) {
      options.ActionType = 'delete';
    }
    options.locationIds = this.linkedIds;
    options.EffectiveDate = this.stepHistoryForm.get('EffectiveDate')?.value;
    options.Notes = this.stepHistoryForm.get('reason')?.value;
    await this.locService
      .makeActiveInactiveOrDelete(options)
      .then(async (res: any) => {
        this.alert.successToast(
          `Selected ` + await this.transformTitle('Location') +`s are now ${options.ActionType}`
        );
        this.resetData();
      })
      .finally(() => {
        this.showSpinner = false;
      });
  }

  async CertificationBulkEdit() {
    this.showSpinner = true;
    var options = new Certification_HistoryCreateOptions();
    if (this.actionId === 1) {
      options.ActionType = this.showActive ? 'inactive' : 'active';
    } else if (this.actionId === 2) {
      options.ActionType = 'delete';
    }
    options.certIds = this.linkedIds;
    options.EffectiveDate = this.stepHistoryForm.get('EffectiveDate')?.value;
    options.Notes = this.stepHistoryForm.get('reason')?.value;
    await this.certService
      .makeActiveInactiveOrDelete(options)
      .then(async (res: any) => {
        this.alert.successToast(
          `Selected ${await this.transformTitle("Certification")}s are now ${options.ActionType}d`
        );
        this.resetData();
      })
      .finally(() => {
        this.showSpinner = false;
      });
  }

  async TaskBulkEdit() {

    this.showSpinner = true;

    let options: TaskOptions = new TaskOptions();

    if (this.actionId === 1) {

      options.actionType = this.showActive ? 'inactive' : 'active';

    } else if (this.actionId === 2) {

      options.actionType = 'delete';

    }

    options.taskIds = this.linkedIds;

    options.effectiveDate = this.stepHistoryForm.get('EffectiveDate')?.value;

    options.changeNotes = this.stepHistoryForm.get('reason')?.value;

    this.taskSrvc

      .delete(options)

      .then(async (res) => {

        this.alert.successToast(`Selected ` + await this.transformTitle('Task') +`s are now ${options.actionType}`);

        this.resetData();

      })

      .finally(() => {

        this.showSpinner = false;

      });

  }

  async PositionBulkEdit() {
    this.showSpinner = true;
    let options: PositionOption = new PositionOption();
    if (this.actionId === 1) {
      options.actionType = this.showActive ? 'inactive' : 'active';
    } else if (this.actionId === 2) {
      options.actionType = 'delete';
    }
    options.positionIds = this.linkedIds;
    options.effectiveDate = this.stepHistoryForm.get('EffectiveDate')?.value;
    options.changeNotes = this.stepHistoryForm.get('reason')?.value;
    this.positionService
      .delete(options)
      .then(async (res) => {
        this.alert.successToast(`Selected ` + await this.transformTitle('Position') +`s are now ${options.actionType}`);
        this.resetData();
      })
      .finally(() => {
        this.showSpinner = false;
      });
  }

  resetData() {
    this.stepHistoryForm.reset();
    this.stepper.reset();
    // this.stepHistoryForm.get('reason')?.setValue(this.datePipe.transform(Date.now(), 'yyyy-MM-dd'))
    this.linkedIds = [];
    this.selection.clear();
    this.stepHistoryForm.get('EffectiveDate')?.setValue(this.datePipe.transform(Date.now(), 'yyyy-MM-dd'));
    this.reviewArray = [];
    this.dataSource = new MatTreeNestedDataSource<TreeData>();
    this.getTreeList();
  }

  async SaveProcedureHistory() {
    this.showSpinner = true;
    let options: Procedure_StatusHistoryCreateOptions = {
      changeEffectiveDate: this.stepHistoryForm.get('EffectiveDate')?.value,
      changeNotes: this.stepHistoryForm.get('reason')?.value,
      newStatus: false,
      oldStatus: false,
      procedureIds: this.linkedIds,
    };
    await this.procedureService
      .saveStatusHistory(options)
      .then((res) => { })
      .finally(() => {
        this.showSpinner = false;
      });
  }

  async goBack() {
    // history.back();
    // this.router.navigate(['my-data/procedures/overview']);

    switch (this.moduleName.toLowerCase()) {
      case 'procedure':
        this.router.navigate(['my-data/procedures/overview']);
        break;
      case 'regulation':
        this.router.navigate(['my-data/reg-requirements/overview']);
        break;
      case 'safety hazard':
        this.router.navigate(['my-data/safety-hazards/overview']);
        break;
      case 'instructor':
        this.router.navigate(['my-data/instructors/overview']);
        break;
      case 'location':
        this.router.navigate(['my-data/locations/overview']);
        break;
      case 'task':
        this.router.navigate(['my-data/tasks/overview']);
        break;
      case 'enabling objective':
        this.router.navigate(['my-data/enabling-objectives/overview']);
        break;
      case 'position':
        this.router.navigate(['my-data/positions/overview']);
        break;
      case 'employees':
        this.router.navigate(['implementation/employees']);
        break;
      case 'certification':
        this.router.navigate(['my-data/certifications/overview']);
        break;
      case 'ilas':
        this.router.navigate(['dnd/ila/list']);
        break;
    }
  }

  stepChanged(event: any) {
    event.selectedIndex > 1
      ? (this.DataSource = new MatTableDataSource(this.reviewArray))
      : '';
  }

  async getInstructors() {
    this.insService
      .getInsCategoryWithIns()
      .then((res) => {
        let treedata: TreeData[] = [];
        let tempArray = res.map((i) => {
          return {
            ...i,
            instructorCompactOptions: i.instructorCompactOptions.filter(
              (x) => x.active == this.showActive
            ),
          };
        });
        for (let i = 0; i < tempArray.length; i++) {
          if (tempArray[i].instructorCompactOptions.length > 0) {
            let tree = new TreeData();
            tree.id = res[i].instructor_Category.id;
            tree.description = res[i].instructor_Category.iCategoryTitle;
            tree.checkbox = true;
            tree.selected = false;
            tree.children = [];
            tree.shouldSelect = null;
            tempArray[i].instructorCompactOptions.forEach((d, i) => {
              if (tree.children) {
                tree.children.push({
                  id: d.id,
                  description:d.number + " - " + d.title,
                  selected: false,
                  checkbox: true,
                  active: d.active,
                  number: d.number,
                  shouldSelect: "Other",
                });
              }
            });

            treedata.push(tree);
          }
        }
        this.dataSource.data = treedata;
        this.toFilter = treedata;
        Object.assign(this.originalDataSource.data, this.dataSource.data);
        this.alterDataSource();
        Object.keys(this.dataSource.data).forEach((key: any) => {
          this.setParent(this.dataSource.data[key], undefined);
          this.setParent(this.originalDataSource.data[key], undefined);
        });
      })
      .finally(() => {
        this.showSpinner = false;
      });
  }

  async getLocations() {
    this.locService
      .getLocCategoryWithLoc()
      .then((res) => {
        let treedata: TreeData[] = [];
        let tempArray = res.map((i) => {
          return {
            ...i,
            locationCompactOptions: i.locationCompactOptions.filter(
              (x) => x.active == this.showActive
            ),
          };
        });
        for (let i = 0; i < tempArray.length; i++) {
          if (tempArray[i].locationCompactOptions.length > 0) {
            let tree = new TreeData();
            tree.id = res[i].location_Category.id;
            tree.description = res[i].location_Category.locCategoryTitle;
            tree.checkbox = true;
            tree.selected = false;
            tree.children = [];
            tree.shouldSelect = null;
            tempArray[i].locationCompactOptions.forEach((d) => {
              if (tree.children) {
                tree.children.push({
                  id: d.id,
                  description:d.locNum + " - " + d.name,
                  selected: false,
                  checkbox: true,
                  active: d.active,
                  number: d.locNum,
                  shouldSelect: "Other",
                });
              }
            });
            treedata.push(tree);
          }
        }
        this.dataSource.data = treedata;
        this.toFilter = treedata;
        Object.assign(this.originalDataSource.data, this.dataSource.data);
        this.alterDataSource();
        Object.keys(this.dataSource.data).forEach((key: any) => {
          this.setParent(this.dataSource.data[key], undefined);
          this.setParent(this.originalDataSource.data[key], undefined);
        });
      })
      .finally(() => {
        this.showSpinner = false;
      });
  }


  toFilter:any;
  async getCertifications() {
    this.certService
      .getCertCategoryWithCert()
      .then((res) => {
        let treedata: TreeData[] = [];
        let tempArray = res.map((i) => {
          return {
            ...i,
            certificationCompactOptions: i.certificationCompactOptions,
          };
        });
        for (let i = 0; i < tempArray.length; i++) {
          if (tempArray[i].certificationCompactOptions.length > 0) {
            let tree = new TreeData();
            tree.id = res[i].certifyingBody.id;
            tree.description = res[i].certifyingBody.name;
            tree.checkbox = true;
            tree.selected = false;
            tree.children = [];
            tree.shouldSelect = null;
            tree.active = res[i].certifyingBody.active;
            tempArray[i].certificationCompactOptions.forEach((d, index) => {
              if (tree.children) {
                tree.children.push({
                  id: d.id,
                  description: d.name,
                  selected: false,
                  checkbox: true,
                  active: d.active,
                   number: null,
                  shouldSelect: "Other",
                });
              }
            });

            treedata.push(tree);
          }
        }
        this.dataSource.data = treedata;
        this.toFilter = treedata;
        Object.assign(this.originalDataSource.data, this.dataSource.data);
        this.alterDataSource();
        Object.keys(this.dataSource.data).forEach((key: any) => {
          this.setParent(this.dataSource.data[key], undefined);
          this.setParent(this.originalDataSource.data[key], undefined);
        });

        this.filterData(this.dataSource.data, "");
      })
      .finally(() => {
        this.showSpinner = false;
      });
  }
  
}








class TreeData {
  id: any;
  description: string;
  children?: TreeData[];
  checkbox?: boolean;
  selected?: boolean;
  indeterminate?: boolean;
  parent?: TreeData;
  active?: boolean;
  number?: any;
  shouldSelect?: 'EO' | 'Task' | 'Other' | 'Meta' | 'SQ' | 'ilas' | null = null;
}
