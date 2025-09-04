import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BaseModule } from 'src/app/components/base/base.module';
import { ReactiveFormsModule } from '@angular/forms';
import { FlyPanelAddUserComponent } from './fly-panel-add-user.component';
import { MatLegacyFormFieldModule as MatFormFieldModule } from '@angular/material/legacy-form-field';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyOptionModule as MatOptionModule } from '@angular/material/legacy-core';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';


@NgModule({
  declarations: [FlyPanelAddUserComponent],
  imports: [
    CommonModule,
    BaseModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatCheckboxModule,
    MatOptionModule,
    MatSelectModule
  ],
  exports:[FlyPanelAddUserComponent]
})
export class FlyPanelAddUserModule { }
