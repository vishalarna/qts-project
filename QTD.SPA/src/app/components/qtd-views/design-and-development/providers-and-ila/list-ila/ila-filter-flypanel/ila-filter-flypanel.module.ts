import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IlaFilterFlypanelComponent } from './ila-filter-flypanel.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [IlaFilterFlypanelComponent],
  imports: [
    BaseModule,
    CommonModule,
    MatCheckboxModule,
    FormsModule
  ],
  exports:[
    IlaFilterFlypanelComponent
  ]
})
export class IlaFilterFlypanelModule { }
