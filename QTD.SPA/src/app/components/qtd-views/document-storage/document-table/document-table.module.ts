import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DocumentTableComponent } from './document-table.component';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatIconModule } from '@angular/material/icon';
import { LayoutModule } from '../../layout/layout.module';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { BaseModule } from 'src/app/components/base/base.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { FlyPanelDocumentStorageModule } from '../fly-panel-document-storage/fly-panel-document-storage.module';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyCheckboxModule as MatCheckboxModule } from "@angular/material/legacy-checkbox";


@NgModule({
  declarations: [
    DocumentTableComponent
  ],
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
    FlyPanelDocumentStorageModule,
    MatSortModule,
    MatCheckboxModule
  ],
  exports: [DocumentTableComponent],
})
export class DocumentTableModule { }
