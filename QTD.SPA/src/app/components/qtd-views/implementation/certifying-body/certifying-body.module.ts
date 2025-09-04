import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CertifyingBodyComponent } from './certifying-body.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { Routes, RouterModule } from '@angular/router';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { FlyPanelCertifyingBodyComponent } from './fly-panel-certifying-body/fly-panel-certifying-body.component';

const routes: Routes = [
  {
    path: '',
    component: CertifyingBodyComponent,
  },
];

@NgModule({
  declarations: [CertifyingBodyComponent],
  imports: [
    CommonModule,
    HttpClientModule,
    RouterModule.forChild(routes),
    LocalizeModule,
    FormsModule,
  ],
})
export class CertifyingBodyModule {}
