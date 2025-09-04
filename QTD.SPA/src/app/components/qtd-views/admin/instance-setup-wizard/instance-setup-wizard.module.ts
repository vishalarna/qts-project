import {NgModule } from '@angular/core';
import { InstanceSetupWizardComponent } from './instance-setup-wizard.component';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/_Guards/auth.guard';
import { RouteGuard } from 'src/app/_Guards/route.guard';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatStepperModule } from '@angular/material/stepper';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatIconModule } from '@angular/material/icon';
import { FlyPanelCreateNewClientModule } from './fly-panel-create-new-client/fly-panel-create-new-client.module';
import { FlyPanelCreateNewInstanceModule } from './fly-panel-create-new-instance/fly-panel-create-new-instance.module';
import { FlyPanelCreateAndEditLicenseModule } from './fly-panel-create-and-edit-license/fly-panel-create-and-edit-license.module';
import { FlyPanelAddUserModule } from './fly-panel-add-user/fly-panel-add-user.module';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';

const routes: Routes = [
  {
    path: '',
    canActivate: [AuthGuard],
    component: InstanceSetupWizardComponent,
  },
];

@NgModule({
  declarations: [
    InstanceSetupWizardComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    FormsModule,    
    BaseModule,
    MatTableModule,
    MatPaginatorModule,
    MatStepperModule,
    MatToolbarModule,
    MatSelectModule,
    MatIconModule,
    ReactiveFormsModule,
    FlyPanelCreateNewClientModule,
    FlyPanelCreateNewInstanceModule,
    FlyPanelCreateAndEditLicenseModule,
    FlyPanelAddUserModule,
    MatSortModule,
    MatMenuModule,
  ],
  exports:[InstanceSetupWizardComponent]
})
export class InstanceSetupWizardModule {}
