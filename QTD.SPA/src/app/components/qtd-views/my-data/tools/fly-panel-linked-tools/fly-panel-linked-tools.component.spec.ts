import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelLinkedToolsComponent } from './fly-panel-linked-tools.component';

describe('FlyPanelLinkedToolsComponent', () => {
  let component: FlyPanelLinkedToolsComponent;
  let fixture: ComponentFixture<FlyPanelLinkedToolsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelLinkedToolsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelLinkedToolsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
