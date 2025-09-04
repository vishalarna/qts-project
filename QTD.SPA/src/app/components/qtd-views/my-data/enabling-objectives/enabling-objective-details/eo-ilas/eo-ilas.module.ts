import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EoIlasComponent } from './eo-ilas.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { FlypanelEoIlaLinkModule } from '../../flypanel-eo-ila-link/flypanel-eo-ila-link.module';
import { FlypanelLinkedEosModule } from '../../flypanel-linked-eos/flypanel-linked-eos.module';
import { MatSortModule } from '@angular/material/sort';


@NgModule({
  declarations: [
    EoIlasComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatTableModule,
    MatPaginatorModule,
    MatCheckboxModule,
    FlypanelEoIlaLinkModule,
    FlypanelLinkedEosModule,
    MatSortModule
  ],
  exports : [EoIlasComponent]
})
export class EoIlasModule { }
