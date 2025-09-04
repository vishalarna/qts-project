import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelReleaseIdpComponent } from './fly-panel-release-idp.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { BaseModule } from 'src/app/components/base/base.module';



@NgModule({
  declarations: [
    FlyPanelReleaseIdpComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    MatCheckboxModule,
    ReactiveFormsModule,
    FormsModule,
  ],

  exports:[FlyPanelReleaseIdpComponent]
})
export class FlyPanelReleaseIdpModule { }
