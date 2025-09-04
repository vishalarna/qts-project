import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BulkEditEvaluationComponent } from './bulk-edit-evaluation.component';

describe('BulkEditEvaluationComponent', () => {
  let component: BulkEditEvaluationComponent;
  let fixture: ComponentFixture<BulkEditEvaluationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BulkEditEvaluationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BulkEditEvaluationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
