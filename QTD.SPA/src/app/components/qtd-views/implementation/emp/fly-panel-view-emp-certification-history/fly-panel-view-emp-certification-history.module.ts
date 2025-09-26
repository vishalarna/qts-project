import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelViewEmpCertificationHistoryComponent } from './fly-panel-view-emp-certification-history.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatSortModule } from '@angular/material/sort';
import { FlyPanelCertificationHistoryModule } from '../fly-panel-certification-history/fly-panel-certification-history.module';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';

@NgModule({
  declarations: [
    FlyPanelViewEmpCertificationHistoryComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatTableModule,
    MatSortModule,
    FlyPanelCertificationHistoryModule,
    MatSortModule,
    MatMenuModule,
    MatCheckboxModule,
  ],
  exports:[FlyPanelViewEmpCertificationHistoryComponent]
})
export class FlyPanelViewEmpCertificationHistoryModule { }
