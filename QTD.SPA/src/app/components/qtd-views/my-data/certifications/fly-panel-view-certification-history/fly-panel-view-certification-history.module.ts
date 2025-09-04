import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { BaseModule } from 'src/app/components/base/base.module';
import { FlyPanelViewCertificationHistoryComponent } from './fly-panel-view-certification-history.component';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    FlyPanelViewCertificationHistoryComponent
  ],
  imports: [
    CommonModule,
    MatPaginatorModule,
    MatSortModule,
    MatTableModule,
    BaseModule,
    FormsModule
  ]
  ,
  exports: [FlyPanelViewCertificationHistoryComponent],
})
export class FlyPanelViewCertificationHistoryModule { }
