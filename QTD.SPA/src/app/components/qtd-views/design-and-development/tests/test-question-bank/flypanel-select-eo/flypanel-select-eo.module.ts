import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlypanelSelectEoComponent } from './flypanel-select-eo.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatTreeModule } from '@angular/material/tree';
import { MatIconModule } from '@angular/material/icon';
import { FormsModule } from '@angular/forms';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';



@NgModule({
  declarations: [
    FlypanelSelectEoComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatTreeModule,
    MatIconModule,
    FormsModule,
    MatMenuModule,
  ],
  exports : [
    FlypanelSelectEoComponent,
  ]
})
export class FlypanelSelectEoModule { }
