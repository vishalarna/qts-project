import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelPositionNotLinkedComponent } from './fly-panel-position-not-linked.component';

describe('FlyPanelPositionNotLinkedComponent', () => {
  let component: FlyPanelPositionNotLinkedComponent;
  let fixture: ComponentFixture<FlyPanelPositionNotLinkedComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelPositionNotLinkedComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelPositionNotLinkedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
