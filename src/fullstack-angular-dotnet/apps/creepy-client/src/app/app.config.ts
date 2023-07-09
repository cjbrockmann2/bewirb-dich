import { ApplicationConfig, DEFAULT_CURRENCY_CODE, LOCALE_ID } from '@angular/core';
import {
  provideRouter,
  withEnabledBlockingInitialNavigation,
} from '@angular/router';
import { appRoutes } from './app.routes';
import {HttpClientModule, provideHttpClient} from "@angular/common/http";
import localeDe from '@angular/common/locales/de';
import localeDeExtra from '@angular/common/locales/extra/de';
import { registerLocaleData } from '@angular/common';
registerLocaleData(localeDe, localeDeExtra);

export const appConfig: ApplicationConfig = {
  providers: [provideRouter(appRoutes
    , withEnabledBlockingInitialNavigation())
    , provideHttpClient()
    , {provide: LOCALE_ID, useValue: 'de-DE' }  
    , {provide: DEFAULT_CURRENCY_CODE, useValue: 'EUR'}
  ],
};
