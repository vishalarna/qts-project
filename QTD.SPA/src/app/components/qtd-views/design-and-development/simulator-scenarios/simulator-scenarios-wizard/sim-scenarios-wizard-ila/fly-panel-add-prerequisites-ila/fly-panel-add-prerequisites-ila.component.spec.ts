import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelAddPrerequisitesIlaComponent } from './fly-panel-add-prerequisites-ila.component';

describe('FlyPanelAddPrerequisitesIlaComponent', () => {
  let component: FlyPanelAddPrerequisitesIlaComponent;
  let fixture: ComponentFixture<FlyPanelAddPrerequisitesIlaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelAddPrerequisitesIlaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelAddPrerequisitesIlaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
