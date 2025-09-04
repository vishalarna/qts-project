import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelFilterTopicComponent } from './fly-panel-filter-topic.component';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { LayoutModule } from '@angular/cdk/layout';
import { BaseModule } from 'src/app/components/base/base.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';



@NgModule({
  declarations: [FlyPanelFilterTopicComponent],
  imports: [
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    BaseModule,
    LayoutModule,
    MatSelectModule,
  ]
  , exports: [FlyPanelFilterTopicComponent]
})
export class FlyPanelFilterTopicModule { }
