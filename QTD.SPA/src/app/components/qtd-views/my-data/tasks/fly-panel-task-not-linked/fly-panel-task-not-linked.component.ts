import { NestedTreeControl } from '@angular/cdk/tree';
import {
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
  ViewChild,
  ViewContainerRef,
} from '@angular/core';
import { UntypedFormBuilder } from '@angular/forms';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { DutyArea } from 'src/app/_DtoModels/DutyArea/DutyArea';
import { DutyAreaTreeVM } from 'src/app/_DtoModels/TreeVMs/TaskTreeVM';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { DutyAreaService } from 'src/app/_Services/QTD/duty-area.service';
import { TasksService } from 'src/app/_Services/QTD/tasks.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-fly-panel-task-not-linked',
  templateUrl: './fly-panel-task-not-linked.component.html',
  styleUrls: ['./fly-panel-task-not-linked.component.scss'],
})
export class FlyPanelTaskNotLinkedComponent implements OnInit {
  filterTaskString: string = '';
  @Input() NotLinkedToName: string;
  @Output() closed = new EventEmitter<any>();
  @Input() activeInactivecheck:any;

  showActive: boolean = true;
  dataSource = new MatTreeNestedDataSource<any>();
  tableDataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator) paginator!:MatPaginator;
  @ViewChild(MatSort) sort !:MatSort;
  originalSource = new MatTreeNestedDataSource<any>();
  tasktreeControl = new NestedTreeControl<any>(
    (node: any) => node.children
  );

  displayColumns = ["number","description"];
  hasChild = (_: number, node: any) =>
    !!node.children && node.children.length > 0;

  linkedIds: any[] = [];
  srctaskList: TaskTree[] = [];
  dutyAreas: DutyAreaTreeVM[];
  constructor(
    public flyPanelSrvc: FlyInPanelService,
    private vcf: ViewContainerRef,
    private fb: UntypedFormBuilder,
    public dialog: MatDialog,
    private dutyAreaService: DutyAreaService,
    private taskSrvc: TasksService,
    private alert:SweetAlertService,
    private labelPipe: LabelReplacementPipe
  ) {}

  ngOnInit(): void {


    if(this.activeInactivecheck === true){
      this.getNames();
    }else if(this.activeInactivecheck === false){
      this.getlinkedIds().then(() => {
        this.readyTasksTreeData();
      });
    }
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  } 

  closeFlyPanel() {
    this.flyPanelSrvc.close();
  }

  async readyTasksTreeData() {
    this.spinner = true;
    await this.dutyAreaService.getMinimizedDataForTree().then((res: DutyAreaTreeVM[]) => {

      this.dutyAreas = res;
      this.makeTaskTreeDataSource(res);
    }).finally(()=>{
      this.spinner = false;
    });
  }

  clearFilter(){
    this.filterTaskString = '';
    this.filterData();
  }

  // Make a specialized and simplified data source for the mat tree structure
  makeTaskTreeDataSource(dutyArea: any) {
    // var treeData: any = [];
    debugger;
    let tempArray: DutyAreaTreeVM[] = [
      ...dutyArea.map((da) => {
        return {
          ...da,
          subdutyAreas: da.subdutyAreas.map((sda) => {
            return {
              ...sda,
              tasks: sda.tasks,
            };
          }),
        };
      }),
    ];

    var temp:any[] = [];
    tempArray.forEach((da,i)=>{
      temp.push({
        id:da.id,
        description:da.letter + da.number + " - " + da.title,
        children:[],
        checkbox:true,
        selected:false,
        active:da.active
      });
      da.subdutyAreas.forEach((sda,j)=>{
        temp[i]?.children.push({
          id:sda.id,
          description:da.letter + da.number + "."  + sda.subNumber + " - " + sda.title,
          children:[],
          checkbox:true,
          selected:false,
          active:sda.active,
        });
        sda.tasks.forEach((task)=>{
          temp[i]?.children[j].children.push({
            id:task.id,
            description:da.letter + da.number + '.' + sda.subNumber + '.' + task.number + " - " + task.description,
            checkbox:false,
            selected:false,
            active:task.active
          })
        })
      })
    })

    // for (var data in dutyArea) {
    //   treeData[data] = {
    //     id: dutyArea[data]['id'],
    //     description:dutyArea[data]['letter'] + dutyArea[data]['number'] + ' - ' + dutyArea[data]['title'],
    //     children: dutyArea[data]['subdutyAreas'],
    //     checkbox: true,
    //     selected: false,
    //     active:dutyArea[data]['active'],
    //   };

    //   for (var data1 in treeData[data]['children']) {
    //     var num =  dutyArea[data]['subdutyAreas'][data1]['subNumber'];
    //     treeData[data]['children'][data1] = {
    //       id: dutyArea[data]['subdutyAreas'][data1]['id'],
    //       description:dutyArea[data]['letter'] + dutyArea[data]['number'] + '.' + dutyArea[data]['subdutyAreas'][data1]['subNumber'] + ' - ' + dutyArea[data]['subdutyAreas'][data1]['title'],
    //       children: dutyArea[data]['subdutyAreas'][data1]['tasks'],
    //       checkbox: true,
    //       active:dutyArea[data]['subdutyAreas'][data1]['active'],
    //     };

    //     for (var data2 in treeData[data]['children'][data1]['children']) {

    //       treeData[data]['children'][data1]['children'][data2]['checkbox'] = false;
    //       treeData[data]['children'][data1]['children'][data2]['description'] =dutyArea[data]['letter'] + dutyArea[data]['number'] + '.' + num + '.' + treeData[data]['children'][data1]['children'][data2]['number'] + ' - ' + treeData[data]['children'][data1]['children'][data2]['description'];
    //     }
    //   }
    // }
    this.dataSource.data = temp;
    this.originalSource.data = this.dataSource.data;
    Object.keys(this.dataSource.data).forEach((key: any) => {
      this.setParent(this.dataSource.data[key], undefined);
      this.setParent(this.originalSource.data[key], undefined);
    });

    this.filterActive(true);

  }

  private setParent(node: TaskTree, parent: TaskTree | undefined) {
    node.parent = parent;
    if (node.children) {
      node.children.forEach((childNode) => {
        this.setParent(childNode, node);
      });
    }
  }

  async getlinkedIds() {
    this.spinner = true;

    await this.taskSrvc
      .getLinkedIds(this.NotLinkedToName.toLowerCase())
      .then((res) => {

        this.linkedIds = res;
      }).finally(()=>{
        this.spinner = false;
      });
  }

  filterActive(makeActive: boolean) {
    // let temparr = [
    //   ...this.originalSource.data.map((element) => {
    //     return {
    //       ...element,
    //       children: element.children?.map((e) => {
    //         return {
    //           ...e,
    //           children: e.children?.filter((c) => {
    //             return c.active == makeActive;
    //           }),
    //         };
    //       }),
    //     };
    //   }),
    // ];
    this.showActive = makeActive;
    this.filterData()
    // this.dataSource.data = temparr;
  }

  filterData() {
    this.dataSource.data = [
      ...this.originalSource.data.filter((f) => {
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
                    .includes(String(this.filterTaskString).toLowerCase().trim()) && c.active === this.showActive
                );
              }),
            };
          }),
        };
      }),
    ];

    this.dataSource.data = this.dataSource.data.filter((x) => { return x.children !== null && x.children !== undefined && x.children.length > 0 && (x.children.some((y) => { return y?.children !== null && y?.children !== undefined && y?.children.length > 0 })) })
      Object.keys(this.dataSource.data).forEach((key: any) => {
        this.setParent(this.dataSource.data[key], undefined);
      });
      this.tasktreeControl.dataNodes = this.dataSource.data;
      this.filterTaskString.length > 0 ? this.tasktreeControl.expandAll(): this.tasktreeControl.collapseAll();
  }

  name:any;
  spinner:boolean;
  async getNames(){
    switch(this.NotLinkedToName){
      case 'Active':
        this.name = 'Active '+ await this.transformTitle('Task') + 's';
        this.spinner = true;
        var temp = [];
        this.taskSrvc.getTaskACtiveInactive(this.NotLinkedToName).then((data)=>{
          data.forEach((task,index) => {
            temp.push({
              id:task.id,
              description: task.description,
             number:task.number
            })
          })
          this.tableDataSource.data = temp;
          this.spinner = false;
            setTimeout(()=>{
              this.tableDataSource.sort = this.sort;
              this.tableDataSource.paginator = this.paginator;
            },1)
        }).finally(()=>{
          this.spinner = false;
        })
        break;

      case 'Inactive':
        this.name = 'Inactive '+ + await this.transformTitle('Task') + 's';
        this.spinner = true;
        var temp=[];
        this.taskSrvc.getTaskACtiveInactive(this.NotLinkedToName).then((data)=>{
            data.forEach((task,index) => {
              temp.push({
                id:task.id,
                description: task.description,
                number:task.number,
              })
            })
            this.tableDataSource.data = temp;
            this.spinner = false;
            setTimeout(()=>{
              this.tableDataSource.sort = this.sort;
              this.tableDataSource.paginator = this.paginator;
            },1)
        }).finally(()=>{
          this.spinner = false;
        })
        break;
    }
  }
}

class TaskTree {
  id: any;
  description: string;
  children?: TaskTree[];
  checkbox?: boolean;
  checked?: boolean;
  active?: boolean;
  selected?: boolean;
  indeterminate?: boolean;
  parent?: TaskTree;
}
