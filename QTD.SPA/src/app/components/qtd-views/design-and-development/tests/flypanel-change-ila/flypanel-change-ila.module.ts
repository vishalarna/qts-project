import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlypanelChangeIlaComponent } from './flypanel-change-ila.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { FormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';




@NgModule({
  declarations: [
    FlypanelChangeIlaComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatSelectModule,
    FormsModule,
    MatCheckboxModule
  ],
  exports : [
    FlypanelChangeIlaComponent,
  ]
})
export class FlypanelChangeIlaModule { }
