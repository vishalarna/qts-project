import { ILA_TopicUpdateOptions } from './../../../../../_DtoModels/ILA_Topic/ILA_TopicUpdateOptions';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import * as ckcustomBuild from 'src/app/ckcustomBuild/build/ckeditor.js';
import { ILA_TopicCreateOptions } from 'src/app/_DtoModels/ILA_Topic/ILA_TopicCreateOptions';
import { IlaService } from 'src/app/_Services/QTD/ila.service';
import { TopicService } from 'src/app/_Services/QTD/ila_topic.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { UploadAdapter } from 'src/app/_Shared/Utils/CKEditor_UploadAdapter';
import { ILA_Topic } from '@models/ILA_Topic/ILA_Topic';
import { ILATopicLinkOption } from '@models/ILA_Topic_Link/ILA_Topic_LinkOptions';
import { DynamicLabelReplacementPipe } from 'src/app/_Pipes/dynamic-label-replacement.pipe';

@Component({
  selector: 'app-fly-panel-topic',
  templateUrl: './fly-panel-topic.component.html',
  styleUrls: ['./fly-panel-topic.component.scss'],
})
export class FlyPanelTopicComponent implements OnInit {
  @Input() TopicName: string;
  @Input() change_topic: boolean;
  @Input() oldTopic: any;
  @Input() ilaIdForChange: any;
  @Input() edit_topic: boolean;
  @Input() topic_copy_mode: boolean;
  @Input() linkedTopicIds: string[];
  edit_topic_array: any = [];
  topic_Check: boolean = true;
  @Output() closed = new EventEmitter<any>();
  topics: any[] = [];
  showLoader = false;
  topicForm: UntypedFormGroup = new UntypedFormGroup({
    isPriority: new UntypedFormControl(false),
    TopicName: new UntypedFormControl('', Validators.required),
  });
  anotherCheck:boolean=false;
  public Editor = ckcustomBuild;
  newTopicIds:string[]=[];
  topicsControl = new UntypedFormControl([]);
  newTopic: any;

  constructor(
    public flyPanelSrvc: FlyInPanelService,
    private topicService: TopicService,
    private alert: SweetAlertService,
    private dataBroadcast: DataBroadcastService,
    private topicSrvc: TopicService,
    private ilaSrvc: IlaService,
    private dynamicLabelPipe:DynamicLabelReplacementPipe
  ) {}

  ngOnInit(): void {
    this.getTopics();

    this.dataBroadcast.topicSaved.subscribe((res) => {
      this.getTopics();
    });
    
  }

  onReady(editor: any) {
    // 
    editor.plugins.get('FileRepository').createUploadAdapter = function (
      loader: any
    ) {
      return new UploadAdapter(loader);
    };
  }

  getEditTopic() {
    
    
    this.topics.forEach((i) => {
      if (i.id === this.oldTopic) {
        this.edit_topic_array.push(i);

        this.topicForm.patchValue({
          isPriority: i.isPriority,
          TopicName: i.name,
        });
      }
    });
    
    
  }

  async getTopics() {
    await this.topicSrvc.getAll().then((res) => {
      this.topics = res;
      if (this.oldTopic) {
        this.newTopic = this.oldTopic;
      }
      if(this.change_topic){
        let array: any[] = [];
        this.linkedTopicIds.forEach((id) => {
          var topic = this.topics.find(x => x.id == id);
          if (topic !== undefined && topic !== null) {
            array.push(topic);
          }
        });
        this.topicsControl.setValue(array);
        this.topicsControl.updateValueAndValidity();
      }
    });

    if (this.edit_topic === true && this.oldTopic !== undefined) {
      this.getEditTopic();
    }
  }

  OnAddNewTopic() {
    this.change_topic = false;
    this.edit_topic = true;
    this.oldTopic = undefined;
  }

  OnSaveTopic() {
    if (this.edit_topic === true && this.oldTopic === undefined) {
      
      
      
      this.showLoader = true;
      var options: ILA_TopicCreateOptions = new ILA_TopicCreateOptions();
      options.name = this.topicForm.get('TopicName')?.value;
      options.isPriority = this.topicForm.get('isPriority')?.value;
      if (options.isPriority === null) {
        options.isPriority = false;
      }
      var val = this.topicService.create(options);
      
      val
        .catch((err) => {
          
          this.showLoader = false;
        })
        .then(async (res) => {
          if (res !== undefined && this.anotherCheck === true) {
            this.alert.successToast(await this.dynamicLabelPipe.transform(res?.message));
            this.anotherCheck = false;
            this.topicForm.reset();
            this.getTopics();
            this.dataBroadcast.topicSaved.next(null);
          }
          else if(this.anotherCheck === false){
            this.alert.successToast(await this.dynamicLabelPipe.transform(res?.message));
            this.getTopics();
            this.dataBroadcast.topicSaved.next(null);
            this.closed.emit(true);
           // this.flyPanelSrvc.close();
          }
          this.showLoader = false;
        })
        .finally(() => {
          this.showLoader = false;
        });
      //this.flyPanelSrvc.close();
    } else if (
      this.edit_topic === true &&
      this.oldTopic !== undefined &&
      this.topic_copy_mode === false
    ) {
      
      
      var updateOptions: ILA_TopicUpdateOptions = new ILA_TopicUpdateOptions();
      updateOptions.name = this.topicForm.get('TopicName')?.value;
      updateOptions.isPriority = this.topicForm.get('isPriority')?.value;
      if (updateOptions.isPriority === null) {
        updateOptions.isPriority = false;
      }

      this.topicService
        .update(this.oldTopic, updateOptions)
        .then((res) => {
          if (res) {
            
            this.alert.successToast('Topic Updated successfully');
            this.flyPanelSrvc.close();
            this.topicForm.reset();
            this.closed.emit(true);
          }
        })
        .catch((err) => {
          
        });
    } else if (
      this.topic_copy_mode === true &&
      this.edit_topic === true &&
      this.oldTopic !== undefined
    ) {
      
      var options: ILA_TopicCreateOptions = new ILA_TopicCreateOptions();
      var concatName = this.topicForm.get('TopicName')?.value;
      options.name = concatName.concat('-Copy');
      options.isPriority = this.topicForm.get('isPriority')?.value;
      if (options.isPriority === null) {
        options.isPriority = false;
      }
      var val = this.topicService.create(options);
      val
        .catch((err) => {
          
          this.showLoader = false;
        })
        .then((res) => {
          if (res !== undefined) {
            this.alert.successAlert(
              'Topic Copy Created',
              concatName.concat('-Copy')
            );
            this.getTopics();
            this.dataBroadcast.topicSaved.next(null);
            this.topicForm.reset();
            this.closed.emit(true);
          }
          this.showLoader = false;
        })
        .finally(() => {
          this.showLoader = false;
        });
    }
  }

  changeTopic() {
    this.showLoader = true;
    this.newTopicIds=[];
    this.topicsControl?.value.forEach((element: any) => {
      this.newTopicIds.push(element.id);
    });
    var updateLinkOptions : ILATopicLinkOption = new ILATopicLinkOption();
    updateLinkOptions.topicIds = Array.from(new Set(this.newTopicIds));
    this.ilaSrvc
      .updateLinkedILATopicsAsync(this.ilaIdForChange, updateLinkOptions)
      .then((res) => {
        if (res) {
          this.closed.emit('refreshtbl');
          this.alert.successToast('Topic Changed successfully');
          this.linkedTopicIds = this.newTopicIds;
        }
      })
      .finally(() => {
        this.showLoader = false;
      });
  }

  closeTopic() {
    if (this.newTopic === undefined) {
      // this.flyPanelSrvc.close();
      this.closed.emit('fp-topic-closed');
    } else if (this.edit_topic === true && this.oldTopic !== undefined) {
      // this.flyPanelSrvc.close();
      this.closed.emit('fp-topic-closed');
    } else {
      this.change_topic = true;
      this.edit_topic = false;
    }
  }
  removeTopic(i: any) {
    const ilaTopics = this.topicsControl.value as ILA_Topic[];
    this.removeFirst(ilaTopics, i);
    this.topicsControl.setValue(ilaTopics);
  }
  private removeFirst(array: any[], toRemove: any): void {
    const index = array.indexOf(toRemove);
    if (index !== -1) {
      array.splice(index, 1);
    }
  }
}
