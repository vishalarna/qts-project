import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyButtonModule as MatButtonModule } from '@angular/material/legacy-button';
import { FlyPanelOrganizationComponent } from '../../organizations/flyPanel-organizations/flyPanel-organizations.component';
import { FlyPanelPositionsComponent } from './fly-panel-positions.component';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [FlyPanelPositionsComponent],
  imports: [
    CommonModule,
    FormsModule,
    MatMenuModule,
    BaseModule,
    MatButtonModule,
  ],
  exports: [FlyPanelPositionsComponent],
})
export class FlyPanelPositionsModule {}
