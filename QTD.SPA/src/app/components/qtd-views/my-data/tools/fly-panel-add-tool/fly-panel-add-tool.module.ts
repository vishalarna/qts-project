import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelAddToolComponent } from './fly-panel-add-tool.component';
import { FlyPanelAddToolCategoryModule } from '../fly-panel-add-tool-category/fly-panel-add-tool-category.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyChipsModule as MatChipsModule } from '@angular/material/legacy-chips';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatStepperModule } from '@angular/material/stepper';
import { BaseModule } from 'src/app/components/base/base.module';
import { DocumentTableModule } from '../../../document-storage/document-table/document-table.module';

@NgModule({
  declarations: [FlyPanelAddToolComponent],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    ReactiveFormsModule,
    MatStepperModule,
    MatSelectModule,
    MatChipsModule,
    MatCheckboxModule,
    FlyPanelAddToolCategoryModule,
    DocumentTableModule
  ],
  exports: [FlyPanelAddToolComponent],
})
export class FlyPanelAddToolModule {}
