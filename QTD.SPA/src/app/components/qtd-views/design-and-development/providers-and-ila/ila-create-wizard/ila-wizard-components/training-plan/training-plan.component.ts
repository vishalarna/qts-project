import { ILA_EnablingObjective_LinkOptions } from './../../../../../../../_DtoModels/ILA_EnablingObjective_Link/ILA_EnablingObjective_LinkOptions';
import { IlaService } from 'src/app/_Services/QTD/ila.service';
import { SelectionModel } from '@angular/cdk/collections';
import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';
import { TemplatePortal } from '@angular/cdk/portal';
import {
  AfterViewInit,
  ChangeDetectorRef,
  Component,
  EventEmitter,
  OnDestroy,
  OnInit,
  Output,
  Input,
  ViewChild,
  ViewContainerRef,
  ViewChildren,
  QueryList,
} from '@angular/core';
import {
  UntypedFormArray,
  UntypedFormBuilder,
  UntypedFormControl,
  UntypedFormGroup,
  SelectControlValueAccessor,
  Validators,
} from '@angular/forms';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import * as ckcustomBuild from 'src/app/ckcustomBuild/build/ckeditor.js';
import { select, Store } from '@ngrx/store';
import { SegmentCreateOptions } from 'src/app/_DtoModels/Segment/SegmentCreateOptions';
import { Segment_ObjectiveLinkOptions } from 'src/app/_DtoModels/Segment_ObjectiveLink/Segment_ObjectiveLinkOptions';
import { SegmentService } from 'src/app/_Services/QTD/segment.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { UploadAdapter } from 'src/app/_Shared/Utils/CKEditor_UploadAdapter';
import { SubSink } from 'subsink';
import { ILACustomObjective_LinkOptions } from 'src/app/_DtoModels/ILACustomObjective_Link/ILACustomObjective_LinkOptions';
import { ILA_Segment_LinkOptions } from 'src/app/_DtoModels/ILA_Segment_Link/ILA_Segment_LinkOptions';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { ILAUpdateOptions } from 'src/app/_DtoModels/ILA/ILAUpdateOptions';
import { Segment } from 'src/app/_DtoModels/Segment/Segment';
import { ILAObjectivesVM } from 'src/app/_DtoModels/ILA/ILAObjectivesVM';
import { CustomEnablingObjectiveService } from 'src/app/_Services/QTD/custom-enabling-objective.service';
import { CustomEnablingObjectiveOption } from '@models/CustomEnablingObjective/CustomEnablingObjectiveOptions';
import { ILATaskObjectiveLinkUpdateOptions } from '@models/ILA_TaskObjective_Link/ILATaskObjectiveLinkUpdateOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { FlyPanelObjectivesComponent } from './fly-panel-objectives/fly-panel-objectives.component';
import { Sort } from '@angular/material/sort';
import { UpdateSegmentObjectiveOrderListVM } from '@models/Segment_ObjectiveLink/UpdateSegmentObjectiveOrderListVM';
@Component({
  selector: 'app-training-plan',
  templateUrl: './training-plan.component.html',
  styleUrls: ['./training-plan.component.scss'],
})
export class TrainingPlanComponent implements OnInit, OnDestroy, AfterViewInit {
  public Editor = ckcustomBuild;
  public help_co :string;
  public configCKEditor = {
    toolbar: [
      'bold',
      'italic',
      '|',
      'link',
      '|',
      'bulletedList',
      'numberedList',
    ],
  };
  displayColumns: string[] = ['drag', 'number', 'type', 'description', 'id'];
  displayedColumns:string[]=['drag', 'number', 'type','order' ,'description', 'id', 'action']
  DataSource: MatTableDataSource<any> = new MatTableDataSource<any>();
  isCustomEO: boolean = false;
  linkSegment: boolean = false;
  addSegment: boolean = false;
  delivery_method: any[] = [];
  @Output() formEvent = new EventEmitter<any>();
  @Output() loadingEvent = new EventEmitter<boolean>();
  @Input() editIlaCheck: any;
  @Input() mode: string;
  eo: any[] = [];
  to: any[] = [];
  unlinkIdList: any = [];
  segmentsArray: any = [];

  //provider
  provider: string = 'Utility Institue';
  providerConditions: boolean = false;

  //topic
  topic: string = 'Power System Protection';
  topicConditions: boolean = false;

  //offical name
  offical_name: string = 'Intro to QTD-QTS_001_01';
  officalNameConditions: boolean = false;

  //number
  numberConditions: boolean = false;
  number: string = '8932345';
  segmentTitle: string = "";

  //ILA Description
  ILADescriptionConditions: boolean = false;
  ILA_Description: string =
    'Continously monitor all pretinent conditions on POWERCO and neighbouring test systems, identify actual or potenial problems, and determine need for corrective actions.';

  segmentForm: UntypedFormGroup = new UntypedFormGroup({});
  linkedObjectives: any[];
  ILAID: any = '';
  customObjectives: ILACustomObjective_LinkOptions[] = [];
  @Output() edit: EventEmitter<Event> = new EventEmitter<Event>();
  selection = new SelectionModel<any>(true, []);
  tempSrcEO: any[] = [];
  tempSrcTO: any[] = [];
  tempSrc: any[] = [];
  trainingPlan: string = '';

  innerSelection: SelectionModel<any> = new SelectionModel<any>(true, []);

  segments = ['Segment 1'];
  saved = [false];
  showLinked = [false];
  isSaving = [false];
  segmentIds: any[] = [null];
  mainSpinner = false;
  rowDetails:any;
  segmentLinksOrder:UpdateSegmentObjectiveOrderListVM = new UpdateSegmentObjectiveOrderListVM();

  @ViewChild('objSort', { static: false }) objSort!: MatSort;
  @ViewChildren('objSort1') objSort1!: QueryList<MatSort>;
  @ViewChild('objPgn', { static: false }) objPgn!: MatPaginator;
  @ViewChild('addObjectives') addObjectivesComponent!:FlyPanelObjectivesComponent;
  constructor(
    public flyPanelSrvc: FlyInPanelService,
    private vcr: ViewContainerRef,
    private fb: UntypedFormBuilder,
    private saveStore: Store<{ saveIla: any }>,
    private segmentService: SegmentService,
    private alert: SweetAlertService,
    private dataBroadcastService: DataBroadcastService,
    private getServices: IlaService,
    private cdref: ChangeDetectorRef,
    public customEOSrvc: CustomEnablingObjectiveService,
    private labelPipe: LabelReplacementPipe
  ) { }
 ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }

  flyPanelIndex: any;

  //Incoming Id from segments
  selectedId: any[] = [];
  otherSelectedObjectives: any[] = [];
  length = [{}];

  // All selected ids from segment flypanel
  allIds: any[];
  // Sink all subscriptions so that they do not cause problems after component is destroyed
  subscriptions = new SubSink();

  // this group is used to dynamically populate
  group: UntypedFormGroup;
  isNercProvider = false;
  ngOnInit() {
    this.initializeTrainingForm();
    this.loadAsync();
  }

  async loadAsync() {
    try {
      this.loadingEvent.emit(true);
      this.help_co ='Use the custom objective free form field to add specific objectives for the selected ' +(await this.labelPipe.transform('ILA')) +'. The custom objectives will only be available for this ' +(await this.labelPipe.transform('ILA')) +'. To add the custom objectives to your EO hierarchy, select the Add to EO List option';
      await this.getObjectives();

      await new Promise<void>((resolve, reject) => {
        this.subscriptions.sink = this.saveStore.pipe(select('saveIla')).subscribe({
          next: async (res: any) => {
            if (res?.saveData?.result !== undefined) {
              this.mainSpinner = true;
              this.ILAID = res['saveData']?.result?.id;
              try {
                await this.getILAData();
                this.delivery_method = [
                  {
                    id: res['saveData']?.result?.deliveryMethodId ?? null,
                    value: res['saveData']?.result?.deliveryMethodName ?? 'N/A',
                  },
                ];
                this.isNercProvider = res['saveData']?.result?.isProviderNERC;
                await this.readySegments();

                if (this.editIlaCheck) {
                  await this.getObjectivesLinkedToILA();
                }
                resolve();
              } catch (e) {
                reject(e);
              } finally {
                this.mainSpinner = false;
              }
            }
          },
          error: (error) => reject(error),
        });
      });

      this.subscriptions.sink = this.group.statusChanges.subscribe((res: any) => {
        this.formEvent.emit(res);
      });
    } catch (error) {
      this.loadingEvent.emit(false);
    } finally {
      this.loadingEvent.emit(false);
    }
  }


  initializeTrainingForm() {
    this.group = this.fb.group({
      segmentsForm: this.fb.array([this.initializeSegmentForm()]),
    });
    if (this.mode === 'view') {
      this.group.disable(); 
    }
  }

  initializeSegmentForm() {
    return this.fb.group({
      title: new UntypedFormControl('', [Validators.required]),
      description: new UntypedFormControl('', Validators.required),
      duration: new UntypedFormControl('', Validators.required),
      isPartialCredit: new UntypedFormControl(false),
      isStandard: new UntypedFormControl(false),
      isOperationTopic: new UntypedFormControl(false),
      isSimulation: new UntypedFormControl(false),
    });
  }

  async getObjectivesLinkedToILA() {
    var data = await this.getServices.getAllObjectives(this.ILAID);
    //data = this.sortLinkedObjectives(data);
    this.DataSource = new MatTableDataSource(data);
    this.DataSource.sort = this.objSort;
    this.DataSource.paginator = this.objPgn;
  }

  sortLinkedObjectives(inputArr: any[]) {
    return inputArr.sort((a, b) => {
      const inputA = a.number.split('§')[1].split('.').map(Number);
      const inputB = b.number.split('§')[1].split('.').map(Number);
      for (let i = 0; i < Math.max(inputA.length, inputB.length); i++) {
        const partA = inputA[i] || 0;
        const partB = inputB[i] || 0;
        if (partA !== partB) {
          return partA - partB;
        }
      }
      return 0;
    });
  }

  async getILAData() {
    this.trainingPlan= await this.getServices.getTrainingPlanAsync(this.ILAID);
  }

  ngAfterViewInit(): void {
    this.subscriptions.sink =
      this.dataBroadcastService.saveTrainingPlan.subscribe((res: any) => {
        if (this.trainingPlan !== '' && this.ILAID !== undefined) {
          var obj = new ILAUpdateOptions();
          obj.trainingPlan = this.trainingPlan;
          if(obj.trainingPlan != null){
            this.getServices
              .saveTrainingPlan(this.ILAID, obj)
              .then((res: any) => {
                this.trainingPlan = '';
              })
              .catch((err) => {
                this.alert.errorToast('Failed To Save Training Plan');
              });
          }
        }
      });

    this.subscriptions.sink =
      this.dataBroadcastService.updateSegments.subscribe((res: any) => {
        if (res.todo === 'DELETE') {
          this.deleteSegment(res);
        } else if (res.todo === 'UPDATE') {
          this.updateSegment(res);
        } else {
          this.segmentAdded(res);
        }
      });
  }

  async getEnablingObjectivesLinked(id: any) {
    await this.getServices
      .getLinkedEnablingObjectives(id)
      .then((res) => {
        this.eo = res;
        /*     this.tempSrcEO = this.tempSrcEO.filter((x) => {
            return x.type !== 'EO';
          }) */
        this.eo?.forEach((e) => {
          this.tempSrcEO.push({
            majornumber: e.majorVersion,
            minorversion: e.minorVersion,
            number:
              /* `${e.majorVersion}.${e.minorVersion}.${e.number}` */ '1.1.' +
              e.number,
            id: e.id,
            type: 'EO',
            description: e.description,
          });
        });
      })
      .catch((err) => {});
    this.DataSource = new MatTableDataSource(this.tempSrcEO);
  }

  async getTaskObjectivesLinked(id: any) {
    await this.getServices
      .getLinkedTaskObjectives(id)
      .then((res) => {
        this.eo = res;
        /*   this.tempSrcEO = this.tempSrcEO.filter((x) => {
          return x.type !== 'Task';
        }) */
        this.eo?.forEach((e) => {
          this.tempSrcEO.push({
            /*  majornumber: e.majorVersion,
           minorversion: e.minorVersion, */
            number:
              /* `${e.majorVersion}.${e.minorVersion}.${ e.number }` */ '1.1.' +
              e.number,
            id: e.id,
            type: 'Task',
            description: e.description,
          });
        });
      })
      .catch((err) => {});
    this.DataSource = new MatTableDataSource(this.tempSrcEO);
  }

  async getCustomObjectivesLinked(id: any) {
    await this.getServices
      .getLinkedCustomObjectives(id)
      .then((res) => {
        this.eo = res;
        this.tempSrcEO = this.tempSrcEO.filter((x) => {
          return x.type !== 'Custom';
        });
        this.eo?.forEach((e, i) => {
          this.tempSrcEO.push({
            /*    majornumber: e.majorVersion,
             minorversion: e.minorVersion, */
            number: '1.1.' + (i + 1),
            id: e.id,
            type: 'Custom',
            description: e.description,
          });
        });
      })
      .catch((err) => {});
    this.DataSource = new MatTableDataSource(this.tempSrcEO);
  }

  async readySegmentsLinked(id: any) {
    await this.getServices
      .getLinkedSegments(id)
      .then((res) => {
        res.forEach((i) => {
          this.segmentsArray.push({
            id: i.id,
            description: i.content.replace(/<[^>]+>/g, ''),
            title: i.title,
          });
        });
      })
      .catch((err) => {});
  }

  segmentData: Segment[] = [];
  async readySegments() {
    this.segmentData = await this.getServices.getLinkedSegments(this.ILAID);
    const formLength = this.group.get('segmentsForm') as UntypedFormArray;
    if (formLength.length > 0 && this.segmentData.length > 0) {
      formLength.clear();
    }
    if (this.segmentData.length > 0 && formLength.length === 0) {
      const controls = this.group.get('segmentsForm') as UntypedFormArray;
      if (this.segmentData.length) {
        this.segmentData.forEach((data, i) => {
          this.readySegmentLinks(data, i);
          const tempForm = this.fb.group({
            title: new UntypedFormControl(data.title, [Validators.required]),
            description: new UntypedFormControl(data.content, [Validators.required]),
            duration: new UntypedFormControl(data.duration, [Validators.required]),
            isPartialCredit: new UntypedFormControl(data.isPartialCredit),
            isStandard: new UntypedFormControl(data.isNercStandard),
            isOperationTopic: new UntypedFormControl(data.isNercOperatingTopics),
            isSimulation: new UntypedFormControl(data.isNercSimulation),
          });
          if (this.mode === 'view') {
            tempForm.disable();
          }
          controls.push(tempForm);
          if(this.segments.findIndex(x=>x == `Segment ${i + 1}`) == -1){
            this.segments.push(`Segment ${i + 1}`);
          }
        });
      }
    }
  }

  async readySegmentLinks(data: Segment, idx: number) {
    var links = await this.segmentService.getLinkedObjectives(data.id);
    this.length[idx] = links.length;
    this.saved[idx] = true;
    this.segmentIds[idx] = data.id;
    this.selectedId[idx] = new MatTableDataSource<any>([]);
    links.forEach((link) => {
      if (link.task !== null) {
        this.selectedId[idx].data.push({
          number: `${link.task.subdutyArea.dutyArea.letter}${link.task.subdutyArea.dutyArea.number}.${link.task.subdutyArea.subNumber}.${link.task.number}`,
          type: 'Task',
          description: `${link.task.subdutyArea.dutyArea.letter}${link.task.subdutyArea.dutyArea.number}.${link.task.subdutyArea.subNumber}.${link.task.number} - ${link.task.description}`,
          id: link.task.id,
          segmentObjId:link.id
        });
        setTimeout(() => {
          const sortsArray = this.objSort1.toArray();
          if (sortsArray[idx]) {
            this.selectedId[idx].sort = sortsArray[idx];
          }
        },1000);
        
      } else if (link.enablingObjective !== null) {
        this.selectedId[idx].data.push({
          number: `${link.enablingObjective.fullNumber}`,
          type: 'EO',
          description: link.enablingObjective.description,
          id: link.enablingObjective.id,
          segmentObjId:link.id
        });
        setTimeout(() => {
          const sortsArray = this.objSort1.toArray();
          if (sortsArray[idx]) {
            this.selectedId[idx].sort = sortsArray[idx];
          }
        },1000);
      } else if (link.customEnablingObjective !== null) {
        this.selectedId[idx].data.push({
          number: `${link.customEnablingObjective.fullNumber}`,
          type: 'Custom',
          description: link.customEnablingObjective.description,
          id: link.customEnablingObjective.id,
          segmentObjId:link.id
        });
        setTimeout(() => {
          const sortsArray = this.objSort1.toArray();
          if (sortsArray[idx]) {
            this.selectedId[idx].sort = sortsArray[idx];
          }
        },1000);
      }
    });
  }

  readyFrom() {
    var numberOfSegments = this.segments.length;
    this.segments.push('Segment ' + (numberOfSegments + 1));
    this.segmentIds.push('');
    const controls = this.group.get('segmentsForm') as UntypedFormArray;
    if (controls.length !== 0) {
      controls.push(this.initializeSegmentForm());
    }
    this.isSaving.push(false);
    this.saved.push(false);
    this.showLinked.push(false);
    this.selectedId.push(new MatTableDataSource());
  }

  async segmentAdded(res: any) {
    await this.segmentService
      .get(res.id)
      .then((data: any) => {
        var index = 0;
        if (!this.saved[this.segments.length - 1]) {
          this.readyFrom();
          // this.addFormWithPrevValues();
          index = 2;
        } else {
          this.readyFrom();
          index = 1;
        }
        const segmentForm = this.group.get('segmentsForm') as UntypedFormArray;
        const segmentFormGroup = segmentForm.at(
          this.segments.length - index
        ) as UntypedFormGroup;
        segmentFormGroup.get('title')?.setValue(data.title);
        segmentFormGroup.get('description')?.setValue(data.content);
        segmentFormGroup.get('duration')?.setValue(data.duration);
        segmentFormGroup.get('isPartialCredit')?.setValue(data.isPartialCredit);
        segmentFormGroup.get('isStandard')?.setValue(data.isNercStandard);
        segmentFormGroup
          .get('isOperationTopic')
          ?.setValue(data.isNercOperatingTopics);
        segmentFormGroup.get('isSimulation')?.setValue(data.isNercSimulation);
        this.refreshLink(res);
        this.segmentIds[this.segments.length - index] = res.id;
        this.saved[this.segments.length - index] = true;
      })
      .catch((err: any) => {
        this.alert.errorToast('Error Fetching Segment Data');
      });
  }

  async updateSegment(res: any) {
    await this.segmentService
      .get(res.id)
      .then((data: any) => {
        this.segmentIds.forEach((value: any, index: any) => {
          if (value === res.id) {
            this.group.get('description' + index)?.setValue(data.content);
            this.refreshLink(res);
          }
        });
      })
      .catch((err: any) => {
        this.alert.errorToast('Error Fetching Segment Data');
      });
  }

  async refreshLink(res: any) {
    await this.segmentService
      .getLinkedObjectives(res.id)
      .then((data: any) => {
        this.segmentIds.forEach((value: any, index: any) => {
          if (value === res.id) {
            var source: any[] = [];
            data.forEach((element: any) => {
              element.task !== null
                ? source.push({ ...element.task, type: 'Task' })
                : source.push({ ...element.enablingObjective, type: 'EO' });
            });
            this.selectedId[index] = new MatTableDataSource(source);
            setTimeout(() => {
              const sortsArray = this.objSort1.toArray();
              if (sortsArray[index]) {
                this.selectedId[index].sort = sortsArray[index];
              }
            });
            this.length[index] = source.length;
            this.cdref.detectChanges();
          }
        });
      })
      .catch((err: any) => {
        this.alert.errorToast('Error Fetching Segment Links');
      });
  }

  deleteSegment(res: any) {
    this.segmentIds = this.segmentIds.filter((value: any, index: any) => {
      if (value === res.id) {
        this.segments.splice(index, 1);
        this.segmentIds.splice(index, 1);
        const segmentForm = this.group.get('segmentsForm') as UntypedFormArray;
        segmentForm.removeAt(index);
        this.selectedId.splice(index, 1);
        this.saved.splice(index, 1);
        this.isSaving.splice(index, 1);
        this.renameSegments();
      }
      return value !== res.id;
    });
  }

  renameSegments() {
    this.segments.forEach((data: any, index: any) => {
      this.segments[index] = `Segment /${index}`;
    });
  }

  async linkEOToSegment(index: any, id: any) {
    this.isSaving[index] = true;
      this.segmentLinksOrder = new UpdateSegmentObjectiveOrderListVM();

      var selectedIdFromIdx  = this.selectedId[index];

      if(selectedIdFromIdx) {
        this.segmentLinksOrder.segmentObjectives = selectedIdFromIdx.data.map(
          (segmentLink: any, i: number) => {
            let type = '';
            if (segmentLink.taskId) {
              type = 'Task';
            } else if (segmentLink.eoId) {
              type = 'EO';
            } else if (segmentLink.customId) {
              type = 'Custom';
            }

            return {
              segmentObjectiveLinkId: segmentLink.segmentObjId,
              order: i + 1,
              type: segmentLink.type,
              objectiveId: segmentLink.id
            };
          }
        );
      }
    await this.segmentService.linkObjectives(id, this.segmentLinksOrder).then(async(res: any) => {
      await this.linkSegmentToILA(id, index);
    });
  }

  async createOnlyLinks(index: any) {
      this.segmentLinksOrder = new UpdateSegmentObjectiveOrderListVM();
      this.segmentLinksOrder.segmentObjectives = this.selectedId[index].data.map(
        (segmentLink: any, i: number) => {
          let type = '';
          if (segmentLink.taskId) {
            type = 'Task';
          } else if (segmentLink.eoId) {
            type = 'EO';
          } else if (segmentLink.customId) {
            type = 'Custom';
          }

          return {
            segmentObjectiveLinkId: segmentLink.segmentObjId,
            order: i + 1,
            type:segmentLink.type,
            objectiveId:segmentLink.id
          };
        }
      );
      await this.segmentService.linkObjectives(this.segmentIds[index], this.segmentLinksOrder)
      await this.linkSegmentToILA(this.segmentIds[index], index);
      await this.getObjectivesLinkedToILA();
      await this.readySegments();

  }

  async linkSegmentToILA(segId: any, index: any) {
    this.isSaving[index] = true;
    var options = new ILA_Segment_LinkOptions();
    options.iLAId = this.ILAID;
    options.segmentId = segId;
    await this.getServices
      .linkSegments(this.ILAID, options)
      .then((res: any) => {
        this.isSaving[index] = true;
        this.saved[index] = true;
        this.showLinked[index] = false;
        this.dataBroadcastService.segmentSaved.next(null);
        this.alert.successToast('Segment Saved and Links created');
      })
      .finally(() => {
        this.isSaving[index] = false;
      });
  }

  durationChanged(event: any) {
    var charCode = event.which ? event.which : event.keyCode;
    // Only Numbers 0-9
    if (charCode < 48 || charCode > 57) {
      event.preventDefault();
      return false;
    } else {
      return true;
    }
  }

  unlinkEO(node: any, index: any) {
    const indexToRemove = this.selectedId[index].data.findIndex((data: any) => {
      return data.id === node.id && data.type === node.type;
    });
    if (indexToRemove !== -1) {
      this.selectedId[index].data.splice(indexToRemove, 1);
    }
    this.selectedId[index] = new MatTableDataSource(this.selectedId[index].data);
    setTimeout(() => {
      const sortsArray = this.objSort1.toArray();
      if (sortsArray[index]) {
        this.selectedId[index].sort = sortsArray[index];
      }
    });
    if (this.saved[index]) {
      this.unlinkFromBackEnd(node, index);
    }
  }

  unlinkObjectiveId(e: any) {
    this.unlinkIdList.push(e);
  }

  async removeObjectives(row:any){
    if(row.type === "Custom"){
      let options: CustomEnablingObjectiveOption= new CustomEnablingObjectiveOption();
        options.actionType ='delete';
        await this.customEOSrvc.delete(row.id,options).then(async (_)=>{
          this.alert.successToast("Custom " + await this.transformTitle('Enabling Objective') + " Deleted Successfully");
        });
       this.getObjectivesLinkedToILA();
    }
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }

  async unlinkObjectivesFunctions() {
    var unlinkEO: ILA_EnablingObjective_LinkOptions =
      new ILA_EnablingObjective_LinkOptions();
    var unlinkCO: ILACustomObjective_LinkOptions =
      new ILACustomObjective_LinkOptions();
    var custIds = this.selection.selected
      .filter((data) => {
        return data.type === 'Custom';
      })
      .map((x) => {
        return x.id;
      });
    var eoIds = this.selection.selected
      .filter((data) => {
        return data.type === 'EO';
      })
      .map((x) => {
        return x.id;
      });
    var taskIds = this.selection.selected
      .filter((data) => {
        return data.type === 'Task';
      })
      .map((x) => {
        return x.id;
      });

    if (custIds.length > 0) {
      unlinkCO.customObjIds = [];
      unlinkCO.ilaId = this.ILAID;
      unlinkCO.customObjIds = [];
      unlinkCO.customObjIds = custIds;
      await this.getServices
        .unlinkCustomObjective(this.ILAID, unlinkCO)
        .then(async(res: any) => {
          this.alert.successToast('Custom Objectives Unlinked');
        });
    }
    if (eoIds.length > 0) {
      unlinkEO.enablingObjectiveIds = [];
      unlinkEO.ilaid = this.ILAID;
      unlinkEO.enablingObjectiveIds = [];
      unlinkEO.enablingObjectiveIds = eoIds;
      await this.getServices
        .unlinkEnablingObjective(this.ILAID, unlinkEO)
        .then(async (res: any) => {
          this.alert.successToast( await this.transformTitle('Enabling Objective') + 's Unlinked');
        });
    }
    if (taskIds.length > 0) {
        var totalLinkedIds = this.DataSource.data.filter((data: ILAObjectivesVM) => data.type === "Task" && !taskIds.includes(data.id)).map((x: ILAObjectivesVM) => x.id);
        totalLinkedIds = Array.from(new Set(totalLinkedIds));
        var taskLinkUpdateOptions = new ILATaskObjectiveLinkUpdateOptions();
        taskLinkUpdateOptions.taskLinks =totalLinkedIds.map((taskId, index) => ({ taskId,sequence: 0 }));
        await this.getServices.updateILATaskObjectiveLinksAsync(this.ILAID,taskLinkUpdateOptions).then(async res=>{
          this.alert.successToast(await this.transformTitle('Task') +" Objectives Unlinked");
        })
    }
    this.unlinkIdList = [];
    this.selection.clear();
    await this.getObjectivesLinkedToILA();
    await this.readySegments();

  }

  async unlinkFromBackEnd(node: any, index: any) {
    var unlinkOptions = new Segment_ObjectiveLinkOptions();
    if (node.type === 'Task') {
      unlinkOptions.enablingObjectiveIds = [];
      unlinkOptions.segmentId = this.segmentIds[index];
      unlinkOptions.taskIds = [node.id];
      unlinkOptions.customEOIds = [];
    } else if (node.type === 'EO') {
      unlinkOptions.enablingObjectiveIds = [node.id];
      unlinkOptions.segmentId = this.segmentIds[index];
      unlinkOptions.taskIds = [];
      unlinkOptions.customEOIds = [];
    } else {
      unlinkOptions.customEOIds = [node.id];
      unlinkOptions.segmentId = this.segmentIds[index];
      unlinkOptions.taskIds = [];
      unlinkOptions.enablingObjectiveIds = [];
    }
    this.segmentService
      .unlinkObjectives(this.segmentIds[index], unlinkOptions)
      .then((res: any) => {
        this.alert.successToast('Segment Objective Unlinked');
      })
      .catch((err: any) => {
        this.alert.errorAlert(err);
      });
  }

  cancelClicked(index: any) {
    this.segments = this.segments.filter((data, idx) => {
      return idx !== index;
    });
    this.isSaving = this.isSaving.filter((data, idx) => {
      return idx !== index;
    });
    this.saved = this.saved.filter((data, idx) => {
      return idx !== index;
    });
    this.segmentIds = this.segmentIds.filter((data, idx) => {
      return idx !== index;
    });
    this.showLinked = this.showLinked.filter((data, idx) => {
      return idx !== index;
    });
    this.selectedId[index] = new MatTableDataSource();
    const segmentForm = this.group.get('segmentsForm') as UntypedFormArray;
    segmentForm.removeAt(index);
  }

  onReady(editor: any) {
    editor.plugins.get('FileRepository').createUploadAdapter = function (
      loader: any
    ) {
      return new UploadAdapter(loader);
    };
  }

  async recieveMessage(event: any) {
    await this.getObjectivesLinkedToILA();
    this.linkedTaskIds = this.DataSource.data.filter((data: ILAObjectivesVM) => {
      return data.type === "Task";
    }).map((x: ILAObjectivesVM) => {
      return x.id;
    });
    if (this.addObjectivesComponent && typeof this.addObjectivesComponent.modifyAlreadyLinkedTaskData == 'function') {
      this.addObjectivesComponent.linkedTaskIds = this.linkedTaskIds;
      this.addObjectivesComponent.modifyAlreadyLinkedTaskData();
    }
    this.linkedEOIds = this.DataSource.data.filter((data: ILAObjectivesVM) => {
      return data.type === "EO";
    }).map((x: ILAObjectivesVM) => {
      return x.id;
    });
    if (this.addObjectivesComponent && typeof this.addObjectivesComponent.modifyAlreadyLinkedEOs == 'function') {
      this.addObjectivesComponent.linkedEOIds = this.linkedEOIds;
      this.addObjectivesComponent.modifyAlreadyLinkedEOs();
    }
  }

  getObjectives() {
    this.linkedObjectives = [];
    this.DataSource = new MatTableDataSource(this.linkedObjectives);
  }

  linkedTaskIds: any[] = [];
  linkedEOIds: any[] = [];
  openFlyInPanel(templateRef: any, customEO: boolean,mode:string,row:any) {
    this.linkedTaskIds = this.DataSource.data.filter((data: ILAObjectivesVM) => {
      return data.type === "Task";
    }).map((x: ILAObjectivesVM) => {
      return x.id;
    });
    this.linkedEOIds = this.DataSource.data.filter((data: ILAObjectivesVM) => {
      return data.type === "EO";
    }).map((x: ILAObjectivesVM) => {
      return x.id;
    });

    this.rowDetails=row;
    this.mode=mode;
    this.isCustomEO = customEO;
    let portal = new TemplatePortal(templateRef, this.vcr);
    this.flyPanelSrvc.open(portal);
  }

  linkedSegmentCOIds: any[] = [];
  linkedSegmentEOIds: any[] = [];
  linkedSegmentTaskIds: any[] = [];
  openLinkSegmentFlyPanel(templateRef: any, customEO: boolean, index: any) {
    const segmentData = this.group.get('segmentsForm')?.['controls'];
    this.segmentTitle = segmentData?.[index]?.value?.title;
    this.isCustomEO = customEO;
    this.flyPanelIndex = index;
    if (
      this.selectedId[index] !== undefined &&
      this.selectedId[index].data !== undefined
    ) {
      this.linkedSegmentTaskIds = this.selectedId[index].data
        .filter((data) => {
          return data.type === 'Task';
        })
        .map((x) => {
          return x.id;
        });

      this.linkedSegmentEOIds = this.selectedId[index].data
        .filter((data) => {
          return data.type === 'EO';
        })
        .map((x) => {
          return x.id;
        });
      this.linkedSegmentCOIds = this.selectedId[index].data
        .filter((data) => {
          return data.type === 'Custom';
        })
        .map((x) => {
          return x.id;
        });
    }
     this.getAllLinkedObjectives();
    let portal = new TemplatePortal(templateRef, this.vcr);
    this.flyPanelSrvc.open(portal);
  }

   getAllLinkedObjectives(){
    this.otherSelectedObjectives = [];
    this.selectedId.forEach((index) => {
      const matTableData = index.data;
      if (matTableData) {
        this.otherSelectedObjectives.push(...matTableData);
      }
    });
    const uniqueOtherSelectedObjectives = new Set(this.otherSelectedObjectives);
    this.otherSelectedObjectives = Array.from(uniqueOtherSelectedObjectives);
  }

  async saveSegmentData(segmentForm: any, index: number) {
    var trueIndex = this.segments.findIndex(
      (x) => x === `Segment ${index + 1}`
    );
    if (trueIndex === -1) {
      trueIndex = index + 1;
    }
    this.isSaving[trueIndex] = true;
    var segmentData = new SegmentCreateOptions();
    segmentData.Content = segmentForm.get('description')?.value;
    segmentData.duration = segmentForm.get('duration')?.value;
    segmentData.isPartialCredit = segmentForm.get('isPartialCredit')?.value;
    segmentData.isNercStandard = segmentForm.get('isStandard')?.value;
    segmentData.isNercOperatingTopics =
      segmentForm.get('isOperationTopic')?.value;
    segmentData.isNercSimulation = segmentForm.get('isSimulation')?.value;
    segmentData.title = segmentForm.get('title')?.value;
    segmentData.segmentId = this.segmentIds[index];
    await this.segmentService
      .create(segmentData)
      .then(async(res: any) => {
        this.segmentIds[index] = res.id;
        await this.linkEOToSegment(trueIndex, this.segmentIds[index]);
        this.saved[trueIndex] = true;
        await this.getObjectivesLinkedToILA();
        await this.readySegments();
      })
      .finally(() => {
        if (this.editIlaCheck) {
          this.isSaving[trueIndex] = false;
        }
      });
  }

  async drop(event: CdkDragDrop<string[]>) {
    if (this.saved.some((x) => x === false)) {
      this.alert.warningAlert(
        'Segment Not Saved',
        'Please Save all Segmets to Reorder data'
      );
    } else {
      moveItemInArray(this.saved, event.previousIndex, event.currentIndex);
      moveItemInArray(this.isSaving, event.previousIndex, event.currentIndex);
      moveItemInArray(this.selectedId, event.previousIndex, event.currentIndex);
      moveItemInArray(this.showLinked, event.previousIndex, event.currentIndex);
      moveItemInArray(this.segments, event.previousIndex, event.currentIndex);
      moveItemInArray(this.segmentIds, event.previousIndex, event.currentIndex);
      moveItemInArray(this.length, event.previousIndex, event.currentIndex);

      this.cdref.detectChanges();

      //update segment orders
      await this.getServices
        .updateLinkedSegmentsOrder(
          this.ILAID,
          this.segmentIds.filter((x) => x !== null && x !== '')
        )
        .then((res: any) => {
          this.alert.successToast('Segments order saved successfully');
        })
        .catch((err: any) => {
          this.alert.errorAlert(err);
        });
    }
  }

  /** Whether the number of selected elements matches the total number of rows. */
  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.DataSource.data.length;
    return numSelected === numRows;
  }

  /** Selects all rows if they are not all selected; otherwise clear selection. */
  masterToggle() {
    this.isAllSelected()
      ? this.selection.clear()
      : this.DataSource.data.forEach((row) => this.selection.select(row));
  }

  receiveId(event: any[]) {
    if (this.selectedId[this.flyPanelIndex] !== undefined) {
      var data = Object.assign(this.selectedId[this.flyPanelIndex].data);
      event.forEach((element) => {
        var check = data.find((x) => {
          return x.type === element.type && x.id === element.id;
        });
        if (!check) {
          data.push(element);
        }
      });
      this.selectedId[this.flyPanelIndex] = new MatTableDataSource(data);
      setTimeout(() => {
        const sortsArray = this.objSort1.toArray();
        if (sortsArray[this.flyPanelIndex]) {
          this.selectedId[this.flyPanelIndex].sort = sortsArray[this.flyPanelIndex];
        }
      });
      if (this.saved[this.flyPanelIndex] === true || this.editIlaCheck) {
        this.showLinked[this.flyPanelIndex] = true;
      }
      this.length[this.flyPanelIndex] = Object.keys(
        this.selectedId[this.flyPanelIndex].data
      ).length;
    } else {
      this.selectedId[this.flyPanelIndex] = new MatTableDataSource(event);
      setTimeout(() => {
        const sortsArray = this.objSort1.toArray();
        if (sortsArray[this.flyPanelIndex]) {
          this.selectedId[this.flyPanelIndex].sort = sortsArray[this.flyPanelIndex];
        }
      });
      if (this.saved[this.flyPanelIndex] === true || this.editIlaCheck) {
        this.showLinked[this.flyPanelIndex] = true;
      }
      this.length[this.flyPanelIndex] = Object.keys(
        this.selectedId[this.flyPanelIndex].data
      ).length;
    }
  }

  moveUp(element: any): void {
    const index = this.linkedObjectives.indexOf(element);
    if (index === 0) {
      // can not move higher
      return;
    }
    const swap = this.linkedObjectives[index - 1];
    this.linkedObjectives[index - 1] = element;
    this.linkedObjectives[index] = swap;
    this.DataSource = new MatTableDataSource(this.linkedObjectives);
  }

  moveDown(element: any): void {
    const index = this.linkedObjectives.indexOf(element);
    if (index === this.linkedObjectives.length - 1) {
      // can not move higher
      return;
    }
    const swap = this.linkedObjectives[index + 1];
    this.linkedObjectives[index + 1] = element;
    this.linkedObjectives[index] = swap;
    this.DataSource = new MatTableDataSource(this.linkedObjectives);
  }

  async dropTable(event: any, index: any) {
    if (this.isInvalidDragEvent) {
      this.isInvalidDragEvent = false;
      return;
    }
    const prevIndex = this.selectedId[index].data.findIndex(
      (d: any) => d === event.item.data
    );

    moveItemInArray(
      this.selectedId[index].data,
      event.previousIndex,
      event.currentIndex
    );
    //this.showLinked[index] = true;
    this.selectedId[index] = new MatTableDataSource(
      this.selectedId[index].data
    );
    setTimeout(() => {
      const sortsArray = this.objSort1.toArray();
      if (sortsArray[index]) {
        this.selectedId[index].sort = sortsArray[index];
      }
    });
    this.segmentLinksOrder = new UpdateSegmentObjectiveOrderListVM();
    this.segmentLinksOrder.segmentObjectives = this.selectedId[index].data.map((segmentLink, index) => ({
      segmentObjectiveLinkId: segmentLink.segmentObjId,
      order: index + 1,
      type: segmentLink.type,
      objectiveId: segmentLink.id
    }));
  }

  isInvalidDragEvent: boolean = false;
  onInvalidDragEventMouseDown() {
    this.isInvalidDragEvent = true;
  }
  dragStarted(event: any) {
    if (this.isInvalidDragEvent) {
      document.dispatchEvent(new Event('mouseup'));
    }
  }

  allSegmentsSaved(): boolean {
    return this.saved.every((data) => data === true);
  }

sortDataSource(sort: Sort) {
  if(sort.active == 'number' && sort.direction == 'asc'){
    const sortedArray = this.DataSource.data.sort((a,b)=> a?.number.localeCompare(b?.number, undefined, { numeric: true }));
    this.DataSource.data = sortedArray;
  }
  else if(sort.active == 'number' && sort.direction == 'desc'){
    const sortedArray = this.DataSource.data.sort((a,b)=> b?.number.localeCompare(a?.number, undefined, { numeric: true }));
    this.DataSource.data = sortedArray;
  }
  if (sort.active === 'order' && sort.direction === 'asc') {
    const sortedArray = this.DataSource.data.sort((a, b) => {
      const orderA = (a?.order ?? '').toString();
      const orderB = (b?.order ?? '').toString();
      return orderA.localeCompare(orderB, undefined, { numeric: true });
    });
    this.DataSource.data = sortedArray;
  } else if (sort.active === 'order' && sort.direction === 'desc') {
    const sortedArray = this.DataSource.data.sort((a, b) => {
      const orderA = (a?.order ?? '').toString();
      const orderB = (b?.order ?? '').toString();
      return orderB.localeCompare(orderA, undefined, { numeric: true });
    });
    this.DataSource.data = sortedArray;
  }

  if (sort.active == 'type' && sort.direction == 'asc') {
    const sortedArray = this.DataSource.data.sort((a, b) => {
      const order = { task: 1, eo: 2 };
      const typeA = order[a?.type?.toLowerCase()] || 0;
      const typeB = order[b?.type?.toLowerCase()] || 0;
      return typeA - typeB;
    });
    this.DataSource.data = sortedArray;
  } else if (sort.active == 'type' && sort.direction == 'desc') {
    const sortedArray = this.DataSource.data.sort((a, b) => {
      const order = { task: 1, eo: 2 };
    const typeA = order[a?.type?.toLowerCase()] || 0;
    const typeB = order[b?.type?.toLowerCase()] || 0;
    return typeB - typeA;
    });
    this.DataSource.data = sortedArray;
  }

  if (sort.active == 'description' && sort.direction == 'asc') {
    const sortedArray = this.DataSource.data.sort((a, b) => {
      const descA = a?.description?.toLowerCase() || '';
      const descB = b?.description?.toLowerCase() || '';
      return descA.localeCompare(descB);
    });
    this.DataSource.data = sortedArray;
  } else if (sort.active == 'description' && sort.direction == 'desc') {
    const sortedArray = this.DataSource.data.sort((a, b) => {
      const descA = a?.description?.toLowerCase() || '';
      const descB = b?.description?.toLowerCase() || '';
      return descB.localeCompare(descA);
    });
    this.DataSource.data = sortedArray;
  }
 }
}
