import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelShIlasLinkComponent } from './fly-panel-sh-ilas-link.component';

describe('FlyPanelShIlasLinkComponent', () => {
  let component: FlyPanelShIlasLinkComponent;
  let fixture: ComponentFixture<FlyPanelShIlasLinkComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelShIlasLinkComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelShIlasLinkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
