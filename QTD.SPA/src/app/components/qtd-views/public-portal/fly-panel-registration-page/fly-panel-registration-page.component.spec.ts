import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelRegistrationPageComponent } from './fly-panel-registration-page.component';

describe('FlyPanelRegistrationPageComponent', () => {
  let component: FlyPanelRegistrationPageComponent;
  let fixture: ComponentFixture<FlyPanelRegistrationPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelRegistrationPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelRegistrationPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
