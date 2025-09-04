import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelRrEosLinkComponent } from './fly-panel-rr-eos-link.component';

describe('FlyPanelRrEosLinkComponent', () => {
  let component: FlyPanelRrEosLinkComponent;
  let fixture: ComponentFixture<FlyPanelRrEosLinkComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelRrEosLinkComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelRrEosLinkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
