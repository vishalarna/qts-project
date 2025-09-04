import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelRrIlasLinkComponent } from './fly-panel-rr-ilas-link.component';

describe('FlyPanelRrIlasLinkComponent', () => {
  let component: FlyPanelRrIlasLinkComponent;
  let fixture: ComponentFixture<FlyPanelRrIlasLinkComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelRrIlasLinkComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelRrIlasLinkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
