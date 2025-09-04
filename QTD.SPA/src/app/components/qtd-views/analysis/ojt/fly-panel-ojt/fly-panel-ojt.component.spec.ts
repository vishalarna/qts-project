import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelOJTComponent } from './fly-panel-ojt.component';

describe('FlyPanelOJTComponent', () => {
  let component: FlyPanelOJTComponent;
  let fixture: ComponentFixture<FlyPanelOJTComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelOJTComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelOJTComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
