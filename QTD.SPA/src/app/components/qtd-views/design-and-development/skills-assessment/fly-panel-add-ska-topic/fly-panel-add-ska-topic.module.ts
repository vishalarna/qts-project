import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelAddSkaTopicComponent } from './fly-panel-add-ska-topic.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { BaseModule } from 'src/app/components/base/base.module';



@NgModule({
  declarations: [
    FlyPanelAddSkaTopicComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    ReactiveFormsModule,
    MatCheckboxModule,
    MatSelectModule
  ],
  exports: [FlyPanelAddSkaTopicComponent]
})
export class FlyPanelAddSkaTopicModule { }
