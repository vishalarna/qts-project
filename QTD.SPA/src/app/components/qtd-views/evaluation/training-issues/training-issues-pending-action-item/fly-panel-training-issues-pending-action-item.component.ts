import { Component, Input, OnInit } from '@angular/core';
import { TrainingIssue_VM } from '@models/TrainingIssues/TrainingIssue_VM';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';

@Component({
  selector: 'app-fly-panel-training-issues-pending-action-item',
  templateUrl: './fly-panel-training-issues-pending-action-item.component.html',
  styleUrls: ['./fly-panel-training-issues-pending-action-item.component.scss'],
})
export class FlyPanelTrainingIssuesPendingActionItemComponent implements OnInit {
  @Input() hasPendingActionItems:any;
  @Input() trainingIssue_Vm: TrainingIssue_VM[] =[];
  
  constructor(public flyPanelService: FlyInPanelService
  ) {}

  async ngOnInit(): Promise<void> {
  }

  closeFlyPanel() {
    this.flyPanelService.close();
  }
}
