import { SelectionModel } from '@angular/cdk/collections';
import { NestedTreeControl } from '@angular/cdk/tree';
import { DatePipe } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Validators } from '@angular/forms';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { ActivatedRoute } from '@angular/router';
import { ILA } from 'src/app/_DtoModels/ILA/ILA';
import { IlaLinkEmployeesOptions } from 'src/app/_DtoModels/ILA/IlaLinkEmployeesOptions';
import { Position } from 'src/app/_DtoModels/Position/Position';
import { TrainingProgram } from 'src/app/_DtoModels/TrainingProgram/TrainingProgram';
import { TrainingProgramType } from 'src/app/_DtoModels/TrainingProgramType/TrainingProgramType';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { IdpService } from 'src/app/_Services/QTD/idp.service';
import { IlaService } from 'src/app/_Services/QTD/ila.service';
import { PositionsService } from 'src/app/_Services/QTD/positions.service';
import { RegulatoryRequirementService } from 'src/app/_Services/QTD/regulatory-requirement.service';
import { SafetyHazardsService } from 'src/app/_Services/QTD/safety-hazards.service';
import { TasksService } from 'src/app/_Services/QTD/tasks.service';
import { TrainingProgramTypeService } from 'src/app/_Services/QTD/training-program-type.service';
import { TrainingProgramsService } from 'src/app/_Services/QTD/training-programs.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-fly-panel-downlod-ilas',
  templateUrl: './fly-panel-downlod-ilas.component.html',
  styleUrls: ['./fly-panel-downlod-ilas.component.scss']
})
export class FlyPanelDownlodIlasComponent implements OnInit {

  trainingProgramList:TrainingProgramType[]=[];
  showVersion:boolean=false;
  showYear:boolean=false;
  positionsList:Position[]=[];
  viewSelected = 'Provider';
  @Input() EmpName:string;
  filterBy: string = 'Provider';
  filterIlaString: string = "";
  linkIlas: boolean = true;
  addIla: boolean = false;
  showActive: boolean = true;
  isILALoading: boolean = false;
  isProvider = true;
  @Output() closed = new EventEmitter<any>();
  linkedIds: any[] = [];
  range: number[] = [];
  idpYearSelected: any;
  parentSelected: number = 0;
  treeControl = new NestedTreeControl<IlaTree>((node: any) => node.children);
  dataSource = new MatTreeNestedDataSource<IlaTree>();
  originalSource = new MatTreeNestedDataSource<IlaTree>();
  hasChild = (_: number, node: IlaTree) =>
    !!node.children && node.children.length > 0;

  IlaCheckListSelection = new SelectionModel<IlaTree>(true);
  showLinkIlaLoader: boolean = false;
  @Input() id: any;
  @Input() alreadyLinkedILAs : any[] = [];
  @Input() yearNow:number;
  @Output() refresh = new EventEmitter<any>();
  selection = new SelectionModel<any>(true, []);
  DataSource =new  MatTableDataSource<ILA>();
  unlinkIds: any[] = [];
  displayColumns: string[] = ['id','num','title'];
  subscription = new SubSink();
  selectedTr!:TrainingProgram[];
  yearsDropdown=[];
  OrignalTr!:TrainingProgram[];
  datepipe = new DatePipe('en-us');
  posId = "";
  typeId = "";
  selectedYear:any;
  selectedVersion:any;
  constructor(
    private route: ActivatedRoute,
    private ilaService: IlaService,
    private rrService: RegulatoryRequirementService,
    private shService: SafetyHazardsService,
    private alert: SweetAlertService,
    private dataBroadcastService: DataBroadcastService,
    private taskService: TasksService,
    private trainingPrgmtypesrvc: TrainingProgramTypeService,
    private trainingProgramsrvc: TrainingProgramsService,
    private positionsrvc: PositionsService,
    private flypanelService: FlyInPanelService,
    private idpService: IdpService,
    private labelPipe:LabelReplacementPipe
  ) { }

  ngOnInit(): void {
    this.getAllPositions();
    this.getyearsdropdown();
  }

  ngAfterViewInit(): void
  {
    this.subscription.sink = this.route.params.subscribe((res: any) => {
      this.id = String(res.id).split('-')[0];
    })
    this.readyIlasTreeData();

    // To refresh ILAs when we link from fly panel.
    this.subscription.sink =
      this.dataBroadcastService.updateProcILALink.subscribe((res: any) => {
        this.getProviderData();
      });
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  async readyIlasTreeData() {
    this.getProviderData();
  }

  getyearsdropdown() {
    var i = 0;
    while (this.range[this.range.length - 1] !== 2005) {
      this.range.push(this.yearNow + 1 - i);
      i++;
    }
    this.idpYearSelected = this.yearNow;
  }

  async getProviderData() {
    this.isILALoading = true;
    this.dataSource.data = [];
    await this.shService
      .getProviderWithILAs()
      .then((res) => {

        this.makeIlaTreeDataSource(res);
      })
      .catch((err: any) => {
        console.error(err);
      })
      .finally(()=>{
        this.isILALoading=false;
      });
  }

  async getTopicData() {
    this.dataSource.data = [];
    this.isILALoading = true;
    await this.rrService
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
      })
      .catch((err: any) => {
        console.error(err);
      });
  }

  makeIlaTreeDataSource(res: any) {
    this.IlaCheckListSelection.clear();
    this.linkedIds = [];
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
            checkbox: !this.alreadyLinkedILAs.includes(element.id),
            active: element.active,
          })
        });
      }
      this.dataSource.data = treeData.sort((a,b) => {return  a.description > b.description ? 1 : -1});
      this.originalSource.data = treeData;
      Object.keys(this.dataSource.data).forEach((key: any) => {
        this.setParent(this.dataSource.data[key], undefined);
        this.setParent(this.originalSource.data[key], undefined);
      });
      this.filterActive(true);


      this.isILALoading = false;
    }
  }

  trainingProgramSelectionChanged(event:any) {
    this.DataSource.data=[];
    this.selection.clear();
    this.typeId = event.value.id;
    this.selectedYear=null;
    this.selectedVersion=null;
    if (event.value.trainingProgramTypeTitle==="Initial Training Program") {
      this.selectedTr=this.OrignalTr.filter((x)=>x.trainingProgramTypeId ===this.typeId);
      this.showVersion=true;
      this.showYear=false;
    }else{
      this.selectedTr=this.OrignalTr.filter((x)=>x.trainingProgramTypeId===this.typeId);
      this.showVersion=false;
      this.yearsDropdown = [...new Set(this.selectedTr.map(x => (new Date(x.year)).getFullYear()))];
      this.showYear=true;
    }
  }

  VersionSelected(event:any){

    this.DataSource.data=[];
    this.selection.clear();
    var filtered=this.selectedTr.filter((x)=>x.id===event.value);
    var ilaList:ILA[]=[];
    var link:any[]= filtered.map((x)=>{
      return x.trainingProgram_ILA_Links;
    });
    link[0].forEach((element) => {
      if(!ilaList.includes(element.ila) && element.ila!==null){
        ilaList.push(element.ila);
      }
    });
    this.DataSource =new MatTableDataSource(ilaList);

  }

  YearSelected(event:any){
    this.DataSource.data=[];
    this.selection.clear();
    var filtered=this.selectedTr.filter((x)=>new Date(x.year).getFullYear()==event.value);
    var ilaList:ILA[]=[];
    var link:any[]= filtered.map((x)=>{
      return x.trainingProgram_ILA_Links;
    });
    link[0].forEach((element) => {
      if(!ilaList.includes(element.ila) && element.ila!==null){
        ilaList.push(element.ila);
      }
    });
    this.DataSource= new MatTableDataSource(ilaList);

  }


  filterActive(makeActive: boolean) {
    this.showActive = makeActive;
    this.filterData("", "");
  }

  onIlaChange(event: any, node: any) {
    if (event.checked) {
      this.IlaCheckListSelection.select(node);
    } else {
      this.IlaCheckListSelection.deselect(node);
    }
    this.itemToggle(event.checked, node);
  }
  filtered(node: any) {
    return node.description.includes(this.filterIlaString);
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
    Object.keys(this.dataSource.data).forEach((key: any) => {
      this.setParent(this.dataSource.data[key], undefined);
    });

    this.dataSource.data.forEach((node) => {
      node.children?.forEach((ila) => {
        this.checkAllParents(ila);
      });
    });

    this.treeControl.dataNodes = this.dataSource.data;
    this.filterIlaString.length > 0 ? this.treeControl.expandAll() : this.treeControl.collapseAll();
  }

  itemToggle(checked: boolean, node: IlaTree) {
    node.selected = node.checkbox ? checked : false;
    if (node.children !== undefined) {
      node.children.forEach((child) => {
        this.itemToggle(checked, child);
      });
    } else {
      // Add TaskIds to list
      if (node.selected && node.checkbox) {
        this.linkedIds.push(node.id);
      }
      else {
        var index = this.linkedIds.indexOf(node.id);
        if (index > -1) {
          this.linkedIds.splice(index, 1);
        }
      }
    }
    this.linkedIds = [...new Set(this.linkedIds)];
    this.checkAllParents(node);
  }

  private setParent(node: IlaTree, parent: IlaTree | undefined) {
    node.parent = parent;
    if (node.children) {
      node.children.forEach((childNode) => {
        this.setParent(childNode, node);
      });
    }
  }

  private checkAllParents(node: IlaTree) {
    if (node.parent) {
      const descendants = this.treeControl.getDescendants(node.parent);
      node.parent.selected = descendants.every((child) => (child.checkbox && child.selected));
      node.parent.indeterminate = descendants.some((child) => (child.checkbox && child.selected));
      this.checkAllParents(node.parent);
    }
  }
  refreshILAsTree() {
    this.IlaCheckListSelection.clear();
    this.linkedIds = [];
    this.readyIlasTreeData();
  }

  addILA() {
    this.linkedIds = [];
  }

  linkILA() {

    this.showLinkIlaLoader = true;
    var options = new IlaLinkEmployeesOptions();
    options.iLAIds = this.linkedIds;
    options.idpYear = this.idpYearSelected;

    this.idpService.linkILA(this.id, options).then(async (_) => {
      this.alert.successToast("Linked Selected " + await this.labelPipe.transform('ILA') + "s to " + await this.labelPipe.transform('Employee') + " Successfully.");
      this.refresh.emit();
      this.flypanelService.close();

    }).finally(() => {
      this.showLinkIlaLoader = false;
    })
  }

  IDPYearSelected(event: any) {
    this.idpYearSelected = event.value;
  }

  linkILAs() {
    this.showLinkIlaLoader = true;
    var options = new IlaLinkEmployeesOptions();
    options.iLAIds = this.selection.selected.map((x)=>{return x.id});
    options.idpYear = this.idpYearSelected;
    this.idpService.linkILA(this.id, options).then(async (_) => {
      this.alert.successToast("Linked Selected " + await this.labelPipe.transform('ILA') +"s to " + await this.labelPipe.transform('Employee') + " Successfully.");
      this.refresh.emit();
      this.flypanelService.close();

    }).finally(() => {
      this.showLinkIlaLoader = false;
    })
  }
  ToggleILAGroupBy(groupBy: string) {
    switch (groupBy) {
      case 'provider':
        //this.getILAsGroupByMeta();
        break;
      case 'trainingProgram':
       // this.getTopics();
        break;
      case 'provider':
      default:
        //this.getProviders();
        break;
    }

    this.viewSelected = groupBy;
  }
  getTrainingProgramTypes(event:any)
  {
    this.DataSource.data=[];
    this.selection.clear();
    this.selectedYear=null;
    this.selectedVersion=null;

      this.trainingPrgmtypesrvc.getAll().then((res) => {
        this.trainingProgramList = res;
        this.selectedTr = event.value.trainingPrograms;
        this.OrignalTr = event.value.trainingPrograms;
        // this.trainingProgramType = 1;
        // if(this.temp)
        // {
        //   this.chnagetrainingProgramType(this.temp)
        // }
        //this.filteredList = res;
      });
  }
  chnagetrainingProgramType(event: any)
  {

    // var title = this.trainingProgramList.filter((data)=> data.id === event.value)[0].trainingProgramTypeTitle
    //
    // if(title.toLowerCase() === 'initial training program')
    // {
    //   this.trainingProgramType = 1
    //   this.step1Form.controls['year']?.clearValidators();
    //   this.step1Form.controls['year']?.updateValueAndValidity();
    //   this.step1Form.controls['title']?.clearValidators();
    //   this.step1Form.controls['title']?.updateValueAndValidity();
    //   this.step1Form.controls['version']?.setValidators(Validators.required);
    //   this.step1Form.controls['version']?.updateValueAndValidity();
    //   this.step1Form.controls['startdate']?.setValidators(Validators.required);
    //   this.step1Form.controls['startdate']?.updateValueAndValidity();



    // }
    // else if(title.toLowerCase() === 'continuing training program')
    // {
    //     this.step1Form.controls['version']?.clearValidators();
    //     this.step1Form.controls['version']?.updateValueAndValidity();
    //     this.step1Form.controls['title']?.clearValidators();
    //     this.step1Form.controls['title']?.updateValueAndValidity();
    //     this.step1Form.controls['startdate']?.clearValidators();
    //     this.step1Form.controls['startdate']?.updateValueAndValidity();
    //     this.step1Form.controls['year']?.setValidators(Validators.required);
    //     this.step1Form.controls['year']?.updateValueAndValidity();
    //   this.trainingProgramType = 2
    // }
    // else if(title.toLowerCase() === 'cycle training program')
    // {
    //   this.trainingProgramType = 3
    //   this.step1Form.controls['version']?.clearValidators();
    //   this.step1Form.controls['version']?.updateValueAndValidity();
    //   this.step1Form.controls['year']?.clearValidators();
    //   this.step1Form.controls['year']?.updateValueAndValidity();
    //   this.step1Form.controls['title']?.setValidators(Validators.required);
    //   this.step1Form.controls['title']?.updateValueAndValidity();
    //   this.step1Form.controls['startdate']?.setValidators(Validators.required);
    //   this.step1Form.controls['startdate']?.updateValueAndValidity();

    // }
  }
  getAllPositions()
  {
    this.positionsrvc.getActiveAsync().then((res) => {


      this.positionsList = res;

  });
  }

  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.DataSource.data.length;
    return numSelected === numRows;
  }
  selectrow(row: any) {
    this.selection.toggle(row);
    this.unlinkIds = [];
    this.selection.selected.forEach((v, i) => {
      this.unlinkIds.push(v.id);
    });
  }

  masterToggle() {
    this.isAllSelected()
      ? this.selection.clear()
      : this.DataSource.data.forEach((row) => this.selection.select(row));

    this.unlinkIds = [];
    this.selection.selected.forEach((v, i) => {
      this.unlinkIds.push(v.id);
    });
  }
  closeFlyPanel()
  {
  this.flypanelService.close();
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
