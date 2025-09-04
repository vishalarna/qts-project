import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelToolNotLinkedComponent } from './fly-panel-tool-not-linked.component';
import { FormsModule } from '@angular/forms';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatTreeModule } from '@angular/material/tree';
import { BaseModule } from 'src/app/components/base/base.module';



@NgModule({
  declarations: [
    FlyPanelToolNotLinkedComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    MatTreeModule,
    MatMenuModule,
  ],exports:[FlyPanelToolNotLinkedComponent]
})
export class FlyPanelToolNotLinkedModule { }
