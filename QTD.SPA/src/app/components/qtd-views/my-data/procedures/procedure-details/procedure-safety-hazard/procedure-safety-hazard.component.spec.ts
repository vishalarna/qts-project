import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProcedureSafetyHazardComponent } from './procedure-safety-hazard.component';

describe('ProcedureSafetyHazardComponent', () => {
  let component: ProcedureSafetyHazardComponent;
  let fixture: ComponentFixture<ProcedureSafetyHazardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProcedureSafetyHazardComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProcedureSafetyHazardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
