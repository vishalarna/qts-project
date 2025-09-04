import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelTestSettingComponent } from './fly-panel-test-setting.component';

describe('FlyPanelTestSettingComponent', () => {
  let component: FlyPanelTestSettingComponent;
  let fixture: ComponentFixture<FlyPanelTestSettingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelTestSettingComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelTestSettingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
