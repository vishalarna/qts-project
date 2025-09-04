import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelLinkTrainingIssuesComponent } from './fly-panel-link-training-issues.component';
import { FormsModule } from '@angular/forms';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { FlyPanelAddTrainingIssueModule } from '../fly-panel-add-training-issue/fly-panel-add-training-issue.module';
import { MatToolbarModule } from '@angular/material/toolbar';


@NgModule({
  declarations: [
    FlyPanelLinkTrainingIssuesComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    MatMenuModule,
    MatTableModule,
    MatCheckboxModule,
    FlyPanelAddTrainingIssueModule,
    MatToolbarModule
  ],
  exports:[FlyPanelLinkTrainingIssuesComponent]
})
export class FlyPanelLinkTrainingIssuesModule { }
