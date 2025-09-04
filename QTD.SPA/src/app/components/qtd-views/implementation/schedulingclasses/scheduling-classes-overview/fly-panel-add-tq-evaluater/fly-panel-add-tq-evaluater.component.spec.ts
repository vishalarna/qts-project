import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelAddTqEvaluaterComponent } from './fly-panel-add-tq-evaluater.component';

describe('FlyPanelAddTqEvaluaterComponent', () => {
  let component: FlyPanelAddTqEvaluaterComponent;
  let fixture: ComponentFixture<FlyPanelAddTqEvaluaterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelAddTqEvaluaterComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelAddTqEvaluaterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
