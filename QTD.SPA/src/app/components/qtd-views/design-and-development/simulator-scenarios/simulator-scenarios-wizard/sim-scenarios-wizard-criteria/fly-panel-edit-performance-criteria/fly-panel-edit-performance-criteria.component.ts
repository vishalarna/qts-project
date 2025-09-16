import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import * as ckcustomBuild from 'src/app/ckcustomBuild/build/ckeditor.js';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { SimulatorScenario_Task_Criteria_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_Task_Criteria_VM';

@Component({
  selector: 'app-fly-panel-edit-performance-criteria',
  templateUrl: './fly-panel-edit-performance-criteria.component.html',
  styleUrls: ['./fly-panel-edit-performance-criteria.component.scss'],
})
export class FlyPanelEditPerformanceCriteriaComponent implements OnInit {
  @Output() closed = new EventEmitter<Event>();
  @Output() saveTaskCriteria = new EventEmitter<SimulatorScenario_Task_Criteria_VM>();
  @Input() taskCriteriaVM: SimulatorScenario_Task_Criteria_VM;
  @Input() mode: string;
  editCriteriaForm: UntypedFormGroup;
  editor = ckcustomBuild;
  saveDescriptionText: string;

  constructor(
    public flyPanelService: FlyInPanelService,
    public dialog: MatDialog,
    private fb: UntypedFormBuilder
  ) {}

  ngOnInit(): void {
    this.initializeEditCriteriaForm();
  }

  initializeEditCriteriaForm() {
    var criteriaValue = '';
    if (this.taskCriteriaVM.criteria) {
      criteriaValue = this.taskCriteriaVM.criteria;
    }
    this.editCriteriaForm = this.fb.group({
      criteriaDescription: [criteriaValue, Validators.required],
    });
  }

  saveTaskCriteriaAsync(templateRef:any){
    this.taskCriteriaVM.criteria = this.editCriteriaForm.get("criteriaDescription").value;
    this.saveTaskCriteria.emit(this.taskCriteriaVM);
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

}
