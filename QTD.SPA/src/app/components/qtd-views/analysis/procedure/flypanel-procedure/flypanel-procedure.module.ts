import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlypanelProcedureComponent } from './flypanel-procedure.component';
import { FormsModule } from '@angular/forms';
import { MatLegacyProgressSpinnerModule as MatProgressSpinnerModule } from '@angular/material/legacy-progress-spinner';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatExpansionModule } from '@angular/material/expansion';

@NgModule({
  declarations: [FlypanelProcedureComponent],
  imports: [
    CommonModule,
    FormsModule,
    MatProgressSpinnerModule,
    MatMenuModule,
    BaseModule,
    MatExpansionModule,
  ],
  exports: [FlypanelProcedureComponent],
})
export class FlypanelProcedureModule {}
