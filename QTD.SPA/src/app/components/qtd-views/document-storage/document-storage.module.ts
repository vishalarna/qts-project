import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { DocumentStorageComponent } from './document-storage.component';
import { BaseModule } from '../../base/base.module';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatIconModule } from '@angular/material/icon';
import { LayoutModule } from '../../qtd-views/layout/layout.module';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { DocumentStorageViewModule } from './document-storage-view/document-storage-view.module';
import { DocumentStorageRoutingModule } from './document-storage-routing.module';
import { DocumentTableComponent } from './document-table/document-table.component';
import { FlyPanelDocumentStorageModule } from './fly-panel-document-storage/fly-panel-document-storage.module';

@NgModule({
  declarations: [DocumentStorageComponent],
  imports: [
    CommonModule,
    FormsModule,
    BaseModule,
    MatTableModule,
    MatExpansionModule,
    MatIconModule,
    LocalizeModule,
    LayoutModule,
    MatMenuModule,
    DocumentStorageRoutingModule,
    DocumentStorageViewModule,
    FlyPanelDocumentStorageModule
  ],
  exports: [DocumentStorageComponent],
})
export class DocumentStorageModule {}
