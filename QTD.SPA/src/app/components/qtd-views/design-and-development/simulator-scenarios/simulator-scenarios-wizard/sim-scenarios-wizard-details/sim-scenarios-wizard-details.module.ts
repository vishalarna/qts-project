import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { RouterModule, Routes } from '@angular/router';
import { SimScenariosWizardDetailsComponent } from './sim-scenarios-wizard-details.component';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';

const routes: Routes = [
  {
    path: '',
    component: SimScenariosWizardDetailsComponent,
  },
];

@NgModule({
  declarations: [SimScenariosWizardDetailsComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    FormsModule,
    BaseModule,
    ReactiveFormsModule,
    MatCheckboxModule,
    MatSelectModule,
    CKEditorModule
  ],
  exports :[SimScenariosWizardDetailsComponent]
})
export class SimScenariosWizardDetailsModule {}
