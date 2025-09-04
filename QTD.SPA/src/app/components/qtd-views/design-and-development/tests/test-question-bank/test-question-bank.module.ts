import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TestQuestionBankComponent } from './test-question-bank.component';
import { RouterModule, Routes } from '@angular/router';
import { BaseModule } from 'src/app/components/base/base.module';
import { LayoutModule } from '../../../layout/layout.module';
import { MatLegacyInputModule as MatInputModule } from '@angular/material/legacy-input';
import { MatIconModule } from '@angular/material/icon';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { FormsModule } from '@angular/forms';
import { FlyPanelNewTestModule } from '../../providers-and-ila/ila-create-wizard/ila-wizard-components/trainee-evaluation/fly-panel-new-test/fly-panel-new-test.module';
import { FlypanelBulkEditTestItemsModule } from '../flypanel-bulk-edit-test-items/flypanel-bulk-edit-test-items.module';
import { FlypanelAddTestItemModule } from './flypanel-add-test-item/flypanel-add-test-item.module';
import { FlypanelSelectEoModule } from './flypanel-select-eo/flypanel-select-eo.module';
import { FlypanelSelectIlaModule } from './flypanel-select-ila/flypanel-select-ila.module';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FlypanelChangeEoModule } from './flypanel-change-eo/flypanel-change-eo.module';
import { FlyPanelTestQuestionBankListModule } from './fly-panel-test-question-bank-list/fly-panel-test-question-bank-list.module';

const route : Routes = [
  {
    path: '',
    component: TestQuestionBankComponent,
  }
]

@NgModule({
  declarations: [
    TestQuestionBankComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(route),
    BaseModule,
    LayoutModule,
    MatInputModule,
    MatIconModule,
    MatTableModule,
    MatCheckboxModule,
    MatMenuModule,
    FormsModule,
    FlypanelAddTestItemModule,
    FlypanelBulkEditTestItemsModule,
    FlypanelSelectEoModule,
    FlypanelSelectIlaModule,
    MatPaginatorModule,
    MatSortModule,
    FlypanelChangeEoModule,
    FlyPanelTestQuestionBankListModule
  ],
  exports : [
    TestQuestionBankComponent,
  ]
})
export class TestQuestionBankModule { }
