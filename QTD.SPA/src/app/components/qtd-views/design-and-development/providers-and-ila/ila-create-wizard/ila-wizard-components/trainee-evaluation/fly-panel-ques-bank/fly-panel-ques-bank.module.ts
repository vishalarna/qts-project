import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelQuesBankComponent } from './fly-panel-ques-bank.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { BaseModule } from 'src/app/components/base/base.module';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import {MatLegacyTooltipModule as MatTooltipModule} from '@angular/material/legacy-tooltip';



@NgModule({
  declarations: [
    FlyPanelQuesBankComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    BaseModule,
    LocalizeModule,
    MatCheckboxModule,
    MatTooltipModule,
    ReactiveFormsModule,
    MatCheckboxModule,
  ],
  exports : [FlyPanelQuesBankComponent]
})
export class FlyPanelQuesBankModule { }
