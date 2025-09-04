import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BaseModule } from 'src/app/components/base/base.module';
import { ReportsViewComponent } from './reports-view.component';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatIconModule } from '@angular/material/icon';
import { LayoutModule } from '../../layout/layout.module';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { MatTreeModule } from '@angular/material/tree';
import { MatLegacyListModule as MatListModule } from '@angular/material/legacy-list';



@NgModule({
  declarations: [
    ReportsViewComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatTableModule,
    MatExpansionModule,
    MatIconModule,
    LayoutModule,
    LocalizeModule,
    MatTreeModule,
    MatListModule
  ],
  exports: [
    ReportsViewComponent
  ]
})
export class ReportsViewModule { }
