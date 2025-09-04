import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelCertificationsListComponent } from './fly-panel-certifications-list.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatTreeModule } from '@angular/material/tree';


@NgModule({
  declarations: [
    FlyPanelCertificationsListComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatTreeModule
  ],
  exports:[FlyPanelCertificationsListComponent]
})
export class FlyPanelCertificationsListModule { }
