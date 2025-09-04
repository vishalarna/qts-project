import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { NgModule, ModuleWithProviders } from '@angular/core';
import {
  TranslateModule,
  TranslateLoader,
  TranslateService,
  TranslateStore,
} from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';

function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http, './assets/i18n/');
}

@NgModule({
  declarations: [],
  imports: [
    TranslateModule.forChild({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient],
      },
      defaultLanguage: 'en',
      isolate: true,
      extend: true,
    }),
  ],
  providers: [TranslateStore],
  exports: [TranslateModule],
})
export class LocalizeModule {}
