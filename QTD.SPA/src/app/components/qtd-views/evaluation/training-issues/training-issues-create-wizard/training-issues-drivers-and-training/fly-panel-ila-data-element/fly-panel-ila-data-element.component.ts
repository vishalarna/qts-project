import { SelectionModel } from '@angular/cdk/collections';
import { NestedTreeControl } from '@angular/cdk/tree';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { ILATree } from '@models/ILA/ILATree';
import { TrainingIssue_DataElement_VM } from '@models/TrainingIssues/TrainingIssue_DataElement_VM';
import { RegulatoryRequirementService } from 'src/app/_Services/QTD/regulatory-requirement.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';

@Component({
  selector: 'app-fly-panel-ila-data-element',
  templateUrl: './fly-panel-ila-data-element.component.html',
  styleUrls: ['./fly-panel-ila-data-element.component.scss']
})
export class FlyPanelIlaDataElementComponent implements OnInit {
  @Output() closed = new EventEmitter<any>();
  @Output() linkDataElement = new EventEmitter<TrainingIssue_DataElement_VM>();
  @Input() inputTrainingIssueDataElementVM: TrainingIssue_DataElement_VM = new TrainingIssue_DataElement_VM();
  treeControl = new NestedTreeControl<ILATree>((node: any) => node.children);
  dataSource = new MatTreeNestedDataSource<ILATree>();
  originalDataSource: ILATree[]=[];
  treeCheckListSelection = new SelectionModel<ILATree>(false);
  hasChild = (_: number, node: ILATree) => !!node.children && node.children.length > 0;
  filterSearchString: string = "";
  linkedId: string;
  loader: boolean = false;
  showActive: boolean = true;

  constructor(
    public flyPanelService: FlyInPanelService,
    private rrService: RegulatoryRequirementService,
  ) { }

  ngOnInit(): void {
    this.linkedId = this.inputTrainingIssueDataElementVM?.dataElementId;
    this.readyIlasTreeDataAsync();
  }

  clearSearchString() {
    this.filterSearchString = '';
    this.filterILA();
  }

  searchFilter(event: any) {
    this.filterSearchString = event?.target?.value ?? "";
    this.filterILA();
  }

  filterILA() {
    let tempData = [
      ...this.originalDataSource.map((element) => {
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
  }

  filterActive(filterType: boolean) {
    this.showActive = filterType ;
    this.filterILA();
  }

  async readyIlasTreeDataAsync() {
    this.loader = true;
    await this.getProviderDataAsync();
    this.loader = false;
  }

  async getProviderDataAsync() {
    this.loader = true;
    this.dataSource.data = [];
    await this.rrService.getProviderWithILAs().then((res) => {
      var provData = res.map((x)=>{
        return{
          id:x.providerId,
          name:x.providerName,
          active:x.providerActive,
          ilAs:x.ilaDetails
        }
      })
      this.makeIlaTreeDataSource(provData);
    })
      .catch((err: any) => {
        console.error(err);
      });
  }

  makeIlaTreeDataSource(res: any) {
    this.treeCheckListSelection.clear();
    if (res.length == 0) {
      this.dataSource.data = [];
      this.loader = false;
    } else {
      this.loader = true;
      var treeData: any = [{}];
      const filteredProviders = res.filter(provider =>provider.ilAs && provider.ilAs.length > 0)

      filteredProviders.forEach((data, index) => {
        treeData[index] = {
          id: data.id,
          description: data.name,
          children: [],
        };

        data.ilAs.forEach(element => {
          treeData[index]['children'].push({
            id: element.id,
            description: element.number + " " + element.name,
            active: element.active,
          });
        });
        treeData.forEach((data) => {
          data.children.forEach((elm) => {
            if (this.inputTrainingIssueDataElementVM?.dataElementId == elm.id) {
              this.treeControl.expand(data);
            }
          });
        })
      });
      this.dataSource.data = treeData;
      this.originalDataSource = Object.assign(treeData);
      Object.keys(this.dataSource.data).forEach((key: any) => {
        this.setParent(this.dataSource.data[key], undefined);
        this.setParent(this.originalDataSource[key], undefined);
      });
      this.treeControl.dataNodes = Object.assign(this.originalDataSource);
    this.filterILA();
      this.loader = false;
    }
  }

  private setParent(node: ILATree, parent: ILATree | undefined) {
    node.parent = parent;
    if (node.children) {
      node.children.forEach((childNode) => {
        this.setParent(childNode, node);
      });
    }
  }

  onChangeILA(selected: boolean, node: ILATree) {
    this.treeCheckListSelection.clear();
    if (selected) {
      this.treeCheckListSelection.select(node);
      this.linkedId = node.id;
    }
  }

  isProviderSelected(node: ILATree) {
    return this.linkedId != null ? node.children.some(x => x.id == this.linkedId) : false;
  }

  isILASelected(node: ILATree) {
    return this.linkedId != null ? this.linkedId == node.id : false;
  }

  linkILA() {
    this.inputTrainingIssueDataElementVM.dataElementId = this.linkedId;
    this.linkDataElement.emit(this.inputTrainingIssueDataElementVM);
    this.closed.emit();
  }
}


