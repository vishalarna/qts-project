import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BaseModule } from 'src/app/components/base/base.module';
import { FlyPanelCreateNewInstanceComponent } from './fly-panel-create-new-instance.component';
import { ReactiveFormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyFormFieldModule as MatFormFieldModule } from '@angular/material/legacy-form-field';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatLegacyOptionModule as MatOptionModule } from '@angular/material/legacy-core';


@NgModule({
  declarations: [FlyPanelCreateNewInstanceComponent],
  imports: [
    CommonModule,
    BaseModule,
    ReactiveFormsModule,
    MatCheckboxModule,
    MatFormFieldModule,
    MatSelectModule,
    MatOptionModule
  ],
  exports:[FlyPanelCreateNewInstanceComponent]
})
export class FlyPanelCreateNewInstanceModule { }
