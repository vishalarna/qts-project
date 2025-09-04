import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DifSurveyPageComponent } from './dif-survey-page.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { RouterModule, Routes } from '@angular/router';
import { LayoutModule } from 'src/app/components/qtd-views/layout/layout.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatLegacyRadioModule as MatRadioModule } from '@angular/material/legacy-radio';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatLegacyFormFieldModule as MatFormFieldModule } from '@angular/material/legacy-form-field';
import { MatLegacyInputModule as MatInputModule } from '@angular/material/legacy-input';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';


 
const routes: Routes = [
  {
    path: '',
    component: DifSurveyPageComponent
  },
];
 

@NgModule({
  declarations: [
    DifSurveyPageComponent
  ],
  imports: [
    BaseModule,
    CommonModule,
    RouterModule.forChild(routes),
    ReactiveFormsModule,
    LayoutModule,
    FormsModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatMenuModule,
    MatCheckboxModule,
    MatRadioModule,
    MatIconModule,
    MatToolbarModule,
    MatRadioModule ,
    MatExpansionModule,
    MatFormFieldModule,
    MatInputModule,
    CKEditorModule
  ]
})
export class DifSurveyPageModule { }
