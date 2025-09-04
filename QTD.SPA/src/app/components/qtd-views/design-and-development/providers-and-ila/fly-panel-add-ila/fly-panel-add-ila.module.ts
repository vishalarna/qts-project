import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelAddIlaComponent } from './fly-panel-add-ila.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyChipsModule as MatChipsModule } from '@angular/material/legacy-chips';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatStepperModule } from '@angular/material/stepper';
import { FlyPanelProviderModule } from '../fly-panel-provider/fly-panel-provider.module';
import { FlyPanelTopicModule } from '../fly-panel-topic/fly-panel-topic.module';

@NgModule({
  declarations: [FlyPanelAddIlaComponent],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    ReactiveFormsModule,
    MatStepperModule,
    MatSelectModule,
    MatChipsModule,
    MatCheckboxModule,
    FlyPanelProviderModule,
    FlyPanelTopicModule,
  ],
  exports: [FlyPanelAddIlaComponent],
})
export class FlyPanelAddIlaModule {}
