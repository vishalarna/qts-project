import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StartTestDialogComponent } from './start-test-dialog.component';

describe('StartTestDialogComponent', () => {
  let component: StartTestDialogComponent;
  let fixture: ComponentFixture<StartTestDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StartTestDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StartTestDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
