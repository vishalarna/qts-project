import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BaseModule } from 'src/app/components/base/base.module';
import { LayoutModule } from '@angular/cdk/layout';
import { SupportingDocumentsAndIssuesComponent } from './supporting-documents-and-issues.component';
import { MatIconModule } from '@angular/material/icon';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { DocumentTableModule } from 'src/app/components/qtd-views/document-storage/document-table/document-table.module';
import { FlyPanelLinkTrainingIssuesModule } from '../fly-panel-link-training-issues/fly-panel-link-training-issues.module';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';


@NgModule({
  declarations: [
    SupportingDocumentsAndIssuesComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    BaseModule,
    ReactiveFormsModule,
    LayoutModule,
    MatIconModule,
    MatTableModule,
    MatMenuModule,
    DocumentTableModule,
    FlyPanelLinkTrainingIssuesModule,
    MatCheckboxModule
  ],
  exports:[SupportingDocumentsAndIssuesComponent]
})
export class SupportingDocumentsAndIssuesModule { }
