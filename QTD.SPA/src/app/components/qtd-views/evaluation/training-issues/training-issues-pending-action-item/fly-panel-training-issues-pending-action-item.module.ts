import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BaseModule } from 'src/app/components/base/base.module';
import { LayoutModule } from '../../../layout/layout.module';
import { FlyPanelTrainingIssuesPendingActionItemComponent } from './fly-panel-training-issues-pending-action-item.component';

@NgModule({
  declarations: [FlyPanelTrainingIssuesPendingActionItemComponent],
  imports: [
    CommonModule,
    BaseModule,
    LayoutModule
  ],
  exports:[FlyPanelTrainingIssuesPendingActionItemComponent]
})
export class FlyPanelTrainingIssuesPendingActionItemModule { }
