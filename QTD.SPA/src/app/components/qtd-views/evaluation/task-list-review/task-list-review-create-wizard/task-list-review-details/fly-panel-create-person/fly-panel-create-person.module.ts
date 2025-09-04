import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BaseModule } from 'src/app/components/base/base.module';
import { LayoutModule } from '@angular/cdk/layout';
import { FlyPanelCreatePersonComponent } from './fly-panel-create-person.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';





@NgModule({
  declarations: [FlyPanelCreatePersonComponent],
  imports: [
    CommonModule,
    BaseModule,
    LayoutModule,
    FormsModule,
    ReactiveFormsModule
  ],
  exports:[FlyPanelCreatePersonComponent]
})
export class FlyPanelCreatePersonModule { }