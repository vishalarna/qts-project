import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { DifSurveysComponent } from './dif-surveys.component';
import { DifSurveysRoutingModule } from './dif-surveys-routing.module';

@NgModule({
  declarations: [DifSurveysComponent],
  imports: [
    CommonModule,
    HttpClientModule,
    DifSurveysRoutingModule,
    LocalizeModule,
  ],
})
export class DifSurveysModule {}
