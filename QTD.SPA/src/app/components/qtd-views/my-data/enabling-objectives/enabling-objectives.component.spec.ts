import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EnablingObjectivesComponent } from './enabling-objectives.component';

describe('EnablingObjectivesComponent', () => {
  let component: EnablingObjectivesComponent;
  let fixture: ComponentFixture<EnablingObjectivesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EnablingObjectivesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EnablingObjectivesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
