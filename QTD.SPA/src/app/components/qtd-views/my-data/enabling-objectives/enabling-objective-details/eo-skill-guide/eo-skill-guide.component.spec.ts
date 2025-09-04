import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EoSkillGuideComponent } from './eo-skill-guide.component';

describe('EoSkillGuideComponent', () => {
  let component: EoSkillGuideComponent;
  let fixture: ComponentFixture<EoSkillGuideComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EoSkillGuideComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EoSkillGuideComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
