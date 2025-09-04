import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home.component';
import { HomeRoutingModule } from './home-routing.module';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { IndexComponent } from './index/index.component';
import { MatSidenavModule } from '@angular/material/sidenav';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { LayoutModule } from '../layout/layout.module';
export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http);
}
@NgModule({
  declarations: [HomeComponent, IndexComponent],
  imports: [
    CommonModule,
    HomeRoutingModule,
    HttpClientModule,
    LocalizeModule,
    LayoutModule,
    MatSidenavModule
  ],
})
export class HomeModule {}
