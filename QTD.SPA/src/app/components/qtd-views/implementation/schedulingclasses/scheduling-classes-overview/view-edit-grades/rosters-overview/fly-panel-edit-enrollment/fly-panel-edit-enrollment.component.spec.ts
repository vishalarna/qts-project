import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelEditEnrollmentComponent } from './fly-panel-edit-enrollment.component';

describe('FlyPanelEditEnrollmentComponent', () => {
  let component: FlyPanelEditEnrollmentComponent;
  let fixture: ComponentFixture<FlyPanelEditEnrollmentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelEditEnrollmentComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelEditEnrollmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
