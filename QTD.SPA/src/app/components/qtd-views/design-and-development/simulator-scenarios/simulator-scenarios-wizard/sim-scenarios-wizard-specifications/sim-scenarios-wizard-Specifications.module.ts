import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatIconModule } from '@angular/material/icon';
import { SimScenariosWizardSpecificationsComponent } from './sim-scenarios-wizard-specifications.component';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';

@NgModule({
  declarations: [SimScenariosWizardSpecificationsComponent],
  imports: [
    CommonModule,
    FormsModule,
    BaseModule,
    ReactiveFormsModule,
    MatCheckboxModule,
    MatTableModule,
    MatIconModule,
    CKEditorModule
  ],
  exports: [SimScenariosWizardSpecificationsComponent]
})
export class SimScenariosWizardSpecificationsModule { }
