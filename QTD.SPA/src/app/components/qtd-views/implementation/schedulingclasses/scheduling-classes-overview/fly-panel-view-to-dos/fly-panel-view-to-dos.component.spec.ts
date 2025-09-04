import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelViewToDosComponent } from './fly-panel-view-to-dos.component';

describe('FlyPanelViewToDosComponent', () => {
  let component: FlyPanelViewToDosComponent;
  let fixture: ComponentFixture<FlyPanelViewToDosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelViewToDosComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelViewToDosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
