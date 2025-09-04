import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelFilterIdpComponent } from './fly-panel-filter-idp.component';

describe('FlyPanelFilterIdpComponent', () => {
  let component: FlyPanelFilterIdpComponent;
  let fixture: ComponentFixture<FlyPanelFilterIdpComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelFilterIdpComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelFilterIdpComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
