import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelShRrLinkComponent } from './fly-panel-sh-rr-link.component';

describe('FlyPanelShRrLinkComponent', () => {
  let component: FlyPanelShRrLinkComponent;
  let fixture: ComponentFixture<FlyPanelShRrLinkComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelShRrLinkComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelShRrLinkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
