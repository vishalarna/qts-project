import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EoPositionsComponent } from './eo-positions.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { FlypanelEoPositionLinkModule } from '../../flypanel-eo-position-link/flypanel-eo-position-link.module';
import { FlypanelLinkedEosModule } from '../../flypanel-linked-eos/flypanel-linked-eos.module';



@NgModule({
  declarations: [
    EoPositionsComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatCheckboxModule,
    FlypanelEoPositionLinkModule,
    FlypanelLinkedEosModule,
  ],
  exports : [
    EoPositionsComponent,
  ]
})
export class EoPositionsModule { }
