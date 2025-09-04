import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlypanelEOSubCategoryComponent } from './flypanel-eo-sub-category.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';



@NgModule({
  declarations: [FlypanelEOSubCategoryComponent],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    ReactiveFormsModule,
    MatCheckboxModule,
    MatSelectModule
  ],
  exports: [FlypanelEOSubCategoryComponent]
})
export class FlypanelEOSubCategoryModule { }
