import { FlyPanelLinkScenarioObjectiveModule } from './../../trainee-evaluation/fly-panel-link-scenario-objective/fly-panel-link-scenario-objective.module';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelContentComponent } from './fly-panel-content.component';
import { FormsModule } from '@angular/forms';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyProgressSpinnerModule as MatProgressSpinnerModule } from '@angular/material/legacy-progress-spinner';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatLegacyFormFieldModule as MatFormFieldModule } from '@angular/material/legacy-form-field';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatLegacyButtonModule as MatButtonModule } from '@angular/material/legacy-button';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatLegacyInputModule as MatInputModule } from '@angular/material/legacy-input';
import { FlyPanelObjectivesModule } from '../../training-plan/fly-panel-objectives/fly-panel-objectives.module';
import {DragDropModule} from '@angular/cdk/drag-drop'
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    FlyPanelContentComponent
  ],
  imports: [
    //BrowserModule,
    CommonModule,
    FormsModule,
    CKEditorModule,
    MatExpansionModule,
    MatMenuModule,
    BaseModule,
    MatProgressSpinnerModule,
    MatCheckboxModule,
    MatTableModule,
    MatSortModule,
    MatPaginatorModule,
    FlyPanelObjectivesModule,
    MatFormFieldModule,
    MatSelectModule,
    MatButtonModule,
    MatToolbarModule,
    MatInputModule,
    FlyPanelLinkScenarioObjectiveModule,
    DragDropModule,
    MatTooltipModule,
    ReactiveFormsModule
  ]
  ,
  exports: [FlyPanelContentComponent],
})
export class FlyPanelContentModule { }
