import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelLinkReviewersComponent } from './fly-panel-link-reviewers.component';
import { FormsModule } from '@angular/forms';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatSortModule } from '@angular/material/sort';


@NgModule({
  declarations: [
    FlyPanelLinkReviewersComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    MatMenuModule,
    MatTableModule,
    MatCheckboxModule,
    MatSortModule
  ],
  exports:[FlyPanelLinkReviewersComponent]
})
export class FlyPanelLinkReviewersModule { }
