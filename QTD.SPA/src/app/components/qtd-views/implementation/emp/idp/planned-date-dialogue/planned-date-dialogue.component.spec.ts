import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PlannedDateDialogueComponent } from './planned-date-dialogue.component';

describe('PlannedDateDialogueComponent', () => {
  let component: PlannedDateDialogueComponent;
  let fixture: ComponentFixture<PlannedDateDialogueComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PlannedDateDialogueComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PlannedDateDialogueComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
