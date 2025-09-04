import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { TrainingIssue_VM } from '@models/TrainingIssues/TrainingIssue_VM';
import { TrainingIssueActionItemsTableComponent } from '../../training-issue-action-items-table/training-issue-action-items-table.component';
import * as ckcustomBuild from 'src/app/ckcustomBuild/build/ckeditor.js';
import { UntypedFormBuilder, UntypedFormGroup } from '@angular/forms';
import { CKEditorComponent } from '@ckeditor/ckeditor5-angular';

@Component({
  selector: 'app-training-issues-action-items',
  templateUrl: './training-issues-action-items.component.html',
  styleUrls: ['./training-issues-action-items.component.scss']
})
export class TrainingIssuesActionItemsComponent implements OnInit {

  @ViewChild('actionItemTable') actionItemTable: TrainingIssueActionItemsTableComponent;
  @ViewChild(CKEditorComponent) ckeditorComponent?: CKEditorComponent;
  @Input() inputTrainingIssue_Vm: TrainingIssue_VM ;
  public Editor = ckcustomBuild;
  plannedResponseForm: UntypedFormGroup;
  constructor(private fb: UntypedFormBuilder) { }

  ngOnInit(): void {
    this.initializeForm();
  }
  
  onEditorReady(editorInstance: any) {
      editorInstance.editing.view.document.on('keydown', (event: any, data: any) => {
        if (data.keyCode === 9) { // Tab key
          data.preventDefault(); // Prevent focus shift
          // Insert a tab character at the cursor position
          editorInstance.model.change((writer: any) => {
            const insertPosition = editorInstance.model.document.selection.getFirstPosition();
            writer.insertText('    ', insertPosition);
          });
        }
      });
    }

  initializeForm(): void {
    this.plannedResponseForm = this.fb.group({
      plannedResponse: [this.inputTrainingIssue_Vm?.plannedResponse || '']
    });
  }

  onPlannedResponseChange(e: any): void {
    const response = this.plannedResponseForm.get('plannedResponse').value;
    if (this.inputTrainingIssue_Vm) {
      this.inputTrainingIssue_Vm.plannedResponse = response;
    }
  }
}
