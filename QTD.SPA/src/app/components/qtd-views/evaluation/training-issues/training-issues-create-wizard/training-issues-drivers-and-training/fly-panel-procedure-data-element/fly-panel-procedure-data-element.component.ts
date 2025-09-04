import { SelectionModel } from '@angular/cdk/collections';
import { NestedTreeControl } from '@angular/cdk/tree';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { ProcedureTree } from '../../../../../../../_DtoModels/Procedure/ProcedureTree';
import { IssuingAuthoritiesService } from 'src/app/_Services/QTD/issuing-authorities.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { TrainingIssue_DataElement_VM } from '@models/TrainingIssues/TrainingIssue_DataElement_VM';

@Component({
  selector: 'app-fly-panel-procedure-data-element',
  templateUrl: './fly-panel-procedure-data-element.component.html',
  styleUrls: ['./fly-panel-procedure-data-element.component.scss']
})
export class FlyPanelProcedureDataElementComponent implements OnInit {
  @Output() closed = new EventEmitter<any>();
  @Output() linkDataElement = new EventEmitter<TrainingIssue_DataElement_VM>();
  @Input() inputTrainingIssueDataElementVM: TrainingIssue_DataElement_VM = new TrainingIssue_DataElement_VM();
  treeControl = new NestedTreeControl<ProcedureTree>((node: any) => node.children);
  dataSource = new MatTreeNestedDataSource<ProcedureTree>();
  treeCheckListSelection = new SelectionModel<ProcedureTree>(false);
  hasChild = (_: number, node: ProcedureTree) => !!node.children && node.children.length > 0;
  treeData: ProcedureTree[] = [];
  showActive: boolean = true;
  spinner: boolean = true;
  showLinkLoader: boolean = true;
  filterSearchString: string;
  myData: any[] = [];
  linkedId: string;
  loader: boolean = false;

  constructor(
    private issuingAuthoritiesService: IssuingAuthoritiesService,
    public flyPanelSrvc: FlyInPanelService
  ) { }

  ngOnInit(): void {
    this.filterSearchString = '';
    this.linkedId = this.inputTrainingIssueDataElementVM?.dataElementId;
    this.makeProcTreeDataSource();
  }

  searchFilter(event: any) {
    this.filterSearchString = event?.target?.value ?? "";
    this.filterProcedure();
  }

  clearSearchString() {
    this.filterSearchString = "";
    this.filterProcedure();
  }

  filterProcedure() {
    let tempData = [
      ...this.myData.map((element) => {
        return {
          ...element,
          children: element.children?.filter((c) => {
            return c.active == this.showActive && (this.filterSearchString.length > 0
                ? c.description.trim().toLowerCase().includes(this.filterSearchString.trim().toLowerCase()) : true);
          }
          ),
        };
      }),
    ];
    this.dataSource.data = tempData.filter((x) => { return x.children !== null && x.children !== undefined && x.children.length > 0 });
    this.treeControl.dataNodes = this.dataSource.data;
    this.filterSearchString.length > 0 ? this.treeControl.expandAll() : this.treeControl.collapseAll();
    this.dataSource.data.forEach((data) => {
      data.children.forEach((elm) => {
        if (this.inputTrainingIssueDataElementVM?.dataElementId == elm.id) {
          this.treeControl.expand(data);
        }
      });
    })
    this.loader = false;
  }

  filterActive(filterType: boolean) {
    this.showActive = filterType ;
    this.filterProcedure();
  }

  async makeProcTreeDataSource() {
    this.loader = true;
    await this.issuingAuthoritiesService.getAll().then((res) => {
      var treeData: any[] = [];
      for (let index = 0; index < res.length; index++) {
        let procTree = new ProcedureTree();
        procTree.description = res[index].title;
        procTree.children = [];

        res[index].procedures.forEach((proc) => {
          procTree.children?.push({
            id: proc.id,
            description: proc.title,
            active: proc.active,
            number: proc.number,
          });
        });
        treeData.push(procTree);
      }
      this.dataSource.data = treeData;
      this.myData = Object.assign(treeData);
      Object.keys(this.dataSource.data).forEach((key: any) => {
        this.setParent(this.dataSource.data[key], undefined);
        this.setParent(this.myData[key], undefined);
      });
      this.treeControl.dataNodes = Object.assign(this.myData);
    });
    this.filterProcedure();
  }

  private setParent(node: ProcedureTree, parent: ProcedureTree | undefined) {
    node.parent = parent;
    if (node.children) {
      node.children.forEach((childNode) => {
        this.setParent(childNode, node);
      });
    }
  }

  onChangeProcedure(selected: boolean, node: ProcedureTree) {
    this.treeCheckListSelection.clear();
    if (selected) {
      this.treeCheckListSelection.select(node);
      this.linkedId = node.id;
    }
  }

  isIssuingAuthoritySelected(node: ProcedureTree) {
    return this.linkedId != null ? node.children.some(x => x.id == this.linkedId) : false;
  }

  isProcedureSelected(node: ProcedureTree) {
    return this.linkedId != null ? this.linkedId == node.id : false;
  }

  linkProcedure() {
    this.inputTrainingIssueDataElementVM.dataElementId = this.linkedId;
    this.linkDataElement.emit(this.inputTrainingIssueDataElementVM);
    this.closed.emit();
  }

}
