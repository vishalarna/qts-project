import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EditCriteriaModalComponent } from './edit-criteria-modal.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BaseModule } from 'src/app/components/base/base.module';
import { LayoutModule } from 'src/app/components/qtd-views/layout/layout.module';
import { MatLegacyDialogModule as MatDialogModule } from '@angular/material/legacy-dialog';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';

@NgModule({
  declarations: [EditCriteriaModalComponent],
  imports: [
    CommonModule,
    FormsModule,
    BaseModule,
    ReactiveFormsModule,
    LayoutModule,
    MatDialogModule,
    MatSelectModule
  ],
  exports :[EditCriteriaModalComponent]
})
export class EditCriteriaModalModule { }
