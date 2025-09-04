import { NgModule } from '@angular/core';
import { FlyPanelDocumentStorageComponent } from '../fly-panel-document-storage/fly-panel-document-storage.component';
import { CommonModule } from '@angular/common';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatIconModule } from '@angular/material/icon';
import { LayoutModule } from '../../layout/layout.module';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { BaseModule } from 'src/app/components/base/base.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';

@NgModule({
  declarations: [FlyPanelDocumentStorageComponent],
  imports: [
    CommonModule,
    MatTableModule,
    MatExpansionModule,
    MatIconModule,
    LocalizeModule,
    LayoutModule,
    BaseModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatSelectModule,
  ],
  exports: [FlyPanelDocumentStorageComponent],
})
export class FlyPanelDocumentStorageModule {}
