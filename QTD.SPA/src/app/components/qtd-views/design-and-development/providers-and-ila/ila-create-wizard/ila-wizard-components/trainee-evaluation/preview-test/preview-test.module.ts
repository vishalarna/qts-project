import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { BaseModule } from "src/app/components/base/base.module";
import { PreviewTestComponent } from "./preview-test.component";
import {MatLegacyRadioModule as MatRadioModule} from '@angular/material/legacy-radio'

@NgModule({
  declarations:[PreviewTestComponent],
  imports:[
    BaseModule,
    CommonModule,
    MatRadioModule,
  ],
  exports:[PreviewTestComponent],
})

export class PreviewTestModule{

}
