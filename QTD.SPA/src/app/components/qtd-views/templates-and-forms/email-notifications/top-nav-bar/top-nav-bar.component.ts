import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-top-nav-bar',
  templateUrl: './top-nav-bar.component.html',
  styleUrls: ['./top-nav-bar.component.scss']
})
export class TopNavBarComponent implements OnInit {
  @Input()
  mode: string

  @Input()
  enabled: boolean

  @Output() saveSettingsClicked: EventEmitter<any> = new EventEmitter();
  @Output()	editClicked: EventEmitter<any> = new EventEmitter();
  @Output()	cancelClicked: EventEmitter<any> = new EventEmitter();

  constructor() { }

  ngOnInit(): void {
  }

  onSaveSettingsClick(): void{
    this.mode = "read";
    const clickedEvent = {
      type: 'save',
      mode: this.mode
    };
    this.saveSettingsClicked.emit(clickedEvent);
  }

  onEditClick(): void{
    this.mode = "write";
    const clickedEvent = {
      type: 'save',
      mode: this.mode
    };
    this.editClicked.emit(clickedEvent);
  }

  onCancelClick(): void{
    this.mode = 'read';
    const clickedEvent = {
      type: 'cancel',
      mode: this.mode
    };
    this.cancelClicked.emit(clickedEvent);
  }

}
