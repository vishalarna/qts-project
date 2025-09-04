import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlypanelEoIlaLinkComponent } from './flypanel-eo-ila-link.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatTreeModule } from '@angular/material/tree';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { FlyPanelAddIlaModule } from '../../../design-and-development/providers-and-ila/fly-panel-add-ila/fly-panel-add-ila.module';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    FlypanelEoIlaLinkComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatTreeModule,
    MatCheckboxModule,
    FlyPanelAddIlaModule,
    MatMenuModule,
    FormsModule,
  ],
  exports : [FlypanelEoIlaLinkComponent]
})
export class FlypanelEoIlaLinkModule { }
