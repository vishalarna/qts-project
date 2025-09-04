import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlypanelLinkEnablingObjectiveComponent } from './flypanel-link-enabling-objective.component';
import { FormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatIconModule } from '@angular/material/icon';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatTreeModule } from '@angular/material/tree';
import { BaseModule } from 'src/app/components/base/base.module';



@NgModule({
  declarations: [FlypanelLinkEnablingObjectiveComponent],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    MatTreeModule,
    MatIconModule,
    MatMenuModule,
    MatCheckboxModule,
  ],
  exports: [FlypanelLinkEnablingObjectiveComponent]
})
export class FlypanelLinkEnablingObjectiveModule { }
