import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelCertifyingBodyComponent } from './fly-panel-certifying-body.component';
import { FormsModule } from '@angular/forms';
import { MatLegacyButtonModule as MatButtonModule } from '@angular/material/legacy-button';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { BaseModule } from 'src/app/components/base/base.module';

@NgModule({
  declarations: [FlyPanelCertifyingBodyComponent],
  imports: [
    CommonModule,
    FormsModule,
    MatMenuModule,
    BaseModule,
    MatButtonModule,
  ],
  exports: [FlyPanelCertifyingBodyComponent],
})
export class FlyPanelCertifyingBodyModule {}
