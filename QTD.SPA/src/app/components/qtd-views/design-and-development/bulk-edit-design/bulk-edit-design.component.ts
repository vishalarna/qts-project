import { SelectionModel } from '@angular/cdk/collections';
import { BreakpointObserver } from '@angular/cdk/layout';
import { TemplatePortal } from '@angular/cdk/portal';
import { StepperOrientation } from '@angular/cdk/stepper';
import { NestedTreeControl } from '@angular/cdk/tree';
import { DatePipe } from '@angular/common';
import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { MatStepper } from '@angular/material/stepper';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { ActivatedRoute, Params } from '@angular/router';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { ILA } from 'src/app/_DtoModels/ILA/ILA';
import { TestOptions } from 'src/app/_DtoModels/Test/TestOptions';
import { TestItemOptions } from 'src/app/_DtoModels/TestItem/TestItemOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { IlaService } from 'src/app/_Services/QTD/ila.service';
import { TestItemService } from 'src/app/_Services/QTD/test-item.service';
import { TestsService } from 'src/app/_Services/QTD/tests.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { sideBarClose } from 'src/app/_Statemanagement/action/state.menutoggle';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-bulk-edit-design',
  templateUrl: './bulk-edit-design.component.html',
  styleUrls: ['./bulk-edit-design.component.scss']
})
export class BulkEditDesignComponent implements OnInit, AfterViewInit, OnDestroy {
  header = "Change Header";
  stepHeader = "Change Step 1 Header";
  stepperOrientation: Observable<StepperOrientation>;
  subscription = new SubSink();
  type = "";
  isTree = false;
  dataSource: MatTableDataSource<any> = new MatTableDataSource<any>();
  originalData: MatTableDataSource<any> = new MatTableDataSource<any>();
  displayedColumns = ["cb", "number", "description"];
  isLoading = false;
  filterString = "";
  moduleName = "";
  showActive = "";
  selection = new SelectionModel<any>(true);
  datepipe = new DatePipe('en-us');
  actionSpinner = false;
  typeFilter = "";
  statusFilter = true;
  eoId = null;
  testStatus = '';
  ilaId = null;
  ilaName = "";
  fromBulkEdit:boolean=true;

  hasChild = (_: number, node: TreeData) => {
    return !!node.children && node.children.length > 0;
  }

  treeDataSource = new MatTreeNestedDataSource<TreeData>();
  originalTreeDataSource = new MatTreeNestedDataSource<TreeData>();
  treeControl = new NestedTreeControl<TreeData>((node: any) => node.children);

  eoDescription = "None";

  condirmDataSource: MatTableDataSource<any> = new MatTableDataSource<any>();

  stepHistoryForm = new UntypedFormGroup({
    reason: new UntypedFormControl('', Validators.required),
    EffectiveDate: new UntypedFormControl(this.datepipe.transform(Date.now(), 'yyyy-MM-dd'), Validators.required),
  })

  displayColumns: string[] = ['number', 'description', 'action'];
  actionId: number = 0;
  testItemoptionsList = [
    { id: 1, description: 'Change Active / Inactive Status' },
    { id: 2, description: 'Change EO' },
  ];

  optionsList = [
    { id: 1, description: 'Change Active / Inactive Status' },
  ]

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort: MatSort;
  @ViewChild(MatStepper) stepper: MatStepper;

  constructor(
    private breakpointObserver: BreakpointObserver,
    private store: Store<any>,
    private route: ActivatedRoute,
    private testItemService: TestItemService,
    private alert: SweetAlertService,
    public flyPanelService: FlyInPanelService,
    private vcf: ViewContainerRef,
    private testService: TestsService,
    private ilaService: IlaService,
    private labelPipe: LabelReplacementPipe
  ) {
    this.stepperOrientation = breakpointObserver
      .observe('(min-width: 800px)')
      .pipe(map(({ matches }) => (matches ? 'horizontal' : 'vertical')));
  }

  ngOnInit(): void {
    this.store.dispatch(sideBarClose());

    // Can be used to apply custom filters if needed based on different views
    this.dataSource.filterPredicate = (row: any, filter: string) => {
      const filterObject = JSON.parse(filter);
      return ((
        row.number.toString().trim().toLowerCase().includes(filterObject.search) ||
        row.description.toString().trim().toLowerCase().includes(filterObject.search)) &&
        (this.type === 'question' || row.type.toString().trim().toLowerCase().includes(filterObject.type))) &&
        (row.status === filterObject.status)
    }
  }

  ngAfterViewInit(): void {
    this.subscription.sink = this.route.params.subscribe((res: Params) => {
      this.type = String(res.type).trim().toLowerCase();
      this.makeSelection();
    })
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  /// For all edits that require tree data set the appropriate tree data sources and also set isTree value to true
  makeSelection() {
    switch (this.type) {
      case "questions":
        this.setTestQuestionsData();
        break;
      case "test":
        this.setTestData();
        break;
    }
  }

  changeStatus() {
    switch (this.type) {
      case "questions":
        this.changeQuestionsStatus();
        break;
      case "test":
        this.changeTestStatus();
        break;
    }
  }

  async changeTestStatus() {
    this.actionSpinner = true;
    var options = new TestOptions();
    options.testIds = this.selection.selected.map((data) => {
      return data.id;
    })
    options.effectiveDate = this.stepHistoryForm.get('EffectiveDate')?.value;
    options.changeNotes = this.stepHistoryForm.get('reason')?.value;
    options.actionType = this.actionId === 3 ? 'Delete' : (this.actionId === 1 ? this.testStatus : 'change');
    options.ilaId = this.ilaId;
    await this.testService.delete(options).then(async (_) => {
      this.alert.successToast(`Bulk Edit Action ${options.actionType === 'Change' ? 'Change ' + await this.labelPipe.transform('ILA')  : options.actionType} Performed on ${this.moduleName}`);
      history.back();
    }).finally(() => {
      this.actionSpinner = false;
    })
  }

  async setTestData() {
    this.header = "Bulk Edit Tests";
    this.stepHeader = "Select Tests & Bulk Edit";
    this.moduleName = "Tests";
    this.isLoading = true;
    this.isTree = true;
    this.readyTestData();
  }

  async readyTestData() {
    this.isLoading = true;
    var ilas = await this.testService.getMinimalDataForILAWithTest().finally(()=>{
      this.isLoading = false;
    });

    ilas.forEach((ila, i) => {
      this.treeDataSource.data.push({
        id: ila.id,
        description: ila.name,
        checkbox: true,
        active: ila.active,
        canSelect: false,
        number:ila.number,
        children: ila.tests.map((test, x) => {
          return {
            id: test.id,
            description: test.testTitle,
            checkbox: true,
            active: test.active,
            canSelect: true,
            number: (x + 1)
          }
        })
      })
    });

    this.originalTreeDataSource.data = Object.assign(this.treeDataSource.data);
    Object.keys(this.treeDataSource.data).forEach((key: any) => {
      this.setParent(this.treeDataSource.data[key], undefined);
      this.setParent(this.originalTreeDataSource.data[key], undefined);
    });
    this.filterTests();


    // var ids = tests.map((data) => {
    //   return data.ilaId;
    // })
    // ids = Array.from(new Set(ids));
    // var ilaList: ILA[] = [];
    // tests.forEach(async (id, i) => {
    //   await this.ilaService.get(id).then((ila) => {
    //     ilaList.push(ila);
    //     if (ids.length === ilaList.length) {
    //       ilaList.forEach((ila) => {

    //         this.treeDataSource.data.push({
    //           id: ila.id,
    //           description: ila.name,
    //           checkbox: true,
    //           active: ila.active,
    //           canSelect : false,
    //           children: tests.filter((data) => {

    //             return data.ilaId === ila.id;
    //           }).map((child, x) => {
    //             return {
    //               id: child.id,
    //               description: child.test.testTitle,
    //               checkbox: true,
    //               active: child.test.active,
    //               canSelect:true,
    //               number: (x + 1)
    //             }
    //           })
    //         })
    //       });
    //       this.originalTreeDataSource.data = Object.assign(this.treeDataSource.data);
    //       Object.keys(this.treeDataSource.data).forEach((key: any) => {
    //         this.setParent(this.treeDataSource.data[key], undefined);
    //         this.setParent(this.originalTreeDataSource.data[key], undefined);
    //       });
    //       this.filterTests();
    //       this.isLoading = false;

    //     }
    //   })
    // })
  }

  private setParent(node: TreeData, parent: TreeData | undefined) {
    node.parent = parent;
    if (node.children) {
      node.children.forEach((childNode) => {
        this.setParent(childNode, node);
      });
    }
  }

  async setTestQuestionsData() {
    this.header = "Bulk Edit Test Items";
    this.stepHeader = "Select Test Items & Bulk Edit";
    this.moduleName = "Test Question";
    this.isLoading = true;
    this.isTree = false;
    var data = await this.testItemService.getAllWithFilterOption('none', null);
    var tempData: any[] = [];

    data.forEach((element, i) => {
      tempData.push({
        id: element.id,
        type: element.testItemType.description,
        typeId: element.testItemType.id,
        taxonomy: element.taxonomyLevel.description,
        taxonomyId: element.taxonomyLevel.id,
        description: element.description,
        isActive: element.active ? "ACTIVE" : "INACTIVE",
        number: element.number,
        status: element.active,
        eoId: element.eoId,
      })
    });
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
    this.dataSource.data = tempData;
    this.originalData.data = tempData;
    this.isLoading = false;
    this.toggleStatusFilter();
  }

  async changeQuestionsStatus() {
    this.actionSpinner = true;
    var options = new TestItemOptions();
    options.effectiveDate = this.stepHistoryForm.get('EffectiveDate')?.value;
    options.changeNotes = this.stepHistoryForm.get('reason')?.value;
    options.testIds = [];
    options.testIds = this.condirmDataSource.data.map((data) => {
      return data.id;
    });
    options.eoId = this.eoId;
    options.actionType = this.actionId === 3 ? "delete" : (this.actionId === 1 ? (this.statusFilter ? 'Inactive' : 'Active') : 'Change');
    await this.testItemService.changeStatus(options.testIds[0], options).then((_) => {
      this.alert.successToast(`Bulk Edit Action ${options.actionType === 'Change' ? 'Change EO' : options.actionType} Performed on ${this.moduleName}`);
      history.back();
    }).finally(() => {
      this.actionSpinner = false;
    });
  }

  redirectBack() {
    history.back();
  }

  filterData() {
    switch (this.type) {
      case "questions":
        this.filterQuestions();
        break;
      case "test":
        this.filterTests();
        break;
    }
  }

  filterTests() {
    this.treeDataSource.data = this.originalTreeDataSource.data.map((data) => {
      return {
        ...data,
        children: data.children?.filter((child) => {
          return (child.description.trim().toLowerCase().includes(this.filterString.trim().toLowerCase()) && child.active === this.statusFilter);
        })
      }
    });
    Object.keys(this.treeDataSource.data).forEach((key: any) => {
      this.setParent(this.treeDataSource.data[key], undefined);
    });

    this.treeDataSource.data.forEach((node: TreeData) => {
      node.children?.forEach((child) => {
        this.checkAllParents(child);
      })
    })
    this.treeControl.dataNodes = Object.assign(this.treeDataSource.data);
    this.filterString.length > 0 ? this.treeControl.expandAll() : this.treeControl.collapseAll();
  }

  filterQuestions() {
    var filterObject = {
      search: this.filterString.trim().toLowerCase(),
      type: this.typeFilter.trim().toLowerCase(),
      status: this.statusFilter,
    }
    this.dataSource.filter = JSON.stringify(filterObject);
  }

  masterToggle(event: any) {
    if (event.checked) {
      this.dataSource.data.forEach((data) => {
        if (this.type === 'questions') {
          if (data.type.trim().toLowerCase().includes(this.typeFilter.trim().toLowerCase())) {
            this.selection.select(data);
          }
        }
        else {
          this.selection.select(data);
        }
      });
    }
    else {
      this.dataSource.data.forEach((data) => {
        if (this.type === 'questions') {
          if (data.type.trim().toLowerCase().includes(this.typeFilter.trim().toLowerCase())) {
            this.selection.deselect(data);
          }
        }
        else {
          this.selection.deselect(data);
        }
      })
    }
  }

  onCheckChange(event: any, node: any) {
    if (event.checked && node.canSelect) {
      this.selection.select(node);
    } else {
      this.selection.deselect(node);
    }
    this.itemToggle(event.checked, node);
  }

  itemToggle(checked: boolean, node: TreeData) {
    node.selected = checked;
    if (node.children) {
      node.children.forEach((child) => {
        this.itemToggle(checked, child);
      });
      // if(!node.children || node.children.length > 0){
      //  this.selection.select(node);
      // }
    }
    else {
      if (node.selected) {
        this.selection.select(node);
      } else {
        this.selection.deselect(node);
      }
    }

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

  stepChanged(event: any) {
    if (event.selectedIndex === 2 && this.type === 'questions') {
      this.formulateMessage();
    }
    if (event.selectedIndex > 0 && this.type !== 'test') {
      var tempData: any[] = []
      this.selection.selected.forEach((data) => {
        tempData.push({
          ...data,
          action: this.actionId === 3 ? 'Delete' : (this.actionId === 1 ? (this.statusFilter ? 'Change Status to Inactive' : 'Change Status To Active') : `Change EO to ${this.eoDescription}`),
        })
      })
      this.condirmDataSource.data = tempData;
    }
    else if (event.selectedIndex > 0 && this.type === 'test') {
      this.formulateMessage();
      var tempData: any[] = []
      this.selection.selected.forEach(async (data) => {
        tempData.push({
          ...data,
          action: this.actionId === 3 ? 'Delete' : (this.actionId === 1 ? `Change Status To ${this.testStatus}` : `Change ` + await this.labelPipe.transform('ILA') +` to ${this.ilaName}`),
        })
      })
      this.condirmDataSource.data = tempData;
    }
  }

  message = "";

  async formulateMessage() {
    this.message = "";
    if (this.actionId === 1) {
      this.message = this.statusFilter ? "changing the status to Inactive" : "changing the status to Active";
    }
    else if (this.actionId === 2) {
      if (this.type === 'test') {
        this.message = "Change " + await this.labelPipe.transform('ILA') ;
      } else {
        this.message = "changing the EO";
      }
    }
    else if (this.actionId === 3) {
      this.message = "deleting the Test Questions";
    }
  }

  toggleStatusFilter() {
    this.selection.clear();
    switch (this.type) {
      case "questions":
        this.toggleFilterQuestions();
        break;
      case "test":
        this.toggleFilterTests();
        break;
    }
  }

  toggleFilterTests() {
    this.treeDataSource.data.forEach((data) => {
      data.selected = false;
      data.children?.forEach((child) => {
        child.selected = false;
      })
    })

    this.filterTests();
  }

  toggleFilterQuestions() {
    this.dataSource.data = this.originalData.data.filter((data) => {
      return data.status === this.statusFilter;
    })
  }

  openFlyPanel(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  eoSelected($event: any) {
    this.eoId = $event.id;
    this.eoDescription = $event.description;
    this.flyPanelService.close()
  }

  setValue(val: any) {
    this.testStatus = val;
  }

  menuClosed(event: any) {

    if (!event && this.testStatus === '') {
      this.actionId = 0;
      this.testStatus = '';
    }
  }

  ilaSelected(ila: any) {
    this.ilaId = ila.id;
    this.ilaName = ila.name;
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
  canSelect?: boolean;
}
