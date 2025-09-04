import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelToolShLinkComponent } from './fly-panel-tool-sh-link.component';

describe('FlyPanelToolShLinkComponent', () => {
  let component: FlyPanelToolShLinkComponent;
  let fixture: ComponentFixture<FlyPanelToolShLinkComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelToolShLinkComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelToolShLinkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
