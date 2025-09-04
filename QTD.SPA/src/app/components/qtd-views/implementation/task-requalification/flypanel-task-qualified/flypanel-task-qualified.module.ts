import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlypanelTaskQualifiedComponent } from './flypanel-task-qualified.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';



@NgModule({
  declarations: [
    FlypanelTaskQualifiedComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatTableModule,
  ],
  exports : [
    FlypanelTaskQualifiedComponent
  ]
})
export class FlypanelTaskQualifiedModule { }
