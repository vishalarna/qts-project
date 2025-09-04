import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelEditGradesComponent } from './fly-panel-edit-grades.component';
import { LayoutModule } from '@angular/cdk/layout';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';



@NgModule({
  declarations: [FlyPanelEditGradesComponent],
  imports: [
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    BaseModule,
    LayoutModule,
    MatSelectModule,
  ],
  exports: [FlyPanelEditGradesComponent]
})
export class FlyPanelEditGradesModule { }
