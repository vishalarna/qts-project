import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { FlyPanelAddLocationCategoryComponent } from './fly-panel-add-location-category.component';




@NgModule({
  declarations: [
    FlyPanelAddLocationCategoryComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    ReactiveFormsModule,
    MatCheckboxModule
  ],
  exports: [FlyPanelAddLocationCategoryComponent]
})
export class FlyPanelAddLocationCategoryModule  { }
