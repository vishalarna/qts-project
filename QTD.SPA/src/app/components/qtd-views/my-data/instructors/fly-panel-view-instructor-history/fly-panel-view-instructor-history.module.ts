import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelViewInstructorHistoryComponent } from './fly-panel-view-instructor-history.component';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { BaseModule } from 'src/app/components/base/base.module';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    FlyPanelViewInstructorHistoryComponent
  ],
  imports: [
    CommonModule,
    MatPaginatorModule,
    MatSortModule,
    MatTableModule,
    BaseModule,
    FormsModule
  ],
  exports: [FlyPanelViewInstructorHistoryComponent],
})
export class FlyPanelViewInstructorHistoryModule { }
