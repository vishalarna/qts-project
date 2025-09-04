import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EnablingObjectiveComponent } from './enabling-objective.component';
import { Route, RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    component: EnablingObjectiveComponent,
  },
];

@NgModule({
  declarations: [EnablingObjectiveComponent],
  imports: [CommonModule, RouterModule.forChild(routes)],
})
export class EnablingObjectiveModule {}
