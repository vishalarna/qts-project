import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BaseModule } from '../../base/base.module';
import { FormsModule } from '@angular/forms';
import { NercRoutingModule } from './nerc-routing.module';
import { CehuploadModule } from './cehupload/cehupload.module';
import { NercComponent } from './nerc.component';
  
@NgModule({
  declarations: [
    NercComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    NercRoutingModule,
    CehuploadModule
  ],  
  exports: [
    NercComponent
  ]
})

export class NercModule { }
