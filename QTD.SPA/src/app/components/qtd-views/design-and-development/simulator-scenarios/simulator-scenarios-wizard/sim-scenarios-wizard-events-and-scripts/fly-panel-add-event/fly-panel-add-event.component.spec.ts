import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelAddEventComponent } from './fly-panel-add-event.component';

describe('FlyPanelAddEventComponent', () => {
  let component: FlyPanelAddEventComponent;
  let fixture: ComponentFixture<FlyPanelAddEventComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelAddEventComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelAddEventComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
