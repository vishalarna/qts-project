import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginSsoComponent} from './login-sso.component';

const routes: Routes = [
    {
        path: '',
        component: LoginSsoComponent,
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class LoginSsoRoutingModule {}
