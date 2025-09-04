import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelAddDefinitionComponent } from './fly-panel-add-definition.component';

describe('FlyPanelAddDefinitionComponent', () => {
  let component: FlyPanelAddDefinitionComponent;
  let fixture: ComponentFixture<FlyPanelAddDefinitionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelAddDefinitionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelAddDefinitionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
