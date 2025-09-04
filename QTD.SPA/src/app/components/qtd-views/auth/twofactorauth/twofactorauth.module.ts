import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TwoFactorAuthComponent } from './twofactorauth.component';
import { TwoFactorAuthRoutingModule } from './twofactorauth-routing.module';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { ReactiveFormsModule } from '@angular/forms';
import { ButtonComponent } from 'src/app/components/base/button/button.component';
import { BaseModule } from 'src/app/components/base/base.module';

@NgModule({
  declarations: [TwoFactorAuthComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    TwoFactorAuthRoutingModule,
    LocalizeModule,
    BaseModule,
  ],
})
export class TwofactorauthModule {}
