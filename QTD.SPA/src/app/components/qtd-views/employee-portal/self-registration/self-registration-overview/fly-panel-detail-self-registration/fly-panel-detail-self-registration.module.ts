import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelDetailSelfRegistrationComponent } from './fly-panel-detail-self-registration.component';
import { RouterModule, Routes } from '@angular/router';
import { BaseModule } from 'src/app/components/base/base.module';
import { LayoutModule } from 'src/app/components/qtd-views/layout/layout.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyChipsModule as MatChipsModule } from '@angular/material/legacy-chips';
import { MatIconModule } from '@angular/material/icon';
import { MatLegacyRadioModule as MatRadioModule } from '@angular/material/legacy-radio';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyInputModule as MatInputModule } from '@angular/material/legacy-input';


const routes: Routes = [
  {
    path: '',
    component: FlyPanelDetailSelfRegistrationComponent,
  },
  {
    path: ':classId/:ilaId',
    component: FlyPanelDetailSelfRegistrationComponent,
  },
];
@NgModule({
  declarations: [
    FlyPanelDetailSelfRegistrationComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    CommonModule,
    BaseModule,
    LayoutModule,
    FormsModule,
    ReactiveFormsModule,
    MatToolbarModule,
    MatIconModule,
    MatToolbarModule,
    MatCheckboxModule,
    MatChipsModule,
    MatTableModule,
    MatInputModule,
  ]
})
export class FlyPanelDetailSelfRegistrationModule { }
