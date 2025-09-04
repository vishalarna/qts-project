import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlypanelViewCourseInfoComponent } from './flypanel-view-course-info.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyTabsModule as MatTabsModule } from '@angular/material/legacy-tabs';
import { MatIconModule } from '@angular/material/icon';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';



@NgModule({
  declarations: [
    FlypanelViewCourseInfoComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatTabsModule,
    MatIconModule,
    MatCheckboxModule,
    MatTableModule,
  ],
  exports: [FlypanelViewCourseInfoComponent]
})
export class FlypanelViewCourseInfoModule { }
