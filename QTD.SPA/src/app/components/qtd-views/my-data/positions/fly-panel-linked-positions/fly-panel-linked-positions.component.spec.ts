import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelLinkedPositionsComponent } from './fly-panel-linked-positions.component';

describe('FlyPanelLinkedPositionsComponent', () => {
  let component: FlyPanelLinkedPositionsComponent;
  let fixture: ComponentFixture<FlyPanelLinkedPositionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelLinkedPositionsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelLinkedPositionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
