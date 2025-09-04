import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelAddToolComponent } from './fly-panel-add-tool.component';

describe('FlyPanelAddToolComponent', () => {
  let component: FlyPanelAddToolComponent;
  let fixture: ComponentFixture<FlyPanelAddToolComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelAddToolComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelAddToolComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
