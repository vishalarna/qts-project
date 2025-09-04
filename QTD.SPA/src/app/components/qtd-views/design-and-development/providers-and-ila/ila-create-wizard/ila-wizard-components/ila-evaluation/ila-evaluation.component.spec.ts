import { ComponentFixture, TestBed } from '@angular/core/testing';
import { IlaEvaluationComponent } from './ila-evaluation.component';


describe('TrainingPlanComponent', () => {
  let component:  IlaEvaluationComponent;
  let fixture: ComponentFixture< IlaEvaluationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [  IlaEvaluationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent( IlaEvaluationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
