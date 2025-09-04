import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EoProceduresComponent } from './eo-procedures.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { FlypanelEoProcedureLinkModule } from '../../flypanel-eo-procedure-link/flypanel-eo-procedure-link.module';
import { FlypanelLinkedEosModule } from '../../flypanel-linked-eos/flypanel-linked-eos.module';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';


@NgModule({
  declarations: [
    EoProceduresComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatTableModule,
    MatPaginatorModule,
    MatCheckboxModule,
    MatTooltipModule,
    FlypanelEoProcedureLinkModule,
    FlypanelLinkedEosModule,
    MatSortModule
  ],
  exports : [EoProceduresComponent]
})
export class EoProceduresModule { }
