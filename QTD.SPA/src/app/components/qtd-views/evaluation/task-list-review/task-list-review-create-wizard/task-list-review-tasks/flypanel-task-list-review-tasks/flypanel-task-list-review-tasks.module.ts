import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { MatLegacyCardModule as MatCardModule } from '@angular/material/legacy-card';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatSidenavModule } from '@angular/material/sidenav';
import { BaseModule } from 'src/app/components/base/base.module';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatIconModule } from '@angular/material/icon';
import { MatTreeModule } from '@angular/material/tree';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatSortModule } from '@angular/material/sort';
import { FlypanelTaskListReviewTasksComponent } from './flypanel-task-list-review-tasks.component';
import { MatLegacyRadioModule as MatRadioModule } from '@angular/material/legacy-radio';


@NgModule({
  declarations: [FlypanelTaskListReviewTasksComponent],
  imports: [
    CommonModule,
    MatCardModule,
    DragDropModule,
    LocalizeModule,
    BaseModule,
    MatSelectModule,
    MatSidenavModule,
    FormsModule,
    ReactiveFormsModule,
    MatExpansionModule,
    MatMenuModule,
    MatCheckboxModule,
    MatIconModule,
    MatTreeModule,
    MatTableModule,
    MatSortModule,
    MatRadioModule,
  ],
  exports: [FlypanelTaskListReviewTasksComponent],
})
export class FlypanelTaskListReviewTasksModule {}
