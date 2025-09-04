import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlypanelChangeEoComponent } from './flypanel-change-eo.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { FlypanelAddEoModule } from '../../flypanel-add-eo/flypanel-add-eo.module';
import { FlypanelSelectEoModule } from '../flypanel-select-eo/flypanel-select-eo.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';



@NgModule({
  declarations: [
    FlypanelChangeEoComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    FlypanelAddEoModule,
    FlypanelSelectEoModule,
    FormsModule,
    ReactiveFormsModule,
    MatIconModule,
  ],
  exports : [
    FlypanelChangeEoComponent,
  ]
})
export class FlypanelChangeEoModule { }
