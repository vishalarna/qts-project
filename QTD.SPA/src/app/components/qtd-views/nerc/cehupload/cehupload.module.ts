import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CehuploadComponent } from './cehupload.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LayoutModule } from '../../layout/layout.module';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatLegacyRadioModule as MatRadioModule } from '@angular/material/legacy-radio';
import { RouterModule, Routes } from '@angular/router';
import { MatLegacyDialogModule as MatDialogModule } from '@angular/material/legacy-dialog';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatIconModule } from '@angular/material/icon';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { CehUploadExportModule } from '../ceh-upload-export/ceh-upload-export.module';

const routes: Routes = [
  {
    path: '',
    component: CehuploadComponent,
    children: []
  }];


@NgModule({
  declarations: [CehuploadComponent],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    LayoutModule,
    RouterModule.forChild(routes),
    ReactiveFormsModule,
    MatSelectModule,
    MatCheckboxModule,
    MatRadioModule,
    MatDialogModule,
    MatTableModule,
    MatIconModule,
    CehUploadExportModule
  ]
})
export class CehuploadModule { }
