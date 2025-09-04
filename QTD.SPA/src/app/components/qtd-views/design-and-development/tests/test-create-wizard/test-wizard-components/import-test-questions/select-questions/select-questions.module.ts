import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SelectQuestionsComponent } from './select-questions.component';
import { LayoutModule } from 'src/app/components/qtd-views/layout/layout.module';
import { BaseModule } from 'src/app/components/base/base.module';
import { FormsModule } from '@angular/forms';
import { MatToolbarModule } from '@angular/material/toolbar';
import { RouterModule, Routes } from '@angular/router';
import { FlypanelLinkEnablingObjectiveModule } from '../../../../flypanel-link-enabling-objective/flypanel-link-enabling-objective.module';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { FlypanelPreviewTestQuestionsModule } from '../../../../flypanel-preview-test-questions/flypanel-preview-test-questions.module';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatLegacyInputModule as MatInputModule } from '@angular/material/legacy-input';
import { MatIconModule } from '@angular/material/icon';
import { MatSortModule } from '@angular/material/sort';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';

const routes: Routes = [
  {
    path: ':id',
    component: SelectQuestionsComponent,
  },
];

@NgModule({
  declarations: [SelectQuestionsComponent],
  imports: [
    CommonModule,
    FormsModule,
    BaseModule,
    LayoutModule,
    MatToolbarModule,
    RouterModule.forChild(routes),
    FlypanelLinkEnablingObjectiveModule,
    MatTableModule,
    FlypanelPreviewTestQuestionsModule,
    MatCheckboxModule,
    MatSelectModule,
    MatSortModule,
    MatIconModule,
    MatInputModule,
    LocalizeModule,
    MatMenuModule,
  ],
  exports: [SelectQuestionsComponent]
})
export class SelectQuestionsModule { }
