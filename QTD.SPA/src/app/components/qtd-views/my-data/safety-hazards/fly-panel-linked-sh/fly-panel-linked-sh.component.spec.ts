import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelLinkedShComponent } from './fly-panel-linked-sh.component';

describe('FlyPanelLinkedShComponent', () => {
  let component: FlyPanelLinkedShComponent;
  let fixture: ComponentFixture<FlyPanelLinkedShComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelLinkedShComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelLinkedShComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
