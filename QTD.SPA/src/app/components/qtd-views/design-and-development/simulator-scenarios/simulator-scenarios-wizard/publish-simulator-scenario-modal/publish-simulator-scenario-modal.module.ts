import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PublishSimulatorScenarioModalComponent } from './publish-simulator-scenario-modal.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LayoutModule } from 'src/app/components/qtd-views/layout/layout.module';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyDialogModule as MatDialogModule } from '@angular/material/legacy-dialog';

@NgModule({
  declarations: [PublishSimulatorScenarioModalComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    LayoutModule,
    BaseModule,
    MatDialogModule
  ],
  exports:[PublishSimulatorScenarioModalComponent]
})
export class PublishSimulatorScenarioModalModule { }
