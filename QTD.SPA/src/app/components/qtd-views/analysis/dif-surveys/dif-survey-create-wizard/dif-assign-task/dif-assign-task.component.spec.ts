import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DifAssignTaskComponent } from './dif-assign-task.component';

describe('DifAssignTaskComponent', () => {
  let component: DifAssignTaskComponent;
  let fixture: ComponentFixture<DifAssignTaskComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DifAssignTaskComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DifAssignTaskComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
