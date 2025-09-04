import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelTrainingprogramPositionLinkComponent } from './fly-panel-trainingprogram-position-link.component';
import { FormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyChipsModule as MatChipsModule } from '@angular/material/legacy-chips';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { BaseModule } from 'src/app/components/base/base.module';
import { DeleteEmpDialogueModule } from '../../../implementation/emp/delete-emp-dialogue/delete-emp-dialogue.module';
import { FlyPanelAddOrganizationModule } from '../../../implementation/emp/fly-panel-add-organization/fly-panel-add-organization.module';
import { MatLegacyRadioModule as MatRadioModule } from '@angular/material/legacy-radio';
import { FlyPanelAddPositionModule } from '../../../my-data/positions/fly-panel-add-position/fly-panel-add-position.module';



@NgModule({
  declarations: [
    FlyPanelTrainingprogramPositionLinkComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatCheckboxModule,
    MatMenuModule,
    FormsModule,
    MatExpansionModule,
    DeleteEmpDialogueModule,
    MatChipsModule,
    FlyPanelAddOrganizationModule,
    MatRadioModule,
    FlyPanelAddPositionModule
  ],
  exports:[FlyPanelTrainingprogramPositionLinkComponent]
})
export class FlyPanelTrainingprogramPositionLinkModule { }
