import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FillEvalFormDialogComponent } from './fill-eval-form-dialog.component';

describe('FillEvalFormDialogComponent', () => {
  let component: FillEvalFormDialogComponent;
  let fixture: ComponentFixture<FillEvalFormDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FillEvalFormDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FillEvalFormDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
