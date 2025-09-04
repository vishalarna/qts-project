import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SelectUnlinkedQuestionsComponent } from './select-unlinked-questions.component';
import { RouterModule, Routes } from '@angular/router';
import { BaseModule } from 'src/app/components/base/base.module';
import { LayoutModule } from 'src/app/components/qtd-views/layout/layout.module';
import { FormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatIconModule } from '@angular/material/icon';
import { MatLegacyInputModule as MatInputModule } from '@angular/material/legacy-input';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { FlypanelPreviewTestQuestionsModule } from '../../../../flypanel-preview-test-questions/flypanel-preview-test-questions.module';
import { MatToolbarModule } from '@angular/material/toolbar';

const routes: Routes = [
  {
    path: ':id',
    component: SelectUnlinkedQuestionsComponent,
  },
];

@NgModule({
  declarations: [SelectUnlinkedQuestionsComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    BaseModule,
    LayoutModule,
    MatInputModule,
    MatIconModule,
    MatTableModule,
    MatCheckboxModule,
    MatMenuModule,
    FormsModule,
    FlypanelPreviewTestQuestionsModule,
    MatPaginatorModule,
    MatSortModule,
    MatToolbarModule,
  ],
  exports: [SelectUnlinkedQuestionsComponent]
})
export class SelectUnlinkedQuestionsModule { }
