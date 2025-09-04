import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelCreatePersonComponent } from './fly-panel-create-person.component';

describe('FlyPanelCreatePersonComponent', () => {
  let component: FlyPanelCreatePersonComponent;
  let fixture: ComponentFixture<FlyPanelCreatePersonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelCreatePersonComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelCreatePersonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
