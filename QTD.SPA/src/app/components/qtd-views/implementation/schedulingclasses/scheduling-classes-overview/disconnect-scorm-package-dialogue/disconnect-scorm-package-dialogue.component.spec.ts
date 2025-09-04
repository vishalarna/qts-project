import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DisconnectScormPackageDialogueComponent } from './disconnect-scorm-package-dialogue.component';

describe('DisconnectScormPackageDialogueComponent', () => {
  let component: DisconnectScormPackageDialogueComponent;
  let fixture: ComponentFixture<DisconnectScormPackageDialogueComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DisconnectScormPackageDialogueComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DisconnectScormPackageDialogueComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
