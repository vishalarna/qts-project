import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProcedureComponent } from './procedure.component';
import { FlypanelProcedureComponent } from './flypanel-procedure/flypanel-procedure.component';

@NgModule({
  declarations: [ProcedureComponent],
  imports: [CommonModule],
})
export class ProcedureModule {}
