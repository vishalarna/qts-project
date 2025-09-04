import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelShEosLinkComponent } from './fly-panel-sh-eos-link.component';

describe('FlyPanelShEosLinkComponent', () => {
  let component: FlyPanelShEosLinkComponent;
  let fixture: ComponentFixture<FlyPanelShEosLinkComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelShEosLinkComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelShEosLinkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
