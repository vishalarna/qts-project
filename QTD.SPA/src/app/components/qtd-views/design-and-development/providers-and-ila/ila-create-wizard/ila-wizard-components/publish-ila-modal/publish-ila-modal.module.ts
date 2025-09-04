import { NgModule } from '@angular/core';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyDialogModule as MatDialogModule } from '@angular/material/legacy-dialog';
import { FlyPanelPositionsModule } from 'src/app/components/qtd-views/implementation/positions/fly-panel-positions/fly-panel-positions.module';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { PublishIlaModalComponent } from './publish-ila-modal.component';
import { MatLegacyFormFieldModule as MatFormFieldModule } from '@angular/material/legacy-form-field';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@NgModule({
  declarations: [PublishIlaModalComponent],
  imports: [
    CommonModule,
    BaseModule,
    MatDialogModule,
    FlyPanelPositionsModule,
    MatTableModule,
    CKEditorModule,
    MatFormFieldModule,
    MatCheckboxModule,
    MatDialogModule,
    ReactiveFormsModule,
  ],
  exports: [PublishIlaModalComponent],
})
export class PublishIlaModalModule {}
