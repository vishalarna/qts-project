import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';

@Component({
  selector: 'app-publish-simulator-scenario-modal',
  templateUrl: './publish-simulator-scenario-modal.component.html',
  styleUrls: ['./publish-simulator-scenario-modal.component.scss']
})
export class PublishSimulatorScenarioModalComponent implements OnInit {
  @Output() closed = new EventEmitter<Event>();
  @Output() publishScenarioValues = new EventEmitter<any>();
  publishScenarioForm: UntypedFormGroup;

  constructor(private fb: UntypedFormBuilder,  private dialogSrvc: MatDialog) { }

  ngOnInit(): void {
    this.initializePublishForm();
  }

  initializePublishForm() {
    this.publishScenarioForm = this.fb.group({
      effectiveDate: new UntypedFormControl(null, [Validators.required]),
      note: new UntypedFormControl(null),
    });
  }

  async publishScenarioFormvalue() {
      this.publishScenarioValues.emit(this.publishScenarioForm.value);
      this.dialogSrvc.closeAll();
  }
}
