import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DutyareaDetailComponent } from './dutyarea-detail.component';

describe('DutyareaDetailComponent', () => {
  let component: DutyareaDetailComponent;
  let fixture: ComponentFixture<DutyareaDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DutyareaDetailComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DutyareaDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
