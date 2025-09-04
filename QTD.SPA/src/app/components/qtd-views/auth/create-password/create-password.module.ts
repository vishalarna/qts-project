import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { CreatePasswordComponent } from './create-password.component';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { ReactiveFormsModule } from '@angular/forms';
import { ButtonComponent } from 'src/app/components/base/button/button.component';
import { InputErrorComponent } from 'src/app/components/base/input-error/input-error.component';
import { LabelComponent } from 'src/app/components/base/label/label.component';
import { LinkComponent } from 'src/app/components/base/link/link.component';
import { PasswordComponent } from 'src/app/components/base/password/password.component';
import { TextboxComponent } from 'src/app/components/base/textbox/textbox.component';
import { BaseModule } from 'src/app/components/base/base.module';

const routes: Routes = [
  {
    path: '',
    component: CreatePasswordComponent,
  },
];

@NgModule({
  declarations: [CreatePasswordComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    LocalizeModule,
    RouterModule.forChild(routes),
    BaseModule,
  ],
})
export class CreatePasswordModule {}
