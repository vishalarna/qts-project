import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelLinkEmpOrganizationComponent } from './fly-panel-link-emp-organization.component';

describe('FlyPanelLinkEmpOrganizationComponent', () => {
  let component: FlyPanelLinkEmpOrganizationComponent;
  let fixture: ComponentFixture<FlyPanelLinkEmpOrganizationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelLinkEmpOrganizationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelLinkEmpOrganizationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
