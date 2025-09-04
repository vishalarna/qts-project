import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {HttpClientModule} from '@angular/common/http';
import {BrowserModule} from '@angular/platform-browser';
import {LocalizeModule} from 'src/app/_Shared/modules/localize.module';
import {TopNavBarComponent} from "./top-nav-bar.component";
import {EmpDefaultComponent} from "../templates/emp-default/emp-default.component";
import {MatIconModule} from "@angular/material/icon";

@NgModule({
  declarations: [
    TopNavBarComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    LocalizeModule,
    MatIconModule
  ],
  exports: [
    TopNavBarComponent
  ]
})
export class TopNavBarModule {
}
