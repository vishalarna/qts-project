import { Component, EventEmitter, Input } from '@angular/core';

@Component({
  selector: 'app-email-editor',
  templateUrl: './email-editor.component.html',
  styleUrls: ['./email-editor.component.scss']
})
export class EmailEditorComponent {
  @Input() public Editor;
  
  public config  = {
    toolbar: [ 'bold', 'italic', 'link', 'numberedList', 'bulletedList', 'imageUpload', 'mediaEmbed', 'chkBox'],
    ui: {
      height: '30rem',
      resize_dir: 'vertical',
      resize_minHeight: '30rem',
      resize_enabled: false,
    }
  };

}
