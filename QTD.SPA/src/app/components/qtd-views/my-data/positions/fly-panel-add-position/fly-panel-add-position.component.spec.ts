import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelAddPositionComponent } from './fly-panel-add-position.component';

describe('FlyPanelAddPositionComponent', () => {
  let component: FlyPanelAddPositionComponent;
  let fixture: ComponentFixture<FlyPanelAddPositionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelAddPositionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelAddPositionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
