import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {LoginSsoComponent} from "./login-sso.component";
import {LoginSsoRoutingModule} from "./login-sso-routing.module";
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BaseModule } from 'src/app/components/base/base.module';

@NgModule({
    declarations: [LoginSsoComponent],
    imports: [
        CommonModule,
        ReactiveFormsModule,
        LoginSsoRoutingModule,
        LocalizeModule,
        BaseModule,
    ],
})
export class LoginSsoModule {}
