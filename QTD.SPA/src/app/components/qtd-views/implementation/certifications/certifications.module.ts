import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { CertificationsComponent } from './certifications.component';
import { HttpClientModule } from '@angular/common/http';
import { Routes, RouterModule } from '@angular/router';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { FormsModule } from '@angular/forms';

const routes: Routes = [
  {
    path: '',
    component: CertificationsComponent,
  },
];

@NgModule({
  declarations: [CertificationsComponent],
  imports: [
    CommonModule,
    HttpClientModule,
    RouterModule.forChild(routes),
    LocalizeModule,
    FormsModule,
  ],
})
export class CertificationsModule {}
