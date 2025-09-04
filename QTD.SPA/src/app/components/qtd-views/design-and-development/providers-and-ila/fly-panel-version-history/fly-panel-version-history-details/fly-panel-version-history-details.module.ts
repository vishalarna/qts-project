import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BaseModule } from 'src/app/components/base/base.module';
import { ReactiveFormsModule } from '@angular/forms';
import { FlyPanelVersionHistoryDetailsComponent } from './fly-panel-version-history-details.component';



@NgModule({
  declarations: [FlyPanelVersionHistoryDetailsComponent],
  imports: [
    CommonModule,
    BaseModule,
    ReactiveFormsModule
  ],
  exports:[
    FlyPanelVersionHistoryDetailsComponent
  ]
})
export class FlyPanelVersionHistoryDetailsModule { }
