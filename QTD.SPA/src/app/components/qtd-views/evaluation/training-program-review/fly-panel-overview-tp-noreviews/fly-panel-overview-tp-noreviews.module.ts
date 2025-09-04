import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BaseModule } from 'src/app/components/base/base.module';
import { LayoutModule } from '@angular/cdk/layout';
import { FlyPanelOverviewTpNoreviewsComponent } from './fly-panel-overview-tp-noreviews.component';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';


@NgModule({
  declarations: [
    FlyPanelOverviewTpNoreviewsComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    BaseModule,
    MatTableModule,
    LayoutModule,
    MatSortModule,
    MatPaginatorModule
  ],
  exports:[FlyPanelOverviewTpNoreviewsComponent]
})
export class FlyPanelOverviewTpNoreviewsModule { }
