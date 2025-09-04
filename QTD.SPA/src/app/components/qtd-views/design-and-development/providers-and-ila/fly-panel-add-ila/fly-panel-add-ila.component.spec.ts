import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelAddIlaComponent } from './fly-panel-add-ila.component';

describe('FlyPanelAddIlaComponent', () => {
  let component: FlyPanelAddIlaComponent;
  let fixture: ComponentFixture<FlyPanelAddIlaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelAddIlaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelAddIlaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
