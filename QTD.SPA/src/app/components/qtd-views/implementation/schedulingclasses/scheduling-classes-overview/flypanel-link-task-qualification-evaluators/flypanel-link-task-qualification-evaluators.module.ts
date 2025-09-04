import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlypanelLinkTaskQualificationEvaluatorsComponent } from './flypanel-link-task-qualification-evaluators.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatToolbarModule } from '@angular/material/toolbar';



@NgModule({
  declarations: [
    FlypanelLinkTaskQualificationEvaluatorsComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    ReactiveFormsModule,
    MatSelectModule,
    MatCheckboxModule,
    MatMenuModule,
    MatToolbarModule,
  ],
  exports : [
    FlypanelLinkTaskQualificationEvaluatorsComponent,
  ]
})
export class FlypanelLinkTaskQualificationEvaluatorsModule { }
