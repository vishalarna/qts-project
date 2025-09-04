import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelTopicComponent } from './fly-panel-topic.component';

describe('FlyPanelTopicComponent', () => {
  let component: FlyPanelTopicComponent;
  let fixture: ComponentFixture<FlyPanelTopicComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelTopicComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelTopicComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
