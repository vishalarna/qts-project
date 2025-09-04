import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelDetailSelfRegistrationComponent } from './fly-panel-detail-self-registration.component';

describe('FlyPanelDetailSelfRegistrationComponent', () => {
  let component: FlyPanelDetailSelfRegistrationComponent;
  let fixture: ComponentFixture<FlyPanelDetailSelfRegistrationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelDetailSelfRegistrationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelDetailSelfRegistrationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
