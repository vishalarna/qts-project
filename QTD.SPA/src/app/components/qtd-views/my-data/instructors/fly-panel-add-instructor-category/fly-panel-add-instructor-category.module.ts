import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelAddInstructorCategoryComponent } from './fly-panel-add-instructor-category.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';



@NgModule({
  declarations: [
    FlyPanelAddInstructorCategoryComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    ReactiveFormsModule,
    MatCheckboxModule
  ],
  exports: [FlyPanelAddInstructorCategoryComponent]
})
export class FlyPanelAddInstructorCategoryModule { }
