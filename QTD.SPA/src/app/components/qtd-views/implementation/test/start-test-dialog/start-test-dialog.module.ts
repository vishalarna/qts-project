import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StartTestDialogComponent } from './start-test-dialog.component';
import { MatLegacyDialogModule as MatDialogModule } from '@angular/material/legacy-dialog';
import { BaseModule } from 'src/app/components/base/base.module';
import { LayoutModule } from '../../../layout/layout.module';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [StartTestDialogComponent],
  imports: [
    CommonModule,
    BaseModule,
    LayoutModule,
    MatDialogModule,
    MatCheckboxModule,
    FormsModule
  ]
})
export class StartTestDialogModule { }
