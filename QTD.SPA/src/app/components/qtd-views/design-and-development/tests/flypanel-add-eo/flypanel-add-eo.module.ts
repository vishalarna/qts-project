import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlypanelAddEoComponent } from './flypanel-add-eo.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatTreeModule } from '@angular/material/tree';
import { MatIconModule } from '@angular/material/icon';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    FlypanelAddEoComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    MatTreeModule,
    MatIconModule,
    MatMenuModule,
    MatCheckboxModule,
  ],
  exports : [
    FlypanelAddEoComponent
  ]
})
export class FlypanelAddEoModule { }
