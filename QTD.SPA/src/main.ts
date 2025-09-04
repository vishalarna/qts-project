import { enableProdMode, NgModule, NgModuleRef,ApplicationRef    } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';
import { environment } from './environments/environment';

import {ConfigService} from "./app/_Services/Local/ConfigService";
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';

async function fetchConfig() {
  const configService = new ConfigService();
  await configService.loadConfig();
  const config = configService.config;
  if (config) {
    // Set environment variables using spread syntax
    Object.assign(environment, { ...config });

    // Enable production mode if specified
    if (environment.production) {
      enableProdMode();
    }
  }
}

@NgModule({
  imports: [BrowserModule, HttpClientModule],
  providers: [ConfigService]
})
class ConfigModule {
  ngDoBootstrap(appRef: ApplicationRef): void {
    // This method needs to be defined for the module to be bootstrapped properly.
  }
}

fetchConfig().then(() => {
  platformBrowserDynamic().bootstrapModule(AppModule)
    .catch(err => console.error(err));
});
