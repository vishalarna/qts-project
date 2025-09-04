import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatIconModule } from '@angular/material/icon';
import { FlyPanelAddIlaComponent } from './fly-panel-add-ila-ila.component';
import { MatTreeModule } from '@angular/material/tree';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';

@NgModule({
  declarations: [FlyPanelAddIlaComponent],
  imports: [
    CommonModule,
    FormsModule,
    BaseModule,
    ReactiveFormsModule,
    MatCheckboxModule,
    MatSelectModule,
    MatIconModule,
    MatTreeModule,
    MatMenuModule,
  ],
  exports :[FlyPanelAddIlaComponent]
})
export class FlyPanelAddIlaModule {}
