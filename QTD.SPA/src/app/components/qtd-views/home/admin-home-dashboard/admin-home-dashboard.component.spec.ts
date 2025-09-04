import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminHomeDashboardComponent } from './admin-home-dashboard.component';

describe('AdminHomeDashboardComponent', () => {
  let component: AdminHomeDashboardComponent;
  let fixture: ComponentFixture<AdminHomeDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdminHomeDashboardComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminHomeDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
