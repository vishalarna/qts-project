import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DutyareaDetailComponent } from './dutyarea-detail.component';
import { Routes, RouterModule } from '@angular/router';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { FlypanelAddDutyareaModule } from '../flypanel-add-dutyarea/flypanel-add-dutyarea.module';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';

const routes: Routes = [
  {
    path: ':id',
    component: DutyareaDetailComponent,
  },
];

@NgModule({
  declarations: [DutyareaDetailComponent],
  imports: [
    CommonModule,
    BaseModule,
    MatMenuModule,
    RouterModule.forChild(routes),
    FlypanelAddDutyareaModule,
    MatTooltipModule,
  ],
})
export class DutyareaDetailModule {}
