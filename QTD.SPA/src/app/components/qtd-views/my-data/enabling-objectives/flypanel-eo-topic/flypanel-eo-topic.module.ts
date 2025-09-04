import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { BaseModule } from 'src/app/components/base/base.module';
import { FlypanelEOTopicComponent } from './flypanel-eo-topic.component';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';



@NgModule({
  declarations: [FlypanelEOTopicComponent],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    ReactiveFormsModule,
    MatCheckboxModule,
    MatSelectModule
  ],
  exports: [FlypanelEOTopicComponent]
})
export class FlypanelEOTopicModule { }
