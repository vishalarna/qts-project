import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelAddRRIssuingAuthorityComponent } from './fly-panel-add-rr-issuing-authority.component';

describe('FlyPanelAddRRIssuingAuthorityComponent', () => {
  let component: FlyPanelAddRRIssuingAuthorityComponent;
  let fixture: ComponentFixture<FlyPanelAddRRIssuingAuthorityComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelAddRRIssuingAuthorityComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelAddRRIssuingAuthorityComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
