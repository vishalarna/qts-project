import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelScenarioComponent } from './fly-panel-scenario.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatLegacyChipsModule as MatChipsModule } from '@angular/material/legacy-chips';


@NgModule({
  declarations: [
    FlyPanelScenarioComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    LocalizeModule,
    FormsModule,
    ReactiveFormsModule,
    CKEditorModule,
    MatCheckboxModule,
    MatTableModule,
    MatSortModule,
    MatSelectModule,
    MatChipsModule
  ],
  exports:[FlyPanelScenarioComponent]
})
export class FlyPanelScenarioModule { }
