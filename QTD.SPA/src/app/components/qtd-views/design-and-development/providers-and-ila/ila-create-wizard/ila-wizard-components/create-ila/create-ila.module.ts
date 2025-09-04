import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreateIlaComponent } from './create-ila.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyFormFieldModule as MatFormFieldModule } from '@angular/material/legacy-form-field';
import { LayoutModule } from '@angular/cdk/layout';
import { FlyPanelAddPositionModule } from 'src/app/components/qtd-views/my-data/positions/fly-panel-add-position/fly-panel-add-position.module';
import { FlyPanelProviderModule } from '../../../fly-panel-provider/fly-panel-provider.module';
import { FlyPanelTopicModule } from '../../../fly-panel-topic/fly-panel-topic.module';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyChipsModule as MatChipsModule } from '@angular/material/legacy-chips';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';
import { MatLegacyTabsModule as MatTabsModule } from '@angular/material/legacy-tabs';
import {MatLegacyRadioModule as MatRadioModule} from '@angular/material/legacy-radio';
import { StoreModule } from '@ngrx/store';


@NgModule({
  declarations: [
    CreateIlaComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    BaseModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    LayoutModule,
    FlyPanelAddPositionModule,
    FlyPanelProviderModule,
    FlyPanelTopicModule,
    MatSelectModule,
    MatCheckboxModule,
    MatChipsModule,
    MatTooltipModule,
    MatTabsModule,
    MatRadioModule,
    StoreModule,
  ],
  exports:[CreateIlaComponent]
})
export class CreateIlaModule { }
