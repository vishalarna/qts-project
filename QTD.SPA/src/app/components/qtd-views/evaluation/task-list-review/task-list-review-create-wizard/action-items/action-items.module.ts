import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActionItemsComponent } from './action-items.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { LayoutModule } from '@angular/cdk/layout';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatIconModule } from '@angular/material/icon';
import { MatTreeModule } from '@angular/material/tree';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { FlypanelActionItemModule } from '../../task-review/flypanel-action-item/flypanel-action-item.module';
import { MatSortModule } from '@angular/material/sort';



@NgModule({
  declarations: [ActionItemsComponent],
  imports: [
    CommonModule,
    BaseModule,
    LayoutModule,
    MatSelectModule,
    MatExpansionModule,
    MatMenuModule,
    MatCheckboxModule,
    MatIconModule,
    MatTreeModule,
    MatTableModule,
    FlypanelActionItemModule,
    MatSortModule
  ],
  exports:[ActionItemsComponent]
})
export class ActionItemsModule { }