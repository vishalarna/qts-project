import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BaseModule } from 'src/app/components/base/base.module';
import { FormsModule } from '@angular/forms';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatIconModule } from '@angular/material/icon';
import { RouterModule, Routes } from '@angular/router';
import { MatToolbarModule } from '@angular/material/toolbar';
import { LayoutModule } from 'src/app/components/qtd-views/layout/layout.module';
import { ImportDifSurveyComponent } from './import-dif-survey.component';
import { ImportCsvWizardModule } from 'src/app/components/shared/import-csv-wizard/import-csv-wizard.module';
import { NgbModalModule } from '@ng-bootstrap/ng-bootstrap';

 
const routes: Routes = [
  {
    path: '',
    component: ImportDifSurveyComponent,
  }
]
 
@NgModule({
  declarations: [
    ImportDifSurveyComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    BaseModule,
    LayoutModule,
    NgbModalModule,
    FormsModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatMenuModule,
    MatCheckboxModule,
    MatIconModule,
    MatToolbarModule,
    ImportCsvWizardModule
  ],
  exports:[ImportDifSurveyComponent]
})
export class ImportDifSurveyModule { }
 