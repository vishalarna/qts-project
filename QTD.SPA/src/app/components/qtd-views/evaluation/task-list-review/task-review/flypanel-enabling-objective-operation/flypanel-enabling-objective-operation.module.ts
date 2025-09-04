import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatLegacyRadioModule as MatRadioModule } from '@angular/material/legacy-radio';
import { LayoutModule } from '@angular/cdk/layout';
import { FlypanelEnablingObjectiveOperationComponent } from './flypanel-enabling-objective-operation.component';


@NgModule({
  declarations: [FlypanelEnablingObjectiveOperationComponent],
  imports: [
    CommonModule,
    MatTableModule,
    FormsModule,
    LayoutModule,
    BaseModule,
    MatMenuModule,
    MatCheckboxModule,
    MatToolbarModule,
    MatSelectModule,
    MatRadioModule,
    ReactiveFormsModule
  ],
  exports:[FlypanelEnablingObjectiveOperationComponent]
})
export class FlypanelEnablingObjectiveOperationModule { }
