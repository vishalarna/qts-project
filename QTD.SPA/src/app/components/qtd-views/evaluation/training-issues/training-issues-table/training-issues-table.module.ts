import { NgModule } from "@angular/core";
import { TrainingIssuesTableComponent } from "./training-issues-table.component";
import { CommonModule } from '@angular/common';
import { LayoutModule } from '../../../layout/layout.module';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatMenuModule } from '@angular/material/menu';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatSelectModule } from '@angular/material/select';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { MatIconModule } from '@angular/material/icon';
import { FlyPanelAddTrainingIssueModule } from "../../training-program-review/trainingprogramreview-wizard/trainingprogramreview-wizard-components/fly-panel-add-training-issue/fly-panel-add-training-issue.module";



@NgModule({
  declarations: [TrainingIssuesTableComponent],
  imports: [
    CommonModule,
    LayoutModule,
    BaseModule,
    MatCheckboxModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatMenuModule,
    MatToolbarModule,
    MatSelectModule,
    DragDropModule,
    MatIconModule,
    FlyPanelAddTrainingIssueModule
  ],
  exports:[TrainingIssuesTableComponent]
})
export class TrainingIssuesTableModule { }