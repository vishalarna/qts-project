import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelEditToolsComponent } from './fly-panel-edit-tools.component';

describe('FlyPanelEditToolsComponent', () => {
  let component: FlyPanelEditToolsComponent;
  let fixture: ComponentFixture<FlyPanelEditToolsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelEditToolsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelEditToolsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
