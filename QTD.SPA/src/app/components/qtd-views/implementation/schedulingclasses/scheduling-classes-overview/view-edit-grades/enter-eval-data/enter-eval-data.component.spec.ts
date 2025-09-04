import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EnterEvalDataComponent } from './enter-eval-data.component';

describe('EnterEvalDataComponent', () => {
  let component: EnterEvalDataComponent;
  let fixture: ComponentFixture<EnterEvalDataComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EnterEvalDataComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EnterEvalDataComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
