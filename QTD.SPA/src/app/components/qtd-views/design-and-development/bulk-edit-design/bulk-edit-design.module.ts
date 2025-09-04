import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BulkEditDesignComponent } from './bulk-edit-design.component';
import { RouterModule, Routes } from '@angular/router';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatStepperModule } from '@angular/material/stepper';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { MatLegacyFormFieldModule as MatFormFieldModule } from '@angular/material/legacy-form-field';
import { MatLegacyInputModule as MatInputModule } from '@angular/material/legacy-input';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { FlypanelSelectEoModule } from '../tests/test-question-bank/flypanel-select-eo/flypanel-select-eo.module';
import { MatTreeModule } from '@angular/material/tree';
import { FlypanelChangeIlaModule } from '../tests/flypanel-change-ila/flypanel-change-ila.module';

const routes : Routes = [
  {
    path : ':type',
    component: BulkEditDesignComponent,
  }
]


@NgModule({
  declarations: [
    BulkEditDesignComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    BaseModule,
    MatCheckboxModule,
    MatTableModule,
    MatStepperModule,
    FormsModule,
    ReactiveFormsModule,
    MatIconModule,
    MatInputModule,
    MatSelectModule,
    MatMenuModule,
    FlypanelSelectEoModule,
    MatTreeModule,
    FlypanelChangeIlaModule,
  ],
  exports : [
    BulkEditDesignComponent,
  ]
})
export class BulkEditDesignModule { }
