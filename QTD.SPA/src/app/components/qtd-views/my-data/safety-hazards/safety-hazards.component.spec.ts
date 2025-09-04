import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SafetyHazardsComponent } from './safety-hazards.component';

describe('SafetyHazardsComponent', () => {
  let component: SafetyHazardsComponent;
  let fixture: ComponentFixture<SafetyHazardsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SafetyHazardsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SafetyHazardsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
