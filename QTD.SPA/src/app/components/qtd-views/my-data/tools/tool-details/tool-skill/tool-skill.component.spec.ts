import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ToolSkillComponent } from './tool-skill.component';

describe('ToolSkillComponent', () => {
  let component: ToolSkillComponent;
  let fixture: ComponentFixture<ToolSkillComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ToolSkillComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ToolSkillComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
