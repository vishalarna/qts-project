import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BaseModule } from 'src/app/components/base/base.module';
import { ReactiveFormsModule } from '@angular/forms';
import { FlyPanelCreateAndEditLicenseComponent } from './fly-panel-create-and-edit-license.component';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';


@NgModule({
  declarations: [FlyPanelCreateAndEditLicenseComponent],
  imports: [
    CommonModule,
    BaseModule,
    ReactiveFormsModule,
    MatTableModule,
  ],
  exports:[FlyPanelCreateAndEditLicenseComponent]
})
export class FlyPanelCreateAndEditLicenseModule { }
