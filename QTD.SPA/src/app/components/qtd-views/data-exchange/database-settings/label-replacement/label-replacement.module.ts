import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LabelReplacementComponent } from './label-replacement.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {BaseModule} from "../../../../base/base.module";

@NgModule({
  declarations: [
    LabelReplacementComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    BaseModule
  ],
  exports: [
    LabelReplacementComponent
  ]
})
export class LabelReplacementModule { }
