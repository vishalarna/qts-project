import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SubDutyareaDetailComponent } from './sub-dutyarea-detail.component';

describe('SubDutyareaDetailComponent', () => {
  let component: SubDutyareaDetailComponent;
  let fixture: ComponentFixture<SubDutyareaDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SubDutyareaDetailComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SubDutyareaDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
