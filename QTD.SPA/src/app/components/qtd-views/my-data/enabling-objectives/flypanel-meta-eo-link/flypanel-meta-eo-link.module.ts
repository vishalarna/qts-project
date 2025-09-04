import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlypanelMetaEoLinkComponent } from './flypanel-meta-eo-link.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatTreeModule } from '@angular/material/tree';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { FlypanelAddEoModule } from '../flypanel-add-eo/flypanel-add-eo.module';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    FlypanelMetaEoLinkComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatTreeModule,
    MatMenuModule,
    MatCheckboxModule,
    FlypanelAddEoModule,
    FormsModule,
  ],
  exports : [
    FlypanelMetaEoLinkComponent,
  ]
})
export class FlypanelMetaEoLinkModule { }
