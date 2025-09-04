import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelTrainingprogramLinkILaComponent } from '../fly-panel-trainingprogram-link-ila/fly-panel-trainingprogram-link-ila.component';
import { FormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatTreeModule } from '@angular/material/tree';
import { BaseModule } from 'src/app/components/base/base.module';
import { FlyPanelAddIlaModule } from '../../providers-and-ila/fly-panel-add-ila/fly-panel-add-ila.module';



@NgModule({
  declarations: [
    FlyPanelTrainingprogramLinkILaComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatTreeModule,
    MatCheckboxModule,
    FormsModule,
    FlyPanelAddIlaModule,
    MatMenuModule,
  ],
  exports : [
    FlyPanelTrainingprogramLinkILaComponent
  ]
})
export class FlyPanelTrainingprogramLinkILaModule { }
