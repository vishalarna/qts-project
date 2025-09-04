import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EvaluationComponent } from './evaluation.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { RouterModule, Routes } from '@angular/router';
import { LayoutModule } from '../../layout/layout.module';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatLegacyTabsModule as MatTabsModule } from '@angular/material/legacy-tabs';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { DisclaimerDialogModule } from './disclaimer-dialog/disclaimer-dialog.module';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';

const routes:Routes = [
  {
    path:'',
    component:EvaluationComponent,
  }
]

@NgModule({
  declarations: [
    EvaluationComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    RouterModule.forChild(routes),
    LayoutModule,
    MatSidenavModule,
    MatTabsModule,
    MatTableModule,
    DisclaimerDialogModule,
    MatSortModule,
    MatPaginatorModule,
    MatTooltipModule,
  ],
  exports:[
    EvaluationComponent
  ]
})
export class EvaluationModule { }
