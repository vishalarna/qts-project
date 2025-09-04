import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { TopicService } from 'src/app/_Services/QTD/ila_topic.service';

@Component({
  selector: 'app-fly-panel-filter-topic',
  templateUrl: './fly-panel-filter-topic.component.html',
  styleUrls: ['./fly-panel-filter-topic.component.scss']
})


export class FlyPanelFilterTopicComponent implements OnInit {
  showSpinner = false;
  topic_list:any;
  @Output() closed = new EventEmitter<any>();
  topicForm: UntypedFormGroup = new UntypedFormGroup({
    topicId: new UntypedFormControl('', Validators.required),
  });
  constructor(private topicService:TopicService) { }
  @Output() idSelected = new EventEmitter<any>();
  @Output() topicSelected = new EventEmitter<any>();
  selectedText:string;
  ngOnInit(): void {
    this.getTopics();
  }

  async getTopics() {
    await this.topicService
      .getAll()
      .then((res) => {
        this.topic_list = res;
        this.topic_list=this.topic_list.filter(x=>x.active===true);
      })
      .catch((err) => {
        console.error(err);
      })
      .finally(() => {
        this.showSpinner = false;
      });
  }
  closeProvider() {
    this.closed.emit('fp-add-provider-closed');
  }
  selectTopic(){
    this.idSelected.emit(this.topicForm.get('topicId')?.value);

  }
  onOptionSelection(event: any): void {
    
    this.selectedText = event.source.triggerValue;
  }
  selectedTopicName(){
    this.topicSelected.emit(this.selectedText);

  }
}
