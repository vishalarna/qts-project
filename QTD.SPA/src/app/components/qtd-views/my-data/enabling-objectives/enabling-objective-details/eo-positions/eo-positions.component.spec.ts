import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EoPositionsComponent } from './eo-positions.component';

describe('EoPositionsComponent', () => {
  let component: EoPositionsComponent;
  let fixture: ComponentFixture<EoPositionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EoPositionsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EoPositionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
