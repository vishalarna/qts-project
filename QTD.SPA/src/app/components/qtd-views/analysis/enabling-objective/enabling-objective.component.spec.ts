import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EnablingObjectiveComponent } from './enabling-objective.component';

describe('EnablingObjectiveComponent', () => {
  let component: EnablingObjectiveComponent;
  let fixture: ComponentFixture<EnablingObjectiveComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EnablingObjectiveComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EnablingObjectiveComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
