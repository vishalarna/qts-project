import { NgModule } from '@angular/core';
import { FlyPanelLinkILAsToMetaILAComponent } from './fly-panel-link-ilas-to-meta-ila.component';
import { CommonModule } from '@angular/common';
import { BaseModule } from 'src/app/components/base/base.module';
import { FormsModule } from '@angular/forms';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { BrowserModule } from '@angular/platform-browser';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';


@NgModule({
  declarations: [
    FlyPanelLinkILAsToMetaILAComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    MatTableModule,
    MatSortModule,
    MatCheckboxModule,
    MatToolbarModule,
    MatSelectModule
  ],
  exports:[FlyPanelLinkILAsToMetaILAComponent]
})
export class FlyPanelLinkILAsToMetaILAModule { }
