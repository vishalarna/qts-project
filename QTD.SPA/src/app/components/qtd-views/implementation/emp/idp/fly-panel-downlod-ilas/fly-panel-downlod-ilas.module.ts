import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelDownlodIlasComponent } from './fly-panel-downlod-ilas.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatTreeModule } from '@angular/material/tree';
import { FlyPanelAddIlaModule } from 'src/app/components/qtd-views/design-and-development/providers-and-ila/fly-panel-add-ila/fly-panel-add-ila.module';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';



@NgModule({
  declarations: [
    FlyPanelDownlodIlasComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatTreeModule,
    MatCheckboxModule,
    FormsModule,
    FlyPanelAddIlaModule,
    MatMenuModule,
    MatSelectModule,
    ReactiveFormsModule  ,
    MatTableModule,  
  ],
  exports : [
    FlyPanelDownlodIlasComponent
  ]
})
export class FlyPanelDownlodIlasModule { }
