import { Component, Input, OnInit } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { Router } from '@angular/router';
import { SimulatorScenario_Task_Criteria_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_Task_Criteria_VM';

@Component({
  selector: 'app-edit-criteria-modal',
  templateUrl: './edit-criteria-modal.component.html',
  styleUrls: ['./edit-criteria-modal.component.scss']
})
export class EditCriteriaModalComponent implements OnInit {
  @Input() taskCriteriaVM: SimulatorScenario_Task_Criteria_VM;

  constructor(private dialogSrvc: MatDialog, private router: Router) { }

  ngOnInit(): void {
  }

  goToTaskView(){
    this.router.navigate([`/my-data/tasks/detail/${this.taskCriteriaVM?.taskId}-${this.taskCriteriaVM?.completeTaskNumber}`]);
    this.dialogSrvc.closeAll();
  }
  backToScenario(){
    this.dialogSrvc.closeAll();
  }
}
