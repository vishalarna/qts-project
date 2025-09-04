import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SeqAndScheduleComponent } from './seq-and-schedule.component';

describe('SeqAndScheduleComponent', () => {
  let component: SeqAndScheduleComponent;
  let fixture: ComponentFixture<SeqAndScheduleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SeqAndScheduleComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SeqAndScheduleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
