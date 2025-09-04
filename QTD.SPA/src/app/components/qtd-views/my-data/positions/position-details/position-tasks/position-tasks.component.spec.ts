import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PositionTasksComponent } from './position-tasks.component';

describe('PositionTasksComponent', () => {
  let component: PositionTasksComponent;
  let fixture: ComponentFixture<PositionTasksComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PositionTasksComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PositionTasksComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
