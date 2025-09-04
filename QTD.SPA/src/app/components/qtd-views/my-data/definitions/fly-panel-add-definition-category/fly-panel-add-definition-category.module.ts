import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelAddDefinitionCategoryComponent } from './fly-panel-add-definition-category.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';



@NgModule({
  declarations: [
    FlyPanelAddDefinitionCategoryComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    ReactiveFormsModule,
    MatCheckboxModule
  ],
  exports: [FlyPanelAddDefinitionCategoryComponent]
})
export class FlyPanelAddDefinitionCategoryModule { }
