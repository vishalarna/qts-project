import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StartEvaluationComponent } from './start-evaluation.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { RouterModule, Routes } from '@angular/router';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatLegacyRadioModule as MatRadioModule } from '@angular/material/legacy-radio';
import { ReactiveFormsModule } from '@angular/forms';

const routes:Routes = [
  {
    path:'',
    component:StartEvaluationComponent,
  }
]

@NgModule({
  declarations: [
    StartEvaluationComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    RouterModule.forChild(routes),
    MatToolbarModule,
    MatIconModule,
    MatExpansionModule,
    MatRadioModule,
    ReactiveFormsModule,
  ],
  exports:[
    StartEvaluationComponent
  ]
})
export class StartEvaluationModule { }
