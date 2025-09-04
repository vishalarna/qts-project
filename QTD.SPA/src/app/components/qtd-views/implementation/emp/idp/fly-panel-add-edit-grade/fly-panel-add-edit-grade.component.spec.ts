import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelAddEditGradeComponent } from './fly-panel-add-edit-grade.component';

describe('FlyPanelAddEditGradeComponent', () => {
  let component: FlyPanelAddEditGradeComponent;
  let fixture: ComponentFixture<FlyPanelAddEditGradeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelAddEditGradeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelAddEditGradeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
