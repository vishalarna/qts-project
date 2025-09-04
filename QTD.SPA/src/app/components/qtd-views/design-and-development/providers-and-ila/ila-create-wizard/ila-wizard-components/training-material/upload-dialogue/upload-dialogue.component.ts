import { Component, OnInit } from '@angular/core';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';

@Component({
  selector: 'app-upload-dialogue',
  templateUrl: './upload-dialogue.component.html',
  styleUrls: ['./upload-dialogue.component.scss']
})
export class UploadDialogueComponent implements OnInit {
  csvLoaded: boolean = false;
  files: File[] = [];
  constructor( public flyPanelSrvc: FlyInPanelService,) { }

  ngOnInit(): void {
  }

  onSelect(event: any) {
    
    this.files.push(...event.addedFiles);
  }

  onRemove(event : any) {
    
    this.files.splice(this.files.indexOf(event), 1);
  }
}
