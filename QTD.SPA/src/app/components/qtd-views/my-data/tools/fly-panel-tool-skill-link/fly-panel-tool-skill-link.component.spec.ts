import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelToolSkillLinkComponent } from './fly-panel-tool-skill-link.component';

describe('FlyPanelToolSkillLinkComponent', () => {
  let component: FlyPanelToolSkillLinkComponent;
  let fixture: ComponentFixture<FlyPanelToolSkillLinkComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelToolSkillLinkComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelToolSkillLinkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
