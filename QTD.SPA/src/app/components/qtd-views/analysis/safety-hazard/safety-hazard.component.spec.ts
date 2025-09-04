import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SafetyHazardComponent } from './safety-hazard.component';

describe('SafetyHazardComponent', () => {
  let component: SafetyHazardComponent;
  let fixture: ComponentFixture<SafetyHazardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SafetyHazardComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SafetyHazardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
