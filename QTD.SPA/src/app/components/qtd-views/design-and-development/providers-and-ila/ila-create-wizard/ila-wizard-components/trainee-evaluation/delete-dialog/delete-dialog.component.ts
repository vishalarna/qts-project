import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';

@Component({
  selector: 'app-delete-dialog',
  templateUrl: './delete-dialog.component.html',
  styleUrls: ['./delete-dialog.component.scss']
})
export class DeleteDialogComponent implements OnInit {

  header: string;
  description: string;
  cancelText: string;
  confirmText: string;
  constructor() { }

  ngOnInit(): void {
    this.settingDialogue();
  }

  settingDialogue() {
    
    this.header = 'Delete Trainee Evaluation';
    this.description = 'You are selecting to delete a Trainee Evaluation, this can not be undone. Are you sure you want to continue?';
    this.cancelText = '';
    this.confirmText = 'Yes';
  }


}
