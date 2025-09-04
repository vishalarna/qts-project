import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelAddSkaSubCategoryComponent } from './fly-panel-add-ska-sub-category.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { BaseModule } from 'src/app/components/base/base.module';



@NgModule({
  declarations: [
    FlyPanelAddSkaSubCategoryComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    ReactiveFormsModule,
    MatCheckboxModule,
    MatSelectModule
  ],
  exports: [FlyPanelAddSkaSubCategoryComponent]
})
export class FlyPanelAddSkaSubCategoryModule { }
