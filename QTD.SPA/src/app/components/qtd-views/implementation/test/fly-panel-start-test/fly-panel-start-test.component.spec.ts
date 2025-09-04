import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelStartTestComponent } from './fly-panel-start-test.component';

describe('FlyPanelStartTestComponent', () => {
  let component: FlyPanelStartTestComponent;
  let fixture: ComponentFixture<FlyPanelStartTestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelStartTestComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelStartTestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
