import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ClientsComponent } from './clients.component';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';
import { SelectDropDownModule } from 'ngx-select-dropdown';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';

const routes: Routes = [
];

@NgModule({
  declarations: [ClientsComponent],
  imports: [
    CommonModule,
    HttpClientModule,
    RouterModule.forChild(routes),
    LocalizeModule,
    SelectDropDownModule,
  ],
})
export class ClientsModule {}
