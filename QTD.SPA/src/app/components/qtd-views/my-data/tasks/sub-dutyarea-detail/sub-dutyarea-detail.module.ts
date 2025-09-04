import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SubDutyareaDetailComponent } from './sub-dutyarea-detail.component';
import { FlypanelAddSubdutyareaModule } from '../flypanel-add-subdutyarea/flypanel-add-subdutyarea.module';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { Routes, RouterModule } from '@angular/router';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';

const routes: Routes = [
  {
    path: ':id',
    component: SubDutyareaDetailComponent,
  },
];

@NgModule({
  declarations: [SubDutyareaDetailComponent],
  imports: [
    CommonModule,
    BaseModule,
    MatMenuModule,
    MatTooltipModule,
    RouterModule.forChild(routes),
    FlypanelAddSubdutyareaModule,
  ],
})
export class SubDutyareaDetailModule {}
