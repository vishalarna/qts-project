import { SelectionModel } from '@angular/cdk/collections';
import { NestedTreeControl } from '@angular/cdk/tree';
import { DatePipe } from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MatStepper, StepperOrientation } from '@angular/material/stepper';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { ToolBulkEdit_Options } from 'src/app/_DtoModels/Tool/ToolBulkEdit_Options';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { ToolsService } from 'src/app/_Services/QTD/tools.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-bulkedit-tool',
  templateUrl: './bulkedit-tool.component.html',
  styleUrls: ['./bulkedit-tool.component.scss']
})
export class BulkeditToolComponent implements OnInit {
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
  firstFormGroup: UntypedFormGroup;
  secondFormGroup: UntypedFormGroup;
  thirdFormGroup:UntypedFormGroup;
  EOTreeCheckListSelection = new SelectionModel<TreeData>(true);




  constructor(
    private fb: UntypedFormBuilder,
    private toolService: ToolsService,
    private alert : SweetAlertService,
    public router: Router,
    private labelPipe: LabelReplacementPipe,


  ) { }


  async ngOnInit(): Promise<void> {
    this.readyStepHistoryForm();
    this.getTools();
    this.optionsList = [
      { id: 1, description: 'Change Active/Inactive Status' },
      { id: 2, description: 'Delete ' + await this.labelPipe.transform('Tool') + 's'},
    ];
  }

  readyStepHistoryForm() {
    this.secondFormGroup = this.fb.group({
      reason: new UntypedFormControl('', Validators.required),
      EffectiveDate: new UntypedFormControl(
        this.datePipe.transform(Date.now(), 'yyyy-MM-dd'),
        Validators.required
      ),
    });
  }


  async getTools() {
    this.showSpinner = true;
    this.dataSource.data = [];
    this.originalDataSource.data = [];
    await this.toolService
      .getAllToolCategories(true)
      .then((res) => {
        let treedata: TreeData[] = [];
        let tempArray = res.map((ia) => {
          return {
            ...ia,
            tools: ia.tools,
          };
        });

        for (let i = 0; i < tempArray.length; i++) {
          if (tempArray[i].tools.length > 0) {
            let tree = new TreeData();
            tree.id = res[i].id;
            tree.description = res[i].title;
            tree.checkbox = true;
            tree.selected = false;
            tree.children = [];
            tree.Collapsed = true,

            tempArray[i].tools.forEach((d) => {
              if (tree.children) {
                tree.children.push({
                  id: d.id,
                  description: d.name,
                  selected: false,
                  checkbox: true,
                  active: d.active,
                  number: d.number,
                });
              }
            });
            treedata.push(tree);
          }
        }
        this.dataSource.data = treedata;
        this.originalDataSource.data = this.dataSource.data;
        this.alterDataSource();
        Object.assign(this.originalDataSource.data, this.dataSource.data);
        Object.keys(this.dataSource.data).forEach((key: any) => {
          this.setParent(this.dataSource.data[key], undefined);
          this.setParent(this.originalDataSource.data[key], undefined);
        });
      })
      .finally(() => {
        this.showSpinner = false;
      });
  }

  alterDataSource(){
    this.dataSource.data = [
      ...this.originalDataSource.data.map((n) => {
        return {
          ...n,
          children: n.children?.filter((c) => c.active === this.showActive),
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
    node.selected = node.checkbox ? checked : false;
    if (node.children) {
      node.children.forEach((child) => {
        this.itemToggle(checked, child);
      });
    } else {
      // Add EOIds to list
      if (checked && node.checkbox) {
        this.linkedIds.push(node.id);
        this.reviewArray.push({
          number: node.number,
          description: node.description
        });
      }
      else {
        var index = this.linkedIds.indexOf(node.id);
        if (index > -1) {
          this.linkedIds.splice(index, 1);
           const descIdx = this.reviewArray.findIndex(x => x.description === node.description);
           descIdx > -1 ? this.reviewArray.splice(descIdx, 1) : '';
        }
      }
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

  OnBulkEdit(){
    switch(this.actionId){
      case 1:
        var options = new ToolBulkEdit_Options();
        options.actionType = this.showActive ? 'inactive' : 'active';
        options.toolIds = [];
        options.toolIds = this.linkedIds;
        this.toolService.bulkEditTools(options).then(async (data)=>{
          this.alert.successToast(await this.labelPipe.transform('Tool') + '(s) made ' + options.actionType + ' successfully' );
          this.resetData();
        }).catch(async (err)=>{
          this.alert.errorToast('Error making ' + await this.labelPipe.transform('Tool') + ' ' + options.actionType);
        });

        break;

      case 2:
        var options = new ToolBulkEdit_Options();
        options.actionType = 'delete'
        options.toolIds = [];
        options.toolIds = this.linkedIds;
        this.toolService.bulkEditTools(options).then(async (data)=>{
          this.alert.successToast(await this.labelPipe.transform('Tool') + '(s) deleted successfully' );
          this.resetData();
        }).catch(async (err)=>{
          this.alert.errorToast('Error deleting ' + await this.labelPipe.transform('Tool'));
        });
        break;
    }
  }

  resetData() {
    this.secondFormGroup.reset();
    this.stepper.reset();
    // this.stepHistoryForm.get('reason')?.setValue(this.datePipe.transform(Date.now(), 'yyyy-MM-dd'))
    this.linkedIds = [];
    this.selection.clear();
    this.secondFormGroup.get('EffectiveDate')?.setValue(this.datePipe.transform(Date.now(), 'yyyy-MM-dd'));
    this.reviewArray = [];
    this.dataSource = new MatTreeNestedDataSource<TreeData>();
    this.getTools();
  }

  continueClicked() {
    if (this.stepper.selectedIndex == 1) {
      this.DataSource = new MatTableDataSource(this.reviewArray);
    }
    this.stepper.next();
  }

  async goBack() {
    this.router.navigate(['my-data/tools/overview']);
  }

  stepChanged(event: any) {
    event.selectedIndex > 1
      ? (this.DataSource = new MatTableDataSource(this.reviewArray))
      : '';
  }

  filterActive(active: boolean) {
    this.showActive = active;
    this.dataSource.data = [];
    this.getTools();
  }

  textFilter(event:any){
    this.toFilter = [
      ...this.originalDataSource.data.map((n) => {
        return {
          ...n,
          Collapsed: true,
          children: n.children?.filter((c) =>
            c.description.toLowerCase().includes(String(event).toLowerCase())
          ),
        };
      }),
    ];

    this.dataSource.data = Object.assign(this.toFilter);
    this.treeControl.dataNodes = Object.assign(this.toFilter);
     Object.keys(this.dataSource.data).forEach((key: any) => {
      this.setParent(this.dataSource.data[key], undefined);
    });
    this.filter.length > 0 ? this.treeControl.expandAll() : this.treeControl.collapseAll();
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
  Collapsed?: boolean = false;
}
