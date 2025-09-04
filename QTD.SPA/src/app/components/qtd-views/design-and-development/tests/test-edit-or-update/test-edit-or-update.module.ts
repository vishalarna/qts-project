import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TestEditOrUpdateComponent } from './test-edit-or-update.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { RouterModule, Routes } from '@angular/router';
import { MatLegacyRadioModule as MatRadioModule } from '@angular/material/legacy-radio';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatLegacyInputModule as MatInputModule } from '@angular/material/legacy-input';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { FlypanelAddTestItemModule } from '../test-question-bank/flypanel-add-test-item/flypanel-add-test-item.module';
import { FlypanelRandomlySelectAllEosModule } from '../test-create-wizard/test-wizard-components/import-test-questions/flypanel-randomly-select-all-eos/flypanel-randomly-select-all-eos.module';
import { FlypanelRandomlySelectByIndividualEoModule } from '../test-create-wizard/test-wizard-components/import-test-questions/flypanel-randomly-select-by-individual-eo/flypanel-randomly-select-by-individual-eo.module';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
const routes :Routes = [
  {
    path:':id',
    component: TestEditOrUpdateComponent,
  }
]

@NgModule({
  declarations: [
    TestEditOrUpdateComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatToolbarModule,
    MatIconModule,
    MatRadioModule,
    MatSelectModule,
    MatInputModule,
    FormsModule,
    NgbDropdownModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes),
    MatCheckboxModule,
    FlypanelAddTestItemModule,
    FlypanelAddTestItemModule,
    FlypanelRandomlySelectAllEosModule,
    FlypanelRandomlySelectByIndividualEoModule,
    DragDropModule,
  ],
  exports : [
    TestEditOrUpdateComponent,
  ]
})
export class TestEditOrUpdateModule { }
