import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlypanelEoTaskLinkComponent } from './flypanel-eo-task-link.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatTreeModule } from '@angular/material/tree';
import { FormsModule } from '@angular/forms';
import { FlypanelAddTaskModule } from '../../tasks/flypanel-add-task/flypanel-add-task.module';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';



@NgModule({
  declarations: [
    FlypanelEoTaskLinkComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatMenuModule,
    MatTreeModule,
    MatCheckboxModule,
    FormsModule,
    FlypanelAddTaskModule,
  ],
  exports : [FlypanelEoTaskLinkComponent]
})
export class FlypanelEoTaskLinkModule { }
