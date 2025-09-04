import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelStartTestComponent } from './fly-panel-start-test.component';
import { RouterModule, Routes } from '@angular/router';
import { LayoutModule } from '@angular/cdk/layout';
import { BaseModule } from 'src/app/components/base/base.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatLegacyRadioModule as MatRadioModule } from '@angular/material/legacy-radio';
import { SanitizeHtml } from './SanitizeHtml';
import { MatLegacyChipsModule as MatChipsModule } from '@angular/material/legacy-chips';
import { SubmitTestDialogueModule } from '../submit-test-dialogue/submit-test-dialogue.module';

const routes: Routes = [
  {
    path: '',
    component: FlyPanelStartTestComponent,
  },
  {
    path: ':testId',
    component: FlyPanelStartTestComponent,
  },
];

@NgModule({
  declarations: [
    FlyPanelStartTestComponent,
    SanitizeHtml
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    CommonModule,
    BaseModule,
    LayoutModule,
    FormsModule,
    ReactiveFormsModule,
    MatSelectModule,
    MatToolbarModule,
    MatCheckboxModule,
    MatIconModule,
    MatRadioModule,
    MatToolbarModule,
    MatCheckboxModule,
    MatChipsModule,
    SubmitTestDialogueModule
  ]
})
export class FlyPanelStartTestModule { }
