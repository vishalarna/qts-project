import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ImportMetaILATestQuestionsComponent } from './import-meta-ila-test-questions.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { BaseModule } from 'src/app/components/base/base.module';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import {MatLegacyTooltipModule as MatTooltipModule} from '@angular/material/legacy-tooltip';



@NgModule({
  declarations: [
    ImportMetaILATestQuestionsComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    BaseModule,
    LocalizeModule,
    MatTooltipModule,
    MatCheckboxModule
  ],
  exports : [ImportMetaILATestQuestionsComponent]
})
export class ImportMetaILATestQuestionsModule { }
