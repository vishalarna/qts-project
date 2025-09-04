import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatIconModule } from '@angular/material/icon';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { LayoutModule } from '../../../../../../layout/layout.module';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { FlyPanelTrainingMaterialComponent } from './fly-panel-training-material.component';



@NgModule({
  declarations: [FlyPanelTrainingMaterialComponent],
  imports: [
    CommonModule,
    MatTableModule,
    MatExpansionModule,
    MatIconModule,
    LocalizeModule,
    LayoutModule,
    BaseModule,
    CommonModule,
    ReactiveFormsModule,
    MatSelectModule,
  ],
  exports:[
    FlyPanelTrainingMaterialComponent
  ]
})
export class FlyPanelTrainingMaterialModule { }
