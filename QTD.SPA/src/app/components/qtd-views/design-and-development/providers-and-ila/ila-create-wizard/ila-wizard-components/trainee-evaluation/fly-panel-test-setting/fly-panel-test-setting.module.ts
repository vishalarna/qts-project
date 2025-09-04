import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelTestSettingComponent } from './fly-panel-test-setting.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import {MatButtonToggleModule} from '@angular/material/button-toggle';



@NgModule({
  declarations: [
    FlyPanelTestSettingComponent
  ],
  imports: [
    CommonModule,
    LocalizeModule,
    BaseModule,
    FormsModule,
    ReactiveFormsModule,
    MatButtonToggleModule
    
  ],
  exports:[FlyPanelTestSettingComponent]
})
export class FlyPanelTestSettingModule { }
