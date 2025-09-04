import { NgModule } from '@angular/core';
import { FlyPanelCreateNewClientComponent } from './fly-panel-create-new-client.component';
import { CommonModule } from '@angular/common';
import { BaseModule } from 'src/app/components/base/base.module';
import { ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [FlyPanelCreateNewClientComponent],
  imports: [
    CommonModule,
    BaseModule,
    ReactiveFormsModule,
  ],
  exports:[FlyPanelCreateNewClientComponent]
})
export class FlyPanelCreateNewClientModule { }
