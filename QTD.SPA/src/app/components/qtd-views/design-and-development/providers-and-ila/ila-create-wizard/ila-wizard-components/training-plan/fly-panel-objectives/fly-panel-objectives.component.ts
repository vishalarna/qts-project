import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { NestedTreeControl } from '@angular/cdk/tree';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { EnablingObjectivesCategoryService } from 'src/app/_Services/QTD/enabling-objectives-category.service';
import { EnablingObjective_Category } from 'src/app/_DtoModels/EnablingObjective_Category/EnablingObjective_Category';
import { EnablingObjective_SubCategory } from 'src/app/_DtoModels/EnablingObjective_SubCategory/EnablingObjective_SubCategory';
import { EnablingObjective_Topic } from 'src/app/_DtoModels/EnablingObjective_Topic/EnablingObjective_Topic';
import {
  UntypedFormBuilder,
  UntypedFormControl,
  UntypedFormGroup,
  Validators,
} from '@angular/forms';
import { CustomEnablingObjectiveService } from 'src/app/_Services/QTD/custom-enabling-objective.service';
import { CustomEnablingObjective } from 'src/app/_DtoModels/CustomEnablingObjective/CustomEnablingObjective';
import { CustomEnablingObjectiveCreateOption } from 'src/app/_DtoModels/CustomEnablingObjective/CustomEnablingObjectiveOption';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { EnablingObjectivesService } from 'src/app/_Services/QTD/enabling-objectives.service';
import { EnablingObjectiveOptions } from 'src/app/_DtoModels/EnablingObjective/EnablingObjectiveOption';
import { EnablingObjectiveCreateOptions } from 'src/app/_DtoModels/EnablingObjective/EnablingObjectiveCreateOptions';
import { IlaService } from 'src/app/_Services/QTD/ila.service';
import { ILACustomObjective_LinkOptions } from 'src/app/_DtoModels/ILACustomObjective_Link/ILACustomObjective_LinkOptions';
import { select, Store } from '@ngrx/store';
import { ILA_EnablingObjective_LinkOptions } from 'src/app/_DtoModels/ILA_EnablingObjective_Link/ILA_EnablingObjective_LinkOptions';
import { DutyAreaService } from 'src/app/_Services/QTD/duty-area.service';
import { DutyArea } from 'src/app/_DtoModels/DutyArea/DutyArea';
import { SubdutyArea } from 'src/app/_DtoModels/SubdutyArea/SubdutyArea';
import { SubSink } from 'subsink';
import { SelectionModel } from '@angular/cdk/collections';
import { ILATaskObjectiveLinkOption } from 'src/app/_DtoModels/ILA_TaskObjective_Link/ILA_TaskObjective_LinkOptions';
import { skip } from 'rxjs/operators';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { EnablingObjectivesTopicService } from 'src/app/_Services/QTD/enabling-objectives-topic.service';
import { EnablingObjective } from 'src/app/_DtoModels/EnablingObjective/EnablingObjective';
import { CustomEnablingObjectiveUpdateOption } from '@models/CustomEnablingObjective/CustomEnablingObjectiveUpdateOption';
import { ILATaskObjectiveLinkUpdateOptions } from '@models/ILA_TaskObjective_Link/ILATaskObjectiveLinkUpdateOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { event } from 'jquery';
@Component({
  selector: 'app-fly-panel-objectives',
  templateUrl: './fly-panel-objectives.component.html',
  styleUrls: ['./fly-panel-objectives.component.scss'],
})
export class FlyPanelObjectivesComponent implements OnInit, OnDestroy {
  active_tasks: boolean = true;
  @Input() isCustomEO: boolean = false;
  @Input() linkedTaskIds: any[] = [];
  @Input() linkedEOIds: any[] = [];
  @Input() mode:string ='';
  @Input() eoDetails:any;
  public customEOs: string[] = ['co', 'co', 'co'];
  @Output() closed = new EventEmitter<Event>();
  @Output() linked = new EventEmitter<any>();
  treeControl = new NestedTreeControl<any>((node: any) => node.children);
  dataSource = new MatTreeNestedDataSource<any>();
  originalTaskDataSource = new MatTreeNestedDataSource<any>();
  EOTreeControl = new NestedTreeControl<EOTreeNew>((node: any) => node.children);
  enablingObjectiveDataSource = new MatTreeNestedDataSource<EOTreeNew>();
  originalSource = new MatTreeNestedDataSource<EOTreeNew>();
  hasChild = (_: number, node: EOTreeNew) =>
    !!node.children && node.children.length > 0;

  hasChildTask = (_: number, node: any) =>
    !!node.children && node.children.length > 0;
  ILAID: any;
  eo_catogories: EnablingObjective_Category[] = [];
  eo_subCategories: EnablingObjective_SubCategory[] = [];
  eo_topics: EnablingObjective_Topic[] = [];
  selectedEO_Cat: any;
  selectedEO: any;
  processing: boolean = false;
  createCustomEOForm!: UntypedFormGroup;
  subscriptions: SubSink = new SubSink();
  showLoader = false;
  showLinkTaskLoader = false;
  showEOLinkLoader = false;
  taskCheckListSelection = new SelectionModel<EOTreeNew>(true);
  EOCheckListSelection = new SelectionModel<EOTreeNew>(true);
  taskIds: any = [];
  eOIds: any = [];
  selectedTaskFilter: string;
  filterTaskString: string;
  addTask: boolean = false;
  addTaskFlyin: boolean = false;
  controlCheck: boolean = true;
  addEO: boolean = false;
  includeEos:boolean=false;
  includeProcedures:boolean=false;
  spinner:boolean=false;
  isIncludeMetaTask:boolean = false;
  isIncludeMetaEO:boolean = false;
  constructor(
    public flyPanelSrvc: FlyInPanelService,
    public _eoCatSrvc: EnablingObjectivesCategoryService,
    public _eoSrvc: EnablingObjectivesService,
    public eoTopicService: EnablingObjectivesTopicService,
    public fb: UntypedFormBuilder,
    public customEOSrvc: CustomEnablingObjectiveService,
    public ilaService: IlaService,
    public alertSrvc: SweetAlertService,
    private saveStore: Store<{ saveIla: any }>,
    private dutyAreaService: DutyAreaService,
    private dataBroadCastService: DataBroadcastService,
    private labelPipe: LabelReplacementPipe,
  ) { }

  ngOnInit(): void {
    this.subscriptions.sink = this.saveStore.pipe(select('saveIla')).subscribe((res) => {
      if ((res?.saveData?.result !== undefined)) {
        this.ILAID = res['saveData']?.result?.id === undefined ? this.ILAID : res['saveData']?.result?.id;
      }
    });
    if (this.isCustomEO) {
      this.readyCustomEOForm();
      this.getEnbalingObjectiveCategories();
    }
    if(this.mode == "edit"){
      this.populateFormForEdit(); 
    }
    this.readyTasksTreeData();
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }

  private async populateFormForEdit() {
    if(this.eoDetails?.type === "Custom"){
      this.selectedEO = await this.customEOSrvc.get(this.eoDetails?.id);
    }
     if(this.selectedEO?.eO_CatId && this.selectedEO?.eO_SubCatId){
      await this.getEO_SubCategories(this.selectedEO?.eO_CatId);
      if(this.selectedEO?.eO_TopicId){
        await this.getEO_Topics(this.selectedEO?.eO_SubCatId );
      }
    }
    else{
      this.eo_subCategories = [];
      this.eo_topics = [];
    }
    this.initializeFormValues();
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }   

  initializeFormValues(){
    const addToEo = (this.eoDetails?.type === "Custom") ? false : true;
      this.createCustomEOForm.patchValue({
        eo_catId: this.selectedEO?.eO_CatId,
        eo_subCatId: this.selectedEO?.eO_SubCatId,
        eo_topicId: this.selectedEO?.eO_TopicId,
        description: this.selectedEO?.description,
        isAddToEO: addToEo
      });
      this.createCustomEOForm.get('isAddToEO').disable();
  }

  onChange(event: any) {
    if (this.enablingObjectiveDataSource.data.length === 0 && event.index === 1) {
      this.showLoader = true;
      this.readyEnablingObjectiveTreeData();
    }
  }

  // material check box selection change detectors
  onEOChange(event: any, node: any) {
    if (event.checked) {
      this.EOCheckListSelection.select(node);
    }
    else {
      this.EOCheckListSelection.deselect(node);
    }
  }

  onTaskChange(event: any, node: any) {
    if (event.checked) {
      this.taskCheckListSelection.select(node);
    }
    else {
      this.taskCheckListSelection.deselect(node);
    }
  }

  onIncludeMetaTaskChange(event: any){
    this.isIncludeMetaTask = event.checked;
  }
  onIncludeMetaEOChange(event: any){
    this.isIncludeMetaEO = event.checked;
  }

  async readyTasksTreeData() {
    await this.dutyAreaService.getWithSubdutyAreas().then((res: DutyArea[]) => {
      this.makeTaskTreeDataSource(res);
    }).catch((err: any) => {
      console.error(err);
    })
  }

  async readyEnablingObjectiveTreeData() {
    await this._eoSrvc.getAll().then((res: EnablingObjective[]) => {
      this.readyEOTreeData(res);
      this.showLoader = false;
    });
  }

  notTopicEOs = 0;
  // Modifying The Enabling Objectives data received to be used without changes to the UI (Trying to make this less messy)
  readyEOTreeData(res: any[]) {
    
    this.notTopicEOs = 0;
    if (res.length === 0) {
      this.enablingObjectiveDataSource.data = [];
    } else {
      var treeData: EOTreeNew[] = [];
      res.forEach((cat, i) => {
        treeData.push({
          children: [],
          description: cat['number'] + ". " + cat['title'],
          id: cat.id,
          IsEO:false,
        })
        cat['enablingObjective_SubCategories'].forEach((subCat, j) => {
          treeData[i].children?.push({
            children: [],
            description: `${cat['number']}.${subCat['number']} ` + subCat['title'],
            id: subCat.id,
            IsEO:false,
          });
          subCat['enablingObjectives'].forEach((eo) => {
            treeData[i].children[j].children?.push({
              children: [],
              description: `${eo['number']} ${eo['description']}`,
              id: eo.id,
              active: eo['active'],
              checkbox: !this.linkedEOIds.includes(eo.id),
              IsEO: true,
              isMeta: eo['isMetaEO'],
              isSkillQualification: eo['isSkillQualification'],
            })
            this.notTopicEOs++;
          });
          subCat['enablingObjective_Topics'].forEach((topic, k) => {
            treeData[i]?.children[j]?.children?.push({
              children: [],
              description: `${cat['number']}.${subCat['number']}.${topic['number']} ${topic['title']}`,
              id: topic.id,
              IsEO:false,
            });
            topic['enablingObjectives'].forEach((eo, l) => {
                treeData[i].children[j]?.children[k + this.notTopicEOs]?.children?.push({
                children: [],
                description: `${eo['number']} ${eo['description']}`,
                active: eo['active'],
                id: eo['id'],
                checkbox: !this.linkedEOIds.includes(eo.id),
                IsEO: true,
                isMeta: eo['isMetaEO'],
                isSkillQualification: eo['isSkillQualification'],
              });
            });
          });
          this.notTopicEOs = 0;
        });
      })

      this.EOTreeControl.dataNodes = Object.assign(treeData);
      this.enablingObjectiveDataSource.data = Object.assign([], treeData);
      this.originalSource.data = Object.assign([], treeData);
      Object.keys(this.enablingObjectiveDataSource.data).forEach((key: any) => {
        this.setParent(this.enablingObjectiveDataSource.data[key], undefined);
        this.setParent(this.originalSource.data[key], undefined);
      });
      
      
      this.filterDataNew(true);
      
    }
    //this.enablingObjectiveDataSource.data = treeDataEO;
  }

  modifyAlreadyLinkedEOs(){
    this.originalSource.data.forEach((cat,i)=>{
      cat.children.forEach((subcat,j) => {
        subcat.children.forEach((topic,k)=>{
          if(topic.IsEO){
            this.enablingObjectiveDataSource.data[i].children[j].children[k].checkbox = !this.linkedEOIds.includes(this.enablingObjectiveDataSource.data[i].children[j].children[k]?.id);
          }
          else{
            
            topic.children.forEach((eo,l)=>{
              this.enablingObjectiveDataSource.data[i].children[j].children[k].children[l].checkbox = !this.linkedEOIds.includes(this.enablingObjectiveDataSource.data[i].children[j].children[k].children[l]?.id);
            });
          }
        });
      });
    });
  }

  private setParent(node: EOTreeNew, parent: EOTreeNew | undefined) {
    node.parent = parent;
    if (node.children) {
      node.children.forEach((childNode) => {
        this.setParent(childNode, node);
      });
    }
  }

  filterEOString = "";
  filterDataNew(filterActive: boolean) {
    //this.showActive = filterActive;
    let temparr = [
      ...this.originalSource.data.map((element) => {
        return {
          ...element,
          children: element.children?.map((e) => {
            return {
              ...e,
              children: e.children?.map((c) => {
                if (c.IsEO) {
                  if (c.description.toLowerCase().match(String(this.filterEOString).toLowerCase()) && c.active === true) {
                    return {
                      ...c,
                      selected: this.EOCheckListSelection.selected.map((x) => { return x.id }).includes(c.id),
                      children: [],
                    }
                  }
                  else {
                    return {
                      description: "",
                      children: [],
                      id: "",
                      IsEO: true,
                    }
                  }
                }
                else {
                  return {
                    ...c,
                    selected: this.EOCheckListSelection.selected.map((x) => { return x.id }).includes(c.id),
                    children: c.children?.filter((f) => {
                      return f.description.toLowerCase().match(String(this.filterEOString).toLowerCase()) && f.active === true;
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
    this.enablingObjectiveDataSource.data = Object.assign(temparr);
    this.EOTreeControl.dataNodes = Object.assign(temparr);
    Object.keys(this.enablingObjectiveDataSource.data).forEach((key: any) => {
      this.setParent(this.enablingObjectiveDataSource.data[key], undefined);
    });

    this.enablingObjectiveDataSource.data.forEach((cat) => {
      cat.children?.forEach((subCat) => {
        subCat.children?.forEach((topic) => {
          if (topic.IsEO) {
            this.EOCheckListSelection.selected.map((x) => x.id ).includes(topic.id) ? topic.selected = true : "";
            this.checkAllParents(topic);
          }
          topic.children?.forEach((eo) => {
            this.checkAllParents(eo);
          });
        });
      });
    });
    
    this.filterEOString.length > 0 ? this.EOTreeControl.expandAll() : this.EOTreeControl.collapseAll();
  }

  private checkAllParents(node: EOTreeNew) {
    if (node.parent) {
      const descendants = this.EOTreeControl.getDescendants(node.parent);
      node.parent.selected = descendants.every((child) => child.selected);
      node.parent.indeterminate = descendants.some((child) => child.selected);
      this.checkAllParents(node.parent);
    }
  }

  // Will try to use this function instead of nested loops for building tree data

  // buildFileTree(obj: any, level: number): Tasks[] {
  //   return Object.keys(obj).reduce<Tasks[]>((accumulator, key) => {
  //     const value = obj[key];
  //     // 
  //     // 
  //     const node = new Tasks();
  //     if (key === 'description') {
  //       //
  //       node.description = obj[key];
  //     }

  //     if (value != null) {
  //       if (typeof value === 'object') {
  //         node.children = this.buildFileTree(value, level + 1);
  //       } else {
  //         if (key === 'description') {
  //           //
  //           node.description = value;
  //         }
  //       }
  //     }

  //     
  //     if (node.description === undefined) {
  //       return accumulator.concat([]);
  //     }
  //     return accumulator.concat(node);
  //   }, []);
  // }
  itemToggle(checked: boolean, node: EOTreeNew) {
    node.selected = node.checkbox ? checked : false;
    if (node.children !== undefined) {
      node.children.forEach((child) => {
        this.itemToggle(checked, child);
      });
    } else {
      // Add TaskIds to list
      if (!node.checkbox) {
        if (checked) {
          node.selected=true;
          this.taskCheckListSelection.select(node);
        }
        else {
          node.selected=false;
          this.taskCheckListSelection.deselect(node);
        }
      }
    }
    //this.taskCheckListSelection = [...new Set(this.linkedIds)];
    this.checkAllParents(node);
  }

  itemToggle2(checked: boolean, node: EOTreeNew) {
    
    node.selected = node.checkbox ? checked : false;
    if (node.children !== undefined && node.children?.length>0) {
      node.children.forEach((child) => {
        this.itemToggle2(checked, child);
      });
    } else {
        if (node.checkbox) {
        if (checked) {
          node.selected=true;
          this.EOCheckListSelection.select(node);
        }
        else {
          node.selected=false;
          this.EOCheckListSelection.deselect(node);
        }
      }
    }
    //this.taskCheckListSelection = [...new Set(this.linkedIds)];
    this.checkAllParents(node);
  }

  async linkTaskObjectives() {
    this.showLinkTaskLoader = true;
    var selectedTasks: any = this.taskCheckListSelection.selected;
    this.taskIds = selectedTasks.map((data) => { return data.id});
    var totalLinkedIds = this.linkedTaskIds.concat(this.taskIds);
    totalLinkedIds = Array.from(new Set(totalLinkedIds));
    var taskLinkUpdateOptions = new ILATaskObjectiveLinkUpdateOptions();
    taskLinkUpdateOptions.taskLinks =totalLinkedIds.map((taskId, index) => ({ taskId,sequence: 0}));
    taskLinkUpdateOptions.isIncludeEos=this.includeEos;
    taskLinkUpdateOptions.isIncludeProcedures=this.includeProcedures;
    taskLinkUpdateOptions.isIncludeMetaTask = this.isIncludeMetaTask;
    await this.ilaService.updateILATaskObjectiveLinksAsync(this.ILAID,taskLinkUpdateOptions).then(async res=>{
      this.alertSrvc.successToast(await this.transformTitle('Task')+" s Successfully Linked");
        this.linked.emit('TO');
        this.dataBroadCastService.refreshTaskLinks.next(null);
        this.showLinkTaskLoader = false;
        this.taskCheckListSelection.clear();
        this.linkedTaskIds= totalLinkedIds;
        this.modifyAlreadyLinkedTaskData();
    }).finally(()=>{
        this.showLinkTaskLoader = false;
      });
  }

  async linkEnablingObjectives() {
    this.showEOLinkLoader = true;
    var selectedEO: any = this.EOCheckListSelection.selected;
    var linkOptions = new ILA_EnablingObjective_LinkOptions();
    // keep track of previous ids in case we want to unlink too.
    var previousEOIds: any = this.eOIds;
    this.eOIds = [];
    for (var data in selectedEO) {
      this.eOIds.push(selectedEO[data]['id']);
    }
    this.eOIds = this.eOIds.filter((x: any) => !previousEOIds.includes(x));
    linkOptions.ilaid = this.ILAID;
    linkOptions.enablingObjectiveIds = Object.assign(this.eOIds);
    linkOptions.isIncludeMetaEO = this.isIncludeMetaEO;
    await this.ilaService.linkEnablingObjective(this.ILAID, linkOptions).then(async (res: any) => {
      this.alertSrvc.successToast("Selected " + await this.transformTitle('Enabling Objective') + "s Linked Successfully");
      this.EOCheckListSelection.clear();
      this.linked.emit('EO');
      this.showEOLinkLoader = false;
      this.linkedEOIds = this.linkedEOIds.concat(this.eOIds);
      this.modifyAlreadyLinkedEOs();
    }).finally(()=>{
      this.showEOLinkLoader = false;
    })
  }



  // Make a specialized and simplified data source for the mat tree structure
  makeTaskTreeDataSource(res: DutyArea[]) {
    var treeData: any = [];
    res.forEach((da, i) => {
      treeData.push({
        description:da.letter+da.number+"."+ da.title,
        children: [],
        checkbox: true,
        id:da.id,
      });
      da.subdutyAreas.forEach((sda, j) => {
        treeData[i].children.push(({
          description:  da.letter +' ' +da.number +'.' +sda.subNumber +' ' +sda.title,
          checkbox: true,
          children: [],
          id:sda.id,
        }));
        sda.tasks.forEach((task) => {
          if(task.active){ // Should be changed if we ever allow linking inactive Tasks
            treeData[i].children[j].children.push({
              description: da.letter + da.number + "." + sda.subNumber + "." + task.number + " - " + task.description,
              number: da.letter + da.number + "." + sda.subNumber + "." + task.number,
              checkbox: this.linkedTaskIds.includes(task.id),
              active:task.active,
              id:task.id,
              isMeta: task.isMeta,
            })
          }
        })
      })
    });

    
    // for (var data in res) {
    //   
    //   treeData[data] = { description: res[data]['title'], children: res[data]['subdutyAreas'], checkbox: true };
    //   
    //   for (var data1 in treeData[data]['children']) {
    //     treeData[data]['children'][data1] = { description: res[data]['subdutyAreas'][data1]['title'], children: res[data]['subdutyAreas'][data1]['tasks'], checkbox: true };
    //   }
    // }

    this.dataSource.data = Object.assign(treeData);
    this.originalTaskDataSource.data = Object.assign(treeData);
    this.treeControl.dataNodes = this.dataSource.data;
  }

  modifyAlreadyLinkedTaskData(){
    this.originalTaskDataSource.data.forEach((da,i)=>{
      da.children.forEach((sda,j)=>{
        sda.children.forEach((task,k)=>{
          this.dataSource.data[i].children[j].children[k].checkbox = this.linkedTaskIds.includes(this.dataSource.data[i].children[j].children[k]?.id);
        })
      })
    })
  }

  readyCustomEOForm() {
    this.createCustomEOForm = this.fb.group({
      eo_catId: new UntypedFormControl(null),
      eo_subCatId: new UntypedFormControl(null),
      eo_topicId: new UntypedFormControl(null),
      description: new UntypedFormControl('',Validators.compose([Validators.required, Validators.maxLength(400)])),
      isAddToEO: new UntypedFormControl(false),
    });
  }

  changeValidations(event:any){
    this.onChangeValidations(event.checked);
  }
   
  onChangeValidations(checked: boolean){
    if(checked){
      this.createCustomEOForm.get('eo_subCatId')?.setValidators([Validators.required]);
      this.createCustomEOForm.get('eo_subCatId')?.value == null ? this.createCustomEOForm.get('eo_subCatId')?.setErrors({required:true}):this.createCustomEOForm.get('eo_subCatId')?.setErrors(null);
      // this.createCustomEOForm.get('eo_topicId')?.setValidators([Validators.required]);
      // this.createCustomEOForm.get('eo_topicId')?.value === null ? this.createCustomEOForm.get('eo_topicId')?.setErrors({required:true}):this.createCustomEOForm.get('eo_topicId')?.setErrors(null);
    }
    else{
      this.createCustomEOForm.get('eo_subCatId')?.clearValidators();
      this.createCustomEOForm.get('eo_subCatId')?.setErrors(null);
      // this.createCustomEOForm.get('eo_topicId')?.clearValidators();
      // this.createCustomEOForm.get('eo_topicId')?.setErrors(null);
    }
    this.createCustomEOForm.updateValueAndValidity();
  }

  customEOField() {
    this.customEOs.push('co');
  }

  /* onClickActive(){
    this.active_tasks=!this.active_tasks
  } */

    async getEnbalingObjectiveCategories() {
        this.spinner = true;
    await this._eoCatSrvc.getAllSimplifiedCategories().then((res) => {
      this.eo_catogories = res;
    }).finally(() => {
      this.spinner=false;
    });
  }

  spinnerSubCat:boolean=false;

    async getEO_SubCategories(eoCatId: any) {
    this.spinnerSubCat = true;
    await this._eoCatSrvc.getAllSimplifiedSubCategories(eoCatId).then((res) => {
      this.eo_subCategories = res;
      this.selectedEO_Cat = eoCatId;
      this.createCustomEOForm.patchValue({ eo_subCatId: null });
    }).finally(() => {
      this.spinnerSubCat=false;
    });;
  }

  async getEO_Topics(eoSubCatId: any) {
    await this.eoTopicService.getAllSimplifiedTopics(this.selectedEO_Cat, eoSubCatId).then((res) => {
      this.eo_topics = res;
      this.createCustomEOForm.patchValue({ eo_topicId: null });
    });
  }

  createEnablingObjective() {
    this.processing = true;
    if (this.createCustomEOForm.valid) {
      if (this.createCustomEOForm.get('isAddToEO')?.value === true) {
        
        let options: EnablingObjectiveCreateOptions = new EnablingObjectiveCreateOptions();
        options.topicId = this.createCustomEOForm.get('eo_topicId')?.value,
          options.statement = this.createCustomEOForm.get('description')?.value,
          options.number = 0,
          options.categoryId = this.createCustomEOForm.get('eo_catId')?.value;
        options.subCategoryId = this.createCustomEOForm.get('eo_subCatId')?.value;
        this._eoSrvc
          .createFromILA(options)
          .then((res) => {
            if (res) { 
              let linkoptions: ILA_EnablingObjective_LinkOptions = {
                ilaid: this.ILAID,
                enablingObjectiveIds: [res.id],
              };
              this.processing = true;
              this.ilaService
                .linkEnablingObjective(this.ILAID, linkoptions)
                .then(async (res) => {
                  this.alertSrvc.successToast(
                    await this.transformTitle('Enabling Objective') + ' Created and linked with ' + await this.labelPipe.transform('ILA')
                  );
                  this.onChangeValidations(false);
                  this.createCustomEOForm.reset();
                  this.eo_subCategories = [];
                  this.eo_topics = [];
                  this.selectedEO_Cat = null;
                  this.linked.emit('EO');
                })
                .finally(() => {
                  this.processing = false;
                });
            }
          }).finally(() => {
            this.processing = false;
          });
      } else {
        let options1: CustomEnablingObjectiveCreateOption = {
          eO_TopicId: this.createCustomEOForm.get('eo_topicId')?.value,
          eO_CatId: this.createCustomEOForm.get('eo_catId')?.value,
          eO_SubCatId: this.createCustomEOForm.get('eo_subCatId')?.value,
          description: this.createCustomEOForm.get('description')?.value,
          isAddtoEO: false
        };
        //  
        //this.processing = true;
        this.customEOSrvc
          .create(options1)
          .then((res) => {
            if (res) {
              this.processing = true;
              let linkoptions: ILACustomObjective_LinkOptions = new ILACustomObjective_LinkOptions();
              linkoptions.customObjIds = [];
              linkoptions.ilaId = this.ILAID;
              linkoptions.customObjIds.push(res.id);
              this.ilaService
                .linkCustomObjective(this.ILAID, linkoptions)
                .then(async (res) => {
                  this.alertSrvc.successToast(
                    'Custom ' + await this.transformTitle('Enabling Objective') + ' Created and Linked'
                  );
                  this.createCustomEOForm.reset();
                  
                  this.eo_subCategories = [];
                  this.eo_topics = [];
                  this.selectedEO_Cat = null;
                  this.linked.emit('CO');
                })
                .finally(() => {
                  this.processing = false;
                });
            }
          })
          .finally(() => {
            this.processing = false;
          });
      }
      this.closed.emit();
    }
  }

  updateEnablingObjective() {
    if (this.createCustomEOForm.valid) {
      if (this.createCustomEOForm.get('isAddToEO')?.value === false) {
        let options1: CustomEnablingObjectiveUpdateOption = {
          eo_TopicId: this.createCustomEOForm.get('eo_topicId')?.value,
          description: this.createCustomEOForm.get('description')?.value,
          isAddtoEO: this.createCustomEOForm.get('isAddToEO')?.value,
          eo_CatId: this.createCustomEOForm.get('eo_catId')?.value,
          eo_SubCatId: this.createCustomEOForm.get('eo_subCatId')?.value,
        };
        this.customEOSrvc
          .update(this.eoDetails?.id,options1)
          .then(async (res) => {
              this.alertSrvc.successToast('Custom ' + await this.transformTitle('Enabling Objective') + ' Updated Successfully');
              this.createCustomEOForm.reset();
              this.linked.emit('CO');
              this.closed.emit();
          })
      }
   
   }
  }

  filter(makeActive:boolean){
    let temparr = [
      ...this.originalTaskDataSource.data.map((element) => {
        return {
          ...element,
          children: element.children?.map((e) => {

            return {
              ...e,
              children: e.children?.filter((c) => {
                return c.active == makeActive && (c.description.toLowerCase().trim().includes(this.filterTaskString.trim().toLowerCase()));
              })
            }
          }
          ),
        };
      }),
    ];
    
    this.dataSource.data = temparr;
    Object.keys(this.dataSource.data).forEach((key: any) => {
      this.setParent(this.dataSource.data[key], undefined);
    });

    this.dataSource.data.forEach((da)=>{
      da.children?.forEach((sda)=>{
        sda.children?.forEach((task)=>{
          this.checkAllParents(task);
        })
      })
    })

    this.treeControl.dataNodes = temparr;

    this.filterTaskString.length > 0 ? this.treeControl.expandAll(): this.treeControl.collapseAll();
  }

  filterData(data: any, toFilter: any) {
    if (this.selectedTaskFilter === 'DA') {
      this.dataSource.data.forEach((x: any) => {
        x.children = x.children.forEach((element: any) => {
          return element.children.includes(this.filterTaskString) ? element.children : { children: {} };
        });
      })
    }
    
  }

  filtered(node: any) {
    return node.description === this.filterTaskString;
  }
}



/**
 * Food data with nested structure.
 * Each node has a name and an optional list of children.
 */
class Tasks {
  description: string;
  children?: Tasks[];
  checkbox?: boolean;
}

// const TREE_DATA: Tasks[] = [
//   {
//     description: '$1. Transmission Operations',
//     children: [
//       {
//         description: '$1.1 Voltage Control',
//         children: [],
//       },
//       {
//         description: '$1.2 Facility Control',
//         children: [],
//       },
//       {
//         description: '$1.3 System Monitoring and Cont...',
//         children: [
//           {
//             description: '$1.3.1 LSO/SO Continuously monitor all pertinent conditions... ',

//           },
//           {
//             description: '$1.3.2 Respond to actual or potential rating violations... ',
//           },
//         ],
//       },
//     ],
//   },
// ];

class EOTreeNew {
  id: any;
  description: string;
  children!: EOTreeNew[];
  checkbox?: boolean;
  selected?: boolean;
  indeterminate?: boolean;
  parent?: EOTreeNew;
  active?: boolean;
  isLink?: boolean;
  IsEO?: boolean = false;
  isMeta?: boolean = false;
  isSkillQualification?: boolean = false;
}
