import { NestedTreeControl } from '@angular/cdk/tree';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { RegulatoryRequirementService } from 'src/app/_Services/QTD/regulatory-requirement.service';
import { SafetyHazardsService } from 'src/app/_Services/QTD/safety-hazards.service';

@Component({
  selector: 'app-flypanel-select-ila',
  templateUrl: './flypanel-select-ila.component.html',
  styleUrls: ['./flypanel-select-ila.component.scss']
})
export class FlypanelSelectIlaComponent implements OnInit {
  filterBy: string = 'Provider';
  filterIlaString: string = "";
  showActive: boolean = true;
  isILALoading: boolean = false;
  isProvider = true;
  @Output() closed = new EventEmitter<any>();
  treeControl = new NestedTreeControl<IlaTree>((node: any) => node.children);
  dataSource = new MatTreeNestedDataSource<IlaTree>();
  originalSource = new MatTreeNestedDataSource<IlaTree>();
  hasChild = (_: number, node: IlaTree) =>
    !!node.children && node.children.length > 0;
  @Input() id: any;
  @Input() alreadyLinked: any[] = [];
  @Output() refresh = new EventEmitter<any>();
  @Output() idSelected = new EventEmitter<any>();

  constructor(
    private shService: SafetyHazardsService,
  ) { }

  ngOnInit(): void {
    this.readyIlasTreeData();
  }

  async readyIlasTreeData() {
    this.getProviderData();
  }

  async getProviderData() {
    this.isILALoading = true;
    this.dataSource.data = [];
    await this.shService
      .getProviderWithILAs()
      .then((res) => {

        this.makeIlaTreeDataSource(res);
      }).finally(()=>{
        this.isILALoading = false;
      })
  }

  async getTopicData() {
    this.dataSource.data = [];
    this.isILALoading = true;
    await this.shService
      .getTopicWithILAs()
      .then((res) => {
        var tempSrc =[];
        res.forEach(topic=>{
          tempSrc.push({
            id:topic.topicId,
            name:topic.topicName,
            active:topic.topicActive,
            ilAs:topic.ilaDetails
          })
        })
        this.makeIlaTreeDataSource(tempSrc);
      }).finally(()=>{
        this.isILALoading = false;
      })
  }

  makeIlaTreeDataSource(res: any) {
    this.isILALoading = true;
    if (res.length == 0) {
      this.dataSource.data = [];
      this.isILALoading = false;
    }
    else {
      this.isILALoading = true;
      var treeData: any = [{}];

      for (var data in res) {

        treeData[data] = {
          id: res[data]['id'],
          description: res[data]['name'],
          children: [],
          checkbox: true,
        };

        res[data].ilAs.forEach(element => {
          treeData[data]['children'].push({
            id: element.id,
            description: element.number + " " + element.name,
            checkbox: !this.alreadyLinked.includes(element.id),
            active: element.active,
          })
        });
      }

      this.dataSource.data = treeData;
      this.originalSource.data = treeData;
      this.filterActive(true);


      this.isILALoading = false;
    }

  }

  filterActive(makeActive: boolean) {
    this.showActive = makeActive;
    this.filterData("", "");
  }

  filterData(data: any, toFilter: any) {
    let temparr = [
      ...this.originalSource.data.map((element) => {
        return {
          ...element,
          children: element.children?.filter((c) => {
            return c.description.toLowerCase().match(String(this.filterIlaString).toLowerCase()) &&
              c.active == this.showActive;
          }
          ),
        };
      }),
    ];

    this.dataSource.data = temparr;
    this.treeControl.dataNodes = this.dataSource.data;
    this.filterIlaString.length > 0 ? this.treeControl.expandAll() : this.treeControl.collapseAll();
  }

}

class IlaTree {
  id: any;
  description: string;
  children?: IlaTree[];
  active?: boolean;
  checkbox?: boolean;
  selected?: boolean;
  indeterminate?: boolean;
  parent?: IlaTree;
}
