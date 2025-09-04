import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EoRegulatoryRequirementsComponent } from './eo-regulatory-requirements.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { FlypanelEoRrLinkModule } from '../../flypanel-eo-rr-link/flypanel-eo-rr-link.module';
import { FlypanelLinkedEosModule } from '../../flypanel-linked-eos/flypanel-linked-eos.module';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';


@NgModule({
  declarations: [
    EoRegulatoryRequirementsComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatTableModule,
    MatCheckboxModule,
    MatTooltipModule,
    MatPaginatorModule,
    FlypanelEoRrLinkModule,
    FlypanelLinkedEosModule,
    MatSortModule
  ],
  exports : [EoRegulatoryRequirementsComponent]
})
export class EoRegulatoryRequirementsModule { }
