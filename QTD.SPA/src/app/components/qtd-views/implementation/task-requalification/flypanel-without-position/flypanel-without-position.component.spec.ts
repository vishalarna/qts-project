import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelWithoutPositionComponent } from './flypanel-without-position.component';

describe('FlypanelWithoutPositionComponent', () => {
  let component: FlypanelWithoutPositionComponent;
  let fixture: ComponentFixture<FlypanelWithoutPositionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelWithoutPositionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelWithoutPositionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
