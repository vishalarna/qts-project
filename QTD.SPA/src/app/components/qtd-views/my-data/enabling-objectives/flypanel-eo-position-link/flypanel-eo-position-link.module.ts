import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlypanelEoPositionLinkComponent } from './flypanel-eo-position-link.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatTreeModule } from '@angular/material/tree';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { FlyPanelAddPositionModule } from '../../positions/fly-panel-add-position/fly-panel-add-position.module';



@NgModule({
  declarations: [
    FlypanelEoPositionLinkComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatTreeModule,
    MatCheckboxModule,
    MatMenuModule,
    FlyPanelAddPositionModule,
  ],
  exports : [
    FlypanelEoPositionLinkComponent,
  ]
})
export class FlypanelEoPositionLinkModule { }
