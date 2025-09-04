import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RemainderOfTheWeekComponent } from './remainder-of-the-week.component';

describe('RemainderOfTheWeekComponent', () => {
  let component: RemainderOfTheWeekComponent;
  let fixture: ComponentFixture<RemainderOfTheWeekComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RemainderOfTheWeekComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RemainderOfTheWeekComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
