import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelRrNotLinkedComponent } from './fly-panel-rr-not-linked.component';

describe('FlyPanelRrNotLinkedComponent', () => {
  let component: FlyPanelRrNotLinkedComponent;
  let fixture: ComponentFixture<FlyPanelRrNotLinkedComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelRrNotLinkedComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelRrNotLinkedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
