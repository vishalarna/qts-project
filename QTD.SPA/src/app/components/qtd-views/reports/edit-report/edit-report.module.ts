import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BaseModule } from 'src/app/components/base/base.module';
import { EditReportComponent } from './edit-report.component';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { FilterRangeModule } from '../filter-range/filter-range.module';
import { FilterSingleModule } from '../filter-single/filter-single.module';
import { MatIconModule } from '@angular/material/icon';
import { FormsModule } from '@angular/forms';
import { ReportViewModule } from '../report-view/report-view.module';
import { FilterParentModule } from '../filter-parent/filter-parent.module';


@NgModule({
  declarations: [
    EditReportComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatIconModule,
    MatTableModule,
    FilterParentModule,
    DragDropModule,
    FormsModule,
    FilterRangeModule,
    FilterSingleModule,
    ReportViewModule
  ],
  exports: [
    EditReportComponent
  ]
})
export class EditReportModule { }
