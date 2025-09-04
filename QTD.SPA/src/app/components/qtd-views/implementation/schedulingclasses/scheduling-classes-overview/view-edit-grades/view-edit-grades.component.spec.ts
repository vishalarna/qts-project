import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewEditGradesComponent } from './view-edit-grades.component';

describe('ViewEditGradesComponent', () => {
  let component: ViewEditGradesComponent;
  let fixture: ComponentFixture<ViewEditGradesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ViewEditGradesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewEditGradesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
