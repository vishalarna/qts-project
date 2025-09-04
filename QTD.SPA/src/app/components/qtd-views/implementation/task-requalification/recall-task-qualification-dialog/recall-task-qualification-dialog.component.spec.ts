import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RecallTaskQualificationDialogComponent } from './recall-task-qualification-dialog.component';

describe('RecallTaskQualificationDialogComponent', () => {
  let component: RecallTaskQualificationDialogComponent;
  let fixture: ComponentFixture<RecallTaskQualificationDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RecallTaskQualificationDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RecallTaskQualificationDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
