import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BaseModule } from 'src/app/components/base/base.module';
import { FormsModule } from '@angular/forms';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatIconModule } from '@angular/material/icon';
import { LayoutModule } from 'src/app/components/qtd-views/layout/layout.module';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { FlyPanelAddMetaIlaComponent } from './fly-panel-add-meta-ila.component';

@NgModule({
  declarations: [FlyPanelAddMetaIlaComponent],
  imports: [
    CommonModule,
    BaseModule,
    LayoutModule,
    FormsModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatMenuModule,
    MatCheckboxModule,
    MatIconModule,
    MatToolbarModule,
    MatSelectModule
  ],
   exports:[FlyPanelAddMetaIlaComponent]
})
export class FlyPanelAddMetaIlaModule { }
