import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelEditToolsComponent } from './fly-panel-edit-tools.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyChipsModule as MatChipsModule } from '@angular/material/legacy-chips';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { FlyPanelLinkToolsModule } from '../fly-panel-link-tools/fly-panel-link-tools.module';



@NgModule({
  declarations: [
    FlyPanelEditToolsComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    MatChipsModule,
    ReactiveFormsModule,
    MatSelectModule,
    MatTableModule,
    MatCheckboxModule,
    FlyPanelLinkToolsModule,
  ],
  exports : [
    FlyPanelEditToolsComponent,
  ]
})
export class FlyPanelEditToolsModule { }
