import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { MatLegacyCardModule as MatCardModule } from '@angular/material/legacy-card';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatSidenavModule } from '@angular/material/sidenav';
import { BaseModule } from 'src/app/components/base/base.module';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatLegacyMenuModule as MatMenuModule} from '@angular/material/legacy-menu';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import {MatLegacyTooltipModule as MatTooltipModule} from '@angular/material/legacy-tooltip';
import {MatLegacyRadioModule as MatRadioModule} from '@angular/material/legacy-radio';
import { McqsTestComponent } from './mcqs-test.component';


@NgModule({
  declarations: [
    McqsTestComponent
  ],
  imports: [
    CommonModule,
    MatCardModule,
    DragDropModule,
    LocalizeModule,
    BaseModule,
    MatSelectModule,
    MatSidenavModule,
    FormsModule,
    ReactiveFormsModule,
    MatExpansionModule,
    MatMenuModule,
    MatCheckboxModule,
    CKEditorModule,
    MatTooltipModule,
    MatRadioModule,

  ],
  exports :[McqsTestComponent]
})
export class McqsTestModule { }
