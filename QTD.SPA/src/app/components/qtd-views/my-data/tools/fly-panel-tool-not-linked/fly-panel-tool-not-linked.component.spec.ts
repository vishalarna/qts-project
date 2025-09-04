import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelToolNotLinkedComponent } from './fly-panel-tool-not-linked.component';

describe('FlyPanelToolNotLinkedComponent', () => {
  let component: FlyPanelToolNotLinkedComponent;
  let fixture: ComponentFixture<FlyPanelToolNotLinkedComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelToolNotLinkedComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelToolNotLinkedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
