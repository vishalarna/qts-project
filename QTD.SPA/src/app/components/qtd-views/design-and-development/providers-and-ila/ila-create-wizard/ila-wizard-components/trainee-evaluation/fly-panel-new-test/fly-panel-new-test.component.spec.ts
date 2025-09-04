import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelIlaPrerequisitesComponent } from './fly-panel-new-test.component';

describe('FlyPanelIlaPrerequisitesComponent', () => {
  let component: FlyPanelIlaPrerequisitesComponent;
  let fixture: ComponentFixture<FlyPanelIlaPrerequisitesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelIlaPrerequisitesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelIlaPrerequisitesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
