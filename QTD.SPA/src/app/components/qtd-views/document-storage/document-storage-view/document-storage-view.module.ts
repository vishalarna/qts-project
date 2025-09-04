import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatIconModule } from '@angular/material/icon';
import { LayoutModule } from '../../layout/layout.module';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { BaseModule } from 'src/app/components/base/base.module';
import { DocumentStorageViewComponent } from './document-storage-view.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { DocumentTableModule } from '../document-table/document-table.module';

@NgModule({
  declarations: [DocumentStorageViewComponent],
  imports: [
    MatTableModule,
    MatExpansionModule,
    MatIconModule,
    LocalizeModule,
    LayoutModule,
    BaseModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatMenuModule,
    DocumentTableModule
  ],
  exports: [DocumentStorageViewComponent],
})
export class DocumentStorageViewModule {}
