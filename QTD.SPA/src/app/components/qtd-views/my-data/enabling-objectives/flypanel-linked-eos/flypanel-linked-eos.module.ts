import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlypanelLinkedEosComponent } from './flypanel-linked-eos.component';
import { BaseModule } from 'src/app/components/base/base.module';



@NgModule({
  declarations: [
    FlypanelLinkedEosComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
  ],
  exports : [
    FlypanelLinkedEosComponent,
  ]
})
export class FlypanelLinkedEosModule { }
