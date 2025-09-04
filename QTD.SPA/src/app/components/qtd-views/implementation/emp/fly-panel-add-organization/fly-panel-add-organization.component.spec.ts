import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelAddOrganizationComponent } from './fly-panel-add-organization.component';

describe('FlyPanelAddOrganizationComponent', () => {
  let component: FlyPanelAddOrganizationComponent;
  let fixture: ComponentFixture<FlyPanelAddOrganizationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelAddOrganizationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelAddOrganizationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
