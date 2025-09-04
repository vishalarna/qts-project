import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EoSafetyHazardsComponent } from './eo-safety-hazards.component';

describe('EoSafetyHazardsComponent', () => {
  let component: EoSafetyHazardsComponent;
  let fixture: ComponentFixture<EoSafetyHazardsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EoSafetyHazardsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EoSafetyHazardsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
