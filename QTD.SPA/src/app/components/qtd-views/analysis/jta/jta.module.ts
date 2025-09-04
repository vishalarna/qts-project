import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { JtaComponent } from './jta.component';
import { HttpClientModule } from '@angular/common/http';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { JTARoutingModule } from './jta-routing.module';

import { MatSidenavModule } from '@angular/material/sidenav';
import { LayoutModule } from '../../layout/layout.module';

@NgModule({
  declarations: [JtaComponent],
  imports: [
    CommonModule,
    HttpClientModule,
    JTARoutingModule,
    LocalizeModule,
    MatSidenavModule,
    LayoutModule
  ],
})
export class JtaModule {}
