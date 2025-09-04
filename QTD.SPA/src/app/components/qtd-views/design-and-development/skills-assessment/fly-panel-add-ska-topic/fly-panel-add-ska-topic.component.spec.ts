import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelAddSkaTopicComponent } from './fly-panel-add-ska-topic.component';

describe('FlyPanelAddSkaTopicComponent', () => {
  let component: FlyPanelAddSkaTopicComponent;
  let fixture: ComponentFixture<FlyPanelAddSkaTopicComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelAddSkaTopicComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelAddSkaTopicComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
