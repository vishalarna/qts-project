import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SelfRegistrationOverviewComponent } from './self-registration-overview.component';

describe('SelfRegistrationOverviewComponent', () => {
  let component: SelfRegistrationOverviewComponent;
  let fixture: ComponentFixture<SelfRegistrationOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SelfRegistrationOverviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SelfRegistrationOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
