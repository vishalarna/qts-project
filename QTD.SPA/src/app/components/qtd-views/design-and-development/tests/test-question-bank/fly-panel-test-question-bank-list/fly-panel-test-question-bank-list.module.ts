import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelTestQuestionBankListComponent } from './fly-panel-test-question-bank-list.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatTreeModule } from '@angular/material/tree';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { FlypanelAddTestItemModule } from '../flypanel-add-test-item/flypanel-add-test-item.module';




@NgModule({
  declarations: [
    FlyPanelTestQuestionBankListComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatTreeModule,
    MatMenuModule,
    FlypanelAddTestItemModule
  ],
  exports: [FlyPanelTestQuestionBankListComponent]
})
export class FlyPanelTestQuestionBankListModule { }
