import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthHelpComponent } from './auth-help.component';
import { AuthHelpRoutingModule } from './auth-help-routing.module';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { BaseModule } from 'src/app/components/base/base.module';

@NgModule({
  declarations: [AuthHelpComponent],
  imports: [CommonModule, AuthHelpRoutingModule, LocalizeModule, BaseModule],
})
export class AuthHelpModule {}
