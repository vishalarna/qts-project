import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelAddRecurrenceEventComponent } from './fly-panel-add-recurrence-event.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyFormFieldModule as MatFormFieldModule } from '@angular/material/legacy-form-field';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatStepperModule } from '@angular/material/stepper';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { BaseModule } from 'src/app/components/base/base.module';
import { LayoutModule } from 'src/app/components/qtd-views/layout/layout.module';
import { MatLegacyRadioModule as MatRadioModule } from '@angular/material/legacy-radio';



@NgModule({
  declarations: [
    FlyPanelAddRecurrenceEventComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    ReactiveFormsModule,
    MatCheckboxModule,
    MatFormFieldModule,
    MatSelectModule,
    MatStepperModule,
    CKEditorModule,
    MatTableModule,
    LayoutModule,
    MatRadioModule,
  ],
  exports:[FlyPanelAddRecurrenceEventComponent]
})
export class FlyPanelAddRecurrenceEventModule { }
