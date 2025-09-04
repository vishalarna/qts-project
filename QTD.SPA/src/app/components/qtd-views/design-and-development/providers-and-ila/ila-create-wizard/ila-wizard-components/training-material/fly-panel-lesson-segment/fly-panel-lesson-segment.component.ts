import { Component, EventEmitter, Input, OnDestroy, OnInit, Output, ViewChild, ViewContainerRef } from '@angular/core';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { TemplatePortal } from '@angular/cdk/portal';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import * as ckcustomBuild from 'src/app/ckcustomBuild/build/ckeditor.js';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { SegmentCreateOptions } from 'src/app/_DtoModels/Segment/SegmentCreateOptions';
import { SegmentService } from 'src/app/_Services/QTD/segment.service';
import { Segment_ObjectiveLinkOptions } from 'src/app/_DtoModels/Segment_ObjectiveLink/Segment_ObjectiveLinkOptions';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { moveItemInArray } from '@angular/cdk/drag-drop';
import { UploadAdapter } from 'src/app/_Shared/Utils/CKEditor_UploadAdapter';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { ILA_Segment_LinkOptions } from 'src/app/_DtoModels/ILA_Segment_Link/ILA_Segment_LinkOptions';
import { IlaService } from 'src/app/_Services/QTD/ila.service';
import { select, Store } from '@ngrx/store';
import { SubSink } from 'subsink';
import { UpdateSegmentObjectiveOrderListVM } from '@models/Segment_ObjectiveLink/UpdateSegmentObjectiveOrderListVM';

@Component({
  selector: 'app-fly-panel-lesson-segment',
  templateUrl: './fly-panel-lesson-segment.component.html',
  styleUrls: ['./fly-panel-lesson-segment.component.scss']
})
export class FlyPanelLessonSegmentComponent implements OnInit,OnDestroy {
  public Editor = ckcustomBuild;
  flyPanelCheck: boolean = true;
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
  DataSource: MatTableDataSource<any>;
  isCustomEO: boolean = false;
  addSegment: boolean = false;
  objpanel: boolean = false;


  group: UntypedFormGroup = new UntypedFormGroup({});
  segments = ['Segment 1'];
  isSaving = [false];
  segmentIds = [''];
  selectedId: any = [{}];
  length = [{}];
  saved = [false];
  showLinked = [false];
  flyPanelIndex: any;
  isInvalidDragEvent: boolean = false;
  subscriptions = new SubSink();
  ILAID:any;
  currIndex = 91
  linkedCOIds: any[] = [];
  linkedTaskIds: any[] = [];
  linkedEOIds: any[] = [];
  segmentTitle : string = "";

  isNercProvider =false;
  @Input() editIlaCheck: any;
  @Output() segSaved = new EventEmitter<string>();
  segmentLinksOrder:UpdateSegmentObjectiveOrderListVM = new UpdateSegmentObjectiveOrderListVM();

  constructor(
    public flyPanelSrvc: FlyInPanelService,
    private vcr: ViewContainerRef,
    private segmentService: SegmentService,
    private alert: SweetAlertService,
    private dataBroadcastService: DataBroadcastService,
    private ilaService: IlaService,
    private saveStore: Store<{ saveIla: any }>,
  ) { }

  ngOnInit(): void {
    this.segments.forEach((segment: any, index: any) => {
      this.group.addControl('title' + index, new UntypedFormControl('', Validators.required))
      this.group.addControl('description' + index, new UntypedFormControl('', Validators.required))
      this.group.addControl('duration' + index, new UntypedFormControl('', Validators.required))
      this.group.addControl('isStandard' + index, new UntypedFormControl(false))
      this.group.addControl('isPartialCredit' + index, new UntypedFormControl(false))
      this.group.addControl('isOperationTopic' + index, new UntypedFormControl(true))
      this.group.addControl('isSimulation' + index, new UntypedFormControl(false))
    })
    this.selectedId[0] = new MatTableDataSource();
    this.subscriptions.sink = this.saveStore.pipe(select('saveIla')).subscribe((res)=>{
      if ((res?.saveData?.result !== undefined)){
        this.ILAID = res?.saveData?.result?.id;
        this.isNercProvider = res['saveData']?.result?.isProviderNERC;
      }
    })
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }

  OnClose() {
    var id: any[] = [{ task: {}, eo: {} }]
    
    if (this.flyPanelCheck !== true) {
      this.flyPanelSrvc.close();
    }
  }

  async saveSegmentData(index: any) {
    this.isSaving[index] = true;
    var segmentData = new SegmentCreateOptions();
    segmentData.Content = this.group.get('description' + index)?.value;
    segmentData.duration = this.group.get('duration' + index)?.value;
    segmentData.isNercStandard = this.group.get('isStandard' + index)?.value;
    segmentData.isPartialCredit = this.group.get('isPartialCredit' + index)?.value;
    segmentData.isNercOperatingTopics = this.group.get('isOperationTopic' + index)?.value;
    segmentData.isNercSimulation = this.group.get('isSimulation' + index)?.value;
    segmentData.title = this.group.get('title' + index)?.value;
    await this.segmentService.create(segmentData).then(async(res: any) => {
      this.segmentIds[index] = res.id;
      await this.linkEOToSegment(index, res.id);
    }).catch((err: any) => {
      this.isSaving[index] = false;
      console.error(err);
    })
  }
  dropTable(event: any, index: any) {
    if (this.isInvalidDragEvent) {
      this.isInvalidDragEvent = false;
      return;
    }
    const prevIndex = this.selectedId[index].data.findIndex(
      (d: any) => d === event.item.data
    );
    
    
    moveItemInArray(this.selectedId[index].data, prevIndex, event.currentIndex);
    
    this.selectedId[index] = new MatTableDataSource(this.selectedId[index].data)
    this.segmentLinksOrder = new UpdateSegmentObjectiveOrderListVM();
    this.segmentLinksOrder.segmentObjectives = this.selectedId[index].data.map((segmentLink, index) => ({
      segmentObjectiveLinkId: segmentLink.segmentObjId,
      order: index + 1,
      type: segmentLink.type,
      objectiveId: segmentLink.id
    }));
  }

  onInvalidDragEventMouseDown() {
    this.isInvalidDragEvent = true;
  }

  durationChanged(event: any) {
    var charCode = (event.which) ? event.which : event.keyCode;
    // Only Numbers 0-9
    if ((charCode < 48 || charCode > 57)) {
      event.preventDefault();
      return false;
    } else {
      return true;
    }
  }

  changeIndex(index:any){
    this.objpanel = true;
    this.flyPanelIndex = index;
    this.openLinkSegmentFlyPanel(index);
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
      if (this.saved[this.flyPanelIndex] === true || this.editIlaCheck) {
        this.showLinked[this.flyPanelIndex] = true;
      }
      this.length[this.flyPanelIndex] = Object.keys(
        this.selectedId[this.flyPanelIndex].data
      ).length;
    } else {
      this.selectedId[this.flyPanelIndex] = new MatTableDataSource(event);
      if (this.saved[this.flyPanelIndex] === true || this.editIlaCheck) {
        this.showLinked[this.flyPanelIndex] = true;
      }
      this.length[this.flyPanelIndex] = Object.keys(
        this.selectedId[this.flyPanelIndex].data
      ).length;
    }
  }

  openLinkSegmentFlyPanel(index: number) {
    this.segmentTitle = this.group.get('title' + index)?.value;
    if (
      this.selectedId[index] !== undefined &&
      this.selectedId[index].data !== undefined
    ) {
      this.linkedTaskIds = this.selectedId[index].data
        .filter((data) => {
          return data.type === 'Task';
        })
        .map((x) => {
          return x.id;
        });

      this.linkedEOIds = this.selectedId[index].data
        .filter((data) => {
          return data.type === 'EO';
        })
        .map((x) => {
          return x.id;
        });
      this.linkedCOIds = this.selectedId[index].data
        .filter((data) => {
          return data.type === 'Custom';
        })
        .map((x) => {
          return x.id;
        });
    }
  }

  async linkEOToSegment(index: any, id: any) {
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
    await this.segmentService.linkObjectives(id, this.segmentLinksOrder).then(async(res: any) => {
      await this.linkSegmentToILA(id, index,true);
    }).catch((err: any) => {
      this.isSaving[index] = false;
      console.error(err);
    })
  }

  async linkSegmentToILA(segId: any, index: any,isAdd:boolean) {
    var options = new ILA_Segment_LinkOptions();
    options.iLAId = this.ILAID;
    options.segmentId = segId;
    await this.ilaService.linkSegments(this.ILAID, options).then((res: any) => {
      this.isSaving[index] = true;
      this.saved[index] = true;
      this.showLinked[index] = false;
      isAdd ? this.dataBroadcastService.updateSegments.next({id:segId,todo:"ADD"}) : this.dataBroadcastService.updateSegments.next({id:segId,todo:"UPDATE"})
      this.segSaved.emit("SAVED");
      this.alert.successAlert("Segment Saved and Links created");
      this.flyPanelSrvc.close();
    }).catch((err: any) => {
      this.isSaving[index] = false;
      console.error(err);
    })
  }

  unlinkEO(node: any, index: any) {
    const indexToRemove = this.selectedId[index].data.findIndex((data: any) => {
      return data.id === node.id && data.type === node.type;
    });
    if (indexToRemove !== -1) {
      this.selectedId[index].data.splice(indexToRemove, 1);
    }
    this.selectedId[index] = new MatTableDataSource(this.selectedId[index].data);
    if (this.saved[index]) {
      this.unlinkFromBackEnd(node, index);
    }
  }

  async unlinkFromBackEnd(node: any, index: any) {
    var unlinkOptions = new Segment_ObjectiveLinkOptions();
    if (node.type === 'Task') {
      unlinkOptions.enablingObjectiveIds = [];
      unlinkOptions.segmentId = this.segmentIds[index];
      unlinkOptions.taskIds = [node.id];
    }
    else {
      unlinkOptions.enablingObjectiveIds = [node.id];
      unlinkOptions.segmentId = this.segmentIds[index];
      unlinkOptions.taskIds = [];
    }
    
    this.segmentService.unlinkObjectives(this.segmentIds[index], unlinkOptions).then((res: any) => {
      this.alert.successAlert(res);
    }).catch((err: any) => {
      this.alert.errorAlert(err);
    })
  }

  dragStarted(event: any) {
    if (this.isInvalidDragEvent) {
      document.dispatchEvent(new Event('mouseup'));
    }
  }

  cancelClicked(index: any) {
    this.segments = this.segments.filter((data, idx) => {
      return idx !== index;
    })
    this.isSaving = this.isSaving.filter((data, idx) => {
      return idx !== index;
    })
    this.saved = this.saved.filter((data, idx) => {
      return idx !== index;
    })
    this.segmentIds = this.segmentIds.filter((data, idx) => {
      return idx !== index;
    })
    this.showLinked = this.showLinked.filter((data, idx) => {
      return idx !== index
    })
    this.selectedId[index] = new MatTableDataSource();
    this.group.removeControl('title' + index)
    this.group.removeControl('description' + index)
    this.group.removeControl('duration' + index)
    this.group.removeControl('isStandard' + index)
    this.group.removeControl('isPartialCredit' + index)
    this.group.removeControl('isOperationTopic' + index)
    this.group.removeControl('isSimulation' + index)
  }

  allSegmentsSaved(): boolean {
    return this.saved.every((data) => data === true);
  }

  createOnlyLinks(index: any) {
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
    this.segmentService.linkObjectives(this.segmentIds[index], this.segmentLinksOrder).then((res: any) => {
      //this.resetValues(index);
      this.linkSegmentToILA(this.segmentIds[index], index,false);
    }).catch((err: any) => {
      this.isSaving[index] = false;
      console.error(err);
    })
  }

  readyFrom() {
    var numberOfSegments = this.segments.length;
    this.segments.push("Segment " + (numberOfSegments + 1));
    this.segmentIds.push("");
    this.group.addControl('title' + numberOfSegments, new UntypedFormControl('', Validators.required))
    this.group.addControl('description' + numberOfSegments, new UntypedFormControl('', Validators.required))
    this.group.addControl('duration' + numberOfSegments, new UntypedFormControl('', Validators.required))
    this.group.addControl('isStandard' + numberOfSegments, new UntypedFormControl(false))
    this.group.addControl('isPartialCredit' + numberOfSegments, new UntypedFormControl(false))
    this.group.addControl('isOperationTopic' + numberOfSegments, new UntypedFormControl(true))
    this.group.addControl('isSimulation' + numberOfSegments, new UntypedFormControl(false))
    this.isSaving.push(false);
    this.segmentIds.push("");
    this.saved.push(false);
    this.showLinked.push(false);
  }

  onReady(editor: any) {
    // 
    editor.plugins.get('FileRepository').createUploadAdapter = function (
      loader: any
    ) {
      return new UploadAdapter(loader);
    };
  }

  openFlyInPanel(templateRef: any, customEO: boolean) {
    this.isCustomEO = customEO;
    let portal = new TemplatePortal(templateRef, this.vcr);
    this.flyPanelSrvc.open(portal);
  }

}
