import {
  Component,
  OnInit,
  ViewChild,
  ViewContainerRef,
  ElementRef,
  Input,
  Output,
  EventEmitter,
  OnDestroy,
} from '@angular/core';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { TemplatePortal } from '@angular/cdk/portal';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import * as ckcustomBuild from 'src/app/ckcustomBuild/build/ckeditor.js';
import { Observable } from 'rxjs';
import { Segment } from 'src/app/_DtoModels/Segment/Segment';
import { SegmentCreateOptions } from 'src/app/_DtoModels/Segment/SegmentCreateOptions';
import { SegmentService } from 'src/app/_Services/QTD/segment.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { moveItemInArray } from '@angular/cdk/drag-drop';
import { Segment_ObjectiveLinkOptions } from 'src/app/_DtoModels/Segment_ObjectiveLink/Segment_ObjectiveLinkOptions';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { Segment_ObjectiveLink } from 'src/app/_DtoModels/Segment_ObjectiveLink/Segment_ObjectiveLink';
import { SubSink } from 'subsink';
import { Store, select } from '@ngrx/store';
import { UpdateSegmentObjectiveOrderListVM } from '@models/Segment_ObjectiveLink/UpdateSegmentObjectiveOrderListVM';
@Component({
  selector: 'app-fly-panel-content',
  templateUrl: './fly-panel-content.component.html',
  styleUrls: ['./fly-panel-content.component.scss'],
})
export class FlyPanelContentComponent implements OnInit, OnDestroy {
  flyPanelCheck: boolean = false;
  public Editor = ckcustomBuild;
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
  selectedId: any[] = [];
  displayColumns: string[] = ['drag', 'number', 'type', 'description', 'unlink'];
  DataSource: MatTableDataSource<any> = new MatTableDataSource();
  isCustomEO: boolean = false;
  addSegment: boolean = false;
  objpanel: boolean = false;
  linkedEoIds=[];
  linkedTaskIds=[];
  linkedCOIds = [];
  edited = false;
  contentData: any;
  @Output() close = new EventEmitter<any>();

  @ViewChild(MatSort) matSort!: MatSort;

  @ViewChild(MatPaginator) set paginator(paging: MatPaginator) {
    if (paging) this.DataSource.paginator = paging;
  }
  @ViewChild('UploadFileInput') uploadFileInput: ElementRef;
  myfilename = 'Select File';
  @Input() myData: any;

  showLoader = false;
  saved = false;
  subscriptions = new SubSink();
  isNercProvider = false;
  segmentLinksOrder:UpdateSegmentObjectiveOrderListVM = new UpdateSegmentObjectiveOrderListVM();

  constructor(
    public flyPanelSrvc: FlyInPanelService,
    private vcr: ViewContainerRef,
    private segmentService: SegmentService,
    private alert: SweetAlertService,
    private segService: SegmentService,
    private dataBroadcastService: DataBroadcastService,
    private saveStore: Store<{ saveIla:any }>,
  ) { }

  ngOnInit(): void {
    this.getObjectives();
    this.contentData = this.myData.content;
    this.subscriptions.sink = this.saveStore.pipe(select('saveIla')).subscribe((res:any)=>{
      if ((res?.saveData?.result !== undefined)){
        this.isNercProvider = res['saveData']?.result?.isProviderNERC;
      }
    })
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }

  onReady(editor: any) {
    //
  }

  checkPartialCredit(event:any){
    this.myData.isPartialCredit = event.checked;
  }

  unlinkEO(data: any) {
    const indexToRemove = this.DataSource.data.findIndex((val: any) => {
      return val.id === data.id && val.type === data.type;
    });
    if (indexToRemove !== -1) {
      this.DataSource.data.splice(indexToRemove, 1);
    } 
    this.DataSource = new MatTableDataSource(this.DataSource.data);
    this.DataSource.sort = this.matSort;
  }

  dropTable(event: any) {
    const prevIndex = this.DataSource.data.findIndex(
      (d: any) => d === event.item.data
    );
    moveItemInArray(this.DataSource.data, prevIndex, event.currentIndex);
    this.DataSource = new MatTableDataSource(this.DataSource.data);
    this.DataSource.sort = this.matSort;
  }

  saveContent() {
    this.showLoader = true;
    var options = new SegmentCreateOptions();
    options.title = this.myData.title;
    options.Content = this.contentData;
    options.duration = this.myData.duration;
    options.isNercOperatingTopics = this.myData.isNercOperatingTopics;
    options.isNercSimulation = this.myData.isNercSimulation;
    options.isNercStandard = this.myData.isNercStandard;
    options.isPartialCredit =this.myData.isPartialCredit;
    this.updateSegment(options);
  }

  async updateSegment(options:SegmentCreateOptions){
    await this.segmentService.update(this.myData.id, options).then(async(res: any) => {
      // this.alert.successToast("Content Updated Successfully");
      // this.showLoader = false;
      // this.close.emit("close");
      await this.updateLinks(this.myData.id);
    }).catch((err: any) => {
      this.showLoader = false;
      this.alert.errorToast("Error updating segment address");
    })
  }

  async updateLinks(id:any){
    this.segmentLinksOrder = new UpdateSegmentObjectiveOrderListVM();
    this.segmentLinksOrder.segmentObjectives = this.DataSource.data.map(
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
   await this.segmentService.linkObjectives(id,this.segmentLinksOrder).then((res:any)=>{

      this.alert.successToast("Content Updated Successfully");
      this.showLoader = false;
      this.dataBroadcastService.updateSegments.next({id:id,todo:"UPDATE"});
      this.close.emit("close");
    }).catch((err:any)=>{
      console.error(err);
    })
  }

  isEdited(event: any) {


    this.edited = true;
  }

  async getObjectives() {
    var tasks: any = [];
    var EO: any = [];
    var customEos: any = [];
    await this.segService.getLinkedObjectives(this.myData.id).then((res: Segment_ObjectiveLink[]) => {
      res.forEach((element: Segment_ObjectiveLink) => {
        if (element.task) {
          tasks.push({
            type: "Task"
            , description: element.task.description
            , id: element.task.id
            , number: element.task.subdutyArea.dutyArea.letter + element.task.subdutyArea.dutyArea.number + "." + element.task.subdutyArea.subNumber + "." + element.task.number
            ,order:element.order
            ,segmentObjId:element.id
          });
        }
        else if(element.customEnablingObjective)
        {
          customEos.push({
            type: "Custom"
            , description: element.customEnablingObjective.description
            , id: element.customEnablingObjective.id
            , number: element.customEnablingObjective.fullNumber
            ,order:element.order
            ,segmentObjId:element.id
          });
        }
        else {
          EO.push({
            type: "EO"
            , description: element.enablingObjective.description
            , id: element.enablingObjective.id
            , number: element.enablingObjective.fullNumber
            ,order:element.order
            ,segmentObjId:element.id
          });
        }
      });
      var objectiveData = tasks.concat(EO).concat(customEos);
      objectiveData = objectiveData.sort((a, b) => {
        return a.order - b.order; 
      });
      this.DataSource = new MatTableDataSource(objectiveData);
      setTimeout(() => {
        this.DataSource.sort = this.matSort;
      },100);
      this.DataSource.paginator = this.paginator;
    }).catch((err: any) => {
      console.error(err);
    });
  }

  openFlyInPanel(templateRef: any, customEO: boolean) {
    this.isCustomEO = customEO;
    this.flyPanelCheck = true
    let portal = new TemplatePortal(templateRef, this.vcr);
    this.flyPanelSrvc.open(portal);
  }

  linkObjectives() {
    this.linkedTaskIds = this.DataSource.data.filter((row) => {
      return row.type === "Task"
    }).map((task) => task.id);
    this.linkedEoIds = this.DataSource.data.filter((row) => {
      return row.type === "EO"
    }).map((eo) => eo.id);
    this.linkedCOIds = this.DataSource.data.filter((row) => {
      return row.type === "Custom"
    }).map((eo) => eo.id);
    this.objpanel = true;
  }

  openCustomObj() {
    this.isCustomEO = true;
    this.objpanel = true;
  }

  captureLinkData(event: any) {
    const objectivesData =  this.DataSource.data;
    event.forEach(val => {
      objectivesData.push(val);
    });
    this.DataSource = new MatTableDataSource<any>(objectivesData);
    setTimeout(() => {
      this.DataSource.sort = this.matSort;
    },100);
  }
}
