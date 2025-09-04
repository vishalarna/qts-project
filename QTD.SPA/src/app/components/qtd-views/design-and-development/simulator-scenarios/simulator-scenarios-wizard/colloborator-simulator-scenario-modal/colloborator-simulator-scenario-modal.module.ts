import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ColloboratorSimulatorScenarioModalComponent } from './colloborator-simulator-scenario-modal.component';
import { ReactiveFormsModule } from '@angular/forms';
import { LayoutModule } from 'src/app/components/qtd-views/layout/layout.module';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatLegacyOptionModule as MatOptionModule } from '@angular/material/legacy-core';
import { MatLegacyDialogModule as MatDialogModule } from '@angular/material/legacy-dialog';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';



@NgModule({
  declarations: [ColloboratorSimulatorScenarioModalComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    BaseModule,
    LayoutModule,
    MatDialogModule,
    MatSelectModule,
    MatOptionModule,
    MatTableModule,
    CKEditorModule
  ],
  exports:[ColloboratorSimulatorScenarioModalComponent]
})
export class ColloboratorSimulatorScenarioModalModule { }
