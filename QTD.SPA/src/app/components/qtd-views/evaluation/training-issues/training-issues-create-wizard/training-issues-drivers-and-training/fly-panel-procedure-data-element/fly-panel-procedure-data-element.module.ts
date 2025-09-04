import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatTreeModule } from '@angular/material/tree';
import { FlyPanelProcedureDataElementComponent } from './fly-panel-procedure-data-element.component';
import { MatLegacyRadioModule as MatRadioModule } from '@angular/material/legacy-radio';


@NgModule({
  declarations: [
    FlyPanelProcedureDataElementComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    ReactiveFormsModule,
    MatExpansionModule,
    MatRadioModule,
    MatTreeModule,
    MatMenuModule,
    ],
  exports:[FlyPanelProcedureDataElementComponent]
})
export class FlyPanelProcedureDataElementModule { }
