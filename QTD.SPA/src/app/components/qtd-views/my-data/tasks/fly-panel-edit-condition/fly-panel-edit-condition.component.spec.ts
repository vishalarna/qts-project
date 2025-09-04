import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelEditConditionComponent } from './fly-panel-edit-condition.component';

describe('FlyPanelEditConditionComponent', () => {
  let component: FlyPanelEditConditionComponent;
  let fixture: ComponentFixture<FlyPanelEditConditionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelEditConditionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelEditConditionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
