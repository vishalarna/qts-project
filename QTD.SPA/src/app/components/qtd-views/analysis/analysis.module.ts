import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AnalysisComponent } from './analysis.component';
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { AnalysisRoutingModule } from './analysis-routing.module';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';

@NgModule({
  declarations: [AnalysisComponent],
  imports: [
    CommonModule,
    HttpClientModule,
    AnalysisRoutingModule,
    LocalizeModule,
  ],
})
export class AnalysisModule {}
