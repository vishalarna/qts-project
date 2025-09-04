import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlypanelSelectIlaComponent } from './flypanel-select-ila.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatTreeModule } from '@angular/material/tree';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    FlypanelSelectIlaComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatTreeModule,
    MatMenuModule,
    FormsModule,
  ],
  exports : [
    FlypanelSelectIlaComponent,
  ]
})
export class FlypanelSelectIlaModule { }
