import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelAddSkaComponent } from './fly-panel-add-ska.component';

describe('FlyPanelAddSkaComponent', () => {
  let component: FlyPanelAddSkaComponent;
  let fixture: ComponentFixture<FlyPanelAddSkaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelAddSkaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelAddSkaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
