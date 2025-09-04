import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelProviderComponent } from './fly-panel-provider.component';

describe('FlyPanelProviderComponent', () => {
  let component: FlyPanelProviderComponent;
  let fixture: ComponentFixture<FlyPanelProviderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelProviderComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelProviderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
