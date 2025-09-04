import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelLinkPositionR6TaskComponent } from './fly-panel-link-position-r6-task.component';
import { FormsModule } from '@angular/forms';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyDialogModule as MatDialogModule } from '@angular/material/legacy-dialog';



@NgModule({
  declarations: [
    FlyPanelLinkPositionR6TaskComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    MatTableModule,
    MatSortModule,
    MatDialogModule 
  ],
  exports: [FlyPanelLinkPositionR6TaskComponent],
})
export class FlyPanelLinkPositionR6TaskModule { }
