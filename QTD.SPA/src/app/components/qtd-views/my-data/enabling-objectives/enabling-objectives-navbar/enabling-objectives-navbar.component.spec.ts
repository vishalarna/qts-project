import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EnablingObjectivesNavbarComponent } from './enabling-objectives-navbar.component';

describe('EnablingObjectivesNavbarComponent', () => {
  let component: EnablingObjectivesNavbarComponent;
  let fixture: ComponentFixture<EnablingObjectivesNavbarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EnablingObjectivesNavbarComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EnablingObjectivesNavbarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
