import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
const routes: Routes = [
    // {
    //     path: 'cbt',
    //     component: CbtDemoPageComponent,
    //   },
];

@NgModule({ imports: [RouterModule.forChild(routes)], exports: [RouterModule] })
export class CbtDemoRoutingModule { }