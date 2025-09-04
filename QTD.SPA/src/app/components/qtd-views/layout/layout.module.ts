import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './header/header.component';
import { JtaNavBarComponent } from './jta-nav-bar/jta-nav-bar.component';
import { NavBarLeftComponent } from './nav-bar-left/nav-bar-left.component';
import { FormsModule } from '@angular/forms';
import { BaseModule } from '../../base/base.module';
import { MatToolbarModule } from '@angular/material/toolbar';
import { RouterModule } from '@angular/router';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { MainNavBarComponent } from './main-nav-bar/main-nav-bar.component';
import { MatLegacyProgressSpinnerModule as MatProgressSpinnerModule } from '@angular/material/legacy-progress-spinner';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { EmpNavBarComponent } from './emp-nav-bar/emp-nav-bar.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { NavBarComponent } from './nav-bar/nav-bar.component';

@NgModule({
  declarations: [
    HeaderComponent,
    JtaNavBarComponent,
    NavBarLeftComponent,
    MainNavBarComponent,
    EmpNavBarComponent,
    NavBarComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    RouterModule,
    BaseModule,
    LocalizeModule,
    MatToolbarModule,
    MatProgressSpinnerModule,
    MatMenuModule,
    NgbModule,
    MatCheckboxModule,
  ],
  exports: [
    HeaderComponent,
    JtaNavBarComponent,
    NavBarLeftComponent,
    MainNavBarComponent,
    EmpNavBarComponent,
    NavBarComponent,
  ],
})
export class LayoutModule {}
