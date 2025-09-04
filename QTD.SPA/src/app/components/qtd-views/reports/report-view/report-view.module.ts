import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BaseModule } from 'src/app/components/base/base.module';
import { ReportViewComponent } from './report-view.component';
import { MatIconModule } from '@angular/material/icon';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    ReportViewComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatIconModule,
    FormsModule
  ],
  exports: [
    ReportViewComponent  ]
})
export class ReportViewModule { }
