import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelContentComponent } from './fly-panel-content.component';

describe('FlyPanelContentComponent', () => {
  let component: FlyPanelContentComponent;
  let fixture: ComponentFixture<FlyPanelContentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelContentComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelContentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
