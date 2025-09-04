import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PublicClassScheduleRequestComponent } from './public-class-schedule-request.component';

describe('PuclicClassScheduleRequestComponent', () => {
  let component: PublicClassScheduleRequestComponent;
  let fixture: ComponentFixture<PublicClassScheduleRequestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PublicClassScheduleRequestComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PublicClassScheduleRequestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
