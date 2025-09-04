import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BaseModule } from 'src/app/components/base/base.module';
import { LayoutModule } from '@angular/cdk/layout';
import { EnrollMetaILAStudentsComponent } from './enroll-meta-ila-students.component';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { RemoveEmployeesDialogueModule } from 'src/app/components/qtd-views/implementation/schedulingclasses/scheduling-classes-overview/remove-employees-dialogue/remove-employees-dialogue.module';


@NgModule({
  declarations: [
    EnrollMetaILAStudentsComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    BaseModule,
    ReactiveFormsModule,
    LayoutModule,
    MatTableModule,
    MatCheckboxModule,
    RemoveEmployeesDialogueModule
  ],
  exports:[EnrollMetaILAStudentsComponent]
})
export class EnrollMetaILAStudentsModule { }
