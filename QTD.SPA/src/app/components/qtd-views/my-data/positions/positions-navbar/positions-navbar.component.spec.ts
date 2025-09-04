import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PositionsNavbarComponent } from './positions-navbar.component';

describe('PositionsNavbarComponent', () => {
  let component: PositionsNavbarComponent;
  let fixture: ComponentFixture<PositionsNavbarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PositionsNavbarComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PositionsNavbarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
