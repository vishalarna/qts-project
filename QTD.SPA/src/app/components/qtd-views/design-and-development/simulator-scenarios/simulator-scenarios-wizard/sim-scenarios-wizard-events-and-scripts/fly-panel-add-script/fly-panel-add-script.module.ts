import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatIconModule } from '@angular/material/icon';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacyChipsModule as MatChipsModule } from '@angular/material/legacy-chips';
import { FlyPanelAddScriptComponent } from './fly-panel-add-script.component';

@NgModule({
  declarations: [FlyPanelAddScriptComponent],
  imports: [
    CommonModule,
    FormsModule,
    BaseModule,
    ReactiveFormsModule,
    MatCheckboxModule,
    MatSelectModule,
    MatTableModule,
    MatIconModule,
    MatMenuModule,
    MatChipsModule,
  ],
  exports :[FlyPanelAddScriptComponent]
})
export class FlyPanelAddScriptModule { }
