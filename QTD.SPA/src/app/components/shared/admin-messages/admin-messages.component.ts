import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AdminMessageVM } from '@models/AdminMessage/AdminMessageVM';

@Component({
  selector: 'app-admin-messages',
  templateUrl: './admin-messages.component.html',
  styleUrls: ['./admin-messages.component.scss']
})
export class AdminMessagesComponent implements OnInit {

  @Input() messages:AdminMessageVM[];
  @Output() dismiss = new EventEmitter<string>();
  constructor() { }

  ngOnInit(): void {
    
  }
  
  onDismiss(messageId: string): void{
    this.dismiss.emit(messageId);
  }

}
