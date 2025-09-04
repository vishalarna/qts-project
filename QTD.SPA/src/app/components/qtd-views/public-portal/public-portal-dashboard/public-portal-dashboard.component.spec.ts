import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PublicPortalDashboardComponent } from './public-portal-dashboard.component';

describe('PublicPortalDashboardComponent', () => {
  let component: PublicPortalDashboardComponent;
  let fixture: ComponentFixture<PublicPortalDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PublicPortalDashboardComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PublicPortalDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
