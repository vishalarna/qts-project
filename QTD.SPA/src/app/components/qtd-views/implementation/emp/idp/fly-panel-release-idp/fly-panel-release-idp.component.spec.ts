import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelReleaseIdpComponent } from './fly-panel-release-idp.component';

describe('FlyPanelReleaseIdpComponent', () => {
  let component: FlyPanelReleaseIdpComponent;
  let fixture: ComponentFixture<FlyPanelReleaseIdpComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelReleaseIdpComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelReleaseIdpComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
