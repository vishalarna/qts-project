import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {HttpClientModule} from '@angular/common/http';
import {LocalizeModule} from 'src/app/_Shared/modules/localize.module';
import {ModelBindingsComponent} from "./model-bindings.component";

@NgModule({
  declarations: [
    ModelBindingsComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    LocalizeModule
  ],
  exports: [
    ModelBindingsComponent
  ]
})
export class ModelBindingsModule {
}
