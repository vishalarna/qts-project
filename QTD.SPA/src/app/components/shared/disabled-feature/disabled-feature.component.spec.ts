import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DisabledFeatureComponent } from './disabled-feature.component';

describe('DisabledFeatureComponent', () => {
  let component: DisabledFeatureComponent;
  let fixture: ComponentFixture<DisabledFeatureComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DisabledFeatureComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DisabledFeatureComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
