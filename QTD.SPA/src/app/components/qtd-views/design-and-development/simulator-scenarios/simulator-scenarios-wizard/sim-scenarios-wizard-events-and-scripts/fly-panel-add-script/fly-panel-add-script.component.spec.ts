import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelAddScriptComponent } from './fly-panel-add-script.component';

describe('FlyPanelAddScriptComponent', () => {
  let component: FlyPanelAddScriptComponent;
  let fixture: ComponentFixture<FlyPanelAddScriptComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelAddScriptComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FlyPanelAddScriptComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
