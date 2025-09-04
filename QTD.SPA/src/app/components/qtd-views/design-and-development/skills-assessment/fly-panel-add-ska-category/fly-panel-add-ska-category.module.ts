import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelAddSkaCategoryComponent } from './fly-panel-add-ska-category.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { BaseModule } from 'src/app/components/base/base.module';



@NgModule({
  declarations: [
    FlyPanelAddSkaCategoryComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    ReactiveFormsModule,
    MatCheckboxModule,
  ],
  exports:[FlyPanelAddSkaCategoryComponent]
})
export class FlyPanelAddSkaCategoryModule { }
