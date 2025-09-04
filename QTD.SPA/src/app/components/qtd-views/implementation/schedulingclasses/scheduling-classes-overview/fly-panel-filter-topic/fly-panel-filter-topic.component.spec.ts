import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelFilterTopicComponent } from './fly-panel-filter-topic.component';

describe('FlyPanelFilterTopicComponent', () => {
  let component: FlyPanelFilterTopicComponent;
  let fixture: ComponentFixture<FlyPanelFilterTopicComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelFilterTopicComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelFilterTopicComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
