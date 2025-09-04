import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelAddRecurrenceEventComponent } from './fly-panel-add-recurrence-event.component';

describe('FlyPanelAddRecurrenceEventComponent', () => {
  let component: FlyPanelAddRecurrenceEventComponent;
  let fixture: ComponentFixture<FlyPanelAddRecurrenceEventComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelAddRecurrenceEventComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelAddRecurrenceEventComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
