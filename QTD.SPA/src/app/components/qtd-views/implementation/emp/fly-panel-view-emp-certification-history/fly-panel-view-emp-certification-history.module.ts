import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelViewEmpCertificationHistoryComponent } from './fly-panel-view-emp-certification-history.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatSortModule } from '@angular/material/sort';

@NgModule({
  declarations: [
    FlyPanelViewEmpCertificationHistoryComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatTableModule,
    MatSortModule
  ],
  exports:[FlyPanelViewEmpCertificationHistoryComponent]
})
export class FlyPanelViewEmpCertificationHistoryModule { }
