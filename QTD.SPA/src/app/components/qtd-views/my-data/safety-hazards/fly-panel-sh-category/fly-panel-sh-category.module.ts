import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelShCategoryComponent } from './fly-panel-sh-category.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { BaseModule } from 'src/app/components/base/base.module';

@NgModule({
  declarations: [FlyPanelShCategoryComponent],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    ReactiveFormsModule,
    MatCheckboxModule,
  ],
  exports: [FlyPanelShCategoryComponent],
})
export class FlyPanelShCategoryModule {}
