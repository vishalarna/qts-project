import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-ila-topic-delete',
  templateUrl: './ila-topic-delete.component.html',
  styleUrls: ['./ila-topic-delete.component.scss']
})
export class IlaTopicDeleteComponent implements OnInit {

  header: string;
  description: string;
  cancelText: string;
  confirmText: string;
  constructor() { }

  ngOnInit(): void {
    this.settingDialogue();
  }

  settingDialogue() {
    
    this.header = 'Delete Topic';
    this.description = 'You are selecting to Delete the selected ILA Topic. Are you sure you want to continue?';
    this.cancelText = '';
    this.confirmText = 'Yes';
  }

}
