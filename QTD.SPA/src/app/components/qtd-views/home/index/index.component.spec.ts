import { CUSTOM_ELEMENTS_SCHEMA, inject } from '@angular/core';
import {
  async,
  ComponentFixture,
  fakeAsync,
  TestBed,
  tick,
  waitForAsync,
} from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { IndexComponent } from './index.component';
import { TranslateService } from '@ngx-translate/core';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { By } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { Location } from '@angular/common';
import { ClientsComponent } from '../../clients/clients.component';

describe('IndexComponent', () => {
  let component: IndexComponent;
  let fixture: ComponentFixture<IndexComponent>;
  let router: Router;
  let location: Location;
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        RouterTestingModule.withRoutes([
          { path: 'clients', component: ClientsComponent },
        ]),
        LocalizeModule,
        HttpClientTestingModule,
     LocalizeModule,
      ],
      declarations: [IndexComponent],
      providers: [TranslateService],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(IndexComponent);
    component = fixture.componentInstance;
    router = TestBed.inject(Router);
    location = TestBed.inject(Location);
    router.initialNavigation();
    fixture.detectChanges();
  });

  it('should create component', () => {
    expect(component).toBeTruthy();
  });

  it('should go to clients url', fakeAsync(() => {
    fixture.debugElement.query(By.css('#gotoClients')).nativeElement.click();
    tick();
    fixture.detectChanges();
    expect(location.path()).toBe('/clients');
  }));
});
