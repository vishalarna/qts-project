import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlypanelAddTaskQualificationComponent } from './flypanel-add-task-qualification.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyRadioModule as MatRadioModule } from '@angular/material/legacy-radio';
import { MatLegacyChipsModule as MatChipsModule } from '@angular/material/legacy-chips';
import { MatLegacyFormFieldModule as MatFormFieldModule } from '@angular/material/legacy-form-field';
import { MatLegacyInputModule as MatInputModule } from '@angular/material/legacy-input';



@NgModule({
  declarations: [
    FlypanelAddTaskQualificationComponent
  ],
  imports: [
    CommonModule,
    MatChipsModule,
    BaseModule,
    MatSelectModule,
    ReactiveFormsModule,
    FormsModule,
    MatCheckboxModule,
    MatRadioModule,
    MatFormFieldModule,
    MatInputModule 
  ],
  exports : [
    FlypanelAddTaskQualificationComponent
  ]
})
export class FlypanelAddTaskQualificationModule { }
