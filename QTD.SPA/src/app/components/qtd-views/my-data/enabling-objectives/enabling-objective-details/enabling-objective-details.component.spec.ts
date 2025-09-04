import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EnablingObjectiveDetailsComponent } from './enabling-objective-details.component';

describe('EnablingObjectiveDetailsComponent', () => {
  let component: EnablingObjectiveDetailsComponent;
  let fixture: ComponentFixture<EnablingObjectiveDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EnablingObjectiveDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EnablingObjectiveDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
