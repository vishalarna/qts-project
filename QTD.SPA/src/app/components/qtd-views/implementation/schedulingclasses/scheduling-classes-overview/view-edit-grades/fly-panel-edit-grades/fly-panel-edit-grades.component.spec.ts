import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelEditGradesComponent } from './fly-panel-edit-grades.component';

describe('FlyPanelEditGradesComponent', () => {
  let component: FlyPanelEditGradesComponent;
  let fixture: ComponentFixture<FlyPanelEditGradesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelEditGradesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelEditGradesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
