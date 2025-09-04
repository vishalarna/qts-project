import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StartEvaluationComponent } from './start-evaluation.component';

describe('StartEvaluationComponent', () => {
  let component: StartEvaluationComponent;
  let fixture: ComponentFixture<StartEvaluationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StartEvaluationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StartEvaluationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
