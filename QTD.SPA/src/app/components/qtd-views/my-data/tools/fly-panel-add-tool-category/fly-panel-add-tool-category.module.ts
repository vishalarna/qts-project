import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelAddToolCategoryComponent } from './fly-panel-add-tool-category.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { BaseModule } from 'src/app/components/base/base.module';

@NgModule({
  declarations: [FlyPanelAddToolCategoryComponent],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    ReactiveFormsModule,
    MatCheckboxModule,
  ],
  exports: [FlyPanelAddToolCategoryComponent],
})
export class FlyPanelAddToolCategoryModule {}
