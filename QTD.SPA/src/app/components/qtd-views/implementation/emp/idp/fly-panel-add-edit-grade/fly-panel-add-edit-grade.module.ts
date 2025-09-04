import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelAddEditGradeComponent } from './fly-panel-add-edit-grade.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';



@NgModule({
  declarations: [
    FlyPanelAddEditGradeComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    MatCheckboxModule,
    FormsModule,
    ReactiveFormsModule,
    MatTooltipModule
  ],
  
  exports:[FlyPanelAddEditGradeComponent]
})
export class FlyPanelAddEditGradeModule { }
