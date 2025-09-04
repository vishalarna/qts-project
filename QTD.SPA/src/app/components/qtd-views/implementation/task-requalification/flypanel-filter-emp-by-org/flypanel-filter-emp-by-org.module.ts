import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlypanelFilterEmpByOrgComponent } from './flypanel-filter-emp-by-org.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { ReactiveFormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    FlypanelFilterEmpByOrgComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatSelectModule,
    ReactiveFormsModule,
  ],
  exports : [
    FlypanelFilterEmpByOrgComponent,
  ]
})
export class FlypanelFilterEmpByOrgModule { }
