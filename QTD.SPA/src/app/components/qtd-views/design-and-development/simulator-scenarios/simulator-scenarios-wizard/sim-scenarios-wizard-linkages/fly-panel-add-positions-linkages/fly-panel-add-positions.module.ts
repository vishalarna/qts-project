import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { BaseModule } from 'src/app/components/base/base.module';
import { FlyPanelAddPositionsLinkagesComponent } from './fly-panel-add-positions-linkages.component';

@NgModule({
  declarations: [FlyPanelAddPositionsLinkagesComponent],
  imports: [
    CommonModule,
    BaseModule,
    MatCheckboxModule,
    MatMenuModule,
    FormsModule,
  ],
  exports: [FlyPanelAddPositionsLinkagesComponent],
})
export class FlyPanelAddPositionsLinkagesModule {}
