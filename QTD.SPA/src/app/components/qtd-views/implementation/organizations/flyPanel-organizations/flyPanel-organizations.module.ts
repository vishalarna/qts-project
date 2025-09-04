import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelOrganizationComponent } from './flyPanel-organizations.component';
import { RouterModule, Routes } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';

import { FormsModule } from '@angular/forms';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { BaseModule } from 'src/app/components/base/base.module';

@NgModule({
  declarations: [FlyPanelOrganizationComponent],
  imports: [
    CommonModule,
    HttpClientModule,
    FormsModule,
    LocalizeModule,
    MatMenuModule,
    BaseModule,
  ],
  exports: [FlyPanelOrganizationComponent],
})
export class FlyPanelOrganizationModule {}
