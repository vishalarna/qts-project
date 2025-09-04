import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelChangeAddEmployeesToTrainingComponent } from './fly-panel-change-add-employees-to-training.component';

describe('FlyPanelChangeAddEmployeesToTrainingComponent', () => {
  let component: FlyPanelChangeAddEmployeesToTrainingComponent;
  let fixture: ComponentFixture<FlyPanelChangeAddEmployeesToTrainingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelChangeAddEmployeesToTrainingComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelChangeAddEmployeesToTrainingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
