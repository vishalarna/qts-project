import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LocationsNavbarComponent } from './locations-navbar.component';

describe('LocationsNavbarComponent', () => {
  let component: LocationsNavbarComponent;
  let fixture: ComponentFixture<LocationsNavbarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LocationsNavbarComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LocationsNavbarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
