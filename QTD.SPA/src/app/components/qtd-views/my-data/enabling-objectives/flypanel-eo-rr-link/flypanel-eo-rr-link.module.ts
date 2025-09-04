import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlypanelEoRrLinkComponent } from './flypanel-eo-rr-link.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatTreeModule } from '@angular/material/tree';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { FormsModule } from '@angular/forms';
import { FlyPanelAddRrModule } from '../../reg-requirements/fly-panel-add-rr/fly-panel-add-rr.module';



@NgModule({
  declarations: [
    FlypanelEoRrLinkComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatMenuModule,
    MatTreeModule,
    MatCheckboxModule,
    FormsModule,
    FlyPanelAddRrModule,
  ],
  exports : [FlypanelEoRrLinkComponent]
})
export class FlypanelEoRrLinkModule { }
