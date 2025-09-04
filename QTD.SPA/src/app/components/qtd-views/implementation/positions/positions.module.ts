import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PositionsComponent } from './positions.component';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { FormsModule } from '@angular/forms';
import { BaseModule } from 'src/app/components/base/base.module';

const routes: Routes = [
  {
    path: '',
    component: PositionsComponent,
  },
];

@NgModule({
  declarations: [PositionsComponent,],
  imports: [
    CommonModule,
    HttpClientModule,
    RouterModule.forChild(routes),
    LocalizeModule,
    FormsModule,
    BaseModule
  ],
})
export class PositionsModule {}
