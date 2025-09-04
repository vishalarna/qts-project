import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelLinkedRRComponent } from './fly-panel-linked-rr.component';

describe('FlyPanelLinkedRRComponent', () => {
  let component: FlyPanelLinkedRRComponent;
  let fixture: ComponentFixture<FlyPanelLinkedRRComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelLinkedRRComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelLinkedRRComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
