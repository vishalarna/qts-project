import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelTrainingprogramPositionFilterComponent } from './fly-panel-trainingprogram-position-filter.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { FormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyChipsModule as MatChipsModule } from '@angular/material/legacy-chips';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { DeleteEmpDialogueModule } from '../../../implementation/emp/delete-emp-dialogue/delete-emp-dialogue.module';
import { FlyPanelAddOrganizationModule } from '../../../implementation/emp/fly-panel-add-organization/fly-panel-add-organization.module';



@NgModule({
  declarations: [
    FlyPanelTrainingprogramPositionFilterComponent
  ],
  imports: [
    CommonModule,
    CommonModule,
    BaseModule,
    MatCheckboxModule,
    MatMenuModule,
    FormsModule,
    MatExpansionModule,
    DeleteEmpDialogueModule,
    MatChipsModule,
    FlyPanelAddOrganizationModule
  ],
  exports:[FlyPanelTrainingprogramPositionFilterComponent]
})
export class FlyPanelTrainingprogramPositionFilterModule { }
