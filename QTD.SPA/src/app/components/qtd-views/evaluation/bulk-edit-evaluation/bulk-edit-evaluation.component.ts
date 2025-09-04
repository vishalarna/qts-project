import { SelectionModel } from '@angular/cdk/collections';
import { BreakpointObserver } from '@angular/cdk/layout';
import { StepperOrientation } from '@angular/cdk/stepper';
import { NestedTreeControl } from '@angular/cdk/tree';
import { DatePipe } from '@angular/common';
import {
  Component,
  EventEmitter,
  OnInit,
  Output,
  ViewChild,
} from '@angular/core';
import {
  UntypedFormBuilder,
  UntypedFormControl,
  UntypedFormGroup,
  Validators,
} from '@angular/forms';
import { MatStepper } from '@angular/material/stepper';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { Router, ActivatedRoute } from '@angular/router';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { PositionOption } from 'src/app/_DtoModels/Position/PositionOption';
import { StudentEvaluationHistoryCreateOptions } from 'src/app/_DtoModels/StudentEvaluationHistory/StudentEvaluationHistoryCreateOptions';
import { StudentEvaluationService } from 'src/app/_Services/QTD/student-evaluation.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { sideBarClose } from 'src/app/_Statemanagement/action/state.menutoggle';

@Component({
  selector: 'app-bulk-edit-evaluation',
  templateUrl: './bulk-edit-evaluation.component.html',
  styleUrls: ['./bulk-edit-evaluation.component.scss'],
})
export class BulkEditEvaluationComponent implements OnInit {
  moduleName: string;

  @Output() closed = new EventEmitter<any>();
  showSpinner: boolean = false;
  stepperOrientation: Observable<StepperOrientation>;
  stepHistoryForm: UntypedFormGroup;
  linkedIds: any[] = [];
  optionsList: any[] = [];
  DataSource: MatTableDataSource<any>;
  showActive: boolean = true;
  filterString: string = '';
  actionId: any;
  selection = new SelectionModel<TreeData>(true);
  dataSource = new MatTreeNestedDataSource<TreeData>();
  originalDataSource = new MatTreeNestedDataSource<TreeData>();
  treeControl = new NestedTreeControl<TreeData>((node: any) => node.children);
  displayColumns: string[] = ['number', 'description'];
  hasChild = (_: number, node: TreeData) =>
    !!node.children && node.children.length > 0;
  filterProcString: any;
  datePipe = new DatePipe('en-us');
  disabledContinue: boolean = false;
  moduleTitle:string

  reviewArray: any[] = [];
  // @ViewChild(MatSort) set tblSort(sort: MatSort) {
  //   if (sort) this.DataSource.sort = sort;
  // }

  @ViewChild('stepper') stepper: MatStepper;
  constructor(
    private fb: UntypedFormBuilder,

    public breakpointObserver: BreakpointObserver,
    private router: Router,
    private store: Store<{ toggle: string }>,
    private activatedRoute: ActivatedRoute,

    private alert: SweetAlertService,

    private studentEvaluationService: StudentEvaluationService
  ) {
    this.stepperOrientation = breakpointObserver
      .observe('(min-width: 800px)')
      .pipe(map(({ matches }) => (matches ? 'horizontal' : 'vertical')));

    this.activatedRoute.params.subscribe((res) => {

      this.moduleName = res?.name.replace('-', ' ');
      // if(this.moduleName === 'Studentevaluation')
      // {
      //   this.moduleName = 'Student Evaluations'
      // }
    });
  }

  ngOnInit(): void {
    this.readyStepHistoryForm();

    this.toggleMainMenu();

    this.getTreeList();

    this.optionsList = [
      { id: 1, description: 'Change Active/ Inactive Status' },
      { id: 2, description: 'Delete' },
    ];
  }

  toggleMainMenu() {
    //this.databroadcastSrvc.ToggleMainMenu.next('');
    this.store.dispatch(sideBarClose());
  }

  getTreeList() {

    this.selection.clear();
    this.linkedIds = [];
    this.reviewArray = [];
    switch (this.moduleName.toLowerCase()) {
      case 'studentevaluation':
        this.moduleTitle = 'Student Evaluation'
        this.getStudentEvaluation();
        break;
    }
  }
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
    }else {
      this.selection.deselect(node);
    }


    this.itemToggle(event.checked, node);

  }

  itemToggle(checked: boolean, node: TreeData) {
    node.selected = checked;
    if (node.selected) {
      this.linkedIds.push(node.id);
      this.reviewArray.push(node.description);
    } else {

        const linkIdx = this.linkedIds.indexOf(node.id);
        const descIdx = this.reviewArray.indexOf(node.description);

        linkIdx > -1 ? this.linkedIds.splice(linkIdx, 1) : '';
        descIdx > -1 ? this.reviewArray.splice(descIdx, 1) : '';
    }
    this.linkedIds = [...new Set(this.linkedIds)];
    this.reviewArray = [...new Set(this.reviewArray)];
    this.checkAllParents(node);



   /*  node.selected = checked;
    if(node.selected)
    {
      this.linkedIds.push(node.id);

      this.reviewArray.push(node);
    }
    else
    {
        const linkIdx = this.linkedIds.indexOf(node.id);
        const descIdx = this.reviewArray.indexOf(node.description);
        linkIdx > -1 ? this.linkedIds.splice(linkIdx, 1) : '';
        linkIdx > -1 ? this.reviewArray.splice(linkIdx, 1) : '';
    }

    this.linkedIds = [...new Set(this.linkedIds)];

    this.checkAllParents(node); */
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
    //
    return node.description.includes(this.filterProcString);
  }

  filterData(data: any, toFilter: any) {
    switch (this.moduleName.toLowerCase()) {
      case 'studentevaluation':
        this.commoStudentEvaluationFilter();
        break;
    }

  }
  commoStudentEvaluationFilter() {
    if (this.filterString.length > 0) {
      this.dataSource.data = [
        ...this.originalDataSource.data.filter((c) => {
          return c.description
            .toLowerCase()
            .includes(String(this.filterString).toLowerCase());
        }),
      ];
    } else {
      this.dataSource.data = this.originalDataSource.data;
    }
  }

  commonFilter() {
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
  OnBulkEdit() {
    switch (this.moduleName.toLowerCase()) {
      case 'studentevaluation':
        this.StudentEvaluationBulkEdit();
        break;
    }
  }
  async StudentEvaluationBulkEdit() {

    this.showSpinner = true;
    let options: StudentEvaluationHistoryCreateOptions =
      new StudentEvaluationHistoryCreateOptions();
    if (this.actionId === 1) {
      options.actionType = this.showActive ? 'inactive' : 'active';
    } else if (this.actionId === 2) {
      options.actionType = 'delete';
    }
    options.studentEvaluationIds = this.linkedIds;
    options.effectiveDate = this.stepHistoryForm.get('EffectiveDate')?.value;
    options.studentEvaluationNotes = this.stepHistoryForm.get('reason')?.value;

    this.studentEvaluationService
      .makeActiveInactiveOrDelete(options.studentEvaluationIds[0], options)
      .then((res) => {
        this.alert.successToast(
          `Selected Student Evaluations are now ${options.actionType}`
        );
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
    this.stepHistoryForm
      .get('EffectiveDate')
      ?.setValue(this.datePipe.transform(Date.now(), 'yyyy-MM-dd'));
    this.reviewArray = [];
    this.dataSource = new MatTreeNestedDataSource<TreeData>();
    this.getTreeList();
  }

  async getStudentEvaluation() {
    this.showSpinner = true;
    this.dataSource.data = [];
    this.originalDataSource.data = [];
    await this.studentEvaluationService
      .getAll()
      .then((res) => {

        let treedata: TreeData[] = [];
        let tempArray = res;

        let tree = new TreeData();

        tempArray.forEach((x) => {
          if (x.active === this.showActive && x.classRoasterIsReleased === false)
          {
            this.dataSource.data.push({
              id: x.id,
              description: x.title,
              checkbox: true,
              selected: false,
              children: [],
              shouldSelect:'Eval'
            });
          }

        });


        Object.assign(this.originalDataSource.data, this.dataSource.data);
      })
      .finally(() => {
        this.showSpinner = false;
      });
  }
  filterActive(active: boolean) {
    this.showActive = active;
    this.dataSource.data = [];
    this.getTreeList();
  }
  async goBack() {
    history.back();
  }
  stepChanged(event: any) {
    event.selectedIndex > 1
      ? (this.DataSource = new MatTableDataSource(this.reviewArray))
      : '';
  }
  selectedChanged(event: any) {
    if (event.selectedIndex === 1) {
    }
  }
  continueClicked() {
    if (this.stepper.selectedIndex == 1) {

      this.DataSource = new MatTableDataSource(this.reviewArray);
    }
    this.stepper.next();
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
  shouldSelect?: 'Eval' | null = null;

}
