import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {ChangeEvent} from '@ckeditor/ckeditor5-angular';
import {Observable, Subscription} from 'rxjs';
import * as ckcustomBuild from "../../../../../ckcustomBuild/build/ckeditor";

@Component({
  selector: 'app-email-template-editor',
  templateUrl: './email-template-editor.component.html',
  styleUrls: ['./email-template-editor.component.scss']
})
export class EmailTemplateEditorComponent implements OnInit {
  @Input()
  emailData: string;

  @Input()
  order: number;

  @Input()
  mode: string;

  @Output()
  templateModifiedEvent: EventEmitter<any> = new EventEmitter();

  @Input() events: Observable<string>;
  private eventsSubscription: Subscription;
  editor = ckcustomBuild;

  public config = {
    toolbar: ['bold', 'italic', 'link', 'numberedList', 'bulletedList', 'imageUpload', 'mediaEmbed'],
    ui: {
      height: '30rem',
      resize_dir: 'vertical',
      resize_minHeight: '30rem',
      resize_enabled: false,
    },
  };

  constructor() {
  }

  ngOnInit(): void {
    this.eventsSubscription = this.events.subscribe((data) => {
      this.emailData = data;
    });
  }

  onEmailTemplateContentChange({editor}: ChangeEvent) {
    const data = editor.getData();
    this.templateModifiedEvent.emit({
      order: this.order,
      template: data,
    });
  }

}
