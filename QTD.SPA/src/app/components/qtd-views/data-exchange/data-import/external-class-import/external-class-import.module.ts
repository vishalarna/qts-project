import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatSidenavModule } from '@angular/material/sidenav';
import { BaseModule } from 'src/app/components/base/base.module';
import { ExternalClassImportComponent } from './external-class-import.component';
import { AuthGuard } from 'src/app/_Guards/auth.guard';
import { RouteGuard } from 'src/app/_Guards/route.guard';
import { RouterModule, Routes } from '@angular/router';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatStepperModule } from '@angular/material/stepper';
import { ImportCsvWizardModule } from 'src/app/components/shared/import-csv-wizard/import-csv-wizard.module';
import { LayoutModule } from '../../../layout/layout.module';

const routes: Routes = [
    {
      path: '',
      canActivate: [AuthGuard, RouteGuard],
      component: ExternalClassImportComponent,
    },
  ];

@NgModule({
  declarations: [ExternalClassImportComponent],
  imports: [
    CommonModule,
    MatSidenavModule,
    BaseModule,
    RouterModule.forChild(routes),
    HttpClientModule,
    LayoutModule,
    FormsModule,
    ImportCsvWizardModule,
    MatMenuModule,
    MatToolbarModule,
    MatStepperModule
    ],
  exports: [ExternalClassImportComponent]
})
export class ExternalClassImportModule { }
