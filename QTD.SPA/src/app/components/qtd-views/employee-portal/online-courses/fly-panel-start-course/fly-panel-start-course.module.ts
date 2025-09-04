import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelStartCourseComponent } from './fly-panel-start-course.component';
import { LayoutModule } from '../../../layout/layout.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatDialogModule } from '@angular/material/dialog';


@NgModule({
  declarations: [
    FlyPanelStartCourseComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    LayoutModule,
    MatCheckboxModule,
    FormsModule,
    MatDialogModule

  ],
  exports:[FlyPanelStartCourseComponent]
})
export class FlyPanelStartCourseModule { }
