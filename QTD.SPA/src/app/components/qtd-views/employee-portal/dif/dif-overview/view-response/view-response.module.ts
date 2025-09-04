import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { BaseModule } from 'src/app/components/base/base.module';
import { LayoutModule } from '../../../../layout/layout.module';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatLegacyDialogModule as MatDialogModule } from '@angular/material/legacy-dialog';
import { MatLegacyTabsModule as MatTabsModule } from '@angular/material/legacy-tabs';
import { MatLegacyChipsModule as MatChipsModule } from '@angular/material/legacy-chips';
import { ViewResponseComponent } from './view-response.component';
import { MatToolbarModule } from '@angular/material/toolbar';
import { RouterModule, Routes } from '@angular/router';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { ReactiveFormsModule } from '@angular/forms';

const routes: Routes = [
  {
    path: '',
    component: ViewResponseComponent
  },
];


@NgModule({
  declarations: [
    ViewResponseComponent
  ],
  imports: [
    CommonModule,
    MatPaginatorModule,
    RouterModule.forChild(routes),
    MatSortModule,
    BaseModule,
    LayoutModule,
    MatMenuModule,
    MatCheckboxModule,
    MatSelectModule,
    MatButtonToggleModule,
    MatDialogModule,
    MatTabsModule,
    MatChipsModule,
    MatTableModule,
    MatToolbarModule,
    CKEditorModule,
    ReactiveFormsModule
  ],
  exports:[
    ViewResponseComponent
  ]
})
export class ViewResponseModule { }
