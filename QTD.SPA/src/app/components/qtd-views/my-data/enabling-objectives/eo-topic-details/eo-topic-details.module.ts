import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EoTopicDetailsComponent } from './eo-topic-details.component';
import { RouterModule, Routes } from '@angular/router';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { FlypanelEOTopicModule } from '../flypanel-eo-topic/flypanel-eo-topic.module';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';

const routes : Routes = [
  {
    path: ':id',
    component : EoTopicDetailsComponent,
  }
]

@NgModule({
  declarations: [
    EoTopicDetailsComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    BaseModule,
    MatMenuModule,
    FlypanelEOTopicModule,
    MatTooltipModule,
  ]
})
export class EoTopicDetailsModule { }
