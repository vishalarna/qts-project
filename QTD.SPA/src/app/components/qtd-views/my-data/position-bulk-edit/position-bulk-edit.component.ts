import { SelectionModel } from '@angular/cdk/collections';
import { BreakpointObserver } from '@angular/cdk/layout';
import { NestedTreeControl } from '@angular/cdk/tree';
import { DatePipe, TitleCasePipe } from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MatStepper, StepperOrientation } from '@angular/material/stepper';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { PositionOption } from 'src/app/_DtoModels/Position/PositionOption';
import { DynamicLabelReplacementPipe } from 'src/app/_Pipes/dynamic-label-replacement.pipe';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { PositionsService } from 'src/app/_Services/QTD/positions.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { sideBarClose} from 'src/app/_Statemanagement/action/state.menutoggle';


@Component({
  selector: 'app-position-bulk-edit',
  templateUrl: './position-bulk-edit.component.html',
  styleUrls: ['./position-bulk-edit.component.scss'],
  providers: [TitleCasePipe]
})
export class PositionBulkEditComponent implements OnInit {

  moduleName: string = 'position';
  @ViewChild('stepper') stepper: MatStepper;
  DataSource: MatTableDataSource<any>;
  dataSource = new MatTreeNestedDataSource<TreeData>();
  originalDataSource = new MatTreeNestedDataSource<TreeData>();
  reviewArray: any[] = [];
  linkedIds: any[] = [];
  actionId: any;
  showSpinner: boolean = false;
  stepperOrientation: Observable<StepperOrientation>;
  showActive: boolean = true;
  stepHistoryForm: UntypedFormGroup;
  datePipe = new DatePipe('en-us');
  filterString: string = "";
  optionsList: any[] = [];
  disabledContinue: boolean = false;
  selection = new SelectionModel<TreeData>(true);
  treeControl = new NestedTreeControl<TreeData>((node: any) => node.children);
  hasChild = (_: number, node: TreeData) =>
    !!node.children && node.children.length > 0;
  displayColumns: string[] = ['number', 'description'];
  posList: any[] = [];
  toFilter: any[] = [];
  filter = '';

  constructor(private store: Store<{ toggle: string }>,
              public breakpointObserver: BreakpointObserver,
              private positionService: PositionsService,
              private alert: SweetAlertService,
              private fb: UntypedFormBuilder,
              public titleCasePipe: TitleCasePipe,
              private router: Router,
              private labelPipe: LabelReplacementPipe,
              private dynamicLabelReplacementPipe: DynamicLabelReplacementPipe) {
    this.stepperOrientation = breakpointObserver
      .observe('(min-width: 800px)')
      .pipe(map(({ matches }) => (matches ? 'horizontal' : 'vertical')));
   }

  async ngOnInit(): Promise<void> {
    this.toggleMainMenu();

    this.readyStepHistoryForm();

    this.getTreeList();

    this.optionsList = [
      { id: 1, description: 'Change Active/Inactive Status' },
      { id: 2, description: 'Delete ' + await this.dynamicLabelReplacementPipe.transform(this.titleCasePipe.transform(this.moduleName + 's'))},
    ];
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }  
  
  toggleMainMenu() {
    this.store.dispatch(sideBarClose());


  }

  readyStepHistoryForm() {
    this.stepHistoryForm = this.fb.group({
      reason: new UntypedFormControl('', Validators.required),
      EffectiveDate: new UntypedFormControl(
        this.datePipe.transform(Date.now(), 'yyyy-MM-dd' ),
        Validators.required
      ),
    });
  }

  async goBack() {
    history.back();
    // this.router.navigate(['my-data/procedures/overview']);
  }

  continueClicked() {
    debugger;
    if (this.stepper.selectedIndex == 1) {

      this.reviewArray.sort((a, b) => a.number - b.number);

      this.DataSource = new MatTableDataSource(this.reviewArray);
    }
    this.stepper.next();
  }

  OnBulkEdit() {
    this.PositionBulkEdit();
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
        this.alert.successToast(`Selected ` + await this.transformTitle('Position') +`s are now ` + this.titleCasePipe.transform(options.actionType));
      //  this.router.navigate(['my-data/positions/overview']);
        this.resetData();
      })
      .finally(() => {
        this.showSpinner = false;
      });
  }

  resetData() {
    this.stepHistoryForm.reset();
    this.stepper.reset();
    this.stepHistoryForm.get('EffectiveDate')?.setValue(this.datePipe.transform(Date.now(), 'yyyy-MM-dd'));

   // this.stepHistoryForm.get('reason')?.setValue(this.datePipe.transform(Date.now(), 'yyyy-MM-dd'))
    this.linkedIds = [];
    this.selection.clear();
    this.reviewArray = [];
    this.dataSource = new MatTreeNestedDataSource<TreeData>();
    this.getTreeList();

  }

  getTreeList() {
    this.selection.clear();
    this.linkedIds = [];
    this.reviewArray = [];
    this.getPositions()
  }

  async getPositions(){
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
        tree.number = res[i].positionNumber;
        treedata.push(tree);
      }
      this.posList = treedata;

      this.dataSource.data = treedata;
      this.toggleActiveFilter(this.showActive);
      this.toFilter = this.posList;
      Object.assign(this.originalDataSource.data, this.dataSource.data);
    })
      .finally(() => {
        this.showSpinner = false;
      });
  }

  toggleActiveFilter(isActive: boolean) {
    this.posList = [
      ...this.posList.filter((pos) => {
        return pos.active == isActive;
      })
    ];
    this.showActive = isActive;
  }

  stepChanged(event: any) {
    event.selectedIndex > 1
      ? (this.DataSource = new MatTableDataSource(this.reviewArray))
      : '';
  }

  filterData(data: any, toFilter: any) {
    this.commonFilter();
  }

  searchFilter(event: any) {
    this.toFilter = [
      ...this.posList.filter((c) =>
            c.description.toLowerCase().includes(String(event).toLowerCase())
          ),
    ];
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

  onCheckChange(event: any, node: any) {
    if (event.checked) {
      this.selection.select(node);
      this.disabledContinue = true;
    } else {
      this.selection.deselect(node);
    }
    this.itemToggle(event.checked, node);
  }


  itemToggle(checked: boolean, node: TreeData) {
    node.selected = checked;
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

  filterActive(active: boolean) {
    this.showActive = active;
    this.dataSource.data = [];
    this.getTreeList();
  }

  selectedChanged(event: any) {
    if (event.selectedIndex === 1) {
    }
  }

  clearSearch:string ='';
  clearFilter(){
    this.filter = null;
    this.getPositions();
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
}
