import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlypanelRetakeStatusComponent } from './flypanel-retake-status.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';



@NgModule({
  declarations: [
    FlypanelRetakeStatusComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatTableModule,
  ],
  exports:[FlypanelRetakeStatusComponent]
})
export class FlypanelRetakeStatusModule { }
