import { NgModule } from "@angular/core";
import { FlyPanelAddBasicIlaComponent } from "./fly-panel-add-basic-ila.component";
import { FlyPanelProviderModule } from "../fly-panel-provider/fly-panel-provider.module";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { MatStepperModule } from "@angular/material/stepper";
import { MatLegacyCheckboxModule as MatCheckboxModule } from "@angular/material/legacy-checkbox";
import { CommonModule } from "@angular/common";
import { BaseModule } from "src/app/components/base/base.module";
import { MatChipsModule } from "@angular/material/chips";
import { MatSelectModule } from "@angular/material/select";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatButtonToggleModule } from "@angular/material/button-toggle";
import { MatLegacyRadioModule as MatRadioModule } from '@angular/material/legacy-radio';
import { MatLegacyTabsModule as MatTabsModule } from '@angular/material/legacy-tabs';

@NgModule({
  declarations: [FlyPanelAddBasicIlaComponent],
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
    MatFormFieldModule,
    MatButtonToggleModule,
    MatRadioModule,
    MatTabsModule
  ],
  exports: [FlyPanelAddBasicIlaComponent],
})
export class FlyPanelAddBasicIlaModule  {}
