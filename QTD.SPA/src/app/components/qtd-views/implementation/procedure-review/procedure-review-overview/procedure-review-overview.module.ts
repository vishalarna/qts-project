import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProcedureReviewOverviewComponent } from './procedure-review-overview.component';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { LayoutModule } from '../../../layout/layout.module';
import { BaseModule } from 'src/app/components/base/base.module';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { DataTablesModule } from 'angular-datatables';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { ActiveInactiveDialogueModule } from '../../emp/active-inactive-dialogue/active-inactive-dialogue.module';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { RouterModule, Routes } from '@angular/router';
import { MatLegacyChipsModule as MatChipsModule } from '@angular/material/legacy-chips';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { FlyPanelprocedureReviewDraftModule } from './fly-panelprocedure-review-draft/fly-panelprocedure-review-draft.module';
import { FlyPanelprocedureReviewPendingModule } from './fly-panelprocedure-review-pending/fly-panelprocedure-review-pending.module';
import { UpdateProcedureDialogueModule } from '../update-procedure-dialogue/update-procedure-dialogue.module';
import { FlyPanelPublishStudentEvaluationModule } from '../../../evaluation/student-evaluation/fly-panel-publish-student-evaluation/fly-panel-publish-student-evaluation.module';
import { FlyPanelPublishedProcedureReviewModule } from './fly-panel-published-procedure-review/fly-panel-published-procedure-review.module';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';


const routes: Routes = [
  {
    path: '',
    component: ProcedureReviewOverviewComponent,

  },
];

@NgModule({
  declarations: [
    ProcedureReviewOverviewComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    LocalizeModule,
    RouterModule.forChild(routes),
    NgbDropdownModule,
    DataTablesModule,
    FormsModule,
    LayoutModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatMenuModule,
    BaseModule,
    MatCheckboxModule,
    ActiveInactiveDialogueModule,
    MatChipsModule,
    MatSelectModule,
    FlyPanelprocedureReviewDraftModule,
    FlyPanelprocedureReviewPendingModule,
    UpdateProcedureDialogueModule,
    FlyPanelPublishedProcedureReviewModule,
    MatTooltipModule

  ]
})
export class ProcedureReviewOverviewModule { }
