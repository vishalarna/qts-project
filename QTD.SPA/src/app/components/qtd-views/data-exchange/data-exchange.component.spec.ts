import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DataExchangeComponent } from './data-exchange.component';

describe('DataExchangeComponent', () => {
  let component: DataExchangeComponent;
  let fixture: ComponentFixture<DataExchangeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DataExchangeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DataExchangeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
