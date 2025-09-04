import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

class AppConfig {
  QTD: string;
  EMP: string;
  Admin: string;
  Auth: string;
  APIAuth: string;
  production: boolean;
  Storybook_UseStub: boolean;
}

@Injectable({
  providedIn: 'root'
})
export class ConfigService {
  private configSubject = new BehaviorSubject<AppConfig | null>(null);
  public config$ = this.configSubject.asObservable();

  constructor() {}

   async loadConfig() {
    try {
      const response = await fetch('assets/env.json');
      if (!response.ok) {
        this.configSubject.next(null);
        throw new Error('Failed to fetch config file');
      }
      const res = await response.json();
      const config = new AppConfig();
      Object.assign(config, res);
      this.configSubject.next(config);
    } catch (error) {
      console.error('Error loading config file:', error);
      // Handle error as needed, e.g., throw error or log
    }
  }

  get config() {
    return this.configSubject.getValue();
  }
}
