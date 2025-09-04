import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SupportingDocumentsComponent } from './supporting-documents.component';
import { DocumentTableModule } from 'src/app/components/qtd-views/document-storage/document-table/document-table.module';



@NgModule({
  declarations: [SupportingDocumentsComponent],
  imports: [
    CommonModule,
    DocumentTableModule
  ],
  exports:[SupportingDocumentsComponent]
})
export class SupportingDocumentsModule { }