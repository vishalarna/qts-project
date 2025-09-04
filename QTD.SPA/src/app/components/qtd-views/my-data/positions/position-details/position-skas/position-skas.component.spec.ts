import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PositionSkasComponent } from './position-skas.component';

describe('PositionSkasComponent', () => {
  let component: PositionSkasComponent;
  let fixture: ComponentFixture<PositionSkasComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PositionSkasComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PositionSkasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
