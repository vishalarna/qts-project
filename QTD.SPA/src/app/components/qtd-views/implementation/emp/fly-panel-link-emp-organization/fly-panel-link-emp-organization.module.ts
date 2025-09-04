import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelLinkEmpOrganizationComponent } from './fly-panel-link-emp-organization.component';
import { FormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatAccordion, MatExpansionModule } from '@angular/material/expansion';
import { DeleteEmpDialogueModule } from '../delete-emp-dialogue/delete-emp-dialogue.module';
import { MatLegacyChipsModule as MatChipsModule } from '@angular/material/legacy-chips';
import { FlyPanelAddOrganizationModule } from '../fly-panel-add-organization/fly-panel-add-organization.module';



@NgModule({
  declarations: [
    FlyPanelLinkEmpOrganizationComponent
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
    FlyPanelAddOrganizationModule
  ],
  exports:[FlyPanelLinkEmpOrganizationComponent]
})
export class FlyPanelLinkEmpOrganizationModule { }
