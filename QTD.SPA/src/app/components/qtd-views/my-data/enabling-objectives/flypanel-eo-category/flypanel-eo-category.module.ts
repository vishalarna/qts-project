import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { BaseModule } from 'src/app/components/base/base.module';
import { FlypanelEOCategoryComponent } from './flypanel-eo-category.component';



@NgModule({
  declarations: [FlypanelEOCategoryComponent],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    ReactiveFormsModule,
    MatCheckboxModule,
  ],
  exports: [FlypanelEOCategoryComponent]
})
export class FlypanelEOCategoryModule { }
