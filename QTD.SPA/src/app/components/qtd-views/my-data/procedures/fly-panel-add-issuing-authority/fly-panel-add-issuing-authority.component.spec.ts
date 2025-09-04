import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelAddIssuingAuthorityComponent } from './fly-panel-add-issuing-authority.component';

describe('FlyPanelAddIssuingAuthorityComponent', () => {
  let component: FlyPanelAddIssuingAuthorityComponent;
  let fixture: ComponentFixture<FlyPanelAddIssuingAuthorityComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelAddIssuingAuthorityComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelAddIssuingAuthorityComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
